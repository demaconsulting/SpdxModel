package SpdxModel {
    private import SoftwareItemMetadata::*;

    part def SpdxSnippet {
        doc /* Portion of a file whose licensing or provenance differs from the rest of the file. */

        metadata info : SoftwareInfo {
            sourceFiles = ("src/DemaConsulting.SpdxModel/SpdxSnippet.cs");
            testFiles = ("test/DemaConsulting.SpdxModel.Tests/SpdxSnippetTests.cs");
            designFiles = ("docs/design/spdx-model/spdx-snippet.md");
            verificationFiles = ("docs/verification/spdx-model/spdx-snippet.md");
            requirementFiles = ("docs/reqstream/spdx-model/spdx-snippet.yaml");
        }
    }
}
