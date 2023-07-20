using EventHubFunction.Utils;

namespace AzureEvenHubTests.Utils
{
    public interface IEventHubTestService<TMessage>: IEventHubService<TMessage>
    {
        List<TMessage> Items { get; }
    }
    public class EventHubHubServiceStub<TMessage> : IEventHubTestService<TMessage>
    {
        public List<TMessage> Items { get; } = new List<TMessage>();

        public Task SendMessageToEventHub(TMessage messageEntity)
        {
            Items.Add(messageEntity);
            return Task.FromResult(true);
        }
    }
}
