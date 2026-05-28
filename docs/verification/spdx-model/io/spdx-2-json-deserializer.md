### Spdx2JsonDeserializer

#### Verification Approach

Spdx2JsonDeserializer is verified through automated unit tests using the xUnit v3 framework.
Tests are located in the IO subdirectory under
`test/DemaConsulting.SpdxModel.Tests/`. Each test provides representative JSON input and
verifies the deserialized SPDX model objects match the expected values. System.Text.Json
is not mocked as JSON parsing is part of the verification scope.

#### Test Environment

N/A - standard test environment.

#### Acceptance Criteria

All automated tests pass with zero failures.

#### Test Scenarios

**Spdx2JsonDeserializer_Deserialize_ValidSpdx22Json_ReturnsExpectedDocument**: Verifies that
a complete SPDX 2.2 JSON document is deserialized to a fully populated SpdxDocument with
all fields correctly mapped.
This scenario is tested by
`Spdx2JsonDeserializer_Deserialize_ValidSpdx22Json_ReturnsExpectedDocument`.

**Spdx2JsonDeserializer_Deserialize_ValidSpdx23Json_ReturnsExpectedDocument**: Verifies that
a complete SPDX 2.3 JSON document is deserialized to a fully populated SpdxDocument with
all fields correctly mapped.
This scenario is tested by
`Spdx2JsonDeserializer_Deserialize_ValidSpdx23Json_ReturnsExpectedDocument`.

**Spdx2JsonDeserializer_DeserializeAnnotation_ValidInput_CorrectResults**: Verifies that a single
annotation JSON object is deserialized to an SpdxAnnotation with all fields correctly
populated.
This scenario is tested by `Spdx2JsonDeserializer_DeserializeAnnotation_ValidInput_CorrectResults`.

**Spdx2JsonDeserializer_DeserializeAnnotations_ValidInput_CorrectResults**: Verifies that a JSON array
of annotation objects is deserialized to a collection of SpdxAnnotation instances.
This scenario is tested by `Spdx2JsonDeserializer_DeserializeAnnotations_ValidInput_CorrectResults`.

**Spdx2JsonDeserializer_DeserializeChecksum_ValidInput_CorrectResults**: Verifies that a single checksum
JSON object is deserialized to an SpdxChecksum with algorithm and value fields correctly
populated.
This scenario is tested by `Spdx2JsonDeserializer_DeserializeChecksum_ValidInput_CorrectResults`.

**Spdx2JsonDeserializer_DeserializeChecksums_ValidInput_CorrectResults**: Verifies that a JSON array of
checksum objects is deserialized to a collection of SpdxChecksum instances.
This scenario is tested by `Spdx2JsonDeserializer_DeserializeChecksums_ValidInput_CorrectResults`.

**Spdx2JsonDeserializer_DeserializeCreationInformation_ValidInput_CorrectResults**: Verifies that the
creationInfo JSON object is deserialized to an SpdxCreationInformation with all fields
correctly populated.
This scenario is tested by
`Spdx2JsonDeserializer_DeserializeCreationInformation_ValidInput_CorrectResults`.

**Spdx2JsonDeserializer_DeserializeDocument_ValidInput_CorrectResults**: Verifies that the top-level
document JSON fields are deserialized to the SpdxDocument with all document-level fields
correctly populated.
This scenario is tested by `Spdx2JsonDeserializer_DeserializeDocument_ValidInput_CorrectResults`.

**Spdx2JsonDeserializer_DeserializeExternalDocumentReference_ValidInput_CorrectResults**: Verifies that
a single external document reference JSON object is deserialized to an
SpdxExternalDocumentReference with all fields correctly populated.
This scenario is tested by
`Spdx2JsonDeserializer_DeserializeExternalDocumentReference_ValidInput_CorrectResults`.

**Spdx2JsonDeserializer_DeserializeExternalDocumentReferences_ValidInput_CorrectResults**: Verifies that
a JSON array of external document reference objects is deserialized to a collection of
SpdxExternalDocumentReference instances.
This scenario is tested by
`Spdx2JsonDeserializer_DeserializeExternalDocumentReferences_ValidInput_CorrectResults`.

**Spdx2JsonDeserializer_DeserializeExternalReference_ValidInput_CorrectResults**: Verifies that a single
external reference JSON object is deserialized to an SpdxExternalReference with category,
type, and locator fields correctly populated.
This scenario is tested by
`Spdx2JsonDeserializer_DeserializeExternalReference_ValidInput_CorrectResults`.

**Spdx2JsonDeserializer_DeserializeExternalReferences_ValidInput_CorrectResults**: Verifies that a JSON
array of external reference objects is deserialized to a collection of SpdxExternalReference
instances.
This scenario is tested by
`Spdx2JsonDeserializer_DeserializeExternalReferences_ValidInput_CorrectResults`.

**Spdx2JsonDeserializer_DeserializeExtractedLicensingInfo_ValidInput_CorrectResults**: Verifies that a
single extracted licensing info JSON object is deserialized to an SpdxExtractedLicensingInfo
with all fields correctly populated.
This scenario is tested by
`Spdx2JsonDeserializer_DeserializeExtractedLicensingInfo_ValidInput_CorrectResults`.

**Spdx2JsonDeserializer_DeserializeExtractedLicensingInfos_ValidInput_CorrectResults**: Verifies that a
JSON array of extracted licensing info objects is deserialized to a collection of
SpdxExtractedLicensingInfo instances.
This scenario is tested by
`Spdx2JsonDeserializer_DeserializeExtractedLicensingInfos_ValidInput_CorrectResults`.

**Spdx2JsonDeserializer_DeserializeFile_ValidInput_CorrectResults**: Verifies that a single file JSON
object is deserialized to an SpdxFile with all fields correctly populated.
This scenario is tested by `Spdx2JsonDeserializer_DeserializeFile_ValidInput_CorrectResults`.

**Spdx2JsonDeserializer_DeserializeFiles_ValidInput_CorrectResults**: Verifies that a JSON array of file
objects is deserialized to a collection of SpdxFile instances.
This scenario is tested by `Spdx2JsonDeserializer_DeserializeFiles_ValidInput_CorrectResults`.

**Spdx2JsonDeserializer_DeserializePackage_ValidInput_CorrectResults**: Verifies that a single package
JSON object is deserialized to an SpdxPackage with all fields correctly populated.
This scenario is tested by `Spdx2JsonDeserializer_DeserializePackage_ValidInput_CorrectResults`.

**Spdx2JsonDeserializer_DeserializePackages_ValidInput_CorrectResults**: Verifies that a JSON array of
package objects is deserialized to a collection of SpdxPackage instances.
This scenario is tested by `Spdx2JsonDeserializer_DeserializePackages_ValidInput_CorrectResults`.

**Spdx2JsonDeserializer_DeserializePackageVerificationCode_ValidInput_CorrectResults**: Verifies that a
package verification code JSON object is deserialized to an SpdxPackageVerificationCode with
all fields correctly populated.
This scenario is tested by
`Spdx2JsonDeserializer_DeserializePackageVerificationCode_ValidInput_CorrectResults`.

**Spdx2JsonDeserializer_DeserializeRelationship_ValidInput_CorrectResults**: Verifies that a single
relationship JSON object is deserialized to an SpdxRelationship with all fields correctly
populated.
This scenario is tested by `Spdx2JsonDeserializer_DeserializeRelationship_ValidInput_CorrectResults`.

**Spdx2JsonDeserializer_DeserializeRelationships_ValidInput_CorrectResults**: Verifies that a JSON array
of relationship objects is deserialized to a collection of SpdxRelationship instances.
This scenario is tested by `Spdx2JsonDeserializer_DeserializeRelationships_ValidInput_CorrectResults`.

**Spdx2JsonDeserializer_DeserializeSnippet_ValidInput_CorrectResults**: Verifies that a single snippet
JSON object is deserialized to an SpdxSnippet with byte ranges and line ranges correctly
populated.
This scenario is tested by `Spdx2JsonDeserializer_DeserializeSnippet_ValidInput_CorrectResults`.

**Spdx2JsonDeserializer_DeserializeSnippet_WithoutLineRanges_DefaultsToZero**: Verifies that
when a snippet JSON object omits line range fields, the deserialized SpdxSnippet defaults
those fields to zero.
This scenario is tested by
`Spdx2JsonDeserializer_DeserializeSnippet_WithoutLineRanges_DefaultsToZero`.

**Spdx2JsonDeserializer_DeserializeSnippets_ValidInput_CorrectResults**: Verifies that a JSON array of
snippet objects is deserialized to a collection of SpdxSnippet instances.
This scenario is tested by `Spdx2JsonDeserializer_DeserializeSnippets_ValidInput_CorrectResults`.
