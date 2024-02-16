using System.Text.Json;
using Hv.Sos100.DataService.SingleSignOn.Api.Models;

namespace Hv.Sos100.DataService.SingleSignOn.Api.Data;

internal class ApiService
{
    private readonly HttpClient _httpClient = new();
    private const string BaseUrl = "https://informatik3.ei.hv.se/KontoInloggAPI";

    internal async Task<List<Account>?> GetAccounts()
    {
        var users = await GetUsers();
        var organizations = await GetOrganizations();

        if (users == null || organizations == null) { return null; }

        foreach (var user in users)
        {
            user.AccountType = "User";
        }

        foreach (var org in organizations)
        {
            org.AccountType = "Organization";
        }

        return users.Concat(organizations).ToList();
    }

    private async Task<List<Account>?> GetUsers()
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/api/Users");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Account>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    private async Task<List<Account>?> GetOrganizations()
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/api/Orgs");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Account>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}