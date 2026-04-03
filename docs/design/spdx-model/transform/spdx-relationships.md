# SpdxRelationships Unit Design

## Purpose

`SpdxRelationships` provides utility methods for adding SPDX relationships to an `SpdxDocument`
without duplication. It simplifies the common pattern of programmatically constructing SPDX
relationship graphs by handling deduplication automatically.

## Design

`SpdxRelationships` is a public static utility class with no instance state.

Key methods:

| Method | Description |
| ------ | ----------- |
| `Add(SpdxDocument, IEnumerable<SpdxRelationship>, bool)` | Adds relationships with deduplication; optional replace |
| `Add(SpdxDocument, SpdxRelationship)` | Adds a single relationship if not already present |

Key design decisions:

- Deduplication is performed using the `SpdxRelationship.Same` equality comparer so that the
  same logical relationship (same elements and type) is never written twice.
- The optional `replace` flag on the batch overload allows callers to update existing
  relationships rather than skip duplicates.

## Dependencies

- `SpdxDocument` — the target document whose `Relationships` array is modified
- `SpdxRelationship` — the relationship type and its `Same` equality comparer
