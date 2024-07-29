using DemaConsulting.SpdxModel.IO;
using DemaConsulting.SpdxModel.Transform;

namespace DemaConsulting.SpdxModel.Tests.Transforms;

[TestClass]
public class SpdxRelationshipsTests
{
    /// <summary>
    /// Test SPDX document for relationships.
    /// </summary>
    private const string Contents = "{\r\n" +
                                    "  \"files\": [],\r\n" +
                                    "  \"packages\": [" +
                                    "    {\r\n" +
                                    "      \"SPDXID\": \"SPDXRef-Package-1\",\r\n" +
                                    "      \"name\": \"Test Package\",\r\n" +
                                    "      \"versionInfo\": \"1.0.0\",\r\n" +
                                    "      \"packageFileName\": \"package1.zip\",\r\n" +
                                    "      \"downloadLocation\": \"https://github.com/demaconsulting/SpdxTool\",\r\n" +
                                    "      \"licenseConcluded\": \"MIT\"\r\n" +
                                    "    },\r\n" +
                                    "    {\r\n" +
                                    "      \"SPDXID\": \"SPDXRef-Package-2\",\r\n" +
                                    "      \"name\": \"Another Test Package\",\r\n" +
                                    "      \"versionInfo\": \"2.0.0\",\r\n" +
                                    "      \"packageFileName\": \"package2.tar\",\r\n" +
                                    "      \"downloadLocation\": \"https://github.com/demaconsulting/SpdxModel\",\r\n" +
                                    "      \"licenseConcluded\": \"MIT\"\r\n" +
                                    "    }\r\n" +
                                    "  ],\r\n" +
                                    "  \"spdxVersion\": \"SPDX-2.2\",\r\n" +
                                    "  \"dataLicense\": \"CC0-1.0\",\r\n" +
                                    "  \"SPDXID\": \"SPDXRef-DOCUMENT\",\r\n" +
                                    "  \"name\": \"Test Document\",\r\n" +
                                    "  \"documentNamespace\": \"https://sbom.spdx.org\",\r\n" +
                                    "  \"creationInfo\": {\r\n" +
                                    "    \"created\": \"2021-10-01T00:00:00Z\",\r\n" +
                                    "    \"creators\": [ \"Person: Malcolm Nixon\" ]\r\n" +
                                    "  },\r\n" +
                                    "  \"documentDescribes\": [ \"SPDXRef-Package-1\" ]\r\n" +
                                    "}";

    [TestMethod]
    public void AddRelationshipBadId()
    {
        // Arrange
        var document = Spdx2JsonDeserializer.Deserialize(Contents);

        // Act
        Assert.ThrowsException<ArgumentException>(() =>
        {
            SpdxRelationships.Add(
                document, 
                new SpdxRelationship
                {
                    Id = "SPDXRef-Package-Missing",
                    RelatedSpdxElement = "SPDXRef-Package-2",
                    RelationshipType = SpdxRelationshipType.DependsOn
                });
        });

        // Assert
        Assert.AreEqual(0, document.Relationships.Length);
    }

    [TestMethod]
    public void AddRelationshipBadRelatedElement()
    {
        // Arrange
        var document = Spdx2JsonDeserializer.Deserialize(Contents);

        // Act
        Assert.ThrowsException<ArgumentException>(() =>
        {
            SpdxRelationships.Add(
                document,
                new SpdxRelationship
                {
                    Id = "SPDXRef-Package-1",
                    RelatedSpdxElement = "SPDXRef-Package-Missing",
                    RelationshipType = SpdxRelationshipType.DependsOn
                });
        });

        // Assert
        Assert.AreEqual(0, document.Relationships.Length);
    }

    [TestMethod]
    public void AddRelationship()
    {
        // Arrange
        var document = Spdx2JsonDeserializer.Deserialize(Contents);

        // Act
        SpdxRelationships.Add(
            document,
            new SpdxRelationship
            {
                Id = "SPDXRef-Package-1",
                RelatedSpdxElement = "SPDXRef-Package-2",
                RelationshipType = SpdxRelationshipType.DependsOn
            });

        // Assert
        Assert.AreEqual(1, document.Relationships.Length);
        Assert.AreEqual("SPDXRef-Package-1", document.Relationships[0].Id);
        Assert.AreEqual("SPDXRef-Package-2", document.Relationships[0].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.DependsOn, document.Relationships[0].RelationshipType);
    }

    [TestMethod]
    public void AddRelationshipDuplicate()
    {
        // Arrange
        var document = Spdx2JsonDeserializer.Deserialize(Contents);

        // Act
        SpdxRelationships.Add(
            document,
            new SpdxRelationship
            {
                Id = "SPDXRef-Package-1",
                RelatedSpdxElement = "SPDXRef-Package-2",
                RelationshipType = SpdxRelationshipType.DependsOn
            });
        SpdxRelationships.Add(
            document,
            new SpdxRelationship
            {
                Id = "SPDXRef-Package-1",
                RelatedSpdxElement = "SPDXRef-Package-2",
                RelationshipType = SpdxRelationshipType.DependsOn
            });

        // Assert
        Assert.AreEqual(1, document.Relationships.Length);
        Assert.AreEqual("SPDXRef-Package-1", document.Relationships[0].Id);
        Assert.AreEqual("SPDXRef-Package-2", document.Relationships[0].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.DependsOn, document.Relationships[0].RelationshipType);
    }

    [TestMethod]
    public void AddRelationships()
    {
        // Arrange
        var document = Spdx2JsonDeserializer.Deserialize(Contents);

        // Act
        SpdxRelationships.Add(
            document,
            new[]
            {
                new SpdxRelationship
                {
                    Id = "SPDXRef-Package-1",
                    RelatedSpdxElement = "SPDXRef-Package-2",
                    RelationshipType = SpdxRelationshipType.DependsOn
                }
            });

        // Assert
        Assert.AreEqual(1, document.Relationships.Length);
        Assert.AreEqual("SPDXRef-Package-1", document.Relationships[0].Id);
        Assert.AreEqual("SPDXRef-Package-2", document.Relationships[0].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.DependsOn, document.Relationships[0].RelationshipType);
    }

    [TestMethod]
    public void AddRelationshipsDuplicate()
    {
        // Arrange
        var document = Spdx2JsonDeserializer.Deserialize(Contents);

        // Act
        SpdxRelationships.Add(
            document,
            new[]
            {
                new SpdxRelationship
                {
                    Id = "SPDXRef-Package-1",
                    RelatedSpdxElement = "SPDXRef-Package-2",
                    RelationshipType = SpdxRelationshipType.DependsOn
                },
                new SpdxRelationship
                {
                    Id = "SPDXRef-Package-1",
                    RelatedSpdxElement = "SPDXRef-Package-2",
                    RelationshipType = SpdxRelationshipType.DependsOn
                }
            });

        // Assert
        Assert.AreEqual(1, document.Relationships.Length);
        Assert.AreEqual("SPDXRef-Package-1", document.Relationships[0].Id);
        Assert.AreEqual("SPDXRef-Package-2", document.Relationships[0].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.DependsOn, document.Relationships[0].RelationshipType);
    }

    [TestMethod]
    public void AddRelationshipsReplace()
    {
        // Arrange
        var document = Spdx2JsonDeserializer.Deserialize(Contents);
        SpdxRelationships.Add(
            document,
            new SpdxRelationship
            {
                Id = "SPDXRef-Package-1",
                RelatedSpdxElement = "SPDXRef-Package-2",
                RelationshipType = SpdxRelationshipType.DependsOn
            });

        // Act
        SpdxRelationships.Add(document, new[]
            {
                new SpdxRelationship
                {
                    Id = "SPDXRef-Package-1",
                    RelatedSpdxElement = "SPDXRef-Package-2",
                    RelationshipType = SpdxRelationshipType.BuildToolOf
                }
            },
            true);

        // Assert
        Assert.AreEqual(1, document.Relationships.Length);
        Assert.AreEqual("SPDXRef-Package-1", document.Relationships[0].Id);
        Assert.AreEqual("SPDXRef-Package-2", document.Relationships[0].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.BuildToolOf, document.Relationships[0].RelationshipType);
    }
}
