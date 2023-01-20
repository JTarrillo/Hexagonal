using System.Configuration;
using System.Web;

namespace Oncosaludv1.Infrastructure.Cross
{
    public static class Redirect
    {
        public static void To(HttpContextBase httpContext)
        {
            httpContext.Response.Redirect(ConfigurationManager.AppSettings["RedirectTo"].ToString());
        }
    }
}
