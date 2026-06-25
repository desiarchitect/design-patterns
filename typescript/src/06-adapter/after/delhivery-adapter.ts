import type { Parcel, TrackingId } from '../../shared/models.js';
import type { ILogisticsPartner } from './ilogistics-partner.js';
import { DelhiveryClient } from './delhivery-client.js';

export class DelhiveryAdapter implements ILogisticsPartner {
  constructor(private readonly client: DelhiveryClient) {}

  ship(parcel: Parcel): TrackingId {
    console.log('  🔄 DelhiveryAdapter: translating Parcel -> DelhiveryShipmentPayload');
    const payload = {
      waybillRef: parcel.orderId,
      dropAddress: parcel.address,
      packageWeight: parcel.weightKg,
    };
    const response = this.client.dispatchShipment(payload);
    return { value: response.trackingNumber };
  }
}