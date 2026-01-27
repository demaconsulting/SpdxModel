# Summary

23 of 23 requirements are satisfied with tests.

# Requirements

## SpdxModel Library Requirements

### Functional Requirements

| ID | Tests Linked | Passed | Failed | Not Executed |
| :- | -----------: | :-: | :-: | :-: |
| REQ-FUNC-001 | 1 | 1 | 0 | 0 |
| REQ-FUNC-002 | 1 | 1 | 0 | 0 |
| REQ-FUNC-003 | 1 | 1 | 0 | 0 |
| REQ-FUNC-004 | 3 | 3 | 0 | 0 |
| REQ-FUNC-005 | 4 | 4 | 0 | 0 |
| REQ-FUNC-006 | 4 | 4 | 0 | 0 |
| REQ-FUNC-007 | 4 | 4 | 0 | 0 |
| REQ-FUNC-008 | 4 | 4 | 0 | 0 |
| REQ-FUNC-009 | 4 | 4 | 0 | 0 |
| REQ-FUNC-010 | 4 | 4 | 0 | 0 |
| REQ-FUNC-011 | 4 | 4 | 0 | 0 |
| REQ-FUNC-012 | 4 | 4 | 0 | 0 |
| REQ-FUNC-013 | 4 | 4 | 0 | 0 |
| REQ-FUNC-014 | 4 | 4 | 0 | 0 |
| REQ-FUNC-015 | 2 | 2 | 0 | 0 |
| REQ-FUNC-016 | 1 | 1 | 0 | 0 |
| REQ-FUNC-017 | 3 | 3 | 0 | 0 |
| REQ-FUNC-018 | 4 | 4 | 0 | 0 |

### Quality Requirements

| ID | Tests Linked | Passed | Failed | Not Executed |
| :- | -----------: | :-: | :-: | :-: |
| REQ-QUAL-001 | 2 | 2 | 0 | 0 |
| REQ-QUAL-002 | 2 | 2 | 0 | 0 |
| REQ-QUAL-003 | 2 | 2 | 0 | 0 |
| REQ-QUAL-004 | 2 | 2 | 0 | 0 |
| REQ-QUAL-005 | 4 | 4 | 0 | 0 |

# Testing

| Test | Requirement | Passed | Failed |
|------|-------------|--------|--------|
| net10.0@Spdx2JsonDeserializer_Deserialize_ValidSpdx22JsonReturnsExpectedDocument | REQ-QUAL-003 | 1 | 0 |
| net10.0@Spdx2JsonSerializer_SerializeDocument_CorrectResults | REQ-QUAL-003 | 1 | 0 |
| net8.0@Spdx2JsonDeserializer_Deserialize_ValidSpdx22JsonReturnsExpectedDocument | REQ-QUAL-001 | 1 | 0 |
| net8.0@Spdx2JsonSerializer_SerializeDocument_CorrectResults | REQ-QUAL-001 | 1 | 0 |
| net9.0@Spdx2JsonDeserializer_Deserialize_ValidSpdx22JsonReturnsExpectedDocument | REQ-QUAL-002 | 1 | 0 |
| net9.0@Spdx2JsonSerializer_SerializeDocument_CorrectResults | REQ-QUAL-002 | 1 | 0 |
| Spdx2JsonDeserializer_Deserialize_ValidSpdx22JsonReturnsExpectedDocument | REQ-FUNC-001 | 3 | 0 |
| Spdx2JsonDeserializer_Deserialize_ValidSpdx23JsonReturnsExpectedDocument | REQ-FUNC-002 | 3 | 0 |
| Spdx2JsonDeserializer_DeserializeAnnotation_CorrectResults | REQ-FUNC-009 | 3 | 0 |
| Spdx2JsonDeserializer_DeserializeChecksum_CorrectResults | REQ-FUNC-010 | 3 | 0 |
| Spdx2JsonDeserializer_DeserializeCreationInformation_CorrectResults | REQ-FUNC-004 | 3 | 0 |
| Spdx2JsonDeserializer_DeserializeExternalDocumentReference_CorrectResults | REQ-FUNC-012 | 3 | 0 |
| Spdx2JsonDeserializer_DeserializeExternalReference_CorrectResults | REQ-FUNC-011 | 3 | 0 |
| Spdx2JsonDeserializer_DeserializeExtractedLicensingInfo_CorrectResults | REQ-FUNC-013 | 3 | 0 |
| Spdx2JsonDeserializer_DeserializeFile_CorrectResults | REQ-FUNC-006 | 3 | 0 |
| Spdx2JsonDeserializer_DeserializePackage_CorrectResults | REQ-FUNC-005 | 3 | 0 |
| Spdx2JsonDeserializer_DeserializePackageVerificationCode_CorrectResults | REQ-FUNC-014 | 3 | 0 |
| Spdx2JsonDeserializer_DeserializeRelationship_CorrectResults | REQ-FUNC-008 | 3 | 0 |
| Spdx2JsonDeserializer_DeserializeSnippet_CorrectResults | REQ-FUNC-007 | 3 | 0 |
| Spdx2JsonSerializer_SerializeAnnotation_CorrectResults | REQ-FUNC-009 | 3 | 0 |
| Spdx2JsonSerializer_SerializeChecksum_CorrectResults | REQ-FUNC-010 | 3 | 0 |
| Spdx2JsonSerializer_SerializeCreationInformation_CorrectResults | REQ-FUNC-004 | 3 | 0 |
| Spdx2JsonSerializer_SerializeDocument_CorrectResults | REQ-FUNC-003 | 3 | 0 |
| Spdx2JsonSerializer_SerializeExternalDocumentReference_CorrectResults | REQ-FUNC-012 | 3 | 0 |
| Spdx2JsonSerializer_SerializeExternalReference_CorrectResults | REQ-FUNC-011 | 3 | 0 |
| Spdx2JsonSerializer_SerializeExtractedLicensingInfo_CorrectResults | REQ-FUNC-013 | 3 | 0 |
| Spdx2JsonSerializer_SerializeFile_CorrectResults | REQ-FUNC-006 | 3 | 0 |
| Spdx2JsonSerializer_SerializePackage_CorrectResults | REQ-FUNC-005 | 3 | 0 |
| Spdx2JsonSerializer_SerializePackageVerificationCode_CorrectResults | REQ-FUNC-014 | 3 | 0 |
| Spdx2JsonSerializer_SerializeRelationship_CorrectResults | REQ-FUNC-008 | 3 | 0 |
| Spdx2JsonSerializer_SerializeSnippet_CorrectResults | REQ-FUNC-007 | 3 | 0 |
| SpdxAnnotation_DeepCopy_CreatesEqualButDistinctInstance | REQ-FUNC-009 | 3 | 0 |
| SpdxAnnotation_SameComparer_ComparesCorrectly | REQ-FUNC-009 | 3 | 0 |
| SpdxAnnotation_Validate_BadAnnotator | REQ-FUNC-018 | 3 | 0 |
| SpdxChecksum_DeepCopy_CreatesEqualButDistinctInstance | REQ-FUNC-010 | 3 | 0 |
| SpdxChecksum_SameComparer_ComparesCorrectly | REQ-FUNC-010 | 3 | 0 |
| SpdxChecksum_Validate_BadAlgorithm | REQ-FUNC-018 | 3 | 0 |
| SpdxCreationInformation_DeepCopy_CreatesEqualButDistinctInstance | REQ-FUNC-004 | 3 | 0 |
| SpdxCreationInformation_Validate_MissingCreators | REQ-FUNC-018 | 3 | 0 |
| SpdxDocument_DeepCopy_CreatesEqualButDistinctInstance | REQ-FUNC-017 | 3 | 0 |
| SpdxDocument_DeepCopy_CreatesEqualButDistinctInstance | REQ-QUAL-004 | 3 | 0 |
| SpdxDocument_GetRootPackages_CorrectPackages | REQ-FUNC-016 | 3 | 0 |
| SpdxDocument_SameComparer_ComparesCorrectly | REQ-QUAL-005 | 3 | 0 |
| SpdxDocument_Validate_NoIssues | REQ-FUNC-018 | 3 | 0 |
| SpdxExternalDocumentReference_DeepCopy_CreatesEqualButDistinctInstance | REQ-FUNC-012 | 3 | 0 |
| SpdxExternalDocumentReference_SameComparer_ComparesCorrectly | REQ-FUNC-012 | 3 | 0 |
| SpdxExternalReference_DeepCopy_CreatesEqualButDistinctInstance | REQ-FUNC-011 | 3 | 0 |
| SpdxExternalReference_SameComparer_ComparesCorrectly | REQ-FUNC-011 | 3 | 0 |
| SpdxExtractedLicensingInfo_DeepCopy_CreatesEqualButDistinctInstance | REQ-FUNC-013 | 3 | 0 |
| SpdxExtractedLicensingInfo_SameComparer_ComparesCorrectly | REQ-FUNC-013 | 3 | 0 |
| SpdxFile_DeepCopy_CreatesEqualButDistinctInstance | REQ-FUNC-006 | 3 | 0 |
| SpdxFile_DeepCopy_CreatesEqualButDistinctInstance | REQ-FUNC-017 | 3 | 0 |
| SpdxFile_SameComparer_ComparesCorrectly | REQ-FUNC-006 | 3 | 0 |
| SpdxFile_SameComparer_ComparesCorrectly | REQ-QUAL-005 | 3 | 0 |
| SpdxPackage_DeepCopy_CreatesEqualButDistinctInstance | REQ-FUNC-005 | 3 | 0 |
| SpdxPackage_DeepCopy_CreatesEqualButDistinctInstance | REQ-FUNC-017 | 3 | 0 |
| SpdxPackage_DeepCopy_CreatesEqualButDistinctInstance | REQ-QUAL-004 | 3 | 0 |
| SpdxPackage_SameComparer_ComparesCorrectly | REQ-FUNC-005 | 3 | 0 |
| SpdxPackage_SameComparer_ComparesCorrectly | REQ-QUAL-005 | 3 | 0 |
| SpdxPackageVerificationCode_DeepCopy_CreatesEqualButDistinctInstance | REQ-FUNC-014 | 3 | 0 |
| SpdxPackageVerificationCode_SameComparer_ComparesCorrectly | REQ-FUNC-014 | 3 | 0 |
| SpdxRelationship_DeepCopy_CreatesEqualButDistinctInstance | REQ-FUNC-008 | 3 | 0 |
| SpdxRelationship_SameComparer_ComparesCorrectly | REQ-FUNC-008 | 3 | 0 |
| SpdxRelationship_SameComparer_ComparesCorrectly | REQ-QUAL-005 | 3 | 0 |
| SpdxRelationships_AddMultiple_Success | REQ-FUNC-015 | 3 | 0 |
| SpdxRelationships_AddSingle_Success | REQ-FUNC-015 | 3 | 0 |
| SpdxSnippet_DeepCopy_CreatesEqualButDistinctInstance | REQ-FUNC-007 | 3 | 0 |
| SpdxSnippet_SameComparer_ComparesCorrectly | REQ-FUNC-007 | 3 | 0 |

