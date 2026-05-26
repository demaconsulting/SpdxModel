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

using DemaConsulting.SpdxModel.IO;
using DemaConsulting.SpdxModel.Transform;

namespace DemaConsulting.SpdxModel.Tests.Transforms;

/// <summary>
///     Integration tests for the SpdxModel Transform subsystem.
/// </summary>
[TestClass]
public class SpdxModelTransformTests
{
    /// <summary>
    ///     Tests that a relationship added to an SPDX document persists in the document.
    /// </summary>
    [TestMethod]
    public void SpdxModelTransform_AddRelationship_ToDocument_RelationshipPersists()
    {
        // Arrange: Load the SPDX 2.3 JSON example as a real document to transform
        var json = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var document = Spdx2JsonDeserializer.Deserialize(json);
        var initialCount = document.Relationships.Length;

        // Act: Add a new relationship using the Transform subsystem
        SpdxRelationships.Add(
            document,
            new SpdxRelationship
            {
                Id = "SPDXRef-Package",
                RelatedSpdxElement = "SPDXRef-fromDoap-0",
                RelationshipType = SpdxRelationshipType.DependsOn
            });

        // Assert: Verify the relationship was added and the document remains valid
        Assert.AreEqual(initialCount + 1, document.Relationships.Length);
        Assert.IsTrue(Array.Exists(
            document.Relationships,
            r => r.Id == "SPDXRef-Package" &&
                 r.RelatedSpdxElement == "SPDXRef-fromDoap-0" &&
                 r.RelationshipType == SpdxRelationshipType.DependsOn));
        var issues = new List<string>();
        document.Validate(issues);
        Assert.IsEmpty(issues);
    }

    /// <summary>
    ///     Tests that adding a relationship with an invalid source element ID throws <see cref="ArgumentException" />.
    /// </summary>
    [TestMethod]
    public void SpdxModelTransform_AddRelationship_InvalidSourceId_ThrowsArgumentException()
    {
        // Arrange: Load the SPDX 2.3 JSON example as a real document to transform
        var json = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var document = Spdx2JsonDeserializer.Deserialize(json);

        // Act / Assert: Adding with a non-existent source ID throws ArgumentException
        Assert.ThrowsExactly<ArgumentException>(() =>
        {
            SpdxRelationships.Add(
                document,
                new SpdxRelationship
                {
                    Id = "SPDXRef-NonExistent",
                    RelatedSpdxElement = "SPDXRef-fromDoap-0",
                    RelationshipType = SpdxRelationshipType.DependsOn
                });
        });
    }

    /// <summary>
    ///     Tests that adding a relationship with an invalid target element ID throws <see cref="ArgumentException" />.
    /// </summary>
    [TestMethod]
    public void SpdxModelTransform_AddRelationship_InvalidTargetId_ThrowsArgumentException()
    {
        // Arrange: Load the SPDX 2.3 JSON example as a real document to transform
        var json = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var document = Spdx2JsonDeserializer.Deserialize(json);

        // Act / Assert: Adding with a non-existent target that is neither NOASSERTION nor DocumentRef- throws
        Assert.ThrowsExactly<ArgumentException>(() =>
        {
            SpdxRelationships.Add(
                document,
                new SpdxRelationship
                {
                    Id = "SPDXRef-Package",
                    RelatedSpdxElement = "SPDXRef-NonExistent",
                    RelationshipType = SpdxRelationshipType.DependsOn
                });
        });
    }

    /// <summary>
    ///     Tests that adding a duplicate relationship enhances the existing entry rather than duplicating it.
    /// </summary>
    [TestMethod]
    public void SpdxModelTransform_AddRelationship_Duplicate_EnhancesExistingRelationship()
    {
        // Arrange: Load the SPDX 2.3 JSON example and add an initial relationship
        var json = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var document = Spdx2JsonDeserializer.Deserialize(json);
        var initialCount = document.Relationships.Length;
        SpdxRelationships.Add(
            document,
            new SpdxRelationship
            {
                Id = "SPDXRef-Package",
                RelatedSpdxElement = "SPDXRef-fromDoap-0",
                RelationshipType = SpdxRelationshipType.DependsOn
            });

        // Act: Add the same relationship again
        SpdxRelationships.Add(
            document,
            new SpdxRelationship
            {
                Id = "SPDXRef-Package",
                RelatedSpdxElement = "SPDXRef-fromDoap-0",
                RelationshipType = SpdxRelationshipType.DependsOn
            });

        // Assert: Only one new relationship was added (duplicate was merged, not appended)
        Assert.AreEqual(initialCount + 1, document.Relationships.Length);
    }

    /// <summary>
    ///     Tests that the batch Add with replace=true removes pre-existing matching relationships.
    /// </summary>
    [TestMethod]
    public void SpdxModelTransform_AddRelationship_Replace_RemovesPreExistingRelationships()
    {
        // Arrange: Load the SPDX 2.3 JSON example and add an initial relationship
        var json = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var document = Spdx2JsonDeserializer.Deserialize(json);
        SpdxRelationships.Add(
            document,
            new SpdxRelationship
            {
                Id = "SPDXRef-Package",
                RelatedSpdxElement = "SPDXRef-fromDoap-0",
                RelationshipType = SpdxRelationshipType.DependsOn
            });
        var countAfterFirstAdd = document.Relationships.Length;

        // Act: Replace the relationship with a different type using the batch overload
        SpdxRelationships.Add(
            document,
            [
                new SpdxRelationship
                {
                    Id = "SPDXRef-Package",
                    RelatedSpdxElement = "SPDXRef-fromDoap-0",
                    RelationshipType = SpdxRelationshipType.BuildToolOf
                }
            ],
            replace: true);

        // Assert: The count is unchanged (old removed, new added) and the type changed
        Assert.AreEqual(countAfterFirstAdd, document.Relationships.Length);
        Assert.IsTrue(Array.Exists(
            document.Relationships,
            r => r.Id == "SPDXRef-Package" &&
                 r.RelatedSpdxElement == "SPDXRef-fromDoap-0" &&
                 r.RelationshipType == SpdxRelationshipType.BuildToolOf));
        Assert.IsFalse(Array.Exists(
            document.Relationships,
            r => r.Id == "SPDXRef-Package" &&
                 r.RelatedSpdxElement == "SPDXRef-fromDoap-0" &&
                 r.RelationshipType == SpdxRelationshipType.DependsOn));
    }

    /// <summary>
    ///     Tests that the batch Add with multiple relationships adds all of them.
    /// </summary>
    [TestMethod]
    public void SpdxModelTransform_AddRelationship_BatchMultiple_AddsAllRelationships()
    {
        // Arrange: Load the SPDX 2.3 JSON example
        var json = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var document = Spdx2JsonDeserializer.Deserialize(json);
        var initialCount = document.Relationships.Length;

        // Act: Add two distinct relationships in a single batch call
        SpdxRelationships.Add(
            document,
            [
                new SpdxRelationship
                {
                    Id = "SPDXRef-Package",
                    RelatedSpdxElement = "SPDXRef-fromDoap-0",
                    RelationshipType = SpdxRelationshipType.DependsOn
                },
                new SpdxRelationship
                {
                    Id = "SPDXRef-Package",
                    RelatedSpdxElement = "SPDXRef-fromDoap-1",
                    RelationshipType = SpdxRelationshipType.DependsOn
                }
            ]);

        // Assert: Both relationships were added
        Assert.AreEqual(initialCount + 2, document.Relationships.Length);
    }

    /// <summary>
    ///     Tests that a relationship with NOASSERTION as the target element is accepted as valid.
    /// </summary>
    [TestMethod]
    public void SpdxModelTransform_AddRelationship_NoAssertionTarget_AddsRelationship()
    {
        // Arrange: Load the SPDX 2.3 JSON example
        var json = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var document = Spdx2JsonDeserializer.Deserialize(json);
        var initialCount = document.Relationships.Length;

        // Act: Add a relationship where the target is NOASSERTION
        SpdxRelationships.Add(
            document,
            new SpdxRelationship
            {
                Id = "SPDXRef-Package",
                RelatedSpdxElement = SpdxElement.NoAssertion,
                RelationshipType = SpdxRelationshipType.DependsOn
            });

        // Assert: Relationship was added without an exception
        Assert.AreEqual(initialCount + 1, document.Relationships.Length);
        Assert.IsTrue(Array.Exists(
            document.Relationships,
            r => r.Id == "SPDXRef-Package" &&
                 r.RelatedSpdxElement == SpdxElement.NoAssertion &&
                 r.RelationshipType == SpdxRelationshipType.DependsOn));
    }

    /// <summary>
    ///     Tests that a relationship with a DocumentRef- prefixed target is accepted as valid.
    /// </summary>
    [TestMethod]
    public void SpdxModelTransform_AddRelationship_DocumentRefTarget_AddsRelationship()
    {
        // Arrange: Load the SPDX 2.3 JSON example
        var json = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var document = Spdx2JsonDeserializer.Deserialize(json);
        var initialCount = document.Relationships.Length;

        // Act: Add a relationship where the target uses the DocumentRef- prefix
        SpdxRelationships.Add(
            document,
            new SpdxRelationship
            {
                Id = "SPDXRef-Package",
                RelatedSpdxElement = "DocumentRef-spdx-tool-1.2:SPDXRef-Package",
                RelationshipType = SpdxRelationshipType.DependsOn
            });

        // Assert: Relationship was added without an exception
        Assert.AreEqual(initialCount + 1, document.Relationships.Length);
        Assert.IsTrue(Array.Exists(
            document.Relationships,
            r => r.Id == "SPDXRef-Package" &&
                 r.RelatedSpdxElement == "DocumentRef-spdx-tool-1.2:SPDXRef-Package" &&
                 r.RelationshipType == SpdxRelationshipType.DependsOn));
    }
}
