## SpdxRelationship

### Purpose

`SpdxRelationship` represents a directed relationship between two SPDX elements. Relationships
define the dependency graph, containment hierarchy, and other associations between packages,
files, and snippets in an SPDX document.

### Data Model

**Id** (inherited): `string` — SPDX ID of the source element of the relationship.

**RelatedSpdxElement**: `string` — SPDX ID of the target element. May be `NOASSERTION` or use
a `DocumentRef-` prefix for cross-document references.

**RelationshipType**: `SpdxRelationshipType` — Type of relationship (e.g., `DESCRIBES`,
`CONTAINS`, `DEPENDS_ON`, `GENERATED_FROM`).

**Comment**: `string?` — Optional explanatory comment.

### Key Methods

**DeepCopy**: Returns a new instance with all fields copied.

- *Parameters*: none.
- *Returns*: `SpdxRelationship` — independent copy.
- *Preconditions*: none.
- *Postconditions*: The returned instance shares no mutable references with the original.

**Enhance**: Fills in missing fields from another instance.

- *Parameters*: `SpdxRelationship other` — source of additional field values.
- *Returns*: `void`
- *Preconditions*: none.
- *Postconditions*: Empty or null fields are populated from `other`.

**Enhance (static array merge)**: Merges two relationship arrays by matching on source ID, target
ID, and type.

- *Parameters*: `SpdxRelationship[] base`, `SpdxRelationship[] additions`.
- *Returns*: `SpdxRelationship[]` — merged array.
- *Preconditions*: none.
- *Postconditions*: Matching entries are enhanced; new entries are appended.

**Validate**: Validates the relationship fields and, when a document is provided, verifies that
referenced element IDs exist in that document.

- *Parameters*: `List<string> issues` — list to append issues to; `SpdxDocument? document` —
  optional document for element ID resolution.
- *Returns*: `void`
- *Preconditions*: none.
- *Postconditions*: An empty source ID, an empty related element ID, or a `Missing` relationship
  type each produce a validation issue. When `document` is provided, source and related element
  IDs that do not resolve to an element in that document also produce issues, unless the related
  element is `NOASSERTION` or uses a `DocumentRef-` cross-document prefix.

**Same**: `static IEqualityComparer<SpdxRelationship>` — compares by source ID, target ID, and
relationship type.

**SameElements**: `static IEqualityComparer<SpdxRelationship>` — compares by source ID and
target ID only, ignoring relationship type. Used by `SpdxRelationships.Add` when `replace` is
`true`.

### Error Handling

Validation errors are collected into the `List<string>` passed to `Validate`. No exceptions are
thrown by `DeepCopy`, `Enhance`, or the static merge method.

### Dependencies

- **SpdxElement** — base class providing the `Id` property.
- **SpdxRelationshipType** — enumeration of relationship types.
- **SpdxDocument** — used during `Validate` to resolve element IDs.

### Callers

- **SpdxDocument** — holds the `Relationships` array.
- **SpdxRelationships** — adds and deduplicates relationships in a document.
- **Spdx2JsonDeserializer** — constructs `SpdxRelationship` instances during deserialization.
- **Spdx2JsonSerializer** — serializes `SpdxRelationship` instances to JSON.

### SpdxRelationshipType

#### Overview

`SpdxRelationshipType` is an enumeration of SPDX-defined relationship type tokens and the
`SpdxRelationshipTypeExtensions` static class provides round-trip text conversion between enum
values and their canonical SPDX string representations.

#### Enum Values

The enumeration defines 45 relationship type values plus the sentinel value `Missing` (= -1).
Key values include:

| Enum Value      | SPDX String                   |
|-----------------|-------------------------------|
| `Missing`       | (sentinel — not serializable) |
| `Describes`     | `DESCRIBES`                   |
| `Contains`      | `CONTAINS`                    |
| `DependsOn`     | `DEPENDS_ON`                  |
| `GeneratedFrom` | `GENERATED_FROM`              |
| …               | … (45 values total)           |

#### Conversion Methods

**FromText**: Converts a string to `SpdxRelationshipType`.

- *Parameters*: `string relationshipType` — SPDX relationship type string (case-insensitive);
  `null` is treated as empty string.
- *Returns*: `SpdxRelationshipType` — corresponding enum value.
- *Postconditions*: An empty or null string returns `Missing`.
- *Throws*: `InvalidOperationException` when the string is not recognized.

**ToText**: Converts a `SpdxRelationshipType` to its canonical SPDX string.

- *Parameters*: `SpdxRelationshipType relationshipType` — enum value to convert.
- *Returns*: `string` — SPDX text representation.
- *Throws*: `InvalidOperationException` when `relationshipType` is `Missing` or an unrecognized enum value.

#### Enum Error Handling

- `FromText` throws `InvalidOperationException` for unrecognized (non-empty) strings.
- `ToText` throws `InvalidOperationException` for `Missing` or out-of-range enum values.
- Neither method performs I/O or has side effects.

#### Dependencies and Callers

- Consumed by **Spdx2JsonDeserializer** (via `FromText`) during JSON parsing.
- Consumed by **Spdx2JsonSerializer** (via `ToText`) during JSON serialization.
