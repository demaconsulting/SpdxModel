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
/// <remarks>
///     Exercises deserialization of SPDX extracted licensing information elements using
///     xUnit v3 as the test framework. Each test constructs
///     inline JSON and verifies the resulting <see cref="SpdxExtractedLicensingInfo"/> fields.
/// </remarks>
public class Spdx2JsonDeserializeExtractedLicensingInfo
{
    /// <summary>
    ///     Tests deserializing an extracted licensing information.
    /// </summary>
    /// <remarks>
    ///     Verifies that licenseId, extractedText, name, seeAlsos (cross-references), and
    ///     comment JSON fields are correctly mapped to the
    ///     <see cref="SpdxExtractedLicensingInfo"/> properties when a single object is
    ///     deserialized.
    /// </remarks>
    [Fact]
    public void Spdx2JsonDeserializer_DeserializeExtractedLicensingInfo_ValidInput_CorrectResults()
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
        Assert.Equal("MIT", extractedLicensingInfo.LicenseId);
        Assert.Equal("This is the MIT license", extractedLicensingInfo.ExtractedText);
        Assert.Equal("MIT License", extractedLicensingInfo.Name);
        Assert.Single(extractedLicensingInfo.CrossReferences);
        Assert.Equal("https://opensource.org/licenses/MIT", extractedLicensingInfo.CrossReferences[0]);
        Assert.Equal("This is a comment", extractedLicensingInfo.Comment);
    }

    /// <summary>
    ///     Tests deserializing multiple extracted licensing information.
    /// </summary>
    /// <remarks>
    ///     Verifies that a JSON array containing one extracted licensing info object is
    ///     deserialized to a single-element array with all fields correctly populated.
    /// </remarks>
    [Fact]
    public void Spdx2JsonDeserializer_DeserializeExtractedLicensingInfos_ValidInput_CorrectResults()
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
        Assert.Single(extractedLicensingInfos);
        Assert.Equal("MIT", extractedLicensingInfos[0].LicenseId);
        Assert.Equal("This is the MIT license", extractedLicensingInfos[0].ExtractedText);
        Assert.Equal("MIT License", extractedLicensingInfos[0].Name);
        Assert.Single(extractedLicensingInfos[0].CrossReferences);
        Assert.Equal("https://opensource.org/licenses/MIT", extractedLicensingInfos[0].CrossReferences[0]);
        Assert.Equal("This is a comment", extractedLicensingInfos[0].Comment);
    }
}
