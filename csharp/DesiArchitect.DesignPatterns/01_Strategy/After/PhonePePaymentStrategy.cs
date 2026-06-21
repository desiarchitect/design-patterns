using DesiArchitect.DesignPatterns.Shared;

namespace DesiArchitect.DesignPatterns.Strategy.After;

public class PhonePePaymentStrategy : IPaymentStrategy
{
    public PaymentResult Pay(decimal amount)
    {
        Console.WriteLine($"  [After] PhonePe payment of ₹{amount} processed via PhonePe gateway");
        return new PaymentResult(true, "PhonePe payment successful");
    }
}
