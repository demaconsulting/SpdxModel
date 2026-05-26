## SpdxHelpers

### Purpose

`SpdxHelpers` is an internal static utility class providing shared helper methods used across
the data model. It centralizes common operations such as string enhancement (selecting the best
available value by fitness ranking) and SPDX date-time validation.

### Data Model

N/A - `SpdxHelpers` is a static utility class with no instance state.

### Key Methods

**IsValidSpdxDateTime**: Returns `true` if the supplied value matches the ISO 8601 UTC timestamp
format required by SPDX.

- *Parameters*: `string? value` — the timestamp string to validate.
- *Returns*: Returns `true` if `value` matches the ISO 8601 UTC format, or if `value` is null or
  empty (both treated as not-set and therefore valid); `false` otherwise.
- *Preconditions*: none.
- *Postconditions*: none.

**EnhanceString**: Returns the highest-fitness string from the supplied candidates.

- *Parameters*: `params string?[] candidates` — ordered list of candidate values.
- *Returns*: `string?` — the best candidate: concrete (non-empty, non-NOASSERTION) > `NOASSERTION`
  > empty string > `null`.
- *Preconditions*: none.
- *Postconditions*: The returned value is the most informative of the candidates regardless of
  argument order.

### Error Handling

N/A - both methods are pure functions with no side effects. `IsValidSpdxDateTime` returns `false`
for invalid input rather than throwing.

### Dependencies

- **System.Text.RegularExpressions** — date-time validation regex. On .NET 7 and later, a
  source-generated `[GeneratedRegex]` is used for AOT safety; earlier targets use a cached
  `Regex` instance.

### Callers

- **SpdxElement** — `EnhanceString` used in `EnhanceElement`.
- All data model units that call `EnhanceString` in their `Enhance` methods.
- **SpdxCreationInformation** — `IsValidSpdxDateTime` used in `Validate`.
- **SpdxAnnotation** — `IsValidSpdxDateTime` used in `Validate`.
- **SpdxPackage** — `IsValidSpdxDateTime` used in `Validate`.
