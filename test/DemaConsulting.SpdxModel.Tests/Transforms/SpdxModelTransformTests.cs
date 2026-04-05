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
}
