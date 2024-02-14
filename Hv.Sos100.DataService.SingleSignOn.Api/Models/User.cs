namespace Hv.Sos100.DataService.SingleSignOn.Api.Models;

public class User
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Token { get; set; }
    public DateTime? LastActivity { get; set; }
}