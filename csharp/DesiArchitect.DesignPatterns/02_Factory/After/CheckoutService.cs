using DesiArchitect.DesignPatterns.Shared;

namespace DesiArchitect.DesignPatterns.Factory.After;

public class CheckoutService
{
    private readonly PaymentStrategyFactory _factory;

    public CheckoutService(PaymentStrategyFactory factory) => _factory = factory;

    public PaymentResult Checkout(string method, decimal amount)
        => _factory.Create(method).Pay(amount);
}
