﻿using Hv.Sos100.DataService.Sync.Model;
using Hv.Sos100.Logger;
using Quartz;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace Hv.Sos100.DataService.Sync.Jobs
{
    public class EventStatisticJob : IJob
    {
        private readonly HttpClient _httpClient;
        //private readonly string _baseURL = "https://informatik7.ei.hv.se/ProfilAPI/api/SignUps";
        private readonly string _baseURL2 = "https://informatik4.ei.hv.se/EVENTAPI2/api/Events";
        private readonly string _baseURL3 = "https://informatik7.ei.hv.se/ProfilAPI/api/Citizens";
        private readonly string _baseURL4 = "https://informatik6.ei.hv.se/statisticapi/api/EventStatistics/list";
        private readonly string _baseURL5 = "https://informatik2.ei.hv.se/OrganizerAPI/api/Organizers";
        private readonly string _baseURL6 = "https://informatik1.ei.hv.se/ActivityAPI/api/Categories";


        public EventStatisticJob(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            List<Event> eventList = new List<Event>();
            List<Citizen> citizenList = new List<Citizen>();
            List<EventStatistics> eventStatisticsList = new List<EventStatistics>();
            List<Organizer> organizerList = new List<Organizer>();
            List<Category> categoryList = new List<Category>();

            try
            {
                _httpClient.BaseAddress = new Uri(_baseURL2);

                HttpResponseMessage response = await _httpClient.GetAsync("");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    eventList = JsonSerializer.Deserialize<List<Event>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }

            }
            catch (Exception ex)
            {
                var logger = new LogService();

                await logger.CreateLog("Activity Hv.Sos100.DataService.Sync", LogService.Severity.Error, ex.Message);
            }

            try
            {
                _httpClient.BaseAddress = new Uri(_baseURL3);

                HttpResponseMessage response = await _httpClient.GetAsync("");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    citizenList = JsonSerializer.Deserialize<List<Citizen>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }

            }
            catch (Exception ex)
            {
                var logger = new LogService();

                await logger.CreateLog("Activity Hv.Sos100.DataService.Sync", LogService.Severity.Error, ex.Message);
            }

            try
            {
                _httpClient.BaseAddress = new Uri(_baseURL5);

                HttpResponseMessage response = await _httpClient.GetAsync("");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    organizerList = JsonSerializer.Deserialize<List<Organizer>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }

            }
            catch (Exception ex)
            {
                var logger = new LogService();

                await logger.CreateLog("Activity Hv.Sos100.DataService.Sync", LogService.Severity.Error, ex.Message);
            }

            try
            {
                _httpClient.BaseAddress = new Uri(_baseURL6);

                HttpResponseMessage response = await _httpClient.GetAsync("");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    categoryList = JsonSerializer.Deserialize<List<Category>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            catch (Exception ex)
            {
                var logger = new LogService();

                await logger.CreateLog("Activity Hv.Sos100.DataService.Sync", LogService.Severity.Error, ex.Message);
            }

            foreach (var eventItem in eventList)
            {
                var organizer = organizerList.FirstOrDefault(o => o.OrganizerID == eventItem.OrganizerID);
                var category = categoryList.FirstOrDefault(c => c.CategoryID == eventItem.CategoryID);
                if (organizer != null)
                {
                    eventStatisticsList.Add(
                        new EventStatistics
                        {
                            EventID = eventItem.EventID,
                            UserID = organizer.UserID,
                            Category = category.Name,
                            TimeStamp = DateTime.Now, 
                        });
                }

                foreach (var citizenItem in citizenList)
                {
                    eventStatisticsList.Add(
                        new EventStatistics
                        {

                            EventID = eventItem.EventID,

                        }


                        );
                }
            }










            try
            {
                _httpClient.BaseAddress = new Uri(_baseURL4);
                var result = await _httpClient.PostAsJsonAsync("", eventStatisticsList);

            }
            catch (Exception ex)
            {
                var logger = new LogService();

                await logger.CreateLog("Activity Hv.Sos100.DataService.Sync", LogService.Severity.Error, ex.Message);
            }



        }
    }
}