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
/// Tests for the <see cref="SpdxFile"/> class.
/// </summary>
[TestClass]
public class SpdxFileTests
{
    /// <summary>
    /// Tests the <see cref="SpdxFile.Same"/> comparer.
    /// </summary>
    [TestMethod]
    public void FileSameComparer()
    {
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

        // Assert files compare to themselves
        Assert.IsTrue(SpdxFile.Same.Equals(f1, f1));
        Assert.IsTrue(SpdxFile.Same.Equals(f2, f2));
        Assert.IsTrue(SpdxFile.Same.Equals(f3, f3));

        // Assert files compare correctly
        Assert.IsTrue(SpdxFile.Same.Equals(f1, f2));
        Assert.IsTrue(SpdxFile.Same.Equals(f2, f1));
        Assert.IsFalse(SpdxFile.Same.Equals(f1, f3));
        Assert.IsFalse(SpdxFile.Same.Equals(f3, f1));
        Assert.IsFalse(SpdxFile.Same.Equals(f2, f3));
        Assert.IsFalse(SpdxFile.Same.Equals(f3, f2));

        // Assert same files have identical hashes
        Assert.AreEqual(SpdxFile.Same.GetHashCode(f1), SpdxFile.Same.GetHashCode(f2));
    }

    /// <summary>
    /// Tests the <see cref="SpdxFile.DeepCopy"/> method.
    /// </summary>
    [TestMethod]
    public void DeepCopy()
    {
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

        var f2 = f1.DeepCopy();
        f2.Checksums[0].Value = "d6a770ba38583ed4bb4525bd96e50461655d2759";

        Assert.IsFalse(ReferenceEquals(f1, f2));
        Assert.AreEqual("85ed0817af83a24ad8da68c2b5094de69833983c", f1.Checksums[0].Value);
        Assert.AreEqual("d6a770ba38583ed4bb4525bd96e50461655d2759", f2.Checksums[0].Value);
    }

    /// <summary>
    /// Tests the <see cref="SpdxFile.Enhance(SpdxFile[], SpdxFile[])"/> method.
    /// </summary>
    [TestMethod]
    public void Enhance()
    {
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

        Assert.AreEqual(2, files.Length);
        Assert.AreEqual("SPDXRef-File1", files[0].Id);
        Assert.AreEqual("./file1.txt", files[0].FileName);
        Assert.AreEqual(2, files[0].Checksums.Length);
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, files[0].Checksums[0].Algorithm);
        Assert.AreEqual("85ed0817af83a24ad8da68c2b5094de69833983c", files[0].Checksums[0].Value);
        Assert.AreEqual(SpdxChecksumAlgorithm.Md5, files[0].Checksums[1].Algorithm);
        Assert.AreEqual("624c1abb3664f4b35547e7c73864ad24", files[0].Checksums[1].Value);
        Assert.AreEqual("File 1", files[0].Comment);
        Assert.AreEqual("./file2.txt", files[1].FileName);
        Assert.AreEqual(1, files[1].Checksums.Length);
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, files[1].Checksums[0].Algorithm);
        Assert.AreEqual("c2b4e1c67a2d28fced849ee1bb76e7391b93f125", files[1].Checksums[0].Value);
    }
}