﻿using System.Text.Json.Nodes;
using DemaConsulting.SpdxModel.IO;

namespace DemaConsulting.SpdxModel.Tests.IO;

[TestClass]
public class Spdx2JsonDeserializeRelationship
{
    [TestMethod]
    public void DeserializeRelationship()
    {
        // Arrange
        var json = new JsonObject
        {
            ["spdxElementId"] = "SPDXRef-DOCUMENT",
            ["relatedSpdxElement"] = "SPDXRef-Package",
            ["relationshipType"] = "DESCRIBES",
            ["comment"] = "This is just an example"
        };

        // Act
        var relationship = Spdx2JsonDeserializer.DeserializeRelationship(json);

        // Assert
        Assert.AreEqual("SPDXRef-DOCUMENT", relationship.Id);
        Assert.AreEqual("SPDXRef-Package", relationship.RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.Describes, relationship.RelationshipType);
        Assert.AreEqual("This is just an example", relationship.Comment);
    }

    [TestMethod]
    public void DeserializeRelationships()
    {
        // Arrange
        var json = new JsonArray
        {
            new JsonObject
            {
                ["spdxElementId"] = "SPDXRef-DOCUMENT",
                ["relatedSpdxElement"] = "SPDXRef-Package",
                ["relationshipType"] = "DESCRIBES",
                ["comment"] = "This is just an example"
            },
            new JsonObject
            {
                ["spdxElementId"] = "SPDXRef-Package",
                ["relatedSpdxElement"] = "SPDXRef-DOCUMENT",
                ["relationshipType"] = "DESCRIBED_BY",
                ["comment"] = "This is just an example"
            }
        };

        // Act
        var relationships = Spdx2JsonDeserializer.DeserializeRelationships(json);

        // Assert
        Assert.AreEqual(2, relationships.Length);
        Assert.AreEqual("SPDXRef-DOCUMENT", relationships[0].Id);
        Assert.AreEqual("SPDXRef-Package", relationships[0].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.Describes, relationships[0].RelationshipType);
        Assert.AreEqual("This is just an example", relationships[0].Comment);
        Assert.AreEqual("SPDXRef-Package", relationships[1].Id);
        Assert.AreEqual("SPDXRef-DOCUMENT", relationships[1].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.DescribedBy, relationships[1].RelationshipType);
        Assert.AreEqual("This is just an example", relationships[1].Comment);
    }
}