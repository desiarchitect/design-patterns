# 7 Design Patterns Every Senior Developer Uses

> Companion code for [The Desi Architect](https://youtube.com/@desiarchitect) YouTube video.

Pattern-by-pattern notes for this project. For setup and how to run (dotnet or Docker), see the
[repository README](../README.md).

## The 7 patterns

| # | Pattern | Category | Real-world example | Key interface |
|---|---------|----------|--------------------|---------------|
| 1 | Strategy  | Behavioral | PhonePe checkout - swap the payment engine | `IPaymentStrategy` |
| 2 | Factory   | Creational | One place decides which strategy to create | `PaymentStrategyFactory` |
| 3 | Observer  | Behavioral | Zomato live tracking - broadcast to subscribers | `IOrderStatusSubscriber` |
| 4 | Decorator | Structural | Swiggy bill - layers of charges | `IBill` |
| 5 | Singleton | Creational | Shared config - DI vs static Instance | `IAppConfig` |
| 6 | Adapter   | Structural | Bluedart / Delhivery - translate third-party APIs | `ILogisticsPartner` |
| 7 | Facade    | Structural | IRCTC booking - one button, many subsystems | `BookingFacade` |

The three categories are the standard Gang-of-Four buckets: **Creational** (how objects are made),
**Structural** (how objects are composed), **Behavioral** (how objects act and react).

## Project structure

Every pattern is a folder with a `Before/` (the anti-pattern) and an `After/` (the clean version).
Folders keep their `NN_` prefix so they read top-to-bottom; the namespaces are clean
(`DesiArchitect.DesignPatterns.Strategy.After`, etc.).

```
DesiArchitect.DesignPatterns/
├── Program.cs                    # DI setup + interactive CLI menu
├── Shared/Models.cs              # Domain models used across patterns
│
├── 01_Strategy/
│   ├── Before/PaymentService.cs          # if-else jungle
│   └── After/                            # IPaymentStrategy + implementations
│
├── 02_Factory/
│   ├── Before/ScatteredCreation.cs       # "new" scattered everywhere
│   └── After/                            # PaymentStrategyFactory
│
├── 03_Observer/
│   ├── Before/OrderManager.cs            # God method
│   └── After/                            # Publisher + Subscribers
│
├── 04_Decorator/
│   ├── Before/BillCalculator.cs          # boolean-flag hell
│   └── After/                            # wrapping decorators
│
├── 05_Singleton/
│   ├── Before/AppConfigStatic.cs         # static Instance
│   └── After/                            # DI-managed singleton
│
├── 06_Adapter/
│   ├── Before/OrderShipping.cs           # partner-specific branches
│   └── After/                            # one adapter per partner
│
└── 07_Facade/
    ├── Before/BookingController.cs       # 300-line method
    └── After/                            # BookingFacade
```

## The one rule

All 7 patterns follow one principle:

> New feature -> new class. Never touch the old, working, tested file.

- New payment method? New `IPaymentStrategy` implementation.
- New notification channel? New `IOrderStatusSubscriber`.
- New billing charge? New `IBill` decorator.
- New logistics partner? New `ILogisticsPartner` adapter.

The real hero of every pattern is not the class - it is the **interface**, the **contract**, the
**boundary**.

## Confused patterns cheat sheet

| Pair | Pattern A | Pattern B | Key difference |
|------|-----------|-----------|----------------|
| 1 | Strategy | Factory | Strategy *executes* behavior. Factory *creates* the object. |
| 2 | Adapter | Facade | Adapter *translates* an interface. Facade *simplifies* complexity. |
| 3 | Decorator | Adapter | Decorator keeps the *same* interface and adds behavior. Adapter *changes* the interface. |

## License

MIT.
