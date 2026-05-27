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

using System.Text.Json.Nodes;
using DemaConsulting.SpdxModel.IO;

namespace DemaConsulting.SpdxModel.Tests.IO;

/// <summary>
///     Tests for deserializing SPDX packages to <see cref="SpdxPackage" /> classes.
/// </summary>
/// <remarks>
///     Exercises deserialization of SPDX package elements using xUnit v3 as the test
///     framework. Each test constructs inline JSON and verifies
///     the resulting <see cref="SpdxPackage"/> fields.
/// </remarks>
public class Spdx2JsonDeserializePackage
{
    /// <summary>
    ///     Tests deserializing a package.
    /// </summary>
    /// <remarks>
    ///     Verifies that all standard package fields (SPDXID, annotations, attributionTexts,
    ///     builtDate, checksums, copyrightText, description, downloadLocation, externalRefs)
    ///     are correctly mapped to <see cref="SpdxPackage"/> properties when a single package
    ///     JSON object is deserialized.
    /// </remarks>
    [Fact]
    public void Spdx2JsonDeserializer_DeserializePackage_ValidInput_CorrectResults()
    {
        // Arrange: Create a JSON object representing a package
        var json = new JsonObject
        {
            ["SPDXID"] = "SPDXRef-Package",
            ["annotations"] = new JsonArray
            {
                new JsonObject
                {
                    ["annotationDate"] = "2011-01-29T18:30:22Z",
                    ["annotationType"] = "OTHER",
                    ["annotator"] = "Person: Package Commenter",
                    ["comment"] = "Package level annotation"
                }
            },
            ["attributionTexts"] = new JsonArray
            {
                "The GNU C Library is free software.  See the file COPYING.LIB for copying conditions, and LICENSES for notices about a few contributions that require these additional notices to be distributed.  License copyright years may be listed using range notation, e.g., 1996-2015, indicating that every year in the range, inclusive, is a copyrightable year that would otherwise be listed individually."
            },
            ["builtDate"] = "2011-01-29T18:30:22Z",
            ["checksums"] = new JsonArray
            {
                new JsonObject
                {
                    ["algorithm"] = "MD5",
                    ["checksumValue"] = "624c1abb3664f4b35547e7c73864ad24"
                },
                new JsonObject
                {
                    ["algorithm"] = "SHA1",
                    ["checksumValue"] = "85ed0817af83a24ad8da68c2b5094de69833983c"
                },
                new JsonObject
                {
                    ["algorithm"] = "SHA256",
                    ["checksumValue"] = "11b6d3ee554eedf79299905a98f9b9a04e498210b59f15094c916c91d150efcd"
                }
            },
            ["copyrightText"] = "Copyright 2008-2010 John Smith",
            ["description"] =
                "The GNU C Library defines functions that are specified by the ISO C standard, as well as additional features specific to POSIX and other derivatives of the Unix operating system, and extensions specific to GNU systems.",
            ["downloadLocation"] = "http://ftp.gnu.org/gnu/glibc/glibc-ports-2.15.tar.gz",
            ["externalRefs"] = new JsonArray
            {
                new JsonObject
                {
                    ["referenceCategory"] = "SECURITY",
                    ["referenceLocator"] = "cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*",
                    ["referenceType"] = "cpe23Type"
                }
            }
        };

        // Act: Deserialize the JSON object to an SpdxPackage object
        var package = Spdx2JsonDeserializer.DeserializePackage(json);

        // Assert: Verify the deserialized object has the expected properties
        Assert.Equal("SPDXRef-Package", package.Id);
        Assert.Single(package.Annotations);
        Assert.Equal("2011-01-29T18:30:22Z", package.Annotations[0].Date);
        Assert.Equal(SpdxAnnotationType.Other, package.Annotations[0].Type);
        Assert.Equal("Person: Package Commenter", package.Annotations[0].Annotator);
        Assert.Equal("Package level annotation", package.Annotations[0].Comment);
        Assert.Single(package.AttributionText);
        Assert.Equal(
            "The GNU C Library is free software.  See the file COPYING.LIB for copying conditions, and LICENSES for notices about a few contributions that require these additional notices to be distributed.  License copyright years may be listed using range notation, e.g., 1996-2015, indicating that every year in the range, inclusive, is a copyrightable year that would otherwise be listed individually.",
            package.AttributionText[0]);
        Assert.Equal("2011-01-29T18:30:22Z", package.BuiltDate);
        Assert.Equal(3, package.Checksums.Length);
        Assert.Equal(SpdxChecksumAlgorithm.Md5, package.Checksums[0].Algorithm);
        Assert.Equal("624c1abb3664f4b35547e7c73864ad24", package.Checksums[0].Value);
        Assert.Equal(SpdxChecksumAlgorithm.Sha1, package.Checksums[1].Algorithm);
        Assert.Equal("85ed0817af83a24ad8da68c2b5094de69833983c", package.Checksums[1].Value);
        Assert.Equal(SpdxChecksumAlgorithm.Sha256, package.Checksums[2].Algorithm);
        Assert.Equal("11b6d3ee554eedf79299905a98f9b9a04e498210b59f15094c916c91d150efcd", package.Checksums[2].Value);
        Assert.Equal("Copyright 2008-2010 John Smith", package.CopyrightText);
        Assert.Equal(
            "The GNU C Library defines functions that are specified by the ISO C standard, as well as additional features specific to POSIX and other derivatives of the Unix operating system, and extensions specific to GNU systems.",
            package.Description);
        Assert.Equal("http://ftp.gnu.org/gnu/glibc/glibc-ports-2.15.tar.gz", package.DownloadLocation);
        Assert.Single(package.ExternalReferences);
        Assert.Equal(SpdxReferenceCategory.Security, package.ExternalReferences[0].Category);
        Assert.Equal("cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*",
            package.ExternalReferences[0].Locator);
        Assert.Equal("cpe23Type", package.ExternalReferences[0].Type);
    }

    /// <summary>
    ///     Tests deserializing multiple packages.
    /// </summary>
    /// <remarks>
    ///     Verifies that a JSON array containing one package object is deserialized to a
    ///     single-element array with all fields correctly populated.
    /// </remarks>
    [Fact]
    public void Spdx2JsonDeserializer_DeserializePackages_ValidInput_CorrectResults()
    {
        // Arrange: Create a JSON array representing multiple packages
        var json = new JsonArray
        {
            new JsonObject
            {
                ["SPDXID"] = "SPDXRef-Package",
                ["annotations"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["annotationDate"] = "2011-01-29T18:30:22Z",
                        ["annotationType"] = "OTHER",
                        ["annotator"] = "Person: Package Commenter",
                        ["comment"] = "Package level annotation"
                    }
                },
                ["attributionTexts"] = new JsonArray
                {
                    "The GNU C Library is free software.  See the file COPYING.LIB for copying conditions, and LICENSES for notices about a few contributions that require these additional notices to be distributed.  License copyright years may be listed using range notation, e.g., 1996-2015, indicating that every year in the range, inclusive, is a copyrightable year that would otherwise be listed individually."
                },
                ["builtDate"] = "2011-01-29T18:30:22Z",
                ["checksums"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["algorithm"] = "MD5",
                        ["checksumValue"] = "624c1abb3664f4b35547e7c73864ad24"
                    },
                    new JsonObject
                    {
                        ["algorithm"] = "SHA1",
                        ["checksumValue"] = "85ed0817af83a24ad8da68c2b5094de69833983c"
                    },
                    new JsonObject
                    {
                        ["algorithm"] = "SHA256",
                        ["checksumValue"] = "11b6d3ee554eedf79299905a98f9b9a04e498210b59f15094c916c91d150efcd"
                    }
                },
                ["copyrightText"] = "Copyright 2008-2010 John Smith",
                ["description"] =
                    "The GNU C Library defines functions that are specified by the ISO C standard, as well as additional features specific to POSIX and other derivatives of the Unix operating system, and extensions specific to GNU systems.",
                ["downloadLocation"] = "http://ftp.gnu.org/gnu/glibc/glibc-ports-2.15.tar.gz",
                ["externalRefs"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["referenceCategory"] = "SECURITY",
                        ["referenceLocator"] = "cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*",
                        ["referenceType"] = "cpe23Type"
                    }
                }
            }
        };

        // Act: Deserialize the JSON array to an array of SpdxPackage objects
        var packages = Spdx2JsonDeserializer.DeserializePackages(json);

        // Assert: Verify the deserialized array has the expected number of packages and their properties
        Assert.Single(packages);
        Assert.Equal("SPDXRef-Package", packages[0].Id);
        Assert.Single(packages[0].Annotations);
        Assert.Equal("2011-01-29T18:30:22Z", packages[0].Annotations[0].Date);
        Assert.Equal(SpdxAnnotationType.Other, packages[0].Annotations[0].Type);
        Assert.Equal("Person: Package Commenter", packages[0].Annotations[0].Annotator);
        Assert.Equal("Package level annotation", packages[0].Annotations[0].Comment);
        Assert.Single(packages[0].AttributionText);
        Assert.Equal(
            "The GNU C Library is free software.  See the file COPYING.LIB for copying conditions, and LICENSES for notices about a few contributions that require these additional notices to be distributed.  License copyright years may be listed using range notation, e.g., 1996-2015, indicating that every year in the range, inclusive, is a copyrightable year that would otherwise be listed individually.",
            packages[0].AttributionText[0]);
        Assert.Equal("2011-01-29T18:30:22Z", packages[0].BuiltDate);
        Assert.Equal(3, packages[0].Checksums.Length);
        Assert.Equal(SpdxChecksumAlgorithm.Md5, packages[0].Checksums[0].Algorithm);
        Assert.Equal("624c1abb3664f4b35547e7c73864ad24", packages[0].Checksums[0].Value);
        Assert.Equal(SpdxChecksumAlgorithm.Sha1, packages[0].Checksums[1].Algorithm);
        Assert.Equal("85ed0817af83a24ad8da68c2b5094de69833983c", packages[0].Checksums[1].Value);
        Assert.Equal(SpdxChecksumAlgorithm.Sha256, packages[0].Checksums[2].Algorithm);
        Assert.Equal("11b6d3ee554eedf79299905a98f9b9a04e498210b59f15094c916c91d150efcd",
            packages[0].Checksums[2].Value);
        Assert.Equal("Copyright 2008-2010 John Smith", packages[0].CopyrightText);
        Assert.Equal(
            "The GNU C Library defines functions that are specified by the ISO C standard, as well as additional features specific to POSIX and other derivatives of the Unix operating system, and extensions specific to GNU systems.",
            packages[0].Description);
        Assert.Equal("http://ftp.gnu.org/gnu/glibc/glibc-ports-2.15.tar.gz", packages[0].DownloadLocation);
        Assert.Single(packages[0].ExternalReferences);
        Assert.Equal(SpdxReferenceCategory.Security, packages[0].ExternalReferences[0].Category);
        Assert.Equal("cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*",
            packages[0].ExternalReferences[0].Locator);
        Assert.Equal("cpe23Type", packages[0].ExternalReferences[0].Type);
    }
}
