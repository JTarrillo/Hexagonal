using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Contracts.Services
{
    public interface ICategoryService : IDisposable
    {
        Task<IEnumerable<Category>> GetCategoriesByCompanyId(int Id, string Title = "", string Description = "");
        Task<IEnumerable<Category>> GetCategoriesCodeQrByUserId(int UserId);
        Task<int> UpdateCategoriesById(Category category);
        Task<int> UpdateCategoriesStatusById(int Id, string Status, int user_id);
        Task<int> DeleteCategoriesStatusById(int Id, int user_id);
        Task<int> SaveCategories(Category category);
        Task<IEnumerable<Category>> GetCategoriesParent();
        Task<IEnumerable<Category>> GetCategoriesChildByParentId(int ParentId);

        Category Validate(string CodeQrToValidate, int CompanyId);
        List<string> searchByDocument(string document, int id_company);
        Task<int> GetCount();
        Task<IEnumerable<Category>> GetCategoriesCodeQrByCompanyId(int Id);
    }
} 