using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Sicv1.Domain.Services
{
    public class PopupConfigurationService : IPopupConfigurationService
    {
        private readonly IPopupConfigurationRepository _popupConfigurationRepository;
        public PopupConfigurationService(IPopupConfigurationRepository popupConfigurationRepository)
        {
            this._popupConfigurationRepository = popupConfigurationRepository;
        }

        public async Task<PopupConfiguration> GetPopupConfigurations()
        {
            try
            {
                return await _popupConfigurationRepository.GetPopupConfigurations();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Save(PopupConfiguration popupConfiguration)
        {
            try
            {
                return await _popupConfigurationRepository.Save(popupConfiguration);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(PopupConfiguration popupConfiguration)
        {
            try
            {
                return await _popupConfigurationRepository.Update(popupConfiguration);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
        {
            _popupConfigurationRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
