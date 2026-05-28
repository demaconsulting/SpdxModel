### Spdx2JsonSerializer

#### Verification Approach

Spdx2JsonSerializer is verified through automated unit tests using the xUnit v3 framework.
Tests are located in the IO subdirectory under
`test/DemaConsulting.SpdxModel.Tests/`. Each test constructs the relevant SPDX model
objects directly and verifies the serialized JSON output. System.Text.Json is not mocked
as JSON output is part of the verification scope.

#### Test Environment

N/A - standard test environment.

#### Acceptance Criteria

All automated tests pass with zero failures.

#### Test Scenarios

**Spdx2JsonSerializer_SerializeAnnotation_ValidInput_CorrectResults**: Verifies that a single
SpdxAnnotation is serialized to the expected JSON structure with all fields correctly mapped.
This scenario is tested by `Spdx2JsonSerializer_SerializeAnnotation_ValidInput_CorrectResults`.

**Spdx2JsonSerializer_SerializeAnnotations_ValidInput_CorrectResults**: Verifies that a collection of
SpdxAnnotation instances is serialized to the expected JSON array structure.
This scenario is tested by `Spdx2JsonSerializer_SerializeAnnotations_ValidInput_CorrectResults`.

**Spdx2JsonSerializer_SerializeAnnotation_NoId_OmitsSpdxId**: Verifies that when an
`SpdxAnnotation` has an empty `Id` string, the serializer omits the `SPDXID` field from the
JSON output while still emitting all other annotation fields (annotator, annotationDate,
annotationType, comment).
This scenario is tested by `Spdx2JsonSerializer_SerializeAnnotation_NoId_OmitsSpdxId`.

**Spdx2JsonSerializer_SerializeChecksum_ValidInput_CorrectResults**: Verifies that a single SpdxChecksum
is serialized to the expected JSON structure with algorithm and value fields correctly mapped.
This scenario is tested by `Spdx2JsonSerializer_SerializeChecksum_ValidInput_CorrectResults`.

**Spdx2JsonSerializer_SerializeChecksums_ValidInput_CorrectResults**: Verifies that a collection of
SpdxChecksum instances is serialized to the expected JSON array structure.
This scenario is tested by `Spdx2JsonSerializer_SerializeChecksums_ValidInput_CorrectResults`.

**Spdx2JsonSerializer_SerializeCreationInformation_ValidInput_CorrectResults**: Verifies that
SpdxCreationInformation is serialized to the expected JSON structure with all creation fields
correctly mapped.
This scenario is tested by
`Spdx2JsonSerializer_SerializeCreationInformation_ValidInput_CorrectResults`.

**Spdx2JsonSerializer_SerializeDocument_ValidInput_CorrectResults**: Verifies that the top-level
SpdxDocument structure (excluding nested collections) is serialized to the expected JSON
with all document-level fields correctly mapped.
This scenario is tested by `Spdx2JsonSerializer_SerializeDocument_ValidInput_CorrectResults`.

**Spdx2JsonSerializer_Serialize_ValidInput_CorrectResults**: Verifies that a complete SpdxDocument
including all nested packages, files, snippets, and relationships is serialized to the
expected JSON output.
This scenario is tested by `Spdx2JsonSerializer_Serialize_ValidInput_CorrectResults`.

**Spdx2JsonSerializer_SerializeExternalDocumentReference_ValidInput_CorrectResults**: Verifies that a
single SpdxExternalDocumentReference is serialized to the expected JSON structure.
This scenario is tested by
`Spdx2JsonSerializer_SerializeExternalDocumentReference_ValidInput_CorrectResults`.

**Spdx2JsonSerializer_SerializeExternalDocumentReferences_ValidInput_CorrectResults**: Verifies that a
collection of SpdxExternalDocumentReference instances is serialized to the expected JSON
array.
This scenario is tested by
`Spdx2JsonSerializer_SerializeExternalDocumentReferences_ValidInput_CorrectResults`.

**Spdx2JsonSerializer_SerializeExternalReference_ValidInput_CorrectResults**: Verifies that a single
SpdxExternalReference is serialized to the expected JSON structure with category, type, and
locator fields correctly mapped.
This scenario is tested by `Spdx2JsonSerializer_SerializeExternalReference_ValidInput_CorrectResults`.

**Spdx2JsonSerializer_SerializeExternalReferences_ValidInput_CorrectResults**: Verifies that a
collection of SpdxExternalReference instances is serialized to the expected JSON array.
This scenario is tested by `Spdx2JsonSerializer_SerializeExternalReferences_ValidInput_CorrectResults`.

**Spdx2JsonSerializer_SerializeExtractedLicensingInfo_ValidInput_CorrectResults**: Verifies that a
single SpdxExtractedLicensingInfo is serialized to the expected JSON structure.
This scenario is tested by
`Spdx2JsonSerializer_SerializeExtractedLicensingInfo_ValidInput_CorrectResults`.

**Spdx2JsonSerializer_SerializeExtractedLicensingInfos_ValidInput_CorrectResults**: Verifies that a
collection of SpdxExtractedLicensingInfo instances is serialized to the expected JSON array.
This scenario is tested by
`Spdx2JsonSerializer_SerializeExtractedLicensingInfos_ValidInput_CorrectResults`.

**Spdx2JsonSerializer_SerializeFile_ValidInput_CorrectResults**: Verifies that a single SpdxFile is
serialized to the expected JSON structure with all file fields correctly mapped.
This scenario is tested by `Spdx2JsonSerializer_SerializeFile_ValidInput_CorrectResults`.

**Spdx2JsonSerializer_SerializeFiles_ValidInput_CorrectResults**: Verifies that a collection of SpdxFile
instances is serialized to the expected JSON array.
This scenario is tested by `Spdx2JsonSerializer_SerializeFiles_ValidInput_CorrectResults`.

**Spdx2JsonSerializer_SerializePackage_ValidInput_CorrectResults**: Verifies that a single SpdxPackage
is serialized to the expected JSON structure with all package fields correctly mapped.
This scenario is tested by `Spdx2JsonSerializer_SerializePackage_ValidInput_CorrectResults`.

**Spdx2JsonSerializer_SerializePackages_ValidInput_CorrectResults**: Verifies that a collection of
SpdxPackage instances is serialized to the expected JSON array.
This scenario is tested by `Spdx2JsonSerializer_SerializePackages_ValidInput_CorrectResults`.

**Spdx2JsonSerializer_SerializeVerificationCode_ValidInput_CorrectResults**: Verifies that an
SpdxPackageVerificationCode is serialized to the expected JSON structure.
This scenario is tested by
`Spdx2JsonSerializer_SerializeVerificationCode_ValidInput_CorrectResults`.

**Spdx2JsonSerializer_SerializeRelationship_ValidInput_CorrectResults**: Verifies that a single
SpdxRelationship is serialized to the expected JSON structure with all relationship fields
correctly mapped.
This scenario is tested by `Spdx2JsonSerializer_SerializeRelationship_ValidInput_CorrectResults`.

**Spdx2JsonSerializer_SerializeRelationships_ValidInput_CorrectResults**: Verifies that a collection of
SpdxRelationship instances is serialized to the expected JSON array.
This scenario is tested by `Spdx2JsonSerializer_SerializeRelationships_ValidInput_CorrectResults`.

**Spdx2JsonSerializer_SerializeSnippet_ValidInput_CorrectResults**: Verifies that a single SpdxSnippet
is serialized to the expected JSON structure with byte ranges and line ranges correctly
mapped.
This scenario is tested by `Spdx2JsonSerializer_SerializeSnippet_ValidInput_CorrectResults`.

**Spdx2JsonSerializer_SerializeSnippets_ValidInput_CorrectResults**: Verifies that a collection of
SpdxSnippet instances is serialized to the expected JSON array.
This scenario is tested by `Spdx2JsonSerializer_SerializeSnippets_ValidInput_CorrectResults`.

**Spdx2JsonSerializer_SerializeSnippet_WithAnnotation_IncludesAnnotation**: Verifies that
when an `SpdxSnippet` includes an annotations array, the serialized JSON output contains
the `annotations` array with all annotation fields correctly mapped.
This scenario is tested by `Spdx2JsonSerializer_SerializeSnippet_WithAnnotation_IncludesAnnotation`.

**Spdx2JsonSerializer_SerializeSnippet_NoLineRange_EmitsByteRangeOnly**: Verifies that when
a snippet has both `SnippetLineStart` and `SnippetLineEnd` set to zero, the serialized
`ranges` array contains only the byte-range entry and no line-range entry is emitted.
This scenario is tested by `Spdx2JsonSerializer_SerializeSnippet_NoLineRange_EmitsByteRangeOnly`.

**Spdx2JsonSerializer_SerializeSnippet_PartialLineRange_EmitsByteRangeOnly**: Verifies that
when a snippet has only one of `SnippetLineStart` or `SnippetLineEnd` set to a non-zero
value (AND logic), the serialized `ranges` array contains only the byte-range entry — a
partial line range is not emitted.
This scenario is tested by `Spdx2JsonSerializer_SerializeSnippet_PartialLineRange_EmitsByteRangeOnly`.
