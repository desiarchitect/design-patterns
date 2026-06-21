namespace DesiArchitect.DesignPatterns.Singleton.Before;

// Textbook singleton: simple syntax, tight coupling, hard to test
public class AppConfigStatic
{
    private static AppConfigStatic? _instance;

    public static AppConfigStatic Instance => _instance ??= new AppConfigStatic();  // global access point

    public string ApiKey => "sk-live-demo-key-12345";
    public string DatabaseConnection => "Server=prod-db;Database=Orders;";

    private AppConfigStatic()
    {
        Console.WriteLine("  [Before] AppConfigStatic created (static instance)");
    }

    public void ShowConfig()
    {
        Console.WriteLine($"  [Before] AppConfig.Instance accessed - tightly coupled, unmockable");
        Console.WriteLine($"     API Key: {ApiKey}");
        Console.WriteLine($"     DB Conn: {DatabaseConnection}");
    }
}
