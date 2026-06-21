using Microsoft.Extensions.DependencyInjection;
using DesiArchitect.DesignPatterns.Shared;

using StrategyAfter  = DesiArchitect.DesignPatterns.Strategy.After;
using FactoryAfter   = DesiArchitect.DesignPatterns.Factory.After;
using ObserverAfter  = DesiArchitect.DesignPatterns.Observer.After;
using DecoratorAfter = DesiArchitect.DesignPatterns.Decorator.After;
using SingletonAfter = DesiArchitect.DesignPatterns.Singleton.After;
using AdapterAfter   = DesiArchitect.DesignPatterns.Adapter.After;
using FacadeAfter    = DesiArchitect.DesignPatterns.Facade.After;

namespace DesiArchitect.DesignPatterns.Tests;

// Each test verifies a property of the "After" code:
// swappable, testable-with-fakes, one singleton instance, correct totals, clean orchestration.

public class StrategyTests
{
    private sealed class RecordingStrategy : StrategyAfter.IPaymentStrategy
    {
        public decimal? Charged { get; private set; }
        public PaymentResult Pay(decimal amount) { Charged = amount; return new PaymentResult(true, "ok"); }
    }

    [Fact]
    public void Context_uses_injected_strategy_without_knowing_its_type()
    {
        var fake = new RecordingStrategy();
        var checkout = new StrategyAfter.CheckoutService(fake);   // swap in any strategy

        var result = checkout.Checkout(500m);

        Assert.True(result.Success);
        Assert.Equal(500m, fake.Charged);                    // the context delegated to it
    }
}

public class FactoryTests
{
    [Theory]
    [InlineData("upi", typeof(StrategyAfter.UpiPaymentStrategy))]
    [InlineData("razorpay", typeof(StrategyAfter.RazorpayPaymentStrategy))]
    [InlineData("phonepe", typeof(StrategyAfter.PhonePePaymentStrategy))]
    public void Create_returns_the_strategy_for_the_method(string method, Type expected)
        => Assert.IsType(expected, new FactoryAfter.PaymentStrategyFactory().Create(method));

    [Fact]
    public void Create_unknown_method_throws()
        => Assert.Throws<ArgumentException>(() => new FactoryAfter.PaymentStrategyFactory().Create("bitcoin"));
}

public class ObserverTests
{
    private sealed class CountingSubscriber : ObserverAfter.IOrderStatusSubscriber
    {
        public int Calls { get; private set; }
        public string? LastStatus { get; private set; }
        public void OnStatusChanged(Order order) { Calls++; LastStatus = order.Status; }
    }

    [Fact]
    public void Publisher_notifies_every_subscriber_once_with_the_new_status()
    {
        var a = new CountingSubscriber();
        var b = new CountingSubscriber();
        var publisher = new ObserverAfter.OrderStatusPublisher();
        publisher.Subscribe(a);
        publisher.Subscribe(b);

        publisher.SetStatus(new Order { Id = "ORD-1" }, "Out for Delivery");
        // SetStatus fires subscribers via Task.Run (demo fire-and-forget)
        Thread.Sleep(300);

        Assert.Equal(1, a.Calls);
        Assert.Equal(1, b.Calls);
        Assert.Equal("Out for Delivery", a.LastStatus);
        Assert.Equal("Out for Delivery", b.LastStatus);
    }
}

public class DecoratorTests
{
    [Fact]
    public void Wrapping_layers_sum_correctly()
    {
        DecoratorAfter.IBill bill = new DecoratorAfter.BaseBill(450m);
        bill = new DecoratorAfter.PackagingDecorator(bill);   // +15
        bill = new DecoratorAfter.DeliveryDecorator(bill);    // +40
        bill = new DecoratorAfter.PlatformFeeDecorator(bill); // +5

        Assert.Equal(510m, bill.Total());
    }

    [Fact]
    public void Base_bill_alone_is_the_item_total()
        => Assert.Equal(450m, new DecoratorAfter.BaseBill(450m).Total());
}

public class SingletonTests
{
    private sealed class FakeConfig : SingletonAfter.IAppConfig
    {
        public string ApiKey => "test-key";
        public string DatabaseConnection => "test-db";
    }

    [Fact]
    public void AddSingleton_resolves_the_same_instance_every_time()
    {
        using var provider = new ServiceCollection()
            .AddSingleton<SingletonAfter.IAppConfig, SingletonAfter.AppConfig>()
            .BuildServiceProvider();

        Assert.Same(provider.GetRequiredService<SingletonAfter.IAppConfig>(),
                    provider.GetRequiredService<SingletonAfter.IAppConfig>());
    }

    [Fact]
    public void OrderService_is_testable_with_an_injected_fake_config()
    {
        var sw = new StringWriter();
        var original = Console.Out;
        Console.SetOut(sw);
        try { new SingletonAfter.OrderService(new FakeConfig()).PlaceOrder("Laptop"); }
        finally { Console.SetOut(original); }

        Assert.Contains("test-key", sw.ToString());   // the fake flowed through - no static, fully mockable
    }
}

public class AdapterTests
{
    [Fact]
    public void Bluedart_adapter_maps_the_vendor_response_into_our_TrackingId()
    {
        var adapter = new AdapterAfter.BluedartAdapter(new AdapterAfter.BluedartClient());

        var tracking = adapter.Ship(new Parcel("ORD-1", "Koramangala", 1.0m));

        Assert.Equal("BD-ORD-1-AWB", tracking.Value);
    }
}

public class FacadeTests
{
    private sealed class OkSeat : FacadeAfter.ISeatService { public bool Reserve(BookingRequest r) => true; }
    private sealed class NoSeat : FacadeAfter.ISeatService { public bool Reserve(BookingRequest r) => false; }
    private sealed class NoopPay : FacadeAfter.IPaymentService { public void Charge(BookingRequest r) { } }
    private sealed class FixedPnr : FacadeAfter.IPnrService { public string Generate(BookingRequest r) => "PNR-TEST"; }
    private sealed class NoopNotify : FacadeAfter.INotificationService { public void Send(string phone, string pnr) { } }

    private static readonly BookingRequest Request = new("12302 Rajdhani", "Rahul", "+91-90000-XXXX", 2450m);

    [Fact]
    public void BookTicket_confirms_and_orchestrates_when_a_seat_is_reserved()
    {
        var facade = new FacadeAfter.BookingFacade(new OkSeat(), new NoopPay(), new FixedPnr(), new NoopNotify());

        var result = facade.BookTicket(Request);

        Assert.True(result.IsConfirmed);
        Assert.Equal("PNR-TEST", result.Pnr);
    }

    [Fact]
    public void BookTicket_returns_NoSeats_and_short_circuits_when_no_seat()
    {
        var facade = new FacadeAfter.BookingFacade(new NoSeat(), new NoopPay(), new FixedPnr(), new NoopNotify());

        var result = facade.BookTicket(Request);

        Assert.False(result.IsConfirmed);
        Assert.Null(result.Pnr);
    }
}
