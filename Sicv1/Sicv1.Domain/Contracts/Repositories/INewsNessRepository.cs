using Sicv1.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Contracts.Repositories
{
    public interface INewsNessRepository : IRepositoryBase<NewsNess>
    {
        Task<IEnumerable<NewsNess>> GetNewsNesses();
        Task<IEnumerable<NewsNessType>> GetNewsNessType();
        Task<int> SaveUpdate(NewsNess model);
        Task<int> SaveNewsness(NewsNess newsNess);
        Task<int> UpdateNewsness(NewsNess newsNess);
        Task<int> DeleteNewsness(NewsNess newsNess);
    }
}