using Dapper;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.data.access.BusinessLogic;
using sms.space.management.domain.Entities;
using sms.space.management.domain.Entities.BookDesk;
using sms.space.management.domain.Entities.BookMeeting;
using sms.space.management.domain.Entities.BookRoom;
using sms.space.management.domain.Entities.Spaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace sms.space.management.data.access.Repositories
{
    public class BookMeetingRepository : IBookMeetingRepository
    {       
        private readonly DbSession _session;

        public BookMeetingRepository(DbSession session)
        {
            _session = session;
        }


        public async Task<List<BookedMeetingDetail>> GetBookMeeting(int spaceId, DateTime startDate, DateTime endDate)
        {
                var queryMeeting = @"select distinct sm.space_id, sm.meeting_id as meetingId,sm.meeting_name as meetingName, sm.meeting_type as meetingType,
                                smas.spacealiasname as spaceName, bm.building_name as buildingName,bm.address as address, f.floor_name as floorName,om.image,
                                om.org_name as orgName, sm.start_date as startDate, sm.end_date as endDate, 
	                            sm.no_of_attendees as noOfAttendees,sm.participants as participants,sm.notes as notes, smas.space_image as spaceImage
                                from space_admin.space_meeting sm 
                                inner join space_admin.space_master smas on sm.space_id=smas.space_id 
                                inner join space_admin.buildings_master bm on sm.building_id = bm.building_id 
                                inner join space_admin.floor f on sm.floor_id=f.floor_id 
                                inner join space_admin.organisation_master om on sm.org_id = om.org_id";

                if (spaceId != 0) {
                    queryMeeting = queryMeeting + " where sm.space_id= " + spaceId + " AND sm.start_date between '" + startDate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + "' and '" + endDate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + "'";
                }
                else
                {
                    queryMeeting = queryMeeting + " where sm.start_date > CURRENT_DATE";
                }
	                            
              var resultMeeting = await _session.Connection.QueryAsync<BookedMeetingDetail>(queryMeeting, null, _session.Transaction);
              return resultMeeting.ToList();
        }


        public async Task<List<spaceresources>> GetSpaceResourcesforBookedMeeting(int spaceid)
        {
            var QueryResources = @"select distinct sm.space_id,sr.resource_id as resourceId,
	                               sr.resource_count as resourceCount,
	                               fr.icon as resourceIcon,
	                               fr.name as resourceName from space_admin.space_meeting sm
                                   inner join space_admin.space_resource sr on sm.space_id = sr.space_id 
                                   inner join space_admin.facility_resources fr on sr.resource_id = fr.resource_id
                                   where sm.space_id in(@Id)";
            var resultMeeting = await _session.Connection.QueryAsync<spaceresources>(QueryResources, new { Id = spaceid }, _session.Transaction);
            return resultMeeting.ToList();
        }


        public async Task<BookMeetingDetails> GetBookMeeting(int meetingId)
        {
            BookMeetingDetails bookMeetingDetails = new BookMeetingDetails();
            var queryMeeting = $@"Select * from space_admin.space_meeting where   meeting_id =@Id";
            var resultMeeting = await _session.Connection.QueryAsync<Meeting>(queryMeeting, new { Id = meetingId }, _session.Transaction);

            if (resultMeeting != null)
            {
                bookMeetingDetails.Meeting = resultMeeting.FirstOrDefault();
                var queryService = $@"Select * from space_admin.space_service where meeting_id =@Id";
                var resultService = await _session.Connection.QueryAsync<List<ServiceDetail>>(queryService, new { Id = bookMeetingDetails.Meeting.MeetingId }, _session.Transaction);
                if (resultService != null)
                {
                    bookMeetingDetails.Services = resultService.FirstOrDefault();
                }

                var queryParkings = $@"Select * from space_admin.space_parking where meeting_id =@Id";
                var resultParkings = await _session.Connection.QueryAsync<Parkings>(queryParkings, new { Id = bookMeetingDetails.Meeting.MeetingId }, _session.Transaction);
                if (resultParkings != null)
                {
                    bookMeetingDetails.Parkings = resultParkings.ToList();
                }
            }


            return bookMeetingDetails;
        }
        //public Task<List<connectordetails>> GetConnectorAndCalendarID(int orgid, int buildingid, string floorid)
        //{
        //    throw new NotImplementedException();
        //}
        public async Task<List<connectordetails>> GetConnectorAndCalendarID(int orgid, int buildingid, int floorid)
        {
            try
            {
                var query = @"SELECT spacealiasname,mappedcalendar_ids as calendarId, mappedconnector_ids as connectorid
	                           FROM space_admin.space_master where org_id=@org_id and building_id=@building_id and space_id=@space_id
                              ORDER BY space_id ASC";

                var result = await _session.Connection.QueryAsync<connectordetails>(
                    query,
                       param: new { org_id = orgid, building_id = buildingid, space_id = floorid },
                       transaction: _session.Transaction);
                return result.Distinct().ToList();
            }
            catch (Exception ex)
            {
                return new List<connectordetails>();
                //throw;
            }
        }

        public async Task<List<EventDetails>> GetSourceEventId(BookMeetingDetails request)
        {
            try
            {
                var query = @" SELECT meeting_id,sourceeventid
	                        from space_admin.space_meeting 
	                       where org_id=@org_id and building_id=@building_id and space_id=@space_id and meeting_id=@meeting_id";

                var result = await _session.Connection.QueryAsync<EventDetails>(
                    query,
                       param: new { org_id = request.Meeting.OrgId, building_id = request.Meeting.BuildingId, space_id = request.Meeting.SpaceId, meeting_id = request.Meeting.MeetingId},
                       transaction: _session.Transaction);
                return result.Distinct().ToList();
            }
            catch (Exception ex)
            {
                return new List<EventDetails>();
                //throw;
            }
        }
        public async Task<BookMeetingDetails> CreateBookMeeting(BookMeetingDetails request)
        {
            var query = $@"INSERT INTO  space_admin.space_meeting(
                            all_days,reminder,start_date,end_date,org_id,building_id,floor_id,no_of_attendees,meeting_name,space_id,meeting_type,space_type,participants,sourceeventid,notes,created_by,created_date)
						VALUES (@Alldays,@Reminder,@StartDateTime,@EndDateTime,@OrgId,@BuildingId,@FloorId,@NoOfAttendees,@MeetingName,@SpaceId,@MeetingType,1,@Participants,@SourceEventId,@Notes,@CreatedBy,now() at time zone 'utc')
						RETURNING  meeting_id";

            int bookingId = await _session.Connection.ExecuteScalarAsync<int>(query, request.Meeting, _session.Transaction);
            request.Meeting.MeetingId = bookingId;
            request.Meeting.ReferenceNumber = $"BRID{(100000 + bookingId).ToString().Substring(1)}";

            await BookServices(request.Services, bookingId);
            await BookParking(request.Parkings, bookingId);

            return request;
        }

        public async Task<BookDeskDetails> CreateBookDesk(BookDeskDetails request)
        {
            var query = $@"INSERT INTO  space_admin.space_meeting(
                            all_days,reminder,start_date,end_date,org_id,building_id,floor_id,no_of_attendees,meeting_name,space_id,meeting_type,space_type)
						VALUES (@Alldays,@Reminder,@StartDateTime,@EndDateTime,@OrgId,@BuildingId,@FloorId,1,@MeetingName,@DeskId,2,2)
						RETURNING  meeting_id";

            int bookingId = await _session.Connection.ExecuteScalarAsync<int>(query, request.Desk, _session.Transaction);
            request.Desk.MeetingId = bookingId;
            request.Desk.ReferenceNumber = $"BDID{(100000 + bookingId).ToString().Substring(1)}";

            await BookServices(request.Services, bookingId);
            await BookParking(request.Parkings, bookingId);

            return request;
        }

        private async Task BookParking(List<Parkings>? parkings, int bookingId)
        {
            if (parkings != null)
            {
                foreach (Parkings item in parkings)
                {
                    item.MeetingId = bookingId;
                    var queryParking = $@"INSERT INTO  space_admin.space_parking(meeting_id,participant_name,vechicle_number,slot_id,slot_name)
						VALUES (@MeetingId,@ParticipantName,@VechicleNumber,@SlotId,@SlotName)";
                    await _session.Connection.ExecuteScalarAsync<int>(queryParking, item, _session.Transaction);
                }
            }
        }

        private async Task BookServices(List<ServiceDetail>? Services, int bookingId)
        {
            if (Services != null && Services.Count > 0)
            {
                foreach (ServiceDetail service in Services)
                {
                    service.MeetingId = bookingId;

                    var queryService = $@"INSERT INTO  space_admin.space_service(meeting_id,service_id,service_count)
						VALUES (@MeetingId,@ServiceId,@ServiceCount)
                    ";
                    await _session.Connection.ExecuteScalarAsync<int>(queryService, service, _session.Transaction);
                };
            }
        }

        public async Task<bool> UpdateBookMeeting(BookMeetingDetails request)
        {
            if (request.Meeting != null)
            {

                var queryMeeting = $@"UPDATE space_admin.space_meeting 
                        SET 
                        all_days = @Alldays,
                        reminder = @Reminder ,
                        start_date = @StartDateTime,
                        end_date = @EndDateTime,
                        meeting_tiltle = @MeetingName ,
                        participants = @Participants ,
                        notes  = @Notes,
                        meeting_type  = @MeetingType,
                        updated_by  =   @CreatedBy,
                        updated_date    =    now() at time zone 'utc',
                        WHERE meeting_id = @MeetingId";
                var result = await _session.Connection.ExecuteAsync(queryMeeting, new
                {
                    request.Meeting.Alldays,
                    request.Meeting.Reminder,
                    request.Meeting.StartDateTime,
                    request.Meeting.EndDateTime,
                    request.Meeting.MeetingName,
                    request.Meeting.Participants,
                    request.Meeting.Notes,
                    request.Meeting.MeetingType,
                    request.Meeting.MeetingId,
                    request.Meeting.CreatedBy,
                }, _session.Transaction);

            }
            if (request.Parkings != null && request.Parkings.Count > 0)
            {
                foreach (var parking in request.Parkings)
                {
                    var queryParking = $@"UPDATE space_admin.space_parking 
                        SET 
                        meeting_id = @MeetingId,
                        participant_name = @ParticipantName ,
                        vechicle_number = @VechicleNumber,
                        slot_number = @SlotNumber 
                        WHERE parking_id = @ParkingId";
                    var serviceResult = await _session.Connection.ExecuteAsync(queryParking, new
                    {
                        ParkingId = parking.ParkingId,
                        MeetingId = parking.MeetingId,
                        VechicleNumber = parking.VechicleNumber,
                        ParticipantName = parking.ParticipantName,
                        SlotNumber = parking.SlotId


                    }, _session.Transaction);
                }

            }
            return true;
        }
        public async Task<bool> DeleteBookMeeting(int meetingId)
        {
            if (meetingId > 0)
            {
                var queryService = "Delete from space_admin.space_service where  meeting_id=@Id";
                var resultService = await _session.Connection.ExecuteAsync(queryService, new { Id = meetingId }, _session.Transaction);

                var queryParking = "Delete from space_admin.space_parking where  meeting_id=@Id";
                var resultParking = await _session.Connection.ExecuteAsync(queryParking, new { Id = meetingId }, _session.Transaction);
                var queryMeeting = "Delete from space_admin.space_meeting where  meeting_id=@Id";
                var resultMeeting = await _session.Connection.ExecuteAsync(queryMeeting, new { Id = meetingId }, _session.Transaction);
                return true;
            }
            else
                return false;
        }

       
    }
}
