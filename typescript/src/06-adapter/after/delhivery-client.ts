export interface DelhiveryShipmentPayload {
  waybillRef: string;
  dropAddress: string;
  packageWeight: number;
}

export interface DelhiveryTrackingResponse {
  trackingNumber: string;
}

export class DelhiveryClient {
  dispatchShipment(payload: DelhiveryShipmentPayload): DelhiveryTrackingResponse {
    console.log(`     [Delhivery SDK] DispatchShipment -> Waybill:${payload.waybillRef}, Drop:${payload.dropAddress}`);
    return { trackingNumber: `DL-${payload.waybillRef}-TRK` };
  }
}