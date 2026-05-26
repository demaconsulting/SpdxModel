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
///     Tests for deserializing SPDX external document references to <see cref="SpdxExternalDocumentReference" /> classes.
/// </summary>
/// <remarks>
///     Exercises deserialization of SPDX external document reference elements using MSTest
///     as the approved test framework for this repository. Each test constructs inline JSON
///     and verifies the resulting <see cref="SpdxExternalDocumentReference"/> fields.
/// </remarks>
[TestClass]
public class Spdx2JsonDeserializeExternalDocumentReference
{
    /// <summary>
    ///     Tests deserializing an external document reference.
    /// </summary>
    /// <remarks>
    ///     Verifies that externalDocumentId, checksum (algorithm and value), and spdxDocument
    ///     JSON fields are correctly mapped to the <see cref="SpdxExternalDocumentReference"/>
    ///     properties when a single object is deserialized.
    /// </remarks>
    [TestMethod]
    public void Spdx2JsonDeserializer_DeserializeExternalDocumentReference_ValidInput_CorrectResults()
    {
        // Arrange: Create a JSON object representing an external document reference
        var json = new JsonObject
        {
            ["externalDocumentId"] = "DocumentRef-1",
            ["checksum"] = new JsonObject
            {
                ["algorithm"] = "SHA1",
                ["checksumValue"] = "d6a770ba38583ed4bb4525bd96e50461655d2759"
            },
            ["comment"] = "This is a comment",
            ["spdxDocument"] = "SPDXRef-Document"
        };

        // Act: Deserialize the JSON object to an SpdxExternalDocumentReference object
        var externalDocumentReference = Spdx2JsonDeserializer.DeserializeExternalDocumentReference(json);

        // Assert: Verify the deserialized object has the expected properties
        Assert.AreEqual("DocumentRef-1", externalDocumentReference.ExternalDocumentId);
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, externalDocumentReference.Checksum.Algorithm);
        Assert.AreEqual("d6a770ba38583ed4bb4525bd96e50461655d2759", externalDocumentReference.Checksum.Value);
        Assert.AreEqual("SPDXRef-Document", externalDocumentReference.Document);
    }

    /// <summary>
    ///     Tests deserializing multiple external document references.
    /// </summary>
    /// <remarks>
    ///     Verifies that a JSON array containing one external document reference object is
    ///     deserialized to a single-element array with all fields correctly populated.
    /// </remarks>
    [TestMethod]
    public void Spdx2JsonDeserializer_DeserializeExternalDocumentReferences_ValidInput_CorrectResults()
    {
        // Arrange: Create a JSON array representing multiple external document references
        var json = new JsonArray
        {
            new JsonObject
            {
                ["externalDocumentId"] = "DocumentRef-1",
                ["checksum"] = new JsonObject
                {
                    ["algorithm"] = "SHA1",
                    ["checksumValue"] = "d6a770ba38583ed4bb4525bd96e50461655d2759"
                },
                ["comment"] = "This is a comment",
                ["spdxDocument"] = "SPDXRef-Document"
            }
        };

        // Act: Deserialize the JSON array to an array of SpdxExternalDocumentReference objects
        var externalDocumentReferences = Spdx2JsonDeserializer.DeserializeExternalDocumentReferences(json);

        // Assert: Verify the deserialized array has the expected number of references and their properties
        Assert.HasCount(1, externalDocumentReferences);
        Assert.AreEqual("DocumentRef-1", externalDocumentReferences[0].ExternalDocumentId);
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, externalDocumentReferences[0].Checksum.Algorithm);
        Assert.AreEqual("d6a770ba38583ed4bb4525bd96e50461655d2759", externalDocumentReferences[0].Checksum.Value);
        Assert.AreEqual("SPDXRef-Document", externalDocumentReferences[0].Document);
    }
}
