using System.Text.Json.Nodes;
using DemaConsulting.SpdxModel.IO;

namespace DemaConsulting.SpdxModel.Tests.IO;

[TestClass]
public class SpdxJsonDeserializeChecksum
{
    [TestMethod]
    public void DeserializeChecksum()
    {
        // Arrange
        var json = new JsonObject
        {
            ["algorithm"] = "SHA1",
            ["checksumValue"] = "2fd4e1c67a2d28f123849ee1bb76e7391b93eb12"
        };

        // Act
        var checksum = SpdxJsonDeserializer.DeserializeChecksum(json);

        // Assert
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, checksum.Algorithm);
        Assert.AreEqual("2fd4e1c67a2d28f123849ee1bb76e7391b93eb12", checksum.Value);
    }

    [TestMethod]
    public void DeserializeChecksums()
    {
        // Arrange
        var json = new JsonArray
        {
            new JsonObject
            {
                ["algorithm"] = "SHA1",
                ["checksumValue"] = "2fd4e1c67a2d28f123849ee1bb76e7391b93eb12"
            },
            new JsonObject
            {
                ["algorithm"] = "MD5",
                ["checksumValue"] = "d41d8cd98f00b204e9800998ecf8427e"
            }
        };

        // Act
        var checksums = SpdxJsonDeserializer.DeserializeChecksums(json);

        // Assert
        Assert.AreEqual(2, checksums.Length);
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, checksums[0].Algorithm);
        Assert.AreEqual("2fd4e1c67a2d28f123849ee1bb76e7391b93eb12", checksums[0].Value);
        Assert.AreEqual(SpdxChecksumAlgorithm.Md5, checksums[1].Algorithm);
        Assert.AreEqual("d41d8cd98f00b204e9800998ecf8427e", checksums[1].Value);
    }
}