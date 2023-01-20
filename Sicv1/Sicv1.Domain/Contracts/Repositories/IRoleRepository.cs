using Sicv1.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Contracts.Repositories
{
    public interface IRoleRepository : IRepositoryBase<Role>
    {
        Task<IEnumerable<Role>> GetRoles(int RoleId);
        Task<int> Save(Role role);
        Task<int> Update(Role role);
    }
}
