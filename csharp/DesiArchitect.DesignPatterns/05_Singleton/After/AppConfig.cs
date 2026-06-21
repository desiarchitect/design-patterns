namespace DesiArchitect.DesignPatterns.Singleton.After;

public class AppConfig : IAppConfig
{
    public string ApiKey => "sk-live-demo-key-12345";
    public string DatabaseConnection => "Server=prod-db;Database=Orders;";

    public AppConfig()
    {
        Console.WriteLine("  [After] AppConfig created by DI container (singleton lifetime)");
    }
}
