namespace DesiArchitect.DesignPatterns.Facade.After;

public class NotificationService : INotificationService
{
    public void Send(string phone, string pnr)
        => Console.WriteLine($"  [After] NotificationService: SMS sent to {phone} - PNR: {pnr}");
}
