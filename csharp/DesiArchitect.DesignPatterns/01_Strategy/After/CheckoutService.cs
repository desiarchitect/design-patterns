using DesiArchitect.DesignPatterns.Shared;

namespace DesiArchitect.DesignPatterns.Strategy.After;

public class CheckoutService
{
    private readonly IPaymentStrategy _payment;

    public CheckoutService(IPaymentStrategy payment) => _payment = payment;

    public PaymentResult Checkout(decimal amount) => _payment.Pay(amount);
}
