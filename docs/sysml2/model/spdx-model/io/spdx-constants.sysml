package SpdxModel {
    private import SoftwareItemMetadata::*;

    part def SpdxConstants {
        doc /* JSON property-name constants shared by the SPDX 2.x JSON serializer and deserializer. */

        metadata info : SoftwareInfo {
            sourceFiles = ("src/DemaConsulting.SpdxModel/IO/SpdxConstants.cs");
            designFiles = ("docs/design/spdx-model/io/spdx-constants.md");
            verificationFiles = ("docs/verification/spdx-model/io/spdx-constants.md");
            requirementFiles = ("docs/reqstream/spdx-model/io/spdx-constants.yaml");
        }
    }
}
