namespace DemaConsulting.SpdxModel.Tests;

[TestClass]
public class SpdxRelationshipTests
{
    [TestMethod]
    public void RelationshipSameComparer()
    {
        var r1 = new SpdxRelationship
        {
            Id = "SPDXRef-Package1",
            RelationshipType = SpdxRelationshipType.Contains,
            RelatedSpdxElement = "SPDXRef-Package2"
        };

        var r2 = new SpdxRelationship
        {
            Id = "SPDXRef-Package1",
            RelationshipType = SpdxRelationshipType.Contains,
            RelatedSpdxElement = "SPDXRef-Package2",
            Comment = "Package 1 contains Package 2"
        };

        var r3 = new SpdxRelationship
        {
            Id = "SPDXRef-Package3",
            RelationshipType = SpdxRelationshipType.DevToolOf,
            RelatedSpdxElement = "SPDXRef-Package4"
        };

        // Assert relationships compare to themselves
        Assert.IsTrue(SpdxRelationship.Same.Equals(r1, r1));
        Assert.IsTrue(SpdxRelationship.Same.Equals(r2, r2));
        Assert.IsTrue(SpdxRelationship.Same.Equals(r3, r3));

        // Assert relationships compare correctly
        Assert.IsTrue(SpdxRelationship.Same.Equals(r1, r2));
        Assert.IsTrue(SpdxRelationship.Same.Equals(r2, r1));
        Assert.IsFalse(SpdxRelationship.Same.Equals(r1, r3));
        Assert.IsFalse(SpdxRelationship.Same.Equals(r3, r1));
        Assert.IsFalse(SpdxRelationship.Same.Equals(r2, r3));
        Assert.IsFalse(SpdxRelationship.Same.Equals(r3, r2));

        // Assert same relationships have identical hashes
        Assert.IsTrue(SpdxRelationship.Same.GetHashCode(r1) == SpdxRelationship.Same.GetHashCode(r2));
    }

    [TestMethod]
    public void DeepCopy()
    {
        var r1 = new SpdxRelationship
        {
            Id = "SPDXRef-Package1",
            RelationshipType = SpdxRelationshipType.Contains,
            RelatedSpdxElement = "SPDXRef-Package2",
            Comment = "Package 1 contains Package 2"
        };

        var r2 = r1.DeepCopy();
        r2.Id = "SPDXRef-Package3";

        Assert.IsFalse(ReferenceEquals(r1, r2));
        Assert.AreEqual("SPDXRef-Package1", r1.Id);
        Assert.AreEqual("SPDXRef-Package3", r2.Id);
    }

    [TestMethod]
    public void Enhance()
    {
        var relationships = new[]
        {
            new SpdxRelationship
            {
                Id = "SPDXRef-Package1",
                RelationshipType = SpdxRelationshipType.Contains,
                RelatedSpdxElement = "SPDXRef-Package2"
            }
        };

        relationships = SpdxRelationship.Enhance(
            relationships,
            new[]
            {
                new SpdxRelationship
                {
                    Id = "SPDXRef-Package1",
                    RelationshipType = SpdxRelationshipType.Contains,
                    RelatedSpdxElement = "SPDXRef-Package2",
                    Comment = "Package 1 contains Package 2"
                },
                new SpdxRelationship
                {
                    Id = "SPDXRef-Package3",
                    RelationshipType = SpdxRelationshipType.DevToolOf,
                    RelatedSpdxElement = "SPDXRef-Package4"
                }
            });

        Assert.AreEqual(2, relationships.Length);
        Assert.AreEqual("SPDXRef-Package1", relationships[0].Id);
        Assert.AreEqual(SpdxRelationshipType.Contains, relationships[0].RelationshipType);
        Assert.AreEqual("SPDXRef-Package2", relationships[0].RelatedSpdxElement);
        Assert.AreEqual("Package 1 contains Package 2", relationships[0].Comment);
        Assert.AreEqual("SPDXRef-Package3", relationships[1].Id);
        Assert.AreEqual(SpdxRelationshipType.DevToolOf, relationships[1].RelationshipType);
        Assert.AreEqual("SPDXRef-Package4", relationships[1].RelatedSpdxElement);
    }
}