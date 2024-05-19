using DemaConsulting.SpdxModel.IO;

namespace DemaConsulting.SpdxModel.Tests.IO;

[TestClass]
public class SpdxJsonSerializeFile
{
    [TestMethod]
    public void SerializeFile()
    {
        // Arrange
        var file = new SpdxFile
        {
            Id = "SPDXRef-DoapSource",
            FileName = "./src/org/spdx/parser/DOAPProject.java",
            FileTypes = new[] { SpdxFileType.Source },
            Checksums = new[]
            {
                new SpdxChecksum
                {
                    Algorithm = SpdxChecksumAlgorithm.Sha1,
                    Value = "2fd4e1c67a2d28f123849ee1bb76e7391b93eb12"
                }
            },
            LicenseConcluded = "Apache-2.0",
            LicenseInfoInFiles = new[] { "Apache-2.0" },
            LicenseComments = "This license is used by Jena",
            Copyright = "Copyright 2010, 2011 Source Auditor Inc.",
            Comment = "This file is a sample DOAP file",
            Notice = "Copyright (c) 2001 Aaron Lehmann aaroni@vitelus.com",
            Contributors = new[]
            {
                "Protecode Inc.",
                "SPDX Technical Team Members",
                "Open Logic Inc.",
                "Source Auditor Inc.",
                "Black Duck Software In.c"
            },
            AttributionText = new[]
            {
                "All advertising materials mentioning features or use of this software must display the following acknowledgement: This product includes software developed by the AT&T."
            },
            Annotations = new[]
            {
                new SpdxAnnotation
                {
                    Date = "2011-01-29T18:30:22Z",
                    Type = SpdxAnnotationType.Other,
                    Comment = "Person: File Commenter",
                    Annotator = "File level annotation"
                }
            }
        };

        // Act
        var json = SpdxJsonSerializer.SerializeFile(file);

        // Assert
        Assert.AreEqual("SPDXRef-DoapSource", json["SPDXID"]?.ToString());
        Assert.AreEqual("./src/org/spdx/parser/DOAPProject.java", json["fileName"]?.ToString());
        Assert.AreEqual("SOURCE", json["fileTypes"]?[0]?.ToString());
        Assert.AreEqual("SHA1", json["checksums"]?[0]?["algorithm"]?.ToString());
        Assert.AreEqual("2fd4e1c67a2d28f123849ee1bb76e7391b93eb12",
            json["checksums"]?[0]?["checksumValue"]?.ToString());
        Assert.AreEqual("Apache-2.0", json["licenseConcluded"]?.ToString());
        Assert.AreEqual("Apache-2.0", json["licenseInfoInFile"]?[0]?.ToString());
        Assert.AreEqual("This license is used by Jena", json["licenseComments"]?.ToString());
        Assert.AreEqual("Copyright 2010, 2011 Source Auditor Inc.", json["copyrightText"]?.ToString());
        Assert.AreEqual("This file is a sample DOAP file", json["comment"]?.ToString());
        Assert.AreEqual("Copyright (c) 2001 Aaron Lehmann aaroni@vitelus.com", json["noticeText"]?.ToString());
        Assert.AreEqual("Protecode Inc.", json["fileContributors"]?[0]?.ToString());
        Assert.AreEqual("SPDX Technical Team Members", json["fileContributors"]?[1]?.ToString());
        Assert.AreEqual("Open Logic Inc.", json["fileContributors"]?[2]?.ToString());
        Assert.AreEqual("Source Auditor Inc.", json["fileContributors"]?[3]?.ToString());
        Assert.AreEqual("Black Duck Software In.c", json["fileContributors"]?[4]?.ToString());
        Assert.AreEqual(
            "All advertising materials mentioning features or use of this software must display the following acknowledgement: This product includes software developed by the AT&T.",
            json["attributionTexts"]?[0]?.ToString());
        Assert.AreEqual("2011-01-29T18:30:22Z", json["annotations"]?[0]?["annotationDate"]?.ToString());
        Assert.AreEqual("OTHER", json["annotations"]?[0]?["annotationType"]?.ToString());
        Assert.AreEqual("Person: File Commenter", json["annotations"]?[0]?["comment"]?.ToString());
        Assert.AreEqual("File level annotation", json["annotations"]?[0]?["annotator"]?.ToString());
    }

    [TestMethod]
    public void SerializeFiles()
    {
        // Arrange
        var file = new[]
        {
            new SpdxFile
            {
                Id = "SPDXRef-DoapSource",
                FileName = "./src/org/spdx/parser/DOAPProject.java",
                FileTypes = new[] { SpdxFileType.Source },
                Checksums = new[]
                {
                    new SpdxChecksum
                    {
                        Algorithm = SpdxChecksumAlgorithm.Sha1,
                        Value = "2fd4e1c67a2d28f123849ee1bb76e7391b93eb12"
                    }
                },
                LicenseConcluded = "Apache-2.0",
                LicenseInfoInFiles = new[] { "Apache-2.0" },
                LicenseComments = "This license is used by Jena",
                Copyright = "Copyright 2010, 2011 Source Auditor Inc.",
                Comment = "This file is a sample DOAP file",
                Notice = "Copyright (c) 2001 Aaron Lehmann aaroni@vitelus.com",
                Contributors = new[]
                {
                    "Protecode Inc.",
                    "SPDX Technical Team Members",
                    "Open Logic Inc.",
                    "Source Auditor Inc.",
                    "Black Duck Software In.c"
                },
                AttributionText = new[]
                {
                    "All advertising materials mentioning features or use of this software must display the following acknowledgement: This product includes software developed by the AT&T."
                },
                Annotations = new[]
                {
                    new SpdxAnnotation
                    {
                        Date = "2011-01-29T18:30:22Z",
                        Type = SpdxAnnotationType.Other,
                        Comment = "Person: File Commenter",
                        Annotator = "File level annotation"
                    }
                }
            }
        };

        // Act
        var json = SpdxJsonSerializer.SerializeFiles(file);

        // Assert
        Assert.AreEqual(1, json.Count);
        Assert.AreEqual("SPDXRef-DoapSource", json[0]?["SPDXID"]?.ToString());
        Assert.AreEqual("./src/org/spdx/parser/DOAPProject.java", json[0]?["fileName"]?.ToString());
        Assert.AreEqual("SOURCE", json[0]?["fileTypes"]?[0]?.ToString());
        Assert.AreEqual("SHA1", json[0]?["checksums"]?[0]?["algorithm"]?.ToString());
        Assert.AreEqual("2fd4e1c67a2d28f123849ee1bb76e7391b93eb12",
            json[0]?["checksums"]?[0]?["checksumValue"]?.ToString());
        Assert.AreEqual("Apache-2.0", json[0]?["licenseConcluded"]?.ToString());
        Assert.AreEqual("Apache-2.0", json[0]?["licenseInfoInFile"]?[0]?.ToString());
        Assert.AreEqual("This license is used by Jena", json[0]?["licenseComments"]?.ToString());
        Assert.AreEqual("Copyright 2010, 2011 Source Auditor Inc.", json[0]?["copyrightText"]?.ToString());
        Assert.AreEqual("This file is a sample DOAP file", json[0]?["comment"]?.ToString());
        Assert.AreEqual("Copyright (c) 2001 Aaron Lehmann aaroni@vitelus.com", json[0]?["noticeText"]?.ToString());
        Assert.AreEqual("Protecode Inc.", json[0]?["fileContributors"]?[0]?.ToString());
        Assert.AreEqual("SPDX Technical Team Members", json[0]?["fileContributors"]?[1]?.ToString());
        Assert.AreEqual("Open Logic Inc.", json[0]?["fileContributors"]?[2]?.ToString());
        Assert.AreEqual("Source Auditor Inc.", json[0]?["fileContributors"]?[3]?.ToString());
        Assert.AreEqual("Black Duck Software In.c", json[0]?["fileContributors"]?[4]?.ToString());
        Assert.AreEqual(
            "All advertising materials mentioning features or use of this software must display the following acknowledgement: This product includes software developed by the AT&T.",
            json[0]?["attributionTexts"]?[0]?.ToString());
        Assert.AreEqual("2011-01-29T18:30:22Z", json[0]?["annotations"]?[0]?["annotationDate"]?.ToString());
        Assert.AreEqual("OTHER", json[0]?["annotations"]?[0]?["annotationType"]?.ToString());
        Assert.AreEqual("Person: File Commenter", json[0]?["annotations"]?[0]?["comment"]?.ToString());
        Assert.AreEqual("File level annotation", json[0]?["annotations"]?[0]?["annotator"]?.ToString());
    }
}