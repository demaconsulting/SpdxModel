namespace DemaConsulting.SpdxModel.Tests;

[TestClass]
public class SpdxFileTests
{
    [TestMethod]
    public void FileSameComparer()
    {
        var f1 = new SpdxFile
        {
            FileName = "./file1.txt",
            Checksums = new[]
            {
                new SpdxChecksum
                {
                    Algorithm = SpdxChecksumAlgorithm.Sha1,
                    Value = "85ed0817af83a24ad8da68c2b5094de69833983c"
                }
            }
        };

        var f2 = new SpdxFile
        {
            Id = "SPDXRef-File1",
            FileName = "./file1.txt",
            Checksums = new[]
            {
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
            },
            Comment = "File 1"
        };

        var f3 = new SpdxFile
        {
            FileName = "./file2.txt",
            Checksums = new[]
            {
                new SpdxChecksum
                {
                    Algorithm = SpdxChecksumAlgorithm.Sha1,
                    Value = "c2b4e1c67a2d28fced849ee1bb76e7391b93f125"
                }
            }
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
        Assert.IsTrue(SpdxFile.Same.GetHashCode(f1) == SpdxFile.Same.GetHashCode(f2));
    }

    [TestMethod]
    public void DeepCopy()
    {
        var f1 = new SpdxFile
        {
            Id = "SPDXRef-File1",
            FileName = "./file1.txt",
            Checksums = new[]
            {
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
            },
            Comment = "File 1"
        };

        var f2 = f1.DeepCopy();
        f2.Checksums[0].Value = "d6a770ba38583ed4bb4525bd96e50461655d2759";

        Assert.IsFalse(ReferenceEquals(f1, f2));
        Assert.AreEqual("85ed0817af83a24ad8da68c2b5094de69833983c", f1.Checksums[0].Value);
        Assert.AreEqual("d6a770ba38583ed4bb4525bd96e50461655d2759", f2.Checksums[0].Value);
    }
}