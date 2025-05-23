﻿using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Countries;
using Microsoft.EntityFrameworkCore;
using Models.Dto.Countries.Output;

namespace Services.Entity.Countries
{

    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork unitOfWork;

        public CountryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CountryDDMDto>> GetCountriesDropdownAsync()
        {
            var countries = await unitOfWork.CountryRepository.AllAsNoTracking()
                .Select(x => new CountryDDMDto()
                {
                    Id = x.Id,
                    Name = $"{x.Name}",
                    PhoneCode = x.PhoneCode
                })
                .ToListAsync();

            return countries;
        }
    }
}
