using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using Sicv1.Presentation.Filters;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sicv1.Presentation.Controllers
{
	[Authentication(ControllerName = "Configuration")]
	public class ConfigurationController : Controller
	{
		private readonly IConfigurationService _configurationService;
		public ConfigurationController(IConfigurationService con)
		{
			this._configurationService = con;
		}

		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<ActionResult> Index()
		{
			return View();
		}

		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<JsonResult> UpdateConfiguration(Domain.Entities.Configuration conf)
		{
			ConfigurationResponseBE objResponse = new ConfigurationResponseBE();
			if (conf != null)
			{
				objResponse = await _configurationService.UpdateConfiguration(conf);
			}
			else
			{
				objResponse.STATUS = true;
				objResponse.MESSAGE = "Debe enviar los valores a actualizar";
			}
			return Json(objResponse);
		}

		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<JsonResult> GetValuesCurrent()
		{
			ConfigurationResponseBE objResponse = new ConfigurationResponseBE();
			objResponse = await _configurationService.GetValuesCurrent();
			return Json(objResponse);
		}
	}
}