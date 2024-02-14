
using Quartz;
using System.Text.Json;

namespace SyncBackgroundJobs.Jobs
{
    public class AdvertisementStaticsJob : IJob
    {
        private readonly StatisticsContext _statisticsContext;
        private readonly ILogger _logger;
        public AdvertisementStaticsJob(StatisticsContext statisticsContext, ILogger logger)
        {
            _statisticsContext = statisticsContext;
            _logger = logger;

        }
        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                //var DemoObj = DemoData();
                //_statisticsContext.Ads.Add(DemoObj);
                //_statisticsContext.SaveChanges();
                _logger.LogInformation(DateTime.UtcNow.ToString());
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                return Task.CompletedTask;
            }
        }

        public AdStatistics DemoData()
        {
            AdStatistics d = new();
            d.TimeStamp = DateTime.Now;
            d.Clicks = 2;
            d.TotalViews = 22;
            d.FemaleViews = 10;
            d.MaleViews = 12;
            d.Age31To50Views = 10;
            d.Age50PlusViews = 10;
            d.Age16To30Views = 2;
            return d;
        }
    }
}
