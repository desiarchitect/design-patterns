# 7 Design Patterns Every Senior Developer Uses

> Companion code for [The Desi Architect](https://youtube.com/@desiarchitect) YouTube video.

**"New feature = a new class. Never touch the old, working, tested file."**

A runnable **.NET 10** console app that teaches 7 essential design patterns through
**Before** (the anti-pattern) and **After** (the clean version) code - the exact code shown
in the video, runnable on your machine.

## Quick start

```bash
git clone https://github.com/desiarchitect/design-patterns.git
cd design-patterns/csharp/DesiArchitect.DesignPatterns
dotnet run
```

Pick a pattern (1-7) from the menu to see its Before vs After run live.
Requires the [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0).

## Part 1 video (menu options 1-3)

The first YouTube episode walks **Strategy**, **Factory**, and **Observer** only.
After `dotnet run`, choose:

| Option | Pattern | What you will see |
|--------|---------|-----------------|
| `1` | Strategy | Before: if-else payment jungle + shared key load. After: inject `IPaymentStrategy` per provider. |
| `2` | Factory | Before: `new` scattered across call sites. After: `PaymentStrategyFactory` resolves the strategy. |
| `3` | Observer | Before: god method blocks ~5s on slow SMS. After: publisher returns; subscribers run in background. |

### Replay the blast-radius incident (Strategy, option 1)

One missing vault key fails the shared static initializer and poisons `PaymentService`
for the rest of the process - every payment method goes down, not just PhonePe.

**Windows (PowerShell):**

```powershell
cd csharp\DesiArchitect.DesignPatterns
$env:SIMULATE_BLAST_RADIUS = "1"
dotnet run
# choose 1, then clear the env var when done:
Remove-Item Env:SIMULATE_BLAST_RADIUS -ErrorAction SilentlyContinue
```

**macOS / Linux (bash):**

```bash
cd csharp/DesiArchitect.DesignPatterns
SIMULATE_BLAST_RADIUS=1 dotnet run   # choose 1
```

Without the env var, option 1 still runs the normal Before/After comparison with
distinct per-method logs (UPI, Card, Wallet, NetBanking).

## Run with Docker (no .NET install needed)

If you do not have the .NET SDK installed, you only need Docker:

```bash
docker build -t desiarchitect-patterns .
docker run -it --rm desiarchitect-patterns
```

Use `-it` so the interactive menu can read your input. The image is multi-stage,
so the final container ships only the .NET runtime, not the full SDK.

## The 7 patterns

| # | Pattern | Category | Real-world example |
|---|---------|----------|--------------------|
| 1 | Strategy  | Behavioral | PhonePe checkout - swap the payment engine |
| 2 | Factory   | Creational | One place decides which strategy to create |
| 3 | Observer  | Behavioral | Zomato live tracking - broadcast to subscribers |
| 4 | Decorator | Structural | Swiggy bill - layers of charges |
| 5 | Singleton | Creational | Shared config - DI vs static Instance |
| 6 | Adapter   | Structural | Bluedart / Delhivery - translate third-party APIs |
| 7 | Facade    | Structural | IRCTC booking - one button, many subsystems |

## The one rule

All 7 patterns follow a single principle: **new feature -> new class, never touch the old,
working, tested file.** The real hero of every pattern is not the class - it is the
**interface**, the **contract**, the **boundary**.

Full pattern-by-pattern notes live in
[`DesiArchitect.DesignPatterns/README.md`](DesiArchitect.DesignPatterns/README.md).

## License

MIT.
