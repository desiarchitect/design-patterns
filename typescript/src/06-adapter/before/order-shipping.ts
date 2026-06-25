import type { Parcel } from '../../shared/models.js';

export class OrderShipping {
  ship(partner: string, parcel: Parcel): void {
    if (partner === 'bluedart') {
      console.log('  [Before] Calling Bluedart.CreateConsignment()');
      console.log(`     Ref: ${parcel.orderId}, Dest: ${parcel.address}, Wt: ${parcel.weightKg}kg`);
      console.log(`     AWB: BD-${parcel.orderId}-001`);
    } else if (partner === 'delhivery') {
      console.log('  [Before] Calling Delhivery.DispatchShipment()');
      console.log(`     Waybill: ${parcel.orderId}, Drop: ${parcel.address}, Pkg: ${parcel.weightKg}kg`);
      console.log(`     Tracking: DL-${parcel.orderId}-001`);
    } else if (partner === 'ekart') {
      console.log('  [Before] Calling Ekart.InitiateDelivery()');
      console.log(`     Order: ${parcel.orderId}, Addr: ${parcel.address}`);
    } else {
      throw new Error(`Unknown logistics partner: ${partner}`);
    }
  }
}