namespace DesiArchitect.DesignPatterns.Decorator.After;

public class BaseBill : IBill
{
    private readonly decimal _items;

    public BaseBill(decimal items) => _items = items;

    public decimal Total()
    {
        Console.WriteLine($"  Base item total: ₹{_items}");
        return _items;
    }
}
