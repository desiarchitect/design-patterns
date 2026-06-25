import type { Parcel, TrackingId } from '../../shared/models.js';
import type { ILogisticsPartner } from './ilogistics-partner.js';
import { BluedartClient } from './bluedart-client.js';

export class BluedartAdapter implements ILogisticsPartner {
  constructor(private readonly client: BluedartClient) {}

  ship(parcel: Parcel): TrackingId {
    console.log('  🔄 BluedartAdapter: translating Parcel -> BluedartConsignmentRequest');
    const request = {
      refId: parcel.orderId,
      destination: parcel.address,
      weight: parcel.weightKg,
    };
    const awb = this.client.createConsignment(request);
    return { value: awb.number };
  }
}