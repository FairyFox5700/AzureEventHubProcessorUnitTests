using Moq;
using Newtonsoft.Json;
using EventHubFunction;
using FluentAssertions;
using AzureEvenHubTests.Utils;
using Microsoft.Azure.WebJobs;
using Azure.Messaging.EventHubs;
using Microsoft.Extensions.Logging;
using Azure.Messaging.EventHubs.Producer;

namespace AzureEvenHubTests
{
    public class AzureEventHubFunction1Tests
    {
        private Mock<ILogger> _loggerMock = new Mock<ILogger>();

        [Test]
        public async Task AzureEventHubFunction1_Run_EmitsCorrectEvents()
        {
            var mockEventHubProducerClient = new Mock<EventHubProducerClient>();
            mockEventHubProducerClient.Setup(e => e.SendAsync(It.IsAny<IEnumerable<EventData>>(), default)).Returns(Task.CompletedTask);

            await AzureEventHubFunction1.Run(default(TimerInfo), mockEventHubProducerClient.Object, _loggerMock.Object);


            mockEventHubProducerClient.Verify(e=>e.SendAsync(It.Is<IEnumerable<EventData>>(data => data.Select(e => e.GetItem<string>()).ToList().SequenceEqual(_expectedEvents)), default), Times.Once);
        }

        private readonly List<string> _expectedEvents = new List<string>()
        {
            "Event1",
            "Event2"
        };
    }
}