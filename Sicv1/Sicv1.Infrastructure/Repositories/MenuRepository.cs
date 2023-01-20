using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Entities;
using Sicv1.Infrastructure.Cross;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Sicv1.Infrastructure.Repositories
{
    public class MenuRepository : Connection, IMenuRepository
    {
        public async Task<IEnumerable<Menu>> GetMenusByUserId(int UserId)
        {
            Menu oMenu = null;
            List<Menu> menus = new List<Menu>();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_MENUS_BY_IDUSER", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USERID", UserId);
                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        oMenu = new Menu();
                        oMenu.ID = dr.IsDBNull(dr.GetOrdinal("ID")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID"));
                        oMenu.NAME = dr.IsDBNull(dr.GetOrdinal("NAME")) ? "nodata" : dr.GetString(dr.GetOrdinal("NAME"));
                        oMenu.CONTROLLER = dr.IsDBNull(dr.GetOrdinal("CONTROLLER")) ? "nodata" : dr.GetString(dr.GetOrdinal("CONTROLLER"));
                        oMenu.ACTION = dr.IsDBNull(dr.GetOrdinal("ACTION")) ? "nodata" : dr.GetString(dr.GetOrdinal("ACTION"));
                        oMenu.CLASS_AHREF = dr.IsDBNull(dr.GetOrdinal("CLASS_AHREF")) ? "nodata" : dr.GetString(dr.GetOrdinal("CLASS_AHREF"));
                        oMenu.CLASS_I = dr.IsDBNull(dr.GetOrdinal("CLASS_I")) ? "nodata" : dr.GetString(dr.GetOrdinal("CLASS_I"));
                        oMenu.RAWURL = dr.IsDBNull(dr.GetOrdinal("RAWURL")) ? "nodata" : dr.GetString(dr.GetOrdinal("RAWURL"));
                        oMenu.STATUS = dr.IsDBNull(dr.GetOrdinal("STATUS")) ? false : dr.GetBoolean(dr.GetOrdinal("STATUS"));
                        menus.Add(oMenu);
                    }
                    dr.Close();
                }
            }
            return menus;
        }

        

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
