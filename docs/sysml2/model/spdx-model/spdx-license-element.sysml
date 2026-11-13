package SpdxModel {
    private import SoftwareItemMetadata::*;

    part def SpdxLicenseElement {
        doc /* Abstract base class adding concluded-license, copyright, and attribution fields to SpdxElement. */

        metadata info : SoftwareInfo {
            sourceFiles = ("src/DemaConsulting.SpdxModel/SpdxLicenseElement.cs");
            testFiles = ("test/DemaConsulting.SpdxModel.Tests/SpdxLicenseElementTests.cs");
            designFiles = ("docs/design/spdx-model/spdx-license-element.md");
            verificationFiles = ("docs/verification/spdx-model/spdx-license-element.md");
            requirementFiles = ("docs/reqstream/spdx-model/spdx-license-element.yaml");
        }
    }
}
