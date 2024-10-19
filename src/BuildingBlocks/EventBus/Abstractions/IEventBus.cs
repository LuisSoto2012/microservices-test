using EventBus.Events;

namespace EventBus.Abstractions;

public interface IEventBus
{
    Task PublishAsync(IntegrationEvent @event);
    void Subscribe<T, TH>() where T : IntegrationEvent where TH : class, IIntegrationEventHandler<T>;
    void Unsubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;
}