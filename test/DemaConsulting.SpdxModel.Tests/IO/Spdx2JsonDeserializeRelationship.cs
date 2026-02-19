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

using System.Text.Json.Nodes;
using DemaConsulting.SpdxModel.IO;

namespace DemaConsulting.SpdxModel.Tests.IO;

/// <summary>
///     Tests for deserializing SPDX relationships to <see cref="SpdxRelationship" /> classes.
/// </summary>
[TestClass]
public class Spdx2JsonDeserializeRelationship
{
    /// <summary>
    ///     Tests deserializing a relationship.
    /// </summary>
    [TestMethod]
    public void Spdx2JsonDeserializer_DeserializeRelationship_CorrectResults()
    {
        // Arrange: Create a JSON object representing a relationship
        var json = new JsonObject
        {
            ["spdxElementId"] = "SPDXRef-DOCUMENT",
            ["relatedSpdxElement"] = "SPDXRef-Package",
            ["relationshipType"] = "DESCRIBES",
            ["comment"] = "This is just an example"
        };

        // Act: Deserialize the JSON object to an SpdxRelationship object
        var relationship = Spdx2JsonDeserializer.DeserializeRelationship(json);

        // Assert: Verify the deserialized object has the expected properties
        Assert.AreEqual("SPDXRef-DOCUMENT", relationship.Id);
        Assert.AreEqual("SPDXRef-Package", relationship.RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.Describes, relationship.RelationshipType);
        Assert.AreEqual("This is just an example", relationship.Comment);
    }

    /// <summary>
    ///     Tests deserializing multiple relationships.
    /// </summary>
    [TestMethod]
    public void Spdx2JsonDeserializer_DeserializeRelationships_CorrectResults()
    {
        // Arrange: Create a JSON array representing multiple relationships
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

        // Act: Deserialize the JSON array to an array of SpdxRelationship objects
        var relationships = Spdx2JsonDeserializer.DeserializeRelationships(json);

        // Assert: Verify the deserialized objects have the expected properties
        Assert.HasCount(2, relationships);
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
