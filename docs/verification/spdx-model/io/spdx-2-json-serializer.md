### Spdx2JsonSerializer

#### Verification Approach

Spdx2JsonSerializer is verified through automated unit tests using the MSTest framework.
Tests are located in the IO subdirectory under
`test/DemaConsulting.SpdxModel.Tests/`. Each test constructs the relevant SPDX model
objects directly and verifies the serialized JSON output. System.Text.Json is not mocked
as JSON output is part of the verification scope.

#### Test Environment

N/A - standard test environment.

#### Acceptance Criteria

All automated tests pass with zero failures.

#### Test Scenarios

**Spdx2JsonSerializer_SerializeAnnotation_CorrectResults**: Verifies that a single
SpdxAnnotation is serialized to the expected JSON structure with all fields correctly mapped.
This scenario is tested by `Spdx2JsonSerializer_SerializeAnnotation_CorrectResults`.

**Spdx2JsonSerializer_SerializeAnnotations_CorrectResults**: Verifies that a collection of
SpdxAnnotation instances is serialized to the expected JSON array structure.
This scenario is tested by `Spdx2JsonSerializer_SerializeAnnotations_CorrectResults`.

**Spdx2JsonSerializer_SerializeChecksum_CorrectResults**: Verifies that a single SpdxChecksum
is serialized to the expected JSON structure with algorithm and value fields correctly mapped.
This scenario is tested by `Spdx2JsonSerializer_SerializeChecksum_CorrectResults`.

**Spdx2JsonSerializer_SerializeChecksums_CorrectResults**: Verifies that a collection of
SpdxChecksum instances is serialized to the expected JSON array structure.
This scenario is tested by `Spdx2JsonSerializer_SerializeChecksums_CorrectResults`.

**Spdx2JsonSerializer_SerializeCreationInformation_CorrectResults**: Verifies that
SpdxCreationInformation is serialized to the expected JSON structure with all creation fields
correctly mapped.
This scenario is tested by
`Spdx2JsonSerializer_SerializeCreationInformation_CorrectResults`.

**Spdx2JsonSerializer_SerializeDocument_CorrectResults**: Verifies that the top-level
SpdxDocument structure (excluding nested collections) is serialized to the expected JSON
with all document-level fields correctly mapped.
This scenario is tested by `Spdx2JsonSerializer_SerializeDocument_CorrectResults`.

**Spdx2JsonSerializer_Serialize_CorrectResults**: Verifies that a complete SpdxDocument
including all nested packages, files, snippets, and relationships is serialized to the
expected JSON output.
This scenario is tested by `Spdx2JsonSerializer_Serialize_CorrectResults`.

**Spdx2JsonSerializer_SerializeExternalDocumentReference_CorrectResults**: Verifies that a
single SpdxExternalDocumentReference is serialized to the expected JSON structure.
This scenario is tested by
`Spdx2JsonSerializer_SerializeExternalDocumentReference_CorrectResults`.

**Spdx2JsonSerializer_SerializeExternalDocumentReferences_CorrectResults**: Verifies that a
collection of SpdxExternalDocumentReference instances is serialized to the expected JSON
array.
This scenario is tested by
`Spdx2JsonSerializer_SerializeExternalDocumentReferences_CorrectResults`.

**Spdx2JsonSerializer_SerializeExternalReference_CorrectResults**: Verifies that a single
SpdxExternalReference is serialized to the expected JSON structure with category, type, and
locator fields correctly mapped.
This scenario is tested by `Spdx2JsonSerializer_SerializeExternalReference_CorrectResults`.

**Spdx2JsonSerializer_SerializeExternalReferences_CorrectResults**: Verifies that a
collection of SpdxExternalReference instances is serialized to the expected JSON array.
This scenario is tested by `Spdx2JsonSerializer_SerializeExternalReferences_CorrectResults`.

**Spdx2JsonSerializer_SerializeExtractedLicensingInfo_CorrectResults**: Verifies that a
single SpdxExtractedLicensingInfo is serialized to the expected JSON structure.
This scenario is tested by
`Spdx2JsonSerializer_SerializeExtractedLicensingInfo_CorrectResults`.

**Spdx2JsonSerializer_SerializeExtractedLicensingInfos_CorrectResults**: Verifies that a
collection of SpdxExtractedLicensingInfo instances is serialized to the expected JSON array.
This scenario is tested by
`Spdx2JsonSerializer_SerializeExtractedLicensingInfos_CorrectResults`.

**Spdx2JsonSerializer_SerializeFile_CorrectResults**: Verifies that a single SpdxFile is
serialized to the expected JSON structure with all file fields correctly mapped.
This scenario is tested by `Spdx2JsonSerializer_SerializeFile_CorrectResults`.

**Spdx2JsonSerializer_SerializeFiles_CorrectResults**: Verifies that a collection of SpdxFile
instances is serialized to the expected JSON array.
This scenario is tested by `Spdx2JsonSerializer_SerializeFiles_CorrectResults`.

**Spdx2JsonSerializer_SerializePackage_CorrectResults**: Verifies that a single SpdxPackage
is serialized to the expected JSON structure with all package fields correctly mapped.
This scenario is tested by `Spdx2JsonSerializer_SerializePackage_CorrectResults`.

**Spdx2JsonSerializer_SerializePackages_CorrectResults**: Verifies that a collection of
SpdxPackage instances is serialized to the expected JSON array.
This scenario is tested by `Spdx2JsonSerializer_SerializePackages_CorrectResults`.

**Spdx2JsonSerializer_SerializePackageVerificationCode_CorrectResults**: Verifies that an
SpdxPackageVerificationCode is serialized to the expected JSON structure.
This scenario is tested by
`Spdx2JsonSerializer_SerializePackageVerificationCode_CorrectResults`.

**Spdx2JsonSerializer_SerializeRelationship_CorrectResults**: Verifies that a single
SpdxRelationship is serialized to the expected JSON structure with all relationship fields
correctly mapped.
This scenario is tested by `Spdx2JsonSerializer_SerializeRelationship_CorrectResults`.

**Spdx2JsonSerializer_SerializeRelationships_CorrectResults**: Verifies that a collection of
SpdxRelationship instances is serialized to the expected JSON array.
This scenario is tested by `Spdx2JsonSerializer_SerializeRelationships_CorrectResults`.

**Spdx2JsonSerializer_SerializeSnippet_CorrectResults**: Verifies that a single SpdxSnippet
is serialized to the expected JSON structure with byte ranges and line ranges correctly
mapped.
This scenario is tested by `Spdx2JsonSerializer_SerializeSnippet_CorrectResults`.

**Spdx2JsonSerializer_SerializeSnippets_CorrectResults**: Verifies that a collection of
SpdxSnippet instances is serialized to the expected JSON array.
This scenario is tested by `Spdx2JsonSerializer_SerializeSnippets_CorrectResults`.
