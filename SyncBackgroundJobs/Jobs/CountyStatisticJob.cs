using Quartz;

namespace SyncBackgroundJobs.Jobs
{
    public class CountyStatisticJob : IJob
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseURL = "https://informatik6.ei.hv.se/statisticapi/";
        public CountyStatisticJob(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var DemoObj = GetDemoData();

            try
            {
                _httpClient.BaseAddress = new Uri(_baseURL);

                _httpClient.PostAsJsonAsync("api/CountyStatistics", DemoObj);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                return Task.CompletedTask;
            }
        }
        public static CountyStatistics GetDemoData()
        {
            Random rand = new Random();
            return new CountyStatistics
            {
                TotalEvents = rand.Next(100, 1000),
                TotalActivities = rand.Next(50, 500),
                TotalActiveUsers = rand.Next(10, 100),
                TotalPeopleAccounts = rand.Next(20, 200),
                TotalEnterpriseAccounts = rand.Next(5, 50),
                TotalAdvertiserAccounts = rand.Next(5, 50)
            };
        }
    }
}
