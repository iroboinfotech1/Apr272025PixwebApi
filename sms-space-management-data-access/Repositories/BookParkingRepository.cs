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
    public class BookParkingRepository : IBookParkingRepository
    {
        private readonly DbSession _session;

        public BookParkingRepository(DbSession session)
        {
            _session = session;
        }


        #region == AddParking ==

        public async Task<List<Parkings>> GetBookParking(int meetingId)
        {
            var query = $@"Select * from space_admin.space_parking where meeting_id =@Id";
            var result = await _session.Connection.QueryAsync<Parkings>(query, new { Id = meetingId }, _session.Transaction);
            return result.ToList();
        }
        public async Task<IReadOnlyList<BookParking>> GetBookParking()
        {
            var query = "Select * from space_admin.space_parking";
            var result = await _session.Connection.QueryAsync<BookParking>(query, null, _session.Transaction);
            return result.ToList();
        }


        public async Task<Parkings> CreateBookParking(Parkings request)
        {
            var query = $@"INSERT INTO  space_admin.space_parking(meeting_id,participant_name,vechicle_number,slot_id,slot_name,created_by,created_date)
						VALUES (@MeetingId,@ParticipantName,@VechicleNumber,@SlotId,@SlotName,@CreatedBy,now() at time zone 'utc')";
            //RETURNING id


            await _session.Connection.ExecuteScalarAsync<int>(query, request, _session.Transaction);
            return request;
        }
        public async Task<bool> UpdateBookParking(BookParking request)
        {
            var query = $@"UPDATE space_admin.space_parking 
                        SET 
                        meeting_id = @MeetingRoomsId ,
                        slot_details = @SlotDetails ,
                        participant_name = @ParticipantName,
                        vechicle_number = @VechicleNumber,
                        updated_by  =   @CreatedBy,
                        updated_date    =    now() at time zone 'utc',
                        WHERE parking_id = @parking_id";
            var result = await _session.Connection.ExecuteAsync(query, new
            {
                ParkingId = request.ParkingId,
                MeetingRoomsId = request.MeetingRoomsId,
                SlotDetails = request.SlotDetails,
                ParticipantName = request.ParticipantName,
                VehicleNumber =  request.VechicleNumber,
                CreatedBy = request.CreatedBy,
            }, _session.Transaction);
            if (result > 0)
            {
                return true;
            }
            else return false;
        }
        public async Task<bool> DeleteBookParking(int parkingId)
        {
            var query = "Delete from space_admin.space_parking where parking_id=@Id";
            var result = await _session.Connection.ExecuteAsync(query, new { Id = parkingId }, _session.Transaction);
            if (result > 0)
            {
                return true;
            }
            else return false;
        }

        #endregion


      
    }
}
