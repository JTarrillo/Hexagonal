using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Entities;
using Sicv1.Infrastructure.Cross;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;

namespace Sicv1.Infrastructure.Repositories
{
    public class NotifyRepository : Connection, INotifyRepository
    {
        public async Task<List<string>> Send(Notification notification)//se añade segments como variable de array de segmentos
        {
            List<string> list = new List<string>();
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["db_connection"].ToString()))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_USERS_GET_TOKENS", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SEGMENTS", notification.segments);
                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        list.Add(dr.GetString(dr.GetOrdinal("TOKENS")).ToString());
                    }
                    dr.Close();
                }
            }
            return list;
        }

        public async Task<User> getDatafromDocument(User objUser)
        {
            User objReturn = new User();
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["db_connection"].ToString()))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_USERS_VALIDATE_EXISTS", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DOCUMENT", objUser.DOCUMENT);
                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        objReturn.ID = dr.GetInt32(dr.GetOrdinal("ID"));
                        objReturn.NAME = dr.GetString(dr.GetOrdinal("NAME"));
                        objReturn.LASTNAME_P = dr.GetString(dr.GetOrdinal("LASTNAME_P"));
                        objReturn.TOKEN = dr.GetString(dr.GetOrdinal("TOKEN"));
                    };
                }
                dr.Close();
            }
            return objReturn;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
