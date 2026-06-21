using DesiArchitect.DesignPatterns.Shared;

namespace DesiArchitect.DesignPatterns.Adapter.After;

public interface ILogisticsPartner
{
    TrackingId Ship(Parcel parcel);
}
