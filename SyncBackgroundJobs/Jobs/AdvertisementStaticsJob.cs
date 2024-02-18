
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
        public Task Execute(IJobExecutionContext context)
        {
            var DemoObj = DemoData();

            try
            {
                _httpClient.BaseAddress = new Uri(_baseURL);

                _httpClient.PostAsJsonAsync("api/AdStatistics", DemoObj);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                return Task.CompletedTask;
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
