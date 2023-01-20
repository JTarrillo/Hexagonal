using Sicv1.Domain.Entities;
using System.Threading.Tasks;

namespace Sicv1.Domain.Contracts.Repositories
{
    public interface ICategoryCardRepository : IRepositoryBase<CategoryCard>
    {
        Task<int> Save(CategoryCard categoryCard);
        Task<int> Update(CategoryCard categoryCard);
    }
}
