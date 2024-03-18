using Hv.Sos100.DataService.Sync.Model;
using Hv.Sos100.Logger;
using Quartz;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace Hv.Sos100.DataService.Sync.Jobs
{
    public class EventStatisticJob : IJob
    {
        private readonly IHttpClientFactory _httpClientFactory;
        //private readonly string _baseURL = "https://informatik7.ei.hv.se/ProfilAPI/api/SignUps";
        private readonly string _baseURL2 = "https://informatik4.ei.hv.se/EVENTAPI2/api/Events";
        private readonly string _baseURL3 = "https://informatik7.ei.hv.se/ProfilAPI/api/Citizens";
        private readonly string _baseURL4 = "https://informatik6.ei.hv.se/statisticapi/api/EventStatistics/event/list";
        private readonly string _baseURL5 = "https://informatik2.ei.hv.se/OrganizerAPI/api/Organizers";
        private readonly string _baseURL6 = "https://informatik1.ei.hv.se/ActivityAPI/api/Categories";


        public EventStatisticJob(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            List<Event>? eventList = new();
            List<Citizen>? citizenList = new();
            List<EventStatistics>? eventStatisticsList = new();
            List<Organizer>? organizerList = new();
            List<Category>? categoryList = new();

            var activityClient = _httpClientFactory.CreateClient("activityapi");
            var eventClient = _httpClientFactory.CreateClient("eventapi");
            var profileClient = _httpClientFactory.CreateClient("profilapi");
            var statisticClient = _httpClientFactory.CreateClient("statisticapi");
            var organizerClient = _httpClientFactory.CreateClient("organizerapi");

            var logger = new LogService();

            try
            {
                HttpResponseMessage response = await eventClient.GetAsync("api/Events");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    eventList = JsonSerializer.Deserialize<List<Event>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            catch (Exception ex)
            {
                await logger.CreateLog("Activity Hv.Sos100.DataService.Sync", LogService.Severity.Error, ex.Message);
            }

            try
            {
                HttpResponseMessage response = await profileClient.GetAsync("api/Citizens");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    citizenList = JsonSerializer.Deserialize<List<Citizen>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }

            }
            catch (Exception ex)
            {
                await logger.CreateLog("Activity Hv.Sos100.DataService.Sync", LogService.Severity.Error, ex.Message);
            }

            try
            {
                HttpResponseMessage response = await organizerClient.GetAsync("api/Organizers");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    organizerList = JsonSerializer.Deserialize<List<Organizer>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }

            }
            catch (Exception ex)
            {
                await logger.CreateLog("Activity Hv.Sos100.DataService.Sync", LogService.Severity.Error, ex.Message);
            }

            try
            {
                HttpResponseMessage response = await activityClient.GetAsync("api/Categories");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    categoryList = JsonSerializer.Deserialize<List<Category>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            catch (Exception ex)
            {
                await logger.CreateLog("Activity Hv.Sos100.DataService.Sync", LogService.Severity.Error, ex.Message);
            }

            foreach (var eventItem in eventList)
            {
                var organizer = organizerList.FirstOrDefault(o => o.OrganizerID != null && o.OrganizerID == eventItem.OrganizerID);
                var category = categoryList.FirstOrDefault(c => c.CategoryID == eventItem.CategoryID);
                var totalSignups = citizenList.Count(c => c.EventList != null && c.EventList.Contains(eventItem.EventID));
                var maleSignups = citizenList.Count(c => c.EventList != null && c.EventList.Contains(eventItem.EventID) && c.Gender == "Male");
                var femaleSignups = citizenList.Count(c => c.EventList != null && c.EventList.Contains(eventItem.EventID) && c.Gender == "Female");
                var ageBelow16Signups = citizenList.Count(c => c.EventList != null && c.EventList.Contains(eventItem.EventID) && c.Age < 16);
                var age16To30Signups = citizenList.Count(c => c.EventList != null && c.EventList.Contains(eventItem.EventID) && c.Age >= 16 && c.Age <= 30);
                var age31To50Signups = citizenList.Count(c => c.EventList != null && c.EventList.Contains(eventItem.EventID) && c.Age > 30 && c.Age <= 50);
                var ageAbove50Signups = citizenList.Count(c => c.EventList != null && c.EventList.Contains(eventItem.EventID) && c.Age > 50);

                eventStatisticsList.Add(
                    new EventStatistics
                    {
                        EventID = eventItem.EventID,
                        UserID = organizer.UserID,
                        Category = category.Name,
                        TimeStamp = DateTime.Now,
                        TotalSignups = totalSignups,
                        FemaleSignups = femaleSignups,
                        MaleSignups = maleSignups,
                        AgeBelow16Signups = ageBelow16Signups,
                        Age16To30Signups = age16To30Signups,
                        Age31To50Signups = age31To50Signups,
                        AgeAbove50Signups = ageAbove50Signups
                    });
            }
            try
            {
                var result = await statisticClient.PostAsJsonAsync("api/EventStatistics/event/list", eventStatisticsList);

            }
            catch (Exception ex)
            {
                await logger.CreateLog("Activity Hv.Sos100.DataService.Sync", LogService.Severity.Error, ex.Message);
            }



        }
    }
}