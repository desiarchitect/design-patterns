import type { INotificationService } from './i-notification-service.js';

export class NotificationService implements INotificationService {
  send(phone: string, pnr: string): void {
    console.log(`  [After] NotificationService: SMS sent to ${phone} - PNR: ${pnr}`);
  }
}