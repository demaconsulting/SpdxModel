## SpdxExternalReference

### Verification Approach

SpdxExternalReference is verified through automated unit tests using the xUnit v3 framework.
Tests are located in
`test/DemaConsulting.SpdxModel.Tests/SpdxExternalReferenceTests.cs`. Each test constructs
an SpdxExternalReference instance directly and exercises the method under test with no
mocked dependencies.

### Test Environment

N/A - standard test environment.

### Acceptance Criteria

All automated tests pass with zero failures.

### Test Scenarios

**SpdxExternalReference_SameComparer_EqualAndUnequalInstances_ComparesCorrectly**: Verifies that SameComparer
correctly identifies two SpdxExternalReference instances as equal when all fields match and
as distinct when any field differs.
This scenario is tested by `SpdxExternalReference_SameComparer_EqualAndUnequalInstances_ComparesCorrectly`.

**SpdxExternalReference_DeepCopy_WithAllFields_CreatesEqualButDistinctInstance**: Verifies that a deep copy
produces a new SpdxExternalReference with equal field values but a distinct object reference.
This scenario is tested by `SpdxExternalReference_DeepCopy_WithAllFields_CreatesEqualButDistinctInstance`.

**SpdxExternalReference_Enhance_WithMatchingAndNewEntries_MergesCorrectly**: Verifies that Enhance
merges external reference data by adding missing fields from the source while preserving
existing values on the target.
This scenario is tested by
`SpdxExternalReference_Enhance_WithMatchingAndNewEntries_MergesCorrectly`.

**SpdxExternalReference_Validate_InvalidCategory_ReportsIssue**: Verifies that validation reports an issue
when the reference category is `SpdxReferenceCategory.Missing`.
This scenario is tested by `SpdxExternalReference_Validate_InvalidCategory_ReportsIssue`.

**SpdxExternalReference_Validate_InvalidType_ReportsIssue**: Verifies that validation reports an issue when
the reference type does not conform to the expected format for the given category.
This scenario is tested by `SpdxExternalReference_Validate_InvalidType_ReportsIssue`.

**SpdxExternalReference_Validate_InvalidLocator_ReportsIssue**: Verifies that validation reports an issue
when the reference locator field is missing or empty.
This scenario is tested by `SpdxExternalReference_Validate_InvalidLocator_ReportsIssue`.

**SpdxReferenceCategoryExtensions_FromText_ValidInput_ParsesCorrectly**: Verifies that FromText correctly parses a
recognized reference category string to its corresponding enum value.
This scenario is tested by `SpdxReferenceCategoryExtensions_FromText_ValidInput_ParsesCorrectly`.

**SpdxReferenceCategoryExtensions_FromText_InvalidInput_ThrowsInvalidOperationException**: Verifies that `FromText` throws
`InvalidOperationException` with a message identifying the unsupported value when given an
unrecognized reference category string.
This scenario is tested by `SpdxReferenceCategoryExtensions_FromText_InvalidInput_ThrowsInvalidOperationException`.

**SpdxReferenceCategoryExtensions_ToText_ValidReference_FormatsCorrectly**: Verifies that ToText correctly converts a
recognized reference category enum value to its SPDX text representation.
This scenario is tested by `SpdxReferenceCategoryExtensions_ToText_ValidReference_FormatsCorrectly`.

**SpdxReferenceCategoryExtensions_ToText_InvalidCategory_ThrowsInvalidOperationException**: Verifies that ToText throws
`InvalidOperationException` with the unsupported-category message when called with an
unrecognized enum value.
This scenario is tested by `SpdxReferenceCategoryExtensions_ToText_InvalidCategory_ThrowsInvalidOperationException`.

**SpdxReferenceCategoryExtensions_ToText_MissingCategory_ThrowsInvalidOperationException**: Verifies that `ToText` throws
`InvalidOperationException` with a specific message when called with `SpdxReferenceCategory.Missing`.
This scenario is tested by `SpdxReferenceCategoryExtensions_ToText_MissingCategory_ThrowsInvalidOperationException`.
