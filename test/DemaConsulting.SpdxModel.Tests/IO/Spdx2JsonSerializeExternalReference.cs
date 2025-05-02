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
/// Tests for serializing <see cref="SpdxExternalReference"/> to JSON.
/// </summary>
[TestClass]
public class Spdx2JsonSerializeExternalReference
{
    /// <summary>
    /// Tests serializing an external reference.
    /// </summary>
    [TestMethod]
    public void SerializeExternalReference()
    {
        // Arrange
        var reference = new SpdxExternalReference
        {
            Category = SpdxReferenceCategory.Security,
            Locator = "cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*",
            Type = "cpe23Type",
            Comment = "Example Comment"
        };

        // Act
        var json = Spdx2JsonSerializer.SerializeExternalReference(reference);

        // Assert
        Assert.IsNotNull(json);
        SpdxJsonHelpers.AssertEqual("SECURITY", json["referenceCategory"]);
        SpdxJsonHelpers.AssertEqual("cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*",
            json["referenceLocator"]);
        SpdxJsonHelpers.AssertEqual("cpe23Type", json["referenceType"]);
        SpdxJsonHelpers.AssertEqual("Example Comment", json["comment"]);
    }

    /// <summary>
    /// Tests serializing multiple external references.
    /// </summary>
    [TestMethod]
    public void SerializeExternalReferences()
    {
        // Arrange
        var references = new[]
        {
            new SpdxExternalReference
            {
                Category = SpdxReferenceCategory.Security,
                Locator = "cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*",
                Type = "cpe23Type",
                Comment = "Example Comment"
            },
            new SpdxExternalReference
            {
                Category = SpdxReferenceCategory.Other,
                Locator = "acmecorp/acmenator/4.1.3-alpha",
                Type =
                    "http://spdx.org/spdxdocs/spdx-example-444504E0-4F89-41D3-9A0C-0305E82C3301#LocationRef-acmeforge",
                Comment = "This is the external ref for Acme"
            }
        };

        // Act
        var json = Spdx2JsonSerializer.SerializeExternalReferences(references);

        // Assert
        Assert.IsNotNull(json);
        Assert.AreEqual(2, json.Count);
        SpdxJsonHelpers.AssertEqual("SECURITY", json[0]?["referenceCategory"]);
        SpdxJsonHelpers.AssertEqual("cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*",
            json[0]?["referenceLocator"]);
        SpdxJsonHelpers.AssertEqual("cpe23Type", json[0]?["referenceType"]);
        SpdxJsonHelpers.AssertEqual("Example Comment", json[0]?["comment"]);
        SpdxJsonHelpers.AssertEqual("OTHER", json[1]?["referenceCategory"]);
        SpdxJsonHelpers.AssertEqual("acmecorp/acmenator/4.1.3-alpha", json[1]?["referenceLocator"]);
        SpdxJsonHelpers.AssertEqual(
            "http://spdx.org/spdxdocs/spdx-example-444504E0-4F89-41D3-9A0C-0305E82C3301#LocationRef-acmeforge",
            json[1]?["referenceType"]);
        SpdxJsonHelpers.AssertEqual("This is the external ref for Acme", json[1]?["comment"]);
    }
}