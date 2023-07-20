using System;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Azure.Messaging.EventHubs.Producer;

namespace EventHubFunction.Utils;
public class EventHubHubService<TMessage> : IEventHubService<TMessage>
{
    private readonly EventHubProducerClient _eventHubProducerClient;
    private readonly ILogger<EventHubHubService<TMessage>> _logger;
    public EventHubHubService(EventHubProducerClient eventHubProducerClient, ILogger<EventHubHubService<TMessage>> logger)
    {
        _eventHubProducerClient = eventHubProducerClient;
        _logger = logger;
    }

    public async Task SendMessageToEventHub(TMessage messageEntity)
    {
        var message = JsonConvert.SerializeObject(messageEntity);
        try
        {
            await SendToEventHub(message);
            _logger.LogInformation($"Sent message to EventHub: {message}");
        }
        catch (Exception e)
        {
            _logger.LogError($"{e.Message} at {e.StackTrace}");
            throw;
        }
    }

    private async Task SendToEventHub(string message)
    {
        var eventBatch = await _eventHubProducerClient.CreateBatchAsync();
        eventBatch.TryAdd(new Azure.Messaging.EventHubs.EventData(Encoding.UTF8.GetBytes(message)));
        await _eventHubProducerClient.SendAsync(eventBatch);
    }
}