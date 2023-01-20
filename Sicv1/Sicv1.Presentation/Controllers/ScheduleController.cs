using Sicv1.Domain.Contracts.Services;
using Sicv1.Presentation.Filters;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sicv1.Presentation.Controllers
{
	[Authentication(ControllerName = "Schedule")]
	public class ScheduleController : Controller
	{
		private readonly IScheduleService _service;	

		public ScheduleController(IScheduleService service)
		{
			this._service = service;
		}

		[HttpGet]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<ActionResult> Index()
		{			
			return View();
		}

		[HttpGet]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public JsonResult GetSchedule(int? companyId)
		{
			try
			{
				var resultado = _service.GetScheduleCalls(companyId);
				return Json(resultado, JsonRequestBehavior.AllowGet);
			}
			catch (Exception)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" });
			}
		}

		[HttpGet]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<int> GetCount()
		{
			try
			{
				var result = await _service.GetCount();
				return result;
			}
			catch (Exception)
			{
				return 0;
			}
		}
	}
}
