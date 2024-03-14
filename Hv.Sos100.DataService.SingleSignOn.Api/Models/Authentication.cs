using System.ComponentModel.DataAnnotations;

namespace Hv.Sos100.DataService.SingleSignOn.Api.Models;

public class Authentication
{
    [Key]
    public Guid AuthenticationID { get; set; } = Guid.NewGuid();
    public string? UserID { get; set; }
    public string? UserRole { get; set; }
    public string? Token { get; set; }
    public DateTime? LastActivity { get; set; }
    public DateTime? TokenExpiration { get; set; }
}