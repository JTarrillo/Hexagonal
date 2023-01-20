using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Contracts.Services
{
    public interface IUserService : IDisposable
    {
        Task<User> SignIn(string UserName, string Password);
        Task<int> Save(User user);
        Task<int> Update(User user);
        Task<string> ValidateNumDocument(string num, int userId);
        IEnumerable<User> ExportToExcel(string fi, string ff);
        IEnumerable<User> ExportToExcelLM(string fi, string ff);
        Task<int> GetCount(int? roleId = -1);

        /// <summary>
        /// Paginación en la tabla HTML de usuarios
        /// </summary>
        /// <param name="NumDniSearch"></param>
        /// <param name="RoleId"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="intRecordsPerPage"></param>
        /// <returns></returns>
        Task<IEnumerable<User>> GetUsers(string NumDniSearch = null, int? RoleId = null, decimal? CurrentPage = null, decimal? RecordsPerPage = null);
        Task<IEnumerable<User>> GetUsersAll(int? RoleId = null);
        Task<User> GetUsersDetail(int id);
        Task<int> RegeneratePassword(User user);
        Task<int> UpdateStatus(int id);


    }
}