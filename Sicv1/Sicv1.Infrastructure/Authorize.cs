using Oncosaludv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Oncosaludv1.Infrastructure.Cross
{
    public static class Authorize
    {
        public static int GetUrlsAllowedUser(List<Menu> uris, ref Menu oMenu)
        {
            int UserId = Convert.ToInt32(HttpContext.Current.Session["username"].ToString().Split('¬')[0].ToString());
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["db_connection"].ToString()))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_MENUS_BY_IDUSER", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USERID", UserId);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        oMenu = new Menu();
                        oMenu.RAWURL = dr.IsDBNull(dr.GetOrdinal("RAWURL")) ? "nodata" : dr.GetString(dr.GetOrdinal("RAWURL"));
                        uris.Add(oMenu);
                    }
                    dr.Close();
                }
            }
            return UserId;
        }

    }
}
