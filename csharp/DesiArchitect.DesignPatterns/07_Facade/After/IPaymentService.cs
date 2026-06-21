using DesiArchitect.DesignPatterns.Shared;

namespace DesiArchitect.DesignPatterns.Facade.After;

public interface IPaymentService
{
    void Charge(BookingRequest request);
}
