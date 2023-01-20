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
    public class CountryRepository : Connection, ICountryRepository
    {       

        public async Task<IEnumerable<Country>> GetCountry()
        {
            Country oCountry = null;
            List<Country> country = new List<Country>();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_COUNTRY_LIST", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        oCountry = new Country();
                        oCountry.ID = dr.IsDBNull(dr.GetOrdinal("ID")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID"));
                        oCountry.NAME_COUNTRY = dr.IsDBNull(dr.GetOrdinal("NAME_COUNTRY")) ? "nodata" : dr.GetString(dr.GetOrdinal("NAME_COUNTRY"));
                        oCountry.IMAGE_FIRST = dr.IsDBNull(dr.GetOrdinal("IMAGE_FIRST")) ? "nodata" : dr.GetString(dr.GetOrdinal("IMAGE_FIRST"));
                        oCountry.STATUS = dr.IsDBNull(dr.GetOrdinal("STATUS")) ? false : dr.GetBoolean(dr.GetOrdinal("STATUS"));
                        country.Add(oCountry);
                    }
                    dr.Close();
                }
            }
            return country;
        }

        public async Task<int> SaveCountry(Country country)
        {
            int i = 0;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_COUNTRY_SAVE", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                //var userId = Convert.ToInt32(HttpContext.Current.Session["username"].ToString().Split('¬')[0].ToString());
                cmd.Parameters.AddWithValue("@NAME_COUNTRY", country.NAME_COUNTRY);
                cmd.Parameters.AddWithValue("@IMAGE_FIRST", country.IMAGE_FIRST);
                country.CREATED_USER = Util.MyUser.ID;

                cmd.Parameters.AddWithValue("@CREATED_USER", Util.MyUser.ID);
                i = (int)cmd.ExecuteScalar();
                return i;
            }
        }

        public async Task<int> UpdateCountry(Country country)
        {
            int i = -1;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_COUNTRY_UPDATE", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                //var userId = Convert.ToInt32(HttpContext.Current.Session["username"].ToString().Split('¬')[0].ToString());
                cmd.Parameters.AddWithValue("@NAME_COUNTRY", country.NAME_COUNTRY);
                cmd.Parameters.AddWithValue("@IMAGE_FIRST", country.IMAGE_FIRST);
                cmd.Parameters.AddWithValue("@ID", country.ID);
                cmd.Parameters.AddWithValue("@UPDATED_USER", Util.MyUser.ID);

                i = await cmd.ExecuteNonQueryAsync();
                return i;
            }
        }

        public async Task<int> DeleteCountry(Country country)
        {
            int i = -1;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_COUNTRY_DELETE", cn);
                cmd.CommandType = CommandType.StoredProcedure;            
                cmd.Parameters.AddWithValue("@ID", country.ID);
                cmd.Parameters.AddWithValue("@DELETED_USER", Util.MyUser.ID);

                i = await cmd.ExecuteNonQueryAsync();
                return i;
            }
        }

        public async Task<int> ActiveCountry(Country country)
        {
            int i = -1;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_COUNTRY_ACTIVE", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", country.ID);
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
