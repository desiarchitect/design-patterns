import type { Parcel, TrackingId } from '../../shared/models.js';

export interface ILogisticsPartner {
  ship(parcel: Parcel): TrackingId;
}