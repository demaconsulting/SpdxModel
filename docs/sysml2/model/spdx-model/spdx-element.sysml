package SpdxModel {
    private import SoftwareItemMetadata::*;

    part def SpdxElement {
        doc /* Abstract base class for all identifiable SPDX elements, defining the common SPDX identifier. */

        metadata info : SoftwareInfo {
            sourceFiles = ("src/DemaConsulting.SpdxModel/SpdxElement.cs");
            testFiles = ("test/DemaConsulting.SpdxModel.Tests/SpdxElementTests.cs");
            designFiles = ("docs/design/spdx-model/spdx-element.md");
            verificationFiles = ("docs/verification/spdx-model/spdx-element.md");
            requirementFiles = ("docs/reqstream/spdx-model/spdx-element.yaml");
        }
    }
}
