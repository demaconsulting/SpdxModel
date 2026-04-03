# SpdxRelationship Unit Design

## Purpose

`SpdxRelationship` represents a directed relationship between two SPDX elements. Relationships
define the dependency graph, containment hierarchy, and other associations between packages,
files, and snippets in an SPDX document.

## Design

`SpdxRelationship` is a sealed class that extends `SpdxElement` (inheriting the `Id` field,
which identifies the *source* element of the relationship).

Data members:

| Property | Type | Description |
| -------- | ---- | ----------- |
| `Id` (inherited) | `string` | SPDX ID of the source element |
| `RelatedSpdxElement` | `string` | SPDX ID of the target element |
| `RelationshipType` | `SpdxRelationshipType` | Type of relationship (DESCRIBES, CONTAINS, DEPENDS_ON, etc.) |
| `Comment` | `string?` | Optional explanatory comment |

Key methods:

- `DeepCopy()` — returns a new instance with all fields copied
- `Enhance(SpdxRelationship)` — fills in missing fields from another instance
- `Enhance(array, array)` — static method merging two relationship arrays by source, target, and type
- `Validate(List<string>, SpdxDocument?)` — validates element ID references exist in the document
- `Same` — static `IEqualityComparer` comparing source, target, and type
- `SameElements` — static `IEqualityComparer` comparing only source and target (ignoring type)

## Dependencies

- `SpdxElement` (base class)
- `SpdxRelationshipType` (enum)
- `SpdxDocument` — used during validation to resolve element IDs
