using DesiArchitect.DesignPatterns.Shared;

namespace DesiArchitect.DesignPatterns.Observer.After;

public class AnalyticsSubscriber : IOrderStatusSubscriber
{
    public void OnStatusChanged(Order order)
        => Console.WriteLine($"  Analytics tracked: Order {order.Id} -> {order.Status}");
}
