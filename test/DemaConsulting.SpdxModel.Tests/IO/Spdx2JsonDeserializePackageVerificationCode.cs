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