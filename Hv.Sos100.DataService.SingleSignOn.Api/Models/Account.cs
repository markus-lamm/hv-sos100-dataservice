namespace Hv.Sos100.DataService.SingleSignOn.Api.Models;

public class Account
{
    public int? Id { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? AccountType { get; set; }
}