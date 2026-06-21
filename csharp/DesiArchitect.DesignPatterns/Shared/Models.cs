namespace DesiArchitect.DesignPatterns.Shared;

public record PaymentResult(bool Success, string Message);

public class Order
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Phone { get; set; } = string.Empty;
    public string Status { get; set; } = "Created";
}

public record Parcel(string OrderId, string Address, decimal WeightKg);

public record TrackingId(string Value);

public record BookingRequest(string TrainNumber, string PassengerName, string Phone, decimal Fare);

public record BookingResult
{
    public bool IsConfirmed { get; init; }
    public string? Pnr { get; init; }
    public string Message { get; init; } = string.Empty;

    public static BookingResult NoSeats => new() { IsConfirmed = false, Message = "No seats available" };
    public static BookingResult Confirmed(string pnr) => new() { IsConfirmed = true, Pnr = pnr, Message = $"Booking confirmed! PNR: {pnr}" };
}
