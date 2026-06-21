using DesiArchitect.DesignPatterns.Shared;

namespace DesiArchitect.DesignPatterns.Observer.Before;

// One god method that handles every downstream action when status changes
public class OrderManager
{
    public void UpdateOrderStatus(Order order, string newStatus)
    {
        order.Status = newStatus;
        Console.WriteLine($"  [Before] Order {order.Id} status -> {newStatus}");

        // SMS notification - hardcoded inline (simulated slow gateway blocks everything below)
        Console.WriteLine($"  [Before] SMS gateway slow (5s)...");
        Thread.Sleep(5000);
        Console.WriteLine($"  [Before] Sending SMS to {order.Phone}: Your order is now {newStatus}");

        // Analytics tracking - crammed in the same method
        Console.WriteLine($"  [Before] Tracking analytics: Order {order.Id} -> {newStatus}");

        // Rider notification - yet another responsibility
        Console.WriteLine($"  [Before] Notifying rider about order {order.Id}");

        // Dashboard refresh inlined in the same method
        Console.WriteLine($"  [Before] Refreshing dashboard for order {order.Id}");

        // New notification channel requires editing this method again
        // SMS service is down? Analytics stops too - everything is coupled.
    }
}
