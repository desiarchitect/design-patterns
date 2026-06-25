import { AppConfigStatic } from './app-config-static.js';

export class OrderService {
  placeOrder(item: string): void {
    const config = AppConfigStatic.instance;
    console.log('  [Before] OrderService using AppConfig.Instance directly');
    console.log(`     Placing order for ${item} using key: ${config.apiKey}`);
  }
}