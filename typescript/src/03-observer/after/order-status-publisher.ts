import type { IOrderStatusSubscriber } from './order-status-subscriber.js';
import type { Order } from '../../shared/models.js';

// Publisher holds a dynamic list of subscribers and broadcasts one event.
//    It has no idea who is listening.
export class OrderStatusPublisher {
  private readonly subscribers: IOrderStatusSubscriber[] = [];

  subscribe(subscriber: IOrderStatusSubscriber): void {
    this.subscribers.push(subscriber);
  }

  setStatus(order: Order, status: string): void {
    order.status = status;
    console.log(`  📢 Publisher: Order ${order.id} status -> ${status}`);

    // Fire-and-forget: we do NOT await the subscriber promises, so the main flow
    // returns immediately. This is the TS analog of C#'s Task.Run - DEMO ONLY.
    // Unawaited promise rejections are swallowed (see README); production uses a
    // proper queue (BullMQ / SQS / Kafka) with real error handling.
    for (const subscriber of this.subscribers) {
      void subscriber.onStatusChanged(order);
    }

    console.log('  [After] Publisher main thread free - subscribers running in background');
  }
}
