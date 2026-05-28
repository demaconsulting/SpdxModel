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
/// <remarks>
///     Tests the <see cref="SpdxDocument"/> class using xUnit v3. Each test constructs its own document state
///     from scratch or deserializes the embedded JSON fixture
///     <c>SPDXJSONExample-v2.3.spdx.json</c>; no shared instance state is used.
/// </remarks>
public class SpdxDocumentTests
{
    /// <summary>
    ///     Tests the <see cref="SpdxDocument.GetRootPackages" /> method returns the correct root packages
    /// </summary>
    /// <remarks>
    ///     Constructs a document with three packages and two root-package indicators (one via the
    ///     Describes list and one via a DescribedBy relationship) to verify that GetRootPackages
    ///     returns exactly the two packages named as roots and excludes the third.
    /// </remarks>
    [Fact]
    public void SpdxDocument_GetRootPackages_WithDescribesAndRelationships_ReturnsCorrectPackages()
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
        Assert.Equal(2, packages.Length);
        Assert.True(Array.Exists(packages, p => p.Id == "SPDXRef-Package1"));
        Assert.True(Array.Exists(packages, p => p.Id == "SPDXRef-Package2"));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxDocument.Same" /> comparer compares documents correctly.
    /// </summary>
    /// <remarks>
    ///     Sets up three documents: d1 and d2 both describe a root package with the same name
    ///     and version (but different IDs and versions), and d3 describes a different package.
    ///     Verifies reflexive, symmetric, cross-inequality comparisons, and hash-code consistency
    ///     for equal documents.
    /// </remarks>
    [Fact]
    public void SpdxDocument_Same_DocumentsWithMatchingRootPackages_AreEqual()
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

        // Act / Assert: Verify documents compare to themselves
        Assert.True(SpdxDocument.Same.Equals(d1, d1));
        Assert.True(SpdxDocument.Same.Equals(d2, d2));
        Assert.True(SpdxDocument.Same.Equals(d3, d3));

        // Act / Assert: Verify documents compare correctly
        Assert.True(SpdxDocument.Same.Equals(d1, d2));
        Assert.True(SpdxDocument.Same.Equals(d2, d1));
        Assert.False(SpdxDocument.Same.Equals(d1, d3));
        Assert.False(SpdxDocument.Same.Equals(d3, d1));
        Assert.False(SpdxDocument.Same.Equals(d2, d3));
        Assert.False(SpdxDocument.Same.Equals(d3, d2));

        // Assert: Verify same documents have identical hashes
        Assert.Equal(SpdxDocument.Same.GetHashCode(d1), SpdxDocument.Same.GetHashCode(d2));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxDocument.DeepCopy" /> method successfully creates a deep copy.
    /// </summary>
    /// <remarks>
    ///     Constructs a fully-populated document with external document references, extracted
    ///     licensing info, annotations, files, packages, snippets, relationships, and a Describes
    ///     list, then deep-copies it. Verifies that every collection and scalar field is equal
    ///     but that all array references are distinct from the original.
    /// </remarks>
    [Fact]
    public void SpdxDocument_DeepCopy_WithPopulatedDocument_CreatesEqualButDistinctInstance()
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
        Assert.Equal(d1, d2, SpdxDocument.Same);
        Assert.Equal(d1.Id, d2.Id);
        Assert.Equal(d1.Version, d2.Version);
        Assert.Equal(d1.Name, d2.Name);
        SpdxTestHelpers.AssertEquivalent(d1.ExternalDocumentReferences, d2.ExternalDocumentReferences,
            SpdxExternalDocumentReference.Same);
        SpdxTestHelpers.AssertEquivalent(d1.ExtractedLicensingInfo, d2.ExtractedLicensingInfo,
            SpdxExtractedLicensingInfo.Same);
        SpdxTestHelpers.AssertEquivalent(d1.Annotations, d2.Annotations, SpdxAnnotation.Same);
        SpdxTestHelpers.AssertEquivalent(d1.Files, d2.Files, SpdxFile.Same);
        SpdxTestHelpers.AssertEquivalent(d1.Packages, d2.Packages, SpdxPackage.Same);
        SpdxTestHelpers.AssertEquivalent(d1.Snippets, d2.Snippets, SpdxSnippet.Same);
        SpdxTestHelpers.AssertEquivalent(d1.Relationships, d2.Relationships, SpdxRelationship.Same);
        Assert.Equal(d1.Describes, d2.Describes);

        // Assert: Verify deep-copy has distinct instances
        Assert.False(ReferenceEquals(d1, d2));
        Assert.False(ReferenceEquals(d1.ExternalDocumentReferences, d2.ExternalDocumentReferences));
        Assert.False(ReferenceEquals(d1.ExtractedLicensingInfo, d2.ExtractedLicensingInfo));
        Assert.False(ReferenceEquals(d1.Annotations, d2.Annotations));
        Assert.False(ReferenceEquals(d1.Files, d2.Files));
        Assert.False(ReferenceEquals(d1.Packages, d2.Packages));
        Assert.False(ReferenceEquals(d1.Snippets, d2.Snippets));
        Assert.False(ReferenceEquals(d1.Relationships, d2.Relationships));
        Assert.False(ReferenceEquals(d1.Describes, d2.Describes));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxDocument.Validate" /> method successfully validates a document with no issues.
    /// </summary>
    /// <remarks>
    ///     Deserializes <c>SPDXJSONExample-v2.3.spdx.json</c> (a known-good SPDX 2.3 example)
    ///     to obtain a fully valid document and verifies that the validator reports no issues.
    ///     Using the embedded JSON fixture ensures the document satisfies all field constraints.
    /// </remarks>
    [Fact]
    public void SpdxDocument_Validate_ValidDocument_ReportsNoIssues()
    {
        // Arrange: Load a valid SPDX JSON document
        var json22Example = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var doc = Spdx2JsonDeserializer.Deserialize(json22Example);
        Assert.NotNull(doc);

        // Act: Perform validation on the document
        var issues = new List<string>();
        doc.Validate(issues);

        // Assert: Verify no validation issues are reported
        Assert.Empty(issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxDocument.Validate" /> method reports invalid IDs.
    /// </summary>
    /// <remarks>
    ///     Deserializes the embedded JSON fixture to obtain a valid baseline document, then
    ///     overwrites its SPDX-ID with <c>"BadId"</c>. Verifies that the validator reports
    ///     the expected diagnostic for a malformed SPDX identifier.
    /// </remarks>
    [Fact]
    public void SpdxDocument_Validate_InvalidId_ReportsIssue()
    {
        // Arrange: Load and deserialize a valid SPDX document
        var json22Example = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var doc = Spdx2JsonDeserializer.Deserialize(json22Example);
        Assert.NotNull(doc);

        // Arrange: Corrupt the document with invalid ID
        doc.Id = "BadId";

        // Act: Perform validation
        var issues = new List<string>();
        doc.Validate(issues);

        // Assert: Verify issue is reported
        Assert.Contains("Document Invalid SPDX Identifier Field 'BadId'", issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxDocument.Validate" /> method reports invalid names.
    /// </summary>
    /// <remarks>
    ///     Deserializes the embedded JSON fixture to obtain a valid baseline document, then
    ///     clears its name field. Verifies that the validator reports the expected diagnostic
    ///     for an empty document name.
    /// </remarks>
    [Fact]
    public void SpdxDocument_Validate_InvalidName_ReportsIssue()
    {
        // Arrange: Load and deserialize a valid SPDX document
        var json22Example = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var doc = Spdx2JsonDeserializer.Deserialize(json22Example);
        Assert.NotNull(doc);

        // Arrange: Corrupt the document with empty name
        doc.Name = "";

        // Act: Perform validation
        var issues = new List<string>();
        doc.Validate(issues);

        // Assert: Verify issue is reported
        Assert.Contains("Document Invalid Document Name Field - Empty", issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxDocument.Validate" /> method reports invalid versions.
    /// </summary>
    /// <remarks>
    ///     Deserializes the embedded JSON fixture to obtain a valid baseline document, then
    ///     overwrites the version with <c>"BadVersion"</c>. Verifies that the validator reports
    ///     the expected diagnostic for a version string that does not match the SPDX-2.x pattern.
    /// </remarks>
    [Fact]
    public void SpdxDocument_Validate_InvalidVersion_ReportsIssue()
    {
        // Arrange: Load and deserialize a valid SPDX document
        var json22Example = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var doc = Spdx2JsonDeserializer.Deserialize(json22Example);
        Assert.NotNull(doc);

        // Arrange: Corrupt the document with invalid version
        doc.Version = "BadVersion";

        // Act: Perform validation
        var issues = new List<string>();
        doc.Validate(issues);

        // Assert: Verify issue is reported
        Assert.Contains("Document Invalid SPDX Version Field 'BadVersion'", issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxDocument.Validate" /> method reports invalid data licenses.
    /// </summary>
    /// <remarks>
    ///     Deserializes the embedded JSON fixture to obtain a valid baseline document, then
    ///     overwrites the data license with <c>"BadLicense"</c>. Verifies that the validator
    ///     reports the expected diagnostic for a value other than the mandatory CC0-1.0 license.
    /// </remarks>
    [Fact]
    public void SpdxDocument_Validate_InvalidDataLicense_ReportsIssue()
    {
        // Arrange: Load and deserialize a valid SPDX document
        var json22Example = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var doc = Spdx2JsonDeserializer.Deserialize(json22Example);
        Assert.NotNull(doc);

        // Arrange: Corrupt the document with invalid data license
        doc.DataLicense = "BadLicense";

        // Act: Perform validation
        var issues = new List<string>();
        doc.Validate(issues);

        // Assert: Verify issue is reported
        Assert.Contains("Document Invalid Data License Field 'BadLicense'", issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxDocument.Validate" /> method reports invalid namespaces.
    /// </summary>
    /// <remarks>
    ///     Deserializes the embedded JSON fixture to obtain a valid baseline document, then
    ///     clears the namespace URI. Verifies that the validator reports the expected diagnostic
    ///     for an empty document namespace.
    /// </remarks>
    [Fact]
    public void SpdxDocument_Validate_InvalidNameSpace_ReportsIssue()
    {
        // Arrange: Load and deserialize a valid SPDX document
        var json22Example = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var doc = Spdx2JsonDeserializer.Deserialize(json22Example);
        Assert.NotNull(doc);

        // Arrange: Corrupt the document with empty namespace
        doc.DocumentNamespace = "";

        // Act: Perform validation
        var issues = new List<string>();
        doc.Validate(issues);

        // Assert: Verify issue is reported
        Assert.Contains("Document Invalid SPDX Document Namespace Field - Empty", issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxDocument.Validate" /> method detects duplicate IDs.
    /// </summary>
    /// <remarks>
    ///     Constructs a minimal document containing two packages with the same SPDX-ID. Verifies
    ///     that the validator reports the expected duplicate-ID diagnostic rather than silently
    ///     accepting the malformed document.
    /// </remarks>
    [Fact]
    public void SpdxDocument_Validate_DuplicatePackageIds_ReportsIssue()
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
    /// <remarks>
    ///     Constructs a minimal document containing a relationship whose
    ///     <c>RelatedSpdxElement</c> references an ID that does not exist in the document.
    ///     Verifies that the validator reports the expected dangling-reference diagnostic.
    /// </remarks>
    [Fact]
    public void SpdxDocument_Validate_InvalidRelationship_ReportsIssue()
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
    /// <remarks>
    ///     Deserializes the embedded JSON fixture, which deliberately omits required NTIA fields
    ///     for some packages (supplier and version for Apache Commons Lang; supplier for Jena and
    ///     Saxon). Verifies that the NTIA validation mode reports exactly those expected issues.
    /// </remarks>
    [Fact]
    public void SpdxDocument_Validate_NtiaMinimumElements_ReportsIssues()
    {
        // Arrange: Load a sample SPDX JSON document with known NTIA issues
        var json22Example = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var doc = Spdx2JsonDeserializer.Deserialize(json22Example);
        Assert.NotNull(doc);

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
    /// <remarks>
    ///     Deserializes the embedded JSON fixture, which contains a document element, four
    ///     packages, and five files. Verifies that <see cref="SpdxDocument.GetAllElements"/>
    ///     returns exactly those elements and that <see cref="SpdxRelationship"/> entries are
    ///     excluded from the result.
    /// </remarks>
    [Fact]
    public void SpdxDocument_GetAllElements_WithMixedElements_ReturnsAllNonRelationshipElements()
    {
        // Arrange: Load a sample SPDX JSON document
        var json22Example = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var doc = Spdx2JsonDeserializer.Deserialize(json22Example);
        Assert.NotNull(doc);

        // Act: Get all elements
        var elements = doc.GetAllElements().ToList();

        // Assert: Verify the document is in the list
        Assert.Single(elements.OfType<SpdxDocument>());
        Assert.NotNull(elements.Find(e => e.Id == "SPDXRef-DOCUMENT"));

        // Assert: Verify all packages are in the list
        Assert.Equal(4, elements.OfType<SpdxPackage>().Count());
        Assert.NotNull(elements.Find(e => e.Id == "SPDXRef-Package"));
        Assert.NotNull(elements.Find(e => e.Id == "SPDXRef-fromDoap-1"));
        Assert.NotNull(elements.Find(e => e.Id == "SPDXRef-fromDoap-0"));
        Assert.NotNull(elements.Find(e => e.Id == "SPDXRef-Saxon"));

        // Assert: Verify all files are in the list
        Assert.Equal(5, elements.OfType<SpdxFile>().Count());
        Assert.NotNull(elements.Find(e => e.Id == "SPDXRef-DoapSource"));
        Assert.NotNull(elements.Find(e => e.Id == "SPDXRef-CommonsLangSrc"));
        Assert.NotNull(elements.Find(e => e.Id == "SPDXRef-JenaLib"));
        Assert.NotNull(elements.Find(e => e.Id == "SPDXRef-Specification"));
        Assert.NotNull(elements.Find(e => e.Id == "SPDXRef-File"));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxDocument.GetElement{T}" /> method returns the document element.
    /// </summary>
    /// <remarks>
    ///     Deserializes the embedded JSON fixture and queries for the document element by its
    ///     SPDX-ID. Verifies that the returned object is the document itself and has the expected
    ///     document name.
    /// </remarks>
    [Fact]
    public void SpdxDocument_GetElement_Document_ReturnsDocumentElement()
    {
        // Arrange: Load a sample SPDX JSON document
        var json22Example = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var doc = Spdx2JsonDeserializer.Deserialize(json22Example);
        Assert.NotNull(doc);

        // Act: Find the document element by ID
        var foundDoc = doc.GetElement<SpdxDocument>("SPDXRef-DOCUMENT");

        // Assert: Verify the document element is correct
        Assert.NotNull(foundDoc);
        Assert.Equal("SPDX-Tools-v2.0", foundDoc.Name);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxDocument.GetElement{T}" /> method returns the correct file element.
    /// </summary>
    /// <remarks>
    ///     Deserializes the embedded JSON fixture and queries for a file element by its SPDX-ID.
    ///     Verifies that the returned object is the correct file and has the expected file name.
    /// </remarks>
    [Fact]
    public void SpdxDocument_GetElement_File_ReturnsFileElement()
    {
        // Arrange: Load a sample SPDX JSON document
        var json22Example = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var doc = Spdx2JsonDeserializer.Deserialize(json22Example);
        Assert.NotNull(doc);

        // Act: Find a file element by ID
        var foundFile = doc.GetElement<SpdxFile>("SPDXRef-JenaLib");

        // Assert: Verify the file element is correct
        Assert.NotNull(foundFile);
        Assert.Equal("./lib-source/jena-2.6.3-sources.jar", foundFile.FileName);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxDocument.GetElement{T}" /> method returns the correct package element.
    /// </summary>
    /// <remarks>
    ///     Deserializes the embedded JSON fixture and queries for a package element by its
    ///     SPDX-ID. Verifies that the returned object is the correct package and has the
    ///     expected file name property.
    /// </remarks>
    [Fact]
    public void SpdxDocument_GetElement_Package_ReturnsPackageElement()
    {
        // Arrange: Load a sample SPDX JSON document
        var json22Example = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var doc = Spdx2JsonDeserializer.Deserialize(json22Example);
        Assert.NotNull(doc);

        // Act: Find a package element by ID
        var foundPackage = doc.GetElement<SpdxPackage>("SPDXRef-Saxon");

        // Assert: Verify the package element is correct
        Assert.NotNull(foundPackage);
        Assert.Equal("saxonB-8.8.zip", foundPackage.FileName);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxDocument.GetElement{T}" /> method returns the correct snippet element.
    /// </summary>
    /// <remarks>
    ///     Deserializes the embedded JSON fixture and queries for a snippet element by its
    ///     SPDX-ID. Verifies that the returned object is the correct snippet and references
    ///     the expected source file SPDX-ID.
    /// </remarks>
    [Fact]
    public void SpdxDocument_GetElement_Snippet_ReturnsSnippetElement()
    {
        // Arrange: Load a sample SPDX JSON document
        var json22Example = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var doc = Spdx2JsonDeserializer.Deserialize(json22Example);
        Assert.NotNull(doc);

        // Act: Find a snippet element by ID
        var foundSnippet = doc.GetElement<SpdxSnippet>("SPDXRef-Snippet");

        // Assert: Verify the snippet element is correct
        Assert.NotNull(foundSnippet);
        Assert.Equal("SPDXRef-DoapSource", foundSnippet.SnippetFromFile);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxDocument.Validate" /> method validates document-level annotations.
    /// </summary>
    /// <remarks>
    ///     Constructs a minimal valid document that contains a single annotation with an empty
    ///     Annotator field. Verifies that the validator reports the annotation-level issue using
    ///     the document element prefix so the issue can be attributed to the correct context.
    /// </remarks>
    [Fact]
    public void SpdxDocument_Validate_InvalidAnnotation_ReportsIssue()
    {
        // Arrange: Create a document with an invalid annotation
        var doc = new SpdxDocument
        {
            Id = "SPDXRef-DOCUMENT",
            Name = "Test Document",
            Version = "SPDX-2.3",
            DataLicense = "CC0-1.0",
            DocumentNamespace = "http://example.com/SpdxDocument",
            CreationInformation = new SpdxCreationInformation
            {
                Creators = ["Tool: test"],
                Created = "2024-01-01T00:00:00Z"
            },
            Annotations =
            [
                new SpdxAnnotation
                {
                    Annotator = "",
                    Date = "2024-05-28T01:30:00Z",
                    Type = SpdxAnnotationType.Review,
                    Comment = "Looks good"
                }
            ]
        };

        // Act: Perform validation
        var issues = new List<string>();
        doc.Validate(issues);

        // Assert: Verify the annotation issue is reported with the "Document" prefix
        Assert.Contains("Document Invalid Annotator Field - Empty", issues);
    }
}
