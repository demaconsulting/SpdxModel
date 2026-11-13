package SpdxModel {
    private import SoftwareItemMetadata::*;

    part def Spdx2JsonSerializer {
        doc /* Writes an in-memory SpdxDocument object model as an SPDX 2.3 JSON string. */

        metadata info : SoftwareInfo {
            sourceFiles = ("src/DemaConsulting.SpdxModel/IO/Spdx2JsonSerializer.cs");
            testFiles = ("test/DemaConsulting.SpdxModel.Tests/IO/Spdx2JsonSerialize*.cs");
            designFiles = ("docs/design/spdx-model/io/spdx-2-json-serializer.md");
            verificationFiles = ("docs/verification/spdx-model/io/spdx-2-json-serializer.md");
            requirementFiles = ("docs/reqstream/spdx-model/io/spdx-2-json-serializer.yaml");
        }
    }
}
