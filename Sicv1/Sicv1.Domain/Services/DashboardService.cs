using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using System.Threading.Tasks;

namespace Sicv1.Domain.Services
{
	public class DashboardService : IDashboardService
	{
		public readonly IDashboardRepository dashboardRepository;
		public DashboardService(IDashboardRepository dashboardRepository)
		{
			this.dashboardRepository = dashboardRepository;
		}

		public async Task<Dashboard> Totals(string start, string end, int channel)
		{
			return await dashboardRepository.Totals(start, end, channel);
		}
	}
}
