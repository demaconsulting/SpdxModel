## SpdxAnnotation

### Verification Approach

SpdxAnnotation is verified through automated unit tests using the xUnit v3 framework. Tests are
located in `test/DemaConsulting.SpdxModel.Tests/SpdxAnnotationTests.cs`. Each test constructs
an SpdxAnnotation instance directly and exercises the method under test with no mocked
dependencies.

### Test Environment

N/A - standard test environment.

### Acceptance Criteria

All automated tests pass with zero failures.

### Test Scenarios

**SpdxAnnotation_SameComparer_ComparesCorrectly**: Verifies that SameComparer correctly
identifies two SpdxAnnotation instances as equal when all fields match and as distinct when
any field differs.
This scenario is tested by `SpdxAnnotation_SameComparer_ComparesCorrectly`.

**SpdxAnnotation_DeepCopy_CreatesEqualButDistinctInstance**: Verifies that a deep copy
produces a new SpdxAnnotation instance with equal field values but a distinct object reference,
confirming no shared state between original and copy.
This scenario is tested by `SpdxAnnotation_DeepCopy_CreatesEqualButDistinctInstance`.

**SpdxAnnotation_Enhance_AddsOrUpdatesInformationCorrectly**: Verifies that Enhance merges
annotation data by adding missing fields from the source while preserving existing field
values on the target.
This scenario is tested by `SpdxAnnotation_Enhance_AddsOrUpdatesInformationCorrectly`.

**SpdxAnnotation_Validate_InvalidAnnotator**: Verifies that validation reports an issue when
the Annotator field is missing or does not conform to the required format.
This scenario is tested by `SpdxAnnotation_Validate_InvalidAnnotator`.

**SpdxAnnotation_Validate_InvalidDate**: Verifies that validation reports an issue when the
annotation date is missing or does not conform to ISO 8601 date-time format.
This scenario is tested by `SpdxAnnotation_Validate_InvalidDate`.

**SpdxAnnotation_Validate_InvalidType**: Verifies that validation reports an issue when the
annotation type is set to an unrecognized or unsupported value.
This scenario is tested by `SpdxAnnotation_Validate_InvalidType`.

**SpdxAnnotation_Validate_InvalidComment**: Verifies that validation reports an issue when the
annotation comment is missing or empty.
This scenario is tested by `SpdxAnnotation_Validate_InvalidComment`.

**SpdxAnnotationTypeExtensions_FromText_Valid**: Verifies that FromText correctly parses a
recognized annotation type string to its corresponding enum value.
This scenario is tested by `SpdxAnnotationTypeExtensions_FromText_Valid`.

**SpdxAnnotationTypeExtensions_FromText_Invalid**: Verifies that `FromText` throws
`InvalidOperationException` with the message `"Unsupported SPDX Annotation Type 'invalid'"` when
given an unrecognized annotation type string.
This scenario is tested by `SpdxAnnotationTypeExtensions_FromText_Invalid`.

**SpdxAnnotationTypeExtensions_ToText_Valid**: Verifies that ToText correctly converts a
recognized annotation type enum value to its SPDX text representation.
This scenario is tested by `SpdxAnnotationTypeExtensions_ToText_Valid`.

**SpdxAnnotationTypeExtensions_ToText_Invalid**: Verifies that ToText always throws
`InvalidOperationException` when given an unknown or unsupported annotation type enum value.
The method never returns an empty string for an invalid input.
This scenario is tested by `SpdxAnnotationTypeExtensions_ToText_Invalid`.

**SpdxAnnotationTypeExtensions_ToText_Missing**: Verifies that calling
`SpdxAnnotationType.Missing.ToText()` throws `InvalidOperationException` with the message
"Attempt to serialize missing SPDX Annotation Type".
This scenario is tested by `SpdxAnnotationTypeExtensions_ToText_Missing`.
