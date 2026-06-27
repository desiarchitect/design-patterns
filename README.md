# 7 Design Patterns Every Senior Developer Uses

Companion code for the complete 7-pattern series on [The Desi Architect](https://www.youtube.com/@TheDesiArchitect) YouTube channel (Part 1 + Part 2).

> **New feature = a new class. Never touch the old, working, tested file.**

The exact **Before** (anti-pattern) and **After** (clean version) code shown in the videos, runnable on your machine. The same patterns are implemented in two languages so you can read them in whichever one you think in. The pattern is the lesson; the language is interchangeable.

---

## Patterns covered

This repo contains the runnable companion code for the complete **7-pattern series** (Part 1 + Part 2).

| # | Pattern | Family | Real-world example |
|---|---------|--------|--------------------|
| 1 | Strategy  | Behavioral | PhonePe checkout - swap the payment engine |
| 2 | Factory   | Creational | One place decides which strategy to create |
| 3 | Observer  | Behavioral | Zomato live tracking - broadcast to subscribers |
| 4 | Decorator | Structural | Swiggy bill - layers of charges |
| 5 | Singleton | Creational | Shared config - DI vs static Instance |
| 6 | Adapter   | Structural | Bluedart / Delhivery - translate third-party APIs |
| 7 | Facade    | Structural | IRCTC booking - one button, many subsystems |

The three categories are the standard Gang-of-Four buckets: **Creational** (how objects are made), **Structural** (how objects are composed), **Behavioral** (how objects act and react).

---

## Pick your stack

| Stack | Folder | Run |
|-------|--------|-----|
| **C# / .NET 10** | `csharp/` | `cd csharp/DesiArchitect.DesignPatterns && dotnet run` |
| **TypeScript / Node** | `typescript/` | `cd typescript && npm install && npm run demo` |

Both open the same interactive menu and print the same Before-vs-After story for all 7 patterns.

- See `csharp/README.md` for Docker instructions and the `SIMULATE_BLAST_RADIUS` trick.
- See `typescript/README.md` for Node-specific notes and TypeScript commands.

Full pattern-by-pattern notes (including key interfaces) are in `csharp/DesiArchitect.DesignPatterns/README.md`.

---

## Repo structure

```
.
├── csharp/
│   ├── DesiArchitect.DesignPatterns/
│   │   ├── 01_Strategy/     Before/  After/
│   │   ├── 02_Factory/      Before/  After/
│   │   ├── 03_Observer/     Before/  After/
│   │   ├── 04_Decorator/    Before/  After/
│   │   ├── 05_Singleton/    Before/  After/
│   │   ├── 06_Adapter/      Before/  After/
│   │   ├── 07_Facade/       Before/  After/
│   │   ├── Shared/
│   │   ├── Program.cs
│   │   └── README.md
│   ├── Dockerfile
│   └── README.md
│
├── typescript/
│   ├── src/
│   │   ├── 01-strategy/   before/  after/
│   │   ├── 02-factory/    before/  after/
│   │   ├── 03-observer/   before/  after/
│   │   ├── 04-decorator/  before/  after/
│   │   ├── 05-singleton/  before/  after/
│   │   ├── 06-adapter/    before/  after/
│   │   ├── 07-facade/     before/  after/
│   │   ├── shared/
│   │   └── index.ts
│   ├── package.json
│   └── README.md
│
├── LICENSE
└── README.md
```

Every pattern has a `Before/` folder (the anti-pattern that breaks in production) and an `After/` folder (the clean fix). Run both and compare the console output.

Note: C# folders use `NN_PascalCase` (with underscores). TypeScript folders use `NN-kebab-case`. The pattern names and lesson are identical across languages.

---

## A note on the blast-radius demo

One demo, the Strategy cold-open "blast radius" crash, fails through a different runtime mechanism in each language:

- **C#**: keys load in a static constructor. A missing key throws `TypeInitializationException` and the type is **poisoned for the life of the process**.
- **TypeScript / Node**: keys load at module-evaluation time. A missing key throws while the module is being imported, so the `import` fails and every consumer of that module is dead. Under ESM the failed module is cached as errored.

Different mechanism, same blast radius: nothing was isolated, so one failure took everything. Each language README has the exact commands.

---

## How to use this repo

Do not just read the code. Open the terminal, run the `Before/` version, watch it break. Then run the `After/` version and watch the same scenario survive. Change something, break it yourself, fix it. Architecture is learned by breaking and building systems, not by watching.

---

## The one rule

All patterns follow a single principle:

```
new feature -> new class, never touch the old, working, tested file
```

The real hero of every pattern is not the class. It is the interface, the contract, the boundary.

---

## Confused patterns cheat sheet

| Pair | Pattern A | Pattern B | Key difference |
|------|-----------|-----------|----------------|
| 1 | Strategy | Factory | Strategy *executes* behavior. Factory *creates* the object. |
| 2 | Adapter | Facade | Adapter *translates* an interface. Facade *simplifies* complexity. |
| 3 | Decorator | Adapter | Decorator keeps the *same* interface and adds behavior. Adapter *changes* the interface. |

---

## Links

- ▶️ **Watch Part 1:** [Strategy, Factory & Observer on YouTube](https://youtu.be/3AzZmLUK1n8)
- 📺 **Full series:** Search "Design Patterns" on [The Desi Architect YouTube channel](https://www.youtube.com/@TheDesiArchitect)
- 🚀 **Developer to Architect track:** [desiarchitect.com](https://desiarchitect.com)
- 💬 **1:1 guidance and career roadmap:** [Topmate](https://topmate.io/deepak_mishra36/)

If this repo helped you, leave a ⭐. It helps more developers find it.

---

## License

MIT.

---

*Maintained by Deepak Mishra, The Desi Architect.*
