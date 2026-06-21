import * as readline from 'node:readline/promises';
import { stdin as input, stdout as output } from 'node:process';
import { performance } from 'node:perf_hooks';
import type { Order } from './shared/models.js';

// Strategy (After)
import { CheckoutService as StrategyCheckout } from './01-strategy/after/checkout-service.js';
import { UpiPaymentStrategy } from './01-strategy/after/upi-payment-strategy.js';
import { RazorpayPaymentStrategy } from './01-strategy/after/razorpay-payment-strategy.js';
import { PhonePePaymentStrategy } from './01-strategy/after/phonepe-payment-strategy.js';

// Factory
import { ScatteredCreation } from './02-factory/before/scattered-creation.js';
import { PaymentStrategyFactory } from './02-factory/after/payment-strategy-factory.js';
import { CheckoutService as FactoryCheckout } from './02-factory/after/checkout-service.js';

// Observer
import { OrderManager } from './03-observer/before/order-manager.js';
import { OrderStatusPublisher } from './03-observer/after/order-status-publisher.js';
import { SmsSubscriber } from './03-observer/after/sms-subscriber.js';
import { AnalyticsSubscriber } from './03-observer/after/analytics-subscriber.js';

const sleep = (ms: number): Promise<void> => new Promise((r) => setTimeout(r, ms));

// ── Pattern 1: Strategy ──────────────────────────────────────────────────────
async function runStrategy(): Promise<void> {
  printHeader('STRATEGY PATTERN', 'Swappable Behaviors');

  printSubHeader('BEFORE - The if-else jungle');
  // Set SIMULATE_BLAST_RADIUS=1 to replay the Friday incident: one missing vault key
  // throws at MODULE LOAD, so the dynamic import below fails and EVERY method is down.
  try {
    const mod = await import('./01-strategy/before/payment-service.js');
    const before = new mod.PaymentService();
    before.process('UPI', 500);
    before.process('Card', 1200);
    before.process('Wallet', 300);
    before.process('NetBanking', 2500);
  } catch (err) {
    console.log(`  [error] Module init failed: ${(err as Error).message}`);
    console.log('  [error] payment-service module poisoned - UPI, Card, Wallet all blocked (blast radius).');
  }

  printSubHeader('AFTER - Clean strategy swap');
  new StrategyCheckout(new UpiPaymentStrategy()).checkout(500);
  new StrategyCheckout(new RazorpayPaymentStrategy()).checkout(1200);
  new StrategyCheckout(new PhonePePaymentStrategy()).checkout(800);

  printInsight('New payment method? New class. CheckoutService stays untouched.');
}

// ── Pattern 2: Factory ───────────────────────────────────────────────────────
function runFactory(): void {
  printHeader('FACTORY PATTERN', 'Centralized Object Creation');

  printSubHeader("BEFORE - 'new' keyword scattered everywhere");
  const scattered = new ScatteredCreation();
  scattered.processCheckout('CheckoutService', 'upi', 750);
  scattered.processCheckout('RefundService', 'razorpay', 2000);
  scattered.processCheckout('SubscriptionService', 'phonepe', 350);

  printSubHeader('AFTER - One factory, one place to create');
  const checkout = new FactoryCheckout(new PaymentStrategyFactory());
  checkout.checkout('upi', 750);
  checkout.checkout('razorpay', 2000);
  checkout.checkout('phonepe', 350);

  printInsight('New provider? Add one line in the factory. Nothing else changes.');
}

// ── Pattern 3: Observer ──────────────────────────────────────────────────────
async function runObserver(): Promise<void> {
  printHeader('OBSERVER PATTERN', 'Event Broadcasting');

  const order: Order = { id: 'ORD-42', phone: '+91-90000-XXXX', status: 'Created' };

  printSubHeader('BEFORE - God method with everything inline');
  let t = performance.now();
  new OrderManager().updateOrderStatus(order, 'Preparing');
  console.log(`  Timing: Before blocked main thread for ${Math.round(performance.now() - t)}ms`);

  order.status = 'Created';

  printSubHeader('AFTER - Publisher + independent subscribers (fire-and-forget demo)');
  t = performance.now();
  const publisher = new OrderStatusPublisher();
  publisher.subscribe(new SmsSubscriber());
  publisher.subscribe(new AnalyticsSubscriber());
  publisher.setStatus(order, 'Out for Delivery');
  console.log(`  Timing: After publisher returned in ${Math.round(performance.now() - t)}ms (subscribers still in background)`);
  await sleep(2500); // let the background subscribers finish so their logs show

  printInsight('New notification channel? New subscriber class. Publisher stays untouched.');
}

// ── Interactive menu (mirrors the C# Program.cs) ─────────────────────────────
async function main(): Promise<void> {
  const rl = readline.createInterface({ input, output });
  try {
    for (;;) {
      console.log();
      console.log('═══════════════════════════════════════════════════════');
      console.log('  THE DESI ARCHITECT - PART 1: 3 Design Patterns (TypeScript)');
      console.log('═══════════════════════════════════════════════════════');
      console.log('  1. Strategy    - Swappable Payment Methods');
      console.log('  2. Factory     - Centralized Object Creation');
      console.log('  3. Observer    - Event Broadcasting');
      console.log('  0. Exit');
      console.log('═══════════════════════════════════════════════════════');

      const choice = (await rl.question('  Choose a pattern (0-3): ')).trim();
      console.log();

      switch (choice) {
        case '0': return;
        case '1': await runStrategy(); break;
        case '2': runFactory(); break;
        case '3': await runObserver(); break;
        default: console.log('  Invalid choice. Try again.'); break;
      }
    }
  } finally {
    rl.close();
  }
}

// ── Helpers ──────────────────────────────────────────────────────────────────
function printHeader(pattern: string, subtitle: string): void {
  console.log('┌─────────────────────────────────────────────────┐');
  console.log(`│  ${pattern.padEnd(47)}│`);
  console.log(`│  ${subtitle.padEnd(47)}│`);
  console.log('└─────────────────────────────────────────────────┘');
}

function printSubHeader(text: string): void {
  console.log();
  console.log(`  ── ${text} ──`);
  console.log();
}

function printInsight(text: string): void {
  console.log();
  console.log(`  💡 ${text}`);
}

main().catch((err) => {
  console.error(err);
  process.exit(1);
});
