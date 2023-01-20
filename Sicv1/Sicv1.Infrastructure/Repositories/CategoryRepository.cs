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
    public class CategoryRepository : Connection, ICategoryRepository
    {
        public async Task<IEnumerable<Category>> GetCategoriesByCompanyId(int Id, string Title = "", string Description = "")
        {
            Category oCategory = null;
            List<Category> categories = new List<Category>();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_CATEGORIES_LIST_BY_IDCOMPANY", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_COMPANY", Id);
                cmd.Parameters.AddWithValue("@TITLE", Title);
                cmd.Parameters.AddWithValue("@DESCRIPTION", Description);
                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        oCategory = new Category();
                        oCategory.ID = dr.IsDBNull(dr.GetOrdinal("ID")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID"));
                        oCategory.BARCODE = dr.IsDBNull(dr.GetOrdinal("BARCODE")) ? "" : dr.GetString(dr.GetOrdinal("BARCODE"));
                        oCategory.BARCODE_FORMAT= dr.IsDBNull(dr.GetOrdinal("BARCODE_FORMAT")) ? "" : dr.GetString(dr.GetOrdinal("BARCODE_FORMAT"));
                        oCategory.TYPE_CODE = dr.IsDBNull(dr.GetOrdinal("TYPE_CODE")) ? "" : dr.GetString(dr.GetOrdinal("TYPE_CODE"));
                        oCategory.ID_COMPANY = dr.IsDBNull(dr.GetOrdinal("ID_COMPANY")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID_COMPANY"));
                        oCategory.ID_COMPANY = dr.IsDBNull(dr.GetOrdinal("ID_COMPANY")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID_COMPANY"));
                        oCategory.NAME_COMPANY = dr.IsDBNull(dr.GetOrdinal("NAME_COMPANY")) ? "nodata" : dr.GetString(dr.GetOrdinal("NAME_COMPANY"));
                        oCategory.LOGO = dr.IsDBNull(dr.GetOrdinal("LOGO")) ? "nodata" : dr.GetString(dr.GetOrdinal("LOGO"));
                        oCategory.ID_PARENT = dr.IsDBNull(dr.GetOrdinal("ID_PARENT")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID_PARENT"));
                        oCategory.ID_PARENT2 = dr.IsDBNull(dr.GetOrdinal("ID_PARENT2")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID_PARENT2"));
                        oCategory.ID_PARENT3 = dr.IsDBNull(dr.GetOrdinal("ID_PARENT3")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID_PARENT3"));
                        oCategory.TITLE = dr.IsDBNull(dr.GetOrdinal("TITLE")) ? "nodata" : dr.GetString(dr.GetOrdinal("TITLE"));
                        oCategory.SUBTITLE = dr.IsDBNull(dr.GetOrdinal("SUBTITLE")) ? "nodata" : dr.GetString(dr.GetOrdinal("SUBTITLE"));
                        oCategory.DESCRIPTION = dr.IsDBNull(dr.GetOrdinal("DESCRIPTION")) ? "nodata" : dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                        oCategory.CONDITIONS = dr.IsDBNull(dr.GetOrdinal("CONDITIONS")) ? "nodata" : dr.GetString(dr.GetOrdinal("CONDITIONS"));
                        oCategory.PRICE = dr.IsDBNull(dr.GetOrdinal("PRICE")) ? Convert.ToDecimal(0) : dr.GetDecimal(dr.GetOrdinal("PRICE"));
                        oCategory.COLOR = dr.IsDBNull(dr.GetOrdinal("COLOR")) ? "nodata" : dr.GetString(dr.GetOrdinal("COLOR"));
                        oCategory.ICON = dr.IsDBNull(dr.GetOrdinal("ICON")) ? "nodata" : dr.GetString(dr.GetOrdinal("ICON"));
                        oCategory.ALIAS = dr.IsDBNull(dr.GetOrdinal("ALIAS")) ? "nodata" : dr.GetString(dr.GetOrdinal("ALIAS"));
                        oCategory.IMAGE_FIRST = dr.IsDBNull(dr.GetOrdinal("IMAGE_FIRST")) ? "nodata" : dr.GetString(dr.GetOrdinal("IMAGE_FIRST"));
                        oCategory.IMAGE_SECOND = dr.IsDBNull(dr.GetOrdinal("IMAGE_SECOND")) ? "nodata" : dr.GetString(dr.GetOrdinal("IMAGE_SECOND"));
                        oCategory.SCORE = dr.IsDBNull(dr.GetOrdinal("SCORE")) ? Convert.ToDecimal(0) : dr.GetDecimal(dr.GetOrdinal("SCORE"));
                        oCategory.TOTAL_RATING = dr.IsDBNull(dr.GetOrdinal("TOTAL_RATING")) ? -1 : dr.GetInt32(dr.GetOrdinal("TOTAL_RATING"));
                        oCategory.STATUS = dr.IsDBNull(dr.GetOrdinal("STATUS")) ? false : dr.GetBoolean(dr.GetOrdinal("STATUS"));
                        oCategory.PERCENTAGE = dr.IsDBNull(dr.GetOrdinal("PERCENTAGE")) ? "nodata" : dr.GetString(dr.GetOrdinal("PERCENTAGE"));
                        oCategory.START_DATE = dr.IsDBNull(dr.GetOrdinal("START_DATE")) ? Convert.ToDateTime("1901-01-01") : dr.GetDateTime(dr.GetOrdinal("START_DATE")).AddHours(5);
                        oCategory.END_DATE = dr.IsDBNull(dr.GetOrdinal("END_DATE")) ? Convert.ToDateTime("1901-01-01") : dr.GetDateTime(dr.GetOrdinal("END_DATE")).AddHours(5);
                        oCategory.TYPE = dr.IsDBNull(dr.GetOrdinal("TYPE")) ? "nodata" : dr.GetString(dr.GetOrdinal("TYPE"));
                        oCategory.HAVE_ACCESS_SI = dr.IsDBNull(dr.GetOrdinal("HAVE_ACCESS_SI")) ? false : dr.GetBoolean(dr.GetOrdinal("HAVE_ACCESS_SI"));
                        oCategory.HAVE_ACCESS_NO = dr.IsDBNull(dr.GetOrdinal("HAVE_ACCESS_NO")) ? false : dr.GetBoolean(dr.GetOrdinal("HAVE_ACCESS_NO"));
                        oCategory.ID_HAVE_ACCESS_SI = dr.IsDBNull(dr.GetOrdinal("ID_HAVE_ACCESS_SI")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID_HAVE_ACCESS_SI"));
                        oCategory.ID_HAVE_ACCESS_NO = dr.IsDBNull(dr.GetOrdinal("ID_HAVE_ACCESS_NO")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID_HAVE_ACCESS_NO"));
                        oCategory.CAT_URL = dr.IsDBNull(dr.GetOrdinal("CAT_URL")) ? "" : dr.GetString(dr.GetOrdinal("CAT_URL"));
                        oCategory.URL_LINK = dr.IsDBNull(dr.GetOrdinal("URL")) ? "" : dr.GetString(dr.GetOrdinal("URL"));

                        oCategory.LIFEMILES_PARTICIPATES_CAMPAIGN = dr.IsDBNull(dr.GetOrdinal("LIFEMILES_PARTICIPATES_CAMPAIGN")) ? -1 : dr.GetInt32(dr.GetOrdinal("LIFEMILES_PARTICIPATES_CAMPAIGN"));
                        oCategory.NUMBER_MILES = dr.IsDBNull(dr.GetOrdinal("NUMBER_MILES")) ? -1 : dr.GetInt32(dr.GetOrdinal("NUMBER_MILES"));
                        
                        oCategory.COMPANY_HAVE_LIFEMILES = dr.IsDBNull(dr.GetOrdinal("COMPANY_HAVE_LIFEMILES")) ? -1 : dr.GetInt32(dr.GetOrdinal("COMPANY_HAVE_LIFEMILES"));
                        oCategory.SEGMENT = dr.IsDBNull(dr.GetOrdinal("SEGMENT")) ? "[]" : dr.GetString(dr.GetOrdinal("SEGMENT"));
                        categories.Add(oCategory);
                    }
                    dr.Close();
                }
            }
            return categories;
        }

        public async Task<IEnumerable<Category>> GetCategoriesCodeQr(int Id)
        {
            Category oCategory = null;
            List<Category> categories = new List<Category>();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_CATEGORIES_CODE_QR_LIST", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_USER", Id);
                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        oCategory = new Category();
                        oCategory.NAME = dr.IsDBNull(dr.GetOrdinal("NAME")) ? "nodata" : dr.GetString(dr.GetOrdinal("NAME"));
                        oCategory.LASTNAME = dr.IsDBNull(dr.GetOrdinal("LASTNAME")) ? "nodata" : dr.GetString(dr.GetOrdinal("LASTNAME"));
                        oCategory.TITLE = dr.IsDBNull(dr.GetOrdinal("TITLE")) ? "nodata" : dr.GetString(dr.GetOrdinal("TITLE"));
                        oCategory.DESCRIPTION = dr.IsDBNull(dr.GetOrdinal("DESCRIPTION")) ? "nodata" : dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                        oCategory.IMAGE_FIRST = dr.IsDBNull(dr.GetOrdinal("IMAGE_FIRST")) ? "nodata" : dr.GetString(dr.GetOrdinal("IMAGE_FIRST"));
                        oCategory.CODE_QR = dr.IsDBNull(dr.GetOrdinal("CODE_QR")) ? "nodata" : dr.GetString(dr.GetOrdinal("CODE_QR"));
                        oCategory.PRICE = dr.IsDBNull(dr.GetOrdinal("PRICE")) ? Convert.ToDecimal(0) : dr.GetDecimal(dr.GetOrdinal("PRICE"));
                        oCategory.PERCENTAGE = dr.IsDBNull(dr.GetOrdinal("PERCENTAGE")) ? "nodata" : dr.GetString(dr.GetOrdinal("PERCENTAGE"));
                        oCategory.DATE_USE = dr.IsDBNull(dr.GetOrdinal("DATE_USE")) ? Convert.ToDateTime("1901-01-01") : dr.GetDateTime(dr.GetOrdinal("DATE_USE"));
                        categories.Add(oCategory);
                    }
                    dr.Close();
                }
            }
            return categories;
        }

        public async Task<int> UpdateCategoriesById(Category category)
        {
            int i = -1;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_CATEGORIES_UPDATE_BY_ID", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", category.ID);
                cmd.Parameters.AddWithValue("@BARCODE", category.BARCODE);
                cmd.Parameters.AddWithValue("@BARCODE_FORMAT", category.BARCODE_FORMAT);
                cmd.Parameters.AddWithValue("@TYPE_CODE", category.TYPE_CODE);
                cmd.Parameters.AddWithValue("@TITLE", category.TITLE);
                cmd.Parameters.AddWithValue("@DESCRIPTION", category.DESCRIPTION);
                cmd.Parameters.AddWithValue("@CONDITIONS", category.CONDITIONS);
                cmd.Parameters.AddWithValue("@IMAGE_FIRST", category.IMAGE_FIRST);
                cmd.Parameters.AddWithValue("@PRICE", category.PRICE);
                cmd.Parameters.AddWithValue("@PERCENTAGE", category.PERCENTAGE);
                cmd.Parameters.AddWithValue("@UPDATED_USER", category.UPDATED_USER);
                cmd.Parameters.AddWithValue("@START_DATE", category.START_DATE);
                cmd.Parameters.AddWithValue("@END_DATE", category.END_DATE);
                cmd.Parameters.AddWithValue("@ID_PARENT", category.ID_PARENT);
                cmd.Parameters.AddWithValue("@ID_COMPANY", category.ID_COMPANY);
                cmd.Parameters.AddWithValue("@TYPE", category.TYPE);
                cmd.Parameters.AddWithValue("@URL_LINK", category.URL_LINK);
                cmd.Parameters.AddWithValue("@LIFEMILES_PARTICIPATES_CAMPAIGN", category.LIFEMILES_PARTICIPATES_CAMPAIGN);
                cmd.Parameters.AddWithValue("@NUMBER_MILES", category.NUMBER_MILES);
                cmd.Parameters.AddWithValue("@SEGMENT", category.SEGMENT);
                cmd.Parameters.AddWithValue("@SHOW_IMAGE", category.SHOW_IMAGE);
                cmd.Parameters.AddWithValue("@CAT_URL", category.CAT_URL);
                i = await cmd.ExecuteNonQueryAsync();
                return i;
            }
        }

        public async Task<int> UpdateCategoriesStatusById(int Id, string Status, int user_id)
        {
            int i = -1;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_CATEGORIES_UPDATE_STATUS_BY_ID", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CATEGORY_ID", Id);
                cmd.Parameters.AddWithValue("@STATUS", Status);
                cmd.Parameters.AddWithValue("@USER_ID", user_id);
                i = await cmd.ExecuteNonQueryAsync();
                return i;
            }
        }

        public async Task<int> DeleteCategoriesStatusById(int Id, int user_id)
        {
            int i = -1;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_CATEGORIES_DELETE", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CATEGORY_ID", Id);
                cmd.Parameters.AddWithValue("@USER_ID", user_id);
                i = await cmd.ExecuteNonQueryAsync();
                return i;
            }
        }

        public async Task<int> SaveCategory(Category category)
        {
            int i = -1;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_CATEGORIES_INSERT", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                //var userId = Convert.ToInt32(HttpContext.Current.Session["username"].ToString().Split('¬')[0].ToString());
                cmd.Parameters.AddWithValue("@IDCOMPANY", category.ID_COMPANY);

                cmd.Parameters.AddWithValue("@BARCODE", category.BARCODE);
                cmd.Parameters.AddWithValue("@BARCODE_FORMAT", category.BARCODE_FORMAT);
                cmd.Parameters.AddWithValue("@TYPE_CODE", category.TYPE_CODE);

                cmd.Parameters.AddWithValue("@ID_PARENT", category.ID_PARENT);
                cmd.Parameters.AddWithValue("@TITLE", category.TITLE);
                cmd.Parameters.AddWithValue("@DESCRIPTION", category.DESCRIPTION);
                cmd.Parameters.AddWithValue("@CONDITIONS", category.CONDITIONS);
                cmd.Parameters.AddWithValue("@PRICE", category.PRICE);
                cmd.Parameters.AddWithValue("@PERCENTAGE", category.PERCENTAGE);
                cmd.Parameters.AddWithValue("@IMAGE_FIRST", category.IMAGE_FIRST);
                category.CREATED_USER = Util.MyUser.ID;
                cmd.Parameters.AddWithValue("@CREATED_USER", category.CREATED_USER);
                cmd.Parameters.AddWithValue("@START_DATE", category.START_DATE);
                cmd.Parameters.AddWithValue("@END_DATE", category.END_DATE);
                cmd.Parameters.AddWithValue("@TYPE", category.TYPE);
                cmd.Parameters.AddWithValue("@URL_LINK", category.URL_LINK);
                cmd.Parameters.AddWithValue("@SEGMENT", category.SEGMENT);
                cmd.Parameters.AddWithValue("@SHOW_IMAGE", category.SHOW_IMAGE);
                cmd.Parameters.AddWithValue("@CAT_URL", category.CAT_URL);//guarda un array con las web y redes sociales

                cmd.Parameters.AddWithValue("@LIFEMILES_PARTICIPATES_CAMPAIGN", category.LIFEMILES_PARTICIPATES_CAMPAIGN);
                cmd.Parameters.AddWithValue("@NUMBER_MILES", category.NUMBER_MILES);

                i = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                return i;
            }
        }

        #region Combos parents / childs
        public async Task<IEnumerable<Category>> GetCategoriesParent()
        {
            Category oCategory = null;
            List<Category> categories = new List<Category>();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_CATEGORIES_LIST_PARENTS", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        oCategory = new Category();
                        oCategory.ID = dr.IsDBNull(dr.GetOrdinal("ID")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID"));
                        oCategory.TITLE = dr.IsDBNull(dr.GetOrdinal("TITLE")) ? "nodata" : dr.GetString(dr.GetOrdinal("TITLE"));
                        oCategory.POSITION = dr.IsDBNull(dr.GetOrdinal("POSITION")) ? -1 : dr.GetInt32(dr.GetOrdinal("POSITION"));
                        categories.Add(oCategory);
                    }
                    dr.Close();
                }
                return categories;
            }
        }

        public async Task<IEnumerable<Category>> GetCategoriesChildByParentId(int ParentId)
        {
            Category oCategory = null;
            List<Category> categories = new List<Category>();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_CATEGORIES_LIST_CHILD_BY_PARENT_ID", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_PARENT", ParentId);
                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        oCategory = new Category();
                        oCategory.ID = dr.IsDBNull(dr.GetOrdinal("ID")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID"));
                        oCategory.ID_PARENT = dr.IsDBNull(dr.GetOrdinal("ID_PARENT")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID_PARENT"));
                        oCategory.TITLE = dr.IsDBNull(dr.GetOrdinal("TITLE")) ? "nodata" : dr.GetString(dr.GetOrdinal("TITLE"));
                        categories.Add(oCategory);
                    }
                    dr.Close();
                }
                return categories;
            }
        }
        #endregion

        public Category Validate(string CodeQrToValidate, int CompanyId)
        {
            Category oCategory = null;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_CATEGORIES_CODE_QR_VALIDATE_BEFORE_UPDATE", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CODE_QR", CodeQrToValidate);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    dr.Read();
                    oCategory = new Category();
                    oCategory.IMAGE_FIRST = dr.IsDBNull(dr.GetOrdinal("IMAGE_FIRST")) ? "nodata" : dr.GetString(dr.GetOrdinal("IMAGE_FIRST"));
                    oCategory.ID_COMPANY = dr.IsDBNull(dr.GetOrdinal("ID_COMPANY")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID_COMPANY"));
                    oCategory.TITLE = dr.IsDBNull(dr.GetOrdinal("TITLE")) ? "nodata" : dr.GetString(dr.GetOrdinal("TITLE"));
                    oCategory.SCORE = dr.IsDBNull(dr.GetOrdinal("SCORE")) ? Convert.ToDecimal(0) : dr.GetDecimal(dr.GetOrdinal("SCORE"));
                    oCategory.DESCRIPTION = dr.IsDBNull(dr.GetOrdinal("DESCRIPTION")) ? "nodata" : dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                    oCategory.CONDITIONS = dr.IsDBNull(dr.GetOrdinal("CONDITIONS")) ? "nodata" : dr.GetString(dr.GetOrdinal("CONDITIONS"));
                    oCategory.PRICE = dr.IsDBNull(dr.GetOrdinal("PRICE")) ? Convert.ToDecimal(0) : dr.GetDecimal(dr.GetOrdinal("PRICE"));
                    oCategory.PERCENTAGE = dr.IsDBNull(dr.GetOrdinal("PERCENTAGE")) ? "nodata" : dr.GetString(dr.GetOrdinal("PERCENTAGE"));
                    oCategory.CODE_QR = dr.IsDBNull(dr.GetOrdinal("CODE_QR")) ? "nodata" : dr.GetString(dr.GetOrdinal("CODE_QR"));
                    oCategory.USED = dr.IsDBNull(dr.GetOrdinal("USED")) ? false : dr.GetBoolean(dr.GetOrdinal("USED"));
                    oCategory.APPROVED_ID_USER = dr.IsDBNull(dr.GetOrdinal("APPROVED_ID_USER")) ? -1 : dr.GetInt32(dr.GetOrdinal("APPROVED_ID_USER"));
                    oCategory.ID = dr.IsDBNull(dr.GetOrdinal("ID")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID"));
                }
                dr.Close();
            }
            return oCategory;
        }

        public List<string> searchByDocument(string document, int id_company)
        {
            List<string> lstCadenas = new List<string>() ;
            SqlDataAdapter _adap = null;
            DataSet dtTables = null;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_USERS_SEARCH_BY_DOCUMENT", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DOCUMENT", document);
                cmd.Parameters.AddWithValue("@ID_COMPANY", id_company);
                _adap = new SqlDataAdapter(cmd);
                dtTables = new DataSet();
                _adap.Fill(dtTables);
                string jsonPerson = "";
                string jsonCategory = "";
                foreach (DataRow row in dtTables.Tables[0].Rows) { jsonPerson += row[0].ToString(); }

                foreach (DataRow row2 in dtTables.Tables[1].Rows) { jsonCategory += row2[0].ToString(); }

                lstCadenas.Add(jsonPerson);
                lstCadenas.Add(jsonCategory);
            }
            return lstCadenas;
        }


        //public IEnumerable<Category> searchCouponByDocument(string document, int idCompany)
        //{
        //    Category oCategory = null;
        //    using (var cn = new SqlConnection(SqlConnectionString))
        //    {
        //        cn.Open();
        //        SqlCommand cmd = new SqlCommand("SP_CATEGORIES_SEARCH_BY_DOCUMENT_USER", cn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@DOCUMENT", document);
        //        cmd.Parameters.AddWithValue("@ID_COMPANY", idCompany);
        //        SqlCommand _cmd_ = new SqlCommand();
        //        DataSet dtTables = new DataSet();
        //        SqlDataAdapter _adap = new SqlDataAdapter(_cmd_);
        //        _adap.Fill(dtTables);

        //        dr.Close();
        //    }
        //    return oCategory;
        //}

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<int> GetCount()
        {
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                await cn.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_CATEGORIES_COUNT", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                object obj = Convert.ToInt32(cmd.ExecuteScalar());
                int respuesta;
                return respuesta = (Convert.ToInt32(obj) > 0) ? Convert.ToInt32(obj) : 0;
            }
        }

        public async Task<IEnumerable<Category>> GetCategoriesCodeQrByCompanyId(int Id)
        {
            List<Category> categories = new List<Category>();
            Category oCategory = null;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                await cn.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_CATEGORIES_CODE_QR_LIST_BY_COMPANY_ID", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@COMPANY_ID", Id);
                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        oCategory = new Category();
                        oCategory.NAME = dr.IsDBNull(dr.GetOrdinal("NAME")) ? "nodata" : dr.GetString(dr.GetOrdinal("NAME"));
                        oCategory.LASTNAME = dr.IsDBNull(dr.GetOrdinal("LASTNAME")) ? "nodata" : dr.GetString(dr.GetOrdinal("LASTNAME"));
                        oCategory.DOCUMENT = dr.IsDBNull(dr.GetOrdinal("DOCUMENT")) ? "nodata" : dr.GetString(dr.GetOrdinal("DOCUMENT"));
                        oCategory.TITLE = dr.IsDBNull(dr.GetOrdinal("TITLE")) ? "nodata" : dr.GetString(dr.GetOrdinal("TITLE"));
                        oCategory.IMAGE_FIRST = dr.IsDBNull(dr.GetOrdinal("IMAGE_FIRST")) ? "nodata" : dr.GetString(dr.GetOrdinal("IMAGE_FIRST"));
                        oCategory.CODE_QR = dr.IsDBNull(dr.GetOrdinal("CODE_QR")) ? "nodata" : dr.GetString(dr.GetOrdinal("CODE_QR"));
                        oCategory.PRICE = dr.IsDBNull(dr.GetOrdinal("PRICE")) ? Convert.ToDecimal(0) : dr.GetDecimal(dr.GetOrdinal("PRICE"));
                        oCategory.PERCENTAGE = dr.IsDBNull(dr.GetOrdinal("PERCENTAGE")) ? "nodata" : dr.GetString(dr.GetOrdinal("PERCENTAGE"));
                        oCategory.DATE_USE = dr.IsDBNull(dr.GetOrdinal("DATE_USE")) ? Convert.ToDateTime("1901-01-01") : dr.GetDateTime(dr.GetOrdinal("DATE_USE"));
                        oCategory.FK_ROLE = dr.IsDBNull(dr.GetOrdinal("FK_ROLE")) ? -1 : dr.GetInt32(dr.GetOrdinal("FK_ROLE"));
                        oCategory.ALLIANCE = dr.IsDBNull(dr.GetOrdinal("ALLIANCE")) ? "nodata" : dr.GetString(dr.GetOrdinal("ALLIANCE"));

                        oCategory.CODE_LIFEMILES = dr.IsDBNull(dr.GetOrdinal("CODE_LIFEMILES")) ? "" : dr.GetString(dr.GetOrdinal("CODE_LIFEMILES"));
                        oCategory.QUANTITY = dr.IsDBNull(dr.GetOrdinal("QUANTITY")) ? -1 : dr.GetInt32(dr.GetOrdinal("QUANTITY"));
                        categories.Add(oCategory);
                    }
                    dr.Close();
                }
            }
            return categories;
        }
    }
}