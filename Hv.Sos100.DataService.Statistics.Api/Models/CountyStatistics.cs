using System.ComponentModel.DataAnnotations;

namespace Hv.Sos100.DataService.Statistics.Api.Models
{
    public class CountyStatistics()
    {
        [Key] public int Id { get; set; }
        public int CountyId { get; set; }
        public int? TotalEvents { get; set; }
        public int? TotalActivities { get; set; }
        public int? TotalActiveUsers { get; set; }
        public int? TotalPeopleAccounts { get; set; }
        public int? TotalEnterpriseAccounts { get; set; }
        
    }
}
