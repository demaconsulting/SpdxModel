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
/// Tests for deserializing SPDX external references to <see cref="SpdxExternalReference"/> classes.
/// </summary>
[TestClass]
public class Spdx2JsonDeserializeExternalReference
{
    /// <summary>
    /// Tests deserializing an external reference.
    /// </summary>
    [TestMethod]
    public void DeserializeExternalReference()
    {
        // Arrange
        var json = new JsonObject
        {
            ["comment"] = "This is just an example",
            ["referenceLocator"] = "cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*",
            ["referenceType"] = "cpe23Type",
            ["referenceCategory"] = "SECURITY"
        };

        // Act
        var reference = Spdx2JsonDeserializer.DeserializeExternalReference(json);

        // Assert
        Assert.AreEqual("This is just an example", reference.Comment);
        Assert.AreEqual("cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*", reference.Locator);
        Assert.AreEqual("cpe23Type", reference.Type);
        Assert.AreEqual(SpdxReferenceCategory.Security, reference.Category);
    }

    /// <summary>
    /// Tests deserializing multiple external references.
    /// </summary>
    [TestMethod]
    public void DeserializeExternalReferences()
    {
        // Arrange
        var json = new JsonArray
        {
            new JsonObject
            {
                ["comment"] = "This is just an example",
                ["referenceLocator"] = "cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*",
                ["referenceType"] = "cpe23Type",
                ["referenceCategory"] = "SECURITY"
            },
            new JsonObject
            {
                ["comment"] = "This is the external ref for Acme",
                ["referenceLocator"] = "acmecorp/acmenator/4.1.3-alpha",
                ["referenceType"] =
                    "http://spdx.org/spdxdocs/spdx-example-444504E0-4F89-41D3-9A0C-0305E82C3301#LocationRef-acmeforge",
                ["referenceCategory"] = "OTHER"
            }
        };

        // Act
        var references = Spdx2JsonDeserializer.DeserializeExternalReferences(json);

        // Assert
        Assert.AreEqual(2, references.Length);
        Assert.AreEqual("This is just an example", references[0].Comment);
        Assert.AreEqual("cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*", references[0].Locator);
        Assert.AreEqual("cpe23Type", references[0].Type);
        Assert.AreEqual(SpdxReferenceCategory.Security, references[0].Category);
        Assert.AreEqual("This is the external ref for Acme", references[1].Comment);
        Assert.AreEqual("acmecorp/acmenator/4.1.3-alpha", references[1].Locator);
        Assert.AreEqual(
            "http://spdx.org/spdxdocs/spdx-example-444504E0-4F89-41D3-9A0C-0305E82C3301#LocationRef-acmeforge",
            references[1].Type);
        Assert.AreEqual(SpdxReferenceCategory.Other, references[1].Category);
    }
}