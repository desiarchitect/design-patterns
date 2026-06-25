import type { IBill } from './ibill.js';

export class BaseBill implements IBill {
  constructor(private readonly items: number) {}

  total(): number {
    console.log(`  Base item total: ₹${this.items}`);
    return this.items;
  }
}