using Hv.Sos100.DataService.Sync.Model;
using Hv.Sos100.Logger;
using Quartz;
using System.Text.Json;

namespace Hv.Sos100.DataService.Sync.Jobs
{
    public class EventStatisticJob : IJob
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly LogService _logger = new();

        public EventStatisticJob(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            List<Event>? eventList = await GetEvents();
            if(eventList == null)
            {
                await _logger.CreateLog("DataService.Statistics.Sync.EventStatisticJob.Execute", LogService.Severity.Warning, "GetEvents returns null");
                return;
            }

            List<Citizen>? citizenList = await GetCitizens();
            if(citizenList == null)
            {
                await _logger.CreateLog("DataService.Statistics.Sync.EventStatisticJob.Execute", LogService.Severity.Warning, "GetCitizens returns null");
                return;
            }

            List<Organizer>? organizerList = await GetOrganizers();
            if(organizerList == null)
            {
                await _logger.CreateLog("DataService.Statistics.Sync.EventStatisticJob.Execute", LogService.Severity.Warning, "GetOrganizers returns null");
                return;
            }


            List<Statistics.Api.Models.EventStatistics>? eventStatisticsList = new();
            foreach (var eventItem in eventList)
            {
                var organizer = organizerList.FirstOrDefault(o => o.OrganizerID != null && o.OrganizerID == eventItem.OrganizerID);
                var totalSignups = citizenList.Count(c => c.EventList != null && c.EventList.Contains(eventItem.EventID));
                var maleSignups = citizenList.Count(c => c.EventList != null && c.EventList.Contains(eventItem.EventID) && c.Gender == "Man");
                var femaleSignups = citizenList.Count(c => c.EventList != null && c.EventList.Contains(eventItem.EventID) && c.Gender == "Kvinna");
                var ageBelow16Signups = citizenList.Count(c => c.EventList != null && c.EventList.Contains(eventItem.EventID) && c.Age < 16);
                var age16To30Signups = citizenList.Count(c => c.EventList != null && c.EventList.Contains(eventItem.EventID) && c.Age >= 16 && c.Age <= 30);
                var age31To50Signups = citizenList.Count(c => c.EventList != null && c.EventList.Contains(eventItem.EventID) && c.Age > 30 && c.Age <= 50);
                var ageAbove50Signups = citizenList.Count(c => c.EventList != null && c.EventList.Contains(eventItem.EventID) && c.Age > 50);

                eventStatisticsList.Add(
                    new Statistics.Api.Models.EventStatistics
                    {
                        EventID = eventItem.EventID,
                        UserID = organizer.UserID,
                        Name = eventItem.Name,
                        CategoryID = eventItem.CategoryID,
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

            await PostEventStatistics(eventStatisticsList);
        }

        private async Task<List<Event>?> GetEvents()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("eventapi");
                HttpResponseMessage response = await client.GetAsync("api/Events");

                string content = await response.Content.ReadAsStringAsync();
                var events = JsonSerializer.Deserialize<List<Event>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return events;
            }
            catch (Exception ex)
            {
                await _logger.CreateLog("DataService.Statistics.Sync.EventStatisticJob.GetEvents", ex);
                return null;
            }
        }

        private async Task<List<Citizen>?> GetCitizens()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("profilapi");
                HttpResponseMessage response = await client.GetAsync("api/Citizens");

                string content = await response.Content.ReadAsStringAsync();
                var citizens = JsonSerializer.Deserialize<List<Citizen>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return citizens;
            }
            catch (Exception ex)
            {
                await _logger.CreateLog("DataService.Statistics.Sync.EventStatisticJob.GetCitizens", ex);
                return null;
            }
        }

        private async Task<List<Organizer>?> GetOrganizers()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("organizerapi");
                HttpResponseMessage response = await client.GetAsync("api/Organizers");

                string content = await response.Content.ReadAsStringAsync();
                var organizer = JsonSerializer.Deserialize<List<Organizer>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return organizer;
            }
            catch (Exception ex)
            {
                await _logger.CreateLog("DataService.Statistics.Sync.EventStatisticJob.GetOrganizers", ex);
                return null;
            }
        }

        private async Task PostEventStatistics(List<Statistics.Api.Models.EventStatistics> eventStatisticsList)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("statisticapi");
                var response = await client.PostAsJsonAsync("api/EventStatistics/event/list", eventStatisticsList);
                if (!response.IsSuccessStatusCode)
                {
                    await _logger.CreateLog("DataService.Statistics.Sync.EventStatisticJob.PostEventStatistics", LogService.Severity.Error, "Post to EventStatistics api creates error");
                }
            }
            catch (Exception ex)
            {
                await _logger.CreateLog("DataService.Statistics.Sync.EventStatisticJob.PostEventStatistics", ex);
            }
        }

    }
}