using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using Sicv1.Presentation.Filters;
using Sicv1.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using Sicv1.Presentation.Utils;
using System.Configuration;

namespace Sicv1.Presentation.Controllers
{
	[Authentication(ControllerName = "Newsness")]
	public class NewsnessController : Controller
	{
		private readonly INewsNessService _newsNessService;
		public NewsnessController(INewsNessService newsNessService)
		{
			this._newsNessService = newsNessService;
		}

		private static Random random = new Random();

		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<ActionResult> Index()
		{	
			return View();
		}

		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		[HttpGet]
		public async Task<JsonResult> GetNewsNesses()
		{
			try
			{
				var resultado = await _newsNessService.GetNewsNesses();
				return Json(resultado, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" + ex.Message });
			}
		}

		public async Task<JsonResult> GetNewsNessType()
		{
			try
			{
				var resultado = await _newsNessService.GetNewsNessType();
				return Json(resultado, JsonRequestBehavior.AllowGet);
			}
			catch (Exception)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" });
			}
		}

		[HttpPost]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<JsonResult> SaveNewsness(NewsnessViewModel model)
		{
			try
			{
				NewsNess newsness = null;				

				if (model.IMAGEBASE64 == null)
					return Json(new { status = false, message = "No ha enviado imagen" });
				string clean = model.IMAGEBASE64.Split(',')[1];
				if (!clean.Substring(0, 1).Equals("/") && !clean.Substring(0, 1).Equals("i"))
					return Json(new { status = false, message = "Tipo de archivo no permitido" });
				string extension = clean.Substring(0, 1).Equals("/") ? ".jpg" : ".png";
				string nameFile = "newness_" + RandomString(10) + extension;
				try
				{
					var bytes = Convert.FromBase64String(clean);
					var text = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
					bool validate = ContainsHTML(text);
					if (validate)
						return Json(new { status = false, message = "Archivo no permitido." });
					model.HDFILENAME = nameFile;
				}
				catch (Exception ex)
				{
					return Json(new { status = false, message = "Formato no valido." });
				}

				string dataimageExtension = Images.GetDataImageExtensionNewsNess(model);
				string imagebase64 = Images.GetImageB64NewsNess(model, dataimageExtension);
				byte[] image = Images.FromB64ToStr(imagebase64);

				var folder = ConfigurationManager.AppSettings["AWSBucketPathNewsNess"].ToString();

				newsness = new NewsNess();

				newsness.ID_CATEGORY = model.ID_CATEGORY;
				newsness.ID_TYPE_NEWNESS = model.ID_TYPE_NEWNESS;
				newsness.URL = model.URL;
				newsness.VIDEO = model.ISVIDEO;
				newsness.TITLE = model.TITLE;
				newsness.DESCRIPTION = model.DESCRIPTION;
				newsness.IMAGE_FIRST = "https://imagesoncobenefits.s3-sa-east-1.amazonaws.com" + ConfigurationManager.AppSettings["AWSFilePathNewsNess"].ToString() + model.HDFILENAME;
				newsness.EXPIRE = model.EXPIRE;
				newsness.DATE_EXPIRATION = model.DATE_EXPIRATION;
				newsness.DATE_PUBLICATION = model.DATE_PUBLICATION;

				var resultado = await _newsNessService.SaveNewsness(newsness);

				var res =  await AmazonWS.UploadImage(model,
					newsness,
					image,
					resultado,
					new HandleResults().ValidateResult(resultado),
					new HandleResults().DisplayErrorImages());

				return Json(new { status = true, id = resultado, /*img = newsness.IMAGE_FIRST*/ });

			}
			catch (Exception ex)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" + ex.Message });
			}
		}

		[HttpPost]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<JsonResult> UpdateNewsness(NewsnessViewModel model)
		{
			try
			{
				NewsNess newsness = null;
				byte[] image = null;

				if (model.IMAGEBASE64 != null) {
					
					string clean = model.IMAGEBASE64.Split(',')[1];
					if (!clean.Substring(0, 1).Equals("/") && !clean.Substring(0, 1).Equals("i"))
						return Json(new { status = false, message = "Tipo de archivo no permitido" });
					string extension = clean.Substring(0, 1).Equals("/") ? ".jpg" : ".png";
					string nameFile = "newness_" + RandomString(10) + extension;
					try
					{
						var bytes = Convert.FromBase64String(clean);
						var text = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
						bool validate = ContainsHTML(text);
						if (validate)
							return Json(new { status = false, message = "Archivo no permitido." });
						model.HDFILENAME = nameFile;
					}
					catch (Exception ex)
					{
						return Json(new { status = false, message = "Formato no valido." });
					}

					string dataimageExtension = Images.GetDataImageExtensionNewsNess(model);
					string imagebase64 = Images.GetImageB64NewsNess(model, dataimageExtension);
					image = Images.FromB64ToStr(imagebase64);

					var folder = ConfigurationManager.AppSettings["AWSBucketPathNewsNess"].ToString();
				}
				

				newsness = new NewsNess();

				newsness.ID = model.ID;

				newsness.ID_CATEGORY = model.ID_CATEGORY;
				newsness.ID_TYPE_NEWNESS = model.ID_TYPE_NEWNESS;
				newsness.URL = model.URL;
				newsness.VIDEO = model.ISVIDEO;
				newsness.TITLE = model.TITLE;
				newsness.DESCRIPTION = model.DESCRIPTION;
				newsness.IMAGE_FIRST = (model.IMAGEBASE64==null) ? model.IMAGE_FIRST : "https://imagesoncobenefits.s3-sa-east-1.amazonaws.com" + ConfigurationManager.AppSettings["AWSFilePathNewsNess"].ToString() + model.HDFILENAME;
				newsness.EXPIRE = model.EXPIRE;
				newsness.DATE_EXPIRATION = model.DATE_EXPIRATION;
				newsness.DATE_PUBLICATION = model.DATE_PUBLICATION;

				var resultado = await _newsNessService.UpdateNewsness(newsness);
				if (model.IMAGEBASE64 != null) {
					var res = await AmazonWS.UploadImage(model,
					newsness,
					image,
					resultado,
					new HandleResults().ValidateResult(resultado),
					new HandleResults().DisplayErrorImages());
				}
				

				return Json(new { status = true, message = resultado });
			}
			catch (Exception ex)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" + ex.Message });
			}
		}

		[HttpPost]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<JsonResult> DeleteNewsness(NewsnessViewModel model)
		{
			try
			{

				NewsNess newsness = new NewsNess();

				newsness.ID = model.ID;

				var resultado = await _newsNessService.DeleteNewsness(newsness);

				return Json(new { status = true, message = resultado });
			}
			catch (Exception ex)
			{
				return Json(new { _ = "UD. no puede acceder a este recurso" + ex.Message });
			}
		}

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