using System.Text.Json.Nodes;
using DemaConsulting.SpdxModel.IO;

namespace DemaConsulting.SpdxModel.Tests.IO;

[TestClass]
public class SpdxJsonDeserializeExternalDocumentReference
{
    [TestMethod]
    public void DeserializeExternalDocumentReference()
    {
        // Arrange
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

        // Act
        var externalDocumentReference = SpdxJsonDeserializer.DeserializeExternalDocumentReference(json);

        // Assert
        Assert.AreEqual("DocumentRef-1", externalDocumentReference.ExternalDocumentId);
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, externalDocumentReference.Checksum.Algorithm);
        Assert.AreEqual("d6a770ba38583ed4bb4525bd96e50461655d2759", externalDocumentReference.Checksum.Value);
        Assert.AreEqual("SPDXRef-Document", externalDocumentReference.Document);
    }

    [TestMethod]
    public void DeserializeExternalDocumentReferences()
    {
        // Arrange
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

        // Act
        var externalDocumentReferences = SpdxJsonDeserializer.DeserializeExternalDocumentReferences(json);

        // Assert
        Assert.AreEqual(1, externalDocumentReferences.Length);
        Assert.AreEqual("DocumentRef-1", externalDocumentReferences[0].ExternalDocumentId);
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, externalDocumentReferences[0].Checksum.Algorithm);
        Assert.AreEqual("d6a770ba38583ed4bb4525bd96e50461655d2759", externalDocumentReferences[0].Checksum.Value);
        Assert.AreEqual("SPDXRef-Document", externalDocumentReferences[0].Document);
    }
}