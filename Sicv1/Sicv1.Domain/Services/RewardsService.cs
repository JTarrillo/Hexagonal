using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sicv1.Domain.Services
{
	public class RewardsService : IRewardsService
	{
		private readonly IRewardsRepository rewardsRepository;
		public RewardsService(IRewardsRepository notifyRepository)
		{
			this.rewardsRepository = notifyRepository;
		}

        public async Task<IEnumerable<PersonRewards>> listPersonRewards(PersonRewards entity)
        {
            try
            {
                return await rewardsRepository.listPersonRewards(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<PersonRewards> listPersonRewardExport(PersonRewards entity)
        {
            try
            {
                return  rewardsRepository.listPersonRewardExport(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> savePersonRewards(PersonRewardsAction entity)
        {
            try
            {
                return await rewardsRepository.savePersonRewards(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> confirmReward(PersonRewards entity)
        {
            try
            {
                return await rewardsRepository.confirmReward(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
		{
            rewardsRepository.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
