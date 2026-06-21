import type { Order } from '../../shared/models.js';

// Synchronous sleep - blocks the event loop, the Node analog of C#'s Thread.Sleep.
function sleepSync(ms: number): void {
  Atomics.wait(new Int32Array(new SharedArrayBuffer(4)), 0, 0, ms);
}

// One god method that handles every downstream action when status changes.
export class OrderManager {
  updateOrderStatus(order: Order, newStatus: string): void {
    order.status = newStatus;
    console.log(`  [Before] Order ${order.id} status -> ${newStatus}`);

    // SMS notification inline - a slow gateway blocks everything below it.
    console.log('  [Before] SMS gateway slow (5s)...');
    sleepSync(5000);
    console.log(`  [Before] Sending SMS to ${order.phone}: Your order is now ${newStatus}`);

    // Analytics crammed into the same method.
    console.log(`  [Before] Tracking analytics: Order ${order.id} -> ${newStatus}`);

    // Rider notification - yet another responsibility.
    console.log(`  [Before] Notifying rider about order ${order.id}`);

    // Dashboard refresh inlined in the same method
    console.log(`  [Before] Refreshing dashboard for order ${order.id}`);

    // New channel requires editing this method again. Slow SMS blocks analytics too.
  }
}
