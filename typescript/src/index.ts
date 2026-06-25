import * as readline from 'node:readline/promises';
import { stdin as input, stdout as output } from 'node:process';
import { performance } from 'node:perf_hooks';
import type { Order } from './shared/models.js';

// Decorator
import { BillCalculator } from './04-decorator/before/bill-calculator.js';
import type { IBill } from './04-decorator/after/ibill.js';
import { BaseBill } from './04-decorator/after/base-bill.js';
import { PackagingDecorator } from './04-decorator/after/packaging-decorator.js';
import { DeliveryDecorator } from './04-decorator/after/delivery-decorator.js';
import { PlatformFeeDecorator } from './04-decorator/after/platform-fee-decorator.js';

// Singleton
import { OrderService as SingletonBeforeOrderService } from './05-singleton/before/order-service.js';
import { AppConfig } from './05-singleton/after/app-config.js';
import { OrderService as SingletonAfterOrderService } from './05-singleton/after/order-service.js';
import { SimpleContainer } from './05-singleton/after/simple-container.js';
import type { IAppConfig } from './05-singleton/after/i-app-config.js';

// Adapter
import { OrderShipping } from './06-adapter/before/order-shipping.js';
import { ShippingService } from './06-adapter/after/shipping-service.js';
import { BluedartAdapter } from './06-adapter/after/bluedart-adapter.js';
import { BluedartClient } from './06-adapter/after/bluedart-client.js';
import { DelhiveryAdapter } from './06-adapter/after/delhivery-adapter.js';
import { DelhiveryClient } from './06-adapter/after/delhivery-client.js';

// Facade
import { BookingController } from './07-facade/before/booking-controller.js';
import { BookingFacade } from './07-facade/after/booking-facade.js';
import { SeatService } from './07-facade/after/seat-service.js';
import { PaymentService as FacadePaymentService } from './07-facade/after/payment-service.js';
import { PnrService } from './07-facade/after/pnr-service.js';
import { NotificationService } from './07-facade/after/notification-service.js';

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

// ── Pattern 4: Decorator ─────────────────────────────────────────────────────
function runDecorator(): void {
  printHeader('DECORATOR PATTERN', 'Wrapping Layers');

  printSubHeader('BEFORE - Boolean flag nightmare');
  const before = new BillCalculator();
  const beforeTotal = before.calculateBill(450, true, true, true, false);
  console.log(`  Final bill: ₹${beforeTotal}`);

  printSubHeader('BEFORE - Overlap bug (platform + convenience both on)');
  const overlapTotal = before.calculateBill(450, true, true, true, false, true);
  console.log(`  Final bill (overlap): ₹${overlapTotal}  <- silent double-fee when both branches active`);

  printSubHeader('AFTER - Each charge is a clean wrapper');
  let bill: IBill = new BaseBill(450);
  bill = new PackagingDecorator(bill);
  bill = new DeliveryDecorator(bill);
  bill = new PlatformFeeDecorator(bill);
  const afterTotal = bill.total();
  console.log(`  Final bill: ₹${afterTotal}`);

  printInsight('New charge? New decorator class. Existing decorators stay untouched.');
}

// ── Pattern 5: Singleton ─────────────────────────────────────────────────────
function runSingleton(): void {
  printHeader('SINGLETON PATTERN', 'DI vs Static Instance');

  printSubHeader('BEFORE - Static AppConfig.Instance everywhere');
  new SingletonBeforeOrderService().placeOrder('Laptop');
  console.log('  [Before] Mock karna practically mushkil - ugly hacks ya test isolation todna padega.');

  printSubHeader('AFTER - DI container manages the single instance');
  const container = new SimpleContainer();
  container.addSingleton('IAppConfig', () => new AppConfig());
  const config = container.resolve<IAppConfig>('IAppConfig');
  new SingletonAfterOrderService(config).placeOrder('Laptop');

  const config1 = container.resolve<IAppConfig>('IAppConfig');
  const config2 = container.resolve<IAppConfig>('IAppConfig');
  console.log(`  Same instance? ${Object.is(config1, config2)} <- addSingleton guarantee`);

  printInsight('Shared state, not global access. Interface-driven, testable, clean.');
}

// ── Pattern 6: Adapter ───────────────────────────────────────────────────────
function runAdapter(): void {
  printHeader('ADAPTER PATTERN', 'Third-Party Translation');

  const parcel = { orderId: 'ORD-42', address: 'Koramangala, Bangalore', weightKg: 2.5 };

  printSubHeader('BEFORE - Direct coupling to every partner\'s API');
  const before = new OrderShipping();
  before.ship('bluedart', parcel);
  before.ship('delhivery', parcel);

  printSubHeader('AFTER - Adapters translate, ShippingService stays clean');
  console.log('\n  ── Using Bluedart ──');
  const bluedartService = new ShippingService(new BluedartAdapter(new BluedartClient()));
  const tracking1 = bluedartService.dispatch(parcel);
  console.log(`  [After] Tracking: ${tracking1.value}`);

  console.log('\n  ── Using Delhivery ──');
  const delhiveryService = new ShippingService(new DelhiveryAdapter(new DelhiveryClient()));
  const tracking2 = delhiveryService.dispatch(parcel);
  console.log(`  [After] Tracking: ${tracking2.value}`);

  printInsight('New partner? New adapter. ShippingService never changes.');
}

// ── Pattern 7: Facade ────────────────────────────────────────────────────────
function runFacade(): void {
  printHeader('FACADE PATTERN', 'Simplified Entry Point');

  const request = {
    trainNumber: '12302 Rajdhani',
    passengerName: 'Rahul Sharma',
    phone: '+91-90000-XXXX',
    fare: 2450,
  };

  printSubHeader('BEFORE - 300-line controller method');
  new BookingController().bookTicket(request);

  printSubHeader('AFTER - One clean facade call');
  const facade = new BookingFacade(
    new SeatService(),
    new FacadePaymentService(),
    new PnrService(),
    new NotificationService(),
  );
  const result = facade.bookTicket(request);
  console.log(`\n  Result: ${result.message}`);

  printInsight('300 lines -> 1 line. Facade orchestrates, subsystems own the logic.');
}

// ── Interactive menu (mirrors the C# Program.cs) ─────────────────────────────
async function main(): Promise<void> {
  const rl = readline.createInterface({ input, output });
  try {
    for (;;) {
      console.log();
      console.log('═══════════════════════════════════════════════════════');
      console.log('  THE DESI ARCHITECT - 7 Design Patterns (TypeScript)');
      console.log('═══════════════════════════════════════════════════════');
      console.log('  1. Strategy    - Swappable Payment Methods');
      console.log('  2. Factory     - Centralized Object Creation');
      console.log('  3. Observer    - Event Broadcasting');
      console.log('  4. Decorator   - Wrapping Layers');
      console.log('  5. Singleton   - DI vs Static Instance');
      console.log('  6. Adapter     - Third-Party Translation');
      console.log('  7. Facade      - Simplified Entry Point');
      console.log('  0. Exit');
      console.log('═══════════════════════════════════════════════════════');

      const choice = (await rl.question('  Choose a pattern (0-7): ')).trim();
      console.log();

      switch (choice) {
        case '0': return;
        case '1': await runStrategy(); break;
        case '2': runFactory(); break;
        case '3': await runObserver(); break;
        case '4': runDecorator(); break;
        case '5': runSingleton(); break;
        case '6': runAdapter(); break;
        case '7': runFacade(); break;
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
