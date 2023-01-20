using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Contracts.Services
{
    public interface ICategoryCodeQrService : IDisposable
    {
        Task<int> Save(CategoryCodeQr categoryCodeQr);
        Task<int> GetCount();
        Task<IEnumerable<CategoryCodeQr>> GetCategoriesCodeQrDetail(int ID_COMPANY);
        Task<IEnumerable<CategoryCodeQr>> GetChart(DateTime? fi, DateTime? ff);
        Task<IEnumerable<CategoryCodeQr>> GetCouponsHierarchyDetail(int CouponId);
        /// <summary>
        /// 20191105 141600
        /// </summary>
        /// <param name="categoryCodeQr"></param>
        /// <returns></returns>
        Task<int> Update(CategoryCodeQr categoryCodeQr);
    }
}
