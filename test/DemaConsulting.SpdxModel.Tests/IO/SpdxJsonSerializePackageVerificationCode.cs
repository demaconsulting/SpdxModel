using DemaConsulting.SpdxModel.IO;

namespace DemaConsulting.SpdxModel.Tests.IO;

[TestClass]
public class SpdxJsonSerializePackageVerificationCode
{
    [TestMethod]
    public void SerializePackageVerificationCode()
    {
        // Arrange
        var code = new SpdxPackageVerificationCode
        {
            Value = "d3b07384d113edec49eaa6238ad5ff00",
            ExcludedFiles = new[]
            {
                "file1.txt",
                "file2.txt"
            }
        };

        // Act
        var json = SpdxJsonSerializer.SerializeVerificationCode(code);

        // Assert
        Assert.IsNotNull(json);
        Assert.AreEqual("d3b07384d113edec49eaa6238ad5ff00", json["packageVerificationCodeValue"]?.ToString());
        Assert.AreEqual("file1.txt", json["packageVerificationCodeExcludedFiles"]?[0]?.ToString());
        Assert.AreEqual("file2.txt", json["packageVerificationCodeExcludedFiles"]?[1]?.ToString());
    }
}
