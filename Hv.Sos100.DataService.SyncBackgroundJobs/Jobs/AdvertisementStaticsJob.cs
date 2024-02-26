using Quartz;
using System;
using System.Net.Http.Json;
using System.Text.Json;
using Hv.Sos100.Logger;

namespace Hv.Sos100.DataService.SyncBackgroundJobs
{
    public class AdvertisementStaticsJob : IJob
    {
        private readonly HttpClient _httpClient = new();
        private readonly string _baseURL = "https://informatik6.ei.hv.se/statisticapi/";
        public async Task Execute(IJobExecutionContext context)
        {
            var DemoObj = DemoData();

            try
            {
                _httpClient.BaseAddress = new Uri(_baseURL);

                await _httpClient.PostAsJsonAsync("api/AdStatistics", DemoObj);
                
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
