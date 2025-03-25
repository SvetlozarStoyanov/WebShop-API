using Models.Common;

namespace Contracts.Services.Seeding
{
    public interface ISeedingService
    {
        /// <summary>
        /// Seeds the Database
        /// </summary>
        /// <returns><see cref="OperationResult"/> result</returns>
        Task<OperationResult> SeedAsync();
    }
}
