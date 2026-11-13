package SpdxModel {
    private import SoftwareItemMetadata::*;

    part def SpdxExternalReference {
        doc /* Link from an SPDX package to an external resource such as a package registry or vulnerability database entry. */

        metadata info : SoftwareInfo {
            sourceFiles = ("src/DemaConsulting.SpdxModel/SpdxExternalReference.cs", "src/DemaConsulting.SpdxModel/SpdxReferenceCategory.cs");
            testFiles = ("test/DemaConsulting.SpdxModel.Tests/SpdxExternalReferenceTests.cs");
            designFiles = ("docs/design/spdx-model/spdx-external-reference.md");
            verificationFiles = ("docs/verification/spdx-model/spdx-external-reference.md");
            requirementFiles = ("docs/reqstream/spdx-model/spdx-external-reference.yaml");
        }
    }
}
