using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hv.Sos100.DataService.Advertisement.Api.Model
{
    public class Ads
    {
        [Key]
        public int AdvertisementID { get; set; }
        public string? ImageSource { get; set; }
        public string? ImageLink { get; set; }
        public int? TotalViews { get; set; }
        public DateTime? TimeStamp { get; set; } = DateTime.Now;
        public string? ImageDimension { get; set; }
    }
}
