// Copyright(c) 2024 DEMA Consulting
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using DemaConsulting.SpdxModel.IO;

namespace DemaConsulting.SpdxModel.Tests.IO;

/// <summary>
/// Tests for serializing <see cref="SpdxPackage"/> to JSON.
/// </summary>
[TestClass]
public class Spdx2JsonSerializePackage
{
    /// <summary>
    /// Tests serializing a package.
    /// </summary>
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
            Annotations =
            [
                new SpdxAnnotation
                {
                    Date = "2011-01-29T18:30:22Z",
                    Type = SpdxAnnotationType.Other,
                    Annotator = "Person: Package Commenter",
                    Comment = "Package level annotation"
                }
            ],
            AttributionText =
            [
                "The GNU C Library is free software.  See the file COPYING.LIB for copying conditions, and LICENSES for notices about a few contributions that require these additional notices to be distributed.  License copyright years may be listed using range notation, e.g., 1996-2015, indicating that every year in the range, inclusive, is a copyrightable year that would otherwise be listed individually."
            ],
            BuiltDate = "2011-01-29T18:30:22Z",
            Checksums =
            [
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
            ],
            CopyrightText = "Copyright 2008-2010 John Smith",
            Description =
                "The GNU C Library defines functions that are specified by the ISO C standard, as well as additional features specific to POSIX and other derivatives of the Unix operating system, and extensions specific to GNU systems.",
            DownloadLocation = "http://ftp.gnu.org/gnu/glibc/glibc-ports-2.15.tar.gz",
            ExternalReferences =
            [
                new SpdxExternalReference
                {
                    Category = SpdxReferenceCategory.Security,
                    Locator = "cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*",
                    Type = "cpe23Type"
                }
            ],
            FilesAnalyzed = true,
            HasFiles = ["file1.txt", "file2.txt"],
            VerificationCode = new SpdxPackageVerificationCode
            {
                ExcludedFiles = ["file1.txt"],
                Value = "1234567890abcdef"
            }
        };

        // Act
        var json = Spdx2JsonSerializer.SerializePackage(package);

        // Assert
        Assert.IsNotNull(json);
        SpdxJsonHelpers.AssertEqual("SPDXRef-Package", json["SPDXID"]);
        SpdxJsonHelpers.AssertEqual("glibc", json["name"]);
        SpdxJsonHelpers.AssertEqual("2.11.1", json["versionInfo"]);
        SpdxJsonHelpers.AssertEqual("glibc-2.11.1.tar.gz", json["packageFileName"]);
        SpdxJsonHelpers.AssertEqual("Person: Jane Doe (jane.doe@example.com)", json["supplier"]);
        SpdxJsonHelpers.AssertEqual("Organization: ExampleCodeInspect (contact@example.com)", json["originator"]);
        SpdxJsonHelpers.AssertEqual("Person: Package Commenter", json["annotations"]?[0]?["annotator"]);
        SpdxJsonHelpers.AssertEqual("2011-01-29T18:30:22Z", json["annotations"]?[0]?["annotationDate"]);
        SpdxJsonHelpers.AssertEqual("OTHER", json["annotations"]?[0]?["annotationType"]);
        SpdxJsonHelpers.AssertEqual("Package level annotation", json["annotations"]?[0]?["comment"]);
        SpdxJsonHelpers.AssertEqual(
            "The GNU C Library is free software.  See the file COPYING.LIB for copying conditions, and LICENSES for notices about a few contributions that require these additional notices to be distributed.  License copyright years may be listed using range notation, e.g., 1996-2015, indicating that every year in the range, inclusive, is a copyrightable year that would otherwise be listed individually.",
            json["attributionTexts"]?[0]);
        SpdxJsonHelpers.AssertEqual("2011-01-29T18:30:22Z", json["builtDate"]);
        SpdxJsonHelpers.AssertEqual("85ed0817af83a24ad8da68c2b5094de69833983c",
            json["checksums"]?[0]?["checksumValue"]);
        SpdxJsonHelpers.AssertEqual("SHA1", json["checksums"]?[0]?["algorithm"]);
        SpdxJsonHelpers.AssertEqual("11b6d3ee554eedf79299905a98f9b9a04e498210b59f15094c916c91d150efcd",
            json["checksums"]?[1]?["checksumValue"]);
        SpdxJsonHelpers.AssertEqual("SHA256", json["checksums"]?[1]?["algorithm"]);
        SpdxJsonHelpers.AssertEqual("Copyright 2008-2010 John Smith", json["copyrightText"]);
        SpdxJsonHelpers.AssertEqual("http://ftp.gnu.org/gnu/glibc/glibc-ports-2.15.tar.gz", json["downloadLocation"]);
        SpdxJsonHelpers.AssertEqual("SECURITY", json["externalRefs"]?[0]?["referenceCategory"]);
        SpdxJsonHelpers.AssertEqual("cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*",
            json["externalRefs"]?[0]?["referenceLocator"]);
        SpdxJsonHelpers.AssertEqual("cpe23Type", json["externalRefs"]?[0]?["referenceType"]);
        SpdxJsonHelpers.AssertEqual("true", json["filesAnalyzed"]);
        SpdxJsonHelpers.AssertEqual("file1.txt", json["hasFiles"]?[0]);
        SpdxJsonHelpers.AssertEqual("file2.txt", json["hasFiles"]?[1]);
        SpdxJsonHelpers.AssertEqual("file1.txt",
            json["packageVerificationCode"]?["packageVerificationCodeExcludedFiles"]?[0]);
        SpdxJsonHelpers.AssertEqual("1234567890abcdef",
            json["packageVerificationCode"]?["packageVerificationCodeValue"]);
    }

    /// <summary>
    /// Tests serializing multiple packages.
    /// </summary>
    [TestMethod]
    public void SerializePackages()
    {
        // Arrange
        var packages = new[]
        {
            new SpdxPackage
            {
                Id = "SPDXRef-Package",
                Name = "glibc",
                Version = "2.11.1",
                FileName = "glibc-2.11.1.tar.gz",
                Supplier = "Person: Jane Doe (jane.doe@example.com)",
                Originator = "Organization: ExampleCodeInspect (contact@example.com)",
                Annotations =
                [
                    new SpdxAnnotation
                    {
                        Date = "2011-01-29T18:30:22Z",
                        Type = SpdxAnnotationType.Other,
                        Annotator = "Person: Package Commenter",
                        Comment = "Package level annotation"
                    }
                ],
                AttributionText =
                [
                    "The GNU C Library is free software.  See the file COPYING.LIB for copying conditions, and LICENSES for notices about a few contributions that require these additional notices to be distributed.  License copyright years may be listed using range notation, e.g., 1996-2015, indicating that every year in the range, inclusive, is a copyrightable year that would otherwise be listed individually."
                ],
                BuiltDate = "2011-01-29T18:30:22Z",
                Checksums =
                [
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
                ],
                CopyrightText = "Copyright 2008-2010 John Smith",
                Description =
                    "The GNU C Library defines functions that are specified by the ISO C standard, as well as additional features specific to POSIX and other derivatives of the Unix operating system, and extensions specific to GNU systems.",
                DownloadLocation = "http://ftp.gnu.org/gnu/glibc/glibc-ports-2.15.tar.gz",
                ExternalReferences =
                [
                    new SpdxExternalReference
                    {
                        Category = SpdxReferenceCategory.Security,
                        Locator = "cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*",
                        Type = "cpe23Type"
                    }
                ],
                FilesAnalyzed = true,
                HasFiles = ["file1.txt", "file2.txt"],
                VerificationCode = new SpdxPackageVerificationCode
                {
                    ExcludedFiles = ["file1.txt"],
                    Value = "1234567890abcdef"
                }
            }
        };

        // Act
        var json = Spdx2JsonSerializer.SerializePackages(packages);

        // Assert
        Assert.IsNotNull(json);
        Assert.AreEqual(1, json.Count);
        SpdxJsonHelpers.AssertEqual("SPDXRef-Package", json[0]?["SPDXID"]);
        SpdxJsonHelpers.AssertEqual("glibc", json[0]?["name"]);
        SpdxJsonHelpers.AssertEqual("2.11.1", json[0]?["versionInfo"]);
        SpdxJsonHelpers.AssertEqual("glibc-2.11.1.tar.gz", json[0]?["packageFileName"]);
        SpdxJsonHelpers.AssertEqual("Person: Jane Doe (jane.doe@example.com)", json[0]?["supplier"]);
        SpdxJsonHelpers.AssertEqual("Organization: ExampleCodeInspect (contact@example.com)", json[0]?["originator"]);
        SpdxJsonHelpers.AssertEqual("Person: Package Commenter", json[0]?["annotations"]?[0]?["annotator"]);
        SpdxJsonHelpers.AssertEqual("2011-01-29T18:30:22Z", json[0]?["annotations"]?[0]?["annotationDate"]);
        SpdxJsonHelpers.AssertEqual("OTHER", json[0]?["annotations"]?[0]?["annotationType"]);
        SpdxJsonHelpers.AssertEqual("Package level annotation", json[0]?["annotations"]?[0]?["comment"]);
        SpdxJsonHelpers.AssertEqual(
            "The GNU C Library is free software.  See the file COPYING.LIB for copying conditions, and LICENSES for notices about a few contributions that require these additional notices to be distributed.  License copyright years may be listed using range notation, e.g., 1996-2015, indicating that every year in the range, inclusive, is a copyrightable year that would otherwise be listed individually.",
            json[0]?["attributionTexts"]?[0]);
        SpdxJsonHelpers.AssertEqual("2011-01-29T18:30:22Z", json[0]?["builtDate"]);
        SpdxJsonHelpers.AssertEqual("85ed0817af83a24ad8da68c2b5094de69833983c",
            json[0]?["checksums"]?[0]?["checksumValue"]);
        SpdxJsonHelpers.AssertEqual("SHA1", json[0]?["checksums"]?[0]?["algorithm"]);
        SpdxJsonHelpers.AssertEqual("11b6d3ee554eedf79299905a98f9b9a04e498210b59f15094c916c91d150efcd",
            json[0]?["checksums"]?[1]?["checksumValue"]);
        SpdxJsonHelpers.AssertEqual("SHA256", json[0]?["checksums"]?[1]?["algorithm"]);
        SpdxJsonHelpers.AssertEqual("Copyright 2008-2010 John Smith", json[0]?["copyrightText"]);
        SpdxJsonHelpers.AssertEqual("http://ftp.gnu.org/gnu/glibc/glibc-ports-2.15.tar.gz",
            json[0]?["downloadLocation"]);
        SpdxJsonHelpers.AssertEqual("SECURITY", json[0]?["externalRefs"]?[0]?["referenceCategory"]);
        SpdxJsonHelpers.AssertEqual("cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*",
            json[0]?["externalRefs"]?[0]?["referenceLocator"]);
        SpdxJsonHelpers.AssertEqual("cpe23Type", json[0]?["externalRefs"]?[0]?["referenceType"]);
        SpdxJsonHelpers.AssertEqual("true", json[0]?["filesAnalyzed"]);
        SpdxJsonHelpers.AssertEqual("file1.txt", json[0]?["hasFiles"]?[0]);
        SpdxJsonHelpers.AssertEqual("file2.txt", json[0]?["hasFiles"]?[1]);
        SpdxJsonHelpers.AssertEqual("file1.txt",
            json[0]?["packageVerificationCode"]?["packageVerificationCodeExcludedFiles"]?[0]);
        SpdxJsonHelpers.AssertEqual("1234567890abcdef",
            json[0]?["packageVerificationCode"]?["packageVerificationCodeValue"]);
    }
}