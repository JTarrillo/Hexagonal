using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sicv1.Infrastructure.Cross
{
	public static class Util
	{
		public static User MyUser
		{
			get {
				try
				{
					return (User)HttpContext.Current.Session["user"];
				}
				catch (Exception)
				{
					throw new Exception("El usuario no tiene sesión activa");
				}
			}
		}
	}
}