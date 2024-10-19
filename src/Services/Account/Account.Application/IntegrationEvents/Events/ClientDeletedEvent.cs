using EventBus.Events;

namespace Account.Application.IntegrationEvents.Events;

public class ClientDeletedEvent : IntegrationEvent
{
    public int ClientId { get; set; }

    public ClientDeletedEvent(int clientId)
    {
        ClientId = clientId;
    }
}