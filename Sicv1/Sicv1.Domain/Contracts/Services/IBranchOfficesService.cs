using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Contracts.Services
{
    public interface IBranchOfficesService : IDisposable
    {
        Task<IEnumerable<BranchOffices>> GetBranchOffices();
        Task<int> SaveBranchOffices(BranchOffices branchOffices);
        Task<int> UpdateBranchOffices(BranchOffices branchOffices);
        Task<int> DeleteBranchOffices(BranchOffices branchOffices);
    }
}