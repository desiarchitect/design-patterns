namespace DesiArchitect.DesignPatterns.Singleton.After;

public interface IAppConfig
{
    string ApiKey { get; }
    string DatabaseConnection { get; }
}
