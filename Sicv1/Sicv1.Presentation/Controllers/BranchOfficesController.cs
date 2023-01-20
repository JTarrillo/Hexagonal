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
    [Authentication(ControllerName = "BranchOffices")]
    public class BranchOfficesController : Controller
    {
        private readonly IBranchOfficesService _branchOfficesService;
        public BranchOfficesController(IBranchOfficesService branchOfficesService)
        {
            this._branchOfficesService = branchOfficesService;
        }

        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        public async Task<ActionResult> Index()
        {
            return View();
        }

        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        [HttpGet]
        public async Task<JsonResult> GetBranchOffices()
        {
            try
            {
                var resultado = await _branchOfficesService.GetBranchOffices();
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { _ = "UD. no puede acceder a este recurso" + ex.Message });
            }
        }

        [HttpPost]
        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        public async Task<JsonResult> SaveBranchOffices(BranchOfficesViewModel model)
        {
            try
            {

                BranchOffices branchoffices = new BranchOffices();

                branchoffices.ID_COMPANY = model.ID_COMPANY;
                branchoffices.NAME = model.NAME;
                //branchoffices.DESCRIPTION = model.DESCRIPTION;
                branchoffices.PHONE = model.PHONE;
                branchoffices.LATITUDE = model.LATITUDE;
                branchoffices.LONGITUDE = model.LONGITUDE;
                branchoffices.DIRECTION = model.DIRECTION;
                branchoffices.COUNTRY = model.COUNTRY;

                var resultado = await _branchOfficesService.SaveBranchOffices(branchoffices);

                return Json(new { status = true, id = resultado });
            }
            catch (Exception ex)
            {
                return Json(new { _ = "UD. no puede acceder a este recurso" + ex.Message });
            }
        }

        [HttpPost]
        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        public async Task<JsonResult> UpdateBranchOffices(BranchOfficesViewModel model)
        {
            try
            {

                BranchOffices branchoffices = new BranchOffices();
                branchoffices.ID = model.ID;
                branchoffices.ID_COMPANY = model.ID_COMPANY;
                branchoffices.NAME = model.NAME;                
                branchoffices.PHONE = model.PHONE;
                branchoffices.LATITUDE = model.LATITUDE;
                branchoffices.LONGITUDE = model.LONGITUDE;
                branchoffices.DIRECTION = model.DIRECTION;
                branchoffices.COUNTRY = model.COUNTRY;

                var resultado = await _branchOfficesService.UpdateBranchOffices(branchoffices);

                return Json(new { status = true, message = resultado });
            }
            catch (Exception ex)
            {
                return Json(new { _ = "UD. no puede acceder a este recurso" + ex.Message });
            }
        }

        [HttpPost]
        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        public async Task<JsonResult> DeleteBranchOffices(BranchOfficesViewModel model)
        {
            try
            {

                BranchOffices branchoffices = new BranchOffices();

                branchoffices.ID = model.ID;

                var resultado = await _branchOfficesService.DeleteBranchOffices(branchoffices);

                return Json(new { status = true, message = resultado });
            }
            catch (Exception ex)
            {
                return Json(new { _ = "UD. no puede acceder a este recurso" + ex.Message });
            }
        }


    }
}