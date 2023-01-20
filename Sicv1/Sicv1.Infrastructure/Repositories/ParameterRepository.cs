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
    public class ParameterRepository : Connection, IParameterRepository
    {
        public async Task<int> Save(Parameter parameter)
        {
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                //var userId = Convert.ToInt32(HttpContext.Current.Session["username"].ToString().Split('¬')[0].ToString());
                //user.CREATED_USER = userId;
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_PARAMETERS_INSERT_UPDATE", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ID", parameter.ID);
                cmd.Parameters.AddWithValue("@PARENT_CODE", -1);
                cmd.Parameters.AddWithValue("@NAME", parameter.NAME);
                cmd.Parameters.AddWithValue("@DESCRIPTION", parameter.DESCRIPTION);
                cmd.Parameters.AddWithValue("@VALUE", parameter.VALUE);
                cmd.Parameters.AddWithValue("@STATUS", 1);
                cmd.Parameters.AddWithValue("@UPDATED_USER", Util.MyUser.ID);
                cmd.Parameters.AddWithValue("@DMLFLAG", "I");
                cmd.Parameters.AddWithValue("@COMPANY_ID", parameter.COMPANY_ID);
                cmd.Parameters.AddWithValue("@SAVE_OR_UPDATE", parameter.SAVE_OR_UPDATE);
                cmd.Parameters.AddWithValue("@ID_PADRE", parameter.ID_PADRE);
                //int i = await cmd.ExecuteNonQueryAsync();
                if (parameter.SAVE_OR_UPDATE==0) {
                    int i = (int)await cmd.ExecuteScalarAsync();
                    return i;
                }
                else {
                    int i = await cmd.ExecuteNonQueryAsync();
                    return i;
                }
                
            }
        }

        public async Task<int> Update(Parameter parameter)
        {
            int i = -1;
            
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                //var userId = Convert.ToInt32(HttpContext.Current.Session["username"].ToString().Split('¬')[0].ToString());
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_USERS_UPDATE", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //user.UPDATED_USER = userId;
                cmd.Parameters.AddWithValue("@ID", parameter.ID);
                cmd.Parameters.AddWithValue("@NAME", parameter.NAME);
                cmd.Parameters.AddWithValue("@LASTNAME_P", parameter.DESCRIPTION);
                
                cmd.Parameters.AddWithValue("@ESTADO", parameter.ESTADO);
                cmd.Parameters.AddWithValue("@UPDATED_USER", Util.MyUser.ID);
                cmd.Parameters.AddWithValue("@COMPANY_ID", parameter.COMPANY_ID);
                i = await cmd.ExecuteNonQueryAsync();
                return i;
            }
            
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<Parameter>> GetParameter(Parameter parameter)
        {
            //Parameter oParameter = null;
            List<Parameter> parameterList = new List<Parameter>();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_PARAMETERS_LIST", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PARENT_CODE", "-1");
                cmd.Parameters.AddWithValue("@TYPE", "PUBLIC");
                cmd.Parameters.AddWithValue("@ID_USER", Util.MyUser.ID);
                cmd.Parameters.AddWithValue("@ID_PADRE", parameter.ID_PADRE);
                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        Parameter oParameter = new Parameter
                        {
                            ID = dr.IsDBNull(dr.GetOrdinal("ID")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID")),
                            NAME = dr.IsDBNull(dr.GetOrdinal("NAME")) ? "nodata" : dr.GetString(dr.GetOrdinal("NAME")),
                            DESCRIPTION = dr.IsDBNull(dr.GetOrdinal("DESCRIPTION")) ? "nodata" : dr.GetString(dr.GetOrdinal("DESCRIPTION")),
                            VALUE = dr.IsDBNull(dr.GetOrdinal("VALUE")) ? "nodata" : dr.GetString(dr.GetOrdinal("VALUE")),
                            COMPANY_ID = dr.IsDBNull(dr.GetOrdinal("ID_COMPANY")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID_COMPANY")),
                            STATUS = Convert.ToBoolean(dr.GetInt32(dr.GetOrdinal("STATUS")))
                        };


                        parameterList.Add(oParameter);
                    }
                    dr.Close();
                }
            }
            return parameterList;
        }


        public async Task<int> DeleteParameter(Parameter parameter)
        {
            
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_PARAMETERS_SET_STATUS", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ID", parameter.ID);
                //cmd.Parameters.AddWithValue("@STATUS", parameter.STATUS);
                cmd.Parameters.AddWithValue("@UPDATED_USER", Util.MyUser.ID);
                int i = await cmd.ExecuteNonQueryAsync();
                return i;
            }
        }

        public async Task<int> UpdateStatus(int id)
        {
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                //var userId = Convert.ToInt32(HttpContext.Current.Session["username"].ToString().Split('¬')[0].ToString());
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_PARAMETERS_SET_STATUS", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@UPDATED_USER", Util.MyUser.ID);

                int i = await cmd.ExecuteNonQueryAsync();
                return i;
            }
        }

        public async Task<int> ActiveParameter(Parameter parameter)
        {
            int i = -1;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_PARAMETERS_CHANGE_STATUS", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", parameter.ID);
                cmd.Parameters.AddWithValue("@STATUS", parameter.STATUS);
                cmd.Parameters.AddWithValue("@DELETED_USER", Util.MyUser.ID);

                i = await cmd.ExecuteNonQueryAsync();
                return i;
            }
        }
    }
}
