## SpdxCreationInformation

### Purpose

`SpdxCreationInformation` captures the metadata about who created an SPDX document and when.
One instance is required per `SpdxDocument`. It enables provenance tracing and forward/backward
compatibility for processing tools.

### Data Model

**Creators**: `string[]` — Identifies the persons, organizations, or tools that created the
document, each entry in the format `Person: name`, `Organization: name`, or `Tool: name`.

**Created**: `string` — ISO 8601 UTC timestamp of document creation; may be empty for
partially-constructed documents (empty is permitted and skips format validation).

**Comment**: `string?` — Optional free-text comment from the creators.

**LicenseListVersion**: `string?` — Optional version string of the SPDX license list used
when constructing the document.

### Key Methods

**DeepCopy**: Returns a new `SpdxCreationInformation` with all fields copied.

- *Parameters*: none.
- *Returns*: `SpdxCreationInformation` — independent copy of this instance.
- *Preconditions*: none.
- *Postconditions*: The returned instance has equal field values and shares no mutable references
  with the original.

**Enhance**: Fills in missing fields from another instance.

- *Parameters*: `SpdxCreationInformation other` — source of additional field values.
- *Returns*: `void`
- *Preconditions*: none.
- *Postconditions*: `Creators` is updated to the union of both arrays, deduplicated (preserving
  order, removing duplicates). Scalar fields (`Created`, `Comment`, `LicenseListVersion`) are
  filled from `other` only when currently empty or null in this instance.

**Validate**: Appends validation issues to the supplied list.

- *Parameters*: `List<string> issues` — list to append issues to.
- *Returns*: `void`
- *Preconditions*: none.
- *Postconditions*: All four validation rules are checked and violations recorded in `issues`:
  (1) `Creators` must be non-empty; (2) each creator entry must start with `Person:`,
  `Organization:`, or `Tool:`; (3) `Created` must be a valid SPDX date-time string when
  non-empty (an empty `Created` is permitted); (4) `LicenseListVersion`, when present, must
  match the pattern `\d+\.\d+`.

### Error Handling

Validation errors are collected into the `List<string>` passed to `Validate`. Four rules are
checked: (1) `Creators` non-empty, (2) each creator prefixed with `Person:`, `Organization:`,
or `Tool:`, (3) `Created` valid SPDX date-time (when non-empty), (4) `LicenseListVersion`
matching `\d+\.\d+` (when present). `LicenseListVersion` format is validated via
`LicenseListVersionRegex`. No exceptions are thrown by `DeepCopy` or `Enhance`.

### Dependencies

- **System.Text.RegularExpressions** — used internally for `LicenseListVersion` format
  validation via `LicenseListVersionRegex` (pattern `^\d+\.\d+$`).
- **SpdxHelpers** — `EnhanceString` used in `Enhance` to fill empty string fields; `IsValidSpdxDateTime`
  used in `Validate` to check the `Created` field format.

### Callers

- **SpdxDocument** — holds exactly one `SpdxCreationInformation` instance as `CreationInformation`.
- **Spdx2JsonDeserializer** — constructs `SpdxCreationInformation` during deserialization.
- **Spdx2JsonSerializer** — serializes `SpdxCreationInformation` to JSON.
