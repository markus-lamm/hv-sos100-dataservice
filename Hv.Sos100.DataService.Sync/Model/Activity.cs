namespace Hv.Sos100.DataService.Sync.Model
{
    public class Activity
    {
        public int ActivityID { get; set; }
        public DateTime TimeStamp { get; set; }
        public int CategoryID { get; set; }
        public string? Name { get; set; }
    }
}
