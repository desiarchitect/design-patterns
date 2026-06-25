// Shared domain types - mirror of the C# Shared/Models.cs

export interface PaymentResult {
  success: boolean;
  message: string;
}

export interface Order {
  id: string;
  phone: string;
  status: string;
}

export interface Parcel {
  orderId: string;
  address: string;
  weightKg: number;
}

export interface TrackingId {
  value: string;
}

export interface BookingRequest {
  trainNumber: string;
  passengerName: string;
  phone: string;
  fare: number;
}

export interface BookingResult {
  isConfirmed: boolean;
  pnr?: string;
  message: string;
}

export const BookingResults = {
  noSeats(): BookingResult {
    return { isConfirmed: false, message: 'No seats available' };
  },
  confirmed(pnr: string): BookingResult {
    return { isConfirmed: true, pnr, message: `Booking confirmed! PNR: ${pnr}` };
  },
};