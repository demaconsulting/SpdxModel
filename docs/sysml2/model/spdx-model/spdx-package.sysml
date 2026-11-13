package SpdxModel {
    private import SoftwareItemMetadata::*;

    part def SpdxPackage {
        doc /* SPDX package: identity, provenance, licensing, verification, and dependency metadata for a software package. */

        metadata info : SoftwareInfo {
            sourceFiles = ("src/DemaConsulting.SpdxModel/SpdxPackage.cs");
            testFiles = ("test/DemaConsulting.SpdxModel.Tests/SpdxPackageTests.cs");
            designFiles = ("docs/design/spdx-model/spdx-package.md");
            verificationFiles = ("docs/verification/spdx-model/spdx-package.md");
            requirementFiles = ("docs/reqstream/spdx-model/spdx-package.yaml");
        }
    }
}
