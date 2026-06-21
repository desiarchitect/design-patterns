using DesiArchitect.DesignPatterns.Shared;

namespace DesiArchitect.DesignPatterns.Observer.After;

public class SmsSubscriber : IOrderStatusSubscriber
{
    public void OnStatusChanged(Order order)
    {
        Thread.Sleep(2000);
        Console.WriteLine($"  SMS -> {order.Phone}: Order {order.Id} is now {order.Status}");
    }
}
