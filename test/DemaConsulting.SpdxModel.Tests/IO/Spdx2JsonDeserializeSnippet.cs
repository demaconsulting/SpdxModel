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
///     Tests for deserializing SPDX snippets to <see cref="SpdxSnippet" /> classes.
/// </summary>
[TestClass]
public class Spdx2JsonDeserializeSnippet
{
    /// <summary>
    ///     Tests deserializing a snippet.
    /// </summary>
    [TestMethod]
    public void Spdx2JsonDeserializer_DeserializeSnippet_CorrectResults()
    {
        // Arrange: Create a JSON object representing a snippet
        var json = new JsonObject
        {
            ["SPDXID"] = "SPDXRef-Snippet",
            ["comment"] =
                "This snippet was identified as significant and highlighted in this Apache-2.0 file, when a commercial scanner identified it as being derived from file foo.c in package xyz which is licensed under GPL-2.0.",
            ["copyrightText"] = "Copyright 2008-2010 John Smith",
            ["licenseComments"] =
                "The concluded license was taken from package xyz, from which the snippet was copied into the current file. The concluded license information was found in the COPYING.txt file in package xyz.",
            ["licenseConcluded"] = "GPL-2.0-only",
            ["licenseInfoInSnippets"] = new JsonArray
            {
                "GPL-2.0-only"
            },
            ["name"] = "from linux kernel",
            ["ranges"] = new JsonArray
            {
                new JsonObject
                {
                    ["endPointer"] = new JsonObject
                    {
                        ["offset"] = 420,
                        ["reference"] = "SPDXRef-DoapSource"
                    },
                    ["startPointer"] = new JsonObject
                    {
                        ["offset"] = 310,
                        ["reference"] = "SPDXRef-DoapSource"
                    }
                },
                new JsonObject
                {
                    ["endPointer"] = new JsonObject
                    {
                        ["lineNumber"] = 23,
                        ["reference"] = "SPDXRef-DoapSource"
                    },
                    ["startPointer"] = new JsonObject
                    {
                        ["lineNumber"] = 5,
                        ["reference"] = "SPDXRef-DoapSource"
                    }
                }
            },
            ["snippetFromFile"] = "SPDXRef-DoapSource"
        };

        // Act: Deserialize the JSON object to an SpdxSnippet object
        var snippet = Spdx2JsonDeserializer.DeserializeSnippet(json);

        // Assert: Verify the deserialized object has the expected properties
        Assert.AreEqual("SPDXRef-Snippet", snippet.Id);
        Assert.AreEqual(
            "This snippet was identified as significant and highlighted in this Apache-2.0 file, when a commercial scanner identified it as being derived from file foo.c in package xyz which is licensed under GPL-2.0.",
            snippet.Comment);
        Assert.AreEqual("Copyright 2008-2010 John Smith", snippet.CopyrightText);
        Assert.AreEqual(
            "The concluded license was taken from package xyz, from which the snippet was copied into the current file. The concluded license information was found in the COPYING.txt file in package xyz.",
            snippet.LicenseComments);
        Assert.AreEqual("GPL-2.0-only", snippet.ConcludedLicense);
        Assert.HasCount(1, snippet.LicenseInfoInSnippet);
        Assert.AreEqual("GPL-2.0-only", snippet.LicenseInfoInSnippet[0]);
        Assert.AreEqual("from linux kernel", snippet.Name);
        Assert.AreEqual(420, snippet.SnippetByteEnd);
        Assert.AreEqual(310, snippet.SnippetByteStart);
        Assert.AreEqual(23, snippet.SnippetLineEnd);
        Assert.AreEqual(5, snippet.SnippetLineStart);
        Assert.AreEqual("SPDXRef-DoapSource", snippet.SnippetFromFile);
    }

    /// <summary>
    ///     Tests deserializing multiple snippets.
    /// </summary>
    [TestMethod]
    public void Spdx2JsonDeserializer_DeserializeSnippets_CorrectResults()
    {
        // Arrange: Create a JSON array representing multiple snippets
        var json = new JsonArray
        {
            new JsonObject
            {
                ["SPDXID"] = "SPDXRef-Snippet",
                ["comment"] =
                    "This snippet was identified as significant and highlighted in this Apache-2.0 file, when a commercial scanner identified it as being derived from file foo.c in package xyz which is licensed under GPL-2.0.",
                ["copyrightText"] = "Copyright 2008-2010 John Smith",
                ["licenseComments"] =
                    "The concluded license was taken from package xyz, from which the snippet was copied into the current file. The concluded license information was found in the COPYING.txt file in package xyz.",
                ["licenseConcluded"] = "GPL-2.0-only",
                ["licenseInfoInSnippets"] = new JsonArray
                {
                    "GPL-2.0-only"
                },
                ["name"] = "from linux kernel",
                ["ranges"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["endPointer"] = new JsonObject
                        {
                            ["offset"] = 420,
                            ["reference"] = "SPDXRef-DoapSource"
                        },
                        ["startPointer"] = new JsonObject
                        {
                            ["offset"] = 310,
                            ["reference"] = "SPDXRef-DoapSource"
                        }
                    },
                    new JsonObject
                    {
                        ["endPointer"] = new JsonObject
                        {
                            ["lineNumber"] = 23,
                            ["reference"] = "SPDXRef-DoapSource"
                        },
                        ["startPointer"] = new JsonObject
                        {
                            ["lineNumber"] = 5,
                            ["reference"] = "SPDXRef-DoapSource"
                        }
                    }
                },
                ["snippetFromFile"] = "SPDXRef-DoapSource"
            }
        };

        // Act: Deserialize the JSON array to an array of SpdxSnippet objects
        var snippets = Spdx2JsonDeserializer.DeserializeSnippets(json);

        // Assert: Verify the deserialized array has the expected properties
        Assert.HasCount(1, snippets);
        Assert.AreEqual("SPDXRef-Snippet", snippets[0].Id);
        Assert.AreEqual(
            "This snippet was identified as significant and highlighted in this Apache-2.0 file, when a commercial scanner identified it as being derived from file foo.c in package xyz which is licensed under GPL-2.0.",
            snippets[0].Comment);
        Assert.AreEqual("Copyright 2008-2010 John Smith", snippets[0].CopyrightText);
        Assert.AreEqual(
            "The concluded license was taken from package xyz, from which the snippet was copied into the current file. The concluded license information was found in the COPYING.txt file in package xyz.",
            snippets[0].LicenseComments);
        Assert.AreEqual("GPL-2.0-only", snippets[0].ConcludedLicense);
        Assert.HasCount(1, snippets[0].LicenseInfoInSnippet);
        Assert.AreEqual("GPL-2.0-only", snippets[0].LicenseInfoInSnippet[0]);
        Assert.AreEqual("from linux kernel", snippets[0].Name);
        Assert.AreEqual(420, snippets[0].SnippetByteEnd);
        Assert.AreEqual(310, snippets[0].SnippetByteStart);
        Assert.AreEqual(23, snippets[0].SnippetLineEnd);
        Assert.AreEqual(5, snippets[0].SnippetLineStart);
        Assert.AreEqual("SPDXRef-DoapSource", snippets[0].SnippetFromFile);
    }
}