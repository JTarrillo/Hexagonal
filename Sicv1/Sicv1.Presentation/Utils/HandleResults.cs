using System.Text;
using System.Web.Mvc;

namespace Sicv1.Presentation.Utils
{
    public class HandleResults : Controller
    {
        public JsonResult DisplayErrorImages()
        {
            return Json(new
            {
                status = false,
                message = "Error en directorio imágenes",
            });
        }

        public JsonResult ValidateResult(int resultado, object data = null)
        {
            if (resultado > 0)
            {
                return Json(new
                {
                    code = 200,
                    status = true,
                    result = resultado,
                    message = "Proceso exitoso",
                    data
                });
            }
            else if (resultado == -1)
            {
                return Json(new
                {
                    code = 800,
                    status = false,
                    message = "Registro Duplicado"
                });
            }
            else
            {
                return Json(new
                {
                    status = false,
                    message = "Error en la operación realizada"
                });
            }
        }

        public JsonResult ValidateResult(object resultado)
        {
            if (resultado != null)
            {
                if (resultado.GetType().GetProperty("CODE_QR").GetValue(resultado).ToString() == "Exists".Trim()) { return Json(new { status = false, data = "Exists" }); }
                if (resultado.GetType().GetProperty("CODE_QR").GetValue(resultado).ToString() == "Null".Trim()) { return Json(new { status = false, data = "Null" }); }
                if (resultado.GetType().GetProperty("CODE_QR").GetValue(resultado).ToString() == "NotBelongCompany".Trim()) { return Json(new { status = false, data = "NotBelong" }); }
                return Json(new { code = 200, status = true, data = resultado });
            }
            else { return Json(new { status = false, message = "Error en la operación realizada", data = resultado }); }
        }
    }
}