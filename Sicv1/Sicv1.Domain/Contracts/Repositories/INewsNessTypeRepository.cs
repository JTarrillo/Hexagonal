using Sicv1.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Contracts.Repositories
{
    public interface INewsNessTypeRepository : IRepositoryBase<NewsNessType>
    {
        Task<IEnumerable<NewsNessType>> GetNewsNessesType();
    }
}