using Database.Entities.Common;
using Models.Dto.Countries.Output;

namespace Contracts.Services.Entity.Countries
{
    public interface ICountryService
    {
        /// <summary>
        /// Returns all <see cref="Country"/> for a dropdown menu
        /// </summary>
        /// <returns><see cref="IEnumerable"/> of <see cref="CountryDDMDto"/></returns>
        Task<IEnumerable<CountryDDMDto>> GetCountriesDropdownAsync();
    }
}
