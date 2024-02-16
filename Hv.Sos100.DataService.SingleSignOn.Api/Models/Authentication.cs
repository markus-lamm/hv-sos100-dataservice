using System.ComponentModel.DataAnnotations;

namespace Hv.Sos100.DataService.SingleSignOn.Api.Models;

public class Authentication
{
    [Key]
    public Guid AuthenticationId { get; set; } = Guid.NewGuid();
    public string? AccountId { get; set; }
    public string? Token { get; set; }
    public DateTime? LastActivity { get; set; }
    public DateTime? TokenExpiration { get; set; }
    public string? AccountType { get; set; }
}