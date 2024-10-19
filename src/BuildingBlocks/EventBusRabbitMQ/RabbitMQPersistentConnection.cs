using RabbitMQ.Client;

namespace EventBusRabbitMQ;

public class RabbitMQPersistentConnection : IRabbitMQPersistentConnection
{
    private readonly IConnection _connection;
    private readonly object _syncRoot = new object();

    public RabbitMQPersistentConnection(string connectionString)
    {
        var factory = new ConnectionFactory { Uri = new Uri(connectionString) };
        _connection = factory.CreateConnection();
    }

    public IModel CreateModel()
    {
        return _connection.CreateModel();
    }

    public bool IsConnected => _connection != null && _connection.IsOpen;

    public bool TryConnect()
    {
        if (IsConnected) return true;

        lock (_syncRoot)
        {
            // Reintentar la conexión
            return IsConnected;
        }
    }
}