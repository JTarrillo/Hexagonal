using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sicv1.Domain.Contracts.Services
{
	public interface IRewardsService : IDisposable
	{
        Task<IEnumerable<PersonRewards>> listPersonRewards(PersonRewards entity);
        List<PersonRewards> listPersonRewardExport(PersonRewards entity);
        Task<int> savePersonRewards(PersonRewardsAction entity);
        Task<int> confirmReward(PersonRewards entity);
    }
}
