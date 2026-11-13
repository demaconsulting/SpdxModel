package SpdxModel {
    private import SoftwareItemMetadata::*;

    part def SpdxRelationships {
        doc /* Utility methods for adding relationships to an SpdxDocument without duplication. */

        metadata info : SoftwareInfo {
            sourceFiles = ("src/DemaConsulting.SpdxModel/Transform/SpdxRelationships.cs");
            testFiles = ("test/DemaConsulting.SpdxModel.Tests/Transform/SpdxRelationshipsTests.cs");
            designFiles = ("docs/design/spdx-model/transform/spdx-relationships.md");
            verificationFiles = ("docs/verification/spdx-model/transform/spdx-relationships.md");
            requirementFiles = ("docs/reqstream/spdx-model/transform/spdx-relationships.yaml");
        }
    }
}
