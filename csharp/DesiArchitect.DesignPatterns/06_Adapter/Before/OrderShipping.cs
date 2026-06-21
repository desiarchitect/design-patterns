using DesiArchitect.DesignPatterns.Shared;

namespace DesiArchitect.DesignPatterns.Adapter.Before;

// Direct coupling to every partner's unique API shape
public class OrderShipping
{
    public void Ship(string partner, Parcel parcel)
    {
        if (partner == "bluedart")                          // partner-specific branch
        {
            Console.WriteLine($"  [Before] Calling Bluedart.CreateConsignment()");
            Console.WriteLine($"     Ref: {parcel.OrderId}, Dest: {parcel.Address}, Wt: {parcel.WeightKg}kg");
            Console.WriteLine($"     AWB: BD-{parcel.OrderId}-001");
        }
        else if (partner == "delhivery")                    // completely different method shape
        {
            Console.WriteLine($"  [Before] Calling Delhivery.DispatchShipment()");
            Console.WriteLine($"     Waybill: {parcel.OrderId}, Drop: {parcel.Address}, Pkg: {parcel.WeightKg}kg");
            Console.WriteLine($"     Tracking: DL-{parcel.OrderId}-001");
        }
        else if (partner == "ekart")                        // yet another shape
        {
            Console.WriteLine($"  [Before] Calling Ekart.InitiateDelivery()");
            Console.WriteLine($"     Order: {parcel.OrderId}, Addr: {parcel.Address}");
        }
        // New partner = edit this file. One wrong branch = all shipping breaks.
        else
        {
            throw new ArgumentException($"Unknown logistics partner: {partner}");
        }
    }
}
