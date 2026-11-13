# FatCat.Projections Coding Standards

This file defines the coding standards for the FatCat.Projections codebase.
All code you generate — in any context — must follow the rules for the relevant language below.
The goal is that AI-generated code is indistinguishable from code written by a senior member of this team.

## ⚠️ READ THIS FIRST — Before Any Work

**Before doing anything else in this repository — reading, planning, or writing code — read [`README.md`](README.md) in full.**
It is the single source of truth for what this product is and how it is built. Do not skip it, even for a "small" change.

The README tells you the things you must know before you touch a line of code:
- **Product:** FatCat.Projections — a small NuGet library (`FatCat.Projections`) that projects (copies) property values from an object of one type onto an object of a different type by matching property names, recursing into sub-objects, lists, and byte arrays. Entry points are the static `Projection.ProjectTo<TDestination>(source)` / `ProjectTo(ref destination, source)`, the injectable `IProjector` / `Projector` wrapper, per-call fluent overrides via `FluentProjection<TDestination, TSource>.ForProperty(...)`, and custom projectors (`IDoProjection<T>`) registered through `ProjectionConfiguration.UseCustomProjection`. `ProjectionSettings` flags control behaviour such as skipping custom projectors or not projecting nulls. The README is only a two-line placeholder — the code and the tests in `Projections.Tests` are the real specification.
- **Tech stack:** **.NET 11 / C# 15** (`net11.0`, `LangVersion` default) class library that depends on **FatCat.Toolkit** (extensions; Fasterflect for reflection) and is otherwise reflection-based. Tests use **xUnit**, **FatCat.Testing** assertions, and **FatCat.Fakes** for test data. There is no UI, web server, database, or DI container in this repo.
- **Architecture:** `Projections` is the shipped library; `Projections.Tests` holds its tests; `TestingConsole` is a scratch console app for trying projections by hand and is not shipped.
- **Standing rules:** this is a published package — changes to public types (`Projection`, `IProjector`, `IFluentProjection`, `IDoProjection`, `ProjectionConfiguration`, `ProjectionSettings`) are breaking changes for consumers, and the version is stepped in `VersionPrefix` in `Projections.csproj` as a deliberate release step.

If anything below in the coding standards appears to conflict with the README, the README describes *what* the system is; these rules describe *how* to write code for it — follow both. Where the README and the actual code disagree, **the code is authoritative** — trust the code and flag the stale README.

## Repository Layout

| Path | Project | Purpose |
|---|---|---|
| `src/Projections` | `FatCat.Projections` | The NuGet library: `Projection`, `Projector`, `FluentProjection`, `ProjectionConfiguration`, `ProjectionProcessor`, `ListCopy`, extensions |
| `src/Projections.Tests` | `FatCat.Projections.Tests` | xUnit tests for `FatCat.Projections` (`BasicProjection`, `FluentProjections`, `ProjectUsing`, `Extensions`) |
| `src/TestingConsole` | `FatCat.Projections.TestingConsole` | Console app for manually exercising projections; not shipped |

All production namespaces start with `FatCat.Projections.*`; test namespaces currently start with `FatCat.Projections.Tests.*` (the rules files describe the `Tests.FatCat.Projections.*` form; follow the existing code in this project).

Everything must build and every test must pass before work is considered done:
```bash
dotnet build src/FatCat.Projections.slnx
dotnet test src/FatCat.Projections.slnx
```

---

## C# Rules

Apply these rules to all C# code. Do not apply them to TypeScript, PowerShell, or any other language.

@.claude/rules/csharp/naming-and-structure.md
@.claude/rules/csharp/types-and-di.md
@.claude/rules/csharp/toolchain.md
@.claude/rules/csharp/async.md
@.claude/rules/csharp/errors-and-logging.md
@.claude/rules/csharp/testing.md
@.claude/rules/csharp/not-allowed.md

## PowerShell Rules

Apply these rules to all PowerShell scripts. Do not apply them to C# or any other language.

@.claude/rules/powershell/powershell.md
