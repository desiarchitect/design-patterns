# 7 Design Patterns Every Senior Developer Uses

Companion code for [The Desi Architect](https://www.youtube.com/@TheDesiArchitect) YouTube video.

> **New feature = a new class. Never touch the old, working, tested file.**

The exact **Before** (anti-pattern) and **After** (clean version) code shown in the video, runnable on your machine. The same patterns are implemented in two languages so you can read them in whichever one you think in. The pattern is the lesson; the language is interchangeable.

---

## Patterns covered

This repo is the home for the full series. Part 1 ships the first three. Part 2 adds the rest.

| # | Pattern | Family | Status |
|---|---------|--------|--------|
| 1 | Strategy | Behavioral | ✅ Available |
| 2 | Factory | Creational | ✅ Available |
| 3 | Observer | Behavioral | ✅ Available |
| 4 | Decorator | Structural | ✅ Available |
| 5 | Adapter | Structural | ✅ Available |
| 6 | Facade | Structural | ✅ Available |
| 7 | Singleton | Creational | ✅ Available |

---

## Pick your stack

| Stack | Folder | Run |
|-------|--------|-----|
| **C# / .NET 10** | `csharp/` | `cd csharp/DesiArchitect.DesignPatterns && dotnet run` |
| **TypeScript / Node** | `typescript/` | `cd typescript && npm install && npm run demo` |

Both open the same interactive menu and print the same Before-vs-After story. Each folder has its own README with stack-specific setup and notes.

---

## Repo structure

```
.
├── csharp/
│   └── DesiArchitect.DesignPatterns/
│       ├── 01_Strategy/        Before/  After/
│       ├── 02_Factory/         Before/  After/
│       ├── 03_Observer/        Before/  After/
│       └── README.md
├── typescript/
│   ├── 01_strategy/            before/  after/
│   ├── 02_factory/             before/  after/
│   ├── 03_observer/            before/  after/
│   └── README.md
└── README.md
```

Every pattern has a `Before/` folder (the anti-pattern that breaks in production) and an `After/` folder (the clean fix). Run both and compare the console output.

---

## A note on the blast-radius demo

One demo, the Strategy cold-open "blast radius" crash, fails through a different runtime mechanism in each language: a C# static constructor poisoning the type, versus a Node module-init throw. Different mechanism, identical architectural lesson. Each language README calls this out.

---

## How to use this repo

Do not just read the code. Open the terminal, run the `Before/` folder, watch it break. Then run the `After/` folder and watch the same scenario survive. Change something, break it yourself, fix it. Architecture is learned by breaking and building systems, not by watching.

---

## The one rule

All patterns follow a single principle:

```
new feature -> new class, never touch the old, working, tested file
```

The real hero of every pattern is not the class. It is the interface, the contract, the boundary.

---

## Links

- ▶️ **Watch the video:** [Part 1 on YouTube](https://youtu.be/3AzZmLUK1n8)
- 🚀 **Developer to Architect track:** [desiarchitect.com](https://desiarchitect.com)
- 💬 **1:1 guidance and career roadmap:** [Topmate](https://topmate.io/deepak_mishra36/)

If this repo helped you, leave a ⭐. It helps more developers find it.

---

*Maintained by Deepak Mishra, The Desi Architect.*
*Jo textbook mein nahi hai, wo yahan milega.*
