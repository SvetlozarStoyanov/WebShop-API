using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Addresses;
using Contracts.Services.Entity.Customers;
using Contracts.Services.Entity.Emails;
using Contracts.Services.Entity.PhoneNumbers;
using Contracts.Services.Managers.Customers;
using Models.Common;
using Models.Dto.Addresses.Input;
using Models.Dto.Emails.Input;
using Models.Dto.PhoneNumbers;

namespace Services.Managers.Customers
{
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
    }
}
