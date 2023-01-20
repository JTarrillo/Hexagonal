using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Entities;
using Sicv1.Infrastructure.Cross;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;

namespace Sicv1.Infrastructure.Repositories
{
    public class UserRepository : Connection, IUserRepository
    {
        public async Task<User> SignIn(string UserName, string Password)
        {
            User user = null;
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["db_connection"].ToString()))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_USERS_SIGNIN", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USERNAME", UserName);
                cmd.Parameters.AddWithValue("@PASSWORD", Password);
                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow);
                if (dr.HasRows)
                {
                    await dr.ReadAsync();
                    user = new User
                    {
                        ID = dr.IsDBNull(dr.GetOrdinal("ID")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID")),
                        USERNAME = dr.IsDBNull(dr.GetOrdinal("USERNAME")) ? "nodata" : dr.GetString(dr.GetOrdinal("USERNAME")),
                        FULLNAME = dr.IsDBNull(dr.GetOrdinal("FULLNAME")) ? "nodata" : dr.GetString(dr.GetOrdinal("FULLNAME")),
                        NAME = dr.IsDBNull(dr.GetOrdinal("NAME")) ? "nodata" : dr.GetString(dr.GetOrdinal("NAME")),
                        LASTNAME_P = dr.IsDBNull(dr.GetOrdinal("LASTNAME_P")) ? "nodata" : dr.GetString(dr.GetOrdinal("LASTNAME_P")),
                        LASTNAME_M = dr.IsDBNull(dr.GetOrdinal("LASTNAME_M")) ? "nodata" : dr.GetString(dr.GetOrdinal("LASTNAME_M")),
                        ESTADO = dr.IsDBNull(dr.GetOrdinal("ESTADO")) ? -1 : dr.GetInt32(dr.GetOrdinal("ESTADO")),
                        SESSION = dr.IsDBNull(dr.GetOrdinal("SESSION")) ? -1 : dr.GetInt32(dr.GetOrdinal("SESSION")),
                        NAMESHORT = dr.IsDBNull(dr.GetOrdinal("NAMESHORT")) ? "nodata" : dr.GetString(dr.GetOrdinal("NAMESHORT")),
                        PASS = dr.IsDBNull(dr.GetOrdinal("PASS")) ? "nodata" : dr.GetString(dr.GetOrdinal("PASS")),
                        LOGO = dr.IsDBNull(dr.GetOrdinal("LOGO")) ? "nodata" : dr.GetString(dr.GetOrdinal("LOGO")),
                        ID_COMPANY = dr.IsDBNull(dr.GetOrdinal("ID_COMPANY")) ? "-1" : dr.GetString(dr.GetOrdinal("ID_COMPANY")),
                        FK_ROLE = dr.IsDBNull(dr.GetOrdinal("FK_ROLE")) ? -1 : dr.GetInt32(dr.GetOrdinal("FK_ROLE"))
                    };
                }
            }
            return user;
        }

        public async Task<int> Save(User user)
        {
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                //var userId = Convert.ToInt32(HttpContext.Current.Session["username"].ToString().Split('¬')[0].ToString());
                //user.CREATED_USER = userId;
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_USERS_INSERT", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@NAME", user.NAME);
                cmd.Parameters.AddWithValue("@LASTNAME_P", user.LASTNAME_P);
                cmd.Parameters.AddWithValue("@LASTNAME_M", user.LASTNAME_M);
                cmd.Parameters.AddWithValue("@USERNAME", user.USERNAME);

                cmd.Parameters.Add(new SqlParameter("PASSWORD", SqlDbType.VarBinary));
                var pwd = new Security().Encrypt(user.PASSWORD);
                user.PWD = Security.StrToByteArray(pwd);
                cmd.Parameters["PASSWORD"].Value = user.PWD;

                cmd.Parameters.AddWithValue("@TYPE_DOCUMENT", user.TYPE_DOCUMENT);
                cmd.Parameters.AddWithValue("@DOCUMENT", user.DOCUMENT);
                cmd.Parameters.AddWithValue("@PHONE1", user.PHONE1);
                cmd.Parameters.AddWithValue("@GENDER", user.GENDER);
                cmd.Parameters.AddWithValue("@EMAIL", user.EMAIL);
                cmd.Parameters.AddWithValue("@ADDRESS", user.ADDRESS);
                //cmd.Parameters.AddWithValue("@UBIGEO", user.UBIGEO);
                cmd.Parameters.AddWithValue("@FK_ROLE", user.FK_ROLE);
                //cmd.Parameters.AddWithValue("@CARD", user.CARD);
                cmd.Parameters.AddWithValue("@ESTADO", user.ESTADO);
                cmd.Parameters.AddWithValue("@CREATED_USER", Util.MyUser.ID);
                cmd.Parameters.AddWithValue("@COMPANY_ID", user.COMPANY_ID);
                cmd.Parameters.AddWithValue("@BIRTHDAY", user.BIRTHDAY);
                //int i = await cmd.ExecuteNonQueryAsync();
                int i = (int)cmd.ExecuteScalar();
                return i;
            }
        }

        public async Task<int> RegeneratePassword(User user)
        {
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                //var userId = Convert.ToInt32(HttpContext.Current.Session["username"].ToString().Split('¬')[0].ToString());
                //user.CREATED_USER = userId;
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_USERS_REGENERATE_PASSWORD", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@USER_ID", user.ID);                

                cmd.Parameters.Add(new SqlParameter("PASSWORD", SqlDbType.VarBinary));
                var pwd = new Security().Encrypt(user.PASSWORD);
                user.PWD = Security.StrToByteArray(pwd);
                cmd.Parameters["PASSWORD"].Value = user.PWD;

                int i = await cmd.ExecuteNonQueryAsync();
                return i;
            }
        }

        public async Task<int> Update(User user)
        {
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                //var userId = Convert.ToInt32(HttpContext.Current.Session["username"].ToString().Split('¬')[0].ToString());
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_USERS_UPDATE", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ID", user.ID);
                cmd.Parameters.AddWithValue("@NAME", user.NAME);
                cmd.Parameters.AddWithValue("@LASTNAME_P", user.LASTNAME_P);
                cmd.Parameters.AddWithValue("@LASTNAME_M", user.LASTNAME_M);
                cmd.Parameters.AddWithValue("@USERNAME", user.USERNAME);

                if (user.PASSWORD != null) {
                    cmd.Parameters.Add(new SqlParameter("PASSWORD", SqlDbType.VarBinary));
                    var _pwd = new Security().Encrypt(user.PASSWORD);
                    user.PWD = Security.StrToByteArray(_pwd);
                    cmd.Parameters["PASSWORD"].Value = user.PWD;
                }

                cmd.Parameters.AddWithValue("@TYPE_DOCUMENT", user.TYPE_DOCUMENT);
                cmd.Parameters.AddWithValue("@DOCUMENT", user.DOCUMENT);
                cmd.Parameters.AddWithValue("@PHONE1", user.PHONE1);
                cmd.Parameters.AddWithValue("@GENDER", user.GENDER);
                cmd.Parameters.AddWithValue("@EMAIL", user.EMAIL);
                cmd.Parameters.AddWithValue("@ADDRESS", user.ADDRESS);        
                cmd.Parameters.AddWithValue("@FK_ROLE", user.FK_ROLE);     
                cmd.Parameters.AddWithValue("@ESTADO", user.ESTADO);
                cmd.Parameters.AddWithValue("@UPDATED_USER", Util.MyUser.ID);     
                cmd.Parameters.AddWithValue("@BIRTHDAY", user.BIRTHDAY);
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
                SqlCommand cmd = new SqlCommand("SP_USERS_SET_STATUS", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ID", id);
                
                int i = await cmd.ExecuteNonQueryAsync();
                return i;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<User>> GetUsers(string NumDniSearch = null, int? RoleId = null, decimal? CurrentPage = null, decimal? RecordsPerPage = null)
        {
            List<User> lstUsers = new List<User>();
            User oUser = null;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_USERS_LIST", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@numDniSearch", NumDniSearch);
                cmd.Parameters.AddWithValue("@roleId", RoleId);
                cmd.Parameters.AddWithValue("@paginaActual", CurrentPage);
                cmd.Parameters.AddWithValue("@numeroRegistrosPorPagina", RecordsPerPage);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        oUser = new User();
                        oUser.ID = dr.GetInt32(dr.GetOrdinal("Id"));
                        oUser.CODE_LIFEMILES = dr.GetString(dr.GetOrdinal("CODE_LIFEMILES"));
                        oUser.NAME = dr.GetString(dr.GetOrdinal("Nombres"));
                        oUser.LASTNAME_P = dr.GetString(dr.GetOrdinal("Ap. Paterno"));
                        oUser.LASTNAME_M = dr.GetString(dr.GetOrdinal("Ap. Materno"));
                        oUser.TYPE_DOCUMENT = dr.GetString(dr.GetOrdinal("Tipo doc."));
                        oUser.DOCUMENT = dr.GetString(dr.GetOrdinal("Nro. doc."));
                        oUser.PHONE1 = dr.GetString(dr.GetOrdinal("Teléf."));
                        oUser.EMAIL = dr.GetString(dr.GetOrdinal("Email"));
                        oUser.ADDRESS = dr.GetString(dr.GetOrdinal("Dirección"));
                        oUser.DEPARTAMENT = dr.GetString(dr.GetOrdinal("Dpto."));
                        oUser.PROVINCE = dr.GetString(dr.GetOrdinal("Prov."));
                        oUser.DISTRICT = dr.GetString(dr.GetOrdinal("Dist."));
                        oUser.REGISTERED_BY = dr.GetString(dr.GetOrdinal("Registrado por"));
                        oUser.CREATED_AT = dr.GetDateTime(dr.GetOrdinal("Fec. de registro"));
                        oUser.UPDATED_BY = dr.GetString(dr.GetOrdinal("Actualizado por"));
                        oUser.UPDATED_AT = dr.GetDateTime(dr.GetOrdinal("Fec. de actualización"));
                        oUser.CARD = dr.GetString(dr.GetOrdinal("Tarjeta"));
                        oUser.ADDITIONALS_PURPOSES = dr.GetBoolean(dr.GetOrdinal("Acepta fines adicionales"));
                        oUser.ROL = dr.GetString(dr.GetOrdinal("Rol"));

                        oUser.DEPARTAMENT_ID= dr.GetString(dr.GetOrdinal("DEPARTAMENT_ID"));
                        oUser.PROVINCE_ID = dr.GetString(dr.GetOrdinal("PROVINCE_ID"));
                        oUser.DISTRICT_ID = dr.GetString(dr.GetOrdinal("DISTRICT_ID"));
                        oUser.GENDER = dr.GetString(dr.GetOrdinal("GENDER"));
						//oUser.PASSWORD = dr.GetString(dr.GetOrdinal("PASSWORD"));//new Security().Decrypt(dr.GetString(dr.GetOrdinal("PASSWORD")));
						oUser.FK_ROLE = dr.GetInt32(dr.GetOrdinal("FK_ROLE"));
                        oUser.USERNAME = dr.GetString(dr.GetOrdinal("USERNAME"));
                        oUser.ESTADO = dr.GetInt32(dr.GetOrdinal("ESTADO"));
                        oUser.TYPE_SEGMENT = dr.GetString(dr.GetOrdinal("TYPE_SEGMENT"));

                        lstUsers.Add(oUser);
                    }
                    dr.Close();
                }
            }
            return lstUsers;
        }

        public async Task<IEnumerable<User>> GetUsersAll(int? RoleId = null)
        {
            List<User> userList = new List<User>();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_USERS_LIST_ALL", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@ROLE_ID", RoleId);
                cmd.Parameters.AddWithValue("@USER_ID", Util.MyUser.ID);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        User oUser = new User
                        {
                            ID = dr.GetInt32(dr.GetOrdinal("ID")),
                            NAME = dr.GetString(dr.GetOrdinal("NAME")),
                            LASTNAME_P = dr.GetString(dr.GetOrdinal("LASTNAME_P")),
                            LASTNAME_M = dr.GetString(dr.GetOrdinal("LASTNAME_M")),
                            TYPE_DOCUMENT = dr.GetString(dr.GetOrdinal("TYPE_DOCUMENT")),
                            DOCUMENT = dr.GetString(dr.GetOrdinal("DOCUMENT")),
                            EMAIL = dr.GetString(dr.GetOrdinal("EMAIL")),
                            ROLE = dr.GetString(dr.GetOrdinal("ROLE")),
                            FK_ROLE = dr.GetInt32(dr.GetOrdinal("FK_ROLE")),
                            PHONE1 = dr.IsDBNull(dr.GetOrdinal("PHONE1")) ? "" : dr.GetString(dr.GetOrdinal("PHONE1")),
                            GENDER = dr.IsDBNull(dr.GetOrdinal("GENDER")) ? "" : dr.GetString(dr.GetOrdinal("GENDER")),
                            BIRTHDAY = dr.IsDBNull(dr.GetOrdinal("BIRTHDAY")) ? "" : dr.GetString(dr.GetOrdinal("BIRTHDAY")),
                            ADDRESS = dr.IsDBNull(dr.GetOrdinal("ADDRESS")) ? "" : dr.GetString(dr.GetOrdinal("ADDRESS")),
                            ID_COMPANY = dr.IsDBNull(dr.GetOrdinal("ID_COMPANY")) ? "" : Convert.ToString(dr.GetInt32(dr.GetOrdinal("ID_COMPANY"))),
                            COMPANY = dr.IsDBNull(dr.GetOrdinal("COMPANY")) ? "" : dr.GetString(dr.GetOrdinal("COMPANY")),

                            ESTADO = dr.GetInt32(dr.GetOrdinal("STATUS"))
                        };

                        userList.Add(oUser);
                    }
                    dr.Close();
                }
            }
            return userList;
        }

        public async Task<User> GetUsersDetail(int id)
        {
            User user = null;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_USERS_DETAIL", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@USER_ID", id);

                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow);
               
                if (dr.HasRows)
                {
                    await dr.ReadAsync();
                    user = new User
                    { 

                        ID = dr.GetInt32(dr.GetOrdinal("ID")),
                        ID_COMPANY = dr.IsDBNull(dr.GetOrdinal("ID_COMPANY")) ? "" : Convert.ToString(dr.GetInt32(dr.GetOrdinal("ID_COMPANY"))),
                        FK_ROLE = dr.GetInt32(dr.GetOrdinal("FK_ROLE")),
                        ESTADO = dr.GetInt32(dr.GetOrdinal("STATUS")),
                        NAME = dr.IsDBNull(dr.GetOrdinal("NAME")) ? "" : dr.GetString(dr.GetOrdinal("NAME")),
                        LASTNAME_P = dr.IsDBNull(dr.GetOrdinal("LASTNAME_P")) ? "" : dr.GetString(dr.GetOrdinal("LASTNAME_P")),
                        LASTNAME_M = dr.IsDBNull(dr.GetOrdinal("LASTNAME_M")) ? "" : dr.GetString(dr.GetOrdinal("LASTNAME_M")),
                        TYPE_DOCUMENT = dr.IsDBNull(dr.GetOrdinal("TYPE_DOCUMENT")) ? "" : dr.GetString(dr.GetOrdinal("TYPE_DOCUMENT")),
                        DOCUMENT = dr.IsDBNull(dr.GetOrdinal("DOCUMENT")) ? "" : dr.GetString(dr.GetOrdinal("DOCUMENT")),
                        PHONE1 = dr.IsDBNull(dr.GetOrdinal("PHONE1"))?"":dr.GetString(dr.GetOrdinal("PHONE1")),
                        GENDER = dr.IsDBNull(dr.GetOrdinal("GENDER")) ? "" : dr.GetString(dr.GetOrdinal("GENDER")),
                        EMAIL = dr.IsDBNull(dr.GetOrdinal("EMAIL")) ? "" : dr.GetString(dr.GetOrdinal("EMAIL")),
                        BIRTHDAY = dr.IsDBNull(dr.GetOrdinal("BIRTHDAY"))?"": dr.GetString(dr.GetOrdinal("BIRTHDAY")),
                        ADDRESS = dr.IsDBNull(dr.GetOrdinal("ADDRESS")) ? "" : dr.GetString(dr.GetOrdinal("ADDRESS"))

                    };
                        
                }   
            }
            return user;
        }           
        
        public async Task<int> GetCount(int? roleId = -1)
        {
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                await cn.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_USERS_COUNT", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RoleId", roleId);
                object obj = Convert.ToInt32(cmd.ExecuteScalar());
                int respuesta;
                return respuesta = (Convert.ToInt32(obj) > 0) ? Convert.ToInt32(obj) : 0;
            }
        }

        public async Task<string> ValidateNumDocument(string num, int userId)
        {
            string result = "";
            User user = null;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_USERS_VALIDATE_DNI", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DOCUMENT", num);
                cmd.Parameters.AddWithValue("@ID", userId);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.HasRows)
                {
                    await dr.ReadAsync();
                    user = new User();
                    user.DOCUMENT = dr.IsDBNull(dr.GetOrdinal("DOCUMENT")) ? "nodata" : dr.GetString(dr.GetOrdinal("DOCUMENT"));
                    result = user.DOCUMENT;
                }
            }
            return result;
        }

        public IEnumerable<User> ExportToExcel(string fi, string ff)
        {
            User user = null;
            List<User> lstUsers = new List<User>();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_USERS_EXPORT_EXCEL", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (fi == "" && ff == "")
                {
                    cmd.Parameters.AddWithValue("@START_DATE", "");
                    cmd.Parameters.AddWithValue("@END_DATE", "");
                }
                else if (fi != "" && ff != "")
                {
                    var vStartDate = Convert.ToDateTime(fi).Year + "-" + Convert.ToDateTime(fi).Month + "-" + Convert.ToDateTime(fi).Day;
                    var vEndDate = Convert.ToDateTime(ff).Year + "-" + Convert.ToDateTime(ff).Month + "-" + Convert.ToDateTime(ff).Day;

                    cmd.Parameters.AddWithValue("@START_DATE", vStartDate);
                    cmd.Parameters.AddWithValue("@END_DATE", vEndDate);
                }

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        user = new User();
                        user.ID = dr.GetInt32(dr.GetOrdinal("ID"));
                        user.CODE_LIFEMILES = dr.GetString(dr.GetOrdinal("CODE_LIFEMILES"));
                        user.NAME = dr.GetString(dr.GetOrdinal("NAME"));
                        user.LASTNAME_P = dr.GetString(dr.GetOrdinal("LASTNAME_P"));
                        user.LASTNAME_M = dr.GetString(dr.GetOrdinal("LASTNAME_M"));
                        user.TYPE_DOCUMENT = dr.GetString(dr.GetOrdinal("TYPE_DOCUMENT"));
                        user.DOCUMENT = dr.GetString(dr.GetOrdinal("DOCUMENT"));
                        user.GENDER = dr.GetString(dr.GetOrdinal("GENDER"));
                        user.PHONE1 = dr.GetString(dr.GetOrdinal("PHONE1"));
                        user.PHONE2 = dr.GetString(dr.GetOrdinal("PHONE2"));
                        user.EMAIL = dr.GetString(dr.GetOrdinal("EMAIL"));
                        user.ADDRESS = dr.GetString(dr.GetOrdinal("ADDRESS"));
                        user.ESTADO = dr.GetInt32(dr.GetOrdinal("STATUS"));
                        user.DEPARTAMENT = dr.GetString(dr.GetOrdinal("DEPARTAMENT"));
                        user.PROVINCE = dr.GetString(dr.GetOrdinal("PROVINCE"));
                        user.DISTRICT = dr.GetString(dr.GetOrdinal("DISTRICT"));
                        user.UPDATE_USERNAME = dr.GetString(dr.GetOrdinal("UPDATE_USERNAME"));
                        user.UPDATED_AT = dr.GetDateTime(dr.GetOrdinal("UPDATED_AT"));
                        lstUsers.Add(user);
                    }
                    dr.Close();
                }
            }
            return lstUsers;
        }

        public IEnumerable<User> ExportToExcelLM(string fi, string ff)
        {
              User user = null;
                List<User> lstUsers = new List<User>();
            try
            {
              
                using (var cn = new SqlConnection(SqlConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SP_USERS_EXPORT_EXCEL_LM", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (fi == "" && ff == "")
                    {
                        cmd.Parameters.AddWithValue("@START_DATE", "");
                        cmd.Parameters.AddWithValue("@END_DATE", "");
                    }
                    else if (fi != "" && ff != "")
                    {
                        var vStartDate = Convert.ToDateTime(fi).Year + "-" + Convert.ToDateTime(fi).Month + "-" + Convert.ToDateTime(fi).Day;
                        var vEndDate = Convert.ToDateTime(ff).Year + "-" + Convert.ToDateTime(ff).Month + "-" + Convert.ToDateTime(ff).Day;

                        cmd.Parameters.AddWithValue("@START_DATE", vStartDate);
                        cmd.Parameters.AddWithValue("@END_DATE", vEndDate);
                    }

                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            user = new User();
                            user.ID = dr.GetInt32(dr.GetOrdinal("ID"));
                            user.CODE_LIFEMILES = dr.GetString(dr.GetOrdinal("CODE_LIFEMILES"));
                            user.NAME = dr.GetString(dr.GetOrdinal("NAME"));
                            user.LASTNAME_P = dr.GetString(dr.GetOrdinal("LASTNAME_P"));
                            user.LASTNAME_M = dr.GetString(dr.GetOrdinal("LASTNAME_M"));
                            user.TYPE_DOCUMENT = dr.GetString(dr.GetOrdinal("TYPE_DOCUMENT"));
                            user.DOCUMENT = dr.GetString(dr.GetOrdinal("DOCUMENT"));
                            user.GENDER = dr.GetString(dr.GetOrdinal("GENDER"));
                            user.PHONE1 = dr.GetString(dr.GetOrdinal("PHONE1"));
                            user.PHONE2 = dr.GetString(dr.GetOrdinal("PHONE2"));
                            user.EMAIL = dr.GetString(dr.GetOrdinal("EMAIL"));
                            user.ADDRESS = dr.GetString(dr.GetOrdinal("ADDRESS"));
                            user.ESTADO = dr.GetInt32(dr.GetOrdinal("STATUS"));
                            user.DEPARTAMENT = dr.GetString(dr.GetOrdinal("DEPARTAMENT"));
                            user.PROVINCE = dr.GetString(dr.GetOrdinal("PROVINCE"));
                            user.DISTRICT = dr.GetString(dr.GetOrdinal("DISTRICT"));
                            user.UPDATE_USERNAME = dr.GetString(dr.GetOrdinal("UPDATE_USERNAME"));
                            user.UPDATED_AT = dr.GetDateTime(dr.GetOrdinal("UPDATED_AT"));
                            lstUsers.Add(user);
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception ex) {
            }
            return lstUsers;
        }
    }
}
