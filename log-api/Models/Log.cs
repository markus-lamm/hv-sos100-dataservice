namespace LogApi.Models
{
    public class Log
    {
        public int Id { get; set; }
        public DateTime? TimeStamp { get; set; }
        public string? SourceSystem { get; set; }
        public string? Message { get; set; }
    }
}
