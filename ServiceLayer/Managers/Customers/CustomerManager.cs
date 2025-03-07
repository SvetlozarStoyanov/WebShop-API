using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Addresses;
using Contracts.Services.Entity.Customers;
using Contracts.Services.Entity.Emails;
using Contracts.Services.Entity.PhoneNumbers;
using Contracts.Services.Managers.Customers;
using Models.Common;
using Models.Dto.Addresses.Input;
using Models.Dto.Emails.Input;
using Models.Dto.PhoneNumbers.Input;

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

        public async Task<OperationResult> UpdateCustomerAddressesAsync(string userId, IEnumerable<AddressEditDto> AddressEditDtos)
        {
            var operationResult = new OperationResult();

            var getCustomerOperationResult = await customerService.GetCustomerWithAddressesAsync(userId);

            if (!getCustomerOperationResult.IsSuccessful)
            {
                operationResult.AppendErrors(getCustomerOperationResult);
                return operationResult;
            }

            var customer = getCustomerOperationResult.Data;

            operationResult.AppendErrors(await addressService.UpdateCustomerAddressesAsync(customer.Addresses, AddressEditDtos));
            if (!operationResult.IsSuccessful)
            {
                return operationResult;
            }

            await unitOfWork.SaveChangesAsync();

            return operationResult;
        }

        public async Task<OperationResult> UpdateCustomerPhoneNumbersAsync(string userId, UpdatePhoneNumbersDto updatePhoneNumbersDto)
        {
            var operationResult = new OperationResult();

            var getCustomerOperationResult = await customerService.GetCustomerWithPhoneNumbersAsync(userId);

            if (!getCustomerOperationResult.IsSuccessful)
            {
                operationResult.AppendErrors(getCustomerOperationResult);
                return operationResult;
            }

            var customer = getCustomerOperationResult.Data;

            operationResult.AppendErrors(await phoneNumberService.UpdateCustomerPhoneNumbersAsync(customer.PhoneNumbers, updatePhoneNumbersDto));
            if (!operationResult.IsSuccessful)
            {
                return operationResult;
            }

            return operationResult;
        }

        public async Task<OperationResult> UpdateCustomerEmailsAsync(string userId, UpdateEmailsDto updateEmailsDto)
        {
            var operationResult = new OperationResult();
            var getCustomerOperationResult = await customerService.GetCustomerWithEmailsAsync(userId);

            if (!getCustomerOperationResult.IsSuccessful)
            {
                operationResult.AppendErrors(getCustomerOperationResult);
                return operationResult;
            }

            var customer = getCustomerOperationResult.Data;

            operationResult.AppendErrors(await emailService.UpdateCustomerEmailsAsync(customer.Emails, updateEmailsDto));
            if (!operationResult.IsSuccessful)
            {
                return operationResult;
            }


            return operationResult;
        }
    }
}
