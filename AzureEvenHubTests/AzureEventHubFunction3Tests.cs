using Moq;
using Newtonsoft.Json;
using EventHubFunction;
using EventHubFunction.Utils;
using AzureEvenHubTests.Utils;
using Microsoft.Azure.WebJobs;
using Azure.Messaging.EventHubs;
using Microsoft.Extensions.Logging;

namespace AzureEvenHubTests
{
    public class AzureEventHubFunction3Tests
    {
        private Mock<ILogger> _loggerMock = new Mock<ILogger>();

        [Test]
        public async Task AzureEventHubFunction3_Run_EmitsCorrectEvents()
        {
            var mockEventHubService = new EventHubHubServiceStub<string>();
            var func = new AzureEventHubFunction3(mockEventHubService);

            await func.Run(default(TimerInfo), _loggerMock.Object);

            Assert.That(mockEventHubService.Items, Is.EqualTo(_expectedEvents));
        }

        private readonly List<string> _expectedEvents = new List<string>()
        {
            "Event1",
            "Event2"
        };
    }
}