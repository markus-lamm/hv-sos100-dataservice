namespace Hv.Sos100.DataService.Sync.Model
{
    public class Event
    {
        public int EventID { get; set; }
        public int OrganizerID { get; set; }
        public string? Name { get; set; }
        public int? CategoryID { get; set; }
    }
}
