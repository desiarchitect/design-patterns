import type { IPaymentStrategy } from './payment-strategy.js';
import type { PaymentResult } from '../../shared/models.js';

// Checkout is decoupled from the gateway. It only knows the contract: .pay(amount).
export class CheckoutService {
  constructor(private readonly payment: IPaymentStrategy) {}

  checkout(amount: number): PaymentResult {
    return this.payment.pay(amount);
  }
}
