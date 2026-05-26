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
    ///     Tests that adding a relationship with a missing source ID throws <see cref="ArgumentException" />.
    /// </summary>
    [TestMethod]
    public void SpdxRelationships_AddSingle_MissingId_ThrowsArgumentException()
    {
        // Arrange: Deserialize the test document contents
        var document = Spdx2JsonDeserializer.Deserialize(TestDocumentContents);

        // Act: Attempt to add a relationship with a non-existent ID
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
        Assert.StartsWith("Element SPDXRef-Package-Missing not found in SPDX document", ex.Message);
        Assert.AreEqual("relationship", ex.ParamName);
        Assert.IsEmpty(document.Relationships);
    }

    /// <summary>
    ///     Tests that adding a relationship with a missing related element throws <see cref="ArgumentException" />.
    /// </summary>
    [TestMethod]
    public void SpdxRelationships_AddSingle_MissingRelatedElement_ThrowsArgumentException()
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
        Assert.StartsWith("Element SPDXRef-Package-Missing not found in SPDX document", ex.Message);
        Assert.AreEqual("relationship", ex.ParamName);
        Assert.IsEmpty(document.Relationships);
    }

    /// <summary>
    ///     Tests that adding a valid relationship appends it to the document.
    /// </summary>
    [TestMethod]
    public void SpdxRelationships_AddSingle_ValidRelationship_AddsRelationship()
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
    ///     Tests that adding a duplicate relationship enhances the existing entry rather than duplicating it.
    /// </summary>
    [TestMethod]
    public void SpdxRelationships_AddSingle_DuplicateRelationship_EnhancesExistingRelationship()
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
    ///     Tests that a relationship with a NOASSERTION target is accepted as valid.
    /// </summary>
    [TestMethod]
    public void SpdxRelationships_AddSingle_NoAssertionTarget_AddsRelationship()
    {
        // Arrange: Deserialize the test document contents
        var document = Spdx2JsonDeserializer.Deserialize(TestDocumentContents);

        // Act: Add a relationship where the target is NOASSERTION
        SpdxRelationships.Add(
            document,
            new SpdxRelationship
            {
                Id = "SPDXRef-Package-1",
                RelatedSpdxElement = SpdxElement.NoAssertion,
                RelationshipType = SpdxRelationshipType.DependsOn
            });

        // Assert: Verify the relationship was added correctly
        Assert.HasCount(1, document.Relationships);
        Assert.AreEqual("SPDXRef-Package-1", document.Relationships[0].Id);
        Assert.AreEqual(SpdxElement.NoAssertion, document.Relationships[0].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.DependsOn, document.Relationships[0].RelationshipType);
    }

    /// <summary>
    ///     Tests that a relationship with a DocumentRef- prefixed target is accepted as valid.
    /// </summary>
    [TestMethod]
    public void SpdxRelationships_AddSingle_DocumentRefTarget_AddsRelationship()
    {
        // Arrange: Deserialize the test document contents
        var document = Spdx2JsonDeserializer.Deserialize(TestDocumentContents);

        // Act: Add a relationship where the target uses the DocumentRef- external-reference prefix
        SpdxRelationships.Add(
            document,
            new SpdxRelationship
            {
                Id = "SPDXRef-Package-1",
                RelatedSpdxElement = "DocumentRef-external:SPDXRef-Package-3",
                RelationshipType = SpdxRelationshipType.DependsOn
            });

        // Assert: Verify the relationship was added correctly without requiring the element in the document
        Assert.HasCount(1, document.Relationships);
        Assert.AreEqual("SPDXRef-Package-1", document.Relationships[0].Id);
        Assert.AreEqual("DocumentRef-external:SPDXRef-Package-3", document.Relationships[0].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.DependsOn, document.Relationships[0].RelationshipType);
    }

    /// <summary>
    ///     Tests that the batch Add with a single relationship appends it to the document.
    /// </summary>
    [TestMethod]
    public void SpdxRelationships_AddMultiple_SingleRelationship_AddsRelationship()
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
    ///     Tests that adding multiple duplicate relationships deduplicates them.
    /// </summary>
    [TestMethod]
    public void SpdxRelationships_AddMultiple_DuplicateRelationships_DeduplicatesRelationships()
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
    ///     Tests that the batch Add with replace=true removes pre-existing matching relationships.
    /// </summary>
    [TestMethod]
    public void SpdxRelationships_AddMultiple_Replace_RemovesAndReplacesExistingRelationships()
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

    /// <summary>
    ///     Tests that a batch Add with replace=true and an invalid relationship leaves the document unmodified.
    /// </summary>
    [TestMethod]
    public void SpdxRelationships_AddMultiple_InvalidRelationship_LeavesDocumentUnmodified()
    {
        // Arrange: Deserialize the test document and add an initial relationship
        var document = Spdx2JsonDeserializer.Deserialize(TestDocumentContents);
        SpdxRelationships.Add(
            document,
            new SpdxRelationship
            {
                Id = "SPDXRef-Package-1",
                RelatedSpdxElement = "SPDXRef-Package-2",
                RelationshipType = SpdxRelationshipType.DependsOn
            });
        var initialRelationships = document.Relationships.ToArray();

        // Act: Attempt a batch-add with replace=true where the second relationship has an invalid source ID
        Assert.ThrowsExactly<ArgumentException>(() =>
        {
            SpdxRelationships.Add(
                document,
                [
                    new SpdxRelationship
                    {
                        Id = "SPDXRef-Package-1",
                        RelatedSpdxElement = "SPDXRef-Package-2",
                        RelationshipType = SpdxRelationshipType.BuildToolOf
                    },
                    new SpdxRelationship
                    {
                        Id = "SPDXRef-Package-Missing",
                        RelatedSpdxElement = "SPDXRef-Package-2",
                        RelationshipType = SpdxRelationshipType.DependsOn
                    }
                ],
                replace: true);
        });

        // Assert: Document relationships are unchanged after the failed batch-add
        Assert.HasCount(initialRelationships.Length, document.Relationships);
        Assert.AreEqual("SPDXRef-Package-1", document.Relationships[0].Id);
        Assert.AreEqual("SPDXRef-Package-2", document.Relationships[0].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.DependsOn, document.Relationships[0].RelationshipType);
    }
}
