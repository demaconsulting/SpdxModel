using DemaConsulting.SpdxModel.IO;

namespace DemaConsulting.SpdxModel.Tests.IO;

[TestClass]
public class SpdxJsonSerializeExternalReference
{
    [TestMethod]
    public void SerializeExternalReference()
    {
        // Arrange
        var reference = new SpdxExternalReference
        {
            Category = SpdxReferenceCategory.Security,
            Locator = "cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*",
            Type = "cpe23Type",
            Comment = "Example Comment"
        };

        // Act
        var json = SpdxJsonSerializer.SerializeExternalReference(reference);

        // Assert
        Assert.IsNotNull(json);
        Assert.AreEqual("SECURITY", json["referenceCategory"]?.ToString());
        Assert.AreEqual("cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*", json["referenceLocator"]?.ToString());
        Assert.AreEqual("cpe23Type", json["referenceType"]?.ToString());
        Assert.AreEqual("Example Comment", json["comment"]?.ToString());
    }

    [TestMethod]
    public void SerializeExternalReferences()
    {
        // Arrange
        var references = new[]
        {
            new SpdxExternalReference
            {
                Category = SpdxReferenceCategory.Security,
                Locator = "cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*",
                Type = "cpe23Type",
                Comment = "Example Comment"
            },
            new SpdxExternalReference
            {
                Category = SpdxReferenceCategory.Other,
                Locator = "acmecorp/acmenator/4.1.3-alpha",
                Type = "http://spdx.org/spdxdocs/spdx-example-444504E0-4F89-41D3-9A0C-0305E82C3301#LocationRef-acmeforge",
                Comment = "This is the external ref for Acme"
            }
        };

        // Act
        var json = SpdxJsonSerializer.SerializeExternalReferences(references);

        // Assert
        Assert.IsNotNull(json);
        Assert.AreEqual(2, json.Count);
        Assert.AreEqual("SECURITY", json[0]?["referenceCategory"]?.ToString());
        Assert.AreEqual("cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*", json[0]?["referenceLocator"]?.ToString());
        Assert.AreEqual("cpe23Type", json[0]?["referenceType"]?.ToString());
        Assert.AreEqual("Example Comment", json[0]?["comment"]?.ToString());
        Assert.AreEqual("OTHER", json[1]?["referenceCategory"]?.ToString());
        Assert.AreEqual("acmecorp/acmenator/4.1.3-alpha", json[1]?["referenceLocator"]?.ToString());
        Assert.AreEqual("http://spdx.org/spdxdocs/spdx-example-444504E0-4F89-41D3-9A0C-0305E82C3301#LocationRef-acmeforge", json[1]?["referenceType"]?.ToString());
        Assert.AreEqual("This is the external ref for Acme", json[1]?["comment"]?.ToString());
    }
}