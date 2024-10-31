using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Customers;
using Database.Entities.Addresses;
using Database.Entities.Common.Enums.Statuses;
using Database.Entities.Common.Nomenclatures.Statuses;
using Database.Entities.Customers;
using Database.Entities.Emails;
using Database.Entities.Identity;
using Database.Entities.PhoneNumbers;
using Models.Common;
using Models.Common.Enums;
using Models.Dto.Addresses;
using Models.Dto.Customers;
using Models.Dto.Emails;
using Models.Dto.PhoneNumbers;

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

            //await unitOfWork.SaveChangesAsync();

            return operationResult;
        }

        public async Task<OperationResult<Customer>> GetCustomerWithPersonalDetailsAsync(string userId)
        {
            var operationResult = new OperationResult<Customer>();
            var customer = await unitOfWork.CustomerRepository.GetCustomerWithPersonalDetailsAsync(userId);

            if (customer is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.NotFound, $"{nameof(Customer)} with user id - {userId} was not found!"));
            }

            operationResult.Data = customer;

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
