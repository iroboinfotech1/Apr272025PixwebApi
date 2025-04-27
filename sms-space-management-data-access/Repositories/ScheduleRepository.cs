using Dapper;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.data.access.BusinessLogic;
using sms.space.management.domain.Entities.Schedule;
using System.Globalization;
using static Dapper.SqlMapper;

namespace sms.space.management.data.access.Repositories
{
    public class ScheduleRepository:IScheduleRepository
    {
        private readonly DbSession _session;
        public ScheduleRepository(DbSession session)
        {
            _session = session;
        }

        //public async Task<Schedule> GetSchedule(int scheduleId)
        //{
        //    var query = $@"Select * from space_admin.tschedule where schedule_id=@scheduleId";
        //    var result = await _session.Connection.QueryAsync<Schedule>(query, new { schedule_id = scheduleId }, _session.Transaction);
        //    return result.FirstOrDefault();
        //}

        public async Task<Schedule> GetSchedule(int scheduleId)
        {
            var query = @"SELECT * FROM space_admin.tschedule WHERE schedule_id = @scheduleId";

            var result = await _session.Connection.QueryAsync(query, new { scheduleId }, _session.Transaction);

            var record = result.FirstOrDefault();
            if (record == null) return null;

            return new Schedule
            {
                ScheduleId = record.schedule_id,
                BuildingId = record.building_id,
                ItemName = record.item_name,
                Timezone = record.timezone,
                // Convert time-only to DateTime using a dummy date
                StartTime = TimeSpan.Parse(record.start_time.ToString()),
                EndTime = TimeSpan.Parse(record.end_time.ToString()),
            };
        }

        public async Task<IReadOnlyList<Schedule>> GetSchedules()
        {
            var query = "Select * from space_admin.tschedule";
            var result = await _session.Connection.QueryAsync<Schedule>(query, null, _session.Transaction);
            return result.ToList();
        }
        public async Task<Schedule> CreateSchedule(Schedule request)
        {

            //Step 1: Get TimeZoneInfo from the timezone string
            TimeZoneInfo tzInfo;
            try
            {
                tzInfo = TimeZoneInfo.FindSystemTimeZoneById(request.Timezone);
            }
            catch (TimeZoneNotFoundException)
            {
                throw new ArgumentException("Invalid timezone provided.");

            }

            DateTime baseDate = DateTime.Today;
            DateTime startLocal = DateTime.SpecifyKind(baseDate.Add(request.StartTime), DateTimeKind.Unspecified);
            DateTime endLocal = DateTime.SpecifyKind(baseDate.Add(request.EndTime), DateTimeKind.Unspecified);

            // Step 2: Convert to UTC based on timezone
            DateTime startUtc = TimeZoneInfo.ConvertTimeToUtc(startLocal, tzInfo);
            DateTime endUtc = TimeZoneInfo.ConvertTimeToUtc(endLocal, tzInfo);


            var query = $@"INSERT INTO  space_admin.tschedule (Building_id,Item_name,start_time,End_time,timezone)
						VALUES (@BuildingId,@ItemName,@StartTime,@EndTime,@Timezone)
						RETURNING schedule_id			
            ";
            //RETURNING id

            var result = await _session.Connection.ExecuteScalarAsync(query, new
            {
                BuildingId = request.BuildingId,
                ItemName = request.ItemName,
                StartTime = startUtc,
                EndTime = endUtc,
                Timezone = request.Timezone
            }, _session.Transaction);

            request.ScheduleId = await _session.Connection.ExecuteScalarAsync<int>(query, request, _session.Transaction);
            return request;
        }


        public async Task<bool> UpdateSchedule(Schedule request)
        {
            //Step 1: Get TimeZoneInfo from the timezone string
            TimeZoneInfo tzInfo;
            try
            {
                tzInfo = TimeZoneInfo.FindSystemTimeZoneById(request.Timezone);
            }
            catch (TimeZoneNotFoundException)
            {
                throw new ArgumentException("Invalid timezone provided.");
            }
            // Step 1: Convert TimeSpan to DateTime (assuming today's date)
            DateTime baseDate = DateTime.Today;
            DateTime startLocal = DateTime.SpecifyKind(baseDate.Add(request.StartTime), DateTimeKind.Unspecified);
            DateTime endLocal = DateTime.SpecifyKind(baseDate.Add(request.EndTime), DateTimeKind.Unspecified);

            // Step 2: Convert to UTC based on timezone
            DateTime startUtc = TimeZoneInfo.ConvertTimeToUtc(startLocal, tzInfo);
            DateTime endUtc = TimeZoneInfo.ConvertTimeToUtc(endLocal, tzInfo);


            // Step 3: SQL query
            var query = $@"
            UPDATE space_admin.tschedule 
            SET 
                Building_id = @BuildingId,
                Item_name = @ItemName,
                start_time = @StartTime,
                End_time = @EndTime,
                timezone = @Timezone
                WHERE schedule_id = @ScheduleId";

            var result = await _session.Connection.ExecuteAsync(query, new
            {
                BuildingId = request.BuildingId,
                ItemName = request.ItemName,
                StartTime = startUtc,
                EndTime =endUtc,
                Timezone = request.Timezone,
                ScheduleId = request.ScheduleId
            }, _session.Transaction);

            return result > 0;
        }

        public async Task<bool> DeleteSchedule(Schedule request)
        {
            var query = "Delete from space_admin.tschedule where schedule_id=@ScheduleId and building_Id= @Buildingid";
            var result = await _session.Connection.ExecuteAsync(query, new
            {
                ScheduleId = request.ScheduleId,     // Match @ScheduleId
                BuildingId = request.BuildingId      // Match @BuildingId
            }, _session.Transaction);

            if (result > 0)
            {
                return true;
            }
            else return false;
        }
    }
}
