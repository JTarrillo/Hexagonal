using Sicv1.Domain.Entities;
using System.Threading.Tasks;

namespace Sicv1.Domain.Contracts.Repositories
{
    public interface IConfigurationRepository : IRepositoryBase<Configuration>
    {
        Task<ConfigurationResponseBE> UpdateConfiguration(Configuration conf);
        Task<ConfigurationResponseBE> GetValuesCurrent();
    }
}