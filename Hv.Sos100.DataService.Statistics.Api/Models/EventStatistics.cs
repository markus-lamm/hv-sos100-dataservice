using System.ComponentModel.DataAnnotations;

namespace Hv.Sos100.DataService.Statistics.Api.Models
{
    public class EventStatistics
    {
        [Key] public int EventStatisticsID { get; set; }
        public int EventID { get; set; }
        public int? UserID { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? TotalSignups { get; set; }
        public int? FemaleSignups { get; set; }
        public int? MaleSignups { get; set; }
        public int? AgeBelow16Signups { get; set; }
        public int? Age16To30Signups { get; set; }
        public int? Age31To50Signups { get; set; }
        public int? AgeAbove50Signups { get; set; }
        public string? Category { get; set; }
    }
}
