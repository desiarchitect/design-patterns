import type { PaymentResult } from '../../shared/models.js';

// One contract. Every payment provider is an interchangeable implementation.
export interface IPaymentStrategy {
  pay(amount: number): PaymentResult;
}
