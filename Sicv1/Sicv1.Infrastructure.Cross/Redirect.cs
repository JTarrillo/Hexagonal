using System.Configuration;
using System.Web;

namespace Sicv1.Infrastructure.Cross
{
    public static class Redirect
    {
        public static void To(HttpContextBase httpContext)
        {
            httpContext.Response.Redirect(ConfigurationManager.AppSettings["RedirectTo"].ToString());
        }
    }
}
