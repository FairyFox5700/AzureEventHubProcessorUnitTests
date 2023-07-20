using System.Text;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs;
using Azure.Messaging.EventHubs;

namespace AzureEvenHubTests.Utils
{
    public class AsyncCollector<T> : IAsyncCollector<T>
    {
        public readonly List<T> Items = new List<T>();

        public Task AddAsync(T item, CancellationToken cancellationToken = default(CancellationToken))
        {

            Items.Add(item);

            return Task.FromResult(true);
        }

        public Task FlushAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(true);
        }
    }

    public static class EvenHubDataExtension
    {
        public static TOutput? GetItem<TOutput>(this EventData eventData)
        {
            string payload = eventData.EventBody.ToString();
            return JsonConvert.DeserializeObject<TOutput>(payload);
        }
    }
}
