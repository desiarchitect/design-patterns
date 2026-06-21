using DesiArchitect.DesignPatterns.Shared;

namespace DesiArchitect.DesignPatterns.Facade.After;

public class PaymentService : IPaymentService
{
    public void Charge(BookingRequest request)
        => Console.WriteLine($"  PaymentService: Charged ₹{request.Fare} for {request.PassengerName}");
}
