namespace Hv.Sos100.DataService.Sync.Model
{
    public class Citizen
    {
        public int CitizenID { get; set; }
        public int UserID { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public List<int> EventList { get; set; } = new List<int>();
    }
}
