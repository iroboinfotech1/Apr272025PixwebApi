using Dapper;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.data.access.BusinessLogic;
using sms.space.management.domain.Entities.BookMeeting;
using sms.space.management.domain.Entities.BookRoom;
using sms.space.management.domain.Entities.PlayerManagement;
using sms.space.management.domain.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace sms.space.management.data.access.Repositories
{
    public class BookServicesRepository : IBookServicesRepository
    {
        private readonly DbSession _session;

        public BookServicesRepository(DbSession session)
        {
            _session = session;
        }


        #region == AddServices ==


        public async Task<List<ServiceDetail>> GetBookServices(int meetingId)
        {
            var query = $@"Select * from space_admin.space_service where meeting_id =@Id";
            var result = await _session.Connection.QueryAsync<ServiceDetail>(query, new { Id = meetingId }, _session.Transaction);
            return result.ToList();
        }
        public async Task<IReadOnlyList<ServiceDetail>> GetBookServices()
        {
            var query = "Select * from space_admin.space_service";
            var result = await _session.Connection.QueryAsync<ServiceDetail>(query, null, _session.Transaction);
            return result.ToList();
        }

        public async Task<ServiceDetail> CreateBookServices(ServiceDetail request)
        {
            var query = $@"INSERT INTO  space_admin.space_service(meeting_id,service_id,service_count,created_by,created_date)
						VALUES (@MeetingId,@ServiceId,@ServiceCount,@CreatedBy,now() at time zone 'utc')
						RETURNING service_id			
            ";
            //RETURNING id


            request.ServiceId = await _session.Connection.ExecuteScalarAsync<int>(query, request, _session.Transaction);
            return request;
        }
        public async Task<bool> UpdateBookServices(ServiceDetail request)
        {
            var query = $@"UPDATE space_admin.space_service
                        SET 
                        service_count=@ServiceCount,
                        updated_by  =   @CreatedBy,
                        updated_date    =    now() at time zone 'utc'
                        WHERE service_id = @ServiceId and meeting_id = @MeetingId";
            var result = await _session.Connection.ExecuteAsync(query, new
            {
                request.ServiceId,
                request.ServiceCount,
                request.MeetingId,
                request.CreatedBy,
            }, _session.Transaction);
            if (result > 0)
            {
                return true;
            }
            else return false;
        }
        public async Task<bool> DeleteBookServices(int serviceId)
        {
            var query = "Delete from space_admin.space_service where service_id=@Id";
            var result = await _session.Connection.ExecuteAsync(query, new { Id = serviceId }, _session.Transaction);
            if (result > 0)
            {
                return true;
            }
            else return false;
        }

        #endregion



    }
}
