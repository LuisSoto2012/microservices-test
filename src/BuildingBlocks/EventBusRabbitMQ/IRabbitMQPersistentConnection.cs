using RabbitMQ.Client;

namespace EventBusRabbitMQ;

public interface IRabbitMQPersistentConnection
{
    IModel CreateModel();
    bool IsConnected { get; }
    bool TryConnect();
}