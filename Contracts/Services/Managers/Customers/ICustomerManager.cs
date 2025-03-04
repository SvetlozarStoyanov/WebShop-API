using Models.Common;
using Models.Dto.Customers;

namespace Contracts.Services.Managers.Customers
{
    public interface ICustomerManager
    {
        Task<OperationResult> UpdateCustomerAsync(string userId, CustomerEditDto customerEditDto);
    }
}
