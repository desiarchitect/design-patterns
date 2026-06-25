import type { IBill } from './ibill.js';

export class PackagingDecorator implements IBill {
  private readonly charge = 15;

  constructor(private readonly inner: IBill) {}

  total(): number {
    const subtotal = this.inner.total();
    const result = subtotal + this.charge;
    console.log(`  📦 + Packaging: ₹${this.charge} -> ₹${result}`);
    return result;
  }
}