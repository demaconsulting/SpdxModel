using DemaConsulting.SpdxModel.IO;

namespace DemaConsulting.SpdxModel.Tests.IO;

[TestClass]
public class SpdxJsonSerializePackage
{
    [TestMethod]
    public void SerializePackage()
    {
        // Arrange
        var package = new SpdxPackage
        {
            Id = "SPDXRef-Package",
            Name = "glibc",
            Version = "2.11.1",
            FileName = "glibc-2.11.1.tar.gz",
            Supplier = "Person: Jane Doe (jane.doe@example.com)",
            Originator = "Organization: ExampleCodeInspect (contact@example.com)",
            Annotations = new []
            {
                new SpdxAnnotation()
                {
                    Date = "2011-01-29T18:30:22Z",
                    Type = SpdxAnnotationType.Other,
                    Annotator = "Person: Package Commenter",
                    Comment = "Package level annotation"
                }
            },
            Attributions = new []
            {
                "The GNU C Library is free software.  See the file COPYING.LIB for copying conditions, and LICENSES for notices about a few contributions that require these additional notices to be distributed.  License copyright years may be listed using range notation, e.g., 1996-2015, indicating that every year in the range, inclusive, is a copyrightable year that would otherwise be listed individually."
            },
            BuiltDate = "2011-01-29T18:30:22Z",
            Checksums = new[]
            {
                new SpdxChecksum
                {
                    Algorithm = SpdxChecksumAlgorithm.Sha1,
                    Value = "85ed0817af83a24ad8da68c2b5094de69833983c"
                },
                new SpdxChecksum
                {
                    Algorithm = SpdxChecksumAlgorithm.Sha256,
                    Value = "11b6d3ee554eedf79299905a98f9b9a04e498210b59f15094c916c91d150efcd"
                }
            },
            CopyrightText = "Copyright 2008-2010 John Smith",
            Description = "The GNU C Library defines functions that are specified by the ISO C standard, as well as additional features specific to POSIX and other derivatives of the Unix operating system, and extensions specific to GNU systems.",
            DownloadLocation = "http://ftp.gnu.org/gnu/glibc/glibc-ports-2.15.tar.gz",
            ExternalReferences = new[]
            {
                new SpdxExternalReference
                {
                    Category = SpdxReferenceCategory.Security,
                    Locator = "cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*",
                    Type = "cpe23Type"
                }
            },
            FilesAnalyzed = true,
            HasFiles = new[] { "file1.txt", "file2.txt" },
            VerificationCode = new SpdxPackageVerificationCode
            {
                ExcludedFiles = new[] { "file1.txt" },
                Value = "1234567890abcdef"
            }
        };

        // Act
        var json = SpdxJsonSerializer.SerializePackage(package);

        // Assert
        Assert.IsNotNull(json);
        Assert.AreEqual("SPDXRef-Package", json["SPDXID"]?.ToString());
        Assert.AreEqual("glibc", json["name"]?.ToString());
        Assert.AreEqual("2.11.1", json["versionInfo"]?.ToString());
        Assert.AreEqual("glibc-2.11.1.tar.gz", json["packageFileName"]?.ToString());
        Assert.AreEqual("Person: Jane Doe (jane.doe@example.com)", json["supplier"]?.ToString());
        Assert.AreEqual("Organization: ExampleCodeInspect (contact@example.com)", json["originator"]?.ToString());
        Assert.AreEqual("Person: Package Commenter", json["annotations"]?[0]?["annotator"]?.ToString());
        Assert.AreEqual("2011-01-29T18:30:22Z", json["annotations"]?[0]?["annotationDate"]?.ToString());
        Assert.AreEqual("OTHER", json["annotations"]?[0]?["annotationType"]?.ToString());
        Assert.AreEqual("Package level annotation", json["annotations"]?[0]?["comment"]?.ToString());
        Assert.AreEqual("The GNU C Library is free software.  See the file COPYING.LIB for copying conditions, and LICENSES for notices about a few contributions that require these additional notices to be distributed.  License copyright years may be listed using range notation, e.g., 1996-2015, indicating that every year in the range, inclusive, is a copyrightable year that would otherwise be listed individually.", json["attributionTexts"]?[0]?.ToString());
        Assert.AreEqual("2011-01-29T18:30:22Z", json["builtDate"]?.ToString());
        Assert.AreEqual("85ed0817af83a24ad8da68c2b5094de69833983c", json["checksums"]?[0]?["checksumValue"]?.ToString());
        Assert.AreEqual("SHA1", json["checksums"]?[0]?["algorithm"]?.ToString());
        Assert.AreEqual("11b6d3ee554eedf79299905a98f9b9a04e498210b59f15094c916c91d150efcd", json["checksums"]?[1]?["checksumValue"]?.ToString());
        Assert.AreEqual("SHA256", json["checksums"]?[1]?["algorithm"]?.ToString());
        Assert.AreEqual("Copyright 2008-2010 John Smith", json["copyrightText"]?.ToString());
        Assert.AreEqual("http://ftp.gnu.org/gnu/glibc/glibc-ports-2.15.tar.gz", json["downloadLocation"]?.ToString());
        Assert.AreEqual("SECURITY", json["externalRefs"]?[0]?["referenceCategory"]?.ToString());
        Assert.AreEqual("cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*", json["externalRefs"]?[0]?["referenceLocator"]?.ToString());
        Assert.AreEqual("cpe23Type", json["externalRefs"]?[0]?["referenceType"]?.ToString());
        Assert.AreEqual("true", json["filesAnalyzed"]?.ToString());
        Assert.AreEqual("file1.txt", json["hasFiles"]?[0]?.ToString());
        Assert.AreEqual("file2.txt", json["hasFiles"]?[1]?.ToString());
        Assert.AreEqual("file1.txt", json["packageVerificationCode"]?["packageVerificationCodeExcludedFiles"]?[0]?.ToString());
        Assert.AreEqual("1234567890abcdef", json["packageVerificationCode"]?["packageVerificationCodeValue"]?.ToString());
    }

    [TestMethod]
    public void SerializePackages()
    {
        // Arrange
        var packages = new[]
        {
            new SpdxPackage()
            {
                Id = "SPDXRef-Package",
                Name = "glibc",
                Version = "2.11.1",
                FileName = "glibc-2.11.1.tar.gz",
                Supplier = "Person: Jane Doe (jane.doe@example.com)",
                Originator = "Organization: ExampleCodeInspect (contact@example.com)",
                Annotations = new[]
                {
                    new SpdxAnnotation()
                    {
                        Date = "2011-01-29T18:30:22Z",
                        Type = SpdxAnnotationType.Other,
                        Annotator = "Person: Package Commenter",
                        Comment = "Package level annotation"
                    }
                },
                Attributions = new[]
                {
                    "The GNU C Library is free software.  See the file COPYING.LIB for copying conditions, and LICENSES for notices about a few contributions that require these additional notices to be distributed.  License copyright years may be listed using range notation, e.g., 1996-2015, indicating that every year in the range, inclusive, is a copyrightable year that would otherwise be listed individually."
                },
                BuiltDate = "2011-01-29T18:30:22Z",
                Checksums = new[]
                {
                    new SpdxChecksum
                    {
                        Algorithm = SpdxChecksumAlgorithm.Sha1,
                        Value = "85ed0817af83a24ad8da68c2b5094de69833983c"
                    },
                    new SpdxChecksum
                    {
                        Algorithm = SpdxChecksumAlgorithm.Sha256,
                        Value = "11b6d3ee554eedf79299905a98f9b9a04e498210b59f15094c916c91d150efcd"
                    }
                },
                CopyrightText = "Copyright 2008-2010 John Smith",
                Description =
                    "The GNU C Library defines functions that are specified by the ISO C standard, as well as additional features specific to POSIX and other derivatives of the Unix operating system, and extensions specific to GNU systems.",
                DownloadLocation = "http://ftp.gnu.org/gnu/glibc/glibc-ports-2.15.tar.gz",
                ExternalReferences = new[]
                {
                    new SpdxExternalReference
                    {
                        Category = SpdxReferenceCategory.Security,
                        Locator = "cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*",
                        Type = "cpe23Type"
                    }
                },
                FilesAnalyzed = true,
                HasFiles = new[] { "file1.txt", "file2.txt" },
                VerificationCode = new SpdxPackageVerificationCode
                {
                    ExcludedFiles = new[] { "file1.txt" },
                    Value = "1234567890abcdef"
                }
            }
        };

        // Act
        var json = SpdxJsonSerializer.SerializePackages(packages);

        // Assert
        Assert.IsNotNull(json);
        Assert.AreEqual(1, json.Count);
        Assert.AreEqual("SPDXRef-Package", json[0]?["SPDXID"]?.ToString());
        Assert.AreEqual("glibc", json[0]?["name"]?.ToString());
        Assert.AreEqual("2.11.1", json[0]?["versionInfo"]?.ToString());
        Assert.AreEqual("glibc-2.11.1.tar.gz", json[0]?["packageFileName"]?.ToString());
        Assert.AreEqual("Person: Jane Doe (jane.doe@example.com)", json[0]?["supplier"]?.ToString());
        Assert.AreEqual("Organization: ExampleCodeInspect (contact@example.com)", json[0]?["originator"]?.ToString());
        Assert.AreEqual("Person: Package Commenter", json[0]?["annotations"]?[0]?["annotator"]?.ToString());
        Assert.AreEqual("2011-01-29T18:30:22Z", json[0]?["annotations"]?[0]?["annotationDate"]?.ToString());
        Assert.AreEqual("OTHER", json[0]?["annotations"]?[0]?["annotationType"]?.ToString());
        Assert.AreEqual("Package level annotation", json[0]?["annotations"]?[0]?["comment"]?.ToString());
        Assert.AreEqual("The GNU C Library is free software.  See the file COPYING.LIB for copying conditions, and LICENSES for notices about a few contributions that require these additional notices to be distributed.  License copyright years may be listed using range notation, e.g., 1996-2015, indicating that every year in the range, inclusive, is a copyrightable year that would otherwise be listed individually.", json[0]?["attributionTexts"]?[0]?.ToString());
        Assert.AreEqual("2011-01-29T18:30:22Z", json[0]?["builtDate"]?.ToString());
        Assert.AreEqual("85ed0817af83a24ad8da68c2b5094de69833983c", json[0]?["checksums"]?[0]?["checksumValue"]?.ToString());
        Assert.AreEqual("SHA1", json[0]?["checksums"]?[0]?["algorithm"]?.ToString());
        Assert.AreEqual("11b6d3ee554eedf79299905a98f9b9a04e498210b59f15094c916c91d150efcd", json[0]?["checksums"]?[1]?["checksumValue"]?.ToString());
        Assert.AreEqual("SHA256", json[0]?["checksums"]?[1]?["algorithm"]?.ToString());
        Assert.AreEqual("Copyright 2008-2010 John Smith", json[0]?["copyrightText"]?.ToString());
        Assert.AreEqual("http://ftp.gnu.org/gnu/glibc/glibc-ports-2.15.tar.gz", json[0]?["downloadLocation"]?.ToString());
        Assert.AreEqual("SECURITY", json[0]?["externalRefs"]?[0]?["referenceCategory"]?.ToString());
        Assert.AreEqual("cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*", json[0]?["externalRefs"]?[0]?["referenceLocator"]?.ToString());
        Assert.AreEqual("cpe23Type", json[0]?["externalRefs"]?[0]?["referenceType"]?.ToString());
        Assert.AreEqual("true", json[0]?["filesAnalyzed"]?.ToString());
        Assert.AreEqual("file1.txt", json[0]?["hasFiles"]?[0]?.ToString());
        Assert.AreEqual("file2.txt", json[0]?["hasFiles"]?[1]?.ToString());
        Assert.AreEqual("file1.txt", json[0]?["packageVerificationCode"]?["packageVerificationCodeExcludedFiles"]?[0]?.ToString());
        Assert.AreEqual("1234567890abcdef", json[0]?["packageVerificationCode"]?["packageVerificationCodeValue"]?.ToString());
    }
}