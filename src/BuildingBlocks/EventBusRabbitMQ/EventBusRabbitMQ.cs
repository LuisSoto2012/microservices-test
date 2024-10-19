using System.Text;
using EventBus;
using EventBus.Abstractions;
using EventBus.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EventBusRabbitMQ;

public class EventBusRabbitMQ : IEventBus
{
    private readonly IRabbitMQPersistentConnection _persistentConnection;
    private readonly EventBusSubscriptionsManager _subscriptionsManager;
    private readonly IServiceProvider _serviceProvider;

    public EventBusRabbitMQ(IRabbitMQPersistentConnection persistentConnection, EventBusSubscriptionsManager subscriptionsManager, IServiceProvider serviceProvider)
    {
        _persistentConnection = persistentConnection;
        _subscriptionsManager = subscriptionsManager;
        _serviceProvider = serviceProvider;
    }
    
    public async Task PublishAsync(IntegrationEvent @event)
    {
        using (var channel = _persistentConnection.CreateModel())
        {
            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            var properties = channel.CreateBasicProperties();
            properties.DeliveryMode = 2; // Persistent

            await Task.Run(() =>
            {
                channel.BasicPublish(
                    exchange: "microservices_test_event_bus",
                    routingKey: @event.GetType().Name,
                    mandatory: false,
                    basicProperties: properties,
                    body: body);
            });
        }
    }

    public void Subscribe<T, TH>() where T : IntegrationEvent where TH : class, IIntegrationEventHandler<T>
    {
        var eventName = typeof(T).Name;
        _subscriptionsManager.AddSubscription<T, TH>();
        StartBasicConsume<T>(eventName);
    }

    public void Unsubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
    {
        var eventName = typeof(T).Name;
        _subscriptionsManager.RemoveSubscription<T, TH>();
    }

    private void StartBasicConsume<T>(string eventName) where T : IntegrationEvent
    {
        var channel = _persistentConnection.CreateModel();
        channel.ExchangeDeclare(exchange: "microservices_test_event_bus", type: "topic", durable: true);
        channel.QueueDeclare(queue: eventName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        channel.QueueBind(queue: eventName, exchange: "microservices_test_event_bus", routingKey: eventName);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var integrationEvent = JsonConvert.DeserializeObject<T>(message);
            
            using (var scope = _serviceProvider.CreateScope())
            {
                var handler = scope.ServiceProvider.GetService<IIntegrationEventHandler<T>>();
                if (handler == null)
                {
                    throw new Exception($"No handler registered for {typeof(T).Name}");
                }

                await handler.Handle(integrationEvent);
            }
        };

        channel.BasicConsume(queue: eventName, autoAck: true, consumer: consumer);
    }
}