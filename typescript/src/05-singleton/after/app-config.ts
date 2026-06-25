import type { IAppConfig } from './i-app-config.js';

export class AppConfig implements IAppConfig {
  readonly apiKey = 'sk-live-demo-key-12345';
  readonly databaseConnection = 'Server=prod-db;Database=Orders;';

  constructor() {
    console.log('  [After] AppConfig created by container (singleton lifetime)');
  }
}