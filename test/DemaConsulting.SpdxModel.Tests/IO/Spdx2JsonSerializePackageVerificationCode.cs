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

namespace DemaConsulting.SpdxModel.Tests.IO;

[TestClass]
public class Spdx2JsonSerializePackageVerificationCode
{
    [TestMethod]
    public void SerializePackageVerificationCode()
    {
        // Arrange
        var code = new SpdxPackageVerificationCode
        {
            Value = "d3b07384d113edec49eaa6238ad5ff00",
            ExcludedFiles =
            [
                "file1.txt",
                "file2.txt"
            ]
        };

        // Act
        var json = Spdx2JsonSerializer.SerializeVerificationCode(code);

        // Assert
        Assert.IsNotNull(json);
        Assert.AreEqual("d3b07384d113edec49eaa6238ad5ff00", json["packageVerificationCodeValue"]?.ToString());
        Assert.AreEqual("file1.txt", json["packageVerificationCodeExcludedFiles"]?[0]?.ToString());
        Assert.AreEqual("file2.txt", json["packageVerificationCodeExcludedFiles"]?[1]?.ToString());
    }
}