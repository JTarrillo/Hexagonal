using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Contracts.Services
{
    public interface IConfigurationService : IDisposable
    {
        Task<ConfigurationResponseBE> UpdateConfiguration(Configuration conf);
        Task<ConfigurationResponseBE> GetValuesCurrent();
    }
}