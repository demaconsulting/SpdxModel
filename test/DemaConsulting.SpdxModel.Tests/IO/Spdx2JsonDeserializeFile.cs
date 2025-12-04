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
///     Tests for deserializing SPDX files to <see cref="SpdxFile" /> classes.
/// </summary>
[TestClass]
public class Spdx2JsonDeserializeFile
{
    /// <summary>
    ///     Tests deserializing a file.
    /// </summary>
    [TestMethod]
    public void Spdx2JsonDeserializer_DeserializeFile_CorrectResults()
    {
        // Arrange: Create a JSON object representing a file
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

        // Act: Deserialize the JSON object to an SpdxFile object
        var file = Spdx2JsonDeserializer.DeserializeFile(json);

        // Assert: Verify the deserialized object has the expected properties
        Assert.AreEqual("SPDXRef-File", file.Id);
        Assert.AreEqual("src/DemaConsulting.SpdxModel/SpdxFile.cs", file.FileName);
        Assert.HasCount(1, file.FileTypes);
        Assert.AreEqual(SpdxFileType.Source, file.FileTypes[0]);
        Assert.HasCount(1, file.Checksums);
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, file.Checksums[0].Algorithm);
        Assert.AreEqual("2fd4e1c67a2d28fced849ee1bb76e7391b93eb12", file.Checksums[0].Value);
        Assert.AreEqual("MIT", file.ConcludedLicense);
        Assert.HasCount(1, file.LicenseInfoInFiles);
        Assert.AreEqual("MIT", file.LicenseInfoInFiles[0]);
        Assert.AreEqual("This is the MIT license", file.LicenseComments);
        Assert.AreEqual("This is a comment", file.Comment);
        Assert.AreEqual("This is a notice", file.Notice);
    }

    /// <summary>
    ///     Tests deserializing multiple files.
    /// </summary>
    [TestMethod]
    public void Spdx2JsonDeserializer_DeserializeFiles_CorrectResults()
    {
        // Arrange: Create a JSON array representing multiple files
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

        // Act: Deserialize the JSON array to an array of SpdxFile objects
        var files = Spdx2JsonDeserializer.DeserializeFiles(json);

        // Assert: Verify the deserialized array has the expected properties
        Assert.HasCount(1, files);
        Assert.AreEqual("SPDXRef-File", files[0].Id);
        Assert.AreEqual("src/DemaConsulting.SpdxModel/SpdxFile.cs", files[0].FileName);
        Assert.HasCount(1, files[0].FileTypes);
        Assert.AreEqual(SpdxFileType.Source, files[0].FileTypes[0]);
        Assert.HasCount(1, files[0].Checksums);
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, files[0].Checksums[0].Algorithm);
        Assert.AreEqual("2fd4e1c67a2d28fced849ee1bb76e7391b93eb12", files[0].Checksums[0].Value);
        Assert.AreEqual("MIT", files[0].ConcludedLicense);
        Assert.HasCount(1, files[0].LicenseInfoInFiles);
        Assert.AreEqual("MIT", files[0].LicenseInfoInFiles[0]);
        Assert.AreEqual("This is the MIT license", files[0].LicenseComments);
        Assert.AreEqual("This is a comment", files[0].Comment);
        Assert.AreEqual("This is a notice", files[0].Notice);
    }
}