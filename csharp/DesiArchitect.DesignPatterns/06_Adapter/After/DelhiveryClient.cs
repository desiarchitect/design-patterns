namespace DesiArchitect.DesignPatterns.Adapter.After;

// Simulated Delhivery SDK - completely different method names and shapes.
// Same point as Bluedart: the vendor's payload/response types belong with the vendor SDK.
public class DelhiveryClient
{
    public DelhiveryTrackingResponse DispatchShipment(DelhiveryShipmentPayload payload)
    {
        Console.WriteLine($"     [Delhivery SDK] DispatchShipment -> Waybill:{payload.WaybillRef}, Drop:{payload.DropAddress}");
        return new DelhiveryTrackingResponse($"DL-{payload.WaybillRef}-TRK");
    }
}

public record DelhiveryShipmentPayload(string WaybillRef, string DropAddress, decimal PackageWeight);
public record DelhiveryTrackingResponse(string TrackingNumber);
