
using Hv.Sos100.Logger;
using Quartz;
using System.Diagnostics;
using System.Text.Json;

namespace Hv.Sos100.DataService.Sync.Jobs
{
    public class AdvertisementStaticsJob : IJob
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly string _baseURL = "https://informatik6.ei.hv.se/advertisement/api/Ads/getallads";
        public AdvertisementStaticsJob(  IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            List<AdStatistics>? adList = new();
            var logger = new LogService();

            try
            {
                var adsClient = _httpClientFactory.CreateClient("adsapi");
                var statisticClient = _httpClientFactory.CreateClient("statisticapi");

                HttpResponseMessage adsResponse = await adsClient.GetAsync("api/Ads/getallads");
                if (adsResponse.IsSuccessStatusCode)
                {
                    string content = await adsResponse.Content.ReadAsStringAsync();
                    adList = JsonSerializer.Deserialize<List<AdStatistics>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else { await logger.CreateLog("ads Hv.Sos100.DataService.Sync", LogService.Severity.Error, "anrop till annons api fungerar inte"); }

                var postAdStatisticResponse = await statisticClient.PostAsJsonAsync("api/AdStatistics/ad/list", adList);
                if (!postAdStatisticResponse.IsSuccessStatusCode) { await logger.CreateLog("ads Hv.Sos100.DataService.Sync", LogService.Severity.Error, "Post till annons statistik api fungerar inte"); }
                //_httpClient.BaseAddress = new Uri(_baseURL);

                //HttpResponseMessage response = await _httpClient.GetAsync("");

                //if (response.IsSuccessStatusCode)
                //{
                //    string content = await response.Content.ReadAsStringAsync();
                //    adList = JsonSerializer.Deserialize<List<AdStatistics>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                //}

                //_httpClient.BaseAddress = new Uri("https://informatik6.ei.hv.se/statisticapi/api/AdStatistics/list");
                //var result = await _httpClient.PostAsJsonAsync("", adList);
            }
            catch (Exception ex)
            {
                await logger.CreateLog("ads Hv.Sos100.DataService.Sync", LogService.Severity.Error, ex.Message);
            }
        }
    }
}
