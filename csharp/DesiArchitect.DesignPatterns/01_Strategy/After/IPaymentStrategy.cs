using DesiArchitect.DesignPatterns.Shared;

namespace DesiArchitect.DesignPatterns.Strategy.After;

public interface IPaymentStrategy
{
    PaymentResult Pay(decimal amount);
}
