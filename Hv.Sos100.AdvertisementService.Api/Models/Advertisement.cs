using System.ComponentModel.DataAnnotations;

namespace Hv.Sos100.AdvertisementService.Api.Models
{
    public class Advertisement
    {
        [Key] public int AdvertisementID { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? Url { get; set; }
        public string? ImageLink { get; set; }
        public int? TotalViews { get; set; }
    }
}
