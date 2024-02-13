using System.ComponentModel.DataAnnotations;

namespace Hv.Sos100.DataService.Statistics.Api.Models
{
    public class EventStatistics
    {
        [Key] public int EventId { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Views { get; set; }
        public int TotalSignups { get; set; }
        public int FemaleSignups { get; set; }
        public int MaleSignups { get; set; }
        public int Age16To30Signups { get; set; }
        public int Age31To50Signups { get; set; }
        public int Age50PlusSignups { get; set; }
        public int SavedEvents { get; set; }
        public int Rating1 { get; set; }
        public int Rating2 { get; set; }
        public int Rating3 { get; set; }
        public int Rating4 { get; set; }
        public int Rating5 { get; set; }
    }

    public class AdStatistics()
    {
        [Key] public int AdId { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Clicks { get; set; }
        public int TotalViews { get; set; }
        public int FemaleViews { get; set; }
        public int MaleViews { get; set; }
        public int Age16To30Views { get; set; }
        public int Age31To50Views { get; set; }
        public int Age50PlusViews { get; set; }
    }

    public class ActivityStatistics()
    {
        [Key] public int ActivityId { get; set; }
        public DateTime TimeStamp { get; set; }
        public int MonthlyViews { get; set; }
        public int SavedActivity { get; set; }

    }

    public class CountyStatistics()
    {
        [Key] public int CountyId { get; set; }
        public int TotalEvents { get; set; }
        public int TotalActivities { get; set; }
        public int TotalActiveUsers { get; set; }
        public int TotalPeopleAccounts { get; set; }
        public int TotalEnterpriseAccounts { get; set; }
        public int TotalAdvertiserAccounts { get; set; }
    }
}
