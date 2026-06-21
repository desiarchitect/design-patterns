import type { PaymentResult } from '../../shared/models.js';
import type { IPaymentStrategy } from '../../01-strategy/after/payment-strategy.js';
import { UpiPaymentStrategy } from '../../01-strategy/after/upi-payment-strategy.js';
import { RazorpayPaymentStrategy } from '../../01-strategy/after/razorpay-payment-strategy.js';
import { PhonePePaymentStrategy } from '../../01-strategy/after/phonepe-payment-strategy.js';

// Object creation scattered everywhere - the "new" keyword copy-pasted across files.
export class ScatteredCreation {
  processCheckout(callSite: string, method: string, amount: number): PaymentResult {
    // Imagine this same block in OrderService, RefundService, SubscriptionService...
    let strategy: IPaymentStrategy;

    if (method === 'upi') strategy = new UpiPaymentStrategy();            // new here
    else if (method === 'razorpay') strategy = new RazorpayPaymentStrategy(); // new here
    else if (method === 'phonepe') strategy = new PhonePePaymentStrategy();   // new here
    else throw new Error(`Unknown method: ${method}`);

    console.log(`  [Before/${callSite}] new ${strategy.constructor.name}() inline - copy-paste block #${callSite}`);
    return strategy.pay(amount);
  }
}
