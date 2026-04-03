# SpdxExternalDocumentReference Unit Design

## Purpose

`SpdxExternalDocumentReference` represents a reference from one SPDX document to another,
enabling modular SBOM construction and cross-document element referencing. Each reference
includes a checksum to verify the referenced document's integrity.

## Design

`SpdxExternalDocumentReference` is a sealed class with no base class (not an `SpdxElement`).

Data members:

| Property | Type | Description |
| -------- | ---- | ----------- |
| `ExternalDocumentId` | `string` | Local identifier for the referenced document (e.g., `DocumentRef-tools`) |
| `Document` | `string` | URI of the referenced SPDX document |
| `Checksum` | `SpdxChecksum` | Checksum of the referenced document for integrity verification |

Key methods:

- `DeepCopy()` — returns a new instance with all fields deep-copied
- `Enhance(SpdxExternalDocumentReference)` — fills in missing fields from another instance
- `Enhance(array, array)` — static method merging two arrays by matching on `ExternalDocumentId`
- `Validate(List<string>)` — appends validation issues to the supplied list
- `Same` — static `IEqualityComparer` comparing by `Document`

## Dependencies

- `SpdxChecksum` — integrity checksum for the referenced document
