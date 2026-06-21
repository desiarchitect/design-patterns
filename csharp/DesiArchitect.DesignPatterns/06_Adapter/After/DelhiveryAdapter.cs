using DesiArchitect.DesignPatterns.Shared;

namespace DesiArchitect.DesignPatterns.Adapter.After;

public class DelhiveryAdapter : ILogisticsPartner
{
    private readonly DelhiveryClient _client;

    public DelhiveryAdapter(DelhiveryClient client) => _client = client;

    public TrackingId Ship(Parcel parcel)
    {
        Console.WriteLine($"  🔄 DelhiveryAdapter: translating Parcel -> DelhiveryShipmentPayload");
        // Each vendor has a different shape; the adapter absorbs that difference, not the domain.
        var payload = new DelhiveryShipmentPayload(parcel.OrderId, parcel.Address, parcel.WeightKg);
        var response = _client.DispatchShipment(payload);
        return new TrackingId(response.TrackingNumber);
    }
}
