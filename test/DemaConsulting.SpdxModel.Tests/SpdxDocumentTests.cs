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
                    Id = "SPDXRef-Package1",
                },
                new SpdxPackage
                {
                    Id = "SPDXRef-Package2",
                },
                new SpdxPackage
                {
                    Id = "SPDXRef-Package3",
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
}