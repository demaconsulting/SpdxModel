package SpdxModel {
    private import SoftwareItemMetadata::*;

    part def SpdxFile {
        doc /* Individual file within an SPDX document, with its licensing, checksums, and contributor information. */

        metadata info : SoftwareInfo {
            sourceFiles = ("src/DemaConsulting.SpdxModel/SpdxFile.cs", "src/DemaConsulting.SpdxModel/SpdxFileType.cs");
            testFiles = ("test/DemaConsulting.SpdxModel.Tests/SpdxFileTests.cs");
            designFiles = ("docs/design/spdx-model/spdx-file.md");
            verificationFiles = ("docs/verification/spdx-model/spdx-file.md");
            requirementFiles = ("docs/reqstream/spdx-model/spdx-file.yaml");
        }
    }
}
