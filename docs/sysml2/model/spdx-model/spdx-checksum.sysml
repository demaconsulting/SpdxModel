package SpdxModel {
    private import SoftwareItemMetadata::*;

    part def SpdxChecksum {
        doc /* SPDX checksum: an algorithm-value pair used to verify the integrity of files and packages. */

        metadata info : SoftwareInfo {
            sourceFiles = ("src/DemaConsulting.SpdxModel/SpdxChecksum.cs", "src/DemaConsulting.SpdxModel/SpdxChecksumAlgorithm.cs");
            testFiles = ("test/DemaConsulting.SpdxModel.Tests/SpdxChecksumTests.cs");
            designFiles = ("docs/design/spdx-model/spdx-checksum.md");
            verificationFiles = ("docs/verification/spdx-model/spdx-checksum.md");
            requirementFiles = ("docs/reqstream/spdx-model/spdx-checksum.yaml");
        }
    }
}
