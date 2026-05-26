## SpdxCreationInformation

### Verification Approach

SpdxCreationInformation is verified through automated unit tests using the MSTest framework.
Tests are located in
`test/DemaConsulting.SpdxModel.Tests/SpdxCreationInformationTests.cs`. Each test constructs
an SpdxCreationInformation instance directly and exercises the method under test with no
mocked dependencies.

### Test Environment

N/A - standard test environment.

### Acceptance Criteria

All automated tests pass with zero failures.

### Test Scenarios

**SpdxCreationInformation_DeepCopy_CreatesEqualButDistinctInstance**: Verifies that a deep
copy produces a new SpdxCreationInformation instance with equal field values but a distinct
object reference, confirming no shared state between original and copy.
This scenario is tested by
`SpdxCreationInformation_DeepCopy_CreatesEqualButDistinctInstance`.

**SpdxCreationInformation_Enhance_AddsOrUpdatesInformationCorrectly**: Verifies that Enhance
merges creation information by adding missing fields from the source while preserving
existing field values on the target.
This scenario is tested by
`SpdxCreationInformation_Enhance_AddsOrUpdatesInformationCorrectly`.

**SpdxCreationInformation_Validate_ValidInformation_NoIssues**: Verifies that validation
reports no issues for a fully populated valid SpdxCreationInformation instance.
This scenario is tested by `SpdxCreationInformation_Validate_ValidInformation_NoIssues`.

**SpdxCreationInformation_Validate_MissingCreators**: Verifies that validation reports an
issue when the Creators list is empty or absent.
This scenario is tested by `SpdxCreationInformation_Validate_MissingCreators`.

**SpdxCreationInformation_Validate_InvalidCreator**: Verifies that validation reports an issue
when one or more entries in the Creators list do not conform to the required tool or
organization format.
This scenario is tested by `SpdxCreationInformation_Validate_InvalidCreator`.

**SpdxCreationInformation_Validate_InvalidCreatedDate**: Verifies that validation reports an
issue when the Created timestamp is missing or does not conform to ISO 8601 date-time format.
This scenario is tested by `SpdxCreationInformation_Validate_InvalidCreatedDate`.

**SpdxCreationInformation_Validate_InvalidVersion**: Verifies that validation reports an issue
when the LicenseListVersion field is present but does not conform to the expected version
format.
This scenario is tested by `SpdxCreationInformation_Validate_InvalidVersion`.

**SpdxCreationInformation_Validate_EmptyCreatedField_NoDateIssue**: Verifies that validation
does not report a date issue when the Created field is empty (empty is a permitted value for
partially-constructed documents).
This scenario is tested by `SpdxCreationInformation_Validate_EmptyCreatedField_NoDateIssue`.

**SpdxCreationInformation_Enhance_DuplicateCreators_DeduplicatesCreators**: Verifies that
Enhance deduplicates the Creators array when merging, ensuring no duplicate entries appear in
the result.
This scenario is tested by `SpdxCreationInformation_Enhance_DuplicateCreators_DeduplicatesCreators`.
