using Newtonsoft.Json;
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
	public class RewardsRepository : Connection, IRewardsRepository
	{

        public async Task<IEnumerable<PersonRewards>> listPersonRewards(PersonRewards entity)
        {
            List<PersonRewards> companies = new List<PersonRewards>();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_REWARDS_PERSON_LIST", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DOCUMENT", entity.DOCUMENT);
                cmd.Parameters.AddWithValue("@UPDATED_USER", entity.UPDATED_USER);
                SqlDataReader dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    string result = string.Empty;
                    while (await dr.ReadAsync())
                    {
                        result += dr.GetString(0);
                    }
                    companies = JsonConvert.DeserializeObject<List<PersonRewards>>(result);
                    dr.Close();
                }
            }
            return companies;
        }

        public List<PersonRewards> listPersonRewardExport(PersonRewards entity)
        {
            List<PersonRewards> companies = new List<PersonRewards>();
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_REWARDS_PERSON_LIST", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DOCUMENT", entity.DOCUMENT);
                cmd.Parameters.AddWithValue("@UPDATED_USER", entity.UPDATED_USER);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.HasRows)
                {
                    string result = string.Empty;
                    while ( dr.Read())
                    {
                        result += dr.GetString(0);
                    }
                    companies = JsonConvert.DeserializeObject<List<PersonRewards>>(result);
                    dr.Close();
                }
            }
            return companies;
        }

        public async Task<int>  savePersonRewards(PersonRewardsAction entity)
        {
            int i = -1;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_REWARDS_PERSON_SAVE", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@COMPANIES", entity.COMPANIES);
                cmd.Parameters.AddWithValue("@DOCUMENTS", entity.DOCUMENTS);
                cmd.Parameters.AddWithValue("@NAME_CHARGE", entity.NAME_CHARGE);
                cmd.Parameters.AddWithValue("@UPDATED_USER", entity.UPDATED_USER);
                i = await cmd.ExecuteNonQueryAsync();
                return i;
            }
        }

        public async Task<int> confirmReward(PersonRewards entity)
        {
            int i = -1;
            using (var cn = new SqlConnection(SqlConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_REWARDS_PERSON_CONFIRM", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_PERSON", entity.ID);
                cmd.Parameters.AddWithValue("@UPDATED_USER", entity.UPDATED_USER);

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
