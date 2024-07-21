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

[TestClass]
public class Spdx2JsonSerializeSnippet
{
    [TestMethod]
    public void SerializeSnippet()
    {
        // Arrange
        var snippet = new SpdxSnippet
        {
            Id = "SPDXRef-Snippet",
            SnippetFromFile = "SnippetFromFile",
            SnippetByteStart = 1,
            SnippetByteEnd = 2,
            SnippetLineStart = 3,
            SnippetLineEnd = 4,
            ConcludedLicense = "ConcludedLicense",
            LicenseInfoInSnippet = new[] { "LicenseInfoInSnippet" },
            LicenseComments = "LicenseComments",
            CopyrightText = "Copyright",
            Comment = "Comment",
            Name = "Name",
            AttributionText = new[] { "AttributionText" }
        };

        // Act
        var json = Spdx2JsonSerializer.SerializeSnippet(snippet);

        // Assert
        Assert.IsNotNull(json);
        Assert.AreEqual("SPDXRef-Snippet", json["SPDXID"]?.ToString());
        Assert.AreEqual("SnippetFromFile", json["snippetFromFile"]?.ToString());
        Assert.AreEqual("Name", json["name"]?.ToString());
        Assert.AreEqual("ConcludedLicense", json["licenseConcluded"]?.ToString());
        Assert.AreEqual("LicenseInfoInSnippet", json["licenseInfoInSnippets"]?[0]?.ToString());
        Assert.AreEqual("LicenseComments", json["licenseComments"]?.ToString());
        Assert.AreEqual("Copyright", json["copyrightText"]?.ToString());
        Assert.AreEqual("Comment", json["comment"]?.ToString());
        Assert.AreEqual("AttributionText", json["attributionTexts"]?[0]?.ToString());
        Assert.AreEqual("SnippetFromFile", json["ranges"]?[0]?["endPointer"]?["reference"]?.ToString());
        Assert.AreEqual("2", json["ranges"]?[0]?["endPointer"]?["offset"]?.ToString());
        Assert.AreEqual("SnippetFromFile", json["ranges"]?[0]?["startPointer"]?["reference"]?.ToString());
        Assert.AreEqual("1", json["ranges"]?[0]?["startPointer"]?["offset"]?.ToString());
        Assert.AreEqual("SnippetFromFile", json["ranges"]?[1]?["endPointer"]?["reference"]?.ToString());
        Assert.AreEqual("4", json["ranges"]?[1]?["endPointer"]?["lineNumber"]?.ToString());
        Assert.AreEqual("SnippetFromFile", json["ranges"]?[1]?["startPointer"]?["reference"]?.ToString());
        Assert.AreEqual("3", json["ranges"]?[1]?["startPointer"]?["lineNumber"]?.ToString());
    }

    [TestMethod]
    public void SerializeSnippets()
    {
        // Arrange
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
                LicenseInfoInSnippet = new[] { "LicenseInfoInSnippet" },
                LicenseComments = "LicenseComments",
                CopyrightText = "Copyright",
                Comment = "Comment",
                Name = "Name",
                AttributionText = new[] { "AttributionText" }
            }
        };

        // Act
        var json = Spdx2JsonSerializer.SerializeSnippets(snippets);

        // Assert
        Assert.IsNotNull(json);
        Assert.AreEqual(1, json.Count);
        Assert.AreEqual("SPDXRef-Snippet", json[0]?["SPDXID"]?.ToString());
        Assert.AreEqual("SnippetFromFile", json[0]?["snippetFromFile"]?.ToString());
        Assert.AreEqual("Name", json[0]?["name"]?.ToString());
        Assert.AreEqual("ConcludedLicense", json[0]?["licenseConcluded"]?.ToString());
        Assert.AreEqual("LicenseInfoInSnippet", json[0]?["licenseInfoInSnippets"]?[0]?.ToString());
        Assert.AreEqual("LicenseComments", json[0]?["licenseComments"]?.ToString());
        Assert.AreEqual("Copyright", json[0]?["copyrightText"]?.ToString());
        Assert.AreEqual("Comment", json[0]?["comment"]?.ToString());
        Assert.AreEqual("AttributionText", json[0]?["attributionTexts"]?[0]?.ToString());
        Assert.AreEqual("SnippetFromFile", json[0]?["ranges"]?[0]?["endPointer"]?["reference"]?.ToString());
        Assert.AreEqual("2", json[0]?["ranges"]?[0]?["endPointer"]?["offset"]?.ToString());
        Assert.AreEqual("SnippetFromFile", json[0]?["ranges"]?[0]?["startPointer"]?["reference"]?.ToString());
        Assert.AreEqual("1", json[0]?["ranges"]?[0]?["startPointer"]?["offset"]?.ToString());
        Assert.AreEqual("SnippetFromFile", json[0]?["ranges"]?[1]?["endPointer"]?["reference"]?.ToString());
        Assert.AreEqual("4", json[0]?["ranges"]?[1]?["endPointer"]?["lineNumber"]?.ToString());
        Assert.AreEqual("SnippetFromFile", json[0]?["ranges"]?[1]?["startPointer"]?["reference"]?.ToString());
        Assert.AreEqual("3", json[0]?["ranges"]?[1]?["startPointer"]?["lineNumber"]?.ToString());
    }
}