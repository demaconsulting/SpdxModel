namespace DemaConsulting.SpdxModel.Tests;

[TestClass]
public class SpdxDocumentTests
{
    [TestMethod]
    public void TestGetRootPackages()
    {
        // Arrange
        var document = new SpdxDocument
        {
            Id = "SPDXRef-DOCUMENT",
            Name = "Test Document",
            DocumentNamespace = "http://example.com/SpdxDocument",
            Packages = new[]
            {
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
            },
            Relationships = new[]
            {
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
            },
            Describes = new[] { "SPDXRef-Package1" }
        };

        // Act
        var packages = document.GetRootPackages();

        // Assert
        Assert.AreEqual(2, packages.Length);
        Assert.IsTrue(Array.Exists(packages, p => p.Id == "SPDXRef-Package1"));
        Assert.IsTrue(Array.Exists(packages, p => p.Id == "SPDXRef-Package2"));
    }

    [TestMethod]
    public void DocumentSameComparer()
    {
        var d1 = new SpdxDocument
        {
            Name = "DemaConsulting.SpdxModel-0.0.0",
            Packages = new[]
            {
                new SpdxPackage
                {
                    Id = "SPDXRef-Package1",
                    Name = "DemaConsulting.SpdxModel",
                    Version = "0.0.0"
                }
            },
            Describes = new[]
            {
                "SPDXRef-Package1"
            }
        };

        var d2 = new SpdxDocument
        {
            Id = "SPDXRef-DOCUMENT",
            Version = "SPDX-2.2",
            Name = "DemaConsulting.SpdxModel-0.0.0",
            Packages = new[]
            {
                new SpdxPackage
                {
                    Id = "SPDXRef-Package-SpdxModel",
                    Name = "DemaConsulting.SpdxModel",
                    Version = "0.0.0"
                }
            },
            Describes = new[]
            {
                "SPDXRef-Package-SpdxModel"
            }
        };

        var d3 = new SpdxDocument
        {
            Name = "SomePackage-1.2.3",
            Packages = new[]
            {
                new SpdxPackage
                {
                    Id = "SPDXRef-Package1",
                    Name = "SomePackage",
                    Version = "1.2.3"
                }
            },
            Describes = new[]
            {
                "SPDXRef-Package1"
            }
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
        Assert.IsTrue(SpdxDocument.Same.GetHashCode(d1) == SpdxDocument.Same.GetHashCode(d2));
    }

    [TestMethod]
    public void DeepCopy()
    {
        var d1 = new SpdxDocument
        {
            Id = "SPDXRef-DOCUMENT",
            Version = "SPDX-2.2",
            Name = "DemaConsulting.SpdxModel-0.0.0",
            Packages = new[]
            {
                new SpdxPackage
                {
                    Id = "SPDXRef-Package-SpdxModel",
                    Name = "DemaConsulting.SpdxModel",
                    Version = "0.0.0"
                }
            },
            Describes = new[]
            {
                "SPDXRef-Package-SpdxModel"
            }
        };

        var d2 = d1.DeepCopy();
        d2.Name = "TestName";

        Assert.IsFalse(ReferenceEquals(d1, d2));
        Assert.AreEqual("DemaConsulting.SpdxModel-0.0.0", d1.Name);
        Assert.AreEqual("TestName", d2.Name);
    }

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