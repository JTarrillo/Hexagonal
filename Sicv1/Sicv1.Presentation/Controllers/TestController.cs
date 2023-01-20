using Sicv1.Presentation.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sicv1.Presentation.Controllers
{
	public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
		//[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult MyAction()
		{
			try
			{
				var req = Request.Form;
				var val = req[0];
				return Json("Hola");
			}			
			catch (Exception ex)
			{
				return Json(ex.Message);
			}
		}
    }
}