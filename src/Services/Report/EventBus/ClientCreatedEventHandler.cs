using EventBus.Abstractions;
using Report.Service.Domain.Entities;
using Report.Service.Domain.Events;
using Report.Service.InMemoryDatabase;

namespace Report.Service.EventBus;

public class ClientCreatedEventHandler : IIntegrationEventHandler<ClientCreatedEvent>
{
    public Task Handle(ClientCreatedEvent @event)
    {
        var client = new ClientInfo
        {
            ClientId = @event.ClientId,
            ClientName = @event.ClientName
        };
        InMemoryDb.Clients.Add(client);
        return Task.CompletedTask;
    }
}