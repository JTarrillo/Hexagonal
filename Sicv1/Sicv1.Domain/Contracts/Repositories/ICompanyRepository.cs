using Sicv1.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Contracts.Repositories
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        Task<IEnumerable<Company>> GetCompaniesByUserId(int Id);
        Task<IEnumerable<Company>> GetCompaniesAll();
        Task<int> Save(Company company);
        Task<int> Update(Company company);
        Task<int> Remove(int Id);
        Task<IEnumerable<Company>> GetCompanies();
        Task<IEnumerable<Company>> listCompanies();
        Task<int> CheckIfHaveLifeMiles(int CompanyId);
        Task<int> UpdateStatus(int id);
    }
}
