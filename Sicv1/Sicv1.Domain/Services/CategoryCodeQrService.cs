using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Services
{
    public class CategoryCodeQrService : ICategoryCodeQrService
    {
        private readonly ICategoryCodeQrRepository _categoryCodeQrRepository;
        public CategoryCodeQrService(ICategoryCodeQrRepository categoryCodeQrRepository)
        {
            this._categoryCodeQrRepository = categoryCodeQrRepository;
        }

        public async Task<int> Save(CategoryCodeQr categoryCodeQr)
        {
            try
            {
                return await _categoryCodeQrRepository.Save(categoryCodeQr);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
        {
            _categoryCodeQrRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<int> GetCount()
        {
            try
            {
                return await _categoryCodeQrRepository.GetCount();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<CategoryCodeQr>> GetCategoriesCodeQrDetail(int ID_COMPANY)
        {
            try
            {
                return await _categoryCodeQrRepository.GetCategoriesCodeQrDetail(ID_COMPANY);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<CategoryCodeQr>> GetChart(DateTime? fi, DateTime? ff)
        {
            try
            {
                return await _categoryCodeQrRepository.GetChart(fi, ff);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<CategoryCodeQr>> GetCouponsHierarchyDetail(int CouponId)
        {
            try
            {
                return await _categoryCodeQrRepository.GetCouponsHierarchyDetail(CouponId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 20191105 141600
        /// </summary>
        /// <param name="categoryCodeQr"></param>
        /// <returns></returns>
        public async Task<int> Update(CategoryCodeQr categoryCodeQr)
        {
            try
            {
                return await _categoryCodeQrRepository.Update(categoryCodeQr);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}