using Sicv1.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Sicv1.Domain.Contracts.Services
{
    public interface IPopupConfigurationService : IDisposable
    {
        Task<int> Save(PopupConfiguration popupConfiguration);
        Task<int> Update(PopupConfiguration popupConfiguration);
        Task<PopupConfiguration> GetPopupConfigurations();
    }
}