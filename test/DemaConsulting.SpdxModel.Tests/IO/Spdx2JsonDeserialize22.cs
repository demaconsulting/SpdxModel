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
///     Tests for deserializing SPDX 2.2 JSON documents to <see cref="SpdxDocument" /> classes.
/// </summary>
[TestClass]
public class Spdx2JsonDeserialize22
{
    /// <summary>
    ///     Test parsing SPDX 2.2 JSON document.
    /// </summary>
    [TestMethod]
    public void Spdx2JsonDeserializer_Deserialize_ValidSpdx22JsonReturnsExpectedDocument()
    {
        // Arrange: Load the SPDX 2.2 JSON example from embedded resources
        var json22Example = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.2.spdx.json");
        var doc = Spdx2JsonDeserializer.Deserialize(json22Example);
        Assert.IsNotNull(doc);

        // Act: Validate the document
        var issues = new List<string>();
        doc.Validate(issues);

        // Assert: Verify that there are no validation issues
        Assert.IsEmpty(issues);

        // Assert: Verify the document properties
        Assert.AreEqual("SPDX-Tools-v2.0", doc.Name);
        Assert.AreEqual("SPDX-2.2", doc.Version);
        Assert.AreEqual("http://spdx.org/spdxdocs/spdx-example-json-2.2-444504E0-4F89-41D3-9A0C-0305E82C3301",
            doc.DocumentNamespace);
        Assert.AreEqual("This document was created using SPDX 2.0 using licenses from the web site.", doc.Comment);
        Assert.HasCount(3, doc.CreationInformation.Creators);
        Assert.AreEqual("Tool: LicenseFind-1.0", doc.CreationInformation.Creators[0]);
        Assert.AreEqual("Organization: ExampleCodeInspect ()", doc.CreationInformation.Creators[1]);
        Assert.AreEqual("Person: Jane Doe ()", doc.CreationInformation.Creators[2]);
        Assert.AreEqual("2010-01-29T18:30:22Z", doc.CreationInformation.Created);
        StringAssert.StartsWith(doc.CreationInformation.Comment, "This package has been shipped in source and");
        Assert.AreEqual("3.9", doc.CreationInformation.LicenseListVersion);

        // Assert: Verify external document references
        Assert.HasCount(1, doc.ExternalDocumentReferences);
        Assert.AreEqual("DocumentRef-spdx-tool-1.2", doc.ExternalDocumentReferences[0].ExternalDocumentId);
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, doc.ExternalDocumentReferences[0].Checksum.Algorithm);
        Assert.AreEqual("d6a770ba38583ed4bb4525bd96e50461655d2759", doc.ExternalDocumentReferences[0].Checksum.Value);
        Assert.AreEqual("http://spdx.org/spdxdocs/spdx-tools-v1.2-3F2504E0-4F89-41D3-9A0C-0305E82C3301",
            doc.ExternalDocumentReferences[0].Document);

        // Assert: Verify extracted licensing info
        Assert.HasCount(5, doc.ExtractedLicensingInfo);
        Assert.AreEqual("LicenseRef-Beerware-4.2", doc.ExtractedLicensingInfo[0].LicenseId);
        StringAssert.StartsWith(doc.ExtractedLicensingInfo[0].ExtractedText, "\"THE BEER-WARE LICENSE\"");
        Assert.AreEqual("LicenseRef-4", doc.ExtractedLicensingInfo[1].LicenseId);
        StringAssert.StartsWith(doc.ExtractedLicensingInfo[1].ExtractedText,
            "/*\n * (c) Copyright 2009 University of Bristol");
        Assert.AreEqual("LicenseRef-3", doc.ExtractedLicensingInfo[2].LicenseId);
        StringAssert.StartsWith(doc.ExtractedLicensingInfo[2].ExtractedText, "The CyberNeko Software License");
        Assert.AreEqual("CyberNeko License", doc.ExtractedLicensingInfo[2].Name);
        Assert.HasCount(2, doc.ExtractedLicensingInfo[2].CrossReferences);
        Assert.AreEqual("http://people.apache.org/~andyc/neko/LICENSE",
            doc.ExtractedLicensingInfo[2].CrossReferences[0]);
        Assert.AreEqual("http://justasample.url.com", doc.ExtractedLicensingInfo[2].CrossReferences[1]);
        Assert.AreEqual("This is the CyperNeko License", doc.ExtractedLicensingInfo[2].Comment);
        Assert.AreEqual("LicenseRef-2", doc.ExtractedLicensingInfo[3].LicenseId);
        StringAssert.StartsWith(doc.ExtractedLicensingInfo[3].ExtractedText, "This package includes the");
        Assert.AreEqual("LicenseRef-1", doc.ExtractedLicensingInfo[4].LicenseId);
        StringAssert.StartsWith(doc.ExtractedLicensingInfo[4].ExtractedText, "/*\n * (c) Copyright 2000, 2001, 2002");

        // Assert: Verify annotations
        Assert.HasCount(3, doc.Annotations);
        Assert.AreEqual("Person: Jane Doe ()", doc.Annotations[0].Annotator);
        Assert.AreEqual("2010-01-29T18:30:22Z", doc.Annotations[0].Date);
        Assert.AreEqual(SpdxAnnotationType.Other, doc.Annotations[0].Type);
        Assert.AreEqual("Document level annotation", doc.Annotations[0].Comment);
        Assert.AreEqual("Person: Suzanne Reviewer", doc.Annotations[1].Annotator);
        Assert.AreEqual("2011-03-13T00:00:00Z", doc.Annotations[1].Date);
        Assert.AreEqual(SpdxAnnotationType.Review, doc.Annotations[1].Type);
        Assert.AreEqual("Another example reviewer.", doc.Annotations[1].Comment);
        Assert.AreEqual("Person: Joe Reviewer", doc.Annotations[2].Annotator);
        Assert.AreEqual("2010-02-10T00:00:00Z", doc.Annotations[2].Date);
        Assert.AreEqual(SpdxAnnotationType.Review, doc.Annotations[2].Type);
        StringAssert.StartsWith(doc.Annotations[2].Comment, "This is just an example");

        // Assert: Verify files
        Assert.HasCount(4, doc.Files);
        Assert.AreEqual("SPDXRef-DoapSource", doc.Files[0].Id);
        Assert.AreEqual("./src/org/spdx/parser/DOAPProject.java", doc.Files[0].FileName);
        Assert.HasCount(1, doc.Files[0].FileTypes);
        Assert.IsTrue(doc.Files[0].FileTypes.Contains(SpdxFileType.Source));
        Assert.HasCount(1, doc.Files[0].Checksums);
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, doc.Files[0].Checksums[0].Algorithm);
        Assert.AreEqual("2fd4e1c67a2d28fced849ee1bb76e7391b93eb12", doc.Files[0].Checksums[0].Value);
        Assert.AreEqual("Apache-2.0", doc.Files[0].ConcludedLicense);
        Assert.HasCount(1, doc.Files[0].LicenseInfoInFiles);
        Assert.AreEqual("Apache-2.0", doc.Files[0].LicenseInfoInFiles[0]);
        Assert.AreEqual("Copyright 2010, 2011 Source Auditor Inc.", doc.Files[0].CopyrightText);
        Assert.HasCount(5, doc.Files[0].Contributors);
        Assert.AreEqual("Protecode Inc.", doc.Files[0].Contributors[0]);
        Assert.AreEqual("SPDX Technical Team Members", doc.Files[0].Contributors[1]);
        Assert.AreEqual("Open Logic Inc.", doc.Files[0].Contributors[2]);
        Assert.AreEqual("Source Auditor Inc.", doc.Files[0].Contributors[3]);
        Assert.AreEqual("Black Duck Software Inc.", doc.Files[0].Contributors[4]);
        Assert.AreEqual("This file is used by Jena", doc.Files[1].Comment);
        StringAssert.StartsWith(doc.Files[1].Notice, "Apache Commons Lang\nCopyright 2001-2011");
        Assert.AreEqual("This license is used by Jena", doc.Files[2].LicenseComments);
        Assert.HasCount(1, doc.Files[3].Annotations);
        Assert.AreEqual("Person: File Commenter", doc.Files[3].Annotations[0].Annotator);
        Assert.AreEqual("2011-01-29T18:30:22Z", doc.Files[3].Annotations[0].Date);
        Assert.AreEqual(SpdxAnnotationType.Other, doc.Files[3].Annotations[0].Type);
        Assert.AreEqual("File level annotation", doc.Files[3].Annotations[0].Comment);
        Assert.HasCount(2, doc.Files[3].Checksums);
        Assert.AreEqual(SpdxChecksumAlgorithm.Md5, doc.Files[3].Checksums[0].Algorithm);
        Assert.AreEqual("624c1abb3664f4b35547e7c73864ad24", doc.Files[3].Checksums[0].Value);
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, doc.Files[3].Checksums[1].Algorithm);
        Assert.AreEqual("d6a770ba38583ed4bb4525bd96e50461655d2758", doc.Files[3].Checksums[1].Value);

        // Assert: Verify snippets
        Assert.HasCount(1, doc.Snippets);
        Assert.AreEqual("SPDXRef-Snippet", doc.Snippets[0].Id);
        Assert.AreEqual("SPDXRef-DoapSource", doc.Snippets[0].SnippetFromFile);
        Assert.AreEqual(310, doc.Snippets[0].SnippetByteStart);
        Assert.AreEqual(420, doc.Snippets[0].SnippetByteEnd);
        Assert.AreEqual(5, doc.Snippets[0].SnippetLineStart);
        Assert.AreEqual(23, doc.Snippets[0].SnippetLineEnd);
        Assert.AreEqual("GPL-2.0-only", doc.Snippets[0].ConcludedLicense);
        Assert.HasCount(1, doc.Snippets[0].LicenseInfoInSnippet);
        Assert.AreEqual("GPL-2.0-only", doc.Snippets[0].LicenseInfoInSnippet[0]);
        StringAssert.StartsWith(doc.Snippets[0].LicenseComments, "The concluded license was taken");
        Assert.AreEqual("Copyright 2008-2010 John Smith", doc.Snippets[0].CopyrightText);
        StringAssert.StartsWith(doc.Snippets[0].Comment, "This snippet was identified as significant");
        Assert.AreEqual("from linux kernel", doc.Snippets[0].Name);

        // Assert: Verify relationships
        Assert.HasCount(9, doc.Relationships);
        Assert.AreEqual("SPDXRef-DOCUMENT", doc.Relationships[0].Id);
        Assert.AreEqual("SPDXRef-Package", doc.Relationships[0].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.Contains, doc.Relationships[0].RelationshipType);
        Assert.AreEqual("SPDXRef-DOCUMENT", doc.Relationships[1].Id);
        Assert.AreEqual("SPDXRef-File", doc.Relationships[1].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.Describes, doc.Relationships[1].RelationshipType);
        Assert.AreEqual("SPDXRef-DOCUMENT", doc.Relationships[2].Id);
        Assert.AreEqual("DocumentRef-spdx-tool-1.2:SPDXRef-ToolsElement", doc.Relationships[2].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.CopyOf, doc.Relationships[2].RelationshipType);
        Assert.AreEqual("SPDXRef-DOCUMENT", doc.Relationships[3].Id);
        Assert.AreEqual("SPDXRef-Package", doc.Relationships[3].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.Describes, doc.Relationships[3].RelationshipType);
        Assert.AreEqual("SPDXRef-Package", doc.Relationships[4].Id);
        Assert.AreEqual("SPDXRef-Saxon", doc.Relationships[4].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.DynamicLink, doc.Relationships[4].RelationshipType);
        Assert.AreEqual("SPDXRef-Package", doc.Relationships[5].Id);
        Assert.AreEqual("SPDXRef-JenaLib", doc.Relationships[5].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.Contains, doc.Relationships[5].RelationshipType);
        Assert.AreEqual("SPDXRef-CommonsLangSrc", doc.Relationships[6].Id);
        Assert.AreEqual("NOASSERTION", doc.Relationships[6].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.GeneratedFrom, doc.Relationships[6].RelationshipType);
        Assert.AreEqual("SPDXRef-JenaLib", doc.Relationships[7].Id);
        Assert.AreEqual("SPDXRef-Package", doc.Relationships[7].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.Contains, doc.Relationships[7].RelationshipType);
        Assert.AreEqual("SPDXRef-File", doc.Relationships[8].Id);
        Assert.AreEqual("SPDXRef-fromDoap-0", doc.Relationships[8].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.GeneratedFrom, doc.Relationships[8].RelationshipType);
    }
}