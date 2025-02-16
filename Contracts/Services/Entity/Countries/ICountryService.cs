using Models.Dto.Countries.Output;

namespace Contracts.Services.Entity.Countries
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryPhoneCodeDDMDto>> GetCountriesAndPhoneCodesDropdownAsync();
        Task<IEnumerable<CountryDDMDto>> GetCountriesDropdownAsync();
    }
}
