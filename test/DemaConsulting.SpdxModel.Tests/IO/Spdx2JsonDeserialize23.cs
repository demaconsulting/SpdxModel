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
///     Tests for deserializing SPDX 2.3 JSON documents to <see cref="SpdxDocument" /> classes.
/// </summary>
/// <remarks>
///     Exercises end-to-end deserialization of a real SPDX 2.3 JSON example document
///     (embedded resource) using xUnit v3 as the test framework.
/// </remarks>
public class Spdx2JsonDeserialize23
{
    /// <summary>
    ///     Test parsing SPDX 2.3 JSON document.
    /// </summary>
    /// <remarks>
    ///     Uses the canonical SPDX 2.3 JSON example bundled as an embedded resource. Verifies
    ///     document-level fields, external document references, extracted licensing info,
    ///     annotations, files, snippets, and relationships are all correctly populated.
    /// </remarks>
    [Fact]
    public void Spdx2JsonDeserializer_Deserialize_ValidSpdx23Json_ReturnsExpectedDocument()
    {
        // Arrange: Get the SPDX 2.3 JSON example from embedded resources
        var json23Example = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");

        // Act: Deserialize the JSON document
        var doc = Spdx2JsonDeserializer.Deserialize(json23Example);

        // Assert: Verify that the document is valid
        Assert.NotNull(doc);
        var issues = new List<string>();
        doc.Validate(issues);
        Assert.Empty(issues);

        // Assert: Verify document
        Assert.Equal("SPDX-Tools-v2.0", doc.Name);
        Assert.Equal("SPDX-2.3", doc.Version);
        Assert.Equal("http://spdx.org/spdxdocs/spdx-example-json-2.3-444504E0-4F89-41D3-9A0C-0305E82C3301",
            doc.DocumentNamespace);
        Assert.Equal("This document was created using SPDX 2.0 using licenses from the web site.", doc.Comment);
        Assert.Equal(3, doc.CreationInformation.Creators.Length);
        Assert.Equal("Tool: LicenseFind-1.0", doc.CreationInformation.Creators[0]);
        Assert.Equal("Organization: ExampleCodeInspect ()", doc.CreationInformation.Creators[1]);
        Assert.Equal("Person: Jane Doe ()", doc.CreationInformation.Creators[2]);
        Assert.Equal("2010-01-29T18:30:22Z", doc.CreationInformation.Created);
        Assert.StartsWith("This package has been shipped in source and", doc.CreationInformation.Comment);
        Assert.Equal("3.17", doc.CreationInformation.LicenseListVersion);

        // Assert: Verify external document references
        Assert.Single(doc.ExternalDocumentReferences);
        Assert.Equal("DocumentRef-spdx-tool-1.2", doc.ExternalDocumentReferences[0].ExternalDocumentId);
        Assert.Equal(SpdxChecksumAlgorithm.Sha1, doc.ExternalDocumentReferences[0].Checksum.Algorithm);
        Assert.Equal("d6a770ba38583ed4bb4525bd96e50461655d2759", doc.ExternalDocumentReferences[0].Checksum.Value);
        Assert.Equal("http://spdx.org/spdxdocs/spdx-tools-v1.2-3F2504E0-4F89-41D3-9A0C-0305E82C3301",
            doc.ExternalDocumentReferences[0].Document);

        // Assert: Verify extracted licensing info
        Assert.Equal(5, doc.ExtractedLicensingInfo.Length);
        Assert.Equal("LicenseRef-1", doc.ExtractedLicensingInfo[0].LicenseId);
        Assert.StartsWith("/*\n * (c) Copyright 2000, 2001, 2002, 2003",
doc.ExtractedLicensingInfo[0].ExtractedText);
        Assert.Equal("LicenseRef-2", doc.ExtractedLicensingInfo[1].LicenseId);
        Assert.StartsWith("This package includes the", doc.ExtractedLicensingInfo[1].ExtractedText);
        Assert.Equal("LicenseRef-4", doc.ExtractedLicensingInfo[2].LicenseId);
        Assert.StartsWith("/*\n * (c) Copyright 2009 University of Bristol",
doc.ExtractedLicensingInfo[2].ExtractedText);
        Assert.Equal("LicenseRef-Beerware-4.2", doc.ExtractedLicensingInfo[3].LicenseId);
        Assert.StartsWith(
"\"THE BEER-WARE LICENSE\" (Revision 42)", doc.ExtractedLicensingInfo[3].ExtractedText);
        Assert.Equal("Beer-Ware License (Version 42)", doc.ExtractedLicensingInfo[3].Name);
        Assert.Single(doc.ExtractedLicensingInfo[3].CrossReferences);
        Assert.Equal("http://people.freebsd.org/~phk/", doc.ExtractedLicensingInfo[3].CrossReferences[0]);
        Assert.StartsWith("The beerware license has", doc.ExtractedLicensingInfo[3].Comment);
        Assert.Equal("LicenseRef-3", doc.ExtractedLicensingInfo[4].LicenseId);
        Assert.StartsWith("The CyberNeko Software License", doc.ExtractedLicensingInfo[4].ExtractedText);
        Assert.Equal("CyberNeko License", doc.ExtractedLicensingInfo[4].Name);
        Assert.Equal(2, doc.ExtractedLicensingInfo[4].CrossReferences.Length);
        Assert.Equal("http://people.apache.org/~andyc/neko/LICENSE",
            doc.ExtractedLicensingInfo[4].CrossReferences[0]);
        Assert.Equal("http://justasample.url.com", doc.ExtractedLicensingInfo[4].CrossReferences[1]);
        Assert.StartsWith("This is the CyperNeko License", doc.ExtractedLicensingInfo[4].Comment);

        // Assert: Verify annotations
        Assert.Equal(3, doc.Annotations.Length);
        Assert.Equal("Person: Jane Doe ()", doc.Annotations[0].Annotator);
        Assert.Equal("2010-01-29T18:30:22Z", doc.Annotations[0].Date);
        Assert.Equal(SpdxAnnotationType.Other, doc.Annotations[0].Type);
        Assert.StartsWith("Document level annotation", doc.Annotations[0].Comment);
        Assert.Equal("Person: Joe Reviewer", doc.Annotations[1].Annotator);
        Assert.Equal("2010-02-10T00:00:00Z", doc.Annotations[1].Date);
        Assert.Equal(SpdxAnnotationType.Review, doc.Annotations[1].Type);
        Assert.StartsWith("This is just an example", doc.Annotations[1].Comment);
        Assert.Equal("Person: Suzanne Reviewer", doc.Annotations[2].Annotator);
        Assert.Equal("2011-03-13T00:00:00Z", doc.Annotations[2].Date);
        Assert.Equal(SpdxAnnotationType.Review, doc.Annotations[2].Type);
        Assert.StartsWith("Another example reviewer.", doc.Annotations[2].Comment);

        // Assert: Verify files
        Assert.Equal(5, doc.Files.Length);
        Assert.Equal("SPDXRef-DoapSource", doc.Files[0].Id);
        Assert.Equal("./src/org/spdx/parser/DOAPProject.java", doc.Files[0].FileName);
        Assert.Single(doc.Files[0].FileTypes);
        Assert.Contains(SpdxFileType.Source, doc.Files[0].FileTypes);
        Assert.Single(doc.Files[0].Checksums);
        Assert.Equal(SpdxChecksumAlgorithm.Sha1, doc.Files[0].Checksums[0].Algorithm);
        Assert.Equal("2fd4e1c67a2d28fced849ee1bb76e7391b93eb12", doc.Files[0].Checksums[0].Value);
        Assert.Equal("Apache-2.0", doc.Files[0].ConcludedLicense);
        Assert.Single(doc.Files[0].LicenseInfoInFiles);
        Assert.Equal("Apache-2.0", doc.Files[0].LicenseInfoInFiles[0]);
        Assert.Equal("Copyright 2010, 2011 Source Auditor Inc.", doc.Files[0].CopyrightText);
        Assert.Equal(5, doc.Files[0].Contributors.Length);
        Assert.Equal("Protecode Inc.", doc.Files[0].Contributors[0]);
        Assert.Equal("SPDX Technical Team Members", doc.Files[0].Contributors[1]);
        Assert.Equal("Open Logic Inc.", doc.Files[0].Contributors[2]);
        Assert.Equal("Source Auditor Inc.", doc.Files[0].Contributors[3]);
        Assert.Equal("Black Duck Software Inc.", doc.Files[0].Contributors[4]);
        Assert.Equal("This file is used by Jena", doc.Files[1].Comment);
        Assert.StartsWith("Apache Commons Lang\nCopyright 2001-2011", doc.Files[1].Notice);
        Assert.Equal("This license is used by Jena", doc.Files[2].LicenseComments);
        Assert.Equal(2, doc.Files[4].Checksums.Length);
        Assert.Equal(SpdxChecksumAlgorithm.Sha1, doc.Files[4].Checksums[0].Algorithm);
        Assert.Equal("d6a770ba38583ed4bb4525bd96e50461655d2758", doc.Files[4].Checksums[0].Value);
        Assert.Equal(SpdxChecksumAlgorithm.Md5, doc.Files[4].Checksums[1].Algorithm);
        Assert.Equal("624c1abb3664f4b35547e7c73864ad24", doc.Files[4].Checksums[1].Value);

        // Assert: Verify snippets
        Assert.Single(doc.Snippets);
        Assert.Equal("SPDXRef-Snippet", doc.Snippets[0].Id);
        Assert.Equal("SPDXRef-DoapSource", doc.Snippets[0].SnippetFromFile);
        Assert.Equal(310, doc.Snippets[0].SnippetByteStart);
        Assert.Equal(420, doc.Snippets[0].SnippetByteEnd);
        Assert.Equal(5, doc.Snippets[0].SnippetLineStart);
        Assert.Equal(23, doc.Snippets[0].SnippetLineEnd);
        Assert.Equal("GPL-2.0-only", doc.Snippets[0].ConcludedLicense);
        Assert.Single(doc.Snippets[0].LicenseInfoInSnippet);
        Assert.Equal("GPL-2.0-only", doc.Snippets[0].LicenseInfoInSnippet[0]);
        Assert.StartsWith("The concluded license was taken", doc.Snippets[0].LicenseComments);
        Assert.Equal("Copyright 2008-2010 John Smith", doc.Snippets[0].CopyrightText);
        Assert.StartsWith("This snippet was identified as significant", doc.Snippets[0].Comment);
        Assert.Equal("from linux kernel", doc.Snippets[0].Name);

        // Assert: Verify relationships
        Assert.Equal(7, doc.Relationships.Length);
        Assert.Equal("SPDXRef-DOCUMENT", doc.Relationships[0].Id);
        Assert.Equal("SPDXRef-Package", doc.Relationships[0].RelatedSpdxElement);
        Assert.Equal(SpdxRelationshipType.Contains, doc.Relationships[0].RelationshipType);
        Assert.Equal("SPDXRef-DOCUMENT", doc.Relationships[1].Id);
        Assert.Equal("DocumentRef-spdx-tool-1.2:SPDXRef-ToolsElement", doc.Relationships[1].RelatedSpdxElement);
        Assert.Equal(SpdxRelationshipType.CopyOf, doc.Relationships[1].RelationshipType);
        Assert.Equal("SPDXRef-Package", doc.Relationships[2].Id);
        Assert.Equal("SPDXRef-Saxon", doc.Relationships[2].RelatedSpdxElement);
        Assert.Equal(SpdxRelationshipType.DynamicLink, doc.Relationships[2].RelationshipType);
        Assert.Equal("SPDXRef-CommonsLangSrc", doc.Relationships[3].Id);
        Assert.Equal("NOASSERTION", doc.Relationships[3].RelatedSpdxElement);
        Assert.Equal(SpdxRelationshipType.GeneratedFrom, doc.Relationships[3].RelationshipType);
        Assert.Equal("SPDXRef-JenaLib", doc.Relationships[4].Id);
        Assert.Equal("SPDXRef-Package", doc.Relationships[4].RelatedSpdxElement);
        Assert.Equal(SpdxRelationshipType.Contains, doc.Relationships[4].RelationshipType);
        Assert.Equal("SPDXRef-Specification", doc.Relationships[5].Id);
        Assert.Equal("SPDXRef-fromDoap-0", doc.Relationships[5].RelatedSpdxElement);
        Assert.Equal(SpdxRelationshipType.SpecificationFor, doc.Relationships[5].RelationshipType);
        Assert.Equal("SPDXRef-File", doc.Relationships[6].Id);
        Assert.Equal("SPDXRef-fromDoap-0", doc.Relationships[6].RelatedSpdxElement);
        Assert.Equal(SpdxRelationshipType.GeneratedFrom, doc.Relationships[6].RelationshipType);
    }
}
