using Hv.Sos100.Logger;

namespace Hv.Sos100.DataService.Log.Gui.Data;

public class ApiService
{
    private readonly LogService _logService = new();
    private readonly HttpClient _httpClient = new();
    private const string BaseUrl = "https://informatik6.ei.hv.se/logapi";

    public async Task<List<Api.Models.Log>?> GetLogs()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/api/Logs");
            return await response.Content.ReadFromJsonAsync<List<Api.Models.Log>>();
        }
        catch (Exception ex)
        {
            await _logService.CreateLog("DataService.Log.Gui.ApiService.GetLogs", ex);
            return null;
        }
    }
}