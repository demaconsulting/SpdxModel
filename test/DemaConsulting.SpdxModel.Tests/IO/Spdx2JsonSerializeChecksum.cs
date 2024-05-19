using DemaConsulting.SpdxModel.IO;

namespace DemaConsulting.SpdxModel.Tests.IO;

[TestClass]
public class Spdx2JsonSerializeChecksum
{
    [TestMethod]
    public void SerializeChecksum()
    {
        // Arrange
        var checksum = new SpdxChecksum
        {
            Algorithm = SpdxChecksumAlgorithm.Sha1,
            Value = "2fd4e1c67a2d28f123849ee1bb76e7391b93eb12"
        };

        // Act
        var json = Spdx2JsonSerializer.SerializeChecksum(checksum);

        // Assert
        Assert.IsNotNull(json);
        Assert.AreEqual("SHA1", json["algorithm"]?.ToString());
        Assert.AreEqual("2fd4e1c67a2d28f123849ee1bb76e7391b93eb12", json["checksumValue"]?.ToString());
    }

    [TestMethod]
    public void SerializeChecksums()
    {
        // Arrange
        var checksums = new[]
        {
            new SpdxChecksum
            {
                Algorithm = SpdxChecksumAlgorithm.Sha1,
                Value = "2fd4e1c67a2d28f123849ee1bb76e7391b93eb12"
            },
            new SpdxChecksum
            {
                Algorithm = SpdxChecksumAlgorithm.Md5,
                Value = "d41d8cd98f00b204e9800998ecf8427e"
            }
        };

        // Act
        var json = Spdx2JsonSerializer.SerializeChecksums(checksums);

        // Assert
        Assert.IsNotNull(json);
        Assert.AreEqual(2, json.Count);
        Assert.AreEqual("SHA1", json[0]?["algorithm"]?.ToString());
        Assert.AreEqual("2fd4e1c67a2d28f123849ee1bb76e7391b93eb12", json[0]?["checksumValue"]?.ToString());
        Assert.AreEqual("MD5", json[1]?["algorithm"]?.ToString());
        Assert.AreEqual("d41d8cd98f00b204e9800998ecf8427e", json[1]?["checksumValue"]?.ToString());
    }
}