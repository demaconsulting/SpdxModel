namespace DemaConsulting.SpdxModel.Tests.IO;

[TestClass]
public class SpdxJsonDeserialize23
{
    [TestMethod]
    public void TestParse23()
    {
        var json22Example = TestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");

        // Deserialize the document
        var doc = SpdxModel.IO.SpdxJsonDeserializer.Deserialize(json22Example);
        Assert.IsNotNull(doc);

        // Validate the document
        var issues = new List<string>();
        doc.Validate(issues);
        Assert.AreEqual(0, issues.Count);

        // Verify document
        Assert.AreEqual("SPDX-Tools-v2.0", doc.Name);
        Assert.AreEqual("SPDX-2.3", doc.SpdxVersion);
        Assert.AreEqual("http://spdx.org/spdxdocs/spdx-example-json-2.3-444504E0-4F89-41D3-9A0C-0305E82C3301",
            doc.DocumentNamespace);
        Assert.AreEqual("This document was created using SPDX 2.0 using licenses from the web site.", doc.Comment);
        Assert.AreEqual(3, doc.CreationInformation.Creators.Length);
        Assert.AreEqual("Tool: LicenseFind-1.0", doc.CreationInformation.Creators[0]);
        Assert.AreEqual("Organization: ExampleCodeInspect ()", doc.CreationInformation.Creators[1]);
        Assert.AreEqual("Person: Jane Doe ()", doc.CreationInformation.Creators[2]);
        Assert.AreEqual("2010-01-29T18:30:22Z", doc.CreationInformation.Created);
        Assert.IsTrue(doc.CreationInformation.Comment?.StartsWith("This package has been shipped in source and"));
        Assert.AreEqual("3.17", doc.CreationInformation.LicenseListVersion);

        // Verify external document references
        Assert.AreEqual(1, doc.ExternalDocumentReferences.Length);
        Assert.AreEqual("DocumentRef-spdx-tool-1.2", doc.ExternalDocumentReferences[0].ExternalDocumentId);
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, doc.ExternalDocumentReferences[0].Checksum.Algorithm);
        Assert.AreEqual("d6a770ba38583ed4bb4525bd96e50461655d2759", doc.ExternalDocumentReferences[0].Checksum.Value);
        Assert.AreEqual("http://spdx.org/spdxdocs/spdx-tools-v1.2-3F2504E0-4F89-41D3-9A0C-0305E82C3301",
            doc.ExternalDocumentReferences[0].SpdxDocument);

        // Verify extracted licensing info
        Assert.AreEqual(5, doc.ExtractedLicensingInfo.Length);
        Assert.AreEqual("LicenseRef-1", doc.ExtractedLicensingInfo[0].LicenseId);
        Assert.IsTrue(doc.ExtractedLicensingInfo[0].ExtractedText
            ?.StartsWith("/*\n * (c) Copyright 2000, 2001, 2002, 2003"));
        Assert.AreEqual("LicenseRef-2", doc.ExtractedLicensingInfo[1].LicenseId);
        Assert.IsTrue(doc.ExtractedLicensingInfo[1].ExtractedText?.StartsWith("This package includes the"));
        Assert.AreEqual("LicenseRef-4", doc.ExtractedLicensingInfo[2].LicenseId);
        Assert.IsTrue(doc.ExtractedLicensingInfo[2].ExtractedText
            ?.StartsWith("/*\n * (c) Copyright 2009 University of Bristol"));
        Assert.AreEqual("LicenseRef-Beerware-4.2", doc.ExtractedLicensingInfo[3].LicenseId);
        Assert.IsTrue(
            doc.ExtractedLicensingInfo[3].ExtractedText?.StartsWith("\"THE BEER-WARE LICENSE\" (Revision 42)"));
        Assert.AreEqual("Beer-Ware License (Version 42)", doc.ExtractedLicensingInfo[3].Name);
        Assert.AreEqual(1, doc.ExtractedLicensingInfo[3].CrossReferences.Length);
        Assert.AreEqual("http://people.freebsd.org/~phk/", doc.ExtractedLicensingInfo[3].CrossReferences[0]);
        Assert.IsTrue(doc.ExtractedLicensingInfo[3].Comment?.StartsWith("The beerware license has"));
        Assert.AreEqual("LicenseRef-3", doc.ExtractedLicensingInfo[4].LicenseId);
        Assert.IsTrue(doc.ExtractedLicensingInfo[4].ExtractedText?.StartsWith("The CyberNeko Software License"));
        Assert.AreEqual("CyberNeko License", doc.ExtractedLicensingInfo[4].Name);
        Assert.AreEqual(2, doc.ExtractedLicensingInfo[4].CrossReferences.Length);
        Assert.AreEqual("http://people.apache.org/~andyc/neko/LICENSE",
            doc.ExtractedLicensingInfo[4].CrossReferences[0]);
        Assert.AreEqual("http://justasample.url.com", doc.ExtractedLicensingInfo[4].CrossReferences[1]);
        Assert.IsTrue(doc.ExtractedLicensingInfo[4].Comment?.StartsWith("This is tye CyperNeko License"));

        // Verify annotations
        Assert.AreEqual(3, doc.Annotations.Length);
        Assert.AreEqual("Person: Jane Doe ()", doc.Annotations[0].Annotator);
        Assert.AreEqual("2010-01-29T18:30:22Z", doc.Annotations[0].Date);
        Assert.AreEqual(SpdxAnnotationType.Other, doc.Annotations[0].Type);
        Assert.IsTrue(doc.Annotations[0].Comment?.StartsWith("Document level annotation"));
        Assert.AreEqual("Person: Joe Reviewer", doc.Annotations[1].Annotator);
        Assert.AreEqual("2010-02-10T00:00:00Z", doc.Annotations[1].Date);
        Assert.AreEqual(SpdxAnnotationType.Review, doc.Annotations[1].Type);
        Assert.IsTrue(doc.Annotations[1].Comment?.StartsWith("This is just an example"));
        Assert.AreEqual("Person: Suzanne Reviewer", doc.Annotations[2].Annotator);
        Assert.AreEqual("2011-03-13T00:00:00Z", doc.Annotations[2].Date);
        Assert.AreEqual(SpdxAnnotationType.Review, doc.Annotations[2].Type);
        Assert.IsTrue(doc.Annotations[2].Comment?.StartsWith("Another example reviewer."));

        // Verify files
        Assert.AreEqual(5, doc.Files.Length);
        Assert.AreEqual("SPDXRef-DoapSource", doc.Files[0].SpdxId);
        Assert.AreEqual("./src/org/spdx/parser/DOAPProject.java", doc.Files[0].FileName);
        Assert.AreEqual(1, doc.Files[0].FileTypes.Length);
        Assert.IsTrue(doc.Files[0].FileTypes.Contains(SpdxFileType.Source));
        Assert.AreEqual(1, doc.Files[0].Checksums.Length);
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, doc.Files[0].Checksums[0].Algorithm);
        Assert.AreEqual("2fd4e1c67a2d28fced849ee1bb76e7391b93eb12", doc.Files[0].Checksums[0].Value);
        Assert.AreEqual("Apache-2.0", doc.Files[0].LicenseConcluded);
        Assert.AreEqual(1, doc.Files[0].LicenseInfoInFiles.Length);
        Assert.AreEqual("Apache-2.0", doc.Files[0].LicenseInfoInFiles[0]);
        Assert.AreEqual("Copyright 2010, 2011 Source Auditor Inc.", doc.Files[0].Copyright);
        Assert.AreEqual(5, doc.Files[0].Contributors.Length);
        Assert.AreEqual("Protecode Inc.", doc.Files[0].Contributors[0]);
        Assert.AreEqual("SPDX Technical Team Members", doc.Files[0].Contributors[1]);
        Assert.AreEqual("Open Logic Inc.", doc.Files[0].Contributors[2]);
        Assert.AreEqual("Source Auditor Inc.", doc.Files[0].Contributors[3]);
        Assert.AreEqual("Black Duck Software In.c", doc.Files[0].Contributors[4]);
        Assert.AreEqual("This file is used by Jena", doc.Files[1].Comment);
        Assert.IsTrue(doc.Files[1].Notice?.StartsWith("Apache Commons Lang\nCopyright 2001-2011"));
        Assert.AreEqual("This license is used by Jena", doc.Files[2].LicenseComments);
        Assert.AreEqual(2, doc.Files[4].Checksums.Length);
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, doc.Files[4].Checksums[0].Algorithm);
        Assert.AreEqual("d6a770ba38583ed4bb4525bd96e50461655d2758", doc.Files[4].Checksums[0].Value);
        Assert.AreEqual(SpdxChecksumAlgorithm.Md5, doc.Files[4].Checksums[1].Algorithm);
        Assert.AreEqual("624c1abb3664f4b35547e7c73864ad24", doc.Files[4].Checksums[1].Value);

        // Verify snippets
        Assert.AreEqual(1, doc.Snippets.Length);
        Assert.AreEqual("SPDXRef-Snippet", doc.Snippets[0].SpdxId);
        Assert.AreEqual("SPDXRef-DoapSource", doc.Snippets[0].SnippetFromFile);
        Assert.AreEqual(310, doc.Snippets[0].SnippetByteStart);
        Assert.AreEqual(420, doc.Snippets[0].SnippetByteEnd);
        Assert.AreEqual(5, doc.Snippets[0].SnippetLineStart);
        Assert.AreEqual(23, doc.Snippets[0].SnippetLineEnd);
        Assert.AreEqual("GPL-2.0-only", doc.Snippets[0].ConcludedLicense);
        Assert.AreEqual(1, doc.Snippets[0].LicenseInfoInSnippet.Length);
        Assert.AreEqual("GPL-2.0-only", doc.Snippets[0].LicenseInfoInSnippet[0]);
        Assert.IsTrue(doc.Snippets[0].LicenseComments?.StartsWith("The concluded license was taken"));
        Assert.AreEqual("Copyright 2008-2010 John Smith", doc.Snippets[0].Copyright);
        Assert.IsTrue(doc.Snippets[0].Comment?.StartsWith("This snippet was identified as significant"));
        Assert.AreEqual("from linux kernel", doc.Snippets[0].Name);

        // Verify relationships
        Assert.AreEqual(7, doc.Relationships.Length);
        Assert.AreEqual("SPDXRef-DOCUMENT", doc.Relationships[0].SpdxElementId);
        Assert.AreEqual("SPDXRef-Package", doc.Relationships[0].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.Contains, doc.Relationships[0].RelationshipType);
        Assert.AreEqual("SPDXRef-DOCUMENT", doc.Relationships[1].SpdxElementId);
        Assert.AreEqual("DocumentRef-spdx-tool-1.2:SPDXRef-ToolsElement", doc.Relationships[1].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.CopyOf, doc.Relationships[1].RelationshipType);
        Assert.AreEqual("SPDXRef-Package", doc.Relationships[2].SpdxElementId);
        Assert.AreEqual("SPDXRef-Saxon", doc.Relationships[2].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.DynamicLink, doc.Relationships[2].RelationshipType);
        Assert.AreEqual("SPDXRef-CommonsLangSrc", doc.Relationships[3].SpdxElementId);
        Assert.AreEqual("NOASSERTION", doc.Relationships[3].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.GeneratedFrom, doc.Relationships[3].RelationshipType);
        Assert.AreEqual("SPDXRef-JenaLib", doc.Relationships[4].SpdxElementId);
        Assert.AreEqual("SPDXRef-Package", doc.Relationships[4].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.Contains, doc.Relationships[4].RelationshipType);
        Assert.AreEqual("SPDXRef-Specification", doc.Relationships[5].SpdxElementId);
        Assert.AreEqual("SPDXRef-fromDoap-0", doc.Relationships[5].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.SpecificationFor, doc.Relationships[5].RelationshipType);
        Assert.AreEqual("SPDXRef-File", doc.Relationships[6].SpdxElementId);
        Assert.AreEqual("SPDXRef-fromDoap-0", doc.Relationships[6].RelatedSpdxElement);
        Assert.AreEqual(SpdxRelationshipType.GeneratedFrom, doc.Relationships[6].RelationshipType);
    }
}