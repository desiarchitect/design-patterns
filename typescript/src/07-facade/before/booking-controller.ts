import type { BookingRequest, BookingResult } from '../../shared/models.js';
import { BookingResults } from '../../shared/models.js';

export class BookingController {
  bookTicket(req: BookingRequest): BookingResult {
    console.log(`  [Before] Checking seat availability for train ${req.trainNumber}...`);
    const seatsAvailable = true;
    if (!seatsAvailable) {
      console.log('  [Before] No seats available!');
      return BookingResults.noSeats();
    }
    console.log('  [Before] Seats available ✓');

    console.log('  [Before] Calculating fare...');
    const fare = req.fare;
    console.log(`  [Before] Fare: ₹${fare} ✓`);

    console.log(`  [Before] Processing payment of ₹${fare}...`);
    console.log('  [Before] Payment successful ✓');

    const pnr = `PNR-${Math.floor(100000 + Math.random() * 900000)}`;
    console.log(`  [Before] Generated PNR: ${pnr} ✓`);

    console.log(`  [Before] Sending SMS to ${req.phone}: Booking confirmed! PNR: ${pnr}`);
    console.log('  [Before] Updating waitlist registry...');

    return BookingResults.confirmed(pnr);
  }
}