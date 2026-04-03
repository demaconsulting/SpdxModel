# SpdxHelpers Unit Design

## Purpose

`SpdxHelpers` is an internal static utility class providing shared helper methods used across
the data model. It centralizes common operations such as string enhancement (selecting the
best available value by fitness ranking) and SPDX date-time validation.

## Design

`SpdxHelpers` is a `partial` internal static class. Date-time validation uses
`[GeneratedRegex]` on .NET 7 and later (source-generated, AOT-safe), with a cached `Regex`
instance as a fallback for earlier targets such as `netstandard2.0`.

Key methods:

| Method | Description |
| ------ | ----------- |
| `IsValidSpdxDateTime(string?)` | Returns `true` if the value matches ISO 8601 UTC format |
| `EnhanceString(params string?[])` | Returns the highest-fitness value: concrete > `NOASSERTION` > empty > `null` |

Key design decisions:

- `internal` visibility — not part of the public API; only used within the assembly.
- `partial` class enables the `[GeneratedRegex]` attribute on .NET 7+; pre-.NET 7 targets use
  a cached `Regex` instance instead.
- `EnhanceString` uses a fitness ranking so that a meaningful value is always preferred over
  `NOASSERTION` or absent values, regardless of argument order.

## Dependencies

- `System.Text.RegularExpressions` — date-time validation regex
