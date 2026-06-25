import type { IBill } from './ibill.js';

export class DeliveryDecorator implements IBill {
  private readonly charge = 40;

  constructor(private readonly inner: IBill) {}

  total(): number {
    const subtotal = this.inner.total();
    const result = subtotal + this.charge;
    console.log(`  🚚 + Delivery: ₹${this.charge} -> ₹${result}`);
    return result;
  }
}