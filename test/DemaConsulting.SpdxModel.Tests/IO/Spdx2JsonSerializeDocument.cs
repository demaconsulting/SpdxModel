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
/// Tests for serializing <see cref="SpdxDocument"/> to JSON.
/// </summary>
[TestClass]
public class Spdx2JsonSerializeDocument
{
    /// <summary>
    /// Tests serializing a document.
    /// </summary>
    [TestMethod]
    public void SerializeDocument()
    {
        // Arrange
        var document = new SpdxDocument
        {
            Id = "SPDXRef-DOCUMENT",
            Version = "SPDX-2.3",
            CreationInformation = new SpdxCreationInformation
            {
                Comment =
                    "This package has been shipped in source and binary form.\nThe binaries were created with gcc 4.5.1 and expect to link to\ncompatible system run time libraries.",
                Created = "2010-01-29T18:30:22Z",
                Creators =
                [
                    "Tool: LicenseFind-1.0",
                    "Organization: ExampleCodeInspect ()",
                    "Person: Jane Doe ()"
                ],
                LicenseListVersion = "3.17"
            },
            Name = "SPDX-Tools-v2.0",
            DataLicense = "CC0-1.0",
            Comment = "This document was created using SPDX 2.0 using licenses from the web site.",
            ExternalDocumentReferences = [],
            ExtractedLicensingInfo = [],
            Describes =
            [
                "SPDXRef-File",
                "SPDXRef-File",
                "SPDXRef-Package"
            ],
            DocumentNamespace = "http://spdx.org/spdxdocs/spdx-example-json-2.3-444504E0-4F89-41D3-9A0C-0305E82C3301",
            Packages = [],
            Files = [],
            Snippets = [],
            Relationships = []
        };

        // Act
        var json = Spdx2JsonSerializer.SerializeDocument(document);

        // Assert
        Assert.AreEqual("SPDXRef-DOCUMENT", json["SPDXID"]?.ToString());
        Assert.AreEqual("SPDX-2.3", json["spdxVersion"]?.ToString());
        Assert.AreEqual("SPDX-Tools-v2.0", json["name"]?.ToString());
        Assert.AreEqual("CC0-1.0", json["dataLicense"]?.ToString());
        Assert.AreEqual("This document was created using SPDX 2.0 using licenses from the web site.",
            json["comment"]?.ToString());
        Assert.AreEqual("SPDXRef-File", json["documentDescribes"]?[0]?.ToString());
        Assert.AreEqual("SPDXRef-File", json["documentDescribes"]?[1]?.ToString());
        Assert.AreEqual("SPDXRef-Package", json["documentDescribes"]?[2]?.ToString());
        Assert.AreEqual("http://spdx.org/spdxdocs/spdx-example-json-2.3-444504E0-4F89-41D3-9A0C-0305E82C3301",
            json["documentNamespace"]?.ToString());
    }
}