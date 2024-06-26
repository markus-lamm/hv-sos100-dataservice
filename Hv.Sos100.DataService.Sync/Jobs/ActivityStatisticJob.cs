﻿using Hv.Sos100.Logger;
using Quartz;
using System.Text.Json;
using Hv.Sos100.DataService.Sync.Model;

namespace Hv.Sos100.DataService.Sync.Jobs
{
    public class ActivityStatisticJob : IJob
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly LogService _logger = new();

        public ActivityStatisticJob(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            List<Activity>? activityList = await GetActivities();
            if(activityList == null) 
            {
                await _logger.CreateLog("DataService.Statistics.Sync.ActivityStatisticJob.Execute", LogService.Severity.Warning, "GetActivities returns null");
                return; 
            }

            List<Statistics.Api.Models.ActivityStatistics> activityStatisticsList = new();
            foreach (var activityItem in activityList)
            {
                var activityStatisticsItem = new Statistics.Api.Models.ActivityStatistics { 
                    ActivityID = activityItem.ActivityID, 
                    TimeStamp = activityItem.TimeStamp,
                    CategoryID = activityItem.CategoryID,
                    Name = activityItem.Name,
                };
                activityStatisticsList.Add(activityStatisticsItem); 
            }

            await PostActivityStatistics(activityStatisticsList);
        }

        private async Task<List<Activity>?> GetActivities()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("activityapi");
                HttpResponseMessage response = await client.GetAsync("api/Activities");

                string content = await response.Content.ReadAsStringAsync();
                var activities = JsonSerializer.Deserialize<List<Activity>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return activities;
            }
            catch (Exception ex)
            {
                await _logger.CreateLog("DataService.Statistics.Sync.ActivityStatisticJob.GetActivities", ex);
                return null;
            }
        }

        private async Task PostActivityStatistics(List<Statistics.Api.Models.ActivityStatistics> activityList)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("statisticapi");
                var response = await client.PostAsJsonAsync("api/ActivityStatistics/activity/list", activityList);
                if (!response.IsSuccessStatusCode) { 
                    await _logger.CreateLog("DataService.Statistics.Sync.ActivityStatisticJob.PostActivityStatistics", LogService.Severity.Error, "Post to ActivityStatistics api creates error"); 
                }
            }
            catch (Exception ex)
            {
                await _logger.CreateLog("DataService.Statistics.Sync.ActivityStatisticJob.PostActivityStatistics", ex);
            }
        }

    }
}
