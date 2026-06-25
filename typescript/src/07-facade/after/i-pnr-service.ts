import type { BookingRequest } from '../../shared/models.js';

export interface IPnrService {
  generate(request: BookingRequest): string;
}