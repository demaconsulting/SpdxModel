package SpdxModel {
    private import SoftwareItemMetadata::*;

    import OtsDependencies::*;

    doc
    /*
     * DemaConsulting SpdxModel is a .NET library providing an in-memory model of
     * SPDX (Software Package Data Exchange) documents, with SPDX 2.2/2.3 JSON
     * serialization and deserialization and relationship transformation utilities.
     */

    part def SpdxModelSystem {
        doc /* The SpdxModel library as a whole: the SPDX data model plus its IO and Transform subsystems. */

        metadata info : SoftwareInfo {
            testFiles = ("test/DemaConsulting.SpdxModel.Tests/SpdxModelTests.cs");
            designFiles = ("docs/design/spdx-model.md");
            verificationFiles = ("docs/verification/spdx-model.md");
            requirementFiles = ("docs/reqstream/spdx-model/spdx-model.yaml", "docs/reqstream/spdx-model/platform-requirements.yaml");
        }

        part io : IOSubsystem;
        part transform : TransformSubsystem;

        part spdxAnnotation : SpdxAnnotation;
        part spdxChecksum : SpdxChecksum;
        part spdxCreationInformation : SpdxCreationInformation;
        part spdxDocument : SpdxDocument;
        part spdxElement : SpdxElement;
        part spdxExternalDocumentReference : SpdxExternalDocumentReference;
        part spdxExternalReference : SpdxExternalReference;
        part spdxExtractedLicensingInfo : SpdxExtractedLicensingInfo;
        part spdxFile : SpdxFile;
        part spdxHelpers : SpdxHelpers;
        part spdxLicenseElement : SpdxLicenseElement;
        part spdxPackage : SpdxPackage;
        part spdxPackageVerificationCode : SpdxPackageVerificationCode;
        part spdxRelationship : SpdxRelationship;
        part spdxSnippet : SpdxSnippet;

        part buildMark : BuildMark;
        part fileAssert : FileAssert;
        part pandoc : Pandoc;
        part reqStream : ReqStream;
        part reviewMark : ReviewMark;
        part sarifMark : SarifMark;
        part sonarMark : SonarMark;
        part sysml2Tools : SysML2Tools;
        part versionMark : VersionMark;
        part weasyPrint : WeasyPrint;
        part xUnit : XUnit;
    }
}
