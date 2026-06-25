using Microsoft.Extensions.DependencyInjection;

// Pattern namespaces
using StrategyBefore   = DesiArchitect.DesignPatterns.Strategy.Before;
using StrategyAfter    = DesiArchitect.DesignPatterns.Strategy.After;
using FactoryBefore    = DesiArchitect.DesignPatterns.Factory.Before;
using FactoryAfter     = DesiArchitect.DesignPatterns.Factory.After;
using ObserverBefore   = DesiArchitect.DesignPatterns.Observer.Before;
using ObserverAfter    = DesiArchitect.DesignPatterns.Observer.After;
using DecoratorBefore  = DesiArchitect.DesignPatterns.Decorator.Before;
using DecoratorAfter   = DesiArchitect.DesignPatterns.Decorator.After;
using SingletonBefore  = DesiArchitect.DesignPatterns.Singleton.Before;
using SingletonAfter   = DesiArchitect.DesignPatterns.Singleton.After;
using AdapterBefore    = DesiArchitect.DesignPatterns.Adapter.Before;
using AdapterAfter     = DesiArchitect.DesignPatterns.Adapter.After;
using FacadeBefore     = DesiArchitect.DesignPatterns.Facade.Before;
using FacadeAfter      = DesiArchitect.DesignPatterns.Facade.After;
using DesiArchitect.DesignPatterns.Shared;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// ── DI Container Setup ──────────────────────────────────────────────────────
var services = new ServiceCollection();

// Singleton pattern - the right way
services.AddSingleton<SingletonAfter.IAppConfig, SingletonAfter.AppConfig>();
services.AddTransient<SingletonAfter.OrderService>();

// Factory pattern
services.AddTransient<FactoryAfter.PaymentStrategyFactory>();
services.AddTransient<FactoryAfter.CheckoutService>();

// Adapter pattern is demoed with hand-built adapters in RunAdapter (to show both partners
// side by side), so it needs no DI registration here.

// Facade pattern
services.AddTransient<FacadeAfter.ISeatService, FacadeAfter.SeatService>();
services.AddTransient<FacadeAfter.IPaymentService, FacadeAfter.PaymentService>();
services.AddTransient<FacadeAfter.IPnrService, FacadeAfter.PnrService>();
services.AddTransient<FacadeAfter.INotificationService, FacadeAfter.NotificationService>();
services.AddTransient<FacadeAfter.BookingFacade>();

var provider = services.BuildServiceProvider();

// ── Interactive Menu ─────────────────────────────────────────────────────────
while (true)
{
    Console.WriteLine();
    Console.WriteLine("═══════════════════════════════════════════════════════");
    Console.WriteLine("  THE DESI ARCHITECT - 7 Design Patterns (.NET 10)");
    Console.WriteLine("═══════════════════════════════════════════════════════");
    Console.WriteLine("  1. Strategy    - Swappable Payment Methods");
    Console.WriteLine("  2. Factory     - Centralized Object Creation");
    Console.WriteLine("  3. Observer    - Event Broadcasting");
    Console.WriteLine("  4. Decorator   - Wrapping Layers");
    Console.WriteLine("  5. Singleton   - DI vs Static Instance");
    Console.WriteLine("  6. Adapter     - Third-Party Translation");
    Console.WriteLine("  7. Facade      - Simplified Entry Point");
    Console.WriteLine("  0. Exit");
    Console.WriteLine("═══════════════════════════════════════════════════════");
    Console.Write("  Choose a pattern (0-7): ");

    var input = Console.ReadLine()?.Trim();
    if (input is null) return;   // EOF (piped input exhausted / Ctrl+Z) - exit cleanly
    Console.WriteLine();

    switch (input)
    {
        case "0": return;
        case "1": RunStrategy(); break;
        case "2": RunFactory(provider); break;
        case "3": RunObserver(); break;
        case "4": RunDecorator(); break;
        case "5": RunSingleton(provider); break;
        case "6": RunAdapter(); break;
        case "7": RunFacade(provider); break;
        default: Console.WriteLine("  Invalid choice. Try again."); break;
    }

    if (!Console.IsInputRedirected)
    {
        Console.WriteLine("\n  Press any key to return to menu...");
        Console.ReadKey(true);
    }
}

// ── Pattern 1: Strategy ──────────────────────────────────────────────────────
void RunStrategy()
{
    PrintHeader("STRATEGY PATTERN", "Swappable Behaviors");

    PrintSubHeader("BEFORE - The if-else jungle");
    // Set SIMULATE_BLAST_RADIUS=1 to replay the Friday incident: one missing
    // vault key fails the shared static init and takes EVERY payment method down.
    try
    {
        var before = new StrategyBefore.PaymentService();
        before.Process("UPI", 500);
        before.Process("Card", 1200);
        before.Process("Wallet", 300);
        before.Process("NetBanking", 2500);
    }
    catch (TypeInitializationException ex)
    {
        Console.WriteLine($"  [error] TypeInitializationException: {ex.InnerException?.Message ?? ex.Message}");
        Console.WriteLine("  [error] PaymentService type poisoned - UPI, Card, Wallet all blocked (blast radius).");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"  [error] All payments down: {ex.Message}");
    }

    PrintSubHeader("AFTER - Clean strategy swap");
    // Each strategy is its own class - CheckoutService doesn't know or care which one
    var upiCheckout = new StrategyAfter.CheckoutService(new StrategyAfter.UpiPaymentStrategy());
    upiCheckout.Checkout(500);

    var razorpayCheckout = new StrategyAfter.CheckoutService(new StrategyAfter.RazorpayPaymentStrategy());
    razorpayCheckout.Checkout(1200);

    var phonepeCheckout = new StrategyAfter.CheckoutService(new StrategyAfter.PhonePePaymentStrategy());
    phonepeCheckout.Checkout(800);

    PrintInsight("New payment method? New class. CheckoutService stays untouched.");
}

// ── Pattern 2: Factory ───────────────────────────────────────────────────────
void RunFactory(IServiceProvider sp)
{
    PrintHeader("FACTORY PATTERN", "Centralized Object Creation");

    PrintSubHeader("BEFORE - 'new' keyword scattered everywhere");
    var before = new FactoryBefore.ScatteredCreation();
    before.ProcessCheckout("CheckoutService", "upi", 750);
    before.ProcessCheckout("RefundService", "razorpay", 2000);
    before.ProcessCheckout("SubscriptionService", "phonepe", 350);

    PrintSubHeader("AFTER - One factory, one place to create");
    var checkout = sp.GetRequiredService<FactoryAfter.CheckoutService>();
    checkout.Checkout("upi", 750);
    checkout.Checkout("razorpay", 2000);
    checkout.Checkout("phonepe", 350);

    PrintInsight("New provider? Add one line in the factory. Nothing else changes.");
}

// ── Pattern 3: Observer ──────────────────────────────────────────────────────
void RunObserver()
{
    PrintHeader("OBSERVER PATTERN", "Event Broadcasting");

    var order = new Order { Id = "ORD-42", Phone = "+91-90000-XXXX" };

    PrintSubHeader("BEFORE - God method with everything inline");
    var sw = System.Diagnostics.Stopwatch.StartNew();
    var before = new ObserverBefore.OrderManager();
    before.UpdateOrderStatus(order, "Preparing");
    sw.Stop();
    Console.WriteLine($"  Timing: Before blocked main thread for {sw.ElapsedMilliseconds}ms");

    order.Status = "Created";

    PrintSubHeader("AFTER - Publisher + independent subscribers (Task.Run demo)");
    sw.Restart();
    var publisher = new ObserverAfter.OrderStatusPublisher();
    publisher.Subscribe(new ObserverAfter.SmsSubscriber());
    publisher.Subscribe(new ObserverAfter.AnalyticsSubscriber());
    publisher.SetStatus(order, "Out for Delivery");
    sw.Stop();
    Console.WriteLine($"  Timing: After publisher returned in {sw.ElapsedMilliseconds}ms (subscribers still in background)");
    Thread.Sleep(2500);

    PrintInsight("New notification channel? New subscriber class. Publisher stays untouched.");
}

// ── Pattern 4: Decorator ─────────────────────────────────────────────────────
void RunDecorator()
{
    PrintHeader("DECORATOR PATTERN", "Wrapping Layers");

    PrintSubHeader("BEFORE - Boolean flag nightmare");
    var before = new DecoratorBefore.BillCalculator();
    var beforeTotal = before.CalculateBill(450, true, true, true, false);
    Console.WriteLine($"  Final bill: ₹{beforeTotal}");

    PrintSubHeader("BEFORE - Overlap bug (platform + convenience both on)");
    var overlapTotal = before.CalculateBill(450, true, true, true, false, hasConvenienceFee: true);
    Console.WriteLine($"  Final bill (overlap): ₹{overlapTotal}  <- silent double-fee when both branches active");

    PrintSubHeader("AFTER - Each charge is a clean wrapper");
    DecoratorAfter.IBill bill = new DecoratorAfter.BaseBill(450);
    bill = new DecoratorAfter.PackagingDecorator(bill);
    bill = new DecoratorAfter.DeliveryDecorator(bill);
    bill = new DecoratorAfter.PlatformFeeDecorator(bill);
    var afterTotal = bill.Total();
    Console.WriteLine($"  Final bill: ₹{afterTotal}");

    PrintInsight("New charge? New decorator class. Existing decorators stay untouched.");
}

// ── Pattern 5: Singleton ─────────────────────────────────────────────────────
void RunSingleton(IServiceProvider sp)
{
    PrintHeader("SINGLETON PATTERN", "DI vs Static Instance");

    PrintSubHeader("BEFORE - Static AppConfig.Instance everywhere");
    var beforeService = new SingletonBefore.OrderService();
    beforeService.PlaceOrder("Laptop");
    Console.WriteLine("  [Before] Mock karna practically mushkil - ugly hacks ya test isolation todna padega.");

    PrintSubHeader("AFTER - DI container manages the single instance");
    var service = sp.GetRequiredService<SingletonAfter.OrderService>();
    service.PlaceOrder("Laptop");

    // Resolve IAppConfig twice - AddSingleton guarantees the SAME instance both times
    var config1 = sp.GetRequiredService<SingletonAfter.IAppConfig>();
    var config2 = sp.GetRequiredService<SingletonAfter.IAppConfig>();
    Console.WriteLine($"  Same instance? {ReferenceEquals(config1, config2)} <- AddSingleton guarantee");

    PrintInsight("Shared state, not global access. Interface-driven, testable, clean.");
}

// ── Pattern 6: Adapter ───────────────────────────────────────────────────────
void RunAdapter()
{
    PrintHeader("ADAPTER PATTERN", "Third-Party Translation");

    var parcel = new Parcel("ORD-42", "Koramangala, Bangalore", 2.5m);

    PrintSubHeader("BEFORE - Direct coupling to every partner's API");
    var before = new AdapterBefore.OrderShipping();
    before.Ship("bluedart", parcel);
    before.Ship("delhivery", parcel);

    PrintSubHeader("AFTER - Adapters translate, ShippingService stays clean");
    Console.WriteLine("\n  ── Using Bluedart ──");
    var bluedartService = new AdapterAfter.ShippingService(
        new AdapterAfter.BluedartAdapter(new AdapterAfter.BluedartClient()));
    var tracking1 = bluedartService.Dispatch(parcel);
    Console.WriteLine($"  [After] Tracking: {tracking1.Value}");

    Console.WriteLine("\n  ── Using Delhivery ──");
    var delhiveryService = new AdapterAfter.ShippingService(
        new AdapterAfter.DelhiveryAdapter(new AdapterAfter.DelhiveryClient()));
    var tracking2 = delhiveryService.Dispatch(parcel);
    Console.WriteLine($"  [After] Tracking: {tracking2.Value}");

    PrintInsight("New partner? New adapter. ShippingService never changes.");
}

// ── Pattern 7: Facade ────────────────────────────────────────────────────────
void RunFacade(IServiceProvider sp)
{
    PrintHeader("FACADE PATTERN", "Simplified Entry Point");

    var request = new BookingRequest("12302 Rajdhani", "Rahul Sharma", "+91-90000-XXXX", 2450m);

    PrintSubHeader("BEFORE - 300-line controller method");
    var before = new FacadeBefore.BookingController();
    before.BookTicket(request);

    PrintSubHeader("AFTER - One clean facade call");
    var facade = sp.GetRequiredService<FacadeAfter.BookingFacade>();
    var result = facade.BookTicket(request);
    Console.WriteLine($"\n  Result: {result.Message}");

    PrintInsight("300 lines -> 1 line. Facade orchestrates, subsystems own the logic.");
}

// ── Helpers ──────────────────────────────────────────────────────────────────
void PrintHeader(string pattern, string subtitle)
{
    Console.WriteLine($"┌─────────────────────────────────────────────────┐");
    Console.WriteLine($"│  {pattern,-47}│");
    Console.WriteLine($"│  {subtitle,-47}│");
    Console.WriteLine($"└─────────────────────────────────────────────────┘");
}

void PrintSubHeader(string text)
{
    Console.WriteLine();
    Console.WriteLine($"  ── {text} ──");
    Console.WriteLine();
}

void PrintInsight(string text)
{
    Console.WriteLine();
    Console.WriteLine($"  💡 {text}");
}
