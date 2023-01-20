using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Contracts.Services
{
    public interface INewsNessService : IDisposable
    {
        Task<IEnumerable<NewsNess>> GetNewsNesses();
        Task<IEnumerable<NewsNessType>> GetNewsNessType();
        Task<int> SaveUpdate(NewsNess model);       
        Task<int> SaveNewsness(NewsNess newsness);
        Task<int> UpdateNewsness(NewsNess newsness);
        Task<int> DeleteNewsness(NewsNess newsness);
    }
}