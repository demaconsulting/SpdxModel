using System.Text.Json.Nodes;
using DemaConsulting.SpdxModel.IO;

namespace DemaConsulting.SpdxModel.Tests.IO;

[TestClass]
public class Spdx2JsonDeserializePackageVerificationCode
{
    [TestMethod]
    public void DeserializePackageVerificationCode()
    {
        // Arrange
        var json = new JsonObject
        {
            ["packageVerificationCodeValue"] = "d3b07384d113edec49eaa6238ad5ff00",
            ["packageVerificationCodeExcludedFiles"] = new JsonArray
            {
                "file1.txt",
                "file2.txt"
            }
        };

        // Act
        var packageVerificationCode = Spdx2JsonDeserializer.DeserializeVerificationCode(json);
        Assert.IsNotNull(packageVerificationCode);

        // Assert
        Assert.AreEqual("d3b07384d113edec49eaa6238ad5ff00", packageVerificationCode.Value);
        Assert.AreEqual(2, packageVerificationCode.ExcludedFiles.Length);
        Assert.AreEqual("file1.txt", packageVerificationCode.ExcludedFiles[0]);
        Assert.AreEqual("file2.txt", packageVerificationCode.ExcludedFiles[1]);
    }
}