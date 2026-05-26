### SpdxRelationships

#### Purpose

`SpdxRelationships` provides utility methods for adding SPDX relationships to an `SpdxDocument`
without duplication. It simplifies the common pattern of programmatically constructing SPDX
relationship graphs by handling deduplication automatically.

#### Data Model

N/A - `SpdxRelationships` is a public static utility class with no instance state.

#### Key Methods

**Add (batch)**: Adds multiple relationships to a document with optional replacement.

- *Parameters*: `SpdxDocument document` — target document; `IEnumerable<SpdxRelationship> relationships` —
  relationships to add; `bool replace = false` — when `true`, existing relationships with matching
  source and target elements are removed before adding.
- *Returns*: `void`
- *Preconditions*: Each relationship's source element ID must exist in the document. Each
  relationship's target element ID must either exist in the document, be `NOASSERTION`, or use
  the `DocumentRef-` prefix.
- *Postconditions*: All supplied relationships are present in the document; if `replace` is `true`,
  previously existing relationships with the same source and target are removed.
- *Note*: When `replace` is `true`, removal uses `SpdxRelationship.SameElements` (type-agnostic,
  matches by source and target ID only) so that a replace-and-add can change the relationship type
  between the same pair of elements. Deduplication during the add path uses `SpdxRelationship.Same`
  (type-inclusive) so that relationships of different types between the same elements co-exist.

**Add (single)**: Adds a single relationship to the document if not already present.

- *Parameters*: `SpdxDocument document` — target document; `SpdxRelationship relationship` —
  relationship to add.
- *Returns*: `void`
- *Preconditions*: The relationship source ID must match an element in the document. The target
  ID must either match an element in the document, be `NOASSERTION`, or use the `DocumentRef-`
  external-reference prefix.
- *Postconditions*: If the same logical relationship (same source, target, and type) already
  exists, it is enhanced with any new field values. Otherwise a deep copy is appended.

#### Error Handling

An `ArgumentException` (parameter name: `relationship`) is thrown under two conditions:

1. The relationship's source element ID (`SpdxRelationship.Id`) is not found in the document.
2. The relationship's target element ID (`SpdxRelationship.RelatedSpdxElement`) is not found
   in the document and is neither `NOASSERTION` nor prefixed with `DocumentRef-`.

These checks prevent malformed documents from being constructed. In the batch overload all
relationships are validated before any mutation so that a failure leaves the document unchanged.

#### Dependencies

- **SpdxDocument** — the target document whose `Relationships` array is modified.
- **SpdxRelationship** — the relationship type and its `Same` and `SameElements` equality comparers.

#### Callers

- External consumers of the library who programmatically build or modify SPDX relationship graphs.
