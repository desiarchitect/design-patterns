import type { IBill } from './ibill.js';

export class PlatformFeeDecorator implements IBill {
  private readonly fee = 5;

  constructor(private readonly inner: IBill) {}

  total(): number {
    const subtotal = this.inner.total();
    const result = subtotal + this.fee;
    console.log(`  🏷️  + Platform fee: ₹${this.fee} -> ₹${result}`);
    return result;
  }
}