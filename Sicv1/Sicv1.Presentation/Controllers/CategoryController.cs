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
	[Authentication(ControllerName = "Category")]
	public class CategoryController : Controller
	{
		private readonly ICategoryService _categoryService;
		public CategoryController(ICategoryService categoryService)
		{
			this._categoryService = categoryService;
		}

		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<ActionResult> Index()
		{
			return View();
		}

		[HttpGet]
		[Authentication(ControllerName = "Category", ActionName = "Validate")]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public ActionResult Validate()
		{
			return View();
		}

		[HttpPost]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<JsonResult> GetCategoriesByCompanyId(int CompanyId, string Title = "", string Desc = "")
		{
			try
			{
				var resultado = await _categoryService.GetCategoriesByCompanyId(CompanyId, Title, Desc);
				return Json(resultado, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso"+ex.Message });
			}
		}

		[Authentication(ControllerName = "Category", ActionName = "Historical")]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<ActionResult> Historical()
		{
			try
			{
				return View();
			}
			catch (Exception)
			{
				return Content("UD. no puede acceder a este recurso.");
			}

		}

		[HttpGet]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<JsonResult> GetCategoriesCodeQrByUserId()
		{
			try
			{
				var resultado = await _categoryService.GetCategoriesCodeQrByUserId(Util.MyUser.ID);
				return Json(resultado, JsonRequestBehavior.AllowGet);
			}
			catch (Exception)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" });
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<JsonResult> UpdateCategoriesById(CouponViewModel model)
		{
			try
			{				
				Category category = new Category();
                byte[] image = null;
                if (model.imageBase64 != null)
                {
					if (model.StatusOriginalImage == 1) { 

						string clean = model.imageBase64.Split(',')[1];
						if (!clean.Substring(0, 1).Equals("/") && !clean.Substring(0, 1).Equals("i"))
							return Json(new { status = false, message = "Tipo de archivo no permitido" });
						string extension = clean.Substring(0, 1).Equals("/") ? ".jpg" : ".png";
						string nameFile = "alianza_" + RandomString(10) + extension;
						try
						{
							var bytes = Convert.FromBase64String(clean);
							var text = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
							bool validate = ContainsHTML(text);
							if (validate)
								return Json(new { status = false, message = "Archivo no permitido." });
							model.hdFileName = nameFile;
						}
						catch (Exception ex)
						{
							return Json(new { status = false, message = "Formato no valido." });
						}

						string dataimageExtension = Images.GetDataImageExtension(model);
						string imagebase64 = Images.GetImageB64(model, dataimageExtension);
						image = Images.FromB64ToStr(imagebase64);
						var folder = ConfigurationManager.AppSettings["AWSBucketPath"].ToString();

					}
					



                    if (model.StatusOriginalImage == 1)
                    {
                        category.IMAGE_FIRST = ConfigurationManager.AppSettings["AWSFilePath"].ToString() + model.hdFileName;

                    }
                    else
                    {
                        category.IMAGE_FIRST = model.imageBase64;
                    }
                }
               
                
                category.ID = model.hdCategoryId;
                category.TITLE = model.Title;
                category.DESCRIPTION = model.Description;
                category.CONDITIONS = model.Conditions;

                category.PRICE = model.Price;
				category.PERCENTAGE = model.Percentage;
				category.ID_PARENT = model.IdParent;
				category.ID_COMPANY = model.CompanyId;
				category.START_DATE = model.StartDate;
				category.END_DATE = model.EndDate;
				category.UPDATED_USER = Util.MyUser.ID;
				category.TYPE = model.Type;
				category.URL_LINK = model.URL_LINK;

				category.BARCODE = model.BARCODE;
                category.BARCODE_FORMAT = model.BARCODE_FORMAT;
                category.TYPE_CODE = model.TYPE_CODE;
                category.SEGMENT = model.SEGMENT;

                category.LIFEMILES_PARTICIPATES_CAMPAIGN = model.LIFEMILES_PARTICIPATES_CAMPAIGN;
				category.NUMBER_MILES = model.NUMBER_MILES;
                category.SHOW_IMAGE = model.SHOW_IMAGE;
                category.CAT_URL = model.CAT_URL;

                var resultado = await _categoryService.UpdateCategoriesById(category);

				if (model.StatusOriginalImage == 1)
				{
                    return await AmazonWS.UploadImage(model,
                    category,
                    image,
                    resultado,
                    new HandleResults().ValidateResult(resultado),
                    new HandleResults().DisplayErrorImages());
                }
				return new HandleResults().ValidateResult(resultado);
			}
			catch (Exception ex)
			{
				var st = new StackTrace(ex, true);
				var frame = st.GetFrame(0);
				var line = frame.GetFileLineNumber();
				return Json(new { status = false, message = ex.Message, lineError = line });
            }
		}

       
		[HttpPost]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<JsonResult> UpdateCategoriesStatusById(int Id, string Status)
		{
			try
			{
				var resultado = await _categoryService.UpdateCategoriesStatusById(Id, Status, Util.MyUser.ID);
				return new HandleResults().ValidateResult(resultado);
			}
			catch (Exception)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" });
			}
		}

        [HttpPost]
        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        public async Task<JsonResult> DeleteCategoriesStatusById(int Id )
        {
            try
            {
                var resultado = await _categoryService.DeleteCategoriesStatusById(Id, Util.MyUser.ID);
                return new HandleResults().ValidateResult(resultado);
            }
            catch (Exception)
            {
                return Json(new { _ = "UD. no puede acceder a este recurso" });
            }
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<JsonResult> SaveCategories(CouponViewModel model)
		{
			try
			{
				Category category = null;
                if (model.imageBase64 == null)
                    return Json(new { status = false, message = "No ha enviado imagen" });
                string clean = model.imageBase64.Split(',')[1];
                if (!clean.Substring(0, 1).Equals("/") && !clean.Substring(0, 1).Equals("i"))
                    return Json(new { status = false, message = "Tipo de archivo no permitido" });

                string extension = clean.Substring(0, 1).Equals("/") ? ".jpg" : ".png";
                string nameFile = "alianza_" + RandomString(10) + extension;
                try
                {
                    var bytes = Convert.FromBase64String(clean);
                    var text = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                    bool validate = ContainsHTML(text);
                    if (validate)
                        return Json(new { status = false, message = "Archivo no permitido." });
                    model.hdFileName = nameFile;
                }
                catch (Exception ex)
                {
                    return Json(new { status = false, message = "Formato no valido." });
                }

                string dataimageExtension = Images.GetDataImageExtension(model);
				string imagebase64 = Images.GetImageB64(model, dataimageExtension);
				byte[] image = Images.FromB64ToStr(imagebase64);

				var folder = ConfigurationManager.AppSettings["AWSBucketPath"].ToString();
				category = new Category();
				category.ID = model.hdCategoryId;
				category.TITLE = model.Title;
				category.DESCRIPTION = model.Description;
				category.CONDITIONS = model.Conditions;
				category.IMAGE_FIRST = ConfigurationManager.AppSettings["AWSFilePath"].ToString() + model.hdFileName;
				category.PRICE = model.Price;
				category.PERCENTAGE = model.Percentage;
				category.ID_PARENT = model.IdParent;
				category.ID_COMPANY = model.CompanyId;
				category.START_DATE = model.StartDate;
				category.END_DATE = model.EndDate;
				category.TYPE = model.Type;
				category.URL_LINK = model.URL_LINK;
				category.BARCODE = model.BARCODE;
                category.BARCODE_FORMAT = model.BARCODE_FORMAT;
                category.TYPE_CODE = model.TYPE_CODE;
                category.LIFEMILES_PARTICIPATES_CAMPAIGN = model.LIFEMILES_PARTICIPATES_CAMPAIGN;
				category.NUMBER_MILES = model.NUMBER_MILES;
                category.SEGMENT = model.SEGMENT;
                category.SHOW_IMAGE = model.SHOW_IMAGE;
                category.CAT_URL = model.CAT_URL;
                var resultado = await _categoryService.SaveCategories(category);
				return await AmazonWS.UploadImage(model,
					category,
					image,
					resultado,
					new HandleResults().ValidateResult(resultado),
					new HandleResults().DisplayErrorImages());
			}
			catch (Exception)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" });
			}
		}


		[HttpGet]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<JsonResult> GetParentCategories()
		{
			try
			{
				var resultado = await _categoryService.GetCategoriesParent();
				return Json(resultado, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
                return Json(new { status = false, message = ex.Message });
            }
		}

		[HttpPost]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<JsonResult> GetCategoriesChildByParentId(int Id)
		{
			try
			{
				var resultado = await _categoryService.GetCategoriesChildByParentId(Id);
				return Json(resultado);
			}
			catch (Exception)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" });
			}
		}

		[HttpPost]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public JsonResult Validate(string codeQrToValidate = "")
		{
			try
			{
				var resultado = _categoryService.Validate(codeQrToValidate, int.Parse(Util.MyUser.ID_COMPANY));
				return new HandleResults().ValidateResult(resultado);
			}
			catch (Exception)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" });
			}
		}

        [HttpPost]
        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        public JsonResult searchByDocument(string document = "")
        {
            try
            {
                return Json(_categoryService.searchByDocument(document, int.Parse(Util.MyUser.ID_COMPANY)));
            }
            catch (Exception ex)
            {
                return Json(new { _ = ex.Message});
            }
        }

        [HttpGet]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<int> GetCount()
		{
			try
			{
				var result = await _categoryService.GetCount();
				return result;
			}
			catch (Exception)
			{
				return 0;
			}
		}


		[HttpGet]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<JsonResult> GetCategoriesCodeQrByCompanyId(int companyId)
		{
			try
			{
				if (Session["user"] != null)
				{
					var resultado = await _categoryService.GetCategoriesCodeQrByCompanyId(companyId);
                    var result_return= Json(resultado, JsonRequestBehavior.AllowGet);
                    result_return.MaxJsonLength = int.MaxValue;
                    return result_return;
                }
				else
				{
					return Json(new { _ = "UD. no puede acceder a este recurso" });
				}
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
            List<string> whitelistTags = new List<string>() { "html", "script", "alert", "head", "body", "title", "deloitte", "strong", "center", "article", "aside", "link", "style" };
            foreach (string word in tags.ToList())
            {
                if (whitelistTags.Contains(word.Trim()))
                {
                    counter++;
                }
            }
            return counter > 0;
        }
    }
}