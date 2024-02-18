using System.ComponentModel.DataAnnotations;

namespace SyncBackgroundJobs
{
    public class CountyStatistics()
    {
        [Key] public int CountyId { get; set; }
        public int? TotalEvents { get; set; }
        public int? TotalActivities { get; set; }
        public int? TotalActiveUsers { get; set; }
        public int? TotalPeopleAccounts { get; set; }
        public int? TotalEnterpriseAccounts { get; set; }
        public int? TotalAdvertiserAccounts { get; set; }
    }
}
