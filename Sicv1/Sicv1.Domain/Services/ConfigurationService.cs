using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfigurationRepository configurationRepository;
        public ConfigurationService(IConfigurationRepository con)
        {
            this.configurationRepository = con;
        }

        public async Task<ConfigurationResponseBE> UpdateConfiguration(Configuration conf)
        {
                return await configurationRepository.UpdateConfiguration(conf);
        }

        public async Task<ConfigurationResponseBE> GetValuesCurrent()
        {
            return await configurationRepository.GetValuesCurrent();
        }


        public void Dispose()
        {
            configurationRepository.Dispose();
            GC.SuppressFinalize(this);
        }


    }
}