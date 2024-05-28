namespace DemaConsulting.SpdxModel.Tests;

[TestClass]
public class SpdxSnippetTests
{
    [TestMethod]
    public void SnippetSameComparer()
    {
        var s1 = new SpdxSnippet
        {
            SnippetFromFile = "SPDXRef-File1",
            SnippetByteStart = 100,
            SnippetByteEnd = 200
        };

        var s2 = new SpdxSnippet
        {
            SnippetFromFile = "SPDXRef-File1",
            SnippetByteStart = 100,
            SnippetByteEnd = 200,
            Comment = "Found snippet",
            ConcludedLicense = "MIT"
        };

        var s3 = new SpdxSnippet
        {
            SnippetFromFile = "SPDXRef-File2",
            SnippetByteStart = 10,
            SnippetByteEnd = 40
        };

        // Assert snippets compare to themselves
        Assert.IsTrue(SpdxSnippet.Same.Equals(s1, s1));
        Assert.IsTrue(SpdxSnippet.Same.Equals(s2, s2));
        Assert.IsTrue(SpdxSnippet.Same.Equals(s3, s3));

        // Assert snippets compare correctly
        Assert.IsTrue(SpdxSnippet.Same.Equals(s1, s2));
        Assert.IsTrue(SpdxSnippet.Same.Equals(s2, s1));
        Assert.IsFalse(SpdxSnippet.Same.Equals(s1, s3));
        Assert.IsFalse(SpdxSnippet.Same.Equals(s3, s1));
        Assert.IsFalse(SpdxSnippet.Same.Equals(s2, s3));
        Assert.IsFalse(SpdxSnippet.Same.Equals(s3, s2));

        // Assert same snippets have identical hashes
        Assert.IsTrue(SpdxSnippet.Same.GetHashCode(s1) == SpdxSnippet.Same.GetHashCode(s2));
    }

    [TestMethod]
    public void DeepCopy()
    {
        var s1 = new SpdxSnippet
        {
            SnippetFromFile = "SPDXRef-File1",
            SnippetByteStart = 100,
            SnippetByteEnd = 200
        };

        var s2 = s1.DeepCopy();
        s2.SnippetFromFile = "SPDXRef-File2";

        Assert.IsFalse(ReferenceEquals(s1, s2));
        Assert.AreEqual("SPDXRef-File1", s1.SnippetFromFile);
        Assert.AreEqual("SPDXRef-File2", s2.SnippetFromFile);
    }
}