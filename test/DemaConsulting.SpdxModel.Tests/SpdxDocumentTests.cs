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

namespace DemaConsulting.SpdxModel.Tests;

/// <summary>
///     Tests for the <see cref="SpdxDocument" /> class.
/// </summary>
[TestClass]
public class SpdxDocumentTests
{
    /// <summary>
    ///     Tests the <see cref="SpdxDocument.GetRootPackages" /> method returns the correct root packages
    /// </summary>
    [TestMethod]
    public void SpdxDocument_GetRootPackages_CorrectPackages()
    {
        // Arrange: Create a sample SPDX document with multiple packages and relationships
        var document = new SpdxDocument
        {
            Id = "SPDXRef-DOCUMENT",
            Name = "Test Document",
            DocumentNamespace = "http://example.com/SpdxDocument",
            Packages =
            [
                new SpdxPackage
                {
                    Id = "SPDXRef-Package1"
                },
                new SpdxPackage
                {
                    Id = "SPDXRef-Package2"
                },
                new SpdxPackage
                {
                    Id = "SPDXRef-Package3"
                }
            ],
            Relationships =
            [
                new SpdxRelationship
                {
                    Id = "SPDXRef-DOCUMENT",
                    RelationshipType = SpdxRelationshipType.Describes,
                    RelatedSpdxElement = "SPDXRef-Package1"
                },
                new SpdxRelationship
                {
                    Id = "SPDXRef-Package2",
                    RelationshipType = SpdxRelationshipType.DescribedBy,
                    RelatedSpdxElement = "SPDXRef-DOCUMENT"
                }
            ],
            Describes = ["SPDXRef-Package1"]
        };

        // Act: Call the method to get root packages
        var packages = document.GetRootPackages();

        // Assert: Verify the correct root packages are returned
        Assert.HasCount(2, packages);
        Assert.IsTrue(Array.Exists(packages, p => p.Id == "SPDXRef-Package1"));
        Assert.IsTrue(Array.Exists(packages, p => p.Id == "SPDXRef-Package2"));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxDocument.Same" /> comparer compares documents correctly.
    /// </summary>
    [TestMethod]
    public void SpdxDocument_SameComparer_ComparesCorrectly()
    {
        // Arrange: Create three documents with different properties
        var d1 = new SpdxDocument
        {
            Name = "DemaConsulting.SpdxModel-0.0.0",
            Packages =
            [
                new SpdxPackage
                {
                    Id = "SPDXRef-Package1",
                    Name = "DemaConsulting.SpdxModel",
                    Version = "0.0.0"
                }
            ],
            Describes =
            [
                "SPDXRef-Package1"
            ]
        };
        var d2 = new SpdxDocument
        {
            Id = "SPDXRef-DOCUMENT",
            Version = "SPDX-2.2",
            Name = "DemaConsulting.SpdxModel-0.0.0",
            Packages =
            [
                new SpdxPackage
                {
                    Id = "SPDXRef-Package-SpdxModel",
                    Name = "DemaConsulting.SpdxModel",
                    Version = "0.0.0"
                }
            ],
            Describes =
            [
                "SPDXRef-Package-SpdxModel"
            ]
        };
        var d3 = new SpdxDocument
        {
            Name = "SomePackage-1.2.3",
            Packages =
            [
                new SpdxPackage
                {
                    Id = "SPDXRef-Package1",
                    Name = "SomePackage",
                    Version = "1.2.3"
                }
            ],
            Describes =
            [
                "SPDXRef-Package1"
            ]
        };

        // Assert: Verify documents compare to themselves
        Assert.IsTrue(SpdxDocument.Same.Equals(d1, d1));
        Assert.IsTrue(SpdxDocument.Same.Equals(d2, d2));
        Assert.IsTrue(SpdxDocument.Same.Equals(d3, d3));

        // Assert: Verify documents compare correctly
        Assert.IsTrue(SpdxDocument.Same.Equals(d1, d2));
        Assert.IsTrue(SpdxDocument.Same.Equals(d2, d1));
        Assert.IsFalse(SpdxDocument.Same.Equals(d1, d3));
        Assert.IsFalse(SpdxDocument.Same.Equals(d3, d1));
        Assert.IsFalse(SpdxDocument.Same.Equals(d2, d3));
        Assert.IsFalse(SpdxDocument.Same.Equals(d3, d2));

        // Assert: Verify same documents have identical hashes
        Assert.AreEqual(SpdxDocument.Same.GetHashCode(d1), SpdxDocument.Same.GetHashCode(d2));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxDocument.DeepCopy" /> method successfully creates a deep copy.
    /// </summary>
    [TestMethod]
    public void SpdxDocument_DeepCopy_CreatesEqualButDistinctInstance()
    {
        // Arrange: Create a sample SPDX document with various elements
        var d1 = new SpdxDocument
        {
            Id = "SPDXRef-DOCUMENT",
            Version = "SPDX-2.2",
            Name = "DemaConsulting.SpdxModel-0.0.0",
            ExternalDocumentReferences =
            [
                new SpdxExternalDocumentReference
                {
                    ExternalDocumentId = "DocumentRef-spdx-tool-1.2",
                    Checksum = new SpdxChecksum
                    {
                        Algorithm = SpdxChecksumAlgorithm.Sha1,
                        Value = "d6a770ba38583ed4bb4525bd96e50461655d2759"
                    }
                }
            ],
            ExtractedLicensingInfo =
            [
                new SpdxExtractedLicensingInfo
                {
                    LicenseId = "LicenseRef-1",
                    ExtractedText = "The CyberNeko Software License"
                }
            ],
            Annotations =
            [
                new SpdxAnnotation
                {
                    Annotator = "Person: Malcolm Nixon",
                    Date = "2024-05-28T01:30:00Z",
                    Type = SpdxAnnotationType.Review,
                    Comment = "Looks good"
                }
            ],
            Files =
            [
                new SpdxFile
                {
                    Id = "SPDXRef-File",
                    FileName = "test.txt",
                    Checksums =
                    [
                        new SpdxChecksum
                        {
                            Algorithm = SpdxChecksumAlgorithm.Sha1,
                            Value = "d6a770ba38583ed4bb4525bd96e50461655d2759"
                        }
                    ]
                }
            ],
            Packages =
            [
                new SpdxPackage
                {
                    Id = "SPDXRef-Package-SpdxModel",
                    Name = "DemaConsulting.SpdxModel",
                    Version = "0.0.0"
                }
            ],
            Snippets =
            [
                new SpdxSnippet
                {
                    SnippetFromFile = "SPDXRef-File1",
                    SnippetByteStart = 100,
                    SnippetByteEnd = 200
                }
            ],
            Relationships =
            [
                new SpdxRelationship
                {
                    Id = "SPDXRef-DOCUMENT",
                    RelationshipType = SpdxRelationshipType.Describes,
                    RelatedSpdxElement = "SPDXRef-Package-SpdxModel"
                }
            ],
            Describes =
            [
                "SPDXRef-Package-SpdxModel"
            ]
        };

        // Act: Create a deep copy of the original document
        var d2 = d1.DeepCopy();

        // Assert: Verify deep-copy is equal to original
        Assert.AreEqual(d1, d2, SpdxDocument.Same);
        Assert.AreEqual(d1.Id, d2.Id);
        Assert.AreEqual(d1.Version, d2.Version);
        Assert.AreEqual(d1.Name, d2.Name);
        CollectionAssert.AreEquivalent(d1.ExternalDocumentReferences, d2.ExternalDocumentReferences,
            SpdxExternalDocumentReference.Same);
        CollectionAssert.AreEquivalent(d1.ExtractedLicensingInfo, d2.ExtractedLicensingInfo,
            SpdxExtractedLicensingInfo.Same);
        CollectionAssert.AreEquivalent(d1.Annotations, d2.Annotations, SpdxAnnotation.Same);
        CollectionAssert.AreEquivalent(d1.Files, d2.Files, SpdxFile.Same);
        CollectionAssert.AreEquivalent(d1.Packages, d2.Packages, SpdxPackage.Same);
        CollectionAssert.AreEquivalent(d1.Snippets, d2.Snippets, SpdxSnippet.Same);
        CollectionAssert.AreEquivalent(d1.Relationships, d2.Relationships, SpdxRelationship.Same);
        CollectionAssert.AreEqual(d1.Describes, d2.Describes);

        // Assert: Verify deep-copy has distinct instances
        Assert.IsFalse(ReferenceEquals(d1, d2));
        Assert.IsFalse(ReferenceEquals(d1.ExternalDocumentReferences, d2.ExternalDocumentReferences));
        Assert.IsFalse(ReferenceEquals(d1.ExtractedLicensingInfo, d2.ExtractedLicensingInfo));
        Assert.IsFalse(ReferenceEquals(d1.Annotations, d2.Annotations));
        Assert.IsFalse(ReferenceEquals(d1.Files, d2.Files));
        Assert.IsFalse(ReferenceEquals(d1.Packages, d2.Packages));
        Assert.IsFalse(ReferenceEquals(d1.Snippets, d2.Snippets));
        Assert.IsFalse(ReferenceEquals(d1.Relationships, d2.Relationships));
        Assert.IsFalse(ReferenceEquals(d1.Describes, d2.Describes));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxDocument.Validate" /> method successfully validates a document with no issues.
    /// </summary>
    [TestMethod]
    public void SpdxDocument_Validate_NoIssues()
    {
        var json22Example = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");

        // Deserialize the document
        var doc = Spdx2JsonDeserializer.Deserialize(json22Example);
        Assert.IsNotNull(doc);

        // Perform validation
        var issues = new List<string>();
        doc.Validate(issues);

        // Ensure no validation issues
        Assert.IsEmpty(issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxDocument.Validate" /> method reports bad IDs.
    /// </summary>
    [TestMethod]
    public void SpdxDocument_Validate_BadId()
    {
        var json22Example = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");

        // Deserialize the document
        var doc = Spdx2JsonDeserializer.Deserialize(json22Example);
        Assert.IsNotNull(doc);

        // Corrupt the document
        doc.Id = "BadId";

        // Perform validation
        var issues = new List<string>();
        doc.Validate(issues);

        // Ensure issue reported
        Assert.Contains("Document Invalid SPDX Identifier Field 'BadId'", issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxDocument.Validate" /> method reports bad names.
    /// </summary>
    [TestMethod]
    public void SpdxDocument_Validate_BadName()
    {
        var json22Example = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");

        // Deserialize the document
        var doc = Spdx2JsonDeserializer.Deserialize(json22Example);
        Assert.IsNotNull(doc);

        // Corrupt the document
        doc.Name = "";

        // Perform validation
        var issues = new List<string>();
        doc.Validate(issues);

        // Ensure issue reported
        Assert.Contains("Document Invalid Document Name Field - Empty", issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxDocument.Validate" /> method reports bad versions.
    /// </summary>
    [TestMethod]
    public void SpdxDocument_Validate_BadVersion()
    {
        var json22Example = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");

        // Deserialize the document
        var doc = Spdx2JsonDeserializer.Deserialize(json22Example);
        Assert.IsNotNull(doc);

        // Corrupt the document
        doc.Version = "BadVersion";

        // Perform validation
        var issues = new List<string>();
        doc.Validate(issues);

        // Ensure issue reported
        Assert.Contains("Document Invalid SPDX Version Field 'BadVersion'", issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxDocument.Validate" /> method reports bad data licenses.
    /// </summary>
    [TestMethod]
    public void SpdxDocument_Validate_BadDataLicense()
    {
        var json22Example = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");

        // Deserialize the document
        var doc = Spdx2JsonDeserializer.Deserialize(json22Example);
        Assert.IsNotNull(doc);

        // Corrupt the document
        doc.DataLicense = "BadLicense";

        // Perform validation
        var issues = new List<string>();
        doc.Validate(issues);

        // Ensure issue reported
        Assert.Contains("Document Invalid Data License Field 'BadLicense'", issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxDocument.Validate" /> method reports bad data licenses.
    /// </summary>
    [TestMethod]
    public void SpdxDocument_Validate_BadNameSpace()
    {
        var json22Example = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");

        // Deserialize the document
        var doc = Spdx2JsonDeserializer.Deserialize(json22Example);
        Assert.IsNotNull(doc);

        // Corrupt the document
        doc.DocumentNamespace = "";

        // Perform validation
        var issues = new List<string>();
        doc.Validate(issues);

        // Ensure issue reported
        Assert.Contains("Document Invalid SPDX Document Namespace Field - Empty", issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxDocument.Validate" /> method detects duplicate IDs.
    /// </summary>
    [TestMethod]
    public void SpdxDocument_Validate_DuplicatePackageIds()
    {
        // Arrange: Create a sample SPDX document with duplicate package IDs
        var doc = new SpdxDocument
        {
            Id = "SPDXRef-DOCUMENT",
            Name = "Test Document",
            DocumentNamespace = "http://example.com/SpdxDocument",
            Packages =
            [
                new SpdxPackage
                {
                    Id = "SPDXRef-Package1"
                },
                new SpdxPackage
                {
                    Id = "SPDXRef-Package1"
                }
            ],
            Relationships =
            [
                new SpdxRelationship
                {
                    Id = "SPDXRef-DOCUMENT",
                    RelationshipType = SpdxRelationshipType.Describes,
                    RelatedSpdxElement = "SPDXRef-Package1"
                }
            ],
            Describes = ["SPDXRef-Package1"]
        };

        // Act: Perform validation
        var issues = new List<string>();
        doc.Validate(issues);

        // Assert: Verify duplicate ID reported
        Assert.Contains("Document Duplicate Element ID 'SPDXRef-Package1'", issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxDocument.Validate" /> method detects bad relationships.
    /// </summary>
    [TestMethod]
    public void SpdxDocument_Validate_BadRelationship()
    {
        // Arrange: Create a sample SPDX document with a relationship to a non-existent package
        var doc = new SpdxDocument
        {
            Id = "SPDXRef-DOCUMENT",
            Name = "Test Document",
            DocumentNamespace = "http://example.com/SpdxDocument",
            Packages =
            [
                new SpdxPackage
                {
                    Id = "SPDXRef-Package1"
                }
            ],
            Relationships =
            [
                new SpdxRelationship
                {
                    Id = "SPDXRef-DOCUMENT",
                    RelationshipType = SpdxRelationshipType.Describes,
                    RelatedSpdxElement = "SPDXRef-Package2"
                }
            ],
            Describes = ["SPDXRef-Package1"]
        };

        // Act: Perform validation
        var issues = new List<string>();
        doc.Validate(issues);

        // Assert: Verify relationship to non-existent package reported
        Assert.Contains("Relationship Invalid Related SPDX Element Field 'SPDXRef-Package2'", issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxDocument.Validate" /> method detects NTIA issues.
    /// </summary>
    [TestMethod]
    public void SpdxDocument_Validate_NtiaIssues()
    {
        // Arrange: Load a sample SPDX JSON document with known NTIA issues
        var json22Example = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var doc = Spdx2JsonDeserializer.Deserialize(json22Example);
        Assert.IsNotNull(doc);

        // Act: Perform validation
        var issues = new List<string>();
        doc.Validate(issues, true);

        // Assert: Verify expected NTIA validation issues are reported.
        Assert.Contains("NTIA: Package 'Apache Commons Lang' Missing Supplier", issues);
        Assert.Contains("NTIA: Package 'Apache Commons Lang' Missing Version", issues);
        Assert.Contains("NTIA: Package 'Jena' Missing Supplier", issues);
        Assert.Contains("NTIA: Package 'Saxon' Missing Supplier", issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxDocument.GetAllElements" /> method returns all elements in the document.
    /// </summary>
    [TestMethod]
    public void SpdxDocument_GetAllElements_Correct()
    {
        // Arrange: Load a sample SPDX JSON document
        var json22Example = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var doc = Spdx2JsonDeserializer.Deserialize(json22Example);
        Assert.IsNotNull(doc);

        // Act: Get all elements
        var elements = doc.GetAllElements().ToList();

        // Assert: Verify the document is in the list
        Assert.AreEqual(1, elements.OfType<SpdxDocument>().Count());
        Assert.IsNotNull(elements.Find(e => e.Id == "SPDXRef-DOCUMENT"));

        // Assert: Verify all packages are in the list
        Assert.AreEqual(4, elements.OfType<SpdxPackage>().Count());
        Assert.IsNotNull(elements.Find(e => e.Id == "SPDXRef-Package"));
        Assert.IsNotNull(elements.Find(e => e.Id == "SPDXRef-fromDoap-1"));
        Assert.IsNotNull(elements.Find(e => e.Id == "SPDXRef-fromDoap-0"));
        Assert.IsNotNull(elements.Find(e => e.Id == "SPDXRef-Saxon"));

        // Assert: Verify all files are in the list
        Assert.AreEqual(5, elements.OfType<SpdxFile>().Count());
        Assert.IsNotNull(elements.Find(e => e.Id == "SPDXRef-DoapSource"));
        Assert.IsNotNull(elements.Find(e => e.Id == "SPDXRef-CommonsLangSrc"));
        Assert.IsNotNull(elements.Find(e => e.Id == "SPDXRef-JenaLib"));
        Assert.IsNotNull(elements.Find(e => e.Id == "SPDXRef-Specification"));
        Assert.IsNotNull(elements.Find(e => e.Id == "SPDXRef-File"));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxDocument.GetElement" /> method returns the correct element by ID.
    /// </summary>
    [TestMethod]
    public void SpdxDocument_GetElement_Correct()
    {
        // Arrange: Load a sample SPDX JSON document
        var json22Example = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var doc = Spdx2JsonDeserializer.Deserialize(json22Example);
        Assert.IsNotNull(doc);

        // Assert: Verify finding the document returns the correct element
        var foundDoc = doc.GetElement<SpdxDocument>("SPDXRef-DOCUMENT");
        Assert.IsNotNull(foundDoc);
        Assert.AreEqual("SPDX-Tools-v2.0", foundDoc.Name);

        // Assert: Verify finding a file returns the correct element
        var foundFile = doc.GetElement<SpdxFile>("SPDXRef-JenaLib");
        Assert.IsNotNull(foundFile);
        Assert.AreEqual("./lib-source/jena-2.6.3-sources.jar", foundFile.FileName);

        // Assert: Verify finding a package returns the correct element
        var foundPackage = doc.GetElement<SpdxPackage>("SPDXRef-Saxon");
        Assert.IsNotNull(foundPackage);
        Assert.AreEqual("saxonB-8.8.zip", foundPackage.FileName);

        // Assert: Verify finding a snippet returns the correct element
        var foundSnippet = doc.GetElement<SpdxSnippet>("SPDXRef-Snippet");
        Assert.IsNotNull(foundSnippet);
        Assert.AreEqual("SPDXRef-DoapSource", foundSnippet.SnippetFromFile);
    }
}
