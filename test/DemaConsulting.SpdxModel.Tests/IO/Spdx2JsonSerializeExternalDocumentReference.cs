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
/// Tests for serializing <see cref="SpdxExternalDocumentReference"/> to JSON.
/// </summary>
[TestClass]
public class Spdx2JsonSerializeExternalDocumentReference
{
    /// <summary>
    /// Tests serializing an external document reference.
    /// </summary>
    [TestMethod]
    public void SerializeExternalDocumentReference()
    {
        // Arrange
        var reference = new SpdxExternalDocumentReference
        {
            ExternalDocumentId = "DocumentRef-spdx-tool-1.2",
            Checksum = new SpdxChecksum
            {
                Algorithm = SpdxChecksumAlgorithm.Sha1,
                Value = "d6a770ba38583ed4bb4525bd96e50461655d2759"
            },
            Document = "http://spdx.org/spdxdocs/spdx-tools-v1.2-3F2504E0-4F89-41D3-9A0C-0305E82C3301"
        };

        // Act
        var json = Spdx2JsonSerializer.SerializeExternalDocumentReference(reference);

        // Assert
        SpdxJsonHelpers.AssertEqual("DocumentRef-spdx-tool-1.2", json["externalDocumentId"]);
        SpdxJsonHelpers.AssertEqual("SHA1", json["checksum"]?["algorithm"]);
        SpdxJsonHelpers.AssertEqual("d6a770ba38583ed4bb4525bd96e50461655d2759", json["checksum"]?["checksumValue"]);
        SpdxJsonHelpers.AssertEqual("http://spdx.org/spdxdocs/spdx-tools-v1.2-3F2504E0-4F89-41D3-9A0C-0305E82C3301",
            json["spdxDocument"]);
    }

    /// <summary>
    /// Tests serializing multiple external document references.
    /// </summary>
    [TestMethod]
    public void SerializeExternalDocumentReferences()
    {
        // Arrange
        var references = new[]
        {
            new SpdxExternalDocumentReference
            {
                ExternalDocumentId = "DocumentRef-spdx-tool-1.2",
                Checksum = new SpdxChecksum
                {
                    Algorithm = SpdxChecksumAlgorithm.Sha1,
                    Value = "d6a770ba38583ed4bb4525bd96e50461655d2759"
                },
                Document = "http://spdx.org/spdxdocs/spdx-tools-v1.2-3F2504E0-4F89-41D3-9A0C-0305E82C3301"
            }
        };

        // Act
        var json = Spdx2JsonSerializer.SerializeExternalDocumentReferences(references);

        // Assert
        Assert.IsNotNull(json);
        Assert.AreEqual(1, json.Count);
        SpdxJsonHelpers.AssertEqual("DocumentRef-spdx-tool-1.2", json[0]?["externalDocumentId"]);
        SpdxJsonHelpers.AssertEqual("SHA1", json[0]?["checksum"]?["algorithm"]);
        SpdxJsonHelpers.AssertEqual("d6a770ba38583ed4bb4525bd96e50461655d2759", json[0]?["checksum"]?["checksumValue"]);
        SpdxJsonHelpers.AssertEqual("http://spdx.org/spdxdocs/spdx-tools-v1.2-3F2504E0-4F89-41D3-9A0C-0305E82C3301",
            json[0]?["spdxDocument"]);
    }
}