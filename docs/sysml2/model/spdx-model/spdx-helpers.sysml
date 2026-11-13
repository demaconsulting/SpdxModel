package SpdxModel {
    private import SoftwareItemMetadata::*;

    part def SpdxHelpers {
        doc /* Internal static utility class providing string enhancement and SPDX date-time validation helpers. */

        metadata info : SoftwareInfo {
            sourceFiles = ("src/DemaConsulting.SpdxModel/SpdxHelpers.cs");
            testFiles = ("test/DemaConsulting.SpdxModel.Tests/SpdxHelpersTests.cs");
            designFiles = ("docs/design/spdx-model/spdx-helpers.md");
            verificationFiles = ("docs/verification/spdx-model/spdx-helpers.md");
            requirementFiles = ("docs/reqstream/spdx-model/spdx-helpers.yaml");
        }
    }
}
