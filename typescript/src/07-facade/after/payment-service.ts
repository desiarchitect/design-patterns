import type { BookingRequest } from '../../shared/models.js';
import type { IPaymentService } from './i-payment-service.js';

export class PaymentService implements IPaymentService {
  charge(request: BookingRequest): void {
    console.log(`  PaymentService: Charged ₹${request.fare} for ${request.passengerName}`);
  }
}