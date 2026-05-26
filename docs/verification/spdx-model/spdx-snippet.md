## SpdxSnippet

### Verification Approach

SpdxSnippet is verified through automated unit tests using the MSTest framework. Tests are
located in `test/DemaConsulting.SpdxModel.Tests/SpdxSnippetTests.cs`. Each test constructs
an SpdxSnippet instance directly and exercises the method under test with no mocked
dependencies.

### Test Environment

N/A - standard test environment.

### Acceptance Criteria

All automated tests pass with zero failures.

### Test Scenarios

**SpdxSnippet_SameComparer_SameFileAndByteRange_ReturnsEqual**: Verifies that SameComparer correctly
identifies two SpdxSnippet instances as equal when all fields match and as distinct when any
field differs.
This scenario is tested by `SpdxSnippet_SameComparer_SameFileAndByteRange_ReturnsEqual`.

**SpdxSnippet_DeepCopy_FullyPopulatedSnippet_CreatesEqualButDistinctCopy**: Verifies that a deep copy produces
a new SpdxSnippet instance with equal field values but a distinct object reference, including
all nested byte and line ranges.
This scenario is tested by `SpdxSnippet_DeepCopy_FullyPopulatedSnippet_CreatesEqualButDistinctCopy`.

**SpdxSnippet_Enhance_MatchingAndNewSnippets_MergesCorrectly**: Verifies that Enhance merges
snippet data by adding missing fields from the source while preserving existing values on
the target.
This scenario is tested by `SpdxSnippet_Enhance_MatchingAndNewSnippets_MergesCorrectly`.

**SpdxSnippet_Validate_InvalidSnippetId_ReportsIssue**: Verifies that validation reports an issue
when the snippet SPDX-ID does not conform to the required SPDXRef- prefix format.
This scenario is tested by `SpdxSnippet_Validate_InvalidSnippetId_ReportsIssue`.

**SpdxSnippet_Validate_AllRequiredFieldsPresent_ReturnsNoIssues**: Verifies that a fully populated valid SpdxSnippet passes
all validation checks without reporting any issues.
This scenario is tested by `SpdxSnippet_Validate_AllRequiredFieldsPresent_ReturnsNoIssues`.

**SpdxSnippet_Validate_InvalidAnnotation_ReportsIssue**: Verifies that validation reports an issue when an
annotation within the snippet contains invalid fields.
This scenario is tested by `SpdxSnippet_Validate_InvalidAnnotation_ReportsIssue`.

**SpdxSnippet_Validate_EmptySnippetFromFile_ReportsIssue**: Verifies that validation reports an
issue when the snippet-from-file field is empty.
This scenario is tested by `SpdxSnippet_Validate_EmptySnippetFromFile_ReportsIssue`.

**SpdxSnippet_Validate_InvalidByteStart_ReportsIssue**: Verifies that validation reports an issue
when the snippet byte range start is less than 1.
This scenario is tested by `SpdxSnippet_Validate_InvalidByteStart_ReportsIssue`.

**SpdxSnippet_Validate_InvalidByteEnd_ReportsIssue**: Verifies that validation reports an issue
when the snippet byte range end is less than the byte range start.
This scenario is tested by `SpdxSnippet_Validate_InvalidByteEnd_ReportsIssue`.

**SpdxSnippet_Validate_EmptyConcludedLicense_ReportsIssue**: Verifies that validation reports an
issue when the concluded license field is empty.
This scenario is tested by `SpdxSnippet_Validate_EmptyConcludedLicense_ReportsIssue`.

**SpdxSnippet_Validate_EmptyCopyrightText_ReportsIssue**: Verifies that validation reports an issue
when the copyright text field is empty.
This scenario is tested by `SpdxSnippet_Validate_EmptyCopyrightText_ReportsIssue`.
