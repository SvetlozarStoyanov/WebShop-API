using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Addresses;
using Contracts.Services.Entity.Customers;
using Contracts.Services.Entity.Emails;
using Contracts.Services.Entity.PhoneNumbers;
using Contracts.Services.Managers.Customers;
using Models.Common;
using Models.Dto.Customers;

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

        public async Task<OperationResult> UpdateCustomerAsync(string userId, CustomerEditDto customerEditDto)
        {
            var operationResult = new OperationResult();

            var getCustomerOperationResult = await customerService.GetCustomerWithPersonalDetailsAsync(userId);

            if (!getCustomerOperationResult.IsSuccessful)
            {
                operationResult.AppendErrors(getCustomerOperationResult);
                return operationResult;
            }

            var customer = getCustomerOperationResult.Data;

            if (customerEditDto.UpdatedAddresses != null)
            {
                operationResult.AppendErrors(await addressService.UpdateCustomerAddressesAsync(customer.Addresses, customerEditDto.UpdatedAddresses));
                if (!operationResult.IsSuccessful)
                {
                    return operationResult;
                }
            }

            if (customerEditDto.UpdatedPhoneNumbers != null)
            {
                operationResult.AppendErrors(await phoneNumberService.UpdateCustomerPhoneNumbersAsync(customer.PhoneNumbers, customerEditDto.UpdatedPhoneNumbers));
                if (!operationResult.IsSuccessful)
                {
                    return operationResult;
                }
            }

            if (customerEditDto.UpdatedEmails != null)
            {
                operationResult.AppendErrors(await emailService.UpdateCustomerEmailsAsync(customer.Emails, customerEditDto.UpdatedEmails));
                if (!operationResult.IsSuccessful)
                {
                    return operationResult;
                }
            }

            await unitOfWork.SaveChangesAsync();

            return operationResult;
        }
    }
}
