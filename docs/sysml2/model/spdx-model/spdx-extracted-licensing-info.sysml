package SpdxModel {
    private import SoftwareItemMetadata::*;

    part def SpdxExtractedLicensingInfo {
        doc /* Full text and metadata of a non-standard license found within a software package. */

        metadata info : SoftwareInfo {
            sourceFiles = ("src/DemaConsulting.SpdxModel/SpdxExtractedLicensingInfo.cs");
            testFiles = ("test/DemaConsulting.SpdxModel.Tests/SpdxExtractedLicensingInfoTests.cs");
            designFiles = ("docs/design/spdx-model/spdx-extracted-licensing-info.md");
            verificationFiles = ("docs/verification/spdx-model/spdx-extracted-licensing-info.md");
            requirementFiles = ("docs/reqstream/spdx-model/spdx-extracted-licensing-info.yaml");
        }
    }
}
