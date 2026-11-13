package SpdxModel {
    private import SoftwareItemMetadata::*;

    part def SpdxAnnotation {
        doc /* SPDX annotation: a comment or review note attached to an SPDX element by a person, organization, or tool. */

        metadata info : SoftwareInfo {
            sourceFiles = ("src/DemaConsulting.SpdxModel/SpdxAnnotation.cs", "src/DemaConsulting.SpdxModel/SpdxAnnotationType.cs");
            testFiles = ("test/DemaConsulting.SpdxModel.Tests/SpdxAnnotationTests.cs");
            designFiles = ("docs/design/spdx-model/spdx-annotation.md");
            verificationFiles = ("docs/verification/spdx-model/spdx-annotation.md");
            requirementFiles = ("docs/reqstream/spdx-model/spdx-annotation.yaml");
        }
    }
}
