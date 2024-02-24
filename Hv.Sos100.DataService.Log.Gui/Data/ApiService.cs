using Hv.Sos100.Logger;

namespace Hv.Sos100.DataService.Log.Gui.Data;

public class ApiService
{
    private readonly LogService _logService = new();
    private readonly HttpClient _httpClient = new();
    private const string BaseUrl = "https://informatik6.ei.hv.se/logapi";

    public async Task<List<Api.Models.Log>?> GetLogs()
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/api/Logs");
        if (!response.IsSuccessStatusCode)
        {
            await _logService.CreateLog("Log.Api.GetLogs", LogService.Severity.Error, response.ReasonPhrase ?? "Unknown api call error");
            return null;
        }

        return await response.Content.ReadFromJsonAsync<List<Api.Models.Log>>();
    }
}