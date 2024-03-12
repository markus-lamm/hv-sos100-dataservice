using System.ComponentModel.DataAnnotations;

namespace Hv.Sos100.DataService.Statistics.Api.Models
{
    public class AdStatistics()
    {
        [Key] public int AdvertisementStatisticsID { get; set; }
        public int AdvertisementID { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? TotalViews { get; set; }
    }
}
