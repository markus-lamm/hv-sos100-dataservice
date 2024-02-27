using System.ComponentModel.DataAnnotations;

namespace Hv.Sos100.DataService.Statistics.Api.Models
{
    public class ActivityStatistics()
    {
        [Key] public int Id { get; set; }
        public int ActivityId { get; set; }
        public int? MonthlyViews  { get; set; }
        public int? SavedActivity { get; set; }
    }
}
