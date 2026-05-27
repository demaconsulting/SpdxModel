## SpdxExtractedLicensingInfo

### Purpose

`SpdxExtractedLicensingInfo` records the full text and metadata of a non-standard license found
within a software package. It is used when the license does not appear on the SPDX License List
and must be captured verbatim for compliance purposes.

### Data Model

**LicenseId**: `string` — Local identifier in `LicenseRef-…` format, unique within the document.

**ExtractedText**: `string` — Full verbatim text of the license as found in the software.

**Name**: `string?` — Optional human-readable license name.

**CrossReferences**: `string[]` — Optional URIs pointing to the license text at canonical
external locations.

**Comment**: `string?` — Optional explanatory comment.

### Key Methods

**DeepCopy**: Returns a new instance with all fields deep-copied.

- *Parameters*: none.
- *Returns*: `SpdxExtractedLicensingInfo` — independent copy.
- *Preconditions*: none.
- *Postconditions*: The returned instance shares no mutable references with the original.

**Enhance**: Fills in missing fields from another instance.

- *Parameters*: `SpdxExtractedLicensingInfo other` — source of additional field values.
- *Returns*: `void`
- *Preconditions*: none.
- *Postconditions*: Empty or null fields are populated from `other`.

**Enhance (static array merge)**: Merges two extracted licensing info arrays by matching on
`ExtractedText`.

- *Parameters*: `SpdxExtractedLicensingInfo[] base`, `SpdxExtractedLicensingInfo[] additions`.
- *Returns*: `SpdxExtractedLicensingInfo[]` — merged array.
- *Preconditions*: none.
- *Postconditions*: Matching entries are enhanced; new entries are appended.

**Validate**: Appends validation issues to the supplied list.

- *Parameters*: `List<string> issues` — list to append issues to.
- *Returns*: `void`
- *Preconditions*: none.
- *Postconditions*: Missing required fields are recorded in `issues`.

**Same**: `static IEqualityComparer<SpdxExtractedLicensingInfo>` — compares by `ExtractedText`.

### Error Handling

Validation errors are collected into the `List<string>` passed to `Validate`. No exceptions are
thrown by `DeepCopy`, `Enhance`, or the static merge method.

### Dependencies

- **SpdxHelpers** — `EnhanceString` used in `Enhance`.

### Callers

- **SpdxDocument** — holds the `ExtractedLicensingInfo` array.
- **Spdx2JsonDeserializer** — constructs `SpdxExtractedLicensingInfo` instances during deserialization.
- **Spdx2JsonSerializer** — serializes `SpdxExtractedLicensingInfo` instances to JSON.
