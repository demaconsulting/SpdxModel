package SpdxModel {
    private import SoftwareItemMetadata::*;

    part def TransformSubsystem {
        doc /* In-memory manipulation of SPDX documents, such as building relationship graphs. */

        metadata info : SoftwareInfo {
            testFiles = ("test/DemaConsulting.SpdxModel.Tests/Transforms/SpdxModelTransformTests.cs");
            designFiles = ("docs/design/spdx-model/transform/transform.md");
            verificationFiles = ("docs/verification/spdx-model/transform/transform.md");
            requirementFiles = ("docs/reqstream/spdx-model/transform/transform.yaml");
        }

        part spdxRelationships : SpdxRelationships;
    }
}
