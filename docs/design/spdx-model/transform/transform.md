### Transform

The Transform subsystem provides utilities for manipulating SPDX documents in memory, enabling
consumers to programmatically build and modify SPDX relationship graphs.

#### Overview

The Transform subsystem contains a single unit, `SpdxRelationships`, which provides static
helper methods for adding relationships to an `SpdxDocument`. It handles deduplication
automatically so that callers do not need to check for existing relationships before adding new
ones.

#### Interfaces

**SpdxRelationships.Add (batch)**: Adds multiple relationships to a document with deduplication.

- *Type*: In-process .NET public API
- *Role*: Provider
- *Contract*: Accepts an `SpdxDocument`, an `IEnumerable<SpdxRelationship>`, and an optional
  `replace` flag. Adds each relationship if not already present; when `replace` is `true`,
  existing relationships with matching source and target elements are removed first.
- *Constraints*: The source element ID must always exist in the document; the target element ID
  must either exist in the document, be `NOASSERTION`, or use the `DocumentRef-` external-reference
  prefix. An `ArgumentException` is thrown when either constraint is violated.
- *Atomicity*: When `ArgumentException` is thrown, the document is left in its original state —
  no relationships are added or removed.

**SpdxRelationships.Add (single)**: Adds a single relationship to a document if not already present.

- *Type*: In-process .NET public API
- *Role*: Provider
- *Contract*: Accepts an `SpdxDocument` and an `SpdxRelationship`. If the same relationship
  (same source, target, and type) already exists it is enhanced; otherwise a deep copy is appended.
- *Constraints*: The source element ID must exist in the document. The target element ID must
  either exist in the document, be `NOASSERTION`, or use the `DocumentRef-` prefix.

#### Design

`SpdxRelationships` is a static class with no instance state:

The batch `Add` overload executes in three phases to preserve atomicity:

1. **Pre-validate**: Materialize the incoming enumerable into an array and call the internal
   `ValidateRelationship` helper on every relationship. If any validation fails an
   `ArgumentException` is thrown immediately and the document is left unchanged.
2. **Replace** (when `replace` is `true`): Remove all existing relationships whose source and
   target element IDs match any incoming relationship, using `SpdxRelationship.SameElements`
   (type-agnostic comparison).
3. **Add**: Call the internal `AddValidated` helper for each incoming relationship. `AddValidated`
   searches for an existing match using `SpdxRelationship.Same` (type-inclusive); if found it
   calls `Enhance`, otherwise it appends a `DeepCopy`.

The single `Add` overload delegates directly: it calls `ValidateRelationship` then `AddValidated`.
