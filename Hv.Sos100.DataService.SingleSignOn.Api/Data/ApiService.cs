using System.Text;
using System.Text.Json;
using Hv.Sos100.DataService.SingleSignOn.Api.Models;

namespace Hv.Sos100.DataService.SingleSignOn.Api.Data;

internal class ApiService
{
    private readonly HttpClient _httpClient = new();
    private const string BaseUrl = "https://informatik3.ei.hv.se/KontoInloggAPI";

    internal async Task<Account?> AuthAccount(string email, string password)
    {
        var user = await AuthUser(email, password);
        if (user != null) { return user; }

        var org = await AuthOrganizations(email, password);
        if (org != null) { return org; }

        return null;
    }

    private async Task<Account?> AuthUser(string email, string password)
    {
        var response = await _httpClient.PostAsync($"{BaseUrl}/api/AuthUsers", new StringContent(JsonSerializer.Serialize(
            new { Email = email, Password = password }), Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        var userList = JsonSerializer.Deserialize<List<Account>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var user = userList?.FirstOrDefault();
        if (user != null) { user.AccountType = "User"; }
        return user;
    }

    private async Task<Account?> AuthOrganizations(string email, string password)
    {
        var response = await _httpClient.PostAsync($"{BaseUrl}/api/AuthOrgs", new StringContent(JsonSerializer.Serialize(
            new { Email = email, Password = password }), Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        var orgList = JsonSerializer.Deserialize<List<Account>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var org = orgList?.FirstOrDefault();
        if (org != null) { org.AccountType = "Organization"; }
        return org;
    }
}