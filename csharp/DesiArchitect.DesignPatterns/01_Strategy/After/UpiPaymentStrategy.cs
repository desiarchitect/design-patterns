using DesiArchitect.DesignPatterns.Shared;

namespace DesiArchitect.DesignPatterns.Strategy.After;

public class UpiPaymentStrategy : IPaymentStrategy
{
    public PaymentResult Pay(decimal amount)
    {
        Console.WriteLine($"  [After] UPI payment of ₹{amount} processed via NPCI gateway");
        return new PaymentResult(true, "UPI payment successful");
    }
}
