namespace DemaConsulting.SpdxModel.Tests;

[TestClass]
public class SpdxAnnotationTests
{
    [TestMethod]
    public void AnnotationSameComparer()
    {
        var a1 = new SpdxAnnotation
        {
            Annotator = "Person: Malcolm Nixon",
            Date = "2024-05-28T01:30:00Z",
            Type = SpdxAnnotationType.Review,
            Comment = "Looks good"
        };

        var a2 = new SpdxAnnotation
        {
            Id = "SPDXRef-Annotation1",
            Annotator = "Person: Malcolm Nixon",
            Date = "2024-05-28T01:30:00Z",
            Type = SpdxAnnotationType.Review,
            Comment = "Looks good"
        };

        var a3 = new SpdxAnnotation
        {
            Annotator = "Person: John Doe",
            Date = "2023-11-20T12:34:23Z",
            Type = SpdxAnnotationType.Other
        };

        // Assert annotations compare to themselves
        Assert.IsTrue(SpdxAnnotation.Same.Equals(a1, a1));
        Assert.IsTrue(SpdxAnnotation.Same.Equals(a2, a2));
        Assert.IsTrue(SpdxAnnotation.Same.Equals(a3, a3));

        // Assert annotations compare correctly
        Assert.IsTrue(SpdxAnnotation.Same.Equals(a1, a2));
        Assert.IsTrue(SpdxAnnotation.Same.Equals(a2, a1));
        Assert.IsFalse(SpdxAnnotation.Same.Equals(a1, a3));
        Assert.IsFalse(SpdxAnnotation.Same.Equals(a3, a1));
        Assert.IsFalse(SpdxAnnotation.Same.Equals(a2, a3));
        Assert.IsFalse(SpdxAnnotation.Same.Equals(a3, a2));

        // Assert same annotations have identical hashes
        Assert.IsTrue(SpdxAnnotation.Same.GetHashCode(a1) == SpdxAnnotation.Same.GetHashCode(a2));
    }

    [TestMethod]
    public void DeepCopy()
    {
        var a1 = new SpdxAnnotation
        {
            Annotator = "Person: Malcolm Nixon",
            Date = "2024-05-28T01:30:00Z",
            Type = SpdxAnnotationType.Review,
            Comment = "Looks good"
        };

        var a2 = a1.DeepCopy();
        a2.Comment = "Looks bad";

        Assert.IsFalse(ReferenceEquals(a1, a2));
        Assert.AreEqual("Looks good", a1.Comment);
        Assert.AreEqual("Looks bad", a2.Comment);
    }
}