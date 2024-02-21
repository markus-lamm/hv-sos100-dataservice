using System.ComponentModel.DataAnnotations;

namespace Hv.Sos100.DataService.Statistics.AdminGui.Models
{
    public class ActivityStatistics()
    {
        [Key] public int ActivityId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? MonthlyViews  { get; set; }
        public int? SavedActivity { get; set; }
    }
}
