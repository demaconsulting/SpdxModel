using System.Text.Json.Nodes;
using DemaConsulting.SpdxModel.IO;

namespace DemaConsulting.SpdxModel.Tests.IO;

[TestClass]
public class SpdxJsonDeserializeSnippet
{
    [TestMethod]
    public void DeserializeSnippet()
    {
        // Arrange
        var json = new JsonObject()
        {
            ["SPDXID"] = "SPDXRef-Snippet",
            ["comment"] =
                "This snippet was identified as significant and highlighted in this Apache-2.0 file, when a commercial scanner identified it as being derived from file foo.c in package xyz which is licensed under GPL-2.0.",
            ["copyrightText"] = "Copyright 2008-2010 John Smith",
            ["licenseComments"] =
                "The concluded license was taken from package xyz, from which the snippet was copied into the current file. The concluded license information was found in the COPYING.txt file in package xyz.",
            ["licenseConcluded"] = "GPL-2.0-only",
            ["licenseInfoInSnippets"] = new JsonArray()
            {
                "GPL-2.0-only"
            },
            ["name"] = "from linux kernel",
            ["ranges"] = new JsonArray()
            {
                new JsonObject()
                {
                    ["endPointer"] = new JsonObject()
                    {
                        ["offset"] = 420,
                        ["reference"] = "SPDXRef-DoapSource"
                    },
                    ["startPointer"] = new JsonObject()
                    {
                        ["offset"] = 310,
                        ["reference"] = "SPDXRef-DoapSource"
                    }
                },
                new JsonObject()
                {
                    ["endPointer"] = new JsonObject()
                    {
                        ["lineNumber"] = 23,
                        ["reference"] = "SPDXRef-DoapSource"
                    },
                    ["startPointer"] = new JsonObject()
                    {
                        ["lineNumber"] = 5,
                        ["reference"] = "SPDXRef-DoapSource"
                    }
                }
            },
            ["snippetFromFile"] = "SPDXRef-DoapSource"
        };

        // Act
        var snippet = SpdxJsonDeserializer.DeserializeSnippet(json);

        // Assert
        Assert.AreEqual("SPDXRef-Snippet", snippet.SpdxId);
        Assert.AreEqual(
            "This snippet was identified as significant and highlighted in this Apache-2.0 file, when a commercial scanner identified it as being derived from file foo.c in package xyz which is licensed under GPL-2.0.",
            snippet.Comment);
        Assert.AreEqual("Copyright 2008-2010 John Smith", snippet.Copyright);
        Assert.AreEqual(
            "The concluded license was taken from package xyz, from which the snippet was copied into the current file. The concluded license information was found in the COPYING.txt file in package xyz.",
            snippet.LicenseComments);
        Assert.AreEqual("GPL-2.0-only", snippet.ConcludedLicense);
        Assert.AreEqual(1, snippet.LicenseInfoInSnippet.Length);
        Assert.AreEqual("GPL-2.0-only", snippet.LicenseInfoInSnippet[0]);
        Assert.AreEqual("from linux kernel", snippet.Name);
        Assert.AreEqual(420, snippet.SnippetByteEnd);
        Assert.AreEqual(310, snippet.SnippetByteStart);
        Assert.AreEqual(23, snippet.SnippetLineEnd);
        Assert.AreEqual(5, snippet.SnippetLineStart);
        Assert.AreEqual("SPDXRef-DoapSource", snippet.SnippetFromFile);
    }

    [TestMethod]
    public void DeserializeSnippets()
    {
        // Arrange
        var json = new JsonArray
        {
            new JsonObject()
            {
                ["SPDXID"] = "SPDXRef-Snippet",
                ["comment"] =
                    "This snippet was identified as significant and highlighted in this Apache-2.0 file, when a commercial scanner identified it as being derived from file foo.c in package xyz which is licensed under GPL-2.0.",
                ["copyrightText"] = "Copyright 2008-2010 John Smith",
                ["licenseComments"] =
                    "The concluded license was taken from package xyz, from which the snippet was copied into the current file. The concluded license information was found in the COPYING.txt file in package xyz.",
                ["licenseConcluded"] = "GPL-2.0-only",
                ["licenseInfoInSnippets"] = new JsonArray()
                {
                    "GPL-2.0-only"
                },
                ["name"] = "from linux kernel",
                ["ranges"] = new JsonArray()
                {
                    new JsonObject()
                    {
                        ["endPointer"] = new JsonObject()
                        {
                            ["offset"] = 420,
                            ["reference"] = "SPDXRef-DoapSource"
                        },
                        ["startPointer"] = new JsonObject()
                        {
                            ["offset"] = 310,
                            ["reference"] = "SPDXRef-DoapSource"
                        }
                    },
                    new JsonObject()
                    {
                        ["endPointer"] = new JsonObject()
                        {
                            ["lineNumber"] = 23,
                            ["reference"] = "SPDXRef-DoapSource"
                        },
                        ["startPointer"] = new JsonObject()
                        {
                            ["lineNumber"] = 5,
                            ["reference"] = "SPDXRef-DoapSource"
                        }
                    }
                },
                ["snippetFromFile"] = "SPDXRef-DoapSource"
            }
        };

        // Act
        var snippets = SpdxJsonDeserializer.DeserializeSnippets(json);

        // Assert
        Assert.AreEqual(1, snippets.Length);
        Assert.AreEqual("SPDXRef-Snippet", snippets[0].SpdxId);
        Assert.AreEqual(
            "This snippet was identified as significant and highlighted in this Apache-2.0 file, when a commercial scanner identified it as being derived from file foo.c in package xyz which is licensed under GPL-2.0.",
            snippets[0].Comment);
        Assert.AreEqual("Copyright 2008-2010 John Smith", snippets[0].Copyright);
        Assert.AreEqual(
            "The concluded license was taken from package xyz, from which the snippet was copied into the current file. The concluded license information was found in the COPYING.txt file in package xyz.",
            snippets[0].LicenseComments);
        Assert.AreEqual("GPL-2.0-only", snippets[0].ConcludedLicense);
        Assert.AreEqual(1, snippets[0].LicenseInfoInSnippet.Length);
        Assert.AreEqual("GPL-2.0-only", snippets[0].LicenseInfoInSnippet[0]);
        Assert.AreEqual("from linux kernel", snippets[0].Name);
        Assert.AreEqual(420, snippets[0].SnippetByteEnd);
        Assert.AreEqual(310, snippets[0].SnippetByteStart);
        Assert.AreEqual(23, snippets[0].SnippetLineEnd);
        Assert.AreEqual(5, snippets[0].SnippetLineStart);
        Assert.AreEqual("SPDXRef-DoapSource", snippets[0].SnippetFromFile);
    }
}