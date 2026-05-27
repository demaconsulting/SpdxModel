## SpdxChecksum

### Verification Approach

SpdxChecksum is verified through automated unit tests using the xUnit v3 framework. Tests are
located in `test/DemaConsulting.SpdxModel.Tests/SpdxChecksumTests.cs`. Each test constructs
an SpdxChecksum instance directly and exercises the method under test with no mocked
dependencies.

### Test Environment

N/A - standard test environment.

### Acceptance Criteria

All automated tests pass with zero failures.

### Test Scenarios

**SpdxChecksum_SameComparer_SameOrDifferentValues_ReturnsCorrectEquality**: Verifies that
SameComparer correctly identifies two SpdxChecksum instances as equal when algorithm and
value both match, and as distinct when either field differs.
This scenario is tested by `SpdxChecksum_SameComparer_SameOrDifferentValues_ReturnsCorrectEquality`.

**SpdxChecksum_SameComparer_NullFirstArgument_ReturnsFalse**: Verifies that the Same comparer
returns false when the first argument is null.
This scenario is tested by `SpdxChecksum_SameComparer_NullFirstArgument_ReturnsFalse`.

**SpdxChecksum_SameComparer_NullSecondArgument_ReturnsFalse**: Verifies that the Same comparer
returns false when the second argument is null.
This scenario is tested by `SpdxChecksum_SameComparer_NullSecondArgument_ReturnsFalse`.

**SpdxChecksum_SameComparer_BothArgumentsNull_ReturnsTrue**: Verifies that the Same comparer
returns true when both arguments are null.
This scenario is tested by `SpdxChecksum_SameComparer_BothArgumentsNull_ReturnsTrue`.

**SpdxChecksum_DeepCopy_PopulatedChecksum_CreatesEqualButDistinctInstance**: Verifies that a
deep copy produces a new SpdxChecksum instance with equal field values but a distinct object
reference.
This scenario is tested by `SpdxChecksum_DeepCopy_PopulatedChecksum_CreatesEqualButDistinctInstance`.

**SpdxChecksum_Enhance_ExistingAndNewAlgorithms_AddsOrUpdatesInformation**: Verifies that
Enhance merges checksum data by adding missing fields from the source while preserving existing
field values on the target.
This scenario is tested by `SpdxChecksum_Enhance_ExistingAndNewAlgorithms_AddsOrUpdatesInformation`.

**SpdxChecksum_Validate_MissingAlgorithm_ReportsAlgorithmIssue**: Verifies that validation
reports an issue when the checksum algorithm field is set to Missing.
This scenario is tested by `SpdxChecksum_Validate_MissingAlgorithm_ReportsAlgorithmIssue`.

**SpdxChecksum_Validate_EmptyValue_ReportsValueIssue**: Verifies that validation reports an
issue when the checksum value field is empty.
This scenario is tested by `SpdxChecksum_Validate_EmptyValue_ReportsValueIssue`.

**SpdxChecksum_Validate_UnknownNumericAlgorithm_ReportsAlgorithmIssue**: Verifies that
validation reports an issue when the checksum algorithm field holds a numeric value that is
not a named member of the SpdxChecksumAlgorithm enumeration.
This scenario is tested by `SpdxChecksum_Validate_UnknownNumericAlgorithm_ReportsAlgorithmIssue`.

**SpdxChecksumAlgorithmExtensions_FromText_KnownAlgorithmStrings_ReturnsCorrectEnumValues**:
Verifies that FromText correctly parses all recognized checksum algorithm strings
(case-insensitive) to their corresponding enum values.
This scenario is tested by `SpdxChecksumAlgorithmExtensions_FromText_KnownAlgorithmStrings_ReturnsCorrectEnumValues`.

**SpdxChecksumAlgorithmExtensions_FromText_UnknownAlgorithmString_ThrowsInvalidOperationException**:
Verifies that FromText throws `InvalidOperationException` when given an unrecognized checksum
algorithm string.
This scenario is tested by `SpdxChecksumAlgorithmExtensions_FromText_UnknownAlgorithmString_ThrowsInvalidOperationException`.

**SpdxChecksumAlgorithmExtensions_FromText_EmptyString_ReturnsMissing**: Verifies that FromText
returns `Missing` when given an empty string.
This scenario is tested by `SpdxChecksumAlgorithmExtensions_FromText_EmptyString_ReturnsMissing`.

**SpdxChecksumAlgorithmExtensions_ToText_KnownAlgorithmEnums_ReturnsCorrectStrings**: Verifies
that ToText correctly converts all recognized checksum algorithm enum values to their SPDX text
representations.
This scenario is tested by `SpdxChecksumAlgorithmExtensions_ToText_KnownAlgorithmEnums_ReturnsCorrectStrings`.

**SpdxChecksumAlgorithmExtensions_ToText_OutOfRangeEnum_ThrowsInvalidOperationException**:
Verifies that ToText throws `InvalidOperationException` when given a numeric enum value that
does not correspond to any named algorithm member (e.g., `(SpdxChecksumAlgorithm)1000`).
This scenario is tested by `SpdxChecksumAlgorithmExtensions_ToText_OutOfRangeEnum_ThrowsInvalidOperationException`.

**SpdxChecksumAlgorithmExtensions_ToText_MissingAlgorithm_ThrowsInvalidOperationException**:
Verifies that ToText throws `InvalidOperationException` when given the `Missing` sentinel
value, which must never be serialized to SPDX text form.
This scenario is tested by
`SpdxChecksumAlgorithmExtensions_ToText_MissingAlgorithm_ThrowsInvalidOperationException`.
