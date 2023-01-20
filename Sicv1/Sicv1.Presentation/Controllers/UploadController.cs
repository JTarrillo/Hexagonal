using OfficeOpenXml;
using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using Sicv1.Infrastructure.Cross;
using Sicv1.Presentation.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sicv1.Presentation.Controllers
{
	[Authentication(ControllerName = "Upload")]
	public class UploadController : Controller
    {	
		private readonly IUploadService uploadService;

		public UploadController(IUploadService uploadService)
		{
			this.uploadService = uploadService;
		}

		[HttpGet]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<ActionResult> Index()
		{
			return View();
		}
		
		[HttpPost]
		public async Task<ActionResult> Preview()
		{
			try
			{
				if (Request.Files.Count == 0)
					throw new Exception("Debe seleccionar un archivo");

				HttpPostedFileBase file = Request.Files[0];
				string extension = System.IO.Path.GetExtension(file.FileName);
				List<string> excelformat = new List<string>() { ".xlsx", ".xls" };
				if (!excelformat.Contains(extension))
					throw new Exception("Cargue un archivo con extensión xls o xlsx");

				if (file.ContentLength > 1024 * 1024 * 2)
					throw new Exception("Debe cargar un archivo de hasta 2 MB");

				List<string> data = new List<string>();

				await Task.Run(() =>
				{
					using (var excel = new ExcelPackage(file.InputStream))
					{
						var ws = excel.Workbook.Worksheets.First();
						for (int i = 1; i <= ws.Dimension.End.Row; i++)
						{
							data.Add(ws.Cells[i, 1].Value?.ToString());
						}
					}
				});
				return Json(new
				{
					status = true,
					message = "correcto",
					data = data
				});
			}
			catch (Exception ex)
			{
				return Json(new
				{
					status = false,
					message = ex.Message,
					trace = ex.StackTrace,
					data = new List<string>()
				});
			}
			
		}

		[HttpPost]
		public async Task<ActionResult> Insert(Upload upload)
		{
			try
			{
				upload.CREATED_USER = Util.MyUser.ID;
				await uploadService.Insert(upload);
				return Json(upload);
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}