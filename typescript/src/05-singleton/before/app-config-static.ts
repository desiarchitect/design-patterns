export class AppConfigStatic {
  private static _instance: AppConfigStatic | undefined;

  static get instance(): AppConfigStatic {
    if (!AppConfigStatic._instance) {
      AppConfigStatic._instance = new AppConfigStatic();
    }
    return AppConfigStatic._instance;
  }

  readonly apiKey = 'sk-live-demo-key-12345';
  readonly databaseConnection = 'Server=prod-db;Database=Orders;';

  private constructor() {
    console.log('  [Before] AppConfigStatic created (static instance)');
  }

  showConfig(): void {
    console.log('  [Before] AppConfig.Instance accessed - tightly coupled, unmockable');
    console.log(`     API Key: ${this.apiKey}`);
    console.log(`     DB Conn: ${this.databaseConnection}`);
  }
}