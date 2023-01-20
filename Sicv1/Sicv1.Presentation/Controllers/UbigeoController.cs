using Sicv1.Domain.Contracts.Services;
using Sicv1.Presentation.Filters;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sicv1.Presentation.Controllers
{
	[Logged]
	public class UbigeoController : Controller
	{
		private readonly IUbigeoService _ubigeoService;
		public UbigeoController(IUbigeoService ubigeoService)
		{
			this._ubigeoService = ubigeoService;
		}

		[HttpPost]
		public async Task<JsonResult> GetDptos()
		{
			try
			{
				var resultado = await _ubigeoService.GetDptos();
				return Json(resultado);
			}
			catch (Exception)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" });
			}
		}

		[HttpPost]
		public async Task<JsonResult> GetProvs(string DptoId = null)
		{
			try
			{
				var resultado = await _ubigeoService.GetProvs(DptoId);
				return Json(resultado);
			}
			catch (Exception)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" });
			}
		}

		[HttpPost]
		public async Task<JsonResult> GetDists(string DptoId = null, string ProvId = null)
		{
			try
			{
				var resultado = await _ubigeoService.GetDists(DptoId, ProvId);
				return Json(resultado, JsonRequestBehavior.AllowGet);
			}
			catch (Exception)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" });
			}
		}
	}
}