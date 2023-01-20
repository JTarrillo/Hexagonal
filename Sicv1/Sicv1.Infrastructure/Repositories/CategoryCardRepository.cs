using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Entities;
using Sicv1.Infrastructure.Cross;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Sicv1.Infrastructure.Repositories
{
    public class CategoryCardRepository : Connection, ICategoryCardRepository
    {
        public async Task<int> Save(CategoryCard categoryCard)
        {
            int i = -1;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_CATEGORIES_CARDS_INSERT", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_CATEGORY", categoryCard.ID_CATEGORY);
                cmd.Parameters.AddWithValue("@HAVE_CARD", categoryCard.HAVE_CARD);
                cmd.Parameters.AddWithValue("@HAVE_ACCESS", categoryCard.HAVE_ACCESS);
                cmd.Parameters.AddWithValue("@CREATE_USER", categoryCard.CREATED_USER);
                i = await cmd.ExecuteNonQueryAsync();
                return i;
            }
        }

        public async Task<int> Update(CategoryCard categoryCard)
        {
            int i = -1;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_CATEGORIES_CARDS_UPDATE", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", categoryCard.ID);
                cmd.Parameters.AddWithValue("@ID_CATEGORY", categoryCard.ID_CATEGORY);
                cmd.Parameters.AddWithValue("@HAVE_ACCESS", categoryCard.HAVE_ACCESS);
                cmd.Parameters.AddWithValue("@BARCODE", categoryCard.BARCODE);
                cmd.Parameters.AddWithValue("@BARCODE_FORMAT", categoryCard.BARCODE_FORMAT);
                cmd.Parameters.AddWithValue("@UPDATE_USER", categoryCard.UPDATED_USER);
                i = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                return i;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
