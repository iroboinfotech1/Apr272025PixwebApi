using Newtonsoft.Json;
using sms.space.management.domain.Entities.BookMeeting;
using sms.space.management.domain.Entities.Spaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static QRCoder.PayloadGenerator;

namespace sms.space.management.application.Services
{
    public class EventServices
    {

        public async Task<string?> CreateMeeting(BookMeetingDetails request)
        {
            BookEvent bookEvent = new BookEvent();
            bookEvent.connectorName = request.Meeting.ConnectorId;
            bookEvent.calendarId = request.Meeting.CalendarId;
            bookEvent.OrgId = request.Meeting.OrgId;
            bookEvent.WorkspaceId = request.Meeting.SpaceId;
            bookEvent.Event = new Event();
            Event bookEvent2 = new Event()
            {
                startTime = request.Meeting.StartDateTime,
                endTime = request.Meeting.EndDateTime,
                summary = request.Meeting.MeetingName,
                description = (!string.IsNullOrEmpty(request?.Meeting?.Notes))? request.Meeting.Notes : request.Meeting.MeetingName,
                TimeZone = request.Meeting.TimeZone,
                status = "Pending",
                sourceEventId = ""
            };
            bookEvent.Event = bookEvent2;
            if (request?.Meeting?.Participants != null)
            {
                bookEvent2.attendees = new List<Attendee>();
                foreach (var item in request?.Meeting?.Participants)
                {
                    Attendee attendee = new Attendee() { email = item, optional = false, isOrganizer = false };
                    bookEvent2.attendees.Add(attendee);
                }
            }
            else
            {
                bookEvent2.attendees = new List<Attendee>();
                Attendee attendee = new Attendee() { email = "Booked by System", optional = false, isOrganizer = true };
                bookEvent2.attendees.Add(attendee);
            }

            await Callservices(bookEvent,"create");
           
            return bookEvent?.SourceCalendarId;
        }

        public async Task<string?> EditMeeting(BookMeetingDetails request)
        {
            BookEvent bookEvent = new BookEvent();
            bookEvent.connectorName = request.Meeting.ConnectorId;
            bookEvent.calendarId = request.Meeting.CalendarId;
            bookEvent.OrgId = request.Meeting.OrgId;
            bookEvent.WorkspaceId = request.Meeting.SpaceId;
            bookEvent.Event = new Event();
            bookEvent.Event.sourceEventId=request.Meeting.SourceEventId;
            Event bookEvent2 = new Event()
            {
                startTime = request.Meeting.StartDateTime,
                endTime = request.Meeting.EndDateTime,
                summary = request.Meeting.MeetingName,
                description = (!string.IsNullOrEmpty(request?.Meeting?.Notes)) ? request.Meeting.Notes : request.Meeting.MeetingName,
                TimeZone = request.Meeting.TimeZone,
                status = "Pending",
                sourceEventId = request.Meeting.SourceEventId
            };
            bookEvent.Event = bookEvent2;
            if (request?.Meeting?.Participants != null)
            {
                bookEvent2.attendees = new List<Attendee>();
                foreach (var item in request?.Meeting?.Participants)
                {
                    Attendee attendee = new Attendee() { email = item, optional = false, isOrganizer = false };
                    bookEvent2.attendees.Add(attendee);
                }
            }
            else
            {
                bookEvent2.attendees = new List<Attendee>();
                Attendee attendee = new Attendee() { email = "Booked by System", optional = false, isOrganizer = true };
                bookEvent2.attendees.Add(attendee);
            }
            await Callservices(bookEvent, "update");
            return bookEvent?.SourceCalendarId;
        }


        public async Task<string?> CancelMeeting(BookMeetingDetails request)
        {
            BookEvent bookEvent = new BookEvent();
            bookEvent.connectorName = request.Meeting.ConnectorId;
            bookEvent.calendarId = request.Meeting.CalendarId;
            bookEvent.eventId = request.Meeting.SourceEventId;
            await Callservices(bookEvent, "update");
            return bookEvent?.Status;
        }
        public async Task<string?> GetCalendarIdForPixRoom(BookMeetingDetails request)
        {
            BookEvent bookEvent = new BookEvent();
            bookEvent.connectorName = request.Meeting.ConnectorId;
            bookEvent.calendarId = request.Meeting.CalendarId;
            bookEvent.calendarId = request.Meeting.CalendarId;
            bookEvent.OrgId = request.Meeting.OrgId;
            bookEvent.Event = new Event();
            Event bookEvent2 = new Event()
            {
                startTime = request.Meeting.StartDateTime,
                endTime = request.Meeting.EndDateTime,
                summary = request.Meeting.MeetingName,
                description = request.Meeting.Notes,
                TimeZone = request.Meeting.TimeZone,
                status = "Pending",
                sourceEventId = ""
            };
            bookEvent.Event = bookEvent2;
            if (request?.Meeting?.Participants != null)
            {
                bookEvent2.attendees = new List<Attendee>();
                foreach (var item in request?.Meeting?.Participants)
                {
                    Attendee attendee = new Attendee() { email = item, optional = false, isOrganizer = false };
                    bookEvent2.attendees.Add(attendee);
                }
            }
            await Callservices(bookEvent, "update");

            return bookEvent?.Status;
        }


        public async Task<string?> Getusers(BookMeetingDetails request)
        {
            BookEvent bookEvent = new BookEvent();
            bookEvent.connectorName = request.Meeting.ConnectorId;
            bookEvent.calendarId = request.Meeting.CalendarId;
            bookEvent.Event = new Event();
            Event bookEvent2 = new Event()
            {
                startTime = request.Meeting.StartDateTime,
                endTime = request.Meeting.EndDateTime,
                summary = request.Meeting.MeetingName,
                description = request.Meeting.Notes,
                status = "Pending",
                sourceEventId = ""
            };
            bookEvent.Event = bookEvent2;
            bookEvent2.attendees = new List<Attendee>();
            Attendee attendee = new Attendee() { email = "PradeepG@1gdsw4.onmicrosoft.com", optional = false, isOrganizer = false };
            Attendee attendee1 = new Attendee() { email = "PattiF@1gdsw4.onmicrosoft.com", optional = false, isOrganizer = false };
            bookEvent2.attendees.Add(attendee);
            bookEvent2.attendees.Add(attendee1);

            await Callservices(bookEvent,"update");

            return bookEvent?.Status;
        }

        public async Task<string> Callservices(BookEvent eventdetails, string action)
        {

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Define the base address of the REST service
                    client.BaseAddress = new Uri("https://demo.pixelkube.io/");

                    // Serialize the object to JSON
                    string json = JsonConvert.SerializeObject(eventdetails);
                    string url = string.Empty;

                    // Convert the JSON string to a StringContent object
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    if (action != "update")
                    {
                        url = "/api/pixconnectors/event/createevent";
                    }
                    else
                    {
                        url = "/api/pixconnectors/event/updateevent";
                    }
                    // Call the REST service asynchronously using a POST request
                    HttpResponseMessage response = await client.PostAsync(url, content);


                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the content of the response
                        string responseBody = await response.Content.ReadAsStringAsync();
                        if (eventdetails != null)
                        {
                            eventdetails.Status = "Success";
                            eventdetails.SourceCalendarId = responseBody;
                        }
                        return responseBody;
                    }
                    else
                    {
                        Console.WriteLine($"Failed to call the API. Status code: {response.StatusCode}");
                        return response.StatusCode.ToString();
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        public async Task<string> CallConnector(BookEvent eventdetails)
        {

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Define the base address of the REST service
                    client.BaseAddress = new Uri("https://demo.pixelkube.io/");

                    // Serialize the object to JSON
                    string json = JsonConvert.SerializeObject(eventdetails);

                    // Convert the JSON string to a StringContent object
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    // Call the REST service asynchronously using a POST request
                    HttpResponseMessage response = await client.PostAsync("/api/pixconnectors/event/createevent", content);

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the content of the response
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return responseBody;
                    }
                    else
                    {
                        Console.WriteLine($"Failed to call the API. Status code: {response.StatusCode}");
                        return response.StatusCode.ToString();
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        internal async Task CreateMeetingAsync(BookMeetingDetails request)
        {
            throw new NotImplementedException();
        }
    }

    public class BookEvent
    {
        public string? connectorName { get; set; }
        public string? calendarId { get; set; }
        public Event? Event { get; set; }
        public string? Status { get; set; }
        public long WorkspaceId { get; set; }
        public long OrgId { get; set; }
        public string SourceCalendarId { get; set; }
        public string eventId { get; set; }

    }
    public class Event
    {
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public string? summary { get; set; }
        public string? description { get; set; }
        public string? status { get; set; }
        public string? sourceEventId { get; set; } = "AAMkADk0ZDBmZDZjLWVmM2MtNGEyYy1hZWNkLTE3ZjM4ZWYxN2VmMQBGAAAAAABPGuN2HzbZSIlDTdX2Xcy8BwAxN4j3Loz8Rq7rEq6a6lIQAAAAAAENAAAxN4j3Loz8Rq7rEq6a6lIQAACb9V0cAAA=";
        public string? TimeZone { get; set; }
        public List<Attendee>? attendees { get; set; }

    }

    public class Attendee
    {
        public string? email { get; set; }
        public bool optional { get; set; }
        public bool isOrganizer { get; set; }
    }



}
