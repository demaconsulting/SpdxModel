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
    public void RelationshipSameElementsComparer()
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
            RelationshipType = SpdxRelationshipType.BuildToolOf,
            RelatedSpdxElement = "SPDXRef-Package2",
            Comment = "Package 1 builds Package 2"
        };

        var r3 = new SpdxRelationship
        {
            Id = "SPDXRef-Package3",
            RelationshipType = SpdxRelationshipType.DevToolOf,
            RelatedSpdxElement = "SPDXRef-Package4"
        };

        // Assert relationships compare to themselves
        Assert.IsTrue(SpdxRelationship.SameElements.Equals(r1, r1));
        Assert.IsTrue(SpdxRelationship.SameElements.Equals(r2, r2));
        Assert.IsTrue(SpdxRelationship.SameElements.Equals(r3, r3));

        // Assert relationships compare correctly
        Assert.IsTrue(SpdxRelationship.SameElements.Equals(r1, r2));
        Assert.IsTrue(SpdxRelationship.SameElements.Equals(r2, r1));
        Assert.IsFalse(SpdxRelationship.SameElements.Equals(r1, r3));
        Assert.IsFalse(SpdxRelationship.SameElements.Equals(r3, r1));
        Assert.IsFalse(SpdxRelationship.SameElements.Equals(r2, r3));
        Assert.IsFalse(SpdxRelationship.SameElements.Equals(r3, r2));

        // Assert same relationships have identical hashes
        Assert.IsTrue(SpdxRelationship.SameElements.GetHashCode(r1) == SpdxRelationship.SameElements.GetHashCode(r2));
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