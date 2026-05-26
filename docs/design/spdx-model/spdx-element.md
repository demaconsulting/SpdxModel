## SpdxElement

### Purpose

`SpdxElement` is the abstract base class for all identifiable SPDX elements. It defines the
common `Id` property (`SPDXRef-…`) and the shared `EnhanceElement` helper, ensuring consistent
identity handling across all element types.

### Data Model

**Id**: `string` — SPDX element identifier in `SPDXRef-<name>` format. Must be unique within
a document.

**NoAssertion**: `const string` — The sentinel value `"NOASSERTION"` used by optional fields to
indicate that the value was intentionally omitted or is not known.

**SpdxRefRegex**: `protected static Regex` — Pre-compiled regular expression that validates
the `SPDXRef-…` format; used by subclass `Validate` methods. Matches the full pattern
`^SPDXRef-[a-zA-Z0-9.-]+$`. The 100 ms timeout is a ReDoS protection measure against
pathological input strings from untrusted SPDX sources.

### Key Methods

**EnhanceElement**: Protected helper that populates `Id` from another element if currently empty.

- *Parameters*: `SpdxElement other` — source element.
- *Returns*: `void`
- *Preconditions*: none.
- *Postconditions*: If `Id` is empty or null, it is set to `other.Id` via `SpdxHelpers.EnhanceString`.

### Error Handling

N/A - `SpdxElement` is abstract and contains no validation logic of its own. Subclasses
implement `Validate` and append issues to a `List<string>`.

### Dependencies

- **System.Text.RegularExpressions** — `SpdxRefRegex` for ID format validation.
- **SpdxHelpers** — `EnhanceString` utility used in `EnhanceElement`.

### Callers

- **SpdxDocument** — extends `SpdxElement`.
- **SpdxRelationship** — extends `SpdxElement`.
- **SpdxAnnotation** — extends `SpdxElement`.
- **SpdxLicenseElement** — extends `SpdxElement` (abstract intermediate class).
