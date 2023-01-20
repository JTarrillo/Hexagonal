using Sicv1.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Contracts.Repositories
{
    public interface IScheduleRepository : IRepositoryBase<Schedule>
    {
        IEnumerable<Schedule> GetSchedule(int? companyId);
        Task<int> GetCount();
    }
}
