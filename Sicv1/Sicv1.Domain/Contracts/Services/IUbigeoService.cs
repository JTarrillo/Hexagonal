using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Contracts.Services
{
    public interface IUbigeoService : IDisposable
    {
        Task<IEnumerable<Ubigeo>> GetDptos();
        Task<IEnumerable<Ubigeo>> GetProvs(string DptoId = null);
        Task<IEnumerable<Ubigeo>> GetDists(string DptoId = null, string ProvId = null);
    }
}
