import type { PaymentResult } from '../../shared/models.js';
import type { PaymentStrategyFactory } from './payment-strategy-factory.js';

// Checkout orchestrates the workflow. It does NOT instantiate objects itself.
export class CheckoutService {
  constructor(private readonly factory: PaymentStrategyFactory) {}

  checkout(method: string, amount: number): PaymentResult {
    return this.factory.create(method).pay(amount); // pick + run
  }
}
