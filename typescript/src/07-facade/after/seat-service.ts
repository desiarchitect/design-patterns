import type { BookingRequest } from '../../shared/models.js';
import type { ISeatService } from './i-seat-service.js';

export class SeatService implements ISeatService {
  reserve(request: BookingRequest): boolean {
    console.log(`  🪑 SeatService: Reserved seat on train ${request.trainNumber}`);
    return true;
  }
}