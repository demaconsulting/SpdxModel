## SpdxRelationship

### Verification Approach

SpdxRelationship is verified through automated unit tests using the MSTest framework. Tests
are located in `test/DemaConsulting.SpdxModel.Tests/SpdxRelationshipTests.cs`. Each test
constructs an SpdxRelationship instance directly and exercises the method under test with no
mocked dependencies.

### Test Environment

N/A - standard test environment.

### Acceptance Criteria

All automated tests pass with zero failures.

### Test Scenarios

**SpdxRelationship_SameComparer_SameFieldsDifferentComment_ReturnsEqual**: Verifies that SameComparer correctly
identifies two SpdxRelationship instances as equal when all fields match and as distinct when
any field differs.
This scenario is tested by `SpdxRelationship_SameComparer_SameFieldsDifferentComment_ReturnsEqual`.

**SpdxRelationship_SameElementsComparer_SameElementsDifferentType_ReturnsEqual**: Verifies that
SameElementsComparer correctly identifies two relationships as equal based solely on the
source and target element IDs, ignoring relationship type.
This scenario is tested by `SpdxRelationship_SameElementsComparer_SameElementsDifferentType_ReturnsEqual`.

**SpdxRelationship_DeepCopy_FullyPopulatedRelationship_CreatesEqualButDistinctCopy**: Verifies that a deep copy
produces a new SpdxRelationship instance with equal field values but a distinct object
reference.
This scenario is tested by `SpdxRelationship_DeepCopy_FullyPopulatedRelationship_CreatesEqualButDistinctCopy`.

**SpdxRelationship_Enhance_MatchingAndNewRelationships_MergesCorrectly**: Verifies that Enhance merges
relationship data by adding missing fields from the source while preserving existing values
on the target.
This scenario is tested by `SpdxRelationship_Enhance_MatchingAndNewRelationships_MergesCorrectly`.

**SpdxRelationship_Validate_MissingRelationshipId_ReportsIssue**: Verifies that validation reports an issue when the
relationship SPDX-ID field is missing or empty.
This scenario is tested by `SpdxRelationship_Validate_MissingRelationshipId_ReportsIssue`.

**SpdxRelationship_Validate_MissingRelatedElementId_ReportsIssue**: Verifies that validation reports an issue
when the related element ID field is missing or empty.
This scenario is tested by `SpdxRelationship_Validate_MissingRelatedElementId_ReportsIssue`.

**SpdxRelationship_Validate_MissingRelationshipType_ReportsIssue**: Verifies that validation reports an issue
when the relationship type field is missing or set to an unrecognized value.
This scenario is tested by `SpdxRelationship_Validate_MissingRelationshipType_ReportsIssue`.

**SpdxRelationshipTypeExtensions_FromText_KnownText_ReturnsMappedEnum**: Verifies that FromText correctly parses a
recognized relationship type string to its corresponding enum value.
This scenario is tested by `SpdxRelationshipTypeExtensions_FromText_KnownText_ReturnsMappedEnum`.

**SpdxRelationshipTypeExtensions_FromText_UnknownText_ThrowsInvalidOperationException**: Verifies that FromText throws an
InvalidOperationException with a descriptive message when given an unrecognized relationship
type string.
This scenario is tested by `SpdxRelationshipTypeExtensions_FromText_UnknownText_ThrowsInvalidOperationException`.

**SpdxRelationshipTypeExtensions_ToText_KnownEnum_ReturnsMappedText**: Verifies that ToText correctly converts a
recognized relationship type enum value to its SPDX text representation.
This scenario is tested by `SpdxRelationshipTypeExtensions_ToText_KnownEnum_ReturnsMappedText`.

**SpdxRelationshipTypeExtensions_ToText_UnknownEnum_ThrowsInvalidOperationException**: Verifies that ToText throws an
InvalidOperationException with a descriptive message when given an unknown (out-of-range)
relationship type enum value.
This scenario is tested by `SpdxRelationshipTypeExtensions_ToText_UnknownEnum_ThrowsInvalidOperationException`.
