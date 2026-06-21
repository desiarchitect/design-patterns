using DesiArchitect.DesignPatterns.Shared;

namespace DesiArchitect.DesignPatterns.Facade.After;

public class SeatService : ISeatService
{
    public bool Reserve(BookingRequest request)
    {
        Console.WriteLine($"  🪑 SeatService: Reserved seat on train {request.TrainNumber}");
        return true;
    }
}
