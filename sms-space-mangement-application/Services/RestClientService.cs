using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Services
{
    public class RestClientService
    {
        private readonly HttpClient _httpClient;

        // Replace with your actual base URL
       

        public RestClientService(string baseUrl)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }

        public async Task<List<CalendarDetails>> GetDataAsync(string endpoint, string queryParamKey, string queryParamValue)
        {
            try
            {
                // Build the request URL with the query parameter
                var url = $"{endpoint}?{queryParamKey}={queryParamValue}";

                // Make the HTTP GET request
                var response = await _httpClient.GetAsync(url);

                // Ensure the response status code is successful
                response.EnsureSuccessStatusCode();

                // Deserialize the response body to a list of response objects
                var data = await response.Content.ReadFromJsonAsync<List<CalendarDetails>>();

                return data;
            }
            catch (HttpRequestException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

    // Define the response object model based on the expected JSON structure
    public class CalendarDetails
    {
        public string sourceCalendarId { get; set; }
        public int calendarId { get; set; }
        public string description { get; set; }
    }
}
