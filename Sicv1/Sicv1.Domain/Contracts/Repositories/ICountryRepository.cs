using Sicv1.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Contracts.Repositories
{
    public interface ICountryRepository : IRepositoryBase<Country>
    {
        Task<IEnumerable<Country>> GetCountry();
        Task<int> SaveCountry(Country country);

        Task<int> UpdateCountry(Country country);

        Task<int> DeleteCountry(Country country);

        Task<int> ActiveCountry(Country country);
    }
}
