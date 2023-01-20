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
    public class UbigeoRepository : Connection, IUbigeoRepository
    {
        public async Task<IEnumerable<Ubigeo>> GetDptos()
        {
            Ubigeo oUbigeo = null;
            List<Ubigeo> ubigeos = new List<Ubigeo>();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_UBIGEO_LIST_DPTO", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        oUbigeo = new Ubigeo();
                        oUbigeo.COD_DPTO = dr.GetString(0);
                        oUbigeo.NOM_DPTO = dr.GetString(1);
                        ubigeos.Add(oUbigeo);
                    }
                    dr.Close();
                }
                return ubigeos;
            }
        }

        public async Task<IEnumerable<Ubigeo>> GetProvs(string DptoId = null)
        {
            Ubigeo oUbigeo = null;
            List<Ubigeo> ubigeos = new List<Ubigeo>();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_UBIGEO_LIST_PROV", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@COD_DPTO", DptoId);
                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        oUbigeo = new Ubigeo();
                        oUbigeo.COD_DPTO = dr.GetString(0);
                        oUbigeo.COD_PROV = dr.GetString(1);
                        oUbigeo.NOM_PROV = dr.GetString(2);
                        ubigeos.Add(oUbigeo);
                    }
                    dr.Close();
                }
                return ubigeos;
            }
        }

        public async Task<IEnumerable<Ubigeo>> GetDists(string DptoId = null, string ProvId = null)
        {
            Ubigeo oUbigeo = null;
            List<Ubigeo> ubigeos = new List<Ubigeo>();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_UBIGEO_LIST_DIST", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@COD_DPTO", DptoId);
                cmd.Parameters.AddWithValue("@COD_PROV", ProvId);
                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    while (await dr.ReadAsync())
                    {
                        oUbigeo = new Ubigeo();
                        oUbigeo.COD_DPTO = dr.GetString(0);
                        oUbigeo.COD_PROV = dr.GetString(1);
                        oUbigeo.COD_DIST = dr.GetString(2);
                        oUbigeo.NOM_DIST = dr.GetString(3);
                        ubigeos.Add(oUbigeo);
                    }
                    dr.Close();
                }
                return ubigeos;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
