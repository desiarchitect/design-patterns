using DesiArchitect.DesignPatterns.Shared;
using DesiArchitect.DesignPatterns.Strategy.After;

namespace DesiArchitect.DesignPatterns.Factory.Before;

// Object creation scattered everywhere - "new" keyword in 12 different files
public class ScatteredCreation
{
    public PaymentResult ProcessCheckout(string callSite, string method, decimal amount)
    {
        // Imagine this same block copy-pasted in OrderService, RefundService, SubscriptionService...
        IPaymentStrategy strategy;

        if (method == "upi")
            strategy = new UpiPaymentStrategy();           // new here
        else if (method == "razorpay")
            strategy = new RazorpayPaymentStrategy();      // new here
        else if (method == "phonepe")
            strategy = new PhonePePaymentStrategy();       // new here
        else
            throw new ArgumentException($"Unknown method: {method}");

        Console.WriteLine($"  [Before/{callSite}] new {strategy.GetType().Name}() inline - copy-paste block #{callSite}");
        return strategy.Pay(amount);
    }
}
