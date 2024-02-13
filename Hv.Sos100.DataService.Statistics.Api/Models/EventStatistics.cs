using System.ComponentModel.DataAnnotations;

namespace Hv.Sos100.DataService.Statistics.Api.Models
{
    public class EventStatistics
    {
        [Key] public int EventId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? Views { get; set; }
        public int? TotalSignups { get; set; }
        public int? FemaleSignups { get; set; }
        public int? MaleSignups { get; set; }
        public int? Age16To30Signups { get; set; }
        public int? Age31To50Signups { get; set; }
        public int? Age50PlusSignups { get; set; }
        public int? SavedEvents { get; set; }
        public int? Rating1 { get; set; }
        public int? Rating2 { get; set; }
        public int? Rating3 { get; set; }
        public int? Rating4 { get; set; }
        public int? Rating5 { get; set; }
    }
}
