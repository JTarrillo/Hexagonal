using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Entities;
using Sicv1.Infrastructure.Cross;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;

namespace Sicv1.Infrastructure.Repositories
{
    public class PopupConfigurationRepository : Connection, IPopupConfigurationRepository
    {

        public async Task<PopupConfiguration> GetPopupConfigurations()
        {
            PopupConfiguration opopupConfiguration = null;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_POPUP_CONFIGURATION_LIST", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.HasRows)
                {
                    await dr.ReadAsync();
                    opopupConfiguration = new PopupConfiguration();
                    opopupConfiguration.ID = dr.IsDBNull(dr.GetOrdinal("ID")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID"));
                    opopupConfiguration.URL = dr.IsDBNull(dr.GetOrdinal("URL")) ? "nodata" : dr.GetString(dr.GetOrdinal("URL"));
                    opopupConfiguration.DESCRIPTION = dr.IsDBNull(dr.GetOrdinal("DESCRIPTION")) ? "nodata" : dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                    opopupConfiguration.LINK_IMAGE = dr.IsDBNull(dr.GetOrdinal("LINK_IMAGE")) ? "nodata" : dr.GetString(dr.GetOrdinal("LINK_IMAGE"));
                    opopupConfiguration.IS_LINKEABLE = dr.IsDBNull(dr.GetOrdinal("IS_LINKEABLE")) ? false : dr.GetBoolean(dr.GetOrdinal("IS_LINKEABLE"));
                    opopupConfiguration.IS_ACTIVE = dr.IsDBNull(dr.GetOrdinal("IS_ACTIVE")) ? false : dr.GetBoolean(dr.GetOrdinal("IS_ACTIVE"));

                    opopupConfiguration.LINK_IMAGE_BANNER = dr.IsDBNull(dr.GetOrdinal("LINK_IMAGE")) ? "nodata" : dr.GetString(dr.GetOrdinal("LINK_IMAGE"));


                    opopupConfiguration.LINK_IMAGE_BANNER = dr.IsDBNull(dr.GetOrdinal("LINK_IMAGE_BANNER")) ? "nodata" : dr.GetString(dr.GetOrdinal("LINK_IMAGE_BANNER"));
                    opopupConfiguration.TERMS_CONDITION_BANNER = dr.IsDBNull(dr.GetOrdinal("DESCRIPTION_BANNER")) ? "nodata" : dr.GetString(dr.GetOrdinal("DESCRIPTION_BANNER")); ;
                    opopupConfiguration.IS_ACTIVE_BANNER = dr.IsDBNull(dr.GetOrdinal("ACTIVE_BANNER")) ? false : dr.GetBoolean(dr.GetOrdinal("ACTIVE_BANNER"));
                    dr.Close();
                }
            }
            return opopupConfiguration;
        }

        public async Task<int> Save(PopupConfiguration popupConfiguration)
        {
            int i = -1;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_POPUP_CONFIGURATION_INSERT", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@URL", popupConfiguration.URL);
                cmd.Parameters.AddWithValue("@DESCRIPTION", popupConfiguration.DESCRIPTION);
                cmd.Parameters.AddWithValue("@LINK_IMAGE", popupConfiguration.LINK_IMAGE);
                cmd.Parameters.AddWithValue("@IS_LINKEABLE", popupConfiguration.IS_LINKEABLE);
                cmd.Parameters.AddWithValue("@IS_ACTIVE", popupConfiguration.IS_ACTIVE);
                cmd.Parameters.AddWithValue("@CREATED_USER", popupConfiguration.CREATED_USER);
                i = await cmd.ExecuteNonQueryAsync();
            }
            return i;
        }

        public async Task<int> Update(PopupConfiguration popupConfiguration)
        {
            int i = -1;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_POPUP_CONFIGURATION_UPDATE", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", popupConfiguration.ID);
                cmd.Parameters.AddWithValue("@URL", popupConfiguration.URL);
                cmd.Parameters.AddWithValue("@DESCRIPTION", popupConfiguration.DESCRIPTION);
                cmd.Parameters.AddWithValue("@LINK_IMAGE", popupConfiguration.LINK_IMAGE);
                cmd.Parameters.AddWithValue("@IS_LINKEABLE", popupConfiguration.IS_LINKEABLE);
                cmd.Parameters.AddWithValue("@IS_ACTIVE", popupConfiguration.IS_ACTIVE);

                cmd.Parameters.AddWithValue("@TERMS_BANNER", popupConfiguration.TERMS_CONDITION_BANNER);
                cmd.Parameters.AddWithValue("@LINK_IMAGE_BANNER", popupConfiguration.LINK_IMAGE_BANNER);
                cmd.Parameters.AddWithValue("@IS_ACTIVE_BANNER", popupConfiguration.IS_ACTIVE_BANNER);

				popupConfiguration.UPDATED_USER = Util.MyUser.ID;
                cmd.Parameters.AddWithValue("@UPDATED_USER", popupConfiguration.UPDATED_USER);
                try
                {
                    i = await cmd.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return i;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
