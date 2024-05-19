using DemaConsulting.SpdxModel.IO;

namespace DemaConsulting.SpdxModel.Tests.IO;

[TestClass]
public class Spdx2JsonSerializeExtractedLicensingInfo
{
    [TestMethod]
    public void SerializeExtractedLicensingInfo()
    {
        // Arrange
        var info = new SpdxExtractedLicensingInfo
        {
            LicenseId = "MIT",
            ExtractedText = "This is the MIT license",
            Name = "MIT License",
            CrossReferences = new[] { "https://opensource.org/licenses/MIT" },
            Comment = "This is a comment"
        };

        // Act
        var json = Spdx2JsonSerializer.SerializeExtractedLicensingInfo(info);

        // Assert
        Assert.AreEqual("MIT", json["licenseId"]?.ToString());
        Assert.AreEqual("This is the MIT license", json["extractedText"]?.ToString());
        Assert.AreEqual("MIT License", json["name"]?.ToString());
        Assert.AreEqual("https://opensource.org/licenses/MIT", json["seeAlsos"]?[0]?.ToString());
        Assert.AreEqual("This is a comment", json["comment"]?.ToString());
    }

    [TestMethod]
    public void SerializeExtractedLicensingInfos()
    {
        // Arrange
        var info = new[]
        {
            new SpdxExtractedLicensingInfo
            {
                LicenseId = "MIT",
                ExtractedText = "This is the MIT license",
                Name = "MIT License",
                CrossReferences = new[] { "https://opensource.org/licenses/MIT" },
                Comment = "This is a comment"
            }
        };

        // Act
        var json = Spdx2JsonSerializer.SerializeExtractedLicensingInfos(info);

        // Assert
        Assert.AreEqual(1, json.Count);
        Assert.AreEqual("MIT", json[0]?["licenseId"]?.ToString());
        Assert.AreEqual("This is the MIT license", json[0]?["extractedText"]?.ToString());
        Assert.AreEqual("MIT License", json[0]?["name"]?.ToString());
        Assert.AreEqual("https://opensource.org/licenses/MIT", json[0]?["seeAlsos"]?[0]?.ToString());
        Assert.AreEqual("This is a comment", json[0]?["comment"]?.ToString());
    }
}