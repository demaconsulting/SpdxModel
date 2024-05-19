using DemaConsulting.SpdxModel.IO;

namespace DemaConsulting.SpdxModel.Tests.IO;

[TestClass]
public class Spdx2JsonSerializeCreationInformation
{
    [TestMethod]
    public void SerializeCreationInformation()
    {
        // Arrange
        var creationInformation = new SpdxCreationInformation
        {
            Comment =
                "This package has been shipped in source and binary form.\nThe binaries were created with gcc 4.5.1 and expect to link to\ncompatible system run time libraries.",
            Created = "2010-01-29T18:30:22Z",
            Creators = new[]
            {
                "Tool: LicenseFind-1.0",
                "Organization: ExampleCodeInspect ()",
                "Person: Jane Doe ()"
            },
            LicenseListVersion = "3.17"
        };

        // Act
        var json = Spdx2JsonSerializer.SerializeCreationInformation(creationInformation);

        // Assert
        Assert.AreEqual(
            "This package has been shipped in source and binary form.\nThe binaries were created with gcc 4.5.1 and expect to link to\ncompatible system run time libraries.",
            json["comment"]?.ToString());
        Assert.AreEqual("2010-01-29T18:30:22Z", json["created"]?.ToString());
        Assert.AreEqual("Tool: LicenseFind-1.0", json["creators"]?[0]?.ToString());
        Assert.AreEqual("Organization: ExampleCodeInspect ()", json["creators"]?[1]?.ToString());
        Assert.AreEqual("Person: Jane Doe ()", json["creators"]?[2]?.ToString());
        Assert.AreEqual("3.17", json["licenseListVersion"]?.ToString());
    }
}