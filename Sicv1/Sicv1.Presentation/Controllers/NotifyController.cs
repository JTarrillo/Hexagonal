using Newtonsoft.Json;
using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using Sicv1.Presentation.Filters;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sicv1.Presentation.Controllers
{
	[Authentication(ControllerName = "Notify")]
	public class NotifyController : Controller
	{
		private readonly INotifyService notifyService;

		public NotifyController(INotifyService notifyService)
		{
			this.notifyService = notifyService;
		}

		[HttpGet]
		[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
		public async Task<ActionResult> Index()
		{
			return View();
		}

        [HttpPost]
        public async Task<ActionResult> Send(Notification notification)
        {
            try
            {
                string resultado = "";
                if (notification.typeToggle.Equals("custom"))
                {
                    foreach (Tokens rs in notification.data)
                    {
                        if (rs != null)
                        {
                            if (!rs.token.Equals(""))
                            {
                                ExpoPush obj = new ExpoPush();
                                obj.to = rs.token;
                                obj.title = notification.title;
                                obj.body = notification.body;
                                obj.priority = "high";
                                obj.sound = "default";
                                obj.channelId = "BreakingNews";
                                obj._displayInForeground = true;

                                string tramaJSON = JsonConvert.SerializeObject(obj);
                                //byte[] postByteArray = new ASCIIEncoding().GetBytes(tramaJSON);
                                await Task.Run(() => request(tramaJSON));

                            }
                        }
                    }
                }
                else // todos los usuarios
                {
                    List<string> lst = await notifyService.Send(notification);
                    
                    List<ExpoPush> t = new List<ExpoPush>();
                    int i = 0;
                    int x = 0;
                    int b = 0;
                    foreach (string rs in lst)
                    {
                        if (rs != null)
                        {
                            if (rs.Length != 0)
                            {
                                t.Add(new ExpoPush()
                                {
                                    to = rs,
                                    title = notification.title,
                                    body = notification.body,
                                    priority = "high",
                                    sound = "default",
                                    channelId = "BreakingNews",
                                    _displayInForeground = true
                                });

                                x++;
                                i++;

                                if (i<100 && x >= lst.Count)
                                {
                                    resultado = JsonConvert.SerializeObject(t);
                                    await Task.Run(() => request(resultado));
                                    t.Clear();
                                    i = 0;
                                    x = 0;
                                    break;//sale del bucle
                                }
                                else{
                                    if(i==100) {
                                        i = 0;
                                        resultado = JsonConvert.SerializeObject(t);
                                        await Task.Run(() => request(resultado));
                                        t.Clear();
                                        b++;
                                    }                                 
                                    
                                }

                                
                                //string tramaJSON = Newtonsoft.Json.JsonConvert.SerializeObject(t);
                                ////byte[] postByteArray = new ASCIIEncoding().GetBytes(tramaJSON);
                                 
                            //await Task.Run(() => request(tramaJSON));

                            }
                        }

                        
                    }



                }

                //resultado = JsonConvert.SerializeObject(t);

                //await Task.Run(() => request(resultado));

                return Json(new Result() { STATUS = true, MESSAGE = "Mensaje Enviado" });
            }
            catch (Exception ex)
            {
                return Json(new Result() { STATUS = false, MESSAGE = ex.Message, TRACE = ex.StackTrace });
            }
        }

        [HttpPost]
        public async Task<ActionResult> getDatafromDocument(User objUser)
        {
            try
            {
                User objEntity = await notifyService.getDatafromDocument(objUser);
                if (objEntity != null)
                {
                    if (objEntity.ID != 0)
                    {
                        return Json(new Result() { STATUS = true, MESSAGE = JsonConvert.SerializeObject(objEntity) });
                    }
                    else
                    {
                        return Json(new Result() { STATUS = false, MESSAGE = "No existe usuario" });
                    }
                }
                else
                {
                    return Json(new Result() { STATUS = false, MESSAGE = "No existe usuario" });
                }
            }
            catch (Exception ex)
            {
                return Json(new Result() { STATUS = false, MESSAGE = ex.Message, TRACE = ex.StackTrace });
            }
        }


        public static bool request(string json)
        {
            bool response = false;
            try
            {
                var client = new RestClient("https://exp.host/--/api/v2/push/send");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept-Encoding", "application/gzip");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                IRestResponse response2 = client.Execute(request);
                response = true;
            }
            catch (Exception ex)
            {
                response = false;
            }
            return response;
        }
    }
}