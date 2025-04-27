using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.domain.Entities.Category;
using sms.space.management.domain.Entities.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _repository;
        public ScheduleService(IScheduleRepository repository)
        {
            _repository = repository;
        }
        public async Task<IReadOnlyList<Schedule>> GetSchedules()
        {
            var schedules = await _repository.GetSchedules();
            return schedules;
        }
        public async Task<Schedule> GetSchedule(int scheduleId)
        {
            var schedule = await _repository.GetSchedule(scheduleId);
            return schedule;
        }
        public async Task<Schedule> CreateSchedule(Schedule request)
        {
            var schedule = await _repository.CreateSchedule(request);
            return schedule;
        }
        public async Task<bool> UpdateSchedule(Schedule request)
        {
            var isupdated = await _repository.UpdateSchedule(request);
            return isupdated;
        }
        public async Task<bool> DeleteSchedule(Schedule request)
        {
            var isdeleted = await _repository.DeleteSchedule(request);
            return isdeleted;
        }
    }
}
