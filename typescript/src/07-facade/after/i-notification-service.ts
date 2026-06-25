export interface INotificationService {
  send(phone: string, pnr: string): void;
}