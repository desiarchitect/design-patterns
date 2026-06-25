import type { BookingRequest } from '../../shared/models.js';

export interface IPaymentService {
  charge(request: BookingRequest): void;
}