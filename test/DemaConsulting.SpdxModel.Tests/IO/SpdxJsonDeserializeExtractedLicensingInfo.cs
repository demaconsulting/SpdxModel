using System.Text.Json.Nodes;
using DemaConsulting.SpdxModel.IO;

namespace DemaConsulting.SpdxModel.Tests.IO;

[TestClass]
public class SpdxJsonDeserializeExtractedLicensingInfo
{
    [TestMethod]
    public void DeserializeExtractedLicensingInfo()
    {
        // Arrange
        var json = new JsonObject
        {
            ["licenseId"] = "MIT",
            ["extractedText"] = "This is the MIT license",
            ["name"] = "MIT License",
            ["seeAlsos"] = new JsonArray { "https://opensource.org/licenses/MIT" },
            ["comment"] = "This is a comment"
        };

        // Act
        var extractedLicensingInfo = SpdxJsonDeserializer.DeserializeExtractedLicensingInfo(json);

        // Assert
        Assert.AreEqual("MIT", extractedLicensingInfo.LicenseId);
        Assert.AreEqual("This is the MIT license", extractedLicensingInfo.ExtractedText);
        Assert.AreEqual("MIT License", extractedLicensingInfo.Name);
        Assert.AreEqual(1, extractedLicensingInfo.CrossReferences.Length);
        Assert.AreEqual("https://opensource.org/licenses/MIT", extractedLicensingInfo.CrossReferences[0]);
        Assert.AreEqual("This is a comment", extractedLicensingInfo.Comment);
    }

    [TestMethod]
    public void DeserializeExtractedLicensingInfos()
    {
        // Arrange
        var json = new JsonArray
        {
            new JsonObject
            {
                ["licenseId"] = "MIT",
                ["extractedText"] = "This is the MIT license",
                ["name"] = "MIT License",
                ["seeAlsos"] = new JsonArray { "https://opensource.org/licenses/MIT" },
                ["comment"] = "This is a comment"
            }
        };

        // Act
        var extractedLicensingInfos = SpdxJsonDeserializer.DeserializeExtractedLicensingInfos(json);

        // Assert
        Assert.AreEqual(1, extractedLicensingInfos.Length);
        Assert.AreEqual("MIT", extractedLicensingInfos[0].LicenseId);
        Assert.AreEqual("This is the MIT license", extractedLicensingInfos[0].ExtractedText);
        Assert.AreEqual("MIT License", extractedLicensingInfos[0].Name);
        Assert.AreEqual(1, extractedLicensingInfos[0].CrossReferences.Length);
        Assert.AreEqual("https://opensource.org/licenses/MIT", extractedLicensingInfos[0].CrossReferences[0]);
        Assert.AreEqual("This is a comment", extractedLicensingInfos[0].Comment);
    }
}