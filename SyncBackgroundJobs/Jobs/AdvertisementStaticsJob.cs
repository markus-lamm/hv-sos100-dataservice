
using Hv.Sos100.Logger;
using Quartz;
using System.Text.Json;

namespace SyncBackgroundJobs.Jobs
{
    public class AdvertisementStaticsJob : IJob
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseURL = "https://informatik6.ei.hv.se/statisticapi/";
        public AdvertisementStaticsJob( HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var DemoObj = DemoData();

            try
            {
                _httpClient.BaseAddress = new Uri(_baseURL);

                var result = await _httpClient.PostAsJsonAsync("api/AdStatistics", DemoObj);

                if (!result.IsSuccessStatusCode)
                {
                    var logger = new LogService();

                    var logResult = await logger.CreateApiLog("Advertisement Sync Job", 3, "Anropet lyckades inte");

                    if (!logResult)
                    {
                        logger.CreateLocalLog("Advertisement Sync Job", 3, "Anropet lyckades inte");

                    }
                }

            }
            catch (Exception ex)
            {
                var logger = new LogService();

                var logResult = await logger.CreateApiLog("Advertisement Sync Job", 3, ex.Message);

                if (!logResult)
                {
                    logger.CreateLocalLog("Advertisement Sync Job", 3, ex.Message);

                }
            }
        }

        public AdStatistics DemoData()
        {
            Random rand = new Random();

            AdStatistics d = new AdStatistics();
            d.TimeStamp = DateTime.Now;
            d.Clicks = rand.Next(1, 100); // Generate a random integer between 1 and 100 for Clicks
            d.TotalViews = rand.Next(1, 1000); // Generate a random integer between 1 and 1000 for TotalViews
            d.FemaleViews = rand.Next(1, 500); // Generate a random integer between 1 and 500 for FemaleViews
            d.MaleViews = rand.Next(1, 500); // Generate a random integer between 1 and 500 for MaleViews
            d.Age31To50Views = rand.Next(1, 200); // Generate a random integer between 1 and 200 for Age31To50Views
            d.Age50PlusViews = rand.Next(1, 200); // Generate a random integer between 1 and 200 for Age50PlusViews
            d.Age16To30Views = rand.Next(1, 100); // Generate a random integer between 1 and 100 for Age16To30Views

            return d;
        }
    }
}
