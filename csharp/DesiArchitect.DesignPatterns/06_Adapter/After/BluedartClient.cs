namespace DesiArchitect.DesignPatterns.Adapter.After;

// Simulated Bluedart SDK - their API shape, not ours.
// The vendor's request/response types live with the vendor SDK, never in our domain layer.
public class BluedartClient
{
    public BluedartAwb CreateConsignment(BluedartConsignmentRequest request)
    {
        Console.WriteLine($"     [Bluedart SDK] CreateConsignment -> Ref:{request.RefId}, Dest:{request.Destination}");
        return new BluedartAwb($"BD-{request.RefId}-AWB");
    }
}

public record BluedartConsignmentRequest(string RefId, string Destination, decimal Weight);
public record BluedartAwb(string Number);
