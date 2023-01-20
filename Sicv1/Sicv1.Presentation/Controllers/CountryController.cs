using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using Sicv1.Infrastructure.Cross;
using Sicv1.Presentation.Filters;
using Sicv1.Presentation.Models;
using Sicv1.Presentation.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Sicv1.Presentation.Controllers
{
    [Authentication(ControllerName = "Country")]
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            this._countryService = countryService;
        }

        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<ActionResult> Index()
		{
			return View();
		}

        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        [HttpGet]        
        public async Task<JsonResult> GetCountry()
        {
            try
            {
                var resultado = await _countryService.GetCountry();
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { _ = "UD. no puede acceder a este recurso" + ex.Message });
            }
        }

        [HttpPost]
        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        public async Task<JsonResult> SaveCountry(CountryViewModel model)
        {
            try
            {

                Country country = new Country();

                country.NAME_COUNTRY = model.NAME_COUNTRY;
                country.IMAGE_FIRST = model.IMAGE_FIRST;

                var resultado = await _countryService.SaveCountry(country);

                return Json(new { status = true, message = resultado });
            }
            catch (Exception ex)
            {
                return Json(new { _ = "UD. no puede acceder a este recurso" + ex.Message });
            }
        }

        [HttpPost]
        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        public async Task<JsonResult> UpdateCountry(CountryViewModel model)
        {
            try
            {

                Country country = new Country();

                country.NAME_COUNTRY = model.NAME_COUNTRY;
                country.IMAGE_FIRST = model.IMAGE_FIRST;
                country.ID = model.ID;

                var resultado = await _countryService.UpdateCountry(country);

                return Json(new { status = true, message = resultado });
            }
            catch (Exception ex)
            {
                return Json(new { _ = "UD. no puede acceder a este recurso" + ex.Message });
            }
        }

        [HttpPost]
        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        public async Task<JsonResult> DeleteCountry(CountryViewModel model)
        {
            try
            {

                Country country = new Country();
               
                country.ID = model.ID;

                var resultado = await _countryService.DeleteCountry(country);

                return Json(new { status = true, message = resultado });
            }
            catch (Exception ex)
            {
                return Json(new { _ = "UD. no puede acceder a este recurso" + ex.Message });
            }
        }

        [HttpPost]
        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        public async Task<JsonResult> ActiveCountry(CountryViewModel model)
        {
            try
            {

                Country country = new Country();

                country.ID = model.ID;

                var resultado = await _countryService.ActiveCountry(country);

                return Json(new { status = true, message = resultado });
            }
            catch (Exception ex)
            {
                return Json(new { _ = "UD. no puede acceder a este recurso" + ex.Message });
            }
        }
    }
}