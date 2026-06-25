import type { BookingRequest } from '../../shared/models.js';
import type { IPnrService } from './i-pnr-service.js';

export class PnrService implements IPnrService {
  generate(request: BookingRequest): string {
    const pnr = `PNR-${Math.floor(100000 + Math.random() * 900000)}`;
    console.log(`  🎫 PnrService: Generated ${pnr} for ${request.passengerName}`);
    return pnr;
  }
}