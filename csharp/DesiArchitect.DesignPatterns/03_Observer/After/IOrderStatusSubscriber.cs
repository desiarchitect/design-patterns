using DesiArchitect.DesignPatterns.Shared;

namespace DesiArchitect.DesignPatterns.Observer.After;

public interface IOrderStatusSubscriber
{
    void OnStatusChanged(Order order);
}
