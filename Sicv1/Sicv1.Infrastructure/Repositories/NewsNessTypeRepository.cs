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
    public class NewsNessTypeRepository : Connection, INewsNessTypeRepository
    {
        public async Task<IEnumerable<NewsNessType>> GetNewsNessesType()
        {
            NewsNessType oNewsNessType = null;
            List<NewsNessType> newsNessTypes = new List<NewsNessType>();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_TYPENEWSNESS_LIST", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        oNewsNessType = new NewsNessType();
                        oNewsNessType.ID = dr.IsDBNull(dr.GetOrdinal("ID")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID"));
                        oNewsNessType.NAME = dr.IsDBNull(dr.GetOrdinal("NAME")) ? "nodata" : dr.GetString(dr.GetOrdinal("NAME"));
                        newsNessTypes.Add(oNewsNessType);
                    }
                    dr.Close();
                }
            }
            return newsNessTypes;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
