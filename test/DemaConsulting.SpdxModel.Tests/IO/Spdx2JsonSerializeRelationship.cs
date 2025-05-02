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

namespace DemaConsulting.SpdxModel.Tests.IO;

/// <summary>
/// Tests for serializing <see cref="SpdxRelationship"/> to JSON.
/// </summary>
[TestClass]
public class Spdx2JsonSerializeRelationship
{
    /// <summary>
    /// Tests serializing a relationship.
    /// </summary>
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
        var json = Spdx2JsonSerializer.SerializeRelationship(relationship);

        // Assert
        Assert.IsNotNull(json);
        SpdxJsonHelpers.AssertEqual("SPDXRef-DOCUMENT", json["spdxElementId"]);
        SpdxJsonHelpers.AssertEqual("SPDXRef-Package", json["relatedSpdxElement"]);
        SpdxJsonHelpers.AssertEqual("DESCRIBES", json["relationshipType"]);
        SpdxJsonHelpers.AssertEqual("Example Comment", json["comment"]);
    }

    /// <summary>
    /// Tests serializing multiple relationships.
    /// </summary>
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
        var json = Spdx2JsonSerializer.SerializeRelationships(relationships);

        // Assert
        Assert.IsNotNull(json);
        Assert.AreEqual(2, json.Count);
        SpdxJsonHelpers.AssertEqual("SPDXRef-DOCUMENT", json[0]?["spdxElementId"]);
        SpdxJsonHelpers.AssertEqual("SPDXRef-Package", json[0]?["relatedSpdxElement"]);
        SpdxJsonHelpers.AssertEqual("DESCRIBES", json[0]?["relationshipType"]);
        SpdxJsonHelpers.AssertEqual("Example Comment", json[0]?["comment"]);
        SpdxJsonHelpers.AssertEqual("SPDXRef-Package", json[1]?["spdxElementId"]);
        SpdxJsonHelpers.AssertEqual("SPDXRef-DOCUMENT", json[1]?["relatedSpdxElement"]);
        SpdxJsonHelpers.AssertEqual("DESCRIBED_BY", json[1]?["relationshipType"]);
        SpdxJsonHelpers.AssertEqual("Example Comment", json[1]?["comment"]);
    }
}