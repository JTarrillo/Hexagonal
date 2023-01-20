using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        //private readonly ICategoryCodeQrRepository _categoryCodeQrRepository;

        //public CategoryService(ICategoryRepository categoryRepository, ICategoryCodeQrRepository categoryCodeQrRepository)
        //{
        //    this._categoryRepository = categoryRepository;
        //    this._categoryCodeQrRepository = categoryCodeQrRepository;
        //}

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetCategoriesByCompanyId(int Id, string Title = "", string Description = "")
        {
            try
            {
                return await _categoryRepository.GetCategoriesByCompanyId(Id, Title, Description);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Category>> GetCategoriesCodeQrByUserId(int UserId)
        {
            try
            {
                return await _categoryRepository.GetCategoriesCodeQr(UserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdateCategoriesById(Category category)
        {
            try
            {
                return await _categoryRepository.UpdateCategoriesById(category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdateCategoriesStatusById(int Id, string Status, int user_id)
        {
            try
            {
                return await _categoryRepository.UpdateCategoriesStatusById(Id, Status, user_id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> DeleteCategoriesStatusById(int Id, int user_id)
        {
            try
            {
                return await _categoryRepository.DeleteCategoriesStatusById(Id, user_id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> SaveCategories(Category category)
        {
            try
            {
                return await _categoryRepository.SaveCategory(category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Category>> GetCategoriesParent()
        {
            try
            {
                return await _categoryRepository.GetCategoriesParent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Category>> GetCategoriesChildByParentId(int ParentId)
        {
            try
            {
                return await _categoryRepository.GetCategoriesChildByParentId(ParentId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Category Validate(string CodeQrToValidate, int CompanyId)
        {
            try
            {
                var resultado = _categoryRepository.Validate(CodeQrToValidate, CompanyId);
                if (resultado == null)
                {
                    return new Category
                    {
                        CODE_QR = "Null"
                    };
                }
                //Comparamos el ID_COMPANY del usuario que se logueó y el que tiene el cupón:
                else if (resultado.ID_COMPANY != CompanyId)
                {
                    return new Category
                    {
                        CODE_QR = "NotBelongCompany"
                    };
                }

                //if (CodeQrToValidate == _categoryRepository.GetCategoryById(CategoryId, UserId).Result.CODE_QR)
                //{
                //    return new Category { CODE_QR = "Exists" };
                //}
                return _categoryRepository.Validate(CodeQrToValidate, CompanyId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<string> searchByDocument(string document, int id_company)
        {
            try
            {
                return _categoryRepository.searchByDocument(document, id_company);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
        {
            _categoryRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<int> GetCount()
        {
            try
            {
                return await _categoryRepository.GetCount();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Category>> GetCategoriesCodeQrByCompanyId(int Id)
        {
            try
            {
                return await _categoryRepository.GetCategoriesCodeQrByCompanyId(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}