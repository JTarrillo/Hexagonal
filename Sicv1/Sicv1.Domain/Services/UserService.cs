using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<User> SignIn(string UserName, string Password)
        {
            try
            {
                return await _userRepository.SignIn(UserName, Password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Save(User user)
        {
            try
            {
                //if (user.DOCUMENT == await ValidateNumDocument(user.DOCUMENT, user.ID))
                //{
                //    return -1;
                //}
                return await _userRepository.Save(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> RegeneratePassword(User user)
        {
            try
            {
                //if (user.DOCUMENT == await ValidateNumDocument(user.DOCUMENT, user.ID))
                //{
                //    return -1;
                //}
                return await _userRepository.RegeneratePassword(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(User user)
        {
            try
            {
                //if (user.DOCUMENT == await ValidateNumDocument(user.DOCUMENT, user.ID)) 
                //{
                //    return -1;
                //}
                return await _userRepository.Update(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdateStatus(int id)
        {
            try
            {
                //if (user.DOCUMENT == await ValidateNumDocument(user.DOCUMENT, user.ID)) 
                //{
                //    return -1;
                //}
                return await _userRepository.UpdateStatus(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
        {
            _userRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<User>> GetUsers(string NumDniSearch = null, int? RoleId = null, decimal? CurrentPage = null, decimal? RecordsPerPage = null)
        {
            try
            {
                return await _userRepository.GetUsers(NumDniSearch, RoleId, CurrentPage, RecordsPerPage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> GetCount(int? roleId = -1)
        {
            try
            {
                return await _userRepository.GetCount(roleId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> ValidateNumDocument(string num, int userId)
        {
            try
            {
                return await _userRepository.ValidateNumDocument(num, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<User> ExportToExcel(string fi, string ff)
        {
            try
            {
                return _userRepository.ExportToExcel(fi, ff);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<User> ExportToExcelLM(string fi, string ff)
        {
            try
            {
                return _userRepository.ExportToExcelLM(fi, ff);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<User>> GetUsersAll(int? RoleId = null)
        {
            try
            {
                return await _userRepository.GetUsersAll(RoleId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetUsersDetail(int id)
        {
            try
            {
                return await _userRepository.GetUsersDetail(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
