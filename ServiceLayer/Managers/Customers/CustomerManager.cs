using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Addresses;
using Contracts.Services.Entity.Customers;
using Contracts.Services.Entity.Emails;
using Contracts.Services.Entity.PhoneNumbers;
using Contracts.Services.Managers.Customers;
using Database.Entities.Customers;
using Database.Entities.Identity;
using Models.Common;
using Models.Dto.Addresses.Input;
using Models.Dto.Addresses.Output;
using Models.Dto.Customers.Input;
using Models.Dto.Customers.Output;
using Models.Dto.Emails.Input;
using Models.Dto.Emails.Output;
using Models.Dto.PhoneNumbers;
using Models.Dto.PhoneNumbers.Output;

namespace Services.Managers.Customers
{
    /// <summary>
    /// Performs <see cref="Customer"/> related operations
    /// </summary>
    public class CustomerManager : ICustomerManager
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICustomerService customerService;
        private readonly IAddressService addressService;
        private readonly IPhoneNumberService phoneNumberService;
        private readonly IEmailService emailService;

        public CustomerManager(IUnitOfWork unitOfWork,
            ICustomerService customerService,
            IAddressService addressService,
            IPhoneNumberService phoneNumberService,
            IEmailService emailService)
        {
            this.unitOfWork = unitOfWork;
            this.customerService = customerService;
            this.addressService = addressService;
            this.phoneNumberService = phoneNumberService;
            this.emailService = emailService;
        }

        ///<inheritdoc/>
        public async Task<OperationResult<IEnumerable<AddressDetailsDto>>> GetCustomerAddressesAsync(string userId)
        {
            return await addressService.GetCustomerAddressesAsync(userId);
        }

        ///<inheritdoc/>
        public async Task<OperationResult<IEnumerable<PhoneNumberDetailsDto>>> GetCustomerPhoneNumbersAsync(string userId)
        {
            return await phoneNumberService.GetCustomerPhoneNumbersAsync(userId);
        }

        ///<inheritdoc/>
        public async Task<OperationResult<IEnumerable<EmailDetailsDto>>> GetCustomerEmailsAsync(string userId)
        {
            return await emailService.GetCustomerEmailsAsync(userId);
        }

        ///<inheritdoc/>
        public async Task<OperationResult> UpdateCustomerAddressesAsync(string userId, IEnumerable<AddressUpdateDto> addressUpdateDtos)
        {
            var operationResult = new OperationResult();

            var getCustomerOperationResult = await customerService.GetCustomerWithAddressesAsync(userId);

            if (!getCustomerOperationResult.IsSuccessful)
            {
                operationResult.AppendErrors(getCustomerOperationResult);
                return operationResult;
            }

            var customer = getCustomerOperationResult.Data;

            operationResult.AppendErrors(await addressService.UpdateCustomerAddressesAsync(customer.Addresses, addressUpdateDtos));
            if (!operationResult.IsSuccessful)
            {
                return operationResult;
            }

            await unitOfWork.SaveChangesAsync();

            return operationResult;
        }

        ///<inheritdoc/>
        public async Task<OperationResult> UpdateCustomerPhoneNumbersAsync(string userId, IEnumerable<PhoneNumberUpdateDto> phoneNumberUpdateDtos)
        {
            var operationResult = new OperationResult();

            var getCustomerOperationResult = await customerService.GetCustomerWithPhoneNumbersAsync(userId);

            if (!getCustomerOperationResult.IsSuccessful)
            {
                operationResult.AppendErrors(getCustomerOperationResult);
                return operationResult;
            }

            var customer = getCustomerOperationResult.Data;

            operationResult.AppendErrors(await phoneNumberService.UpdateCustomerPhoneNumbersAsync(customer.PhoneNumbers, phoneNumberUpdateDtos));
            if (!operationResult.IsSuccessful)
            {
                return operationResult;
            }

            await unitOfWork.SaveChangesAsync();

            return operationResult;
        }

        ///<inheritdoc/>
        public async Task<OperationResult> UpdateCustomerEmailsAsync(string userId, IEnumerable<EmailUpdateDto> emailUpdateDtos)
        {
            var operationResult = new OperationResult();
            var getCustomerOperationResult = await customerService.GetCustomerWithEmailsAsync(userId);

            if (!getCustomerOperationResult.IsSuccessful)
            {
                operationResult.AppendErrors(getCustomerOperationResult);
                return operationResult;
            }

            var customer = getCustomerOperationResult.Data;

            operationResult.AppendErrors(await emailService.UpdateCustomerEmailsAsync(customer.Emails, emailUpdateDtos));
            if (!operationResult.IsSuccessful)
            {
                return operationResult;
            }

            await unitOfWork.SaveChangesAsync();

            return operationResult;
        }

        ///<inheritdoc/>
        public async Task<OperationResult<CustomerDetailsDto>> GetCustomerDetailsAsync(string userId)
        {
            var operationResult = await customerService.GetCustomerDetailsAsync(userId);

            return operationResult;
        }

        ///<inheritdoc/>
        public async Task<OperationResult> CreateCustomerAsync(ApplicationUser user, CustomerRegisterDto customerRegisterDto)
        {
            return await customerService.CreateCustomerAsync(user, customerRegisterDto);
        }
    }
}
