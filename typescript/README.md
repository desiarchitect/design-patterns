# Design Patterns - TypeScript companion

> Companion code for [The Desi Architect](https://youtube.com/@desiarchitect) YouTube video.

The same three patterns shown in the video - **Strategy, Factory, Observer** - implemented in
**TypeScript / Node**, with the same **Before** (anti-pattern) and **After** (clean) split.
The C# version lives in [`../csharp/`](../csharp/). The pattern is the lesson; the language is
interchangeable.

## Quick start

```bash
cd typescript
npm install
npm run demo
```

Pick a pattern (1-3) from the menu to see its Before vs After run live.
Requires Node 18+ (uses ESM + `Atomics.wait`). `npm run typecheck` runs `tsc --noEmit`.

## What each pattern shows

| # | Pattern | Before (the pain) | After (the fix) |
|---|---------|-------------------|-----------------|
| 1 | Strategy | `payment-service.ts` - one if-else file, shared key load | a class per provider behind `IPaymentStrategy` |
| 2 | Factory  | `new` scattered across files | `PaymentStrategyFactory` centralizes creation |
| 3 | Observer | god method blocks on a slow SMS call | publisher broadcasts; subscribers run in the background |

## Blast radius across runtimes (important)

The Strategy cold-open demonstrates how **one missing key takes the whole payment surface down**.
The *mechanism* differs by runtime, but the architectural lesson is identical:

- **C#:** keys load in a `static` constructor. A missing key throws `TypeInitializationException`
  and the type is **poisoned for the life of the process** - every later access throws too.
- **TypeScript / Node:** keys load at **module-evaluation time**. A missing key throws while the
  module is being imported, so the `import` fails and **every consumer of that module is dead**.
  Under ESM the failed module is cached as errored, so re-importing it fails the same way.

Different mechanism, same blast radius: nothing was isolated, so one failure took everything.
Run it:

```bash
SIMULATE_BLAST_RADIUS=1 npm run demo   # then choose 1
# Windows PowerShell:  $env:SIMULATE_BLAST_RADIUS=1; npm run demo
```

## A note on the Observer demo

The `After` publisher uses **fire-and-forget** (`void subscriber.onStatusChanged(...)`, no
`await`) only so the non-blocking behaviour is visible on screen. Do **not** ship this as-is:
unawaited promise rejections are swallowed and nothing applies backpressure. In production you
put events on a real queue (BullMQ / SQS / Kafka) with proper error handling. The core idea -
publisher leaves the event, subscribers consume independently - stays the same; only the
infrastructure scales up. (The C# version makes the same point with `Task.Run`.)
