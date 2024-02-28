using Hv.Sos100.Logger;
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

        public async Task Execute(IJobExecutionContext context)
        {
            var DemoObj = GetDemoData();

            try
            {
                _httpClient.BaseAddress = new Uri(_baseURL);

                var result = await _httpClient.PostAsJsonAsync("api/ActivityStatistics", DemoObj);

                if (!result.IsSuccessStatusCode)
                {
                    var logger = new LogService();

                    var logResult = await logger.CreateApiLog("Activity Statistic Sync Job", 3, "Anropet lyckades inte");

                    if (!logResult)
                    {
                        logger.CreateLocalLog("Activity Statistic Sync Job", 3, "Anropet lyckades inte");

                    }
                }
            }
            catch (Exception ex)
            {
                var logger = new LogService();

                var logResult = await logger.CreateApiLog("Activity Statistic Sync Job", 3, ex.Message);

                if (!logResult)
                {
                    logger.CreateLocalLog("Activity Statistic Sync Job", 3, ex.Message);

                }
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
