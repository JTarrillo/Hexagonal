using Sicv1.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Contracts.Repositories
{
    public interface IUbigeoRepository : IRepositoryBase<Ubigeo>
    {
        Task<IEnumerable<Ubigeo>> GetDptos();
        Task<IEnumerable<Ubigeo>> GetProvs(string DptoId = null);
        Task<IEnumerable<Ubigeo>> GetDists(string DptoId = null, string ProvId = null);
    }
}