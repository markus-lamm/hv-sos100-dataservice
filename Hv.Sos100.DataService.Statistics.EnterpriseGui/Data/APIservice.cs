using Hv.Sos100.Logger;

namespace Hv.Sos100.DataService.Statistics.EnterpriseGui.Data
{
    public class APIservice
    {
        private readonly LogService _logService = new();
        private readonly HttpClient _httpClient = new();
        private const string BaseUrl = "https://informatik6.ei.hv.se/statisticapi";

        public async Task<List<Api.Models.ActivityStatistics>?> GetActivities()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/api/ActivityStatistics");
            if (!response.IsSuccessStatusCode)
            {
                await _logService.CreateLog("StatisticsEnterpriseGUI", LogService.Severity.Error, response.ReasonPhrase ?? "Unknown api call error");
                return null;
            }

            return await response.Content.ReadFromJsonAsync<List<Api.Models.ActivityStatistics>>();
        }
        public async Task<List<Api.Models.EventStatistics>?> GetEvents()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/api/EventStatistics");
            if (!response.IsSuccessStatusCode)
            {
                await _logService.CreateLog("StatisticsEnterpriseGUI", LogService.Severity.Error, response.ReasonPhrase ?? "Unknown api call error");
                return null;
            }

            return await response.Content.ReadFromJsonAsync<List<Api.Models.EventStatistics>>();
        }

        public async Task<List<Api.Models.AdStatistics>?> GetAds()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/api/AdStatistics");
            if (!response.IsSuccessStatusCode)
            {
                await _logService.CreateLog("StatisticsEnterpriseGUI", LogService.Severity.Error, response.ReasonPhrase ?? "Unknown api call error");
                return null;
            }

            return await response.Content.ReadFromJsonAsync<List<Api.Models.AdStatistics>>();
        }
    }
}
