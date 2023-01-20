using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            this._countryRepository = countryRepository;
        }

        public async Task<IEnumerable<Country>> GetCountry()
        {
            try
            {
                return await _countryRepository.GetCountry();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
        {
            _countryRepository.Dispose();
            GC.SuppressFinalize(this);
        }


        public async Task<int> SaveCountry(Country country)
        {
            try
            {
                return await _countryRepository.SaveCountry(country);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdateCountry(Country country)
        {
            try
            {
                return await _countryRepository.UpdateCountry(country);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> DeleteCountry(Country country)
        {
            try
            {
                return await _countryRepository.DeleteCountry(country);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> ActiveCountry(Country country)
        {
            try
            {
                return await _countryRepository.ActiveCountry(country);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
