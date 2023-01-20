using ClosedXML.Excel;
//using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using Sicv1.Infrastructure.Cross;
using Sicv1.Presentation.Filters;
using Sicv1.Presentation.Models;
using Sicv1.Presentation.Utils;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Sicv1.Presentation.Controllers
{
	[Authentication(ControllerName = "Parameter")]
	public class ParameterController : Controller
	{
		private readonly IParameterService _parameterService;
		
		public ParameterController(IParameterService parameterService)
		{
			this._parameterService = parameterService;
		}

		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<ActionResult> Index()
		{
			return View();
		}

		[HttpPost]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<JsonResult> SaveParameter(Parameter model)
		{
			try
			{

                //Parameter parameter = new Parameter
                //{
                //    ID = model.ID,
                //    NAME = model.NAME,
                //    DESCRIPTION = model.DESCRIPTION,
                //    VALUE = model.VALUE,
                //    COMPANY_ID = model.COMPANY_ID,
                //    SAVE_OR_UPDATE = model.SAVE_OR_UPDATE,
                //    ID_PADRE = model.ID_PADRE,
                //    CREATED_USER = Util.MyUser.ID
                //};
                var resultado = await _parameterService.Save(model);
				return new HandleResults().ValidateResult(resultado);
			}
			catch (Exception ex)
			{
				//return Json(new { _ = "UD. no puede acceder a este recurso" });
				return Json(new { status = false, message = ex.Message });
			}
		}

		[HttpPost]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]		
		public async Task<JsonResult> GetParameter(ParameterViewModel model)
		{
			try
			{
				Parameter parameter = new Parameter();
				parameter.ID_PADRE = model.ID_PADRE;

				var data = await _parameterService.GetParameter(parameter);
				//return Json(resultado, JsonRequestBehavior.AllowGet);
				String timeStamp = GetTimestamp(DateTime.Now);
				return Json(new
				{
					code = 200,
					status = true,
					timeStamp,
					message = "Consulta exitosa",
					data
				});
				/*
				var resultado = await _parameterService.GetParameter();
				return Json(new
				{
					code = 200,
					status = true,
					message = "Consulta exitosa",
					resultado
				});
				*/
			}
			catch (Exception ex)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" + ex.Message });
			}
		}

		[HttpPost]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<JsonResult> DeleteParameter(ParameterViewModel model)
		{
			try
			{

				Parameter parameter = new Parameter();

				parameter.ID = model.ID;
				parameter.STATUS = model.STATUS;

				var resultado = await _parameterService.DeleteParameter(parameter);

				return Json(new { status = true, message = resultado });
			}
			catch (Exception ex)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" + ex.Message });
			}
		}

		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		//[ValidateAntiForgeryToken]
		[HttpPost]
		public async Task<JsonResult> UpdateStatus(int id)
		{
			try
			{
				var resultado = await _parameterService.UpdateStatus(id);

				return new HandleResults().ValidateResult(resultado);
			}
			catch (Exception ex)
			{
				return Json(new { status = false, message = ex.Message });
			}
		}



		[HttpPost]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<JsonResult> ActiveParameter(ParameterViewModel model)
		{
			try
			{

				Parameter parameter = new Parameter();

				parameter.ID = model.ID;
				parameter.STATUS = true;

				var resultado = await _parameterService.ActiveParameter(parameter);

				return Json(new { status = true, message = resultado });
			}
			catch (Exception ex)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" + ex.Message });
			}
		}
		/*
		[HttpPost]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<JsonResult> GetUsersPaginate(string NumDniSearch = null, int? RoleId = null, decimal? CurrentPage = null, decimal? RecordsPerPage = null)
		{
			try
			{
				return await GetUsers(NumDniSearch, RoleId, CurrentPage, RecordsPerPage);				
			}
			catch (Exception)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso+1" });
			}
		}

		private async Task<JsonResult> GetUsers(string NumDniSearch = null, int? RoleId = null, decimal? CurrentPage = null, decimal? RecordsPerPage = null)
		{
			try
			{
				var resultado = await _userService.GetUsers(NumDniSearch, RoleId, CurrentPage, RecordsPerPage);
				return Json(resultado);
			}
			catch (Exception)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso"+ RoleId });
			}
		}
		*/
		/*
		[HttpPost]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public JsonResult GetCount(int? roleId = null)
		{
			try
			{
				var result = _userService.GetCount(roleId).Result;
				return Json(result);
			}
			catch (Exception)
			{
				return Json("0");
			}

		}
		*/

		//[HttpPost]
		//[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		//public async Task<int> GetCount(int roleId = -1)
		//{
		//    try
		//    {
		//        var result = await _userService.GetCount(roleId);
		//        return result;
		//    }
		//    catch (Exception)
		//    {
		//        return 0;
		//    }

		//}
		/*
		[HttpPost]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<JsonResult> UpdateUser(UserViewModel model)
		{
			try
			{
				User user = new User();
				user.ID = model.ID;
				user.NAME = model.NAME;
				user.LASTNAME_P = model.LASTNAME_P;
				user.LASTNAME_M = model.LASTNAME_M;
				user.USERNAME = model.USERNAME;
				user.PASSWORD = model.PASSWORD;
				user.TYPE_DOCUMENT = model.TYPE_DOCUMENT;
				user.DOCUMENT = model.DOCUMENT;
				user.PHONE1 = model.PHONE1;
				user.GENDER = model.GENDER;
				user.EMAIL = model.EMAIL;
				user.ADDRESS = model.ADDRESS;
				user.UBIGEO = model.UBIGEO;
				user.FK_ROLE = model.FK_ROLE;
				user.CARD = model.CARD;
				user.ESTADO = model.ESTADO;
				user.COMPANY_ID = model.COMPANY_ID;
				user.UPDATED_USER = Util.MyUser.ID;
				var resultado = await _userService.Update(user);
				return new HandleResults().ValidateResult(resultado);
			}
			catch (Exception)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" });
			}
		}

		private string generatePassword()
		{
			int length = 15;
			string charset = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			string retVal = "";
			Random random = new Random();
			for (int i = 0, n = charset.Length; i < length; ++i)
			{
				retVal += charset[(int)Math.Floor(random.NextDouble() * n)];
			}
			return retVal;
		}

		[HttpPost]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<JsonResult> SaveUser(UserViewModel model)
		{
			try
			{
				string password = generatePassword();
				User user = new User();
				user.NAME = model.NAME;
				user.LASTNAME_P = model.LASTNAME_P;
				user.LASTNAME_M = model.LASTNAME_M;
				user.USERNAME = model.USERNAME;
				user.PASSWORD = password;//model.PASSWORD;
				user.TYPE_DOCUMENT = model.TYPE_DOCUMENT;
				user.DOCUMENT = model.DOCUMENT;
				user.PHONE1 = model.PHONE1;
				user.GENDER = model.GENDER;
				user.EMAIL = model.EMAIL;
				user.ADDRESS = model.ADDRESS;
				user.UBIGEO = model.UBIGEO;
				user.FK_ROLE = model.FK_ROLE;
				user.CARD = model.CARD;
				user.ESTADO = model.ESTADO;
				user.COMPANY_ID = model.COMPANY_ID;
				user.CREATED_USER = Util.MyUser.ID;
				var resultado = await _userService.Save(user);
				return new HandleResults().ValidateResult(resultado, password);
			}
			catch (Exception)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" });
			}
		}


		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public ActionResult ExportToExcel(string fi, string ff)
		{
			try
			{
				var workbook = new XLWorkbook();
				workbook.AddWorksheet("Usuarios");
				var ws = workbook.Worksheet("Usuarios");

				ws.Cell(1, 1).Value = "ID"; ws.Cell(1, 1).Style.Font.Bold = true;
				ws.Cell(1, 2).Value = "CODIGO LIFEMILES"; ws.Cell(1, 2).Style.Font.Bold = true;
				ws.Cell(1, 3).Value = "NOMBRES"; ws.Cell(1, 3).Style.Font.Bold = true;
				ws.Cell(1, 4).Value = "APELLIDO PATERNO"; ws.Cell(1, 4).Style.Font.Bold = true;
				ws.Cell(1, 5).Value = "APELLIDOS MATERNO"; ws.Cell(1, 5).Style.Font.Bold = true;
				ws.Cell(1, 6).Value = "TIPO DE DOCUMENTO"; ws.Cell(1, 6).Style.Font.Bold = true;
				ws.Cell(1, 7).Value = "NRO. DOCUMENTO"; ws.Cell(1, 7).Style.Font.Bold = true;

				ws.Cell(1, 8).Value = "GÉNERO"; ws.Cell(1, 8).Style.Font.Bold = true;
				ws.Cell(1, 9).Value = "TELÉF.1"; ws.Cell(1, 9).Style.Font.Bold = true;
				ws.Cell(1, 10).Value = "TELÉF.2"; ws.Cell(1, 10).Style.Font.Bold = true;
				ws.Cell(1, 11).Value = "EMAIL"; ws.Cell(1, 11).Style.Font.Bold = true;
				ws.Cell(1, 12).Value = "DIRECCIÓN"; ws.Cell(1, 12).Style.Font.Bold = true;
				//ws.Cell(1, 12).Value = "ESTADO"; ws.Cell(1, 12).Style.Font.Bold = true;

				ws.Cell(1, 13).Value = "DEPARTAMENTO"; ws.Cell(1, 13).Style.Font.Bold = true;
				ws.Cell(1, 14).Value = "PROVINCIA"; ws.Cell(1, 14).Style.Font.Bold = true;
				ws.Cell(1, 15).Value = "DISTRITO"; ws.Cell(1, 15).Style.Font.Bold = true;
				ws.Cell(1, 16).Value = "REGISTRO ACTUALIZADO POR"; ws.Cell(1, 16).Style.Font.Bold = true;
				ws.Cell(1, 17).Value = "FECHA DE ACTUALIZACIÓN"; ws.Cell(1, 17).Style.Font.Bold = true;

				int row = 2;
				foreach (var item in _userService.ExportToExcel(fi, ff))
				{
					ws.Cell("A" + row.ToString()).Value = item.ID;
					ws.Cell("B" + row.ToString()).Value = item.CODE_LIFEMILES;
					ws.Cell("C" + row.ToString()).Value = item.NAME;
					ws.Cell("D" + row.ToString()).Value = item.LASTNAME_P;
					ws.Cell("E" + row.ToString()).Value = item.LASTNAME_M;
					ws.Cell("F" + row.ToString()).Value = item.TYPE_DOCUMENT;

					ws.Cell("G" + row.ToString()).Value = item.DOCUMENT.ToString();
					ws.Cell("H" + row.ToString()).Value = item.GENDER;
					ws.Cell("I" + row.ToString()).Value = item.PHONE1;
					ws.Cell("J" + row.ToString()).Value = item.PHONE2;
					ws.Cell("K" + row.ToString()).Value = item.EMAIL;
					ws.Cell("L" + row.ToString()).Value = item.ADDRESS;
					//ws.Cell("L" + row.ToString()).Value = item.ESTADO;
					ws.Cell("M" + row.ToString()).Value = item.DEPARTAMENT;
					ws.Cell("N" + row.ToString()).Value = item.PROVINCE;
					ws.Cell("O" + row.ToString()).Value = item.DISTRICT;
					ws.Cell("P" + row.ToString()).Value = item.UPDATE_USERNAME;
					ws.Cell("Q" + row.ToString()).Value = item.UPDATED_AT;
					row++;
				}
				ws.Column(6).CellsUsed().SetDataType(XLDataType.Text);
				return new ExcelResult(workbook, "Usuarios");
			}
			catch (Exception)
			{
				return Content("UD. no puede acceder a este recurso.");
			}
		}

		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public ActionResult ExportToExcelLM(string fi, string ff)
		{
			try
			{
				var workbook = new XLWorkbook();
				workbook.AddWorksheet("Usuarios");
				var ws = workbook.Worksheet("Usuarios");

				ws.Cell(1, 1).Value = "ID"; ws.Cell(1, 1).Style.Font.Bold = true;
				ws.Cell(1, 2).Value = "CODIGO LIFEMILES"; ws.Cell(1, 2).Style.Font.Bold = true;
				ws.Cell(1, 3).Value = "NOMBRES"; ws.Cell(1, 3).Style.Font.Bold = true;
				ws.Cell(1, 4).Value = "APELLIDO PATERNO"; ws.Cell(1, 4).Style.Font.Bold = true;
				ws.Cell(1, 5).Value = "APELLIDOS MATERNO"; ws.Cell(1, 5).Style.Font.Bold = true;
				ws.Cell(1, 6).Value = "TIPO DE DOCUMENTO"; ws.Cell(1, 6).Style.Font.Bold = true;
				ws.Cell(1, 7).Value = "NRO. DOCUMENTO"; ws.Cell(1, 7).Style.Font.Bold = true;

				ws.Cell(1, 8).Value = "GÉNERO"; ws.Cell(1, 8).Style.Font.Bold = true;
				ws.Cell(1, 9).Value = "TELÉF.1"; ws.Cell(1, 9).Style.Font.Bold = true;
				ws.Cell(1, 10).Value = "TELÉF.2"; ws.Cell(1, 10).Style.Font.Bold = true;
				ws.Cell(1, 11).Value = "EMAIL"; ws.Cell(1, 11).Style.Font.Bold = true;
				ws.Cell(1, 12).Value = "DIRECCIÓN"; ws.Cell(1, 12).Style.Font.Bold = true;
				//ws.Cell(1, 12).Value = "ESTADO"; ws.Cell(1, 12).Style.Font.Bold = true;

				ws.Cell(1, 13).Value = "DEPARTAMENTO"; ws.Cell(1, 13).Style.Font.Bold = true;
				ws.Cell(1, 14).Value = "PROVINCIA"; ws.Cell(1, 14).Style.Font.Bold = true;
				ws.Cell(1, 15).Value = "DISTRITO"; ws.Cell(1, 15).Style.Font.Bold = true;
				ws.Cell(1, 16).Value = "REGISTRO ACTUALIZADO POR"; ws.Cell(1, 16).Style.Font.Bold = true;
				ws.Cell(1, 17).Value = "FECHA DE ACTUALIZACIÓN LM"; ws.Cell(1, 17).Style.Font.Bold = true;

				int row = 2;
				foreach (var item in _userService.ExportToExcelLM(fi, ff))
				{
					ws.Cell("A" + row.ToString()).Value = item.ID;
					ws.Cell("B" + row.ToString()).Value = item.CODE_LIFEMILES;
					ws.Cell("C" + row.ToString()).Value = item.NAME;
					ws.Cell("D" + row.ToString()).Value = item.LASTNAME_P;
					ws.Cell("E" + row.ToString()).Value = item.LASTNAME_M;
					ws.Cell("F" + row.ToString()).Value = item.TYPE_DOCUMENT;

					ws.Cell("G" + row.ToString()).Value = item.DOCUMENT.ToString();
					ws.Cell("H" + row.ToString()).Value = item.GENDER;
					ws.Cell("I" + row.ToString()).Value = item.PHONE1;
					ws.Cell("J" + row.ToString()).Value = item.PHONE2;
					ws.Cell("K" + row.ToString()).Value = item.EMAIL;
					ws.Cell("L" + row.ToString()).Value = item.ADDRESS;
					//ws.Cell("L" + row.ToString()).Value = item.ESTADO;
					ws.Cell("M" + row.ToString()).Value = item.DEPARTAMENT;
					ws.Cell("N" + row.ToString()).Value = item.PROVINCE;
					ws.Cell("O" + row.ToString()).Value = item.DISTRICT;
					ws.Cell("P" + row.ToString()).Value = item.UPDATE_USERNAME;
					ws.Cell("Q" + row.ToString()).Value = item.UPDATED_AT;
					row++;
				}
				ws.Column(6).CellsUsed().SetDataType(XLDataType.Text);
				return new ExcelResult(workbook, "Usuarios");
			}
			catch (Exception ex)
			{
				return Content("UD. no puede acceder a este recurso." + ex.Message);
			}
		}
		*/
		public static String GetTimestamp(DateTime value)
		{
			return value.ToString("yyyy-MM-dd HH:mm:ss");
		}
	}
}
