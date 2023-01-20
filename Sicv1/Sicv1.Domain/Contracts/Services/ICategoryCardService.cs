using Sicv1.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Sicv1.Domain.Contracts.Services
{
    public interface ICategoryCardService : IDisposable
    {
        Task<int> Save(CategoryCard categoryCard);
        Task<int> Update(CategoryCard categoryCard);
    }
}
