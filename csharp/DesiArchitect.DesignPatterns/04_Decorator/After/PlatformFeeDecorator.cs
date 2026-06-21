namespace DesiArchitect.DesignPatterns.Decorator.After;

public class PlatformFeeDecorator : IBill
{
    private readonly IBill _inner;
    private readonly decimal _fee = 5.0m;

    public PlatformFeeDecorator(IBill inner) => _inner = inner;

    public decimal Total()
    {
        var subtotal = _inner.Total();
        var result = subtotal + _fee;
        Console.WriteLine($"  🏷️  + Platform fee: ₹{_fee} -> ₹{result}");
        return result;
    }
}
