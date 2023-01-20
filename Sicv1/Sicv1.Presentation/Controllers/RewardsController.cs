using ClosedXML.Excel;
using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using Sicv1.Infrastructure.Cross;
using Sicv1.Presentation.Filters;
using Sicv1.Presentation.Models;
using Sicv1.Presentation.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Sicv1.Presentation.Controllers
{
    [Authentication(ControllerName = "Rewards")]
    public class RewardsController : Controller
    {
        private readonly IRewardsService rewardsService;

        public RewardsController(IRewardsService notifyService)
        {
            this.rewardsService = notifyService;
        }

        [HttpGet]
        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        public async Task<ActionResult> Index()
        {
            return View();
        }


        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        public ActionResult export(string nameFile)
        {
            try
            {
                var workbook = new XLWorkbook();
                workbook.AddWorksheet(nameFile);
                var ws = workbook.Worksheet(nameFile);

                ws.Cell(1, 1).Value = "ID"; ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 2).Value = "DOCUMENTO"; ws.Cell(1, 2).Style.Font.Bold = true;
                ws.Cell(1, 3).Value = "NOMBRE Y APELLIDOS"; ws.Cell(1, 3).Style.Font.Bold = true;
                ws.Cell(1, 4).Value = "CARGA"; ws.Cell(1, 4).Style.Font.Bold = true;
                ws.Cell(1, 5).Value = "FECHA CREACION"; ws.Cell(1, 5).Style.Font.Bold = true;
                ws.Cell(1, 6).Value = "ALIANZA"; ws.Cell(1, 6).Style.Font.Bold = true;
                ws.Cell(1, 7).Value = "CANJEADO"; ws.Cell(1, 7).Style.Font.Bold = true;
                ws.Cell(1, 8).Value = "FECHA DE CANJE"; ws.Cell(1, 8).Style.Font.Bold = true;

                int row = 2;
                List<PersonRewards> resultado =  rewardsService.listPersonRewardExport(new PersonRewards() { UPDATED_USER = Util.MyUser.ID, DOCUMENT = "" });
                foreach (PersonRewards item in resultado)
                { 
                    foreach (CompanyReward com in item.COMPANIES) {
                        ws.Cell("A" + row.ToString()).Value = item.ID;
                        ws.Cell("B" + row.ToString()).Value = item.DOCUMENT;
                        ws.Cell("C" + row.ToString()).Value = item.NAME + " " + item.LASTNAME_P + " " + item.LASTNAME_M;
                        ws.Cell("D" + row.ToString()).Value = item.NAME_CHARGE;
                        ws.Cell("E" + row.ToString()).Value = item.CREATED_AT;
                        ws.Cell("F" + row.ToString()).Value = com.NAME;
                        ws.Cell("G" + row.ToString()).Value = com.USED?"SI":"NO";
                        ws.Cell("H" + row.ToString()).Value = com.CREATED_AT;
                        row++;
                    }
                    
                }
                ws.Column(6).CellsUsed().SetDataType(XLDataType.Text);
                return new ExcelResult(workbook, nameFile);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }



        [HttpPost]
        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        public async Task<JsonResult> list(PersonRewards obj)
        {
            try
            {
                var resultado = await rewardsService.listPersonRewards(new PersonRewards() { UPDATED_USER = Util.MyUser.ID, DOCUMENT=obj.DOCUMENT });
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { _ = "UD. no puede acceder a este recurso ["+ex.Message+"]" });
            }
        }

        [HttpPost]
        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        public async Task<JsonResult> save(PersonRewardsAction obj)
        {
            try
            {
                obj.UPDATED_USER = Util.MyUser.ID;
                var resultado = await rewardsService.savePersonRewards(obj);
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { _ = "UD. no puede acceder a este recurso [" + ex.Message + "]" });
            }
        }

        [HttpPost]
        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        public async Task<JsonResult> confirm(PersonRewards obj)
        {
            try
            {
                obj.UPDATED_USER = Util.MyUser.ID;
                var resultado = await rewardsService.confirmReward(obj);
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { _ = "UD. no puede acceder a este recurso [" + ex.Message + "]" });
            }
        }

    }
}