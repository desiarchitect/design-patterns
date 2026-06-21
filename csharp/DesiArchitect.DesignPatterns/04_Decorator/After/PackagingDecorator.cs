namespace DesiArchitect.DesignPatterns.Decorator.After;

public class PackagingDecorator : IBill
{
    private readonly IBill _inner;
    private readonly decimal _charge = 15m;

    public PackagingDecorator(IBill inner) => _inner = inner;

    public decimal Total()
    {
        var subtotal = _inner.Total();
        var result = subtotal + _charge;
        Console.WriteLine($"  📦 + Packaging: ₹{_charge} -> ₹{result}");
        return result;
    }
}
