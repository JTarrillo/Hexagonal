using Sicv1.Domain.Contracts.Services;
using Sicv1.Infrastructure.Cross;
using Sicv1.Presentation.Filters;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sicv1.Presentation.Controllers
{
	[Authentication(ControllerName = "Role")]
	public class RoleController : Controller
	{
		private readonly IRoleService _roleService;
				
		public RoleController(IRoleService roleService)
		{
			this._roleService = roleService;
		}
		
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<ActionResult> Index()
		{
			return View();
		}

		[HttpGet]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<JsonResult> GetRoles()
		{
			try
			{
				var resultado = await _roleService.GetRoles(Util.MyUser.FK_ROLE);
				return Json(resultado, JsonRequestBehavior.AllowGet);
			}
			catch (Exception)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" });
			}
		}
	}
}