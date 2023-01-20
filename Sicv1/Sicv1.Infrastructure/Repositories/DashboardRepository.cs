using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Entities;
using Sicv1.Infrastructure.Cross;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sicv1.Infrastructure.Repositories
{
    public class DashboardRepository : Connection, IDashboardRepository
    {
        public async Task<Dashboard> Totals(string start, string end, int channel)
        {
            Dashboard dashboard = new Dashboard();
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlConnectionString))
                {
                    //var vStartDate;// = Convert.ToDateTime(start).Year + "";
                   // var vEndDate;// = Convert.ToDateTime(end).Year + "";
                    var vChannel = channel;

                    //vStartDate += Convert.ToDateTime(start).Month.ToString().Length == 1 ? "0" + Convert.ToDateTime(start).Month : Convert.ToDateTime(start).Month + "";
                    //vStartDate += Convert.ToDateTime(start).Day.ToString().Length == 1 ? "0" + Convert.ToDateTime(start).Day : Convert.ToDateTime(start).Day + "";

                    //vEndDate += Convert.ToDateTime(end).Month.ToString().Length == 1 ? "0" + Convert.ToDateTime(end).Month : Convert.ToDateTime(end).Month + "";
                    //vEndDate += Convert.ToDateTime(end).Day.ToString().Length == 1 ? "0" + Convert.ToDateTime(end).Day : Convert.ToDateTime(end).Day + "";
                    conn.Open();
                    SqlCommand cmd = new SqlCommand((channel==0?"SP_DASHBOARD_INDICATORS":"SP_DASHBOARD_INDICATORS_FOR_CHANNEL"), conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@START", start);
                    cmd.Parameters.AddWithValue("@END", end);
                    cmd.Parameters.AddWithValue("@CHANNEL", vChannel);
                    SqlDataAdapter _adap = new SqlDataAdapter(cmd);
                    DataSet dtTables = new DataSet();
                    _adap.Fill(dtTables);
                    if (dtTables != null)
                    {
                        if (dtTables.Tables.Count > 3)
                        {
                            dashboard.STATUS = true;
                            DataTable dt1 = dtTables.Tables[0];
                            DataTable dt2 = dtTables.Tables[1];
                            DataTable dt3 = dtTables.Tables[2];
                            DataTable dt4 = dtTables.Tables[3];
                            if (dt1.Rows.Count != 0)
                            {
                                DataRow reader = dt1.Rows[0];
                                dashboard.CounterSchedule = int.Parse(reader["COUNTER_SCHEDULE"].ToString());
                                dashboard.CounterUserAll = int.Parse(reader["COUNTER_USER_ALL"].ToString());
                                dashboard.CounterUserActive = int.Parse(reader["COUNTER_USER_ACTIVE"].ToString());
                                dashboard.CounterSession = int.Parse(reader["COUNTER_SESSION"].ToString());
                                dashboard.CounterCouponGenerated = int.Parse(reader["COUNTER_COUPON_GENERATED"].ToString());
                                dashboard.CounterCouponApproved = int.Parse(reader["COUNTER_COUPON_APPROVED"].ToString());
                                dashboard.CounterUserUpdateInfo = int.Parse(reader["COUNTER_USERS_UPDATE_INFO"].ToString());
                                dashboard.CounterUserAndroid = int.Parse((string.IsNullOrEmpty(reader["COUNTER_USERS_ANDROID"].ToString()) ? "0" : reader["COUNTER_USERS_ANDROID"].ToString()));
                                dashboard.CounterUserIos = int.Parse((string.IsNullOrEmpty(reader["COUNTER_USERS_IOS"].ToString()) ? "0" : reader["COUNTER_USERS_IOS"].ToString()));
                            }
                            if (dt2.Rows.Count != 0)
                            {
                                DataRow reader2 = dt2.Rows[0];
                                Int64[] days = reader2["DAYS"].ToString().Split(',').Select(Int64.Parse).ToArray();
                                Int64[] totales = reader2["TOTALS"].ToString().Split(',').Select(Int64.Parse).ToArray();
                                object[,] oks = new object[,] { { } };
                                for (int i = 0; i <days.Length; i++) {
                                    dashboard.chart__1.Add(new Int64[]{ days[i], totales[i]});
                                }
                            }
                            if (dt3.Rows.Count != 0)
                            {
                                DataRow reader3 = dt3.Rows[0];
                                dashboard.chart__2.days = reader3["DAYS"].ToString().Split(',');
                                dashboard.chart__2.bien = reader3["COUNT_BIENESTAR"].ToString().Split(',').Select(Int32.Parse).ToArray();
                                dashboard.chart__2.nutr = reader3["COUNT_NUTRICION"].ToString().Split(',').Select(Int32.Parse).ToArray();
                                dashboard.chart__2.rend = reader3["COUNT_RENDIMIENTO"].ToString().Split(',').Select(Int32.Parse).ToArray();
                                dashboard.chart__2.rela = reader3["COUNT_RELAJACION"].ToString().Split(',').Select(Int32.Parse).ToArray();
                                dashboard.chart__2.entr = reader3["COUNT_ENTRETENIMIENTO"].ToString().Split(',').Select(Int32.Parse).ToArray();
                            }
                            if (dt4.Rows.Count != 0)
                            {
                                DataRow reader4 = dt4.Rows[0];
                                dashboard.chart__3.days = reader4["DAYS"].ToString().Split(',');
                                dashboard.chart__3.generated = reader4["TOTALS"].ToString().Split(',').Select(Int32.Parse).ToArray();
                                dashboard.chart__3.used = reader4["TOTALS_USED"].ToString().Split(',').Select(Int32.Parse).ToArray();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dashboard.STATUS = false;
                dashboard.MESSAGE = ex.Message;
                dashboard.TRACE = ex.StackTrace;
            }
            return dashboard;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
