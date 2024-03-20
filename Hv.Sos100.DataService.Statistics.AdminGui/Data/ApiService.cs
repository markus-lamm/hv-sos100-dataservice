using Hv.Sos100.Logger;
using System.Text.Json;

namespace Hv.Sos100.DataService.Statistics.AdminGui.Data;

public class ApiService
{
    private readonly LogService _logService;

    public ApiService(LogService logService)
    {
        _logService = logService;
    }

    public async Task<List<T>?> GetApiRequest<T>(string url)
    {
        try
        {
            HttpClient client = new();
            var response = await client.GetAsync(url);
            string content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<T>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch(Exception ex)
        {
            await _logService.CreateLog("DataService.Statistics.AdminGui.ApiService.GetApiRequest", ex);
            return null;
        }
    }
}

