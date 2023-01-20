using Sicv1.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Contracts.Repositories
{
    public interface IBranchOfficesRepository : IRepositoryBase<BranchOffices>
    {
        Task<IEnumerable<BranchOffices>> GetBranchOffices();
        Task<int> SaveBranchOffices(BranchOffices branchOffices);

        Task<int> UpdateBranchOffices(BranchOffices branchOffices);

        Task<int> DeleteBranchOffices(BranchOffices branchOffices);

        //Task<int> ActiveBranchOffices(BranchOffices branchOffices);
    }
}
