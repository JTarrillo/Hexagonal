using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Sicv1.Infrastructure.Cross
{
	public static class Authorize
	{
		public static List<Menu> GetUrlsAllowedUser(User user)
		{
			List<Menu> menus = new List<Menu>();
			try
			{
				//int UserId = Convert.ToInt32(HttpContext.Current.Session["username"].ToString().Split('¬')[0].ToString());
				using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["db_connection"].ToString()))
				{
					cn.Open();
					SqlCommand cmd = new SqlCommand("SP_MENUS_BY_IDUSER", cn);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@USERID", user.ID);
					SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
					if (dr.HasRows)
					{
						Menu oMenu;
						while (dr.Read())
						{
							oMenu = new Menu();
							oMenu.RAWURL = dr.IsDBNull(dr.GetOrdinal("RAWURL")) ? "nodata" : dr.GetString(dr.GetOrdinal("RAWURL"));
							oMenu.CONTROLLER = dr.IsDBNull(dr.GetOrdinal("CONTROLLER")) ? "nodata" : dr.GetString(dr.GetOrdinal("CONTROLLER"));
							oMenu.ACTION = dr.IsDBNull(dr.GetOrdinal("ACTION")) ? "nodata" : dr.GetString(dr.GetOrdinal("ACTION"));
							menus.Add(oMenu);
						}
						dr.Close();
					}
				}
				//return UserId;
			}
			catch (Exception)
			{
				
			}
			return menus;
		}
	}
}
