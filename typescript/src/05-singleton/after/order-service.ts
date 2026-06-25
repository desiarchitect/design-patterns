import type { IAppConfig } from './i-app-config.js';

export class OrderService {
  constructor(private readonly config: IAppConfig) {}

  placeOrder(item: string): void {
    console.log('  [After] OrderService using injected IAppConfig');
    console.log(`     Placing order for ${item} using key: ${this.config.apiKey}`);
    console.log('     Testable - inject a fake IAppConfig in unit tests');
  }
}