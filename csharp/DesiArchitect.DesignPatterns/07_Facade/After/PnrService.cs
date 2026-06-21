using DesiArchitect.DesignPatterns.Shared;

namespace DesiArchitect.DesignPatterns.Facade.After;

public class PnrService : IPnrService
{
    public string Generate(BookingRequest request)
    {
        var pnr = $"PNR-{Random.Shared.Next(100000, 999999)}";
        Console.WriteLine($"  🎫 PnrService: Generated {pnr} for {request.PassengerName}");
        return pnr;
    }
}
