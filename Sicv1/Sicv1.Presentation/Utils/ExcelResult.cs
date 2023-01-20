using ClosedXML.Excel;
using System;
using System.IO;
using System.Web.Mvc;

namespace Sicv1.Presentation.Utils
{
    public class ExcelResult : ActionResult
    {
        private readonly XLWorkbook _workbook;
        private readonly string _fileName;

        public ExcelResult(XLWorkbook workbook, string fileName)
        {
            this._workbook = workbook;
            this._fileName = fileName;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            try
            {
                var response = context.HttpContext.Response;
                response.Clear();
                response.ContentType = "application/vnd.openxmlformats-officedocument." + "spreadsheetml.sheet";
                response.AddHeader("content-disposition",
                                   "attachment;filename=\"" + _fileName + ".xlsx\"");

                using (var memoryStream = new MemoryStream())
                {
                    _workbook.SaveAs(memoryStream);
                    memoryStream.WriteTo(response.OutputStream);
                }
                response.End();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }
    }
}