using DemaConsulting.SpdxModel.IO;

namespace DemaConsulting.SpdxModel.Tests.IO;

[TestClass]
public class Spdx2JsonSerializeDocument
{
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
                Creators = new[]
                {
                    "Tool: LicenseFind-1.0",
                    "Organization: ExampleCodeInspect ()",
                    "Person: Jane Doe ()"
                },
                LicenseListVersion = "3.17"
            },
            Name = "SPDX-Tools-v2.0",
            DataLicense = "CC0-1.0",
            Comment = "This document was created using SPDX 2.0 using licenses from the web site.",
            ExternalDocumentReferences = Array.Empty<SpdxExternalDocumentReference>(),
            ExtractedLicensingInfo = Array.Empty<SpdxExtractedLicensingInfo>(),
            Describes = new[]
            {
                "SPDXRef-File",
                "SPDXRef-File",
                "SPDXRef-Package"
            },
            DocumentNamespace = "http://spdx.org/spdxdocs/spdx-example-json-2.3-444504E0-4F89-41D3-9A0C-0305E82C3301",
            Packages = Array.Empty<SpdxPackage>(),
            Files = Array.Empty<SpdxFile>(),
            Snippets = Array.Empty<SpdxSnippet>(),
            Relationships = Array.Empty<SpdxRelationship>()
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