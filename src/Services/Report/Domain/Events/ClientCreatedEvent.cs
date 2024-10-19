using EventBus.Events;

namespace Report.Service.Domain.Events;

public class ClientCreatedEvent : IntegrationEvent
{
    public int ClientId { get; set; }
    public string ClientName { get; set; }

    public ClientCreatedEvent(int clientId, string clientName)
    {
        ClientId = clientId;
        ClientName = clientName;
    }
}