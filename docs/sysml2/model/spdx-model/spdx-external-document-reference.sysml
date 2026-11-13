package SpdxModel {
    private import SoftwareItemMetadata::*;

    part def SpdxExternalDocumentReference {
        doc /* Reference from one SPDX document to another, with a checksum of the referenced document. */

        metadata info : SoftwareInfo {
            sourceFiles = ("src/DemaConsulting.SpdxModel/SpdxExternalDocumentReference.cs");
            testFiles = ("test/DemaConsulting.SpdxModel.Tests/SpdxExternalDocumentReferenceTests.cs");
            designFiles = ("docs/design/spdx-model/spdx-external-document-reference.md");
            verificationFiles = ("docs/verification/spdx-model/spdx-external-document-reference.md");
            requirementFiles = ("docs/reqstream/spdx-model/spdx-external-document-reference.yaml");
        }
    }
}
