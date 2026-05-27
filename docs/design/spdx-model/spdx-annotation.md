## SpdxAnnotation

### Purpose

`SpdxAnnotation` represents an SPDX annotation — a comment or review note attached to any SPDX
element by a person, organization, or tool. Annotations support compliance workflows where
reviewers document findings about software components.

### Data Model

**Annotator**: `string` — Person, organization, or tool that made the annotation, in the format
`Person: name`, `Organization: name`, or `Tool: name`.

**Date**: `string` — ISO 8601 UTC timestamp of the annotation (e.g., `2023-01-01T00:00:00Z`).

**Type**: `SpdxAnnotationType` — Enumerated annotation type; either `Review` or `Other`.

**Comment**: `string` — Free-text annotation content describing the finding or note.

### Key Methods

**DeepCopy**: Returns a new `SpdxAnnotation` with all fields copied.

- *Parameters*: none.
- *Returns*: `SpdxAnnotation` — independent copy of this instance.
- *Preconditions*: none.
- *Postconditions*: The returned instance has the same field values and shares no mutable
  references with the original.

**Enhance**: Fills in missing fields from another instance.

- *Parameters*: `SpdxAnnotation other` — source of additional field values.
- *Returns*: `void`
- *Preconditions*: none.
- *Postconditions*: Any empty or default-valued fields in this instance are replaced with the
  corresponding non-empty values from `other`.

**Enhance (static array overload)**: Merges two annotation arrays, returning an updated array.

- *Parameters*: `SpdxAnnotation[] array` — base array to merge into; `SpdxAnnotation[] others` —
  additional annotations to incorporate.
- *Returns*: `SpdxAnnotation[]` — updated array containing all annotations from both inputs.
- *Algorithm*: Iterates `others`; for each item, searches `array` using `Same`. If a match is
  found, the existing item is enhanced with the other's field values. If no match is found, a
  deep copy of the item is appended.
- *Preconditions*: none.
- *Postconditions*: The returned array contains at least all elements of `array` and at least
  one representative of each element in `others`.

**Validate**: Appends validation issues to the supplied list.

- *Parameters*: `string parent` — identifier of the parent element (e.g. package or file SPDX-ID)
  used as a prefix in issue messages; `List<string> issues` — list to append issues to.
- *Returns*: `void`
- *Preconditions*: none.
- *Postconditions*: Any missing required fields are recorded as strings in `issues`.

**Same**: `static IEqualityComparer<SpdxAnnotation>` — compares annotations by annotator, date,
type, and comment. Used for deduplication when merging annotation arrays.

### Error Handling

Validation errors (missing required fields) are collected into a `List<string>` passed to
`Validate` rather than thrown as exceptions. Callers decide whether to surface or suppress the
issues. No exceptions are thrown by `DeepCopy` or `Enhance`.

### Dependencies

- **SpdxElement** — base class providing the `Id` property.
- **SpdxAnnotationType** — enumeration for annotation type values.
- **SpdxHelpers** — `IsValidSpdxDateTime` used in `Validate`; `EnhanceString` used in `Enhance`.

### SpdxAnnotationType

`SpdxAnnotationType` is an enumeration of the SPDX annotation type tokens, with round-trip text
conversion provided by `SpdxAnnotationTypeExtensions`.

#### Purpose

Enumerate the valid SPDX annotation type strings and provide lossless conversion between the enum
representation used in the in-memory model and the text representation used in SPDX JSON documents.

#### Data Model

| Enum Value | Integer | SPDX Text Form |
|------------|---------|----------------|
| `Missing`  | -1      | `""` (sentinel; indicates no type has been set) |
| `Review`   | 0       | `REVIEW` |
| `Other`    | 1       | `OTHER` |

#### Key Methods

**FromText**: Converts an SPDX annotation type text string to its enum value.

- *Signature*: `static SpdxAnnotationType FromText(string annotationType)`
- *Parameters*: `string annotationType` — the raw text from the SPDX document (case-insensitive).
- *Returns*: `SpdxAnnotationType.Missing` when `annotationType` is an empty string; otherwise the
  matching enum value.
- *Exceptions*: `InvalidOperationException` — thrown when `annotationType` is a non-empty string
  that does not match any known annotation type name.

**ToText**: Converts an enum value to its SPDX text form.

- *Signature*: `static string ToText(this SpdxAnnotationType annotationType)`
- *Parameters*: `SpdxAnnotationType annotationType` — the enum value to serialize.
- *Returns*: The canonical SPDX text (e.g., `"REVIEW"`, `"OTHER"`).
- *Exceptions*: `InvalidOperationException` — thrown when the value is `Missing` or is a numeric
  value that does not correspond to any named enum member.

#### Error Handling

- **`FromText`**: throws `InvalidOperationException` with a message identifying the unsupported
  value when given a non-empty string that is not a recognized annotation type token.
- **`ToText`**: throws `InvalidOperationException` when the enum value is `Missing` or does not
  correspond to a named enum member.

#### Dependencies / Callers

- **Spdx2JsonDeserializer** — calls `FromText` when deserializing annotation type fields from JSON.
- **Spdx2JsonSerializer** — calls `ToText` when serializing annotation type fields to JSON.

### Callers

- **SpdxDocument** — holds the document-level `Annotations` array.
- **SpdxLicenseElement** — holds element-level `Annotations` arrays.
- **Spdx2JsonDeserializer** — constructs `SpdxAnnotation` instances during deserialization.
- **Spdx2JsonSerializer** — serializes `SpdxAnnotation` instances to JSON.
