using DesiArchitect.DesignPatterns.Shared;

namespace DesiArchitect.DesignPatterns.Facade.Before;

// The 300-line controller method - everything inline
public class BookingController
{
    public BookingResult BookTicket(BookingRequest req)
    {
        // Step 1: Seat availability - inline
        Console.WriteLine($"  [Before] Checking seat availability for train {req.TrainNumber}...");
        var seatsAvailable = true;  // simplified
        if (!seatsAvailable)
        {
            Console.WriteLine($"  [Before] No seats available!");
            return BookingResult.NoSeats;
        }
        Console.WriteLine($"  [Before] Seats available ✓");

        // Step 2: Fare calculation - inline
        Console.WriteLine($"  [Before] Calculating fare...");
        var fare = req.Fare;
        Console.WriteLine($"  [Before] Fare: ₹{fare} ✓");

        // Step 3: Payment processing - inline
        Console.WriteLine($"  [Before] Processing payment of ₹{fare}...");
        Console.WriteLine($"  [Before] Payment successful ✓");

        // Step 4: PNR generation - inline
        var pnr = $"PNR-{Random.Shared.Next(100000, 999999)}";
        Console.WriteLine($"  [Before] Generated PNR: {pnr} ✓");

        // Step 5: SMS notification - inline
        Console.WriteLine($"  [Before] Sending SMS to {req.Phone}: Booking confirmed! PNR: {pnr}");

        // Step 6: Waitlist handling - inline
        Console.WriteLine($"  [Before] Updating waitlist registry...");

        // Six responsibilities in one method; onboarding cost is high
        // Change anything = risk breaking everything.

        return BookingResult.Confirmed(pnr);
    }
}
