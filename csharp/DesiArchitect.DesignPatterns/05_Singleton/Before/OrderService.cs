namespace DesiArchitect.DesignPatterns.Singleton.Before;

// Any class using this is bound to the static instance
public class OrderService
{
    public void PlaceOrder(string item)
    {
        var config = AppConfigStatic.Instance;   // hardcoded dependency
        Console.WriteLine($"  [Before] OrderService using AppConfig.Instance directly");
        Console.WriteLine($"     Placing order for {item} using key: {config.ApiKey}");
        // Cannot mock this in unit tests
        // Cannot swap config for testing environment
    }
}
