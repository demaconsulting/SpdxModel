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

[TestClass]
public class Spdx2JsonDeserializeDocument
{
    [TestMethod]
    public void DeserializeDocument()
    {
        // Arrange
        var json = new JsonObject
        {
            ["SPDXID"] = "SPDXRef-DOCUMENT",
            ["spdxVersion"] = "SPDX-2.3",
            ["creationInfo"] = new JsonObject
            {
                ["comment"] =
                    "This package has been shipped in source and binary form.\nThe binaries were created with gcc 4.5.1 and expect to link to\ncompatible system run time libraries.",
                ["created"] = "2010-01-29T18:30:22Z",
                ["creators"] = new JsonArray
                {
                    "Tool: LicenseFind-1.0",
                    "Organization: ExampleCodeInspect ()",
                    "Person: Jane Doe ()"
                },
                ["licenseListVersion"] = "3.17"
            },
            ["name"] = "SPDX-Tools-v2.0",
            ["dataLicense"] = "CC0-1.0",
            ["comment"] = "This document was created using SPDX 2.0 using licenses from the web site.",
            ["externalDocumentRefs"] = new JsonArray(),
            ["hasExtractedLicensingInfos"] = new JsonArray(),
            ["documentDescribes"] = new JsonArray
            {
                "SPDXRef-File",
                "SPDXRef-File",
                "SPDXRef-Package"
            },
            ["documentNamespace"] =
                "http://spdx.org/spdxdocs/spdx-example-json-2.3-444504E0-4F89-41D3-9A0C-0305E82C3301",
            ["packages"] = new JsonArray(),
            ["files"] = new JsonArray(),
            ["snippets"] = new JsonArray(),
            ["relationships"] = new JsonArray()
        };

        // Act
        var document = Spdx2JsonDeserializer.DeserializeDocument(json);

        // Assert
        Assert.AreEqual("SPDXRef-DOCUMENT", document.Id);
        Assert.AreEqual("SPDX-2.3", document.Version);
        Assert.AreEqual("SPDX-Tools-v2.0", document.Name);
        Assert.AreEqual("CC0-1.0", document.DataLicense);
        Assert.AreEqual("This document was created using SPDX 2.0 using licenses from the web site.", document.Comment);
        Assert.AreEqual("http://spdx.org/spdxdocs/spdx-example-json-2.3-444504E0-4F89-41D3-9A0C-0305E82C3301",
            document.DocumentNamespace);
        Assert.AreEqual(3, document.Describes.Length);
        Assert.AreEqual("SPDXRef-File", document.Describes[0]);
        Assert.AreEqual("SPDXRef-File", document.Describes[1]);
        Assert.AreEqual("SPDXRef-Package", document.Describes[2]);
        Assert.AreEqual(0, document.ExternalDocumentReferences.Length);
        Assert.AreEqual(0, document.ExtractedLicensingInfo.Length);
        Assert.AreEqual(0, document.Packages.Length);
        Assert.AreEqual(0, document.Files.Length);
        Assert.AreEqual(0, document.Snippets.Length);
        Assert.AreEqual(0, document.Relationships.Length);
    }
}