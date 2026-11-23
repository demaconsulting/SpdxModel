package SpdxModel {
    public import SpdxModel_SpdxAnnotation::*;
    public import SpdxModel_SpdxChecksum::*;
    public import SpdxModel_SpdxCreationInformation::*;
    public import SpdxModel_SpdxDocument::*;
    public import SpdxModel_SpdxElement::*;
    public import SpdxModel_SpdxExternalDocumentReference::*;
    public import SpdxModel_SpdxExternalReference::*;
    public import SpdxModel_SpdxExtractedLicensingInfo::*;
    public import SpdxModel_SpdxFile::*;
    public import SpdxModel_SpdxHelpers::*;
    public import SpdxModel_SpdxLicenseElement::*;
    public import SpdxModel_SpdxPackage::*;
    public import SpdxModel_SpdxPackageVerificationCode::*;
    public import SpdxModel_SpdxRelationship::*;
    public import SpdxModel_SpdxSnippet::*;
    public import SpdxModel_IOSubsystem::*;
    public import SpdxModel_TransformSubsystem::*;
    private import OtsDependencies::*;
    private import SoftwareItemMetadata::*;

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
