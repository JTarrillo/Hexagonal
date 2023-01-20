using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using Sicv1.Infrastructure.Cross;
using Sicv1.Presentation.Filters;
using Sicv1.Presentation.Utils;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sicv1.Presentation.Controllers
{		
	public class CategoryCodeQrController : Controller
	{
		private readonly ICategoryCodeQrService _categoryCodeQrService;
		public CategoryCodeQrController(ICategoryCodeQrService categoryCodeQrService)
		{
			this._categoryCodeQrService = categoryCodeQrService;
		}

		[Authentication(ControllerName = "CategoryCodeQr")]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<ActionResult> Index()
		{
			try
			{
				return View();
			}
			catch (Exception)
			{
				return Content("UD. no puede acceder a este recurso.");
			}
		}

		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		[HttpPost]
		public async Task<JsonResult> Confirm(string p_CODE_QR, int p_ID, string p_TYPE)
		{
			try
			{
				var resultado = await _categoryCodeQrService.Update(new CategoryCodeQr()
				{
					CODE_QR = p_CODE_QR,
					APPROVED_ID_USER = Util.MyUser.ID,
                    TYPE= p_TYPE,
                    ID = p_ID
				});
				return new HandleResults().ValidateResult(resultado);
			}
			catch (Exception)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso", });
			}
		}

		[HttpGet]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<int> GetCount()
		{
			try
			{
				var result = await _categoryCodeQrService.GetCount();
				return result;
			}
			catch (Exception)
			{
				return 0;
			}
		}

        [HttpGet]
        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        public async Task<JsonResult> GetCategoriesHistoricalDetail()
        {
            try
            {
                if (Session["user"] != null)
                {
                    var result = await _categoryCodeQrService.GetCategoriesCodeQrDetail(int.Parse(Util.MyUser.ID_COMPANY));
                    var result_return = Json(result, JsonRequestBehavior.AllowGet);
                    result_return.MaxJsonLength = int.MaxValue;
                    return result_return;
                }
                else
                {
                    return Json(new { _ = "UD. no puede acceder a este recurso", });
                }
            }
            catch (Exception ex)
            {
                return Json(new { _ = "UD. no puede acceder a este recurso..", });
            }
        }

		[HttpGet]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<JsonResult> GetChart(string fi, string ff)
		{
			try
			{
				DateTime? fini;
				DateTime? ffin;

				if (fi == "" && ff == "")
				{
					fini = null;
					ffin = null;
				}
				else
				{
					fini = Convert.ToDateTime(fi);
					ffin = Convert.ToDateTime(ff);
				}

				var result = await _categoryCodeQrService.GetChart(fini, ffin);
				return Json(result, JsonRequestBehavior.AllowGet);
			}
			catch (Exception)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso...", });
			}
		}

		[HttpPost]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<JsonResult> GetHierarchyCouponDetail(int CouponId)
		{
			try
			{
				var result = await _categoryCodeQrService.GetCouponsHierarchyDetail(CouponId);
				return Json(result);
			}
			catch (Exception)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso.", });
			}
		}

	}
}