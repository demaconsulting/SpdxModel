package SpdxModel {
    private import SoftwareItemMetadata::*;

    part def SpdxRelationship {
        doc /* Directed relationship between two SPDX elements. */

        metadata info : SoftwareInfo {
            sourceFiles = ("src/DemaConsulting.SpdxModel/SpdxRelationship.cs", "src/DemaConsulting.SpdxModel/SpdxRelationshipType.cs");
            testFiles = ("test/DemaConsulting.SpdxModel.Tests/SpdxRelationshipTests.cs");
            designFiles = ("docs/design/spdx-model/spdx-relationship.md");
            verificationFiles = ("docs/verification/spdx-model/spdx-relationship.md");
            requirementFiles = ("docs/reqstream/spdx-model/spdx-relationship.yaml");
        }
    }
}
