namespace Hv.Sos100.DataService.SingleSignOn.Api.Models;

public class User
{
    public int? UserID { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; }
}