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

namespace DemaConsulting.SpdxModel.Tests;

/// <summary>
///     Tests for the <see cref="SpdxFile" /> class.
/// </summary>
/// <remarks>
///     Covers the Same equality comparer, DeepCopy, Enhance merge, Validate, and the
///     SpdxFileType text-conversion extension methods (FromText/ToText).
/// </remarks>
[TestClass]
public class SpdxFileTests
{
    /// <summary>
    ///     Tests the <see cref="SpdxFile.Same" /> comparer compares files correctly.
    /// </summary>
    /// <remarks>
    ///     Verifies that two files with the same FileName and compatible SHA1 checksums are
    ///     considered equal, that differing SHA1 checksums or file names produce inequality,
    ///     and that equal files produce identical hash codes.
    /// </remarks>
    [TestMethod]
    public void SpdxFile_SameComparer_MatchingAndDistinctFiles_ComparesCorrectly()
    {
        // Arrange: Create several SpdxFile instances with different IDs, names, and checksums
        var f1 = new SpdxFile
        {
            FileName = "./file1.txt",
            Checksums =
            [
                new SpdxChecksum
                {
                    Algorithm = SpdxChecksumAlgorithm.Sha1,
                    Value = "85ed0817af83a24ad8da68c2b5094de69833983c"
                }
            ]
        };
        var f2 = new SpdxFile
        {
            Id = "SPDXRef-File1",
            FileName = "./file1.txt",
            Checksums =
            [
                new SpdxChecksum
                {
                    Algorithm = SpdxChecksumAlgorithm.Sha1,
                    Value = "85ed0817af83a24ad8da68c2b5094de69833983c"
                },
                new SpdxChecksum
                {
                    Algorithm = SpdxChecksumAlgorithm.Md5,
                    Value = "624c1abb3664f4b35547e7c73864ad24"
                }
            ],
            Comment = "File 1"
        };
        var f3 = new SpdxFile
        {
            FileName = "./file2.txt",
            Checksums =
            [
                new SpdxChecksum
                {
                    Algorithm = SpdxChecksumAlgorithm.Sha1,
                    Value = "c2b4e1c67a2d28fced849ee1bb76e7391b93f125"
                }
            ]
        };
        var f4 = new SpdxFile
        {
            FileName = "./file1.txt"
            // no checksums — should still match f1/f2 by FileName
        };

        // Assert: Verify files compare to themselves
        Assert.IsTrue(SpdxFile.Same.Equals(f1, f1));
        Assert.IsTrue(SpdxFile.Same.Equals(f2, f2));
        Assert.IsTrue(SpdxFile.Same.Equals(f3, f3));

        // Assert: Verify files compare correctly
        Assert.IsTrue(SpdxFile.Same.Equals(f1, f2));
        Assert.IsTrue(SpdxFile.Same.Equals(f2, f1));
        Assert.IsFalse(SpdxFile.Same.Equals(f1, f3));
        Assert.IsFalse(SpdxFile.Same.Equals(f3, f1));
        Assert.IsFalse(SpdxFile.Same.Equals(f2, f3));
        Assert.IsFalse(SpdxFile.Same.Equals(f3, f2));

        // Assert: Verify one-sided SHA1 boundary — same FileName, one has SHA1, other does not
        Assert.IsTrue(SpdxFile.Same.Equals(f1, f4));
        Assert.IsTrue(SpdxFile.Same.Equals(f4, f1));

        // Assert: Verify same files have identical hashes
        Assert.AreEqual(SpdxFile.Same.GetHashCode(f1), SpdxFile.Same.GetHashCode(f2));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxFile.DeepCopy" /> method successfully creates a deep copy.
    /// </summary>
    /// <remarks>
    ///     Verifies that the copy has equal field values to the original and that all array
    ///     fields are independently copied with no shared references between original and copy.
    /// </remarks>
    [TestMethod]
    public void SpdxFile_DeepCopy_FullyPopulatedFile_CreatesEqualButDistinctCopy()
    {
        // Arrange: Create an SpdxFile instance with all deep-copied fields populated
        var f1 = new SpdxFile
        {
            Id = "SPDXRef-File1",
            FileName = "./file1.txt",
            FileTypes = [SpdxFileType.Source, SpdxFileType.Text],
            Checksums =
            [
                new SpdxChecksum
                {
                    Algorithm = SpdxChecksumAlgorithm.Sha1,
                    Value = "85ed0817af83a24ad8da68c2b5094de69833983c"
                },
                new SpdxChecksum
                {
                    Algorithm = SpdxChecksumAlgorithm.Md5,
                    Value = "624c1abb3664f4b35547e7c73864ad24"
                }
            ],
            LicenseInfoInFiles = ["MIT"],
            LicenseComments = "No issues",
            ConcludedLicense = "MIT",
            CopyrightText = "Copyright 2024",
            Comment = "File 1",
            Notice = "See LICENSE",
            Contributors = ["Contributor A"],
            AttributionText = ["Attribution notice"],
            Annotations =
            [
                new SpdxAnnotation
                {
                    Annotator = "Tool: test",
                    Date = "2024-01-01T00:00:00Z",
                    Type = SpdxAnnotationType.Review,
                    Comment = "Reviewed"
                }
            ]
        };

        // Act: Create a deep copy of the SpdxFile instance
        var f2 = f1.DeepCopy();

        // Assert: Verify deep-copy is equal to original
        Assert.AreEqual(f1, f2, SpdxFile.Same);
        Assert.AreEqual(f1.Id, f2.Id);
        Assert.AreEqual(f1.FileName, f2.FileName);
        CollectionAssert.AreEquivalent(f1.FileTypes, f2.FileTypes);
        CollectionAssert.AreEquivalent(f1.Checksums, f2.Checksums, SpdxChecksum.Same);
        CollectionAssert.AreEquivalent(f1.LicenseInfoInFiles, f2.LicenseInfoInFiles);
        Assert.AreEqual(f1.LicenseComments, f2.LicenseComments);
        Assert.AreEqual(f1.ConcludedLicense, f2.ConcludedLicense);
        Assert.AreEqual(f1.CopyrightText, f2.CopyrightText);
        Assert.AreEqual(f1.Comment, f2.Comment);
        Assert.AreEqual(f1.Notice, f2.Notice);
        CollectionAssert.AreEquivalent(f1.Contributors, f2.Contributors);
        CollectionAssert.AreEquivalent(f1.AttributionText, f2.AttributionText);

        // Assert: Verify deep-copy has distinct instances
        Assert.IsFalse(ReferenceEquals(f1, f2));
        Assert.IsFalse(ReferenceEquals(f1.Checksums, f2.Checksums));
        Assert.IsFalse(ReferenceEquals(f1.FileTypes, f2.FileTypes));
        Assert.IsFalse(ReferenceEquals(f1.LicenseInfoInFiles, f2.LicenseInfoInFiles));
        Assert.IsFalse(ReferenceEquals(f1.Contributors, f2.Contributors));
        Assert.IsFalse(ReferenceEquals(f1.AttributionText, f2.AttributionText));
        Assert.IsFalse(ReferenceEquals(f1.Annotations, f2.Annotations));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxFile.Enhance(SpdxFile[], SpdxFile[])" /> method correctly adds or updates information
    /// </summary>
    /// <remarks>
    ///     Verifies that matching entries are enhanced in place and unmatched entries from the
    ///     source array are appended as new independent copies.
    /// </remarks>
    [TestMethod]
    public void SpdxFile_Enhance_MatchingAndNewFiles_MergesCorrectly()
    {
        // Arrange: Create an array of SpdxFile objects with one file
        var files = new[]
        {
            new SpdxFile
            {
                FileName = "./file1.txt",
                Checksums =
                [
                    new SpdxChecksum
                    {
                        Algorithm = SpdxChecksumAlgorithm.Sha1,
                        Value = "85ed0817af83a24ad8da68c2b5094de69833983c"
                    }
                ]
            }
        };

        // Act: Enhance the files with additional information
        files = SpdxFile.Enhance(
            files,
            [
                new SpdxFile
                {
                    Id = "SPDXRef-File1",
                    FileName = "./file1.txt",
                    Checksums =
                    [
                        new SpdxChecksum
                        {
                            Algorithm = SpdxChecksumAlgorithm.Sha1,
                            Value = "85ed0817af83a24ad8da68c2b5094de69833983c"
                        },
                        new SpdxChecksum
                        {
                            Algorithm = SpdxChecksumAlgorithm.Md5,
                            Value = "624c1abb3664f4b35547e7c73864ad24"
                        }
                    ],
                    Comment = "File 1"
                },
                new SpdxFile
                {
                    FileName = "./file2.txt",
                    Checksums =
                    [
                        new SpdxChecksum
                        {
                            Algorithm = SpdxChecksumAlgorithm.Sha1,
                            Value = "c2b4e1c67a2d28fced849ee1bb76e7391b93f125"
                        }
                    ]
                }
            ]);

        // Assert: Verify the files array has been enhanced correctly
        Assert.HasCount(2, files);
        Assert.AreEqual("SPDXRef-File1", files[0].Id);
        Assert.AreEqual("./file1.txt", files[0].FileName);
        Assert.HasCount(2, files[0].Checksums);
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, files[0].Checksums[0].Algorithm);
        Assert.AreEqual("85ed0817af83a24ad8da68c2b5094de69833983c", files[0].Checksums[0].Value);
        Assert.AreEqual(SpdxChecksumAlgorithm.Md5, files[0].Checksums[1].Algorithm);
        Assert.AreEqual("624c1abb3664f4b35547e7c73864ad24", files[0].Checksums[1].Value);
        Assert.AreEqual("File 1", files[0].Comment);
        Assert.AreEqual("./file2.txt", files[1].FileName);
        Assert.HasCount(1, files[1].Checksums);
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, files[1].Checksums[0].Algorithm);
        Assert.AreEqual("c2b4e1c67a2d28fced849ee1bb76e7391b93f125", files[1].Checksums[0].Value);
    }

    /// <summary>
    ///     Tests that an invalid file ID fails validation.
    /// </summary>
    /// <remarks>
    ///     Verifies that Validate appends an issue message when the SPDX-ID does not conform
    ///     to the required SPDXRef- prefix format.
    /// </remarks>
    [TestMethod]
    public void SpdxFile_Validate_InvalidFileId_ReportsIssue()
    {
        // Arrange: Create an SpdxFile instance with an invalid ID format
        var spdxFile = new SpdxFile
        {
            Id = "Invalid_ID",
            FileName = "./file1.txt",
            Checksums =
            [
                new SpdxChecksum
                {
                    Algorithm = SpdxChecksumAlgorithm.Sha1,
                    Value = "85ed0817af83a24ad8da68c2b5094de69833983c"
                }
            ]
        };

        // Act: Perform validation on the SpdxFile instance.
        var issues = new List<string>();
        spdxFile.Validate(issues);

        // Assert: Verify that the validation fails and the error message includes the invalid ID.
        Assert.Contains(issue => issue.Contains("File './file1.txt' Invalid SPDX Identifier Field"), issues);
    }

    /// <summary>
    ///     Tests that an invalid file name fails validation.
    /// </summary>
    /// <remarks>
    ///     Verifies that Validate appends an issue message when FileName does not start with
    ///     the required "./" prefix.
    /// </remarks>
    [TestMethod]
    public void SpdxFile_Validate_InvalidFileName_ReportsIssue()
    {
        // Arrange: Create an SpdxFile instance with a FileName that has no "./" prefix
        var spdxFile = new SpdxFile
        {
            Id = "SPDXRef-File1",
            FileName = "file1.txt",
            Checksums =
            [
                new SpdxChecksum
                {
                    Algorithm = SpdxChecksumAlgorithm.Sha1,
                    Value = "85ed0817af83a24ad8da68c2b5094de69833983c"
                }
            ]
        };

        // Act: Perform validation on the SpdxFile instance.
        var issues = new List<string>();
        spdxFile.Validate(issues);

        // Assert: Verify that the validation reports the invalid file name.
        Assert.Contains(issue => issue.Contains("Invalid File Name Field"), issues);
    }

    /// <summary>
    ///     Tests that a missing SHA1 checksum fails validation.
    /// </summary>
    /// <remarks>
    ///     Verifies that Validate appends an issue message when no SHA1 checksum is present
    ///     in the Checksums array.
    /// </remarks>
    [TestMethod]
    public void SpdxFile_Validate_MissingSha1Checksum_ReportsIssue()
    {
        // Arrange: Create an SpdxFile instance with only an MD5 checksum (no SHA1)
        var spdxFile = new SpdxFile
        {
            Id = "SPDXRef-File1",
            FileName = "./file1.txt",
            Checksums =
            [
                new SpdxChecksum
                {
                    Algorithm = SpdxChecksumAlgorithm.Md5,
                    Value = "624c1abb3664f4b35547e7c73864ad24"
                }
            ]
        };

        // Act: Perform validation on the SpdxFile instance.
        var issues = new List<string>();
        spdxFile.Validate(issues);

        // Assert: Verify that the validation reports the missing SHA1.
        Assert.Contains(issue => issue.Contains("missing SHA1"), issues);
    }

    /// <summary>
    ///     Tests that a valid file passes validation.
    /// </summary>
    /// <remarks>
    ///     Verifies that a fully populated valid SpdxFile passes all validation checks
    ///     without reporting any issues.
    /// </remarks>
    [TestMethod]
    public void SpdxFile_Validate_ValidFile_ReportsNoIssues()
    {
        // Arrange: Create a valid SpdxFile instance
        var spdxFile = new SpdxFile
        {
            Id = "SPDXRef-File--file1.txt-85ed0817af83a24ad8da68c2b5094de69833983c",
            FileName = "./file1.txt",
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
                    Value = "2e5c078f9471ccf53c786b3949b34dbb9d2937da0752317f79693879ddf34d5b"
                }
            ]
        };

        // Act: Perform validation on the SpdxFile instance.
        var issues = new List<string>();
        spdxFile.Validate(issues);

        // Assert: Verify that the validation reports no issues.
        Assert.IsEmpty(issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxFileTypeExtensions.FromText(string)" /> method with valid inputs.
    /// </summary>
    /// <remarks>
    ///     Verifies that all recognized file type strings, including case variants, map to the
    ///     expected enum values.
    /// </remarks>
    [TestMethod]
    public void SpdxFileTypeExtensions_FromText_ValidInput_ParsesCorrectly()
    {
        // Arrange: (no external state needed)

        // Act / Assert: Verify all recognized file type strings map to expected enum values
        Assert.AreEqual(SpdxFileType.Source, SpdxFileTypeExtensions.FromText("SOURCE"));
        Assert.AreEqual(SpdxFileType.Source, SpdxFileTypeExtensions.FromText("source"));
        Assert.AreEqual(SpdxFileType.Source, SpdxFileTypeExtensions.FromText("Source"));
        Assert.AreEqual(SpdxFileType.Binary, SpdxFileTypeExtensions.FromText("BINARY"));
        Assert.AreEqual(SpdxFileType.Archive, SpdxFileTypeExtensions.FromText("ARCHIVE"));
        Assert.AreEqual(SpdxFileType.Application, SpdxFileTypeExtensions.FromText("APPLICATION"));
        Assert.AreEqual(SpdxFileType.Audio, SpdxFileTypeExtensions.FromText("AUDIO"));
        Assert.AreEqual(SpdxFileType.Image, SpdxFileTypeExtensions.FromText("IMAGE"));
        Assert.AreEqual(SpdxFileType.Text, SpdxFileTypeExtensions.FromText("TEXT"));
        Assert.AreEqual(SpdxFileType.Video, SpdxFileTypeExtensions.FromText("VIDEO"));
        Assert.AreEqual(SpdxFileType.Documentation, SpdxFileTypeExtensions.FromText("DOCUMENTATION"));
        Assert.AreEqual(SpdxFileType.Spdx, SpdxFileTypeExtensions.FromText("SPDX"));
        Assert.AreEqual(SpdxFileType.Other, SpdxFileTypeExtensions.FromText("OTHER"));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxFileTypeExtensions.FromText(string)" /> method with invalid input.
    /// </summary>
    /// <remarks>
    ///     Verifies that FromText throws <see cref="InvalidOperationException"/> with a message
    ///     identifying the unsupported value when given an unrecognized file type string.
    /// </remarks>
    [TestMethod]
    public void SpdxFileTypeExtensions_FromText_InvalidInput_ThrowsException()
    {
        // Arrange: An unrecognized file type string

        // Act / Assert: Verify that FromText throws with a message identifying the unsupported value
        var exception =
            Assert.ThrowsExactly<InvalidOperationException>(() => SpdxFileTypeExtensions.FromText("invalid"));
        Assert.AreEqual("Unsupported SPDX File Type 'invalid'", exception.Message);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxFileTypeExtensions.ToText" /> method with valid inputs
    /// </summary>
    /// <remarks>
    ///     Verifies that all known file type enum values map to their expected SPDX text
    ///     representations.
    /// </remarks>
    [TestMethod]
    public void SpdxFileTypeExtensions_ToText_ValidEnum_FormatsCorrectly()
    {
        // Arrange: (no external state needed)

        // Act / Assert: Verify all known enum values map to expected text representations
        Assert.AreEqual("SOURCE", SpdxFileType.Source.ToText());
        Assert.AreEqual("BINARY", SpdxFileType.Binary.ToText());
        Assert.AreEqual("ARCHIVE", SpdxFileType.Archive.ToText());
        Assert.AreEqual("APPLICATION", SpdxFileType.Application.ToText());
        Assert.AreEqual("AUDIO", SpdxFileType.Audio.ToText());
        Assert.AreEqual("IMAGE", SpdxFileType.Image.ToText());
        Assert.AreEqual("TEXT", SpdxFileType.Text.ToText());
        Assert.AreEqual("VIDEO", SpdxFileType.Video.ToText());
        Assert.AreEqual("DOCUMENTATION", SpdxFileType.Documentation.ToText());
        Assert.AreEqual("SPDX", SpdxFileType.Spdx.ToText());
        Assert.AreEqual("OTHER", SpdxFileType.Other.ToText());
    }

    /// <summary>
    ///     Tests the <see cref="SpdxFileTypeExtensions.ToText" /> method with invalid input.
    /// </summary>
    /// <remarks>
    ///     Verifies that ToText throws <see cref="InvalidOperationException"/> when given an
    ///     unsupported file type enum value.
    /// </remarks>
    [TestMethod]
    public void SpdxFileTypeExtensions_ToText_InvalidEnum_ThrowsException()
    {
        // Arrange: An unsupported file type enum value

        // Act / Assert: Verify that ToText throws when given an unsupported enum value
        var exception = Assert.ThrowsExactly<InvalidOperationException>(() => ((SpdxFileType)1000).ToText());
        Assert.AreEqual("Unsupported SPDX File Type '1000'", exception.Message);
    }
}
