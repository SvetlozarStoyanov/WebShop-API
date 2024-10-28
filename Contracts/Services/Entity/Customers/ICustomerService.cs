using Database.Entities.Identity;
using Models.Common;

namespace Contracts.Services.Entity.Customers
{
    public interface ICustomerService
    {
        Task<OperationResult> CreateCustomerAsync(ApplicationUser user);
    }
}
