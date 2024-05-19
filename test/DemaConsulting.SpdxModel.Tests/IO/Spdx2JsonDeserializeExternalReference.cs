using System.Text.Json.Nodes;
using DemaConsulting.SpdxModel.IO;

namespace DemaConsulting.SpdxModel.Tests.IO;

[TestClass]
public class Spdx2JsonDeserializeExternalReference
{
    [TestMethod]
    public void DeserializeExternalReference()
    {
        // Arrange
        var json = new JsonObject
        {
            ["comment"] = "This is just an example",
            ["referenceLocator"] = "cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*",
            ["referenceType"] = "cpe23Type",
            ["referenceCategory"] = "SECURITY"
        };

        // Act
        var reference = Spdx2JsonDeserializer.DeserializeExternalReference(json);

        // Assert
        Assert.AreEqual("This is just an example", reference.Comment);
        Assert.AreEqual("cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*", reference.Locator);
        Assert.AreEqual("cpe23Type", reference.Type);
        Assert.AreEqual(SpdxReferenceCategory.Security, reference.Category);
    }

    [TestMethod]
    public void DeserializeExternalReferences()
    {
        // Arrange
        var json = new JsonArray
        {
            new JsonObject
            {
                ["comment"] = "This is just an example",
                ["referenceLocator"] = "cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*",
                ["referenceType"] = "cpe23Type",
                ["referenceCategory"] = "SECURITY"
            },
            new JsonObject
            {
                ["comment"] = "This is the external ref for Acme",
                ["referenceLocator"] = "acmecorp/acmenator/4.1.3-alpha",
                ["referenceType"] =
                    "http://spdx.org/spdxdocs/spdx-example-444504E0-4F89-41D3-9A0C-0305E82C3301#LocationRef-acmeforge",
                ["referenceCategory"] = "OTHER"
            }
        };

        // Act
        var references = Spdx2JsonDeserializer.DeserializeExternalReferences(json);

        // Assert
        Assert.AreEqual(2, references.Length);
        Assert.AreEqual("This is just an example", references[0].Comment);
        Assert.AreEqual("cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*", references[0].Locator);
        Assert.AreEqual("cpe23Type", references[0].Type);
        Assert.AreEqual(SpdxReferenceCategory.Security, references[0].Category);
        Assert.AreEqual("This is the external ref for Acme", references[1].Comment);
        Assert.AreEqual("acmecorp/acmenator/4.1.3-alpha", references[1].Locator);
        Assert.AreEqual(
            "http://spdx.org/spdxdocs/spdx-example-444504E0-4F89-41D3-9A0C-0305E82C3301#LocationRef-acmeforge",
            references[1].Type);
        Assert.AreEqual(SpdxReferenceCategory.Other, references[1].Category);
    }
}