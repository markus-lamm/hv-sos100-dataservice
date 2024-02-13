using System.ComponentModel.DataAnnotations;

namespace Hv.Sos100.DataService.Statistics.Api.Models
{
    public class AdStatistics()
    {
        [Key] public int AdId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? Clicks { get; set; }
        public int? TotalViews { get; set; }
        public int? FemaleViews { get; set; }
        public int? MaleViews { get; set; }
        public int? Age16To30Views { get; set; }
        public int? Age31To50Views { get; set; }
        public int? Age50PlusViews { get; set; }
    }
}
