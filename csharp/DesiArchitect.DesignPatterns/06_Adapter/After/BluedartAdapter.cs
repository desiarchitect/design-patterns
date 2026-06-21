using DesiArchitect.DesignPatterns.Shared;

namespace DesiArchitect.DesignPatterns.Adapter.After;

public class BluedartAdapter : ILogisticsPartner
{
    private readonly BluedartClient _client;

    public BluedartAdapter(BluedartClient client) => _client = client;

    public TrackingId Ship(Parcel parcel)
    {
        Console.WriteLine($"  🔄 BluedartAdapter: translating Parcel -> BluedartConsignmentRequest");
        // The adapter owns the translation - our domain Parcel never knows the vendor's shape.
        var request = new BluedartConsignmentRequest(parcel.OrderId, parcel.Address, parcel.WeightKg);
        var awb = _client.CreateConsignment(request);
        return new TrackingId(awb.Number);
    }
}
