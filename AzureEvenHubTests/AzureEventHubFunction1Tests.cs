using Moq;
using Newtonsoft.Json;
using EventHubFunction;
using AzureEvenHubTests.Utils;
using Microsoft.Azure.WebJobs;
using Azure.Messaging.EventHubs;
using Microsoft.Extensions.Logging;

namespace AzureEvenHubTests
{
    public class AzureEventHubFunction2Tests
    {
        private Mock<ILogger> _loggerMock = new Mock<ILogger>();

        [Test]
        public async Task AzureEventHubFunction2_Run_EmitsCorrectEvents()
        {
            var col = new AsyncCollector<EventData>();

            await AzureEventHubFunction2.Run(default(TimerInfo), col, _loggerMock.Object);

            Assert.That(col.Items.Select(e=>e.GetItem<string>()), Is.EqualTo(_expectedEvents));
        }

        private readonly List<string> _expectedEvents = new List<string>()
        {
            "Event1",
            "Event2"
        };
    }
}