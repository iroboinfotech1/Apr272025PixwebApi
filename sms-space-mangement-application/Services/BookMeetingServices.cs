using Microsoft.AspNetCore.Mvc;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.domain.Entities;
using sms.space.management.domain.Entities.BookDesk;
using sms.space.management.domain.Entities.BookMeeting;
using sms.space.management.domain.Entities.Building;
using sms.space.management.domain.Entities.Spaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Services
{
    public class BookMeetingServices : IBooKMeetingService
    {   
        private readonly HttpClient _client;
        private readonly IBookMeetingRepository _repository;
        public BookMeetingServices(IBookMeetingRepository repository)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://demo.pixelkube.io/auth/admin/realms/master/");
            _repository = repository;
        }

        public async Task<List<BookedMeetingDetail>> GetBookMeeting(int spaceId, DateTime startDate, DateTime endDate)
        {
           var services = await _repository.GetBookMeeting(spaceId, startDate, endDate);
           List<BookedMeetingDetail> tempbookeeddetails = services.ToList();
           if(tempbookeeddetails!=null && tempbookeeddetails.Count > 0) 
           {
                foreach (var BookedMeetingDetail in tempbookeeddetails)
                {
                    if (BookedMeetingDetail != null && BookedMeetingDetail.SpaceResources == null)
                    {
                        var resources = await _repository.GetSpaceResourcesforBookedMeeting(BookedMeetingDetail.SpaceId);
                        if (resources != null && resources.Count > 0)
                        {
                            List<BookedMeetingDetail> BookedMeetingDetaillist = tempbookeeddetails.FindAll(x => x.SpaceId == BookedMeetingDetail.SpaceId);
                            BookedMeetingDetaillist.ForEach(x => { x.SpaceResources = new List<spaceresources>(resources); });
                        }
                    }
                }
           }
           return services;
        }
        [HttpGet]
        public async Task<string> GetusersViaRestAPI()
        {
            
            try
            {
                string accesstoken = "eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJxRWt2RUxRVE0tdUEza0RnSjhuREJXTVNyTU1TNktNdVNybnZCVFg1S1FzIn0.eyJleHAiOjE3MTc5ODcyMjEsImlhdCI6MTcxNzk1MTIyMSwianRpIjoiM2JjY2FjMGMtOTdiOC00YWQwLTk1NWEtMGJkYzM3MmIyZGZhIiwiaXNzIjoiaHR0cHM6Ly9kZW1vLnBpeGVsa3ViZS5pby9hdXRoL3JlYWxtcy9tYXN0ZXIiLCJzdWIiOiJmYjJlNzUyYy1mZTM1LTRiOTMtYjIxNS1kYjVlMzMzZWRiZWIiLCJ0eXAiOiJCZWFyZXIiLCJhenAiOiJhZG1pbi1jbGkiLCJzZXNzaW9uX3N0YXRlIjoiZDIwOWU3YmQtOWQ5OC00ZWFmLWE5ZGItOTliMTQ0MTRlNTY4IiwiYWNyIjoiMSIsInNjb3BlIjoicHJvZmlsZSBlbWFpbCIsInNpZCI6ImQyMDllN2JkLTlkOTgtNGVhZi1hOWRiLTk5YjE0NDE0ZTU2OCIsImVtYWlsX3ZlcmlmaWVkIjpmYWxzZSwibmFtZSI6IkFkbWluc3RyYXRvciBLZXlDbG9hayIsInByZWZlcnJlZF91c2VybmFtZSI6ImFkbWluIiwiZ2l2ZW5fbmFtZSI6IkFkbWluc3RyYXRvciIsImZhbWlseV9uYW1lIjoiS2V5Q2xvYWsiLCJlbWFpbCI6ImFkbWluQGdtYWlsLmNvbSJ9.e7ama9eqviO29yUY2Zq8w2k_mwWZLEMgj4VSEMWuElKRx3PjyVv8KXeH0X6094E9zV6paIOIH10Og3ZZtncV3-BD34c6zfzxWF6kgdUL3dxYRnfwxWZwc1sQxM7nZcdlRhovh-5K0DZdM-HT4ASlYY5HAGJeDpdwoYYGwjJpjRKJhX_TeiS39mL_wpGErIMtQ0OIhNDHK5Zff1uIPn7bfjOr7pYRGrawd-NnFaKAn8j759IWB7W8AaUD5yo125U0MyJPylC5JgyYYclAfmqEMztvObxzsCMng8ibVk-vEAfEezs4LdDLiL5KgqEpizbN9Wy5LhHPPKu55gUeGIrvKA";
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);

                HttpResponseMessage response = await _client.GetAsync("users");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine($"Failed to retrieve users. Status code: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }

        }
        public async Task<BookMeetingDetails> GetBookMeeting(int meetingId)
        {
            var services = await _repository.GetBookMeeting(meetingId);
            
            return services;
        }

        public async Task<BookMeetingDetails> CreateMeeting(BookMeetingDetails request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Meeting == null) throw new ArgumentNullException(nameof(request.Meeting));

            var services1 = await _repository.GetConnectorAndCalendarID(request.Meeting.OrgId, request.Meeting.BuildingId, request.Meeting.SpaceId);
            if (services1 == null) return new BookMeetingDetails();

            List<connectordetails> connectordetails = new List<connectordetails>(services1);
            request.Meeting.ConnectorId = connectordetails.FirstOrDefault()?.connectorid.FirstOrDefault()?.ToString();
            request.Meeting.CalendarId = connectordetails.FirstOrDefault()?.calendarId.FirstOrDefault()?.ToString();

            Task<string?> sourcecalendarIdTask;
            if (request.Meeting.Action != "update" && string.IsNullOrEmpty(request.Meeting.SourceEventId))
            {
                sourcecalendarIdTask = new EventServices().CreateMeeting(request);
                request.Meeting.SourceEventId = sourcecalendarIdTask?.Result;
                request.SourceEventId = sourcecalendarIdTask?.Result;
            }
            else
            {
                if (string.IsNullOrEmpty(request.Meeting.SourceEventId))
                {
                    var sourceeventiddetails = await _repository.GetSourceEventId(request);
                    request.Meeting.SourceEventId = sourceeventiddetails.FirstOrDefault()?.sourceeventid;
                }
                sourcecalendarIdTask = new EventServices().EditMeeting(request);
            }

            var sourcecalendarId = await sourcecalendarIdTask;
            
            if (!string.IsNullOrEmpty(request.Meeting.SourceEventId) && request.Meeting.Action != "update")
            {
                if(request.Meeting != null && request.Meeting.Participants!=null && request.Meeting.Participants.Length >0 )
                {
                    request.Meeting.NoOfAttendees = request.Meeting.Participants.Length;
                }
                var services= await _repository.CreateBookMeeting(request);
                request.Status = "Success";
                return services;
            }
            else if(!string.IsNullOrEmpty(request.Meeting.SourceEventId) && request.Meeting.Action == "update")
            {
                bool updateserives = await _repository.UpdateBookMeeting(request);
                request.Status = "Success";
                return updateserives ? request : new BookMeetingDetails();
            }
            return new BookMeetingDetails();
        }

        public async Task<BookDeskDetails> CreateDeskBooking(BookDeskDetails request)
        {
            var services = await _repository.CreateBookDesk(request);
            return services;
        }

        public Task<bool> UpdateMeeting(BookMeetingDetails request)
        {
            var isupdated = _repository.UpdateBookMeeting(request);
            return isupdated;
        }

        public async Task<bool> DeleteMeeting(int meetingId)
        {
            var isdeleted = await _repository.DeleteBookMeeting(meetingId);
            return isdeleted;
        }

        public Task<List<spaceresources>> GetSpaceResourcesforBookedMeeting(int spaceid)
        {
            throw new NotImplementedException();
        }

        public Task<List<connectordetails>> GetConnectorAndCalendarID(int orgid, int buildingid, int floorid)
        {
            throw new NotImplementedException();
        }
    }
}