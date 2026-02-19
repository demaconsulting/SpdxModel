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
///     Tests for serializing <see cref="SpdxFile" /> to JSON.
/// </summary>
[TestClass]
public class Spdx2JsonSerializeFile
{
    /// <summary>
    ///     Tests serializing a file.
    /// </summary>
    [TestMethod]
    public void Spdx2JsonSerializer_SerializeFile_CorrectResults()
    {
        // Arrange: Create a sample SpdxFile object
        var file = new SpdxFile
        {
            Id = "SPDXRef-DoapSource",
            FileName = "./src/org/spdx/parser/DOAPProject.java",
            FileTypes = [SpdxFileType.Source],
            Checksums =
            [
                new SpdxChecksum
                {
                    Algorithm = SpdxChecksumAlgorithm.Sha1,
                    Value = "2fd4e1c67a2d28f123849ee1bb76e7391b93eb12"
                }
            ],
            ConcludedLicense = "Apache-2.0",
            LicenseInfoInFiles = ["Apache-2.0"],
            LicenseComments = "This license is used by Jena",
            CopyrightText = "Copyright 2010, 2011 Source Auditor Inc.",
            Comment = "This file is a sample DOAP file",
            Notice = "Copyright (c) 2001 Aaron Lehmann aaroni@vitelus.com",
            Contributors =
            [
                "Protecode Inc.",
                "SPDX Technical Team Members",
                "Open Logic Inc.",
                "Source Auditor Inc.",
                "Black Duck Software In.c"
            ],
            AttributionText =
            [
                "All advertising materials mentioning features or use of this software must display the following acknowledgement: This product includes software developed by the AT&T."
            ],
            Annotations =
            [
                new SpdxAnnotation
                {
                    Date = "2011-01-29T18:30:22Z",
                    Type = SpdxAnnotationType.Other,
                    Comment = "Person: File Commenter",
                    Annotator = "File level annotation"
                }
            ]
        };

        // Act: Serialize the file to JSON
        var json = Spdx2JsonSerializer.SerializeFile(file);

        // Assert: Verify the JSON is not null and has the expected structure
        SpdxJsonHelpers.AssertEqual("SPDXRef-DoapSource", json["SPDXID"]);
        SpdxJsonHelpers.AssertEqual("./src/org/spdx/parser/DOAPProject.java", json["fileName"]);
        SpdxJsonHelpers.AssertEqual("SOURCE", json["fileTypes"]?[0]);
        SpdxJsonHelpers.AssertEqual("SHA1", json["checksums"]?[0]?["algorithm"]);
        SpdxJsonHelpers.AssertEqual("2fd4e1c67a2d28f123849ee1bb76e7391b93eb12",
            json["checksums"]?[0]?["checksumValue"]);
        SpdxJsonHelpers.AssertEqual("Apache-2.0", json["licenseConcluded"]);
        SpdxJsonHelpers.AssertEqual("Apache-2.0", json["licenseInfoInFiles"]?[0]);
        SpdxJsonHelpers.AssertEqual("This license is used by Jena", json["licenseComments"]);
        SpdxJsonHelpers.AssertEqual("Copyright 2010, 2011 Source Auditor Inc.", json["copyrightText"]);
        SpdxJsonHelpers.AssertEqual("This file is a sample DOAP file", json["comment"]);
        SpdxJsonHelpers.AssertEqual("Copyright (c) 2001 Aaron Lehmann aaroni@vitelus.com", json["noticeText"]);
        SpdxJsonHelpers.AssertEqual("Protecode Inc.", json["fileContributors"]?[0]);
        SpdxJsonHelpers.AssertEqual("SPDX Technical Team Members", json["fileContributors"]?[1]);
        SpdxJsonHelpers.AssertEqual("Open Logic Inc.", json["fileContributors"]?[2]);
        SpdxJsonHelpers.AssertEqual("Source Auditor Inc.", json["fileContributors"]?[3]);
        SpdxJsonHelpers.AssertEqual("Black Duck Software In.c", json["fileContributors"]?[4]);
        SpdxJsonHelpers.AssertEqual(
            "All advertising materials mentioning features or use of this software must display the following acknowledgement: This product includes software developed by the AT&T.",
            json["attributionTexts"]?[0]);
        SpdxJsonHelpers.AssertEqual("2011-01-29T18:30:22Z", json["annotations"]?[0]?["annotationDate"]);
        SpdxJsonHelpers.AssertEqual("OTHER", json["annotations"]?[0]?["annotationType"]);
        SpdxJsonHelpers.AssertEqual("Person: File Commenter", json["annotations"]?[0]?["comment"]);
        SpdxJsonHelpers.AssertEqual("File level annotation", json["annotations"]?[0]?["annotator"]);
    }

    /// <summary>
    ///     Tests serializing multiple files.
    /// </summary>
    [TestMethod]
    public void Spdx2JsonSerializer_SerializeFiles_CorrectResults()
    {
        // Arrange
        var file = new[]
        {
            new SpdxFile
            {
                Id = "SPDXRef-DoapSource",
                FileName = "./src/org/spdx/parser/DOAPProject.java",
                FileTypes = [SpdxFileType.Source],
                Checksums =
                [
                    new SpdxChecksum
                    {
                        Algorithm = SpdxChecksumAlgorithm.Sha1,
                        Value = "2fd4e1c67a2d28f123849ee1bb76e7391b93eb12"
                    }
                ],
                ConcludedLicense = "Apache-2.0",
                LicenseInfoInFiles = ["Apache-2.0"],
                LicenseComments = "This license is used by Jena",
                CopyrightText = "Copyright 2010, 2011 Source Auditor Inc.",
                Comment = "This file is a sample DOAP file",
                Notice = "Copyright (c) 2001 Aaron Lehmann aaroni@vitelus.com",
                Contributors =
                [
                    "Protecode Inc.",
                    "SPDX Technical Team Members",
                    "Open Logic Inc.",
                    "Source Auditor Inc.",
                    "Black Duck Software In.c"
                ],
                AttributionText =
                [
                    "All advertising materials mentioning features or use of this software must display the following acknowledgement: This product includes software developed by the AT&T."
                ],
                Annotations =
                [
                    new SpdxAnnotation
                    {
                        Date = "2011-01-29T18:30:22Z",
                        Type = SpdxAnnotationType.Other,
                        Comment = "Person: File Commenter",
                        Annotator = "File level annotation"
                    }
                ]
            }
        };

        // Act
        var json = Spdx2JsonSerializer.SerializeFiles(file);

        // Assert
        Assert.IsNotNull(json);
        Assert.AreEqual(1, json.Count);
        SpdxJsonHelpers.AssertEqual("SPDXRef-DoapSource", json[0]?["SPDXID"]);
        SpdxJsonHelpers.AssertEqual("./src/org/spdx/parser/DOAPProject.java", json[0]?["fileName"]);
        SpdxJsonHelpers.AssertEqual("SOURCE", json[0]?["fileTypes"]?[0]);
        SpdxJsonHelpers.AssertEqual("SHA1", json[0]?["checksums"]?[0]?["algorithm"]);
        SpdxJsonHelpers.AssertEqual("2fd4e1c67a2d28f123849ee1bb76e7391b93eb12",
            json[0]?["checksums"]?[0]?["checksumValue"]);
        SpdxJsonHelpers.AssertEqual("Apache-2.0", json[0]?["licenseConcluded"]);
        SpdxJsonHelpers.AssertEqual("Apache-2.0", json[0]?["licenseInfoInFiles"]?[0]);
        SpdxJsonHelpers.AssertEqual("This license is used by Jena", json[0]?["licenseComments"]);
        SpdxJsonHelpers.AssertEqual("Copyright 2010, 2011 Source Auditor Inc.", json[0]?["copyrightText"]);
        SpdxJsonHelpers.AssertEqual("This file is a sample DOAP file", json[0]?["comment"]);
        SpdxJsonHelpers.AssertEqual("Copyright (c) 2001 Aaron Lehmann aaroni@vitelus.com", json[0]?["noticeText"]);
        SpdxJsonHelpers.AssertEqual("Protecode Inc.", json[0]?["fileContributors"]?[0]);
        SpdxJsonHelpers.AssertEqual("SPDX Technical Team Members", json[0]?["fileContributors"]?[1]);
        SpdxJsonHelpers.AssertEqual("Open Logic Inc.", json[0]?["fileContributors"]?[2]);
        SpdxJsonHelpers.AssertEqual("Source Auditor Inc.", json[0]?["fileContributors"]?[3]);
        SpdxJsonHelpers.AssertEqual("Black Duck Software In.c", json[0]?["fileContributors"]?[4]);
        SpdxJsonHelpers.AssertEqual(
            "All advertising materials mentioning features or use of this software must display the following acknowledgement: This product includes software developed by the AT&T.",
            json[0]?["attributionTexts"]?[0]);
        SpdxJsonHelpers.AssertEqual("2011-01-29T18:30:22Z", json[0]?["annotations"]?[0]?["annotationDate"]);
        SpdxJsonHelpers.AssertEqual("OTHER", json[0]?["annotations"]?[0]?["annotationType"]);
        SpdxJsonHelpers.AssertEqual("Person: File Commenter", json[0]?["annotations"]?[0]?["comment"]);
        SpdxJsonHelpers.AssertEqual("File level annotation", json[0]?["annotations"]?[0]?["annotator"]);
    }
}
