using System.Text;
using System.Text.Json;
using Hv.Sos100.DataService.SingleSignOn.Api.Models;
using Hv.Sos100.Logger;

namespace Hv.Sos100.DataService.SingleSignOn.Api.Data;

internal class ApiService
{
    private readonly HttpClient _httpClient = new();
    private const string BaseUrl = "https://informatik3.ei.hv.se/KontoInloggAPI";
    private readonly LogService _logService = new();

    internal async Task<User?> AuthAccount(string email, string password)
    {
        try
        {
            var user = await AuthUser(email, password);
            return user;
        }
        catch (Exception ex)
        {
            await _logService.CreateLog("Sso.Api.AuthAccount", ex);
            return null;
        }
    }

    private async Task<User?> AuthUser(string email, string password)
    {
        var response = await _httpClient.PostAsync($"{BaseUrl}/api/UserAuths", new StringContent(JsonSerializer.Serialize(
            new { Email = email, Password = password }), Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(content)) { return null; }

        var user = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return user;
    }
}
