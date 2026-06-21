using DesiArchitect.DesignPatterns.Shared;

namespace DesiArchitect.DesignPatterns.Strategy.After;

public class RazorpayPaymentStrategy : IPaymentStrategy
{
    public PaymentResult Pay(decimal amount)
    {
        Console.WriteLine($"  [After] Razorpay payment of ₹{amount} processed via Razorpay API");
        return new PaymentResult(true, "Razorpay payment successful");
    }
}
