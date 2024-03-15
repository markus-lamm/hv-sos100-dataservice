
using Hv.Sos100.Logger;
using Quartz;
using System.Text.Json;

namespace Hv.Sos100.DataService.Sync.Jobs
{
    public class AdvertisementStaticsJob : IJob
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseURL = "https://informatik6.ei.hv.se/advertisement/api/Ads/getallads";
        public AdvertisementStaticsJob( HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            
            List<AdStatistics> adList = new List<AdStatistics>();
            try
            {
                _httpClient.BaseAddress = new Uri(_baseURL);

                HttpResponseMessage response = await _httpClient.GetAsync("");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    adList = JsonSerializer.Deserialize<List<AdStatistics>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }   
            }
            catch (Exception ex)
            {
                var logger = new LogService();

                await logger.CreateLog("Activity Hv.Sos100.DataService.Sync", LogService.Severity.Error, ex.Message);
            }
        }
    }
}
