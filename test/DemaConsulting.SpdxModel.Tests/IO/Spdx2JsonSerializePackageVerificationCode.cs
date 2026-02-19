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

/// <summary>
///     Tests for serializing <see cref="SpdxPackageVerificationCode" /> to JSON.
/// </summary>
[TestClass]
public class Spdx2JsonSerializePackageVerificationCode
{
    /// <summary>
    ///     Tests serializing a package verification code.
    /// </summary>
    [TestMethod]
    public void Spdx2JsonSerializer_SerializePackageVerificationCode_CorrectResults()
    {
        // Arrange: Create a sample SpdxPackageVerificationCode object
        var code = new SpdxPackageVerificationCode
        {
            Value = "d3b07384d113edec49eaa6238ad5ff00",
            ExcludedFiles =
            [
                "file1.txt",
                "file2.txt"
            ]
        };

        // Act: Serialize the package verification code to JSON
        var json = Spdx2JsonSerializer.SerializeVerificationCode(code);

        // Assert: Verify the JSON is not null and has the expected structure
        Assert.IsNotNull(json);
        SpdxJsonHelpers.AssertEqual("d3b07384d113edec49eaa6238ad5ff00", json["packageVerificationCodeValue"]);
        SpdxJsonHelpers.AssertEqual("file1.txt", json["packageVerificationCodeExcludedFiles"]?[0]);
        SpdxJsonHelpers.AssertEqual("file2.txt", json["packageVerificationCodeExcludedFiles"]?[1]);
    }
}
