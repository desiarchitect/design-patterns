namespace DesiArchitect.DesignPatterns.Decorator.After;

public class DeliveryDecorator : IBill
{
    private readonly IBill _inner;
    private readonly decimal _charge = 40m;

    public DeliveryDecorator(IBill inner) => _inner = inner;

    public decimal Total()
    {
        var subtotal = _inner.Total();
        var result = subtotal + _charge;
        Console.WriteLine($"  🚚 + Delivery: ₹{_charge} -> ₹{result}");
        return result;
    }
}
