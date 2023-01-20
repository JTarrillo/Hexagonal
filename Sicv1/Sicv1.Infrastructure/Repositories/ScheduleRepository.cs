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
    public class ScheduleRepository : Connection, IScheduleRepository
    {
        public IEnumerable<Schedule> GetSchedule(int? companyId)
        {
            Schedule oScheduleCall = null;
            List<Schedule> scheduleCalls = new List<Schedule>();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_SCHEDULE_CALL_LIST", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyID", companyId);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        oScheduleCall = new Schedule();
                        oScheduleCall.ID_CUPON = dr.IsDBNull(dr.GetOrdinal("ID_CUPON")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID_CUPON"));
                        oScheduleCall.CUPON = dr.IsDBNull(dr.GetOrdinal("CUPON")) ? "nodata" : dr.GetString(dr.GetOrdinal("CUPON"));
                        oScheduleCall.ID_COMPANY = dr.IsDBNull(dr.GetOrdinal("ID_COMPANY")) ? -1 : dr.GetInt32(dr.GetOrdinal("ID_COMPANY"));
                        oScheduleCall.COMPANY = dr.IsDBNull(dr.GetOrdinal("COMPANY")) ? "nodata" : dr.GetString(dr.GetOrdinal("COMPANY"));
                        //oScheduleCall.CONTACT_NAME = dr.IsDBNull(dr.GetOrdinal("CONTACT_NAME")) ? "nodata" : dr.GetString(dr.GetOrdinal("CONTACT_NAME"));
                        oScheduleCall.PHONE = dr.IsDBNull(dr.GetOrdinal("PHONE")) ? "nodata" : dr.GetString(dr.GetOrdinal("PHONE"));
                        //oScheduleCall.USERNAME = dr.IsDBNull(dr.GetOrdinal("USERNAME")) ? "nodata" : dr.GetString(dr.GetOrdinal("USERNAME"));
                        oScheduleCall.CREATED_AT = dr.IsDBNull(dr.GetOrdinal("CREATED_AT")) ? Convert.ToDateTime("1901-01-01") : dr.GetDateTime(dr.GetOrdinal("CREATED_AT"));
                        oScheduleCall.DOCUMENT = dr.IsDBNull(dr.GetOrdinal("DOCUMENT")) ? "nodata" : dr.GetString(dr.GetOrdinal("DOCUMENT"));

                        oScheduleCall.NAME = dr.IsDBNull(dr.GetOrdinal("NAME")) ? "nodata" : dr.GetString(dr.GetOrdinal("NAME"));
                        oScheduleCall.LASTNAME = dr.IsDBNull(dr.GetOrdinal("LASTNAME")) ? "nodata" : dr.GetString(dr.GetOrdinal("LASTNAME"));

                        scheduleCalls.Add(oScheduleCall);
                    }
                    dr.Close();
                }
            }
            return scheduleCalls;
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
                SqlCommand cmd = new SqlCommand("SP_SCHEDULE_CALL_COUNT", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                object obj = Convert.ToInt32(cmd.ExecuteScalar());
                int respuesta;
                return respuesta = (Convert.ToInt32(obj) > 0) ? Convert.ToInt32(obj) : 0;
            }
        }
    }
}
