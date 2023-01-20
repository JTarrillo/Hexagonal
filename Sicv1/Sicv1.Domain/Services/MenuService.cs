using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        public MenuService(IMenuRepository menuRepository)
        {
            this._menuRepository = menuRepository;
        }

        public async Task<IEnumerable<Menu>> GetMenusByUserId(int UserId)
        {
            try
            {
                return await _menuRepository.GetMenusByUserId(UserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
        {
            _menuRepository.Dispose();
            GC.SuppressFinalize(this);
        }

       
    }
}
