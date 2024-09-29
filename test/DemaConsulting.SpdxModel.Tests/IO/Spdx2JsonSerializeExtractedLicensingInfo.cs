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
/// Tests for serializing <see cref="SpdxExtractedLicensingInfo"/> to JSON.
/// </summary>
[TestClass]
public class Spdx2JsonSerializeExtractedLicensingInfo
{
    /// <summary>
    /// Tests serializing an extracted licensing info.
    /// </summary>
    [TestMethod]
    public void SerializeExtractedLicensingInfo()
    {
        // Arrange
        var info = new SpdxExtractedLicensingInfo
        {
            LicenseId = "MIT",
            ExtractedText = "This is the MIT license",
            Name = "MIT License",
            CrossReferences = ["https://opensource.org/licenses/MIT"],
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

    /// <summary>
    /// Tests serializing multiple extracted licensing infos.
    /// </summary>
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
                CrossReferences = ["https://opensource.org/licenses/MIT"],
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