using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Reporting_Service
{
    public static class PotholeReport
    {
        [FunctionName("PotholeReport")]
        public static void Run([QueueTrigger("potholes", Connection = "")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
