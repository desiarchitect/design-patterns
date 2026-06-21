import type { IOrderStatusSubscriber } from './order-status-subscriber.js';
import type { Order } from '../../shared/models.js';

const sleep = (ms: number): Promise<void> => new Promise((r) => setTimeout(r, ms));

export class AnalyticsSubscriber implements IOrderStatusSubscriber {
  async onStatusChanged(order: Order): Promise<void> {
    await sleep(300);
    console.log(`  [After] Analytics tracked: Order ${order.id} -> ${order.status}`);
  }
}
