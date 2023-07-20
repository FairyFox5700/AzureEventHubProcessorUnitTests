using System.Threading.Tasks;

namespace EventHubFunction.Utils;
public interface IEventHubService<in TMessage>
{
    Task SendMessageToEventHub(TMessage messageEntity);
}