using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using Sicv1.Infrastructure.Cross;
using Sicv1.Presentation.Filters;
using Sicv1.Presentation.Models;
using Sicv1.Presentation.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sicv1.Presentation.Controllers
{
    [Authentication(ControllerName = "Company")]
    public class CompanyController : Controller
	{
		private readonly ICompanyService _companyService;

		public CompanyController(ICompanyService companyService)
		{
			this._companyService = companyService;
		}

		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<ActionResult> Index()
		{
			return View();
		}

        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        [HttpGet]
		public async Task<JsonResult> GetCompaniesByUserId()
		{
			try
			{
				var resultado = await _companyService.GetCompaniesByUserId(Util.MyUser.ID);
				return Json(resultado, JsonRequestBehavior.AllowGet);
			}
			catch (Exception)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" });
			}
		}

        [HttpPost]
        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        public async Task<JsonResult> GetCompaniesAll()
        {
            try
            {
                var resultado = await _companyService.GetCompaniesAll();
                return Json(resultado);
            }
            catch (Exception)
            {
                return Json(new { _ = "UD. no puede acceder a este recurso" });
            }
        }

        [HttpPost]
        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        public async Task<JsonResult> listCompanies()
        {
            try
            {
                var resultado = await _companyService.listCompanies();
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { _ = "UD. no puede acceder a este recurso" });
            }
        }

        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		[HttpPost]
		public async Task<JsonResult> List()
		{
			try
			{
				var data = await _companyService.GetCompanies();
				String timeStamp = GetTimestamp(DateTime.Now);
				return Json(new
                {
                    code = 200,
                    status = true,
                    message = "Consulta exitosa",
					timeStamp,
					data
                });
            }
			catch (Exception)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" });
			}
		}

	
		       
        
        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<JsonResult> Save(Company model)
		{
			try
			{
                var resultado = await _companyService.Save(model);

				return new HandleResults().ValidateResult(resultado);

			}
			catch (Exception ex)
			{				
				return Json(new { status = false, message = ex.Message});
            }
		}

		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		//[ValidateAntiForgeryToken]
		[HttpPost]
		public async Task<JsonResult> Update(Company model)
		{
            try { 
                var resultado = await _companyService.Update(model);
				
		        return new HandleResults().ValidateResult(resultado);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });
            }
        }

		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		//[ValidateAntiForgeryToken]
		[HttpPost]
		public async Task<JsonResult> UpdateStatus(int id)
		{
			try
			{
				var resultado = await _companyService.UpdateStatus(id);

				return new HandleResults().ValidateResult(resultado);
			}
			catch (Exception ex)
			{
				return Json(new { status = false, message = ex.Message });
			}
		}



		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		[HttpPost]
		public async Task<JsonResult> CheckIfHaveLifeMiles(int CompanyId)
		{
			try
			{
				var resultado = await _companyService.CheckIfHaveLifeMiles(CompanyId);
				return Json(new { _ = resultado });
			}
			catch (Exception)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" });
			}
		}

		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		[HttpPost]
		public ActionResult Create()
		{
			try
			{
				return View("Create");
			}
			catch (Exception)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" });
			}
		}

		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		[HttpPost]
		public ActionResult Edit(CompanyViewModel model)
		{
			try
			{
				return View("Edit", model);
			}
			catch (Exception)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" });
			}
		}

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private bool ContainsHTML(string checkString)
        {
            var regex = new Regex("(</?([^>/]*)/?>)");
            var matches = regex.Matches(checkString);
            var tags = matches.OfType<Match>().Select(m => m.Groups[2].Value);
            int counter = 0;
            List<string> whitelistTags = new List<string>() { "html", "script", "i", "alert", "head", "body", "title", "deloitte", "strong", "center", "article", "aside", "link", "style" };
            foreach(string word in tags.ToList())
            {
                if (whitelistTags.Contains(word.Trim()))
                {
                    counter++;
                }
            }
            return counter>0;
        }

		public static String GetTimestamp(DateTime value)
		{
			return value.ToString("yyyy-MM-dd HH:mm:ss");
		}
	}
}