import type { IOrderStatusSubscriber } from './order-status-subscriber.js';
import type { Order } from '../../shared/models.js';

const sleep = (ms: number): Promise<void> => new Promise((r) => setTimeout(r, ms));

export class SmsSubscriber implements IOrderStatusSubscriber {
  async onStatusChanged(order: Order): Promise<void> {
    await sleep(1500); // slow SMS gateway - but now it runs in the background, not inline
    console.log(`  [After] SMS sent to ${order.phone}: order is now ${order.status}`);
  }
}
