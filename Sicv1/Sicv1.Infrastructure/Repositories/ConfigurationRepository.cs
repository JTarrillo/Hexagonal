using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Entities;
using Sicv1.Infrastructure.Cross;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Sicv1.Infrastructure.Repositories
{
    public class ConfigurationRepository : Connection, IConfigurationRepository
    {

        public async Task<ConfigurationResponseBE> UpdateConfiguration(Domain.Entities.Configuration conf)
        {
            int i = -1;
            ConfigurationResponseBE objResponse = new ConfigurationResponseBE();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_CONFIGURATION_UPDATE", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DATA_PRIVACY_POLICY", conf.DATA_PRIVACY_POLICY);
                cmd.Parameters.AddWithValue("@TREATMENT_OF_PERSONAL_DATA", conf.TREATMENT_OF_PERSONAL_DATA);
                cmd.Parameters.AddWithValue("@NAME", conf.NAME);
                cmd.Parameters.AddWithValue("@DESCRIPTION", conf.DESCRIPTION);
                cmd.Parameters.AddWithValue("@URL_LOGO", conf.URL_LOGO);
                cmd.Parameters.AddWithValue("@URL_OFICIAL", conf.URL_OFICIAL);
                cmd.Parameters.AddWithValue("@VERSION", conf.VERSION);
                try
                {
                    i = await cmd.ExecuteNonQueryAsync();
                    if (i == 1)
                    {
                        objResponse.STATUS = true;
                    }
                    else
                    {
                        objResponse.STATUS = false;
                        objResponse.MESSAGE = "Error al actualizar";
                    }
                }
                catch (Exception ex)
                {
                    objResponse.STATUS = false;
                    objResponse.MESSAGE = "Error al actualizar [" + ex.Message + "]";
                }

                return objResponse;
            }
        }

        public async Task<ConfigurationResponseBE> GetValuesCurrent()
        {
            ConfigurationResponseBE objResponse = new ConfigurationResponseBE();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_CONFIGURATION_GET_VALUES", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                    if (dr.HasRows)
                    {
                        while (await dr.ReadAsync())
                        {
                            objResponse.DATA.NAME = dr.IsDBNull(dr.GetOrdinal("APP_NAME")) ? "" : dr.GetString(dr.GetOrdinal("APP_NAME"));
                            objResponse.DATA.DESCRIPTION = dr.IsDBNull(dr.GetOrdinal("APP_DESCRIPTION")) ? "" : dr.GetString(dr.GetOrdinal("APP_DESCRIPTION"));
                            objResponse.DATA.DATA_PRIVACY_POLICY = dr.IsDBNull(dr.GetOrdinal("DATA_PRIVACY_POLICY")) ? "" : dr.GetString(dr.GetOrdinal("DATA_PRIVACY_POLICY"));
                            objResponse.DATA.TREATMENT_OF_PERSONAL_DATA = dr.IsDBNull(dr.GetOrdinal("TREATMENT_OF_PERSONAL_DATA")) ? "" : dr.GetString(dr.GetOrdinal("TREATMENT_OF_PERSONAL_DATA"));
                            objResponse.DATA.VERSION = dr.IsDBNull(dr.GetOrdinal("VERSION")) ? "" : dr.GetString(dr.GetOrdinal("VERSION"));
                            objResponse.DATA.URL_LOGO = dr.IsDBNull(dr.GetOrdinal("URL_LOGO")) ? "" : dr.GetString(dr.GetOrdinal("URL_LOGO"));
                            objResponse.DATA.URL_OFICIAL = dr.IsDBNull(dr.GetOrdinal("URL_OFICIAL")) ? "" : dr.GetString(dr.GetOrdinal("URL_OFICIAL"));
                        }
                        dr.Close();
                    }
                    objResponse.STATUS = true;
                }
                catch (Exception ex)
                {
                    objResponse.STATUS = false;
                    objResponse.MESSAGE = "Error al traer datos [" + ex.Message + "]";
                    objResponse.DATA = null;
                }

                return objResponse;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}