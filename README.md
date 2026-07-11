# exercism-csharp

Solutions to C# exercises from [Exercism](https://exercism.org/tracks/csharp) — a free platform for practicing programming languages through small, focused exercises with mentor feedback.

## About

This repo tracks my progress through the Exercism C# track, sort of. Each exercise lives in its own project under `src/`, with a matching test project under `test/`, following the typical `src`/`test` structure.

I'm not working through the track in order — I pick exercises based on what looks interesting rather than following the suggested sequence.
Later on, the repo also became a sandbox for Claude Code experiments, testing how different models behave under different circumstances against small, self-contained, well-tested problems.

## Project structure

```
exercism-csharp/
├── src/
│   └── <exercise-name>/
├── test/
│   └── <exercise-name>.UnitTests/
├── Exercism.slnx
└── LICENSE
```

## Getting started

**Prerequisites:** [.NET SDK](https://dotnet.microsoft.com/download)

Clone the repo and restore/build the solution:

```bash
git clone https://github.com/lgamorim/exercism-csharp.git
cd exercism-csharp
dotnet build
```

Run all tests:

```bash
dotnet test
```

Run tests for a single exercise:

```bash
dotnet test test/<exercise-name>
```

## Exercises

| Exercise | Concept(s) practiced |
|---|---|
| [Forth](https://exercism.org/tracks/csharp/exercises/forth) | Stack-based interpreter, token parsing, custom word definitions |
| [Matrix](https://exercism.org/tracks/csharp/exercises/matrix) | String parsing into 2D structures, row/column extraction |
| [Minesweeper](https://exercism.org/tracks/csharp/exercises/minesweeper) | 2D grid traversal, neighbor counting, board annotation |
| [React](https://exercism.org/tracks/csharp/exercises/react) | Observer/reactive pattern, computed cells, dependency propagation |
| [Tournament](https://exercism.org/tracks/csharp/exercises/tournament) | String parsing, data aggregation, sorting and formatted output |
| [Zipper](https://exercism.org/tracks/csharp/exercises/zipper) | Immutable binary tree navigation, functional-style data structures |

## License

MIT — see [LICENSE](./LICENSE).
