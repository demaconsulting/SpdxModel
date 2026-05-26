## SpdxExternalDocumentReference

### Verification Approach

SpdxExternalDocumentReference is verified through automated unit tests using the MSTest
framework. Tests are located in
`test/DemaConsulting.SpdxModel.Tests/SpdxExternalDocumentReferenceTests.cs`. Each test
constructs an SpdxExternalDocumentReference instance directly and exercises the method under
test with no mocked dependencies.

### Test Environment

N/A - standard test environment.

### Acceptance Criteria

All automated tests pass with zero failures.

### Test Scenarios

**SpdxExternalDocumentReference_SameComparer_ComparesCorrectly**: Verifies the comparer
considers two instances equal when their `Document` URI matches, and distinct when the URI
differs (regardless of `ExternalDocumentId`).
This scenario is tested by
`SpdxExternalDocumentReference_SameComparer_ComparesCorrectly`.

**SpdxExternalDocumentReference_DeepCopy_CreatesEqualButDistinctInstance**: Verifies that a
deep copy produces a new SpdxExternalDocumentReference with equal field values but a distinct
object reference.
This scenario is tested by
`SpdxExternalDocumentReference_DeepCopy_CreatesEqualButDistinctInstance`.

**SpdxExternalDocumentReference_Enhance_AddsOrUpdatesInformationCorrectly**: Verifies that
Enhance merges external document reference data by adding missing fields from the source
while preserving existing values on the target.
This scenario is tested by
`SpdxExternalDocumentReference_Enhance_AddsOrUpdatesInformationCorrectly`.

**SpdxExternalDocumentReference_Validate_MissingId**: Verifies that validation reports an
issue when the external document reference ID field is missing or empty.
This scenario is tested by `SpdxExternalDocumentReference_Validate_MissingId`.

**SpdxExternalDocumentReference_Validate_MissingDocument**: Verifies that validation reports
an issue when the referenced external document URI is missing or empty.
This scenario is tested by `SpdxExternalDocumentReference_Validate_MissingDocument`.

**SpdxExternalDocumentReference_Validate_InvalidChecksum_ReportsIssue**: Verifies that
validation reports an issue when the external document reference contains an invalid checksum
(missing algorithm or empty value).
This scenario is tested by `SpdxExternalDocumentReference_Validate_InvalidChecksum_ReportsIssue`.
