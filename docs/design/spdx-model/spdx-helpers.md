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

- *Parameters*: `params string?[] values` — ordered list of candidate values.
- *Returns*: `string?` — the best candidate: concrete (non-empty, non-NOASSERTION) > `NOASSERTION`
  > empty string > `null`.
- *Preconditions*: none.
- *Postconditions*: The returned value is the most informative of the candidates regardless of
  argument order.

### Error Handling

N/A - both methods are pure functions with no side effects. `IsValidSpdxDateTime` returns `false`
for invalid input rather than throwing.

### Dependencies

- **SpdxElement** — `SpdxElement.NoAssertion` constant used in `EnhanceString` fitness ranking to
  identify `NOASSERTION` values.
- **System.Text.RegularExpressions** — date-time validation regex. On .NET 7 and later, a
  source-generated `[GeneratedRegex]` is used for AOT safety; earlier targets use a cached
  `Regex` instance.

### Callers

**EnhanceString callers** (all data model `Enhance` methods):

- **SpdxElement** — `EnhanceString` used in `EnhanceElement`.
- **SpdxAnnotation** — `EnhanceString` used in `Enhance`.
- **SpdxChecksum** — `EnhanceString` used in `Enhance`.
- **SpdxCreationInformation** — `EnhanceString` used in `Enhance`.
- **SpdxExternalDocumentReference** — `EnhanceString` used in `Enhance`.
- **SpdxExternalReference** — `EnhanceString` used in `Enhance`.
- **SpdxExtractedLicensingInfo** — `EnhanceString` used in `Enhance`.
- **SpdxFile** — `EnhanceString` used in `Enhance`.
- **SpdxLicenseElement** — `EnhanceString` used in `Enhance`.
- **SpdxPackage** — `EnhanceString` used in `Enhance`.
- **SpdxPackageVerificationCode** — `EnhanceString` used in `Enhance`.
- **SpdxRelationship** — `EnhanceString` used in `Enhance`.
- **SpdxSnippet** — `EnhanceString` used in `Enhance`.

**IsValidSpdxDateTime callers**:

- **SpdxCreationInformation** — `IsValidSpdxDateTime` used in `Validate`.
- **SpdxAnnotation** — `IsValidSpdxDateTime` used in `Validate`.
- **SpdxPackage** — `IsValidSpdxDateTime` used in `Validate`.
