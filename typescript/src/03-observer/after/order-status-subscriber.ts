import type { Order } from '../../shared/models.js';

// One contract every downstream channel implements.
export interface IOrderStatusSubscriber {
  onStatusChanged(order: Order): Promise<void>;
}
