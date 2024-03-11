using System.ComponentModel.DataAnnotations;

namespace Hv.Sos100.DataService.Statistics.Api.Models
{
    public class EventStatistics
    {
        [Key] public int Id { get; set; }
        public int EventId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? Views { get; set; }
        public int? TotalSignups { get; set; }
        public int? FemaleSignups { get; set; }
        public int? MaleSignups { get; set; }
        public int? Age16To30Signups { get; set; }
        public int? Age31To50Signups { get; set; }
        public int? Age50PlusSignups { get; set; }
        
       
    }
}
