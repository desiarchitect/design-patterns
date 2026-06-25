// Minimal lifetime container - mirrors AddSingleton without extra dependencies
export class SimpleContainer {
  private readonly singletons = new Map<string, unknown>();

  addSingleton<T>(key: string, factory: () => T): void {
    if (!this.singletons.has(key)) {
      this.singletons.set(key, factory());
    }
  }

  resolve<T>(key: string): T {
    const instance = this.singletons.get(key);
    if (instance === undefined) {
      throw new Error(`Service not registered: ${key}`);
    }
    return instance as T;
  }
}