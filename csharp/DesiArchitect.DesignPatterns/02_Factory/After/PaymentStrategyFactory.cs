using DesiArchitect.DesignPatterns.Strategy.After;

namespace DesiArchitect.DesignPatterns.Factory.After;

public class PaymentStrategyFactory
{
    public IPaymentStrategy Create(string method) => method switch
    {
        "upi"      => new UpiPaymentStrategy(),
        "razorpay" => new RazorpayPaymentStrategy(),
        "phonepe"  => new PhonePePaymentStrategy(),
        _ => throw new ArgumentException($"Unknown method: {method}")
    };
}
