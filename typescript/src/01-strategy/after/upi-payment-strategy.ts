import type { IPaymentStrategy } from './payment-strategy.js';
import type { PaymentResult } from '../../shared/models.js';

export class UpiPaymentStrategy implements IPaymentStrategy {
  pay(amount: number): PaymentResult {
    console.log(`  [After] Charging ₹${amount} via UPI strategy`);
    return { success: true, message: 'UPI payment processed' };
  }
}
