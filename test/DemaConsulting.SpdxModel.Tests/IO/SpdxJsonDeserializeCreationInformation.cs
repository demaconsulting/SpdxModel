using System.Text.Json.Nodes;
using DemaConsulting.SpdxModel.IO;

namespace DemaConsulting.SpdxModel.Tests.IO;

[TestClass]
public class SpdxJsonDeserializeCreationInformation
{
    [TestMethod]
    public void DeserializeCreationInformation()
    {
        // Arrange
        var json = new JsonObject
        {
            ["comment"] =
                "This package has been shipped in source and binary form.\nThe binaries were created with gcc 4.5.1 and expect to link to\ncompatible system run time libraries.",
            ["created"] = "2010-01-29T18:30:22Z",
            ["creators"] = new JsonArray
            {
                "Tool: LicenseFind-1.0",
                "Organization: ExampleCodeInspect ()",
                "Person: Jane Doe ()"
            },
            ["licenseListVersion"] = "3.17"
        };

        // Act
        var creationInformation = SpdxJsonDeserializer.DeserializeCreationInformation(json);

        // Assert
        Assert.AreEqual(
            "This package has been shipped in source and binary form.\nThe binaries were created with gcc 4.5.1 and expect to link to\ncompatible system run time libraries.",
            creationInformation.Comment);
        Assert.AreEqual("2010-01-29T18:30:22Z", creationInformation.Created);
        Assert.AreEqual(3, creationInformation.Creators.Length);
        Assert.AreEqual("Tool: LicenseFind-1.0", creationInformation.Creators[0]);
        Assert.AreEqual("Organization: ExampleCodeInspect ()", creationInformation.Creators[1]);
        Assert.AreEqual("Person: Jane Doe ()", creationInformation.Creators[2]);
        Assert.AreEqual("3.17", creationInformation.LicenseListVersion);
    }
}