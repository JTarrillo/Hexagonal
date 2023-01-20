using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Entities;
using Sicv1.Infrastructure.Cross;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Sicv1.Infrastructure.Repositories
{
    public class CategoryCodeQrRepository : Connection, ICategoryCodeQrRepository
    {
        public async Task<int> Save(CategoryCodeQr categoryCodeQr)
        {
            int i = -1;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_CATEGORIES_CODE_QR_INSERT", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_CATEGORY", categoryCodeQr.ID_CATEGORY);
                cmd.Parameters.AddWithValue("@ID_USER", categoryCodeQr.ID_USER);
                cmd.Parameters.AddWithValue("@CODE_QR", categoryCodeQr.CODE_QR);
                cmd.Parameters.AddWithValue("@VIA", categoryCodeQr.VIA);
                cmd.Parameters.AddWithValue("@CREATED_USER", categoryCodeQr.CREATED_USER);
                i = await cmd.ExecuteNonQueryAsync();
                return i;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<int> GetCount()
        {
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                await cn.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_CATEGORIES_CODE_QR_COUNT", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                object obj = Convert.ToInt32(cmd.ExecuteScalar());
                int respuesta;
                return respuesta = (Convert.ToInt32(obj) > 0) ? Convert.ToInt32(obj) : 0;
            }
        }

        /// <summary>
        /// use SP_CATEGORIES_HISTORICAL_DETAIL
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CategoryCodeQr>> GetCategoriesCodeQrDetail(int ID_COMPANY)
        {
            CategoryCodeQr oCategoryCodeQr = null;
            List<CategoryCodeQr> lstCategoryCodeQr = new List<CategoryCodeQr>();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_CATEGORIES_COUPONS_HIERARCHY", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_COMPANY", ID_COMPANY);
                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        oCategoryCodeQr = new CategoryCodeQr();
                        oCategoryCodeQr.COUPON_ID = dr.IsDBNull(dr.GetOrdinal("COUPON_ID")) ? -1 : dr.GetInt32(dr.GetOrdinal("COUPON_ID"));
                        oCategoryCodeQr.CODE_QR = dr.IsDBNull(dr.GetOrdinal("CODE_QR")) ? "nodata" : dr.GetString(dr.GetOrdinal("CODE_QR"));
                        //oCategoryCodeQr.VIA = dr.IsDBNull(dr.GetOrdinal("VIA")) ? "nodata" : dr.GetString(dr.GetOrdinal("VIA"));
                        oCategoryCodeQr.REDEMPTION_DATE = dr.IsDBNull(dr.GetOrdinal("REDEMPTION_DATE")) ? Convert.ToDateTime("1901-01-01") : dr.GetDateTime(dr.GetOrdinal("REDEMPTION_DATE"));
                        oCategoryCodeQr.TITLE = dr.IsDBNull(dr.GetOrdinal("TITLE")) ? "nodata" : dr.GetString(dr.GetOrdinal("TITLE"));
                        //oCategoryCodeQr.DESCRIPTION = dr.IsDBNull(dr.GetOrdinal("DESCRIPTION")) ? "nodata" : dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                        oCategoryCodeQr.ALLIANCE_NAME = dr.IsDBNull(dr.GetOrdinal("ALLIANCE_NAME")) ? "nodata" : dr.GetString(dr.GetOrdinal("ALLIANCE_NAME"));

                        oCategoryCodeQr.EXCHANGE_USER = dr.IsDBNull(dr.GetOrdinal("EXCHANGE_USER")) ? "nodata" : dr.GetString(dr.GetOrdinal("EXCHANGE_USER"));
                        oCategoryCodeQr.ALLIANCE_USER = dr.IsDBNull(dr.GetOrdinal("ALLIANCE_USER")) ? "nodata" : dr.GetString(dr.GetOrdinal("ALLIANCE_USER"));

                        oCategoryCodeQr.LEVEL_3 = dr.IsDBNull(dr.GetOrdinal("LEVEL_3")) ? "nodata" : dr.GetString(dr.GetOrdinal("LEVEL_3"));
                        oCategoryCodeQr.LEVEL_2 = dr.IsDBNull(dr.GetOrdinal("LEVEL_2")) ? "nodata" : dr.GetString(dr.GetOrdinal("LEVEL_2"));
                        oCategoryCodeQr.LEVEL_1 = dr.IsDBNull(dr.GetOrdinal("LEVEL_1")) ? "nodata" : dr.GetString(dr.GetOrdinal("LEVEL_1"));
                        oCategoryCodeQr.NUMBER_OF_TIMES_REDEEMED = dr.IsDBNull(dr.GetOrdinal("NUMBER_OF_TIMES_REDEEMED")) ? -1 : dr.GetInt32(dr.GetOrdinal("NUMBER_OF_TIMES_REDEEMED"));

                        lstCategoryCodeQr.Add(oCategoryCodeQr);
                    }
                    dr.Close();
                }
            }
            return lstCategoryCodeQr;
        }

        public async Task<IEnumerable<CategoryCodeQr>> GetChart(DateTime? fi, DateTime? ff)
        {
            CategoryCodeQr oCategoryCodeQr = null;
            List<CategoryCodeQr> lst = new List<CategoryCodeQr>();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_CATEGORIES_CODE_QR_CHART_COUNT_COUPONS_BY_DATE", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@START_DATE", fi);
                cmd.Parameters.AddWithValue("@END_DATE", ff);

                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        oCategoryCodeQr = new CategoryCodeQr();
                        oCategoryCodeQr.num_cupones = dr.IsDBNull(dr.GetOrdinal("num_cupones")) ? -1 : dr.GetInt32(dr.GetOrdinal("num_cupones"));
                        oCategoryCodeQr.fecha = dr.IsDBNull(dr.GetOrdinal("fecha")) ? "nodata" : dr.GetString(dr.GetOrdinal("fecha"));
                        lst.Add(oCategoryCodeQr);
                    }
                    dr.Close();
                }
            }
            return lst;
        }

        public async Task<IEnumerable<CategoryCodeQr>> GetCouponsHierarchyDetail(int CouponId)
        {
            CategoryCodeQr oCategoryCodeQr = null;
            List<CategoryCodeQr> lista = new List<CategoryCodeQr>();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_CATEGORIES_COUPONS_HIERARCHY_DETAIL", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@COUPON_ID", CouponId);
                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        oCategoryCodeQr = new CategoryCodeQr();
                        oCategoryCodeQr.EXCHANGE_USER = dr.IsDBNull(dr.GetOrdinal("EXCHANGE_USER")) ? "nodata" : dr.GetString(dr.GetOrdinal("EXCHANGE_USER"));
                        oCategoryCodeQr.ALLIANCE_USER = dr.IsDBNull(dr.GetOrdinal("ALLIANCE_USER")) ? "nodata" : dr.GetString(dr.GetOrdinal("ALLIANCE_USER"));
                        oCategoryCodeQr.CREATED_AT = dr.IsDBNull(dr.GetOrdinal("CREATED_AT")) ? Convert.ToDateTime("1901-01-01") : dr.GetDateTime(dr.GetOrdinal("CREATED_AT"));
                        lista.Add(oCategoryCodeQr);
                    }
                    dr.Close();
                }
            }
            return lista;
        }


        /// <summary>
        /// SP_CATEGORIES_CODE_QR_UPDATE
        /// </summary>
        /// <param name="categoryCodeQr"></param>
        /// <returns></returns>
        public async Task<int> Update(CategoryCodeQr categoryCodeQr)
        {
            int i = -1;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_CATEGORIES_CODE_QR_UPDATE", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@APPROVED_ID_USER", categoryCodeQr.APPROVED_ID_USER);
                cmd.Parameters.AddWithValue("@CODE_QR", categoryCodeQr.CODE_QR);
                cmd.Parameters.AddWithValue("@TYPE", categoryCodeQr.TYPE);
                cmd.Parameters.AddWithValue("@ID", categoryCodeQr.ID);
                i = await cmd.ExecuteNonQueryAsync();
                return i;
            }
        }
    }
}
