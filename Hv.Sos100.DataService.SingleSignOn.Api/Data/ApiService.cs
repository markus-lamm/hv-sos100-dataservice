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

    internal async Task<Account?> AuthAccount(string email, string password)
    {
        try
        {
            // Call both user and organization authentication endpoints simultaneously and return the first successful result
            var userTask = AuthUser(email, password);
            var orgTask = AuthOrganizations(email, password);

            var user = await userTask;
            if (user != null) { return user; }

            var org = await orgTask;
            if (org != null) { return org; }

            return null;
        }
        catch (Exception ex)
        {
            await _logService.CreateLog("Sso.Api.AuthAccount", ex);
            return null;
        }
    }

    private async Task<Account?> AuthUser(string email, string password)
    {
        var response = await _httpClient.PostAsync($"{BaseUrl}/api/AuthUsers", new StringContent(JsonSerializer.Serialize(
            new { Email = email, Password = password }), Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        var user = JsonSerializer.Deserialize<Account>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if (user != null) { user.AccountType = "User"; }
        return user;
    }

    private async Task<Account?> AuthOrganizations(string email, string password)
    {
        var response = await _httpClient.PostAsync($"{BaseUrl}/api/AuthOrgs", new StringContent(JsonSerializer.Serialize(
            new { Email = email, Password = password }), Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        var org = JsonSerializer.Deserialize<Account>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if (org != null) { org.AccountType = "Organization"; }
        return org;
    }
}