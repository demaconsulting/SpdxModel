using DemaConsulting.SpdxModel.IO;
using DemaConsulting.SpdxModel.Transform;

namespace DemaConsulting.SpdxModel.Tests.Transforms;

/// <summary>
///     Tests for the <see cref="SpdxRelationship" /> transforms.
/// </summary>
[TestClass]
public class SpdxRelationshipsTests
{
    /// <summary>
    ///     Test SPDX document for relationships.
    /// </summary>
    private const string TestDocumentContents =
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

    /// <summary>
    ///     Tests adding a relationship with a bad ID.
    /// </summary>
    [TestMethod]
    public void SpdxRelationships_AddSingle_BadId()
    {
        // Arrange: Deserialize the test document contents
        var document = Spdx2JsonDeserializer.Deserialize(TestDocumentContents);

        // Act: Attempt to add a relationship with a missing ID
        var ex = Assert.ThrowsExactly<ArgumentException>(() =>
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

        // Assert: Verify the exception message and that no relationships were added
        Assert.AreEqual("Element SPDXRef-Package-Missing not found in SPDX document (Parameter 'relationship')",
            ex.Message);
        Assert.IsEmpty(document.Relationships);
    }

    /// <summary>
    ///     Tests adding a relationship with a bad related element.
    /// </summary>
    [TestMethod]
    public void SpdxRelationships_AddSingle_BadRelatedElement()
    {
        // Arrange: Deserialize the test document contents
        var document = Spdx2JsonDeserializer.Deserialize(TestDocumentContents);

        // Act: Attempt to add a relationship with a missing related element
        var ex = Assert.ThrowsExactly<ArgumentException>(() =>
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

        // Assert: Verify the exception message and that no relationships were added
        Assert.AreEqual("Element SPDXRef-Package-Missing not found in SPDX document (Parameter 'relationship')",
            ex.Message);
        Assert.IsEmpty(document.Relationships);
    }

    /// <summary>
    ///     Tests adding a relationship.
    /// </summary>
    [TestMethod]
    public void SpdxRelationships_AddSingle_Success()
    {
        // Arrange: Deserialize the test document contents
        var document = Spdx2JsonDeserializer.Deserialize(TestDocumentContents);

        // Act: Add a new relationship
        SpdxRelationships.Add(
            document,
            new SpdxRelationship
            {
                Id = "SPDXRef-Package-1",
                RelatedSpdxElement = "SPDXRef-Package-2",
                RelationshipType = SpdxRelationshipType.DependsOn
            });

        // Assert: Verify the relationship was added correctly
        Assert.HasCount(1, document.Relationships);
        Assert.AreEqual("SPDXRef-Package-1", document.Relationships[0].Id);
        Assert.AreEqual("SPDXRef-Package-2", document.Relationships[0].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.DependsOn, document.Relationships[0].RelationshipType);
    }

    /// <summary>
    ///     Tests adding a duplicate relationship.
    /// </summary>
    [TestMethod]
    public void SpdxRelationships_AddSingle_Duplicate()
    {
        // Arrange: Deserialize the test document contents
        var document = Spdx2JsonDeserializer.Deserialize(TestDocumentContents);

        // Act: Add the same new relationship twice
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

        // Assert: Verify the relationship was added only once
        Assert.HasCount(1, document.Relationships);
        Assert.AreEqual("SPDXRef-Package-1", document.Relationships[0].Id);
        Assert.AreEqual("SPDXRef-Package-2", document.Relationships[0].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.DependsOn, document.Relationships[0].RelationshipType);
    }

    /// <summary>
    ///     Tests adding multiple relationships.
    /// </summary>
    [TestMethod]
    public void SpdxRelationships_AddMultiple_Success()
    {
        // Arrange: Deserialize the test document contents
        var document = Spdx2JsonDeserializer.Deserialize(TestDocumentContents);

        // Act: Add relationship
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

        // Assert: Verify the relationship was added correctly
        Assert.HasCount(1, document.Relationships);
        Assert.AreEqual("SPDXRef-Package-1", document.Relationships[0].Id);
        Assert.AreEqual("SPDXRef-Package-2", document.Relationships[0].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.DependsOn, document.Relationships[0].RelationshipType);
    }

    /// <summary>
    ///     Tests adding multiple relationships with a duplicate.
    /// </summary>
    [TestMethod]
    public void SpdxRelationships_AddMultiple_Duplicate()
    {
        // Arrange: Deserialize the test document contents
        var document = Spdx2JsonDeserializer.Deserialize(TestDocumentContents);

        // Act: Add new relationships with a duplicate
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

        // Assert: Verify the relationship was added only once
        Assert.HasCount(1, document.Relationships);
        Assert.AreEqual("SPDXRef-Package-1", document.Relationships[0].Id);
        Assert.AreEqual("SPDXRef-Package-2", document.Relationships[0].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.DependsOn, document.Relationships[0].RelationshipType);
    }

    /// <summary>
    ///     Tests adding multiple relationships with a duplicate and replace.
    /// </summary>
    [TestMethod]
    public void SpdxRelationships_AddMultiple_Replace()
    {
        // Arrange: Deserialize the test document contents and add an initial relationship
        var document = Spdx2JsonDeserializer.Deserialize(TestDocumentContents);
        SpdxRelationships.Add(
            document,
            new SpdxRelationship
            {
                Id = "SPDXRef-Package-1",
                RelatedSpdxElement = "SPDXRef-Package-2",
                RelationshipType = SpdxRelationshipType.DependsOn
            });

        // Act: Add new relationships duplicating the original, but with a different type
        SpdxRelationships.Add(document, [
                new SpdxRelationship
                {
                    Id = "SPDXRef-Package-1",
                    RelatedSpdxElement = "SPDXRef-Package-2",
                    RelationshipType = SpdxRelationshipType.BuildToolOf
                }
            ],
            true);

        // Assert: Verify the relationship was replaced with the new type
        Assert.HasCount(1, document.Relationships);
        Assert.AreEqual("SPDXRef-Package-1", document.Relationships[0].Id);
        Assert.AreEqual("SPDXRef-Package-2", document.Relationships[0].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.BuildToolOf, document.Relationships[0].RelationshipType);
    }
}
