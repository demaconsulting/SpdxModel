## SpdxDocument

### Verification Approach

SpdxDocument is verified through automated unit tests using the xUnit v3 framework. Tests are
located in `test/DemaConsulting.SpdxModel.Tests/SpdxDocumentTests.cs`. Each test constructs
an SpdxDocument instance directly and exercises the method under test with no mocked
dependencies.

### Test Environment

N/A - standard test environment.

### Acceptance Criteria

All automated tests pass with zero failures.

### Test Scenarios

**SpdxDocument_GetRootPackages_WithDescribesAndRelationships_ReturnsCorrectPackages**: Verifies that
GetRootPackages returns only the packages that are the targets of DESCRIBES relationships from the
document element.
This scenario is tested by `SpdxDocument_GetRootPackages_WithDescribesAndRelationships_ReturnsCorrectPackages`.

**SpdxDocument_Same_DocumentsWithMatchingRootPackages_AreEqual**: Verifies that SameComparer correctly
identifies two SpdxDocument instances as equal when all fields match and as distinct when any
field differs.
This scenario is tested by `SpdxDocument_Same_DocumentsWithMatchingRootPackages_AreEqual`.

**SpdxDocument_DeepCopy_WithPopulatedDocument_CreatesEqualButDistinctInstance**: Verifies that a deep copy produces
a new SpdxDocument instance with equal field values but a distinct object reference, including
all nested packages, files, snippets, and relationships.
This scenario is tested by `SpdxDocument_DeepCopy_WithPopulatedDocument_CreatesEqualButDistinctInstance`.

**SpdxDocument_Validate_ValidDocument_ReportsNoIssues**: Verifies that a fully populated valid SpdxDocument passes
all validation checks without reporting any issues.
This scenario is tested by `SpdxDocument_Validate_ValidDocument_ReportsNoIssues`.

**SpdxDocument_Validate_InvalidId_ReportsIssue**: Verifies that validation reports an issue when the
document SPDX-ID field is missing or does not conform to the required format.
This scenario is tested by `SpdxDocument_Validate_InvalidId_ReportsIssue`.

**SpdxDocument_Validate_InvalidName_ReportsIssue**: Verifies that validation reports an issue when the
document name field is missing or empty.
This scenario is tested by `SpdxDocument_Validate_InvalidName_ReportsIssue`.

**SpdxDocument_Validate_InvalidVersion_ReportsIssue**: Verifies that validation reports an issue when the
SPDX version field is missing or does not match the expected SPDX-2.x format.
This scenario is tested by `SpdxDocument_Validate_InvalidVersion_ReportsIssue`.

**SpdxDocument_Validate_InvalidDataLicense_ReportsIssue**: Verifies that validation reports an issue when
the data license field is missing or is not set to the required CC0-1.0 value.
This scenario is tested by `SpdxDocument_Validate_InvalidDataLicense_ReportsIssue`.

**SpdxDocument_Validate_InvalidNameSpace_ReportsIssue**: Verifies that validation reports an issue when the
document namespace field is missing or is not a valid URI.
This scenario is tested by `SpdxDocument_Validate_InvalidNameSpace_ReportsIssue`.

**SpdxDocument_Validate_DuplicatePackageIds_ReportsIssue**: Verifies that validation reports an issue when
two or more packages in the document share the same SPDX-ID.
This scenario is tested by `SpdxDocument_Validate_DuplicatePackageIds_ReportsIssue`.

**SpdxDocument_Validate_InvalidRelationship_ReportsIssue**: Verifies that validation reports an issue when
a relationship references an element ID that does not exist within the document.
This scenario is tested by `SpdxDocument_Validate_InvalidRelationship_ReportsIssue`.

**SpdxDocument_Validate_NtiaMinimumElements_ReportsIssues**: Verifies that validation reports issues when the
document does not satisfy NTIA minimum element requirements for an SBOM.
This scenario is tested by `SpdxDocument_Validate_NtiaMinimumElements_ReportsIssues`.

**SpdxDocument_GetAllElements_WithMixedElements_ReturnsAllNonRelationshipElements**: Verifies that
`GetAllElements` returns the combined collection of all packages, files, snippets, annotations,
and the document element itself, and that `SpdxRelationship` elements are excluded.
This scenario is tested by `SpdxDocument_GetAllElements_WithMixedElements_ReturnsAllNonRelationshipElements`.

**SpdxDocument_GetElement_Document_ReturnsDocumentElement**: Verifies that GetElement returns
the document element when queried by the document SPDX-ID.
This scenario is tested by `SpdxDocument_GetElement_Document_ReturnsDocumentElement`.

**SpdxDocument_GetElement_File_ReturnsFileElement**: Verifies that GetElement returns a file
element when queried by a file SPDX-ID.
This scenario is tested by `SpdxDocument_GetElement_File_ReturnsFileElement`.

**SpdxDocument_GetElement_Package_ReturnsPackageElement**: Verifies that GetElement returns a
package element when queried by a package SPDX-ID.
This scenario is tested by `SpdxDocument_GetElement_Package_ReturnsPackageElement`.

**SpdxDocument_GetElement_Snippet_ReturnsSnippetElement**: Verifies that GetElement returns a
snippet element when queried by a snippet SPDX-ID.
This scenario is tested by `SpdxDocument_GetElement_Snippet_ReturnsSnippetElement`.

**SpdxDocument_Validate_InvalidAnnotation_ReportsIssue**: Verifies that validation reports an issue when
an annotation within the document contains invalid fields.
This scenario is tested by `SpdxDocument_Validate_InvalidAnnotation_ReportsIssue`.
