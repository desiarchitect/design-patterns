import type { IPaymentStrategy } from './payment-strategy.js';
import type { PaymentResult } from '../../shared/models.js';

// New provider: add a class without changing existing checkout code.
export class PhonePePaymentStrategy implements IPaymentStrategy {
  pay(amount: number): PaymentResult {
    console.log(`  [After] Charging ₹${amount} via PhonePe strategy`);
    return { success: true, message: 'PhonePe payment processed' };
  }
}
