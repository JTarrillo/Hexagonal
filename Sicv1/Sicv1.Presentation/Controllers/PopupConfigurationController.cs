using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using Sicv1.Presentation.Filters;
using Sicv1.Presentation.Models;
using Sicv1.Presentation.Utils;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sicv1.Presentation.Controllers
{
	[Authentication(ControllerName = "PopupConfiguration")]
	public class PopupConfigurationController : Controller
	{		
		private readonly IPopupConfigurationService _popupConfigurationService;
		public PopupConfigurationController(IPopupConfigurationService popupConfigurationService)
		{
			this._popupConfigurationService = popupConfigurationService;
		}

		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<ActionResult> Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<JsonResult> Get()
		{
			var data = await _popupConfigurationService.GetPopupConfigurations();
			return Json(data, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public async Task<JsonResult> Update(PopupConfigurationViewModel model)
		{
			var entity = new PopupConfiguration
			{
				ID = model.Id,
				URL = model.Url,
				DESCRIPTION = model.Description,
				LINK_IMAGE = model.LinkImage,
				IS_LINKEABLE = model.IsLinkeable,
				IS_ACTIVE = model.IsActive,
				UPDATED_USER = model.UpdateUser,

				TERMS_CONDITION_BANNER = model.TermsBanner,
				LINK_IMAGE_BANNER = model.LinkBanner,
				IS_ACTIVE_BANNER = model.IsActiveBanner
			};

			var resultado = await _popupConfigurationService.Update(entity);
			return new HandleResults().ValidateResult(resultado);
		}
	}
}