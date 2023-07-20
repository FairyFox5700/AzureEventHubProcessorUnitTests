using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Azure.Messaging.EventHubs;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Azure.Messaging.EventHubs.Producer;

namespace EventHubFunction
{
    public static class AzureEventHubFunction1
    {
        [FunctionName("Function1")]
        public static async Task Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer,
            [EventHub("<event_hub_name>", Connection = "<connection_name>")] EventHubProducerClient eventHubProducerClient,
            ILogger logger)
        {
            var exceptions = new List<Exception>();
            try
            {
                await eventHubProducerClient.SendAsync(new[]
                {
                    new EventData(JsonConvert.SerializeObject("Event1")),
                    new EventData(JsonConvert.SerializeObject("Event2")),
                });
            }
            catch (Exception e)
            {
                exceptions.Add(e);
            }

            if (exceptions.Count > 1)
                throw new AggregateException(exceptions);

            if (exceptions.Count == 1)
                throw exceptions.Single();
        }
    }
}
