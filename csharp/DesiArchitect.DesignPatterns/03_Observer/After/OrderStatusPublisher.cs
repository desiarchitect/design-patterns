using DesiArchitect.DesignPatterns.Shared;

namespace DesiArchitect.DesignPatterns.Observer.After;

public class OrderStatusPublisher
{
    private readonly List<IOrderStatusSubscriber> _subscribers = new();

    public void Subscribe(IOrderStatusSubscriber subscriber) => _subscribers.Add(subscriber);

    public void SetStatus(Order order, string status)
    {
        order.Status = status;
        Console.WriteLine($"  📢 Publisher: Order {order.Id} status -> {status}");
        foreach (var subscriber in _subscribers)
            Task.Run(() => subscriber.OnStatusChanged(order));
        Console.WriteLine("  [After] Publisher main thread free - subscribers running in background");
    }
}
