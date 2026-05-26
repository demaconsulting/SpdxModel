## SpdxFile

### Verification Approach

SpdxFile is verified through automated unit tests using the MSTest framework. Tests are
located in `test/DemaConsulting.SpdxModel.Tests/SpdxFileTests.cs`. Each test constructs an
SpdxFile instance directly and exercises the method under test with no mocked dependencies.

### Test Environment

N/A - standard test environment.

### Acceptance Criteria

All automated tests pass with zero failures.

### Test Scenarios

**SpdxFile_SameComparer_ComparesCorrectly**: Verifies that SameComparer correctly identifies
two SpdxFile instances as equal when all fields match and as distinct when any field differs.
This scenario is tested by `SpdxFile_SameComparer_ComparesCorrectly`.

**SpdxFile_DeepCopy_CreatesEqualButDistinctInstance**: Verifies that a deep copy produces a
new SpdxFile instance with equal field values but a distinct object reference, including all
nested checksums and annotations.
This scenario is tested by `SpdxFile_DeepCopy_CreatesEqualButDistinctInstance`.

**SpdxFile_Enhance_AddsOrUpdatesInformationCorrectly**: Verifies that Enhance merges file
data by adding missing fields from the source while preserving existing field values on the
target.
This scenario is tested by `SpdxFile_Enhance_AddsOrUpdatesInformationCorrectly`.

**SpdxFile_Validate_ReportsInvalidFileId**: Verifies that validation reports an issue when
the file SPDX-ID does not conform to the required SPDXRef- prefix format.
This scenario is tested by `SpdxFile_Validate_ReportsInvalidFileId`.

**SpdxFile_Validate_ReportsInvalidFileName**: Verifies that validation reports an issue when
the FileName does not start with the required "./" prefix.
This scenario is tested by `SpdxFile_Validate_ReportsInvalidFileName`.

**SpdxFile_Validate_ReportsWhenSha1ChecksumMissing**: Verifies that validation reports an
issue when no SHA1 checksum is present in the Checksums array.
This scenario is tested by `SpdxFile_Validate_ReportsWhenSha1ChecksumMissing`.

**SpdxFile_Validate_Success**: Verifies that a fully populated valid SpdxFile passes all
validation checks without reporting any issues.
This scenario is tested by `SpdxFile_Validate_Success`.

**SpdxFileTypeExtensions_FromText_Valid**: Verifies that FromText correctly parses a
recognized file type string to its corresponding enum value, and that matching is
case-insensitive.
This scenario is tested by `SpdxFileTypeExtensions_FromText_Valid`.

**SpdxFileTypeExtensions_FromText_Invalid**: Verifies that `FromText` throws
`InvalidOperationException` with a message identifying the unsupported value when given an
unrecognized file type string.
This scenario is tested by `SpdxFileTypeExtensions_FromText_Invalid`.

**SpdxFileTypeExtensions_ToText_Valid**: Verifies that ToText correctly converts a recognized
file type enum value to its SPDX text representation.
This scenario is tested by `SpdxFileTypeExtensions_ToText_Valid`.

**SpdxFileTypeExtensions_ToText_Invalid**: Verifies that `ToText` throws
`InvalidOperationException` when given an unsupported file type enum value.
This scenario is tested by `SpdxFileTypeExtensions_ToText_Invalid`.
