using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Customers;
using Database.Entities.Customers;
using Database.Entities.Identity;
using Models.Common;
using Models.Common.Enums;

namespace Services.Entity.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> CreateCustomerAsync(ApplicationUser user)
        {
            var operationResult = new OperationResult();

            if (user is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.InternalServerError, "User cannot be null!"));
                return operationResult;
            }

            Customer customer = new Customer()
            {
                User = user,
            };

            await unitOfWork.CustomerRepository.AddAsync(customer);

            //await unitOfWork.SaveChangesAsync();

            return operationResult;
        }
    }
}
