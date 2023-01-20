using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Contracts.Services
{
    public interface IRoleService : IDisposable
    {
        Task<IEnumerable<Role>> GetRoles(int roleId);
        Task<int> Save(Role role);
        Task<int> Update(Role role);
    }
}
