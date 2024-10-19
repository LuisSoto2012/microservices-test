namespace EventBus.Events;

public class SubscriptionInfo
{
    public Type HandlerType { get; }
    public string EventName { get; }

    private SubscriptionInfo(Type handlerType, string eventName)
    {
        HandlerType = handlerType;
        EventName = eventName;
    }

    public static SubscriptionInfo Typed<TH>() where TH : class
    {
        var handlerType = typeof(TH);
        var eventName = typeof(TH).Name;
        return new SubscriptionInfo(handlerType, eventName);
    }
}