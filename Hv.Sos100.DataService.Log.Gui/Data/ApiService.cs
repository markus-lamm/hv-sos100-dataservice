using Hv.Sos100.Logger;

namespace Hv.Sos100.DataService.Log.Gui.Data;

public class ApiService
{
    private readonly LogService _logService = new();
    private readonly HttpClient _httpClient = new();
    private const string BaseUrl = "https://informatik6.ei.hv.se/logapi";

    public async Task<List<Models.Log>?> GetLogs()
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/api/Logs");
        var logSuccess = false;
        if (!response.IsSuccessStatusCode)
        {
            logSuccess = await _logService.CreateApiLog("LogApi", response.ReasonPhrase ?? "Unknown api call error");
            if (logSuccess) { return null; }
        }
        if (!response.IsSuccessStatusCode && !logSuccess)
        {
            _logService.CreateLocalLog("LogApi", response.ReasonPhrase ?? "Unknown api call error");
            return null;
        }

        return await response.Content.ReadFromJsonAsync<List<Models.Log>>();
    }
}