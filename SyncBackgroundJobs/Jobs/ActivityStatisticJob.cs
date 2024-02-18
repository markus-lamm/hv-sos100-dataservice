using Quartz;

namespace SyncBackgroundJobs.Jobs
{
    public class ActivityStatisticJob : IJob
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseURL = "https://informatik6.ei.hv.se/statisticapi/";
        public ActivityStatisticJob(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var DemoObj = GetDemoData();

            try
            {
                _httpClient.BaseAddress = new Uri(_baseURL);

                _httpClient.PostAsJsonAsync("api/ActivityStatistics", DemoObj);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                return Task.CompletedTask;
            }
        }


        public static ActivityStatistics GetDemoData()
        {
            Random rand = new Random();
            return new ActivityStatistics
            {
                TimeStamp = DateTime.Now,
                MonthlyViews = rand.Next(100, 1000),
                SavedActivity = rand.Next(10, 100)
            };
        }
    }
}
