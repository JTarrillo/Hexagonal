using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _schedule;
        public ScheduleService(IScheduleRepository schedule)
        {
            this._schedule = schedule;
        }

        public IEnumerable<Schedule> GetScheduleCalls(int? companyId)
        {
            try
            {
                return _schedule.GetSchedule(companyId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
        {
            _schedule.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<int> GetCount()
        {
            try
            {
                return await _schedule.GetCount();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
