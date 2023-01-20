using Sicv1.Domain.Entities;
using System.Threading.Tasks;

namespace Sicv1.Domain.Contracts.Repositories
{
	public interface IDashboardRepository
	{
		Task<Dashboard> Totals(string start, string end, int channel);
	}
}
