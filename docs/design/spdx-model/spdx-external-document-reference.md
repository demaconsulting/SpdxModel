## SpdxExternalDocumentReference

### Purpose

`SpdxExternalDocumentReference` represents a reference from one SPDX document to another,
enabling modular SBOM construction and cross-document element referencing. Each reference
includes a checksum to verify the integrity of the referenced document.

### Data Model

**ExternalDocumentId**: `string` — Local identifier for the referenced document within this
document (e.g., `DocumentRef-tools`). Used as a prefix when referencing elements across documents.

**Document**: `string` — URI of the referenced SPDX document.

**Checksum**: `SpdxChecksum` — Cryptographic checksum of the referenced document for integrity
verification.

### Key Methods

**DeepCopy**: Returns a new instance with all fields deep-copied.

- *Parameters*: none.
- *Returns*: `SpdxExternalDocumentReference` — independent copy.
- *Preconditions*: none.
- *Postconditions*: The returned instance shares no mutable references with the original.

**Enhance**: Fills in missing fields from another instance.

- *Parameters*: `SpdxExternalDocumentReference other` — source of additional field values.
- *Returns*: `void`
- *Preconditions*: none.
- *Postconditions*: Empty fields in this instance are populated from `other`.

**Enhance (static array merge)**: Merges two external document reference arrays by matching on
`Document` URI.

- *Parameters*: `SpdxExternalDocumentReference[] base`, `SpdxExternalDocumentReference[] additions`.
- *Returns*: `SpdxExternalDocumentReference[]` — merged array.
- *Preconditions*: none.
- *Postconditions*: Entries present in both arrays (matched by `Document` URI via `Same.Equals`)
  are enhanced; new entries from `additions` are deep-copied and appended.

**Validate**: Appends validation issues to the supplied list.

- *Parameters*: `List<string> issues` — list to append issues to.
- *Returns*: `void`
- *Preconditions*: none.
- *Postconditions*: Missing or malformed fields are recorded in `issues`.

**Same**: `static IEqualityComparer<SpdxExternalDocumentReference>` — compares by `Document` URI.

### Error Handling

Validation errors are collected into the `List<string>` passed to `Validate`. The nested
`Checksum` is also validated. No exceptions are thrown by `DeepCopy`, `Enhance`, or the
static merge method.

### Dependencies

- **SpdxChecksum** — integrity checksum for the referenced document.

### Callers

- **SpdxDocument** — holds the `ExternalDocumentReferences` array.
- **Spdx2JsonDeserializer** — constructs `SpdxExternalDocumentReference` instances during deserialization.
- **Spdx2JsonSerializer** — serializes `SpdxExternalDocumentReference` instances to JSON.
