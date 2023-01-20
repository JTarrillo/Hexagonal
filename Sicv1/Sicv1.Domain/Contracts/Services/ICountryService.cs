using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Contracts.Services
{
    public interface ICountryService : IDisposable
    {   
        Task<IEnumerable<Country>> GetCountry();
        Task<int> SaveCountry(Country country);
        Task<int> UpdateCountry(Country country);
        Task<int> DeleteCountry(Country country);
        Task<int> ActiveCountry(Country country);
        

    }
}
