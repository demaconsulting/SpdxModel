using DemaConsulting.SpdxModel.IO;

namespace DemaConsulting.SpdxModel.Tests.IO;

[TestClass]
public class SpdxJsonSerializeRelationship
{
    [TestMethod]
    public void SerializeRelationship()
    {
        // Arrange
        var relationship = new SpdxRelationship
        {
            Id = "SPDXRef-DOCUMENT",
            RelatedSpdxElement = "SPDXRef-Package",
            RelationshipType = SpdxRelationshipType.Describes,
            Comment = "Example Comment"
        };

        // Act
        var json = SpdxJsonSerializer.SerializeRelationship(relationship);

        // Assert
        Assert.IsNotNull(json);
        Assert.AreEqual("SPDXRef-DOCUMENT", json["spdxElementId"]?.ToString());
        Assert.AreEqual("SPDXRef-Package", json["relatedSpdxElement"]?.ToString());
        Assert.AreEqual("DESCRIBES", json["relationshipType"]?.ToString());
        Assert.AreEqual("Example Comment", json["comment"]?.ToString());
    }

    [TestMethod]
    public void SerializeRelationships()
    {
        // Arrange
        var relationships = new[]
        {
            new SpdxRelationship
            {
                Id = "SPDXRef-DOCUMENT",
                RelatedSpdxElement = "SPDXRef-Package",
                RelationshipType = SpdxRelationshipType.Describes,
                Comment = "Example Comment"
            },
            new SpdxRelationship
            {
                Id = "SPDXRef-Package",
                RelatedSpdxElement = "SPDXRef-DOCUMENT",
                RelationshipType = SpdxRelationshipType.DescribedBy,
                Comment = "Example Comment"
            }
        };

        // Act
        var json = SpdxJsonSerializer.SerializeRelationships(relationships);

        // Assert
        Assert.IsNotNull(json);
        Assert.AreEqual(2, json.Count);
        Assert.AreEqual("SPDXRef-DOCUMENT", json[0]?["spdxElementId"]?.ToString());
        Assert.AreEqual("SPDXRef-Package", json[0]?["relatedSpdxElement"]?.ToString());
        Assert.AreEqual("DESCRIBES", json[0]?["relationshipType"]?.ToString());
        Assert.AreEqual("Example Comment", json[0]?["comment"]?.ToString());
        Assert.AreEqual("SPDXRef-Package", json[1]?["spdxElementId"]?.ToString());
        Assert.AreEqual("SPDXRef-DOCUMENT", json[1]?["relatedSpdxElement"]?.ToString());
        Assert.AreEqual("DESCRIBED_BY", json[1]?["relationshipType"]?.ToString());
        Assert.AreEqual("Example Comment", json[1]?["comment"]?.ToString());
    }
}