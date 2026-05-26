## SpdxPackage

### Verification Approach

SpdxPackage is verified through automated unit tests using the MSTest framework. Tests are
located in `test/DemaConsulting.SpdxModel.Tests/SpdxPackageTests.cs`. Each test constructs
an SpdxPackage instance directly and exercises the method under test with no mocked
dependencies.

### Test Environment

N/A - standard test environment.

### Acceptance Criteria

All automated tests pass with zero failures.

### Test Scenarios

**SpdxPackage_SameComparer_ComparesCorrectly**: Verifies that SameComparer correctly
identifies two SpdxPackage instances as equal when all fields match and as distinct when any
field differs.
This scenario is tested by `SpdxPackage_SameComparer_ComparesCorrectly`.

**SpdxPackage_DeepCopy_CreatesEqualButDistinctInstance**: Verifies that a deep copy produces
a new SpdxPackage instance with equal field values but a distinct object reference, including
all nested checksums, verification code, external references, and annotations. Also verifies
that the VerificationCode is deep-copied (value equality but reference distinctness).
This scenario is tested by `SpdxPackage_DeepCopy_CreatesEqualButDistinctInstance`.

**SpdxPackage_Enhance_AddsOrUpdatesPackagesCorrectly**: Verifies that Enhance merges package
data by adding missing fields from the source while preserving existing values on the target.
This scenario is tested by `SpdxPackage_Enhance_AddsOrUpdatesPackagesCorrectly`.

**SpdxPackage_Validate_Success**: Verifies that a fully populated valid SpdxPackage passes
all validation checks without reporting any issues.
This scenario is tested by `SpdxPackage_Validate_Success`.

**SpdxPackage_Validate_MissingPackageName**: Verifies that validation reports an issue when
the package name field is missing or empty.
This scenario is tested by `SpdxPackage_Validate_MissingPackageName`.

**SpdxPackage_Validate_InvalidPackageId**: Verifies that validation reports an issue when
the package SPDX-ID does not conform to the required SPDXRef- prefix format.
This scenario is tested by `SpdxPackage_Validate_InvalidPackageId`.

**SpdxPackage_Validate_MissingDownload**: Verifies that validation reports an issue when the
download location field is missing or empty.
This scenario is tested by `SpdxPackage_Validate_MissingDownload`.

**SpdxPackage_Validate_InvalidSupplier**: Verifies that validation reports an issue when the
supplier field is present but does not conform to the required organization or tool format.
This scenario is tested by `SpdxPackage_Validate_InvalidSupplier`.

**SpdxPackage_Validate_InvalidOriginator**: Verifies that validation reports an issue when
the originator field is present but does not conform to the required organization or tool
format.
This scenario is tested by `SpdxPackage_Validate_InvalidOriginator`.

**SpdxPackage_Validate_InvalidReleaseDate**: Verifies that validation reports an issue when
the release date field is present but does not conform to ISO 8601 date-time format.
This scenario is tested by `SpdxPackage_Validate_InvalidReleaseDate`.

**SpdxPackage_Validate_InvalidBuiltDate**: Verifies that validation reports an issue when the
built date field is present but does not conform to ISO 8601 date-time format.
This scenario is tested by `SpdxPackage_Validate_InvalidBuiltDate`.

**SpdxPackage_Validate_InvalidValidUntilDate**: Verifies that validation reports an issue when
the valid-until date field is present but does not conform to ISO 8601 date-time format.
This scenario is tested by `SpdxPackage_Validate_InvalidValidUntilDate`.

**SpdxPackage_Validate_InvalidAnnotation**: Verifies that validation reports an issue when an
annotation within the package contains invalid fields.
This scenario is tested by `SpdxPackage_Validate_InvalidAnnotation`.

**SpdxPackage_Validate_HasFilesReferencesMissingFile_ReportsIssue**: Verifies that validation
reports an issue when the HasFiles array references a file ID that does not exist in the
owning document.
This scenario is tested by `SpdxPackage_Validate_HasFilesReferencesMissingFile_ReportsIssue`.

**SpdxPackage_ValidateNtia_MissingSupplier_ReportsIssue**: Verifies that NTIA validation
reports an issue when the package supplier field is absent.
This scenario is tested by `SpdxPackage_ValidateNtia_MissingSupplier_ReportsIssue`.

**SpdxPackage_ValidateNtia_MissingVersion_ReportsIssue**: Verifies that NTIA validation
reports an issue when the package version field is absent.
This scenario is tested by `SpdxPackage_ValidateNtia_MissingVersion_ReportsIssue`.
