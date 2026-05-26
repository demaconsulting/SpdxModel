### Transform

#### Verification Approach

The Transform subsystem is verified through automated integration tests using the MSTest
framework. Tests are located in
`test/DemaConsulting.SpdxModel.Tests/Transforms/SpdxModelTransformTests.cs`. Integration tests
verify Transform operations using real SpdxDocument instances with no mocked dependencies.

#### Test Environment

N/A - standard test environment.

#### Acceptance Criteria

All integration tests pass with zero failures.

#### Test Scenarios

**SpdxModelTransform_AddRelationship_ToDocument_RelationshipPersists**: Verifies that adding
a relationship to an SpdxDocument through the Transform subsystem results in the relationship
being present in the document's relationship collection after the operation.

**SpdxModelTransform_AddRelationship_InvalidSourceId_ThrowsArgumentException**: Verifies that
providing a source element ID that does not exist in the document causes `AddRelationship` to
throw `ArgumentException`.

**SpdxModelTransform_AddRelationship_InvalidTargetId_ThrowsArgumentException**: Verifies that
providing a target element ID that does not exist in the document (and is neither `NOASSERTION`
nor `DocumentRef-`-prefixed) causes `AddRelationship` to throw `ArgumentException`.

**SpdxModelTransform_AddRelationship_Duplicate_EnhancesExistingRelationship**: Verifies that
adding the same relationship twice does not duplicate the entry — the second add enhances the
existing relationship rather than appending a new one.

**SpdxModelTransform_AddRelationship_Replace_RemovesPreExistingRelationships**: Verifies that
the batch Add overload with `replace=true` removes all pre-existing relationships between the
same source and target elements before adding the new relationship.

**SpdxModelTransform_AddRelationship_BatchMultiple_AddsAllRelationships**: Verifies that
passing multiple distinct relationships in a single batch call results in all of them being
appended to the document.

**SpdxModelTransform_AddRelationship_NoAssertionTarget_AddsRelationship**: Verifies that a
relationship with `NOASSERTION` as the target element is accepted as valid and added without
error.

**SpdxModelTransform_AddRelationship_DocumentRefTarget_AddsRelationship**: Verifies that a
relationship whose target uses the `DocumentRef-` external-reference prefix is accepted as
valid and added without error.
