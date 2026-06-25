import type { BookingRequest, BookingResult } from '../../shared/models.js';
import { BookingResults } from '../../shared/models.js';
import type { ISeatService } from './i-seat-service.js';
import type { IPaymentService } from './i-payment-service.js';
import type { IPnrService } from './i-pnr-service.js';
import type { INotificationService } from './i-notification-service.js';

export class BookingFacade {
  constructor(
    private readonly seats: ISeatService,
    private readonly payment: IPaymentService,
    private readonly pnr: IPnrService,
    private readonly notify: INotificationService,
  ) {}

  bookTicket(req: BookingRequest): BookingResult {
    if (!this.seats.reserve(req)) return BookingResults.noSeats();
    this.payment.charge(req);
    const pnr = this.pnr.generate(req);
    this.notify.send(req.phone, pnr);
    return BookingResults.confirmed(pnr);
  }
}