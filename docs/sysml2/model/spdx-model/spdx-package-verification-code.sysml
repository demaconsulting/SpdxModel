package SpdxModel {
    private import SoftwareItemMetadata::*;

    part def SpdxPackageVerificationCode {
        doc /* SPDX package verification code: a SHA1 digest over the contents of a package. */

        metadata info : SoftwareInfo {
            sourceFiles = ("src/DemaConsulting.SpdxModel/SpdxPackageVerificationCode.cs");
            testFiles = ("test/DemaConsulting.SpdxModel.Tests/SpdxPackageVerificationCodeTests.cs");
            designFiles = ("docs/design/spdx-model/spdx-package-verification-code.md");
            verificationFiles = ("docs/verification/spdx-model/spdx-package-verification-code.md");
            requirementFiles = ("docs/reqstream/spdx-model/spdx-package-verification-code.yaml");
        }
    }
}
