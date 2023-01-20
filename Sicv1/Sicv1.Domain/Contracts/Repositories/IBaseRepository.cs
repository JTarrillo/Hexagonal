using System;

namespace Sicv1.Domain.Contracts.Repositories
{
    public interface IRepositoryBase<T> : IDisposable where T : class
    {
    }
}
