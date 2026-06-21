using DesiArchitect.DesignPatterns.Shared;

namespace DesiArchitect.DesignPatterns.Facade.After;

public interface IPnrService
{
    string Generate(BookingRequest request);
}
