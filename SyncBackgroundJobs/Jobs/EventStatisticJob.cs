using Quartz;

namespace SyncBackgroundJobs.Jobs
{
    public class EventStatisticJob : IJob
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseURL = "https://informatik6.ei.hv.se/statisticapi/";
        public EventStatisticJob(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var DemoObj = GetDemoData();

            try
            {
                _httpClient.BaseAddress = new Uri(_baseURL);

                _httpClient.PostAsJsonAsync("api/EventStatistics", DemoObj);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                return Task.CompletedTask;
            }
        }

        public static EventStatistics GetDemoData()
        {
            Random rand = new Random();
            return new EventStatistics
            {
                EventId = 1,
                TimeStamp = DateTime.Now,
                Views = rand.Next(100, 1000),
                TotalSignups = rand.Next(10, 100),
                FemaleSignups = rand.Next(5, 50),
                MaleSignups = rand.Next(5, 50),
                Age16To30Signups = rand.Next(1, 30),
                Age31To50Signups = rand.Next(1, 30),
                Age50PlusSignups = rand.Next(1, 30),
                SavedEvents = rand.Next(1, 50),
                Rating1 = rand.Next(1, 20),
                Rating2 = rand.Next(1, 20),
                Rating3 = rand.Next(1, 20),
                Rating4 = rand.Next(1, 20),
                Rating5 = rand.Next(1, 20)
            };
        }
    }
}
