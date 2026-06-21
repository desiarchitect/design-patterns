import type { IPaymentStrategy } from './payment-strategy.js';
import type { PaymentResult } from '../../shared/models.js';

export class RazorpayPaymentStrategy implements IPaymentStrategy {
  pay(amount: number): PaymentResult {
    console.log(`  [After] Charging ₹${amount} via Razorpay strategy`);
    return { success: true, message: 'Razorpay payment processed' };
  }
}
