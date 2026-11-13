package SpdxModel {
    private import SoftwareItemMetadata::*;

    part def Spdx2JsonDeserializer {
        doc /* Reads SPDX 2.2 and 2.3 JSON documents into the in-memory SpdxDocument object model. */

        metadata info : SoftwareInfo {
            sourceFiles = ("src/DemaConsulting.SpdxModel/IO/Spdx2JsonDeserializer.cs");
            testFiles = ("test/DemaConsulting.SpdxModel.Tests/IO/Spdx2JsonDeserialize*.cs");
            designFiles = ("docs/design/spdx-model/io/spdx-2-json-deserializer.md");
            verificationFiles = ("docs/verification/spdx-model/io/spdx-2-json-deserializer.md");
            requirementFiles = ("docs/reqstream/spdx-model/io/spdx-2-json-deserializer.yaml");
        }
    }
}
