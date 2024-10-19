using EventBus.Abstractions;
using EventBus.Events;

namespace EventBus;

public class EventBusSubscriptionsManager
{
    private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;

    public EventBusSubscriptionsManager()
    {
        _handlers = new Dictionary<string, List<SubscriptionInfo>>();
    }

    public void AddSubscription<T, TH>() 
        where T : IntegrationEvent 
        where TH : class, IIntegrationEventHandler<T>
    {
        var key = GetEventKey<T>();
        if (!_handlers.ContainsKey(key))
        {
            _handlers[key] = new List<SubscriptionInfo>();
        }
        if (_handlers[key].Exists(s => s.HandlerType == typeof(TH)))
        {
            throw new ArgumentException($"Handler Type {typeof(TH).Name} already registered for event {key}");
        }

        _handlers[key].Add(SubscriptionInfo.Typed<TH>());
    }
    
    public void RemoveSubscription<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
    {
        var key = GetEventKey<T>();
        if (_handlers.ContainsKey(key))
        {
            var handlerToRemove = _handlers[key].Find(s => s.HandlerType == typeof(TH));
            if (handlerToRemove != null)
            {
                _handlers[key].Remove(handlerToRemove);
            }
        }
    }

    public string GetEventKey<T>()
    {
        return typeof(T).Name;
    }

    public List<SubscriptionInfo> GetHandlersForEvent(string eventName)
    {
        return _handlers.ContainsKey(eventName) ? _handlers[eventName] : null;
    }
}