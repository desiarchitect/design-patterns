# 7 Design Patterns Every Senior Developer Uses

> Companion code for [The Desi Architect](https://youtube.com/@desiarchitect) YouTube video.

**"New feature = a new class. Never touch the old, working, tested file."**

The exact Before (anti-pattern) and After (clean version) code shown in the video, runnable on
your machine. The same patterns are implemented in **two languages** so you can read them in
whichever one you think in. The pattern is the lesson; the language is interchangeable.

## Pick your stack

| Stack | Folder | Run |
|-------|--------|-----|
| C# / .NET 10 | [`csharp/`](csharp/) | `cd csharp/DesiArchitect.DesignPatterns && dotnet run` |
| TypeScript / Node | [`typescript/`](typescript/) | `cd typescript && npm install && npm run demo` |

Both open the same interactive menu and print the same Before-vs-After story. Each folder has
its own README with stack-specific setup and notes.

> Note: one demo, the Strategy cold-open "blast radius" crash, fails through a different runtime
> mechanism in each language (a C# `static` constructor poisoning the type vs a Node module-init
> throw). Different mechanism, identical architectural lesson. Each README calls this out.

## The one rule

All patterns follow a single principle: **new feature -> new class, never touch the old,
working, tested file.** The real hero of every pattern is not the class - it is the
**interface**, the **contract**, the **boundary**.

## License

MIT.
