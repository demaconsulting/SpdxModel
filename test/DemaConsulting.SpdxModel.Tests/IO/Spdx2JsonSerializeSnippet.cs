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
///     Tests for serializing <see cref="SpdxSnippet" /> to JSON.
/// </summary>
[TestClass]
public class Spdx2JsonSerializeSnippet
{
    /// <summary>
    ///     Tests serializing a snippet.
    /// </summary>
    [TestMethod]
    public void Spdx2JsonSerializer_SerializeSnippet_CorrectResults()
    {
        // Arrange: Create a sample SpdxSnippet object
        var snippet = new SpdxSnippet
        {
            Id = "SPDXRef-Snippet",
            SnippetFromFile = "SnippetFromFile",
            SnippetByteStart = 1,
            SnippetByteEnd = 2,
            SnippetLineStart = 3,
            SnippetLineEnd = 4,
            ConcludedLicense = "ConcludedLicense",
            LicenseInfoInSnippet = ["LicenseInfoInSnippet"],
            LicenseComments = "LicenseComments",
            CopyrightText = "Copyright",
            Comment = "Comment",
            Name = "Name",
            AttributionText = ["AttributionText"]
        };

        // Act: Serialize the snippet to JSON
        var json = Spdx2JsonSerializer.SerializeSnippet(snippet);

        // Assert: Verify the JSON is not null and has the expected structure
        Assert.IsNotNull(json);
        SpdxJsonHelpers.AssertEqual("SPDXRef-Snippet", json["SPDXID"]);
        SpdxJsonHelpers.AssertEqual("SnippetFromFile", json["snippetFromFile"]);
        SpdxJsonHelpers.AssertEqual("Name", json["name"]);
        SpdxJsonHelpers.AssertEqual("ConcludedLicense", json["licenseConcluded"]);
        SpdxJsonHelpers.AssertEqual("LicenseInfoInSnippet", json["licenseInfoInSnippets"]?[0]);
        SpdxJsonHelpers.AssertEqual("LicenseComments", json["licenseComments"]);
        SpdxJsonHelpers.AssertEqual("Copyright", json["copyrightText"]);
        SpdxJsonHelpers.AssertEqual("Comment", json["comment"]);
        SpdxJsonHelpers.AssertEqual("AttributionText", json["attributionTexts"]?[0]);
        SpdxJsonHelpers.AssertEqual("SnippetFromFile", json["ranges"]?[0]?["endPointer"]?["reference"]);
        SpdxJsonHelpers.AssertEqual("2", json["ranges"]?[0]?["endPointer"]?["offset"]);
        SpdxJsonHelpers.AssertEqual("SnippetFromFile", json["ranges"]?[0]?["startPointer"]?["reference"]);
        SpdxJsonHelpers.AssertEqual("1", json["ranges"]?[0]?["startPointer"]?["offset"]);
        SpdxJsonHelpers.AssertEqual("SnippetFromFile", json["ranges"]?[1]?["endPointer"]?["reference"]);
        SpdxJsonHelpers.AssertEqual("4", json["ranges"]?[1]?["endPointer"]?["lineNumber"]);
        SpdxJsonHelpers.AssertEqual("SnippetFromFile", json["ranges"]?[1]?["startPointer"]?["reference"]);
        SpdxJsonHelpers.AssertEqual("3", json["ranges"]?[1]?["startPointer"]?["lineNumber"]);
    }

    /// <summary>
    ///     Tests serializing multiple snippets.
    /// </summary>
    [TestMethod]
    public void Spdx2JsonSerializer_SerializeSnippets_CorrectResults()
    {
        // Arrange: Create a sample array of SpdxSnippet objects
        var snippets = new[]
        {
            new SpdxSnippet
            {
                Id = "SPDXRef-Snippet",
                SnippetFromFile = "SnippetFromFile",
                SnippetByteStart = 1,
                SnippetByteEnd = 2,
                SnippetLineStart = 3,
                SnippetLineEnd = 4,
                ConcludedLicense = "ConcludedLicense",
                LicenseInfoInSnippet = ["LicenseInfoInSnippet"],
                LicenseComments = "LicenseComments",
                CopyrightText = "Copyright",
                Comment = "Comment",
                Name = "Name",
                AttributionText = ["AttributionText"]
            }
        };

        // Act: Serialize the array of snippets to JSON
        var json = Spdx2JsonSerializer.SerializeSnippets(snippets);

        // Assert: Verify the JSON is not null and has the expected structure
        Assert.IsNotNull(json);
        Assert.AreEqual(1, json.Count);
        SpdxJsonHelpers.AssertEqual("SPDXRef-Snippet", json[0]?["SPDXID"]);
        SpdxJsonHelpers.AssertEqual("SnippetFromFile", json[0]?["snippetFromFile"]);
        SpdxJsonHelpers.AssertEqual("Name", json[0]?["name"]);
        SpdxJsonHelpers.AssertEqual("ConcludedLicense", json[0]?["licenseConcluded"]);
        SpdxJsonHelpers.AssertEqual("LicenseInfoInSnippet", json[0]?["licenseInfoInSnippets"]?[0]);
        SpdxJsonHelpers.AssertEqual("LicenseComments", json[0]?["licenseComments"]);
        SpdxJsonHelpers.AssertEqual("Copyright", json[0]?["copyrightText"]);
        SpdxJsonHelpers.AssertEqual("Comment", json[0]?["comment"]);
        SpdxJsonHelpers.AssertEqual("AttributionText", json[0]?["attributionTexts"]?[0]);
        SpdxJsonHelpers.AssertEqual("SnippetFromFile", json[0]?["ranges"]?[0]?["endPointer"]?["reference"]);
        SpdxJsonHelpers.AssertEqual("2", json[0]?["ranges"]?[0]?["endPointer"]?["offset"]);
        SpdxJsonHelpers.AssertEqual("SnippetFromFile", json[0]?["ranges"]?[0]?["startPointer"]?["reference"]);
        SpdxJsonHelpers.AssertEqual("1", json[0]?["ranges"]?[0]?["startPointer"]?["offset"]);
        SpdxJsonHelpers.AssertEqual("SnippetFromFile", json[0]?["ranges"]?[1]?["endPointer"]?["reference"]);
        SpdxJsonHelpers.AssertEqual("4", json[0]?["ranges"]?[1]?["endPointer"]?["lineNumber"]);
        SpdxJsonHelpers.AssertEqual("SnippetFromFile", json[0]?["ranges"]?[1]?["startPointer"]?["reference"]);
        SpdxJsonHelpers.AssertEqual("3", json[0]?["ranges"]?[1]?["startPointer"]?["lineNumber"]);
    }
}
