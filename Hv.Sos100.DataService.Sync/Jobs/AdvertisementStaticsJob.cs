﻿
using Hv.Sos100.Logger;
using Quartz;
using System.Text.Json;

namespace Hv.Sos100.DataService.Sync.Jobs
{
    public class AdvertisementStaticsJob : IJob
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly LogService _logger;

        public AdvertisementStaticsJob(  IHttpClientFactory httpClientFactory, LogService logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            List<Statistics.Api.Models.AdStatistics>? adStatisticsList = await GetAdvertisements();
            if(adStatisticsList == null) 
            {
                await _logger.CreateLog("Hv.Sos100.DataService.Sync.AdExecute", LogService.Severity.Warning, "GetAdvertisements returns null");
                return; 
            }

            await PostAdvertisementStatistics(adStatisticsList);
        }

        private async Task<List<Statistics.Api.Models.AdStatistics>?> GetAdvertisements()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("adsapi");
                HttpResponseMessage response = await client.GetAsync("api/Ads/getallads");

                string content = await response.Content.ReadAsStringAsync();
                var ads = JsonSerializer.Deserialize<List<Statistics.Api.Models.AdStatistics>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return ads;
            }
            catch (Exception ex)
            {
                await _logger.CreateLog("Hv.Sos100.DataService.Sync.GetAdvertisements", ex);
                return null;
            }
        }

        private async Task PostAdvertisementStatistics(List<Statistics.Api.Models.AdStatistics> adList)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("statisticapi");
                var response = await client.PostAsJsonAsync("api/AdStatistics/ad/list", adList);
                if (!response.IsSuccessStatusCode)
                {
                    await _logger.CreateLog("Hv.Sos100.DataService.Sync.PostAdvertisementStatistics", LogService.Severity.Error, "Post to AdStatistics api creates error");
                }
            }
            catch (Exception ex)
            {
                await _logger.CreateLog("Hv.Sos100.DataService.Sync.PostAdvertisementStatistics", ex);
            }
        }

    }
}
