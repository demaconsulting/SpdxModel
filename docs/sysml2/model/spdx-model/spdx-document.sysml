package SpdxModel {
    private import SoftwareItemMetadata::*;

    part def SpdxDocument {
        doc /* Root container of the SPDX object model, aggregating all elements and exposing validation, deep copy, and element retrieval. */

        metadata info : SoftwareInfo {
            sourceFiles = ("src/DemaConsulting.SpdxModel/SpdxDocument.cs");
            testFiles = ("test/DemaConsulting.SpdxModel.Tests/SpdxDocumentTests.cs");
            designFiles = ("docs/design/spdx-model/spdx-document.md");
            verificationFiles = ("docs/verification/spdx-model/spdx-document.md");
            requirementFiles = ("docs/reqstream/spdx-model/spdx-document.yaml");
        }
    }
}
