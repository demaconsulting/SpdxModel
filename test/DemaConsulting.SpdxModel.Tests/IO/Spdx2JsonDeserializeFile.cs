using System.Text.Json.Nodes;
using DemaConsulting.SpdxModel.IO;

namespace DemaConsulting.SpdxModel.Tests.IO;

[TestClass]
public class Spdx2JsonDeserializeFile
{
    [TestMethod]
    public void DeserializeFile()
    {
        // Arrange
        var json = new JsonObject
        {
            ["SPDXID"] = "SPDXRef-File",
            ["fileName"] = "src/DemaConsulting.SpdxModel/SpdxFile.cs",
            ["fileTypes"] = new JsonArray { "SOURCE" },
            ["checksums"] = new JsonArray
            {
                new JsonObject
                {
                    ["algorithm"] = "SHA1",
                    ["checksumValue"] = "2fd4e1c67a2d28fced849ee1bb76e7391b93eb12"
                }
            },
            ["licenseConcluded"] = "MIT",
            ["licenseInfoInFiles"] = new JsonArray { "MIT" },
            ["licenseComments"] = "This is the MIT license",
            ["comment"] = "This is a comment",
            ["noticeText"] = "This is a notice"
        };

        // Act
        var file = Spdx2JsonDeserializer.DeserializeFile(json);

        // Assert
        Assert.AreEqual("SPDXRef-File", file.Id);
        Assert.AreEqual("src/DemaConsulting.SpdxModel/SpdxFile.cs", file.FileName);
        Assert.AreEqual(1, file.FileTypes.Length);
        Assert.AreEqual(SpdxFileType.Source, file.FileTypes[0]);
        Assert.AreEqual(1, file.Checksums.Length);
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, file.Checksums[0].Algorithm);
        Assert.AreEqual("2fd4e1c67a2d28fced849ee1bb76e7391b93eb12", file.Checksums[0].Value);
        Assert.AreEqual("MIT", file.ConcludedLicense);
        Assert.AreEqual(1, file.LicenseInfoInFiles.Length);
        Assert.AreEqual("MIT", file.LicenseInfoInFiles[0]);
        Assert.AreEqual("This is the MIT license", file.LicenseComments);
        Assert.AreEqual("This is a comment", file.Comment);
        Assert.AreEqual("This is a notice", file.Notice);
    }

    [TestMethod]
    public void DeserializeFiles()
    {
        // Arrange
        var json = new JsonArray
        {
            new JsonObject
            {
                ["SPDXID"] = "SPDXRef-File",
                ["fileName"] = "src/DemaConsulting.SpdxModel/SpdxFile.cs",
                ["fileTypes"] = new JsonArray { "SOURCE" },
                ["checksums"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["algorithm"] = "SHA1",
                        ["checksumValue"] = "2fd4e1c67a2d28fced849ee1bb76e7391b93eb12"
                    }
                },
                ["licenseConcluded"] = "MIT",
                ["licenseInfoInFiles"] = new JsonArray { "MIT" },
                ["licenseComments"] = "This is the MIT license",
                ["comment"] = "This is a comment",
                ["noticeText"] = "This is a notice"
            }
        };

        // Act
        var files = Spdx2JsonDeserializer.DeserializeFiles(json);

        // Assert
        Assert.AreEqual(1, files.Length);
        Assert.AreEqual("SPDXRef-File", files[0].Id);
        Assert.AreEqual("src/DemaConsulting.SpdxModel/SpdxFile.cs", files[0].FileName);
        Assert.AreEqual(1, files[0].FileTypes.Length);
        Assert.AreEqual(SpdxFileType.Source, files[0].FileTypes[0]);
        Assert.AreEqual(1, files[0].Checksums.Length);
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, files[0].Checksums[0].Algorithm);
        Assert.AreEqual("2fd4e1c67a2d28fced849ee1bb76e7391b93eb12", files[0].Checksums[0].Value);
        Assert.AreEqual("MIT", files[0].ConcludedLicense);
        Assert.AreEqual(1, files[0].LicenseInfoInFiles.Length);
        Assert.AreEqual("MIT", files[0].LicenseInfoInFiles[0]);
        Assert.AreEqual("This is the MIT license", files[0].LicenseComments);
        Assert.AreEqual("This is a comment", files[0].Comment);
        Assert.AreEqual("This is a notice", files[0].Notice);
    }
}