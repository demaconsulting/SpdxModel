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
[TestClass]
public class SpdxFileTests
{
    /// <summary>
    ///     Tests the <see cref="SpdxFile.Same" /> comparer compares files correctly.
    /// </summary>
    [TestMethod]
    public void SpdxFile_SameComparer_ComparesCorrectly()
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

        // Assert: Verify same files have identical hashes
        Assert.AreEqual(SpdxFile.Same.GetHashCode(f1), SpdxFile.Same.GetHashCode(f2));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxFile.DeepCopy" /> method successfully creates a deep copy.
    /// </summary>
    [TestMethod]
    public void SpdxFile_DeepCopy_CreatesEqualButDistinctInstance()
    {
        // Arrange: Create an SpdxFile instance with checksums and comments
        var f1 = new SpdxFile
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

        // Act: Create a deep copy of the SpdxFile instance
        var f2 = f1.DeepCopy();

        // Assert: Verify deep-copy is equal to original
        Assert.AreEqual(f1, f2, SpdxFile.Same);
        Assert.AreEqual(f1.Id, f2.Id);
        Assert.AreEqual(f1.FileName, f2.FileName);
        CollectionAssert.AreEquivalent(f1.Checksums, f2.Checksums, SpdxChecksum.Same);
        Assert.AreEqual(f1.Comment, f2.Comment);

        // Assert: Verify deep-copy has distinct instances
        Assert.IsFalse(ReferenceEquals(f1, f2));
        Assert.IsFalse(ReferenceEquals(f1.Checksums, f2.Checksums));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxFile.Enhance(SpdxFile[], SpdxFile[])" /> method correctly adds or updates information
    /// </summary>
    [TestMethod]
    public void SpdxFile_Enhance_AddsOrUpdatesInformationCorrectly()
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
    [TestMethod]
    public void SpdxFile_Validate_ReportsInvalidFileId()
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
        Assert.IsTrue(
            issues.Any(issue => issue.Contains("File ./file1.txt Invalid SPDX Identifier Field")));
    }

    /// <summary>
    ///     Tests that a valid file passes validation.
    /// </summary>
    [TestMethod]
    public void SpdxFile_Validate_Success()
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
    [TestMethod]
    public void SpdxFileTypeExtensions_FromText_Valid()
    {
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
    [TestMethod]
    public void SpdxFileTypeExtensions_FromText_Invalid()
    {
        var exception =
            Assert.ThrowsExactly<InvalidOperationException>(() => SpdxFileTypeExtensions.FromText("invalid"));
        Assert.AreEqual("Unsupported SPDX File Type 'invalid'", exception.Message);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxFileTypeExtensions.ToText" /> method with valid inputs
    /// </summary>
    [TestMethod]
    public void SpdxFileTypeExtensions_ToText_Valid()
    {
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
    [TestMethod]
    public void SpdxFileTypeExtensions_ToText_Invalid()
    {
        var exception = Assert.ThrowsExactly<InvalidOperationException>(() => ((SpdxFileType)1000).ToText());
        Assert.AreEqual("Unsupported SPDX File Type '1000'", exception.Message);
    }
}