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
    public class NewsNessRepository : Connection, INewsNessRepository
    {
        public async Task<IEnumerable<NewsNess>> GetNewsNesses()
        {
            NewsNess oNewsNess = null;
            List<NewsNess> newsNesses = new List<NewsNess>();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                //SqlCommand cmd = new SqlCommand("SP_NEWNESS_LIST", cn);
                SqlCommand cmd = new SqlCommand("SP_NEWNESS_WEB_LIST", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        oNewsNess = new NewsNess();

                        oNewsNess.ID = dr.IsDBNull(dr.GetOrdinal("ID")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID"));
                        oNewsNess.ID_CATEGORY = dr.IsDBNull(dr.GetOrdinal("ID_CATEGORY")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID_CATEGORY"));
                        oNewsNess.CATEGORY = dr.IsDBNull(dr.GetOrdinal("CATEGORY")) ? "" : dr.GetString(dr.GetOrdinal("CATEGORY"));
                        oNewsNess.ID_TYPE_NEWNESS = dr.IsDBNull(dr.GetOrdinal("ID_TYPE_NEWNESS")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID_TYPE_NEWNESS"));
                        oNewsNess.TYPE_NEWNESS = dr.IsDBNull(dr.GetOrdinal("TYPE_NEWNESS")) ? "" : dr.GetString(dr.GetOrdinal("TYPE_NEWNESS"));
                        oNewsNess.URL = dr.IsDBNull(dr.GetOrdinal("URL")) ? "" : dr.GetString(dr.GetOrdinal("URL"));
                        oNewsNess.VIDEO = dr.IsDBNull(dr.GetOrdinal("VIDEO")) ? false : dr.GetBoolean(dr.GetOrdinal("VIDEO"));
                        oNewsNess.TITLE = dr.IsDBNull(dr.GetOrdinal("TITLE")) ? "" : dr.GetString(dr.GetOrdinal("TITLE"));
                        oNewsNess.DATE_PUBLISH = dr.IsDBNull(dr.GetOrdinal("DATE_PUBLISH")) ? "" : dr.GetString(dr.GetOrdinal("DATE_PUBLISH"));
                        oNewsNess.DESCRIPTION = dr.IsDBNull(dr.GetOrdinal("DESCRIPTION")) ? "" : dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                        oNewsNess.IMAGE_FIRST = dr.IsDBNull(dr.GetOrdinal("IMAGE_FIRST")) ? "" : dr.GetString(dr.GetOrdinal("IMAGE_FIRST"));
                        oNewsNess.DATE_EXPIRATION = dr.IsDBNull(dr.GetOrdinal("DATE_EXPIRATION")) ? "" : dr.GetDateTime(dr.GetOrdinal("DATE_EXPIRATION")).ToString("yyyy-MM-dd");
                        oNewsNess.DATE_PUBLICATION = dr.IsDBNull(dr.GetOrdinal("DATE_PUBLICATION")) ? "" : dr.GetDateTime(dr.GetOrdinal("DATE_PUBLICATION")).ToString("yyyy-MM-dd");
                        oNewsNess.EXPIRE = dr.IsDBNull(dr.GetOrdinal("EXPIRE")) ? false : dr.GetBoolean(dr.GetOrdinal("EXPIRE"));
                        oNewsNess.CREATED_AT = dr.IsDBNull(dr.GetOrdinal("CREATED_AT")) ? Convert.ToDateTime("1901-01-01") : dr.GetDateTime(dr.GetOrdinal("CREATED_AT"));
                        oNewsNess.UPDATED_AT = dr.IsDBNull(dr.GetOrdinal("UPDATED_AT")) ? Convert.ToDateTime("1901-01-01") : dr.GetDateTime(dr.GetOrdinal("UPDATED_AT"));


                        newsNesses.Add(oNewsNess);
                    }
                    dr.Close();
                }
            }
            return newsNesses;
        }

        public async Task<IEnumerable<NewsNessType>> GetNewsNessType()
        {
            //NewsNessType oNewsNessType = null;
            List<NewsNessType> NewsNessType = new List<NewsNessType>();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                //SqlCommand cmd = new SqlCommand("SP_NEWNESS_LIST", cn);
                SqlCommand cmd = new SqlCommand("SP_NEWNESS_TYPE", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        NewsNessType oNewsNessType = new NewsNessType();

                        oNewsNessType.ID = dr.IsDBNull(dr.GetOrdinal("ID")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID"));
                        oNewsNessType.NAME = dr.IsDBNull(dr.GetOrdinal("NAME")) ? "" : dr.GetString(dr.GetOrdinal("NAME"));

                        NewsNessType.Add(oNewsNessType);
                    }
                    dr.Close();
                }
            }
            return NewsNessType;
        }

        public async Task<int> SaveNewsness(NewsNess newsness)
        {
            int i = -1;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_NEWNESS_WEB_INSERT", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                //var userId = Convert.ToInt32(HttpContext.Current.Session["username"].ToString().Split('¬')[0].ToString());
                cmd.Parameters.AddWithValue("@ID_CATEGORY", newsness.ID_CATEGORY);
                cmd.Parameters.AddWithValue("@ID_TYPE_NEWNESS", newsness.ID_TYPE_NEWNESS);
                cmd.Parameters.AddWithValue("@URL", newsness.URL);
                cmd.Parameters.AddWithValue("@ISVIDEO", newsness.VIDEO);
                cmd.Parameters.AddWithValue("@TITLE", newsness.TITLE);
                cmd.Parameters.AddWithValue("@DESCRIPTION", newsness.DESCRIPTION);
                cmd.Parameters.AddWithValue("@IMAGE_FIRST", newsness.IMAGE_FIRST);
                cmd.Parameters.AddWithValue("@EXPIRE", newsness.EXPIRE);
                cmd.Parameters.AddWithValue("@DATE_EXPIRATION", newsness.DATE_EXPIRATION);
                cmd.Parameters.AddWithValue("@DATE_PUBLICATION", newsness.DATE_PUBLICATION); 
                cmd.Parameters.AddWithValue("@CREATED_USER", Util.MyUser.ID);
                i = (int)cmd.ExecuteScalar();
                return i;
            }
        }

        public async Task<int> UpdateNewsness(NewsNess newsness)
        {
            int i = -1;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_NEWNESS_WEB_UPDATE", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", newsness.ID);
                cmd.Parameters.AddWithValue("@ID_CATEGORY", newsness.ID_CATEGORY);
                cmd.Parameters.AddWithValue("@ID_TYPE_NEWNESS", newsness.ID_TYPE_NEWNESS);
                cmd.Parameters.AddWithValue("@URL", newsness.URL);
                cmd.Parameters.AddWithValue("@ISVIDEO", newsness.VIDEO);
                cmd.Parameters.AddWithValue("@TITLE", newsness.TITLE);
                cmd.Parameters.AddWithValue("@DESCRIPTION", newsness.DESCRIPTION);
                cmd.Parameters.AddWithValue("@IMAGE_FIRST", newsness.IMAGE_FIRST);
                cmd.Parameters.AddWithValue("@EXPIRE", newsness.EXPIRE);
                cmd.Parameters.AddWithValue("@DATE_EXPIRATION", newsness.DATE_EXPIRATION);
                cmd.Parameters.AddWithValue("@DATE_PUBLICATION", newsness.DATE_PUBLICATION);      

                cmd.Parameters.AddWithValue("@UPDATED_USER", Util.MyUser.ID);

                i = await cmd.ExecuteNonQueryAsync();
                return i;
            }
        }

        public async Task<int> DeleteNewsness(NewsNess newsness)
        {
            int i = -1;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_NEWNESS_WEB_DELETE", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", newsness.ID);        

                cmd.Parameters.AddWithValue("@UPDATED_USER", Util.MyUser.ID);

                i = await cmd.ExecuteNonQueryAsync();
                return i;
            }
        }
        public Task<int> SaveUpdate(NewsNess model)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
