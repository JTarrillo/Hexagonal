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
    public class BranchOfficesRepository : Connection, IBranchOfficesRepository
    {
        public async Task<IEnumerable<BranchOffices>> GetBranchOffices()
        {
            BranchOffices oBranchOffices = null;
            List<BranchOffices> branchOffices = new List<BranchOffices>();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_BRANCH_OFFICES_LIST", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        oBranchOffices = new BranchOffices();
                        oBranchOffices.ID = dr.IsDBNull(dr.GetOrdinal("ID")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID"));
                        oBranchOffices.ID_COMPANY = dr.IsDBNull(dr.GetOrdinal("ID_COMPANY")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID_COMPANY"));
                        oBranchOffices.NAME = dr.IsDBNull(dr.GetOrdinal("NAME")) ? "nodata" : dr.GetString(dr.GetOrdinal("NAME"));
                        oBranchOffices.PHONE = dr.IsDBNull(dr.GetOrdinal("PHONE")) ? "0" : dr.GetString(dr.GetOrdinal("PHONE"));
                        oBranchOffices.LATITUDE = dr.IsDBNull(dr.GetOrdinal("LATITUDE")) ? "0" : dr.GetString(dr.GetOrdinal("LATITUDE"));
                        oBranchOffices.LONGITUDE = dr.IsDBNull(dr.GetOrdinal("LONGITUDE")) ? "0" : dr.GetString(dr.GetOrdinal("LONGITUDE"));
                        oBranchOffices.DIRECTION = dr.IsDBNull(dr.GetOrdinal("DIRECTION")) ? "0" : dr.GetString(dr.GetOrdinal("DIRECTION"));
                        oBranchOffices.COUNTRY = dr.IsDBNull(dr.GetOrdinal("COUNTRY")) ? "0" : dr.GetString(dr.GetOrdinal("COUNTRY"));
                        //oBranchOffices.STATUS = dr.IsDBNull(dr.GetOrdinal("STATUS")) ? false : dr.GetBoolean(dr.GetOrdinal("STATUS"));
                        branchOffices.Add(oBranchOffices);
                    }
                    dr.Close();
                }
            }
            return branchOffices;
        }

        public async Task<int> SaveBranchOffices(BranchOffices branchOffices)
        {
            int i = 0;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_BRANCH_OFFICES_SAVE", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                //var userId = Convert.ToInt32(HttpContext.Current.Session["username"].ToString().Split('¬')[0].ToString());
                cmd.Parameters.AddWithValue("@ID_COMPANY", branchOffices.ID_COMPANY);
                cmd.Parameters.AddWithValue("@NAME", branchOffices.NAME);
                //cmd.Parameters.AddWithValue("@DESCRIPTION", branchOffices.DESCRIPTION);
                cmd.Parameters.AddWithValue("@PHONE", branchOffices.PHONE);
                cmd.Parameters.AddWithValue("@LATITUDE", branchOffices.LATITUDE);
                cmd.Parameters.AddWithValue("@LONGITUDE", branchOffices.LONGITUDE);
                cmd.Parameters.AddWithValue("@DIRECTION", branchOffices.DIRECTION);
                cmd.Parameters.AddWithValue("@COUNTRY", branchOffices.COUNTRY);
                cmd.Parameters.AddWithValue("@CREATED_USER", Util.MyUser.ID);

                i = (int) cmd.ExecuteScalar();

                return i;
            }
        }

        public async Task<int> UpdateBranchOffices(BranchOffices branchOffices)
        {
            int i = -1;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_BRANCH_OFFICES_UPDATE", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                //var userId = Convert.ToInt32(HttpContext.Current.Session["username"].ToString().Split('¬')[0].ToString());
                cmd.Parameters.AddWithValue("@ID", branchOffices.ID);
                cmd.Parameters.AddWithValue("@NAME", branchOffices.NAME);
                cmd.Parameters.AddWithValue("@PHONE", branchOffices.PHONE);
                cmd.Parameters.AddWithValue("@LATITUDE", branchOffices.LATITUDE);
                cmd.Parameters.AddWithValue("@LONGITUDE", branchOffices.LONGITUDE);
                cmd.Parameters.AddWithValue("@DIRECTION", branchOffices.DIRECTION);
                cmd.Parameters.AddWithValue("@COUNTRY", branchOffices.COUNTRY);
                cmd.Parameters.AddWithValue("@UPDATED_USER", Util.MyUser.ID);

                i = await cmd.ExecuteNonQueryAsync();
                return i;
            }
        }

        public async Task<int> DeleteBranchOffices(BranchOffices branchOffices)
        {
            int i = -1;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_BRANCH_OFFICES_DELETE", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                //var userId = Convert.ToInt32(HttpContext.Current.Session["username"].ToString().Split('¬')[0].ToString());
                cmd.Parameters.AddWithValue("@ID", branchOffices.ID);   
                cmd.Parameters.AddWithValue("@DELETED_USER", Util.MyUser.ID);

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
