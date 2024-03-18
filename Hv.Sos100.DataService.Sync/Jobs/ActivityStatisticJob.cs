using Hv.Sos100.Logger;
using Quartz;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using Hv.Sos100.DataService.Sync.Model;
using System.Diagnostics;
using System.Net.Sockets;

namespace Hv.Sos100.DataService.Sync.Jobs
{
    public class ActivityStatisticJob : IJob
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseURL = "https://informatik1.ei.hv.se/ActivityAPI/api/Activities";
        private readonly string _baseURL2 = "https://informatik1.ei.hv.se/ActivityAPI/api/Categories";

        //"https://informatik7.ei.hv.se/ProfilAPI/api/Citizens"
        public ActivityStatisticJob( IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            List<Model.Activity>? activities = new();
            List<Category>? categories = new();
            List<ActivityStatistics> activityList = new();
            var activityClient = _httpClientFactory.CreateClient("activityapi");
            var statisticClient = _httpClientFactory.CreateClient("statisticapi");
            var logger = new LogService();

            try
            {
                HttpResponseMessage activityResponse = await activityClient.GetAsync("api/Activities");

                if (activityResponse.IsSuccessStatusCode)
                {
                    string content = await activityResponse.Content.ReadAsStringAsync();
                    activities = JsonSerializer.Deserialize<List<Model.Activity>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else { await logger.CreateLog("activity Hv.Sos100.DataService.Sync", LogService.Severity.Error, "anrop till activity api fungerar inte"); }

                //_httpClient.BaseAddress = new Uri(_baseURL);

                //HttpResponseMessage response = await _httpClient.GetAsync("");

                //if (response.IsSuccessStatusCode)
                //{
                //    string content = await response.Content.ReadAsStringAsync();
                //    activities = JsonSerializer.Deserialize<List<Model.Activity>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                //}
            }
            catch (Exception ex)
            {
                await logger.CreateLog("Activity Hv.Sos100.DataService.Sync", LogService.Severity.Error, ex.Message);
            }

            try
            {
                HttpResponseMessage categoryResponse = await activityClient.GetAsync("api/Categories");

                if (categoryResponse.IsSuccessStatusCode)
                {
                    string content = await categoryResponse.Content.ReadAsStringAsync();
                    categories = JsonSerializer.Deserialize<List<Category>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else { await logger.CreateLog("activity Hv.Sos100.DataService.Sync", LogService.Severity.Error, "anrop till activity api fungerar inte"); }

                //_httpClient.BaseAddress = new Uri(_baseURL2);

                //HttpResponseMessage response = await _httpClient.GetAsync("");

                //if (response.IsSuccessStatusCode)
                //{
                //    string content = await response.Content.ReadAsStringAsync();
                //    categories = JsonSerializer.Deserialize<List<Category>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                //}
            }
            catch (Exception ex)
            {
                await logger.CreateLog("Activity Hv.Sos100.DataService.Sync", LogService.Severity.Error, ex.Message);
            }

            foreach (var item in activities)
            {
                var category = categories.FirstOrDefault(c => c.CategoryID == item.CategoryID);
                var activityStatisticsItem = new ActivityStatistics { 
                    ActivityID = item.ActivityID, 
                    TimeStamp = item.TimeStamp,
                    Category = category.Name,
                };
                activityList.Add(activityStatisticsItem); 
            }
            try
            {
                var postactivityStatisticResponse = await statisticClient.PostAsJsonAsync("api/ActivityStatistics/activity/list", activityList);
                if (!postactivityStatisticResponse.IsSuccessStatusCode) { await logger.CreateLog("ads Hv.Sos100.DataService.Sync", LogService.Severity.Error, "Post till activity statistik api fungerar inte"); }
            }
            catch (Exception ex)
            {
                await logger.CreateLog("Activity Hv.Sos100.DataService.Sync", LogService.Severity.Error, ex.Message);
            }
        }
    }
}
