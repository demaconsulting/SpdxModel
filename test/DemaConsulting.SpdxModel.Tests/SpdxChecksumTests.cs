namespace DemaConsulting.SpdxModel.Tests;

[TestClass]
public class SpdxChecksumTests
{
    [TestMethod]
    public void ChecksumSameComparer()
    {
        var c1 = new SpdxChecksum
        {
            Algorithm = SpdxChecksumAlgorithm.Sha1,
            Value = "c2b4e1c67a2d28fced849ee1bb76e7391b93f125"
        };

        var c2 = new SpdxChecksum
        {
            Algorithm = SpdxChecksumAlgorithm.Sha1,
            Value = "c2b4e1c67a2d28fced849ee1bb76e7391b93f125"
        };

        var c3 = new SpdxChecksum
        {
            Algorithm = SpdxChecksumAlgorithm.Md5,
            Value = "624c1abb3664f4b35547e7c73864ad24"
        };

        // Assert checksums compare to themselves
        Assert.IsTrue(SpdxChecksum.Same.Equals(c1, c1));
        Assert.IsTrue(SpdxChecksum.Same.Equals(c2, c2));
        Assert.IsTrue(SpdxChecksum.Same.Equals(c3, c3));

        // Assert checksums compare correctly
        Assert.IsTrue(SpdxChecksum.Same.Equals(c1, c2));
        Assert.IsTrue(SpdxChecksum.Same.Equals(c2, c1));
        Assert.IsFalse(SpdxChecksum.Same.Equals(c1, c3));
        Assert.IsFalse(SpdxChecksum.Same.Equals(c3, c1));
        Assert.IsFalse(SpdxChecksum.Same.Equals(c2, c3));
        Assert.IsFalse(SpdxChecksum.Same.Equals(c3, c2));

        // Assert same checksums have identical hashes
        Assert.IsTrue(SpdxChecksum.Same.GetHashCode(c1) == SpdxChecksum.Same.GetHashCode(c2));
    }

    [TestMethod]
    public void DeepCopy()
    {
        var c1 = new SpdxChecksum
        {
            Algorithm = SpdxChecksumAlgorithm.Sha1,
            Value = "c2b4e1c67a2d28fced849ee1bb76e7391b93f125"
        };

        var c2 = c1.DeepCopy();
        c2.Value = "d6a770ba38583ed4bb4525bd96e50461655d2759";

        Assert.IsFalse(ReferenceEquals(c1, c2));
        Assert.AreEqual("c2b4e1c67a2d28fced849ee1bb76e7391b93f125", c1.Value);
        Assert.AreEqual("d6a770ba38583ed4bb4525bd96e50461655d2759", c2.Value);
    }
}