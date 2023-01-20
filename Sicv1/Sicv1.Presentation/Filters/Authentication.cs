using Sicv1.Domain.Entities;
using Sicv1.Infrastructure.Cross;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sicv1.Presentation.Filters
{
    public class Authentication : AuthorizeAttribute
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["user"] == null)
                return false;

            User user = (User)httpContext.Session["user"];
            List<Menu> menus = Authorize.GetUrlsAllowedUser(user);
            string[] paths = httpContext.Request.FilePath.ToString().Split('/');
            bool isPathCompany = false;
            if (user.FK_ROLE == 5 || user.FK_ROLE == 2)
            {
                return true;
            }
            else
            {
                foreach (string path in paths)
                {
                    if (path.Equals("GetCompaniesByUserId"))
                    {
                        isPathCompany = true;
                    }
                }

                if (isPathCompany)
                {
                    return true;
                }
                else
                {
                    if (ActionName != null)
                    {
                        if (menus.Any(x => x.CONTROLLER == ControllerName && x.ACTION == ActionName)) return true;
                    }
                    else
                    {
                        if (menus.Any(x => x.CONTROLLER == ControllerName)) return true;
                    }

                }
                Redirect.To(httpContext);
                return false;
            }

        }
    }

    public class Logged : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return !(httpContext.Session["user"] == null);
        }
    }

}