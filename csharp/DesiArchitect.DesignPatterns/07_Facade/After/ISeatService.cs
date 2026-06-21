using DesiArchitect.DesignPatterns.Shared;

namespace DesiArchitect.DesignPatterns.Facade.After;

public interface ISeatService
{
    bool Reserve(BookingRequest request);
}
