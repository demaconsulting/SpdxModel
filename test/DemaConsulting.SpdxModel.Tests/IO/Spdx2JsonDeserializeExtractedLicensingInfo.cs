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
///     Tests for deserializing SPDX extracted licensing information to <see cref="SpdxExtractedLicensingInfo" /> classes.
/// </summary>
[TestClass]
public class Spdx2JsonDeserializeExtractedLicensingInfo
{
    /// <summary>
    ///     Tests deserializing an extracted licensing information.
    /// </summary>
    [TestMethod]
    public void Spdx2JsonDeserializer_DeserializeExtractedLicensingInfo_CorrectResults()
    {
        // Arrange: Create a JSON object representing extracted licensing information
        var json = new JsonObject
        {
            ["licenseId"] = "MIT",
            ["extractedText"] = "This is the MIT license",
            ["name"] = "MIT License",
            ["seeAlsos"] = new JsonArray { "https://opensource.org/licenses/MIT" },
            ["comment"] = "This is a comment"
        };

        // Act: Deserialize the JSON object to an SpdxExtractedLicensingInfo object
        var extractedLicensingInfo = Spdx2JsonDeserializer.DeserializeExtractedLicensingInfo(json);

        // Assert: Verify the deserialized object has the expected properties
        Assert.AreEqual("MIT", extractedLicensingInfo.LicenseId);
        Assert.AreEqual("This is the MIT license", extractedLicensingInfo.ExtractedText);
        Assert.AreEqual("MIT License", extractedLicensingInfo.Name);
        Assert.HasCount(1, extractedLicensingInfo.CrossReferences);
        Assert.AreEqual("https://opensource.org/licenses/MIT", extractedLicensingInfo.CrossReferences[0]);
        Assert.AreEqual("This is a comment", extractedLicensingInfo.Comment);
    }

    /// <summary>
    ///     Tests deserializing multiple extracted licensing information.
    /// </summary>
    [TestMethod]
    public void Spdx2JsonDeserializer_DeserializeExtractedLicensingInfos_CorrectResults()
    {
        // Arrange: Create a JSON array representing multiple extracted licensing information
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

        // Act: Deserialize the JSON array to an array of SpdxExtractedLicensingInfo objects
        var extractedLicensingInfos = Spdx2JsonDeserializer.DeserializeExtractedLicensingInfos(json);

        // Assert: Verify the deserialized array has the expected properties
        Assert.HasCount(1, extractedLicensingInfos);
        Assert.AreEqual("MIT", extractedLicensingInfos[0].LicenseId);
        Assert.AreEqual("This is the MIT license", extractedLicensingInfos[0].ExtractedText);
        Assert.AreEqual("MIT License", extractedLicensingInfos[0].Name);
        Assert.HasCount(1, extractedLicensingInfos[0].CrossReferences);
        Assert.AreEqual("https://opensource.org/licenses/MIT", extractedLicensingInfos[0].CrossReferences[0]);
        Assert.AreEqual("This is a comment", extractedLicensingInfos[0].Comment);
    }
}
