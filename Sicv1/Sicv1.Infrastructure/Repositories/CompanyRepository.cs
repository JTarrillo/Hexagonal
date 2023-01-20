using Newtonsoft.Json;
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
    public class CompanyRepository : Connection, ICompanyRepository
    {
        public async Task<IEnumerable<Company>> GetCompaniesByUserId(int Id)
        {
            Company oCompany = null;
            List<Company> companies = new List<Company>();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_COMPANIES_LIST_BY_IDUSER", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_USER", Id);
                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        oCompany = new Company();
                        oCompany.ID = dr.IsDBNull(dr.GetOrdinal("ID")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID"));
                        oCompany.NAME = dr.IsDBNull(dr.GetOrdinal("NAME")) ? "nodata" : dr.GetString(dr.GetOrdinal("NAME"));
                        oCompany.ID_USER = dr.IsDBNull(dr.GetOrdinal("ID_USER")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID_USER"));
                        oCompany.FK_ROLE = dr.IsDBNull(dr.GetOrdinal("FK_ROLE")) ? -1 : dr.GetInt32(dr.GetOrdinal("FK_ROLE"));
                        oCompany.CompanyHaveLifeMiles = dr.IsDBNull(dr.GetOrdinal("CompanyHaveLifeMiles")) ? -1 : dr.GetInt32(dr.GetOrdinal("CompanyHaveLifeMiles"));
                        companies.Add(oCompany);
                    }
                    dr.Close();
                }
            }
            return companies;
        }
        public async Task<IEnumerable<Company>> GetCompaniesAll()
        {
            List<Company> companies = new List<Company>();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_COMPANIES_LIST_ALL", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        Company oCompany = new Company
                        {
                            ID = dr.GetInt32(dr.GetOrdinal("ID")),
                            NAME = dr.GetString(dr.GetOrdinal("NAME"))
                        };
                        companies.Add(oCompany);
                    }
                    dr.Close();
                }
            }
            return companies;
        }

        public async Task<int> Save(Company company)
        {
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_COMPANIES_SAVE", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };               
                cmd.Parameters.AddWithValue("@NAME",    company.NAME);            
                cmd.Parameters.AddWithValue("@RUC", company.RUC);
                cmd.Parameters.AddWithValue("@STATUS",  company.ESTADO);
                cmd.Parameters.AddWithValue("@CREATED_USER",    Util.MyUser.ID);
                int i = (int)await cmd.ExecuteScalarAsync();
                return i;
            }
        }

        public async Task<int> Update(Company company)
        {
         
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_COMPANIES_UPDATE", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };                
                cmd.Parameters.AddWithValue("@ID", company.ID);
                cmd.Parameters.AddWithValue("@NAME", company.NAME);
                cmd.Parameters.AddWithValue("@RUC", company.RUC);
                cmd.Parameters.AddWithValue("@STATUS", company.ESTADO);               
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
                SqlCommand cmd = new SqlCommand("SP_COMPANIES_SET_STATUS", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ID", id);

                int i = await cmd.ExecuteNonQueryAsync();
                return i;
            }
        }

        public async Task<int> Remove(int Id)
        {
            int i = -1;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_COMPANIES_DELETE", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                //var userId = Convert.ToInt32(HttpContext.Current.Session["username"].ToString().Split('¬')[0].ToString());
                cmd.Parameters.AddWithValue("@ID", Id);
                i = await cmd.ExecuteNonQueryAsync();
                return i;
            }
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            List<Company> companies = new List<Company>();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_COMPANIES_LIST", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        Company oCompany = new Company
                        {
                            ID = dr.IsDBNull(dr.GetOrdinal("ID")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID")),
                            NAME = dr.IsDBNull(dr.GetOrdinal("NAME")) ? "--" : dr.GetString(dr.GetOrdinal("NAME")),
                            LOGO = dr.IsDBNull(dr.GetOrdinal("LOGO")) ? "--" : dr.GetString(dr.GetOrdinal("LOGO")),
                            RUC = dr.IsDBNull(dr.GetOrdinal("RUC")) ? "--" : dr.GetString(dr.GetOrdinal("RUC")),
                            PHONE = dr.IsDBNull(dr.GetOrdinal("PHONE")) ? "--" : dr.GetString(dr.GetOrdinal("PHONE")),
                            ESTADO = dr.IsDBNull(dr.GetOrdinal("STATUS")) ? 0 : dr.GetInt32(dr.GetOrdinal("STATUS")),
                            Direction = dr.IsDBNull(dr.GetOrdinal("DIRECTION")) ? "--" : dr.GetString(dr.GetOrdinal("DIRECTION"))
                        };

                        companies.Add(oCompany);
                    }
                    dr.Close();
                }
            }
            return companies;
        }

        public async Task<IEnumerable<Company>> listCompanies()
        {
            List<Company> companies = new List<Company>();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_COMPANIES_LIST_T", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_USER", Util.MyUser.ID);
                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                
                if (dr.HasRows)
                {
                    /* string result = string.Empty;
                    while (await dr.ReadAsync())
                    {
                        result += dr.GetString(0);
                    }
                    companies = JsonConvert.DeserializeObject<List<Company>>(result);
                    */

                    while (await dr.ReadAsync())
                    {
                        Company oCompany = new Company
                        {
                            ID = dr.GetInt32(dr.GetOrdinal("ID")),
                            NAME = dr.GetString(dr.GetOrdinal("NAME"))
                        };
                        companies.Add(oCompany);
                    }

                    dr.Close();
                }
            }
            return companies;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 20191127 215400
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public async Task<int> CheckIfHaveLifeMiles(int CompanyId)
        {
            int i = -1;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_COMPANIES_CHECK_IF_YOU_HAVE_LIFEMILES", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                //var userId = Convert.ToInt32(HttpContext.Current.Session["username"].ToString().Split('¬')[0].ToString());
                cmd.Parameters.AddWithValue("@COMPANY_ID", CompanyId);
                cmd.Parameters.AddWithValue("@USER_ID", Util.MyUser.ID);

                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    dr.Read();
                    i = dr.IsDBNull(dr.GetOrdinal("CompanyHaveLifeMiles")) ? -1 : dr.GetInt32(dr.GetOrdinal("CompanyHaveLifeMiles"));
                    dr.Close();
                }
                return i;
            }
        }

    }
}