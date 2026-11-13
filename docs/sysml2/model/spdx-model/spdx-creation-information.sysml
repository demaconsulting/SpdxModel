package SpdxModel {
    private import SoftwareItemMetadata::*;

    part def SpdxCreationInformation {
        doc /* Metadata about who created an SPDX document and when. */

        metadata info : SoftwareInfo {
            sourceFiles = ("src/DemaConsulting.SpdxModel/SpdxCreationInformation.cs");
            testFiles = ("test/DemaConsulting.SpdxModel.Tests/SpdxCreationInformationTests.cs");
            designFiles = ("docs/design/spdx-model/spdx-creation-information.md");
            verificationFiles = ("docs/verification/spdx-model/spdx-creation-information.md");
            requirementFiles = ("docs/reqstream/spdx-model/spdx-creation-information.yaml");
        }
    }
}
