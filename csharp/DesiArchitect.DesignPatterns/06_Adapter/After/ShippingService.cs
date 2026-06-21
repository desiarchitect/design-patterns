using DesiArchitect.DesignPatterns.Shared;

namespace DesiArchitect.DesignPatterns.Adapter.After;

public class ShippingService
{
    private readonly ILogisticsPartner _partner;

    public ShippingService(ILogisticsPartner partner) => _partner = partner;

    public TrackingId Dispatch(Parcel parcel) => _partner.Ship(parcel);
}
