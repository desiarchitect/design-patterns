import type { BookingRequest } from '../../shared/models.js';

export interface ISeatService {
  reserve(request: BookingRequest): boolean;
}