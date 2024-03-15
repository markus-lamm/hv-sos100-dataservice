using Hv.Sos100.Logger;
using Quartz;

namespace Hv.Sos100.DataService.Sync.Jobs
{
    public class EventStatisticJob : IJob
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseURL = "https://informatik6.ei.hv.se/statisticapi/";
        public EventStatisticJob(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task Execute(IJobExecutionContext context)
        {

        }
    }
}