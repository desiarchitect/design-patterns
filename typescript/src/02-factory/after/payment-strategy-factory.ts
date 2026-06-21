import type { IPaymentStrategy } from '../../01-strategy/after/payment-strategy.js';
import { UpiPaymentStrategy } from '../../01-strategy/after/upi-payment-strategy.js';
import { RazorpayPaymentStrategy } from '../../01-strategy/after/razorpay-payment-strategy.js';
import { PhonePePaymentStrategy } from '../../01-strategy/after/phonepe-payment-strategy.js';

// One place that decides which object gets created. New provider? One line here.
// The essence is NOT the switch - it is centralizing creation. A dictionary lookup
// or a DI container would serve the same purpose.
export class PaymentStrategyFactory {
  create(method: string): IPaymentStrategy {
    switch (method) {
      case 'upi': return new UpiPaymentStrategy();
      case 'razorpay': return new RazorpayPaymentStrategy();
      case 'phonepe': return new PhonePePaymentStrategy();
      default: throw new Error(`Unknown method: ${method}`);
    }
  }
}
