# SpdxHelpers Unit Design

## Purpose

`SpdxHelpers` is an internal static utility class providing shared helper methods used across
the data model. It centralizes common operations such as string enhancement (choosing the first
non-empty value) and SPDX date-time validation.

## Design

`SpdxHelpers` is a `partial` internal static class using a source-generated `Regex` for
date-time validation (C# 7+ `GeneratedRegex` pattern).

Key methods:

| Method | Description |
| ------ | ----------- |
| `IsValidSpdxDateTime(string?)` | Returns `true` if the value matches ISO 8601 UTC format |
| `EnhanceString(params string?[])` | Returns the first non-null, non-empty string from the arguments, or `null` |

Key design decisions:

- `internal` visibility — not part of the public API; only used within the assembly.
- `partial` class enables the `[GeneratedRegex]` attribute on the regex factory method for
  AOT-safe compiled regular expressions.

## Dependencies

- `System.Text.RegularExpressions` — date-time validation regex
