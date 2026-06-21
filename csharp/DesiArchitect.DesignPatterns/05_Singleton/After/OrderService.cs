namespace DesiArchitect.DesignPatterns.Singleton.After;

public class OrderService
{
    private readonly IAppConfig _config;

    public OrderService(IAppConfig config) => _config = config;

    public void PlaceOrder(string item)
    {
        Console.WriteLine($"  [After] OrderService using injected IAppConfig");
        Console.WriteLine($"     Placing order for {item} using key: {_config.ApiKey}");
        Console.WriteLine($"     Testable - inject a fake IAppConfig in unit tests");
    }
}
