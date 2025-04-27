using sms.space.management.domain.Entities.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Abstracts.Repositories
{
    public interface IScheduleRepository
    {
        Task<IReadOnlyList<Schedule>> GetSchedules();
        Task<Schedule> GetSchedule(int ScheduleId);
        Task<Schedule> CreateSchedule(Schedule request);
        Task<bool> UpdateSchedule(Schedule request);
        Task<bool> DeleteSchedule(Schedule request);
    }
}
