using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Entities;
using Sicv1.Infrastructure.Cross;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;

namespace Sicv1.Infrastructure.Repositories
{
    public class RoleRepository : Connection, IRoleRepository
    {
        public async Task<IEnumerable<Role>> GetRoles(int RoleId)
        {
            Role oRole = null;
            List<Role> roles = new List<Role>();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_ROLES_LIST", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ROLE_ID", RoleId);
                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        oRole = new Role();
                        oRole.ID = dr.IsDBNull(dr.GetOrdinal("ID")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID"));
                        oRole.NAME = dr.IsDBNull(dr.GetOrdinal("NAME")) ? "nodata" : dr.GetString(dr.GetOrdinal("NAME"));
                        oRole.DESCRIPTION = dr.IsDBNull(dr.GetOrdinal("DESCRIPTION")) ? "nodata" : dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                        oRole.ESTADO = dr.IsDBNull(dr.GetOrdinal("STATUS")) ? -1 : dr.GetInt32(dr.GetOrdinal("STATUS"));
                        roles.Add(oRole);
                    }
                    dr.Close();
                }
            }
            return roles;
        }

        public async Task<int> Save(Role role)
        {
            int i = -1;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_ROLES_INSERT", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                //var userId = Convert.ToInt32(HttpContext.Current.Session["username"].ToString().Split('¬')[0].ToString());
                cmd.Parameters.AddWithValue("@NAME", role.NAME);
                cmd.Parameters.AddWithValue("@DESCRIPTION", role.DESCRIPTION);
                cmd.Parameters.AddWithValue("@STATUS", role.ESTADO);
                role.CREATED_USER = Util.MyUser.ID;
                cmd.Parameters.AddWithValue("@CREATED_USER", Util.MyUser.ID);
                i = await cmd.ExecuteNonQueryAsync();
                return i;
            }
        }

        public async Task<int> Update(Role role)
        {
            int i = -1;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_ROLES_UPDATE", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", role.NAME);
                cmd.Parameters.AddWithValue("@NAME", role.NAME);
                cmd.Parameters.AddWithValue("@DESCRIPTION", role.DESCRIPTION);
                cmd.Parameters.AddWithValue("@STATUS", role.ESTADO);
                //var userId = Convert.ToInt32(HttpContext.Current.Session["username"].ToString().Split('¬')[0].ToString());
                role.UPDATED_USER = Util.MyUser.ID;
                cmd.Parameters.AddWithValue("@UPDATED_USER", Util.MyUser.ID);
                i = await cmd.ExecuteNonQueryAsync();
                return i;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
