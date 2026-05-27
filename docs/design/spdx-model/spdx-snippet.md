## SpdxSnippet

### Purpose

`SpdxSnippet` represents a portion of a file in an SPDX document. Snippets are used when a
specific byte or line range within a file has different licensing or provenance from the rest of
the file, enabling granular compliance tracking for reused code segments.

### Data Model

**SnippetFromFile**: `string` — SPDX ID of the `SpdxFile` containing this snippet.

**SnippetByteStart**: `int` — Inclusive start byte offset of the snippet within the file.

**SnippetByteEnd**: `int` — Inclusive end byte offset of the snippet within the file.

**SnippetLineStart**: `int` — Optional start line number; `0` if unspecified.

**SnippetLineEnd**: `int` — Optional end line number; `0` if unspecified.

**LicenseInfoInSnippet**: `string[]` — License expressions found in this snippet.

**Comment**: `string?` — Optional free-text comment.

**Name**: `string?` — Optional human-readable snippet name.

*Inherited from `SpdxLicenseElement`*: `Id`, `ConcludedLicense`, `LicenseComments`,
`CopyrightText`, `AttributionText`, `Annotations`.

### Key Methods

**DeepCopy**: Returns a fully deep-copied instance.

- *Parameters*: none.
- *Returns*: `SpdxSnippet` — independent copy.
- *Preconditions*: none.
- *Postconditions*: The returned instance shares no mutable references with the original.

**Enhance**: Fills in missing fields from another instance.

- *Parameters*: `SpdxSnippet other` — source of additional field values.
- *Returns*: `void`
- *Preconditions*: none.
- *Postconditions*: Empty or null string fields are populated from `other`; integer fields with value
  `0` are replaced by the corresponding non-zero value from `other`; `LicenseInfoInSnippet` is merged
  by deduplication from both instances.

**Enhance (static array merge)**: Merges two snippet arrays by matching on file SPDX ID and byte
range.

- *Parameters*: `SpdxSnippet[] array`, `SpdxSnippet[] others`.
- *Returns*: `SpdxSnippet[]` — merged array.
- *Preconditions*: none.
- *Postconditions*: Matching entries are enhanced; new entries are appended.

**Validate**: Appends validation issues to the supplied list.

- *Parameters*: `List<string> issues` — list to append issues to.
- *Returns*: `void`
- *Preconditions*: none.
- *Postconditions*: Missing required fields, invalid byte ranges, malformed IDs, and invalid
  annotations are recorded in `issues`.

**Same**: `static IEqualityComparer<SpdxSnippet>` — compares by `SnippetFromFile`, byte start,
and byte end.

### Error Handling

Validation errors are collected into the `List<string>` passed to `Validate`. No exceptions are
thrown by `DeepCopy`, `Enhance`, or the static merge method.

### Dependencies

- **SpdxLicenseElement** — abstract base class.

### Callers

- **SpdxDocument** — holds the `Snippets` array.
- **Spdx2JsonDeserializer** — constructs `SpdxSnippet` instances during deserialization.
- **Spdx2JsonSerializer** — serializes `SpdxSnippet` instances to JSON.
