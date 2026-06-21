using DesiArchitect.DesignPatterns.Shared;

namespace DesiArchitect.DesignPatterns.Facade.After;

public class BookingFacade
{
    private readonly ISeatService _seats;
    private readonly IPaymentService _payment;
    private readonly IPnrService _pnr;
    private readonly INotificationService _notify;

    public BookingFacade(ISeatService seats, IPaymentService payment,
                         IPnrService pnr, INotificationService notify)
        => (_seats, _payment, _pnr, _notify) = (seats, payment, pnr, notify);

    public BookingResult BookTicket(BookingRequest req)
    {
        if (!_seats.Reserve(req)) return BookingResult.NoSeats;
        _payment.Charge(req);
        var pnr = _pnr.Generate(req);
        _notify.Send(req.Phone, pnr);
        return BookingResult.Confirmed(pnr);
    }
}
