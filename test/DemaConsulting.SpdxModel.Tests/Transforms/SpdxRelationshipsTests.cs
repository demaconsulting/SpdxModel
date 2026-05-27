using DemaConsulting.SpdxModel.IO;
using DemaConsulting.SpdxModel.Transform;

namespace DemaConsulting.SpdxModel.Tests.Transforms;

/// <summary>
///     Tests for the <see cref="SpdxRelationships" /> transforms.
/// </summary>
/// <remarks>
///     Uses xUnit v3 as the test framework.
///     Every test deserializes a fresh copy of <see cref="TestDocumentContents"/> to prevent
///     inter-test state leakage. The class covers the full scope of <see cref="SpdxRelationships"/>
///     operations: adding single and multiple relationships, deduplication, replacement, and
///     atomicity on failure.
/// </remarks>
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
    /// <remarks>
    ///     Verifies the error path where the source element ID does not exist in the document.
    ///     A fresh document is deserialized to ensure baseline state contains no relationships.
    ///     Confirms the exception carries the correct parameter name and that the document
    ///     relationships collection remains empty after the failed call.
    /// </remarks>
    [Fact]
    public void SpdxRelationships_AddSingle_MissingId_ThrowsArgumentException()
    {
        // Arrange: Deserialize the test document contents
        var document = Spdx2JsonDeserializer.Deserialize(TestDocumentContents);

        // Act: Attempt to add a relationship with a non-existent ID
        var ex = Assert.Throws<ArgumentException>(() =>
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
        Assert.Equal("relationship", ex.ParamName);
        Assert.Empty(document.Relationships);
    }

    /// <summary>
    ///     Tests that adding a relationship with a missing related element throws <see cref="ArgumentException" />.
    /// </summary>
    /// <remarks>
    ///     Verifies the error path where the target element ID does not exist in the document.
    ///     A fresh document is deserialized to ensure baseline state contains no relationships.
    ///     Confirms the exception carries the correct parameter name and that the document
    ///     relationships collection remains empty after the failed call.
    /// </remarks>
    [Fact]
    public void SpdxRelationships_AddSingle_MissingRelatedElement_ThrowsArgumentException()
    {
        // Arrange: Deserialize the test document contents
        var document = Spdx2JsonDeserializer.Deserialize(TestDocumentContents);

        // Act: Attempt to add a relationship with a missing related element
        var ex = Assert.Throws<ArgumentException>(() =>
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
        Assert.Equal("relationship", ex.ParamName);
        Assert.Empty(document.Relationships);
    }

    /// <summary>
    ///     Tests that adding a valid relationship appends it to the document.
    /// </summary>
    /// <remarks>
    ///     Verifies the happy path where both source and target elements exist in the document.
    ///     A fresh document is deserialized so the initial relationships collection is empty,
    ///     making the single post-add entry the definitive proof of a successful append.
    /// </remarks>
    [Fact]
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
        Assert.Single(document.Relationships);
        Assert.Equal("SPDXRef-Package-1", document.Relationships[0].Id);
        Assert.Equal("SPDXRef-Package-2", document.Relationships[0].RelatedSpdxElement);
        Assert.Equal(SpdxRelationshipType.DependsOn, document.Relationships[0].RelationshipType);
    }

    /// <summary>
    ///     Tests that adding a duplicate relationship enhances the existing entry rather than duplicating it.
    /// </summary>
    /// <remarks>
    ///     Verifies the deduplication behaviour: a second call with an identical relationship
    ///     must not create a second entry. A fresh document is deserialized to isolate the
    ///     count assertion; the final count of one proves that the second add merged into the
    ///     existing entry rather than appending a new one.
    /// </remarks>
    [Fact]
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
        Assert.Single(document.Relationships);
        Assert.Equal("SPDXRef-Package-1", document.Relationships[0].Id);
        Assert.Equal("SPDXRef-Package-2", document.Relationships[0].RelatedSpdxElement);
        Assert.Equal(SpdxRelationshipType.DependsOn, document.Relationships[0].RelationshipType);
    }

    /// <summary>
    ///     Tests that a relationship with a NOASSERTION target is accepted as valid.
    /// </summary>
    /// <remarks>
    ///     Verifies that <see cref="SpdxElement.NoAssertion"/> is treated as a sentinel value
    ///     that bypasses the element-existence check on the target. A fresh document is
    ///     deserialized so the single resulting entry proves the add succeeded without requiring
    ///     the target to be present in the document's element collections.
    /// </remarks>
    [Fact]
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
        Assert.Single(document.Relationships);
        Assert.Equal("SPDXRef-Package-1", document.Relationships[0].Id);
        Assert.Equal(SpdxElement.NoAssertion, document.Relationships[0].RelatedSpdxElement);
        Assert.Equal(SpdxRelationshipType.DependsOn, document.Relationships[0].RelationshipType);
    }

    /// <summary>
    ///     Tests that a relationship with a DocumentRef- prefixed target is accepted as valid.
    /// </summary>
    /// <remarks>
    ///     Verifies that a target ID beginning with <c>DocumentRef-</c> is treated as an
    ///     external-document reference and bypasses the local element-existence check. A fresh
    ///     document is deserialized so the single resulting entry proves the add succeeded
    ///     without requiring the external element to appear in the local document.
    /// </remarks>
    [Fact]
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
        Assert.Single(document.Relationships);
        Assert.Equal("SPDXRef-Package-1", document.Relationships[0].Id);
        Assert.Equal("DocumentRef-external:SPDXRef-Package-3", document.Relationships[0].RelatedSpdxElement);
        Assert.Equal(SpdxRelationshipType.DependsOn, document.Relationships[0].RelationshipType);
    }

    /// <summary>
    ///     Tests that the batch Add with a single relationship appends it to the document.
    /// </summary>
    /// <remarks>
    ///     Verifies the batch overload behaves identically to the single-item overload for a
    ///     one-element array. A fresh document is deserialized so the count assertion unambiguously
    ///     reflects the result of the batch call rather than any pre-existing state.
    /// </remarks>
    [Fact]
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
        Assert.Single(document.Relationships);
        Assert.Equal("SPDXRef-Package-1", document.Relationships[0].Id);
        Assert.Equal("SPDXRef-Package-2", document.Relationships[0].RelatedSpdxElement);
        Assert.Equal(SpdxRelationshipType.DependsOn, document.Relationships[0].RelationshipType);
    }

    /// <summary>
    ///     Tests that adding multiple duplicate relationships deduplicates them.
    /// </summary>
    /// <remarks>
    ///     Verifies that a batch containing two identical entries results in only one relationship
    ///     in the document. A fresh document is deserialized so the final count of one is solely
    ///     the result of the batch call; no pre-existing entries could mask a deduplication failure.
    /// </remarks>
    [Fact]
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
        Assert.Single(document.Relationships);
        Assert.Equal("SPDXRef-Package-1", document.Relationships[0].Id);
        Assert.Equal("SPDXRef-Package-2", document.Relationships[0].RelatedSpdxElement);
        Assert.Equal(SpdxRelationshipType.DependsOn, document.Relationships[0].RelationshipType);
    }

    /// <summary>
    ///     Tests that the batch Add with replace=true removes pre-existing matching relationships.
    /// </summary>
    /// <remarks>
    ///     Verifies the replace flag: when <c>replace: true</c> is passed, any pre-existing
    ///     relationships whose source-target pair matches an entry in the batch are removed
    ///     before the new relationships are inserted. A fresh document with a single pre-seeded
    ///     relationship is used so the type change from the replacement is unambiguous.
    /// </remarks>
    [Fact]
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
        Assert.Single(document.Relationships);
        Assert.Equal("SPDXRef-Package-1", document.Relationships[0].Id);
        Assert.Equal("SPDXRef-Package-2", document.Relationships[0].RelatedSpdxElement);
        Assert.Equal(SpdxRelationshipType.BuildToolOf, document.Relationships[0].RelationshipType);
    }

    /// <summary>
    ///     Tests that a batch Add with replace=true and an invalid relationship leaves the document unmodified.
    /// </summary>
    /// <remarks>
    ///     Verifies atomicity: when the batch contains an invalid relationship (missing source ID),
    ///     the entire operation is rolled back and the document's relationships are unchanged.
    ///     A pre-seeded relationship is added before the batch call so the assertion on the
    ///     unchanged collection proves that even the valid first entry in the batch was not committed.
    /// </remarks>
    [Fact]
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
        Assert.Throws<ArgumentException>(() =>
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
        Assert.Equal(initialRelationships.Length, document.Relationships.Length);
        Assert.Equal("SPDXRef-Package-1", document.Relationships[0].Id);
        Assert.Equal("SPDXRef-Package-2", document.Relationships[0].RelatedSpdxElement);
        Assert.Equal(SpdxRelationshipType.DependsOn, document.Relationships[0].RelationshipType);
    }
}
