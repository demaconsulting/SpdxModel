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

**SpdxRelationship_SameComparer_MatchingRelationships_ReturnsTrue**: Verifies that SameComparer correctly
identifies two SpdxRelationship instances as equal when the key fields (Id, RelationshipType, RelatedSpdxElement)
match, even when Comment differs.
This scenario is tested by `SpdxRelationship_SameComparer_MatchingRelationships_ReturnsTrue`.

**SpdxRelationship_SameComparer_DifferentRelationships_ReturnsFalse**: Verifies that SameComparer correctly
identifies two SpdxRelationship instances as distinct when any key field differs.
This scenario is tested by `SpdxRelationship_SameComparer_DifferentRelationships_ReturnsFalse`.

**SpdxRelationship_SameComparer_MatchingRelationships_ReturnsSameHashCode**: Verifies that SameComparer
produces identical hash codes for two relationships that are considered equal, satisfying the hash/equality contract.
This scenario is tested by `SpdxRelationship_SameComparer_MatchingRelationships_ReturnsSameHashCode`.

**SpdxRelationship_SameElementsComparer_MatchingElements_ReturnsTrue**: Verifies that
SameElementsComparer correctly identifies two relationships as equal based solely on the
source and target element IDs, ignoring relationship type.
This scenario is tested by `SpdxRelationship_SameElementsComparer_MatchingElements_ReturnsTrue`.

**SpdxRelationship_SameElementsComparer_DifferentElements_ReturnsFalse**: Verifies that
SameElementsComparer correctly identifies two relationships as distinct when their source or
target element IDs differ.
This scenario is tested by `SpdxRelationship_SameElementsComparer_DifferentElements_ReturnsFalse`.

**SpdxRelationship_SameElementsComparer_MatchingElements_ReturnsSameHashCode**: Verifies that
SameElementsComparer produces identical hash codes for two relationships with the same element
IDs, satisfying the hash/equality contract.
This scenario is tested by `SpdxRelationship_SameElementsComparer_MatchingElements_ReturnsSameHashCode`.

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

**SpdxRelationshipTypeExtensions_ToText_MissingSentinel_ThrowsInvalidOperationException**: Verifies that ToText
throws an InvalidOperationException with message "Attempt to serialize missing SPDX Relationship Type" when
called on the Missing sentinel value.
This scenario is tested by `SpdxRelationshipTypeExtensions_ToText_MissingSentinel_ThrowsInvalidOperationException`.

**SpdxRelationshipTypeExtensions_ToText_UnknownEnum_ThrowsInvalidOperationException**: Verifies that ToText throws an
InvalidOperationException with a descriptive message when given an unknown (out-of-range)
relationship type enum value.
This scenario is tested by `SpdxRelationshipTypeExtensions_ToText_UnknownEnum_ThrowsInvalidOperationException`.
