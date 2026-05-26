## SpdxExtractedLicensingInfo

### Verification Approach

SpdxExtractedLicensingInfo is verified through automated unit tests using the MSTest
framework. Tests are located in
`test/DemaConsulting.SpdxModel.Tests/SpdxExtractedLicensingInfoTests.cs`. Each test
constructs an SpdxExtractedLicensingInfo instance directly and exercises the method under
test with no mocked dependencies.

### Test Environment

N/A - standard test environment.

### Acceptance Criteria

All automated tests pass with zero failures.

### Test Scenarios

**SpdxExtractedLicensingInfo_SameComparer_ComparesCorrectly**: Verifies that SameComparer
correctly identifies two SpdxExtractedLicensingInfo instances as equal when all fields match
and as distinct when any field differs.
This scenario is tested by `SpdxExtractedLicensingInfo_SameComparer_ComparesCorrectly`.

**SpdxExtractedLicensingInfo_DeepCopy_CreatesEqualButDistinctInstance**: Verifies that a deep
copy produces a new SpdxExtractedLicensingInfo instance with equal field values but a
distinct object reference, including array independence for CrossReferences.
This scenario is tested by
`SpdxExtractedLicensingInfo_DeepCopy_CreatesEqualButDistinctInstance`.

**SpdxExtractedLicensingInfo_Enhance_AddsOrUpdatesInformationCorrectly**: Verifies that
Enhance merges extracted licensing information by adding missing fields from the source while
preserving existing values on the target.
This scenario is tested by
`SpdxExtractedLicensingInfo_Enhance_AddsOrUpdatesInformationCorrectly`.

**SpdxExtractedLicensingInfo_Validate_ValidInput_ReturnsNoIssues**: Verifies that a valid
extracted licensing info with both LicenseId and ExtractedText populated returns no issues.
This scenario is tested by `SpdxExtractedLicensingInfo_Validate_ValidInput_ReturnsNoIssues`.

**SpdxExtractedLicensingInfo_Validate_InvalidLicenseId**: Verifies that validation reports an
issue when the LicenseId field is empty.
This scenario is tested by `SpdxExtractedLicensingInfo_Validate_InvalidLicenseId`.

**SpdxExtractedLicensingInfo_Validate_InvalidExtractedText**: Verifies that validation reports
an issue when the extracted license text field is missing or empty.
This scenario is tested by `SpdxExtractedLicensingInfo_Validate_InvalidExtractedText`.
