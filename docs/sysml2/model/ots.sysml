package OtsDependencies {
    private import SoftwareItemMetadata::*;

    doc /* Off-the-shelf (OTS) items used by SpdxModel: build, test, and compliance tooling. */

    part def BuildMark {
        doc /* OTS: BuildMark tool that generates build notes. */

        metadata info : SoftwareInfo {
            requirementFiles = ("docs/reqstream/ots/buildmark.yaml");
        }
    }

    part def FileAssert {
        doc /* OTS: FileAssert tool that asserts properties of generated files. */

        metadata info : SoftwareInfo {
            requirementFiles = ("docs/reqstream/ots/fileassert.yaml");
        }
    }

    part def Pandoc {
        doc /* OTS: Pandoc document converter used to compile documentation to HTML. */

        metadata info : SoftwareInfo {
            requirementFiles = ("docs/reqstream/ots/pandoc.yaml");
        }
    }

    part def ReqStream {
        doc /* OTS: ReqStream requirements trace and enforcement tool. */

        metadata info : SoftwareInfo {
            requirementFiles = ("docs/reqstream/ots/reqstream.yaml");
        }
    }

    part def ReviewMark {
        doc /* OTS: ReviewMark file-review planning and evidence tool. */

        metadata info : SoftwareInfo {
            requirementFiles = ("docs/reqstream/ots/reviewmark.yaml");
        }
    }

    part def SarifMark {
        doc /* OTS: SarifMark tool that converts SARIF analysis results into reports. */

        metadata info : SoftwareInfo {
            requirementFiles = ("docs/reqstream/ots/sarifmark.yaml");
        }
    }

    part def SonarMark {
        doc /* OTS: SonarMark tool that reports SonarCloud quality-gate results. */

        metadata info : SoftwareInfo {
            requirementFiles = ("docs/reqstream/ots/sonarmark.yaml");
        }
    }

    part def SysML2Tools {
        doc /* OTS: SysML2Tools linter, query engine, and renderer for the architecture model. */

        metadata info : SoftwareInfo {
            verificationFiles = ("docs/verification/ots/sysml2tools.md");
            requirementFiles = ("docs/reqstream/ots/sysml2tools.yaml");
        }
    }

    part def VersionMark {
        doc /* OTS: VersionMark tool that captures tool versions used in a build. */

        metadata info : SoftwareInfo {
            requirementFiles = ("docs/reqstream/ots/versionmark.yaml");
        }
    }

    part def WeasyPrint {
        doc /* OTS: WeasyPrint HTML-to-PDF renderer for compliance documents. */

        metadata info : SoftwareInfo {
            requirementFiles = ("docs/reqstream/ots/weasyprint.yaml");
        }
    }

    part def XUnit {
        doc /* OTS: xUnit test framework used to run the test suite. */

        metadata info : SoftwareInfo {
            requirementFiles = ("docs/reqstream/ots/xunit.yaml");
        }
    }
}
