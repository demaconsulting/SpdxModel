## SpdxPackageVerificationCode

### Verification Approach

SpdxPackageVerificationCode is verified through automated unit tests using the xUnit v3
framework. Tests are located in
`test/DemaConsulting.SpdxModel.Tests/SpdxPackageVerificationCodeTests.cs`. Each test
constructs an SpdxPackageVerificationCode instance directly and exercises the method under
test with no mocked dependencies.

### Test Environment

N/A - standard test environment.

### Acceptance Criteria

All automated tests pass with zero failures.

### Test Scenarios

**SpdxPackageVerificationCode_SameComparer_SameValueDifferentExcludedFiles_ReturnsEqual**: Verifies that SameComparer
correctly identifies two SpdxPackageVerificationCode instances as equal when their Value
fields are identical (even if ExcludedFiles differs), and as distinct when Values differ.
Equality is determined by Value alone.
This scenario is tested by `SpdxPackageVerificationCode_SameComparer_SameValueDifferentExcludedFiles_ReturnsEqual`.

**SpdxPackageVerificationCode_DeepCopy_FullyPopulatedCode_CreatesEqualButDistinctCopy**: Verifies that a
deep copy produces a new SpdxPackageVerificationCode instance with equal field values but a
distinct object reference.
This scenario is tested by
`SpdxPackageVerificationCode_DeepCopy_FullyPopulatedCode_CreatesEqualButDistinctCopy`.

**SpdxPackageVerificationCode_Enhance_MissingFields_MergesCorrectly**: Verifies that
Enhance merges verification code data by adding missing fields from the source while
preserving existing values on the target.
This scenario is tested by
`SpdxPackageVerificationCode_Enhance_MissingFields_MergesCorrectly`.

**SpdxPackageVerificationCode_Validate_InvalidValue_ReportsIssue**: Verifies that validation reports an
issue when the verification code value field is missing or does not conform to the required
SHA1 hash format.
This scenario is tested by `SpdxPackageVerificationCode_Validate_InvalidValue_ReportsIssue`.

**SpdxPackageVerificationCode_Validate_ValidValue_ReportsNoIssues**: Verifies that validation
reports no issues when the verification code value is a valid 40-character SHA1 hex digest.
This scenario is tested by `SpdxPackageVerificationCode_Validate_ValidValue_ReportsNoIssues`.

**SpdxPackageVerificationCode_Validate_NonHexValue_ReportsIssue**: Verifies that validation
reports an issue when the verification code value is 40 characters but contains non-hexadecimal
characters.
This scenario is tested by `SpdxPackageVerificationCode_Validate_NonHexValue_ReportsIssue`.
