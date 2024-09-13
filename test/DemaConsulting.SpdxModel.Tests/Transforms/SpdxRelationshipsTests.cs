﻿using DemaConsulting.SpdxModel.IO;
using DemaConsulting.SpdxModel.Transform;

namespace DemaConsulting.SpdxModel.Tests.Transforms;

[TestClass]
public class SpdxRelationshipsTests
{
    /// <summary>
    /// Test SPDX document for relationships.
    /// </summary>
    private const string Contents = 
        """
        {
          "files": [],
          "packages": [    {
              "SPDXID": "SPDXRef-Package-1",
              "name": "Test Package",
              "versionInfo": "1.0.0",
              "packageFileName": "package1.zip",
              "downloadLocation": "https://github.com/demaconsulting/SpdxTool",
              "licenseConcluded": "MIT"
            },
            {
              "SPDXID": "SPDXRef-Package-2",
              "name": "Another Test Package",
              "versionInfo": "2.0.0",
              "packageFileName": "package2.tar",
              "downloadLocation": "https://github.com/demaconsulting/SpdxModel",
              "licenseConcluded": "MIT"
            }
          ],
          "spdxVersion": "SPDX-2.2",
          "dataLicense": "CC0-1.0",
          "SPDXID": "SPDXRef-DOCUMENT",
          "name": "Test Document",
          "documentNamespace": "https://sbom.spdx.org",
          "creationInfo": {
            "created": "2021-10-01T00:00:00Z",
            "creators": [ "Person: Malcolm Nixon" ]
          },
          "documentDescribes": [ "SPDXRef-Package-1" ]
        }
        """;

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
            [
                new SpdxRelationship
                {
                    Id = "SPDXRef-Package-1",
                    RelatedSpdxElement = "SPDXRef-Package-2",
                    RelationshipType = SpdxRelationshipType.DependsOn
                }
            ]);

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
            [
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
            ]);

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
        SpdxRelationships.Add(document, [
                new SpdxRelationship
                {
                    Id = "SPDXRef-Package-1",
                    RelatedSpdxElement = "SPDXRef-Package-2",
                    RelationshipType = SpdxRelationshipType.BuildToolOf
                }
            ],
            true);

        // Assert
        Assert.AreEqual(1, document.Relationships.Length);
        Assert.AreEqual("SPDXRef-Package-1", document.Relationships[0].Id);
        Assert.AreEqual("SPDXRef-Package-2", document.Relationships[0].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.BuildToolOf, document.Relationships[0].RelationshipType);
    }
}