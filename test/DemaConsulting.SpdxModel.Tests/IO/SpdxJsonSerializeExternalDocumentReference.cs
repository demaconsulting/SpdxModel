using DemaConsulting.SpdxModel.IO;

namespace DemaConsulting.SpdxModel.Tests.IO;

[TestClass]
public class SpdxJsonSerializeExternalDocumentReference
{
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
        var json = SpdxJsonSerializer.SerializeExternalDocumentReference(reference);

        // Assert
        Assert.AreEqual("DocumentRef-spdx-tool-1.2", json["externalDocumentId"]?.ToString());
        Assert.AreEqual("SHA1", json["checksum"]?["algorithm"]?.ToString());
        Assert.AreEqual("d6a770ba38583ed4bb4525bd96e50461655d2759", json["checksum"]?["checksumValue"]?.ToString());
        Assert.AreEqual("http://spdx.org/spdxdocs/spdx-tools-v1.2-3F2504E0-4F89-41D3-9A0C-0305E82C3301",
            json["spdxDocument"]?.ToString());
    }

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
        var json = SpdxJsonSerializer.SerializeExternalDocumentReferences(references);

        // Assert
        Assert.AreEqual(1, json.Count);
        Assert.AreEqual("DocumentRef-spdx-tool-1.2", json[0]?["externalDocumentId"]?.ToString());
        Assert.AreEqual("SHA1", json[0]?["checksum"]?["algorithm"]?.ToString());
        Assert.AreEqual("d6a770ba38583ed4bb4525bd96e50461655d2759", json[0]?["checksum"]?["checksumValue"]?.ToString());
        Assert.AreEqual("http://spdx.org/spdxdocs/spdx-tools-v1.2-3F2504E0-4F89-41D3-9A0C-0305E82C3301",
            json[0]?["spdxDocument"]?.ToString());
    }
}