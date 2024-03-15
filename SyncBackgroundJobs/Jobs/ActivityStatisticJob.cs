using Hv.Sos100.Logger;
using Quartz;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using SyncBackgroundJobs.Model;
using System.Diagnostics;

namespace SyncBackgroundJobs.Jobs
{
    public class ActivityStatisticJob : IJob
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseURL = "https://informatik1.ei.hv.se/ActivityAPI/api/Activities";
        private readonly string _baseURL2 = "https://informatik1.ei.hv.se/ActivityAPI/api/Categories";

        //"https://informatik7.ei.hv.se/ProfilAPI/api/Citizens"
        public ActivityStatisticJob(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Execute(IJobExecutionContext context)
        {

            List<Model.Activity>? activities = new List<Model.Activity>();
            List<Category>? categories = new List<Category>();
            List<ActivityStatistics> activityList = new List<ActivityStatistics>();

            try
            {
                _httpClient.BaseAddress = new Uri(_baseURL);

                HttpResponseMessage response = await _httpClient.GetAsync("");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    activities = JsonSerializer.Deserialize<List<Model.Activity>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            catch (Exception ex)
            {
                var logger = new LogService();

                await logger.CreateLog("Activity SyncBackgroundJobs", LogService.Severity.Error, ex.Message);
            }

             try
            {
                _httpClient.BaseAddress = new Uri(_baseURL2);

                HttpResponseMessage response = await _httpClient.GetAsync("");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    categories = JsonSerializer.Deserialize<List<Category>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            catch (Exception ex)
            {
                var logger = new LogService();

                await logger.CreateLog("Activity SyncBackgroundJobs", LogService.Severity.Error, ex.Message);
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
                _httpClient.BaseAddress = new Uri("https://informatik6.ei.hv.se/statisticapi/api/ActivityStatistics/list");
                var result = await _httpClient.PostAsJsonAsync("", activityList);
            }
            catch (Exception ex)
            {
                var logger = new LogService();

                await logger.CreateLog("Activity SyncBackgroundJobs", LogService.Severity.Error, ex.Message);
            }

            try
            {
                _httpClient.BaseAddress = new Uri(_baseURL);

                HttpResponseMessage response = await _httpClient.GetAsync("");

                if (response.IsSuccessStatusCode) 
                { 
                    string content = await response.Content.ReadAsStringAsync();
                    activityList = JsonSerializer.Deserialize<List<ActivityStatistics>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            catch (Exception ex)
            {
                var logger = new LogService();

                await logger.CreateLog("Activity SyncBackgroundJobs", LogService.Severity.Error, ex.Message);
            }
        }
        
    }
}
