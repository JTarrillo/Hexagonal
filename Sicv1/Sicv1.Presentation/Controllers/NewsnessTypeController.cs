using Sicv1.Domain.Contracts.Services;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sicv1.Presentation.Controllers
{	
	public class NewsnessTypeController : Controller
	{
		private readonly INewsNessTypeService _newsNessTypeService;		

		public NewsnessTypeController(INewsNessTypeService newsNessTypeService)
		{
			this._newsNessTypeService = newsNessTypeService;
		}

		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		[HttpGet]
		public async Task<JsonResult> GetNewsNessesType()
		{
			try
			{
				var resultado = await _newsNessTypeService.GetNewsNessesType();
				return Json(resultado, JsonRequestBehavior.AllowGet);
			}
			catch (Exception)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" });
			}
		}
	}
}