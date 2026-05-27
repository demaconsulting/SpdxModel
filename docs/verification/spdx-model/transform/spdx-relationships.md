### SpdxRelationships

#### Verification Approach

SpdxRelationships is verified through automated unit tests using the xUnit v3 framework. Tests
are located in `test/DemaConsulting.SpdxModel.Tests/Transforms/SpdxRelationshipsTests.cs`.
Each test constructs an SpdxDocument with a known set of relationships and exercises the
SpdxRelationships methods directly with no mocked dependencies.

#### Test Environment

N/A - standard test environment.

#### Acceptance Criteria

All automated tests pass with zero failures.

#### Test Scenarios

**SpdxRelationships_AddSingle_MissingId_ThrowsArgumentException**: Verifies that attempting to add
a single relationship where the source element ID does not exist in the document throws
`ArgumentException` with a message identifying the missing element and leaves the document
unmodified.

**SpdxRelationships_AddSingle_MissingRelatedElement_ThrowsArgumentException**: Verifies that
attempting to add a single relationship where the related element ID does not exist in the document
(and is neither NOASSERTION nor DocumentRef-prefixed) throws `ArgumentException` and leaves the
document unmodified.

**SpdxRelationships_AddSingle_ValidRelationship_AddsRelationship**: Verifies that adding a single
valid relationship between two existing elements results in the relationship being appended to the
document's relationships collection.

**SpdxRelationships_AddSingle_DuplicateRelationship_EnhancesExistingRelationship**: Verifies that
adding a relationship that is identical to one already present in the document enhances the existing
entry rather than creating a duplicate.

**SpdxRelationships_AddSingle_NoAssertionTarget_AddsRelationship**: Verifies that a relationship
whose target element is `NOASSERTION` is accepted as valid and added to the document without error.

**SpdxRelationships_AddSingle_DocumentRefTarget_AddsRelationship**: Verifies that a relationship
whose target element uses the `DocumentRef-` external-reference prefix is accepted as valid and
added to the document without error.

**SpdxRelationships_AddMultiple_SingleRelationship_AddsRelationship**: Verifies that the batch Add
overload with a single-element array appends the relationship to the document.

**SpdxRelationships_AddMultiple_DuplicateRelationships_DeduplicatesRelationships**: Verifies that
passing duplicate relationships in a single batch call results in only one entry being added to the
document.

**SpdxRelationships_AddMultiple_Replace_RemovesAndReplacesExistingRelationships**: Verifies that
invoking the batch Add with `replace=true` removes pre-existing relationships between the same
source and target elements before adding the new ones.

**SpdxRelationships_AddMultiple_InvalidRelationship_LeavesDocumentUnmodified**: Verifies that when
any relationship in a batch is invalid (e.g., missing source ID), an `ArgumentException` is thrown
and the document's relationships collection is left in its original state — no relationships are
added or removed.
