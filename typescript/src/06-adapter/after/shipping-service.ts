import type { Parcel, TrackingId } from '../../shared/models.js';
import type { ILogisticsPartner } from './ilogistics-partner.js';

export class ShippingService {
  constructor(private readonly partner: ILogisticsPartner) {}

  dispatch(parcel: Parcel): TrackingId {
    return this.partner.ship(parcel);
  }
}