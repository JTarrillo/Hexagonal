using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using Sicv1.Infrastructure.Cross;
using Sicv1.Presentation.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sicv1.Presentation.Controllers
{
	//[Authentication(ControllerName = "Home")]

	public class HomeController : Controller
	{
		private readonly IMenuService _menuService;
		private readonly IDashboardService dashboardService;
		//public HomeController(IMenuService menuService, IDashboardService dashboardService)
		//{
		//	this._menuService = menuService;
		//	this.dashboardService = dashboardService;
		//}

		//[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		//public async Task<ActionResult> Index()
		//{			
		//	return View();
		//}

        [AllowAnonymous]
		//public async Task<ActionResult> LoadMenu()
		//{
		//	try
		//	{
		//		var resultado = await _menuService.GetMenusByUserId(Util.MyUser.ID);
		//		return PartialView("_Sidebar", resultado);
		//	}
		//	catch (Exception ex)
		//	{
		//		return Content("UD. no puede acceder a este recurso.");
		//	}
		//}

		[HttpPost]
		public async Task<ActionResult> DashboardCounters(string start, string end, int channel)
		{
			Dashboard dashboard = await dashboardService.Totals(start, end, channel);
			return Json(dashboard);
		}
	}
}