## SpdxModel

### Verification Approach

The SpdxModel library is verified through automated unit and integration tests using the MSTest
framework. Tests are organized in `test/DemaConsulting.SpdxModel.Tests/`. Unit tests verify
individual data model classes in isolation; integration tests verify the IO subsystem end-to-end
and the Transform subsystem with real document instances. No external dependencies are mocked.

### Test Environment

N/A - standard test environment.

### Acceptance Criteria

All automated tests pass with zero failures.

### Test Scenarios

**SpdxModel_ReadSpdxJson_Spdx22Example_ParsesSuccessfully**: Verifies that the library
successfully reads and parses an SPDX 2.2 example JSON document, returning a non-null
SpdxDocument without throwing exceptions.

**SpdxModel_ReadSpdxJson_Spdx23Example_ParsesSuccessfully**: Verifies that the library
successfully reads and parses an SPDX 2.3 example JSON document, returning a non-null
SpdxDocument without throwing exceptions.

**SpdxModel_ReadSpdxJson_Spdx22Example_PassesValidation**: Verifies that the document parsed
from the SPDX 2.2 example JSON passes all validation checks with no reported issues.

**SpdxModel_ReadSpdxJson_Spdx23Example_PassesValidation**: Verifies that the document parsed
from the SPDX 2.3 example JSON passes all validation checks with no reported issues.

**SpdxModel_ReadSpdxJson_Spdx23Example_RootPackagesIdentified**: Verifies that the root
packages are correctly identified in the SPDX 2.3 example document after parsing, confirming
DESCRIBES relationship traversal works as expected.

**SpdxModel_ReadSpdxJson_Spdx23Example_DeepCopyProducesEquivalentDocument**: Verifies that a
deep copy of the parsed SPDX 2.3 document is structurally equal to the original, confirming
that all nested objects are fully duplicated.

**SpdxModel_WriteReadSpdxJson_Spdx23Example_RoundTripSucceeds**: Verifies that an SPDX 2.3
document serialized to JSON and then deserialized produces a document equivalent to the
original, confirming end-to-end serialization fidelity.

**SpdxModel_Deserialize_MalformedJson_ThrowsJsonException**: Verifies that passing malformed
JSON to `Spdx2JsonDeserializer.Deserialize` throws a `JsonException` rather than returning a
partially-populated document.

**SpdxModel_Validate_InvalidDocument_ReportsIssues**: Verifies that calling `Validate()` on a
deliberately incomplete SPDX document produces a non-empty issues list with the expected
validation error messages.

**SpdxModel_FieldOptionality_RequiredFieldsNotNull_OptionalFieldsNullable**: Verifies that
required fields on key SPDX data model types are non-nullable string types with default empty
values, and that optional fields are nullable.

**SpdxModel_Helpers_DateTimeValidation_IsObservableThroughDocumentModel**: Verifies that the
date-time validation utility (`SpdxHelpers.IsValidSpdxDateTime`) is exercised through the
document model by confirming that an invalid creation date is caught by document-level
validation. Linked from `SpdxModel-Data-Helpers`.

**SpdxModel_Transform_AddRelationship_IsObservableThroughDocumentModel**: Verifies that the
`AddRelationship` transform utility correctly adds a new relationship to an SPDX document and
that the addition is observable through the document model's relationship collection. Linked
from `SpdxModel-Transform`.
