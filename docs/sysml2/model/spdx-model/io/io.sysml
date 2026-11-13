package SpdxModel {
    private import SoftwareItemMetadata::*;

    part def IOSubsystem {
        doc /* JSON serialization and deserialization of SPDX 2.x documents to and from the in-memory SpdxDocument model. */

        metadata info : SoftwareInfo {
            testFiles = ("test/DemaConsulting.SpdxModel.Tests/IO/SpdxModelIOTests.cs");
            designFiles = ("docs/design/spdx-model/io/io.md");
            verificationFiles = ("docs/verification/spdx-model/io/io.md");
            requirementFiles = ("docs/reqstream/spdx-model/io/io.yaml");
        }

        part spdx2JsonDeserializer : Spdx2JsonDeserializer;
        part spdx2JsonSerializer : Spdx2JsonSerializer;
        part spdxConstants : SpdxConstants;
    }
}
