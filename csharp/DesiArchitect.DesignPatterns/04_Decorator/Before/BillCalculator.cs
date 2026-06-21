namespace DesiArchitect.DesignPatterns.Decorator.Before;

// Boolean flags for each charge; every new fee adds another if-block
public class BillCalculator
{
    public decimal CalculateBill(
        decimal itemTotal,
        bool hasPackaging,
        bool hasDelivery,
        bool hasPlatformFee,
        bool hasSurge)
    {
        var total = itemTotal;
        Console.WriteLine($"  [Before] Item total: ₹{total}");

        if (hasPackaging)                               // flag #1
        {
            total += 15;
            Console.WriteLine($"  [Before] + Packaging: ₹15 -> ₹{total}");
        }
        if (hasDelivery)                                // flag #2
        {
            total += 40;
            Console.WriteLine($"  [Before] + Delivery: ₹40 -> ₹{total}");
        }
        if (hasPlatformFee)                             // flag #3
        {
            total += 5;
            Console.WriteLine($"  [Before] + Platform fee: ₹5 -> ₹{total}");
        }
        if (hasSurge)                                   // flag #4
        {
            total += 20;
            Console.WriteLine($"  [Before] + Surge: ₹20 -> ₹{total}");
        }
        // New charge = new flag = new parameter = update every caller
        // Testing? 2^4 = 16 combinations. Add one more flag? 32.

        return total;
    }
}
