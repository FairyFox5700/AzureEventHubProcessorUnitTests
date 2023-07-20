using System;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using EventHubFunction.Utils;
using Microsoft.Azure.WebJobs;
using Azure.Messaging.EventHubs;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace EventHubFunction
{
    public class AzureEventHubFunction3
    {
        private readonly IEventHubService<string> _eventHubService;

        public AzureEventHubFunction3(IEventHubService<string> eventHubService)
        {
            _eventHubService = eventHubService;
        }

        [FunctionName("AzureEventHubFunction3")]
        public async Task Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer,
            ILogger logger)
        {
            var exceptions = new List<Exception>();
            try
            {
                await _eventHubService.SendMessageToEventHub("Event1");
                await _eventHubService.SendMessageToEventHub("Event2");
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
