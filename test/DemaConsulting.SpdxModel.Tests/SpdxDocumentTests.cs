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

namespace DemaConsulting.SpdxModel.Tests;

/// <summary>
/// Tests for the <see cref="SpdxDocument"/> class.
/// </summary>
[TestClass]
public class SpdxDocumentTests
{
    /// <summary>
    /// Tests the <see cref="SpdxDocument.GetRootPackages"/> method.
    /// </summary>
    [TestMethod]
    public void TestGetRootPackages()
    {
        // Arrange
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

        // Act
        var packages = document.GetRootPackages();

        // Assert
        Assert.AreEqual(2, packages.Length);
        Assert.IsTrue(Array.Exists(packages, p => p.Id == "SPDXRef-Package1"));
        Assert.IsTrue(Array.Exists(packages, p => p.Id == "SPDXRef-Package2"));
    }

    /// <summary>
    /// Tests the <see cref="SpdxDocument.Same"/> comparer.
    /// </summary>
    [TestMethod]
    public void DocumentSameComparer()
    {
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

        // Assert documents compare to themselves
        Assert.IsTrue(SpdxDocument.Same.Equals(d1, d1));
        Assert.IsTrue(SpdxDocument.Same.Equals(d2, d2));
        Assert.IsTrue(SpdxDocument.Same.Equals(d3, d3));

        // Assert documents compare correctly
        Assert.IsTrue(SpdxDocument.Same.Equals(d1, d2));
        Assert.IsTrue(SpdxDocument.Same.Equals(d2, d1));
        Assert.IsFalse(SpdxDocument.Same.Equals(d1, d3));
        Assert.IsFalse(SpdxDocument.Same.Equals(d3, d1));
        Assert.IsFalse(SpdxDocument.Same.Equals(d2, d3));
        Assert.IsFalse(SpdxDocument.Same.Equals(d3, d2));

        // Assert same documents have identical hashes
        Assert.AreEqual(SpdxDocument.Same.GetHashCode(d1), SpdxDocument.Same.GetHashCode(d2));
    }

    /// <summary>
    /// Tests the <see cref="SpdxDocument.DeepCopy"/> method.
    /// </summary>
    [TestMethod]
    public void DeepCopy()
    {
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
            Snippets = [
                new SpdxSnippet
                {
                    SnippetFromFile = "SPDXRef-File1",
                    SnippetByteStart = 100,
                    SnippetByteEnd = 200
                }
            ],
            Relationships = [
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

        var d2 = d1.DeepCopy();

        // Assert both objects are equal
        Assert.AreEqual(d1, d2, SpdxDocument.Same);
        Assert.AreEqual(d1.Id, d2.Id);
        Assert.AreEqual(d1.Version, d2.Version);
        Assert.AreEqual(d1.Name, d2.Name);
        CollectionAssert.AreEquivalent(d1.ExternalDocumentReferences, d2.ExternalDocumentReferences, SpdxExternalDocumentReference.Same);
        CollectionAssert.AreEquivalent(d1.ExtractedLicensingInfo, d2.ExtractedLicensingInfo, SpdxExtractedLicensingInfo.Same);
        CollectionAssert.AreEquivalent(d1.Annotations, d2.Annotations, SpdxAnnotation.Same);
        CollectionAssert.AreEquivalent(d1.Files, d2.Files, SpdxFile.Same);
        CollectionAssert.AreEquivalent(d1.Packages, d2.Packages, SpdxPackage.Same);
        CollectionAssert.AreEquivalent(d1.Snippets, d2.Snippets, SpdxSnippet.Same);
        CollectionAssert.AreEquivalent(d1.Relationships, d2.Relationships, SpdxRelationship.Same);
        CollectionAssert.AreEqual(d1.Describes, d2.Describes);

        // Assert separate instances
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
    /// Tests the <see cref="SpdxDocument.Validate"/> method.
    /// </summary>
    [TestMethod]
    public void Validate()
    {
        var json22Example = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");

        // Deserialize the document
        var doc = SpdxModel.IO.Spdx2JsonDeserializer.Deserialize(json22Example);
        Assert.IsNotNull(doc);

        // Perform validation
        var issues = new List<string>();
        doc.Validate(issues);

        // Ensure no validation issues
        Assert.AreEqual(0, issues.Count);
    }

    /// <summary>
    /// Tests the <see cref="SpdxDocument.Validate"/> method detects duplicate IDs.
    /// </summary>
    [TestMethod]
    public void ValidateDuplicateIds()
    {
        // Arrange
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

        // Perform validation
        var issues = new List<string>();
        doc.Validate(issues);

        // Ensure no validation issues
        Assert.IsTrue(issues.Contains("Document Duplicate Element ID: SPDXRef-Package1"));
    }

    /// <summary>
    /// Tests the <see cref="SpdxDocument.Validate"/> method detects bad relationships.
    /// </summary>
    [TestMethod]
    public void ValidateBadRelationship()
    {
        // Arrange
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

        // Perform validation
        var issues = new List<string>();
        doc.Validate(issues);

        // Ensure no validation issues
        Assert.IsTrue(issues.Contains("Relationship Invalid Related SPDX Element Field: SPDXRef-Package2"));
    }

    /// <summary>
    /// Tests the <see cref="SpdxDocument.Validate"/> method detects NTIA issues.
    /// </summary>
    [TestMethod]
    public void ValidateNtia()
    {
        var json22Example = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");

        // Deserialize the document
        var doc = SpdxModel.IO.Spdx2JsonDeserializer.Deserialize(json22Example);
        Assert.IsNotNull(doc);

        // Perform validation
        var issues = new List<string>();
        doc.Validate(issues, true);

        // We expect NTIA validation issues from the example.
        Assert.IsTrue(issues.Contains("NTIA: Package Apache Commons Lang Missing Supplier"));
        Assert.IsTrue(issues.Contains("NTIA: Package Apache Commons Lang Missing Version"));
        Assert.IsTrue(issues.Contains("NTIA: Package Jena Missing Supplier"));
        Assert.IsTrue(issues.Contains("NTIA: Package Saxon Missing Supplier"));
    }

    /// <summary>
    /// Tests the <see cref="SpdxDocument.GetAllElements"/> method.
    /// </summary>
    [TestMethod]
    public void GetAllElements()
    {
        var json22Example = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");

        // Deserialize the document
        var doc = SpdxModel.IO.Spdx2JsonDeserializer.Deserialize(json22Example);
        Assert.IsNotNull(doc);

        // Get all elements
        var elements = doc.GetAllElements().ToList();

        // Ensure the document is in the list
        Assert.AreEqual(1, elements.OfType<SpdxDocument>().Count());
        Assert.IsNotNull(elements.Find(e => e.Id == "SPDXRef-DOCUMENT"));

        // Ensure all packages are in the list
        Assert.AreEqual(4, elements.OfType<SpdxPackage>().Count());
        Assert.IsNotNull(elements.Find(e => e.Id == "SPDXRef-Package"));
        Assert.IsNotNull(elements.Find(e => e.Id == "SPDXRef-fromDoap-1"));
        Assert.IsNotNull(elements.Find(e => e.Id == "SPDXRef-fromDoap-0"));
        Assert.IsNotNull(elements.Find(e => e.Id == "SPDXRef-Saxon"));

        // Ensure all files are in the list
        Assert.AreEqual(5, elements.OfType<SpdxFile>().Count());
        Assert.IsNotNull(elements.Find(e => e.Id == "SPDXRef-DoapSource"));
        Assert.IsNotNull(elements.Find(e => e.Id == "SPDXRef-CommonsLangSrc"));
        Assert.IsNotNull(elements.Find(e => e.Id == "SPDXRef-JenaLib"));
        Assert.IsNotNull(elements.Find(e => e.Id == "SPDXRef-Specification"));
        Assert.IsNotNull(elements.Find(e => e.Id == "SPDXRef-File"));
    }

    /// <summary>
    /// Tests the <see cref="SpdxDocument.GetElement"/> method.
    /// </summary>
    [TestMethod]
    public void GetElement()
    {
        var json22Example = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");

        // Deserialize the document
        var doc = SpdxModel.IO.Spdx2JsonDeserializer.Deserialize(json22Example);
        Assert.IsNotNull(doc);

        // Find a document
        var foundDoc = doc.GetElement<SpdxDocument>("SPDXRef-DOCUMENT");
        Assert.IsNotNull(foundDoc);
        Assert.AreEqual("SPDX-Tools-v2.0", foundDoc.Name);

        // Find a file
        var foundFile = doc.GetElement<SpdxFile>("SPDXRef-JenaLib");
        Assert.IsNotNull(foundFile);
        Assert.AreEqual("./lib-source/jena-2.6.3-sources.jar", foundFile.FileName);

        // Find a package
        var foundPackage = doc.GetElement<SpdxPackage>("SPDXRef-Saxon");
        Assert.IsNotNull(foundPackage);
        Assert.AreEqual("saxonB-8.8.zip", foundPackage.FileName);

        // Find a snippet
        var foundSnippet = doc.GetElement<SpdxSnippet>("SPDXRef-Snippet");
        Assert.IsNotNull(foundSnippet);
        Assert.AreEqual("SPDXRef-DoapSource", foundSnippet.SnippetFromFile);
    }
}