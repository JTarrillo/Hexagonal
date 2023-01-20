using Sicv1.Domain.Entities;
using Sicv1.Domain.Services;
using Sicv1.Infrastructure.Cross;
using Sicv1.Presentation.Models;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sicv1.Presentation.Controllers
{

	public class AccountController : Controller
	{
		private readonly UserService _userService;
		public AccountController(UserService userService)
		{
			this._userService = userService;
		}

		[HttpGet]
		[AllowAnonymous]
		public ActionResult Login()
		{
			if (Session["user"] == null)
			{
				return View();
			}
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginViewModel model)
		{
			try
			{
				var username = model.Username;
				var password = AESEncrytDecry.DecryptStringAES(model.Password);

				if (model.Username == null || model.Username == "")
					return View(model);
				if (model.Password == null || model.Password == "")
					return View(model);
				if (!ModelState.IsValid)
					return View();

				var resultado = await _userService.SignIn(username, new Security().Encrypt(password));
				if (resultado == null)
				{
					ModelState.AddModelError("", "Usuario o password incorrecto");
					return View();
				}
				else
				{
					var status = await _userService.SignIn(username, new Security().Encrypt(password));
					if (status.ESTADO != 1)
					{
						ModelState.AddModelError("", "Usuario inactivo");
						return View();
					}

					Session["user"] = resultado;
					return RedirectWhenAllianceUser(resultado);
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View();
			}
		}

		private ActionResult RedirectWhenAllianceUser(User user)
		{
			if (user.FK_ROLE == Convert.ToInt32(ConfigurationManager.AppSettings["AllianceRole"]))
			{
				return RedirectToAction("Index", "CategoryCodeQr");
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		public ActionResult Logout()
		{
			try
			{
				Session["user"] = null;
				Session.Abandon();
				Session.Clear();
				return RedirectToAction("Login", "Account");
			}
			catch (Exception)
			{
				return Content("UD. no puede acceder a este recurso.");
			}
		}
	}
}