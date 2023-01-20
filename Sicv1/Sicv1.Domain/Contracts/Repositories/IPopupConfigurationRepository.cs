using Sicv1.Domain.Entities;
using System.Threading.Tasks;

namespace Sicv1.Domain.Contracts.Repositories
{
    public interface IPopupConfigurationRepository : IRepositoryBase<PopupConfiguration>
    {
        Task<int> Save(PopupConfiguration popupConfiguration);
        Task<int> Update(PopupConfiguration popupConfiguration);
        Task<PopupConfiguration> GetPopupConfigurations();
    }
}