using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Customers;
using Database.Entities.Addresses;
using Database.Entities.Common.Enums.Statuses;
using Database.Entities.Common.Nomenclatures.Statuses;
using Database.Entities.Customers;
using Database.Entities.Emails;
using Database.Entities.Identity;
using Database.Entities.PhoneNumbers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Models.Common;
using Models.Common.Enums;
using Models.Dto.Addresses.Input;
using Models.Dto.Addresses.Output;
using Models.Dto.Customers.Input;
using Models.Dto.Customers.Output;
using Models.Dto.Emails.Input;
using Models.Dto.Emails.Output;
using Models.Dto.PhoneNumbers.Input;
using Models.Dto.PhoneNumbers.Output;
using Models.Dto.Users.Output;

namespace Services.Entity.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> CreateCustomerAsync(ApplicationUser user, CustomerRegisterDto customerRegisterDto)
        {
            var operationResult = new OperationResult();

            if (user is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.InternalServerError, "User cannot be null!"));
                return operationResult;
            }

            var createAddressesOperationResult = await CreateCustomerAddressesAsync(customerRegisterDto.Addresses);
            var createPhoneNumbersOperationResult = await CreateCustomerPhoneNumbersAsync(customerRegisterDto.PhoneNumbers);
            var createEmailsOperationResult = await CreateCustomerEmailsAsync(customerRegisterDto.Emails);

            if (!createAddressesOperationResult.IsSuccessful)
            {
                operationResult.AppendErrors(createAddressesOperationResult);
                return operationResult;
            }

            if (!createPhoneNumbersOperationResult.IsSuccessful)
            {
                operationResult.AppendErrors(createPhoneNumbersOperationResult);
                return operationResult;
            }

            if (!createEmailsOperationResult.IsSuccessful)
            {
                operationResult.AppendErrors(createEmailsOperationResult);
                return operationResult;
            }

            Customer customer = new Customer()
            {
                User = user,
                Addresses = createAddressesOperationResult.Data,
                PhoneNumbers = createPhoneNumbersOperationResult.Data,
                Emails = createEmailsOperationResult.Data
            };

            await unitOfWork.CustomerRepository.AddAsync(customer);

            // Commented because save changes is called elsewhere in the logic this method is used in
            //await unitOfWork.SaveChangesAsync();

            return operationResult;
        }

        public async Task<OperationResult<Customer>> GetCustomerWithOrdersAsync(string userId)
        {
            var operationResult = new OperationResult<Customer>();

            var customer = await unitOfWork.CustomerRepository.GetCustomerWithOrdersAsync(userId);

            if (customer is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.NotFound, $"{nameof(Customer)} with userId: {userId} was not found!"));
                return operationResult;
            }

            operationResult.Data = customer;
            return operationResult;
        }

        public async Task<OperationResult<Customer>> GetCustomerWithAddressesAsync(string userId)
        {
            var operationResult = new OperationResult<Customer>();

            var customerWithAddresses = await unitOfWork.CustomerRepository.GetCustomerWithAddressesAsync(userId);

            if (customerWithAddresses is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            operationResult.Data = customerWithAddresses;

            return operationResult;
        }

        public async Task<OperationResult<Customer>> GetCustomerWithPhoneNumbersAsync(string userId)
        {
            var operationResult = new OperationResult<Customer>();

            var customerWithPhoneNumbers = await unitOfWork.CustomerRepository.GetCustomerWithPhoneNumbersAsync(userId);

            if (customerWithPhoneNumbers is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            operationResult.Data = customerWithPhoneNumbers;

            return operationResult;
        }

        public async Task<OperationResult<Customer>> GetCustomerWithEmailsAsync(string userId)
        {
            var operationResult = new OperationResult<Customer>();

            var customerWithEmails = await unitOfWork.CustomerRepository.GetCustomerWithEmailsAsync(userId);

            if (customerWithEmails is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            operationResult.Data = customerWithEmails;

            return operationResult;
        }


        public async Task<OperationResult<CustomerDetailsDto>> GetCustomerDetailsAsync(string userId)
        {
            var operationResult = new OperationResult<CustomerDetailsDto>();
            var customer = await unitOfWork.CustomerRepository.GetCustomerWithPersonalDetailsAsNoTrackingAsync(userId);

            if (customer is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            var userDto = new CustomerDetailsDto()
            {
                User = new UserDetailsDto()
                {
                    UserName = customer.User.UserName,
                    FirstName = customer.User.FirstName,
                    MiddleName = customer.User.MiddleName,
                    LastName = customer.User.LastName,
                },
                Addresses = customer.Addresses.Select(x => new AddressDetailsDto
                {
                    Id = x.Id,
                    AddressLineOne = x.AddressLineOne,
                    AddressLineTwo = x.AddressLineTwo,
                    City = x.City,
                    PostCode = x.PostCode,
                    CountryId = x.CountryId,
                    CountryName = x.Country.Name,
                    IsMain = x.IsMain,
                }),
                PhoneNumbers = customer.PhoneNumbers.Select(x => new PhoneNumberDetailsDto()
                {
                    Id = x.Id,
                    Number = x.Number,
                    CountryId = x.CountryId,
                    PhoneCode = x.Country.PhoneCode,
                    CountryName = x.Country.Name,
                    IsMain = x.IsMain,
                }),
                Emails = customer.Emails.Select(x => new EmailDetailsDto()
                {
                    Id = x.Id,
                    Address = x.Address,
                    IsMain = x.IsMain
                })
            };

            operationResult.Data = userDto;

            return operationResult;
        }

        private async Task<OperationResult<List<Address>>> CreateCustomerAddressesAsync(IEnumerable<AddressCreateDto> addressDtos)
        {
            var operationResult = new OperationResult<List<Address>>();

            if (addressDtos.Count(x => x.IsMain) != 1)
            {
                operationResult.AppendError(new Error(ErrorTypes.BadRequest, "Customer must have 1 main address!"));
                return operationResult;
            }

            var addresses = new List<Address>();

            foreach (var addressDto in addressDtos)
            {
                var activeStatus = await unitOfWork.AddressStatusRepository.GetByIdAsync((long)AddressStatuses.Active);

                if (activeStatus is null)
                {
                    throw new InvalidOperationException($"{nameof(AddressStatus)} of type: {AddressStatuses.Active.ToString()} was not found!");
                }

                var country = await unitOfWork.CountryRepository.GetByIdAsync(addressDto.CountryId);

                if (country is null)
                {
                    operationResult.AppendError(new Error(ErrorTypes.NotFound, $"Country with id - {addressDto.CountryId} was not found!"));
                    return operationResult;
                }

                var address = new Address()
                {
                    AddressLineOne = addressDto.AddressLineOne,
                    AddressLineTwo = addressDto.AddressLineTwo,
                    City = addressDto.City,
                    PostCode = addressDto.PostCode,
                    IsMain = addressDto.IsMain,
                    Status = activeStatus,
                    Country = country,
                };

                addresses.Add(address);
            }

            operationResult.Data = addresses;

            return operationResult;
        }

        private async Task<OperationResult<List<PhoneNumber>>> CreateCustomerPhoneNumbersAsync(IEnumerable<PhoneNumberCreateDto> phoneNumberDtos)
        {
            var operationResult = new OperationResult<List<PhoneNumber>>();

            if (phoneNumberDtos.Count(x => x.IsMain) != 1)
            {
                operationResult.AppendError(new Error(ErrorTypes.BadRequest, "Customer must have only 1 main phone number!"));
                return operationResult;
            }

            var phoneNumbers = new List<PhoneNumber>();

            foreach (var phoneNumberDto in phoneNumberDtos)
            {
                var activeStatus = await unitOfWork.PhoneNumberStatusRepository.GetByIdAsync((long)PhoneNumberStatuses.Active);

                if (activeStatus is null)
                {
                    throw new InvalidOperationException($"{nameof(PhoneNumberStatus)} of type: {PhoneNumberStatuses.Active.ToString()} was not found!");
                }

                var country = await unitOfWork.CountryRepository.GetByIdAsync(phoneNumberDto.CountryId);

                if (country is null)
                {
                    operationResult.AppendError(new Error(ErrorTypes.NotFound, $"Country with id - {phoneNumberDto.CountryId} was not found!"));
                    return operationResult;
                }

                var phoneNumber = new PhoneNumber()
                {
                    Number = phoneNumberDto.Number,
                    IsMain = phoneNumberDto.IsMain,
                    Status = activeStatus,
                    Country = country,
                };

                phoneNumbers.Add(phoneNumber);
            }

            operationResult.Data = phoneNumbers;

            return operationResult;
        }

        private async Task<OperationResult<List<Email>>> CreateCustomerEmailsAsync(IEnumerable<EmailCreateDto> emailDtos)
        {
            var operationResult = new OperationResult<List<Email>>();

            if (emailDtos.Count(x => x.IsMain) != 1)
            {
                operationResult.AppendError(new Error(ErrorTypes.BadRequest, "Customer must have 1 main email!"));
                return operationResult;
            }

            var emails = new List<Email>();

            foreach (var emailDto in emailDtos)
            {
                var activeStatus = await unitOfWork.EmailStatusRepository.GetByIdAsync((long)EmailStatuses.Active);

                if (activeStatus is null)
                {
                    throw new InvalidOperationException($"{nameof(EmailStatus)} of type: {EmailStatuses.Active.ToString()} was not found!");
                }

                var email = new Email()
                {
                    Address = emailDto.Address,
                    IsMain = emailDto.IsMain,
                    Status = activeStatus,
                };

                emails.Add(email);
            }

            operationResult.Data = emails;

            return operationResult;
        }
    }
}
