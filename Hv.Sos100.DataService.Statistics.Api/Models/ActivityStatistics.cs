using System.ComponentModel.DataAnnotations;

namespace Hv.Sos100.DataService.Statistics.Api.Models
{
    public class ActivityStatistics()
    {
        [Key] public int ActivityStatisticsID { get; set; }
        public int ActivityID { get; set; }
        public DateTime TimeStamp { get; set; }
        public int? TotalSaved { get; set; }
        public int? FemaleSaved {  get; set; }
        public int? MaleSaved { get; set; }
        public int? AgeBelow16Saved { get; set; }
        public int? Age16To30Saved {  get; set; }
        public int? Age31To50Saved { get; set; }
        public int? AgeAbove50Saved { get; set; }
        public string? Category {  get; set; }
    }
}
          