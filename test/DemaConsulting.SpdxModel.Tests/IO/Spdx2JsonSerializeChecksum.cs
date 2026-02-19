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
///     Tests for serializing <see cref="SpdxChecksum" /> to JSON.
/// </summary>
[TestClass]
public class Spdx2JsonSerializeChecksum
{
    /// <summary>
    ///     Tests serializing a checksum.
    /// </summary>
    [TestMethod]
    public void Spdx2JsonSerializer_SerializeChecksum_CorrectResults()
    {
        // Arrange: Create a sample checksum
        var checksum = new SpdxChecksum
        {
            Algorithm = SpdxChecksumAlgorithm.Sha1,
            Value = "2fd4e1c67a2d28f123849ee1bb76e7391b93eb12"
        };

        // Act: Serialize the checksum to JSON
        var json = Spdx2JsonSerializer.SerializeChecksum(checksum);

        // Assert: Verify the JSON is not null and has the expected structure
        Assert.IsNotNull(json);
        SpdxJsonHelpers.AssertEqual("SHA1", json["algorithm"]);
        SpdxJsonHelpers.AssertEqual("2fd4e1c67a2d28f123849ee1bb76e7391b93eb12", json["checksumValue"]);
    }

    /// <summary>
    ///     Tests serializing multiple checksums.
    /// </summary>
    [TestMethod]
    public void Spdx2JsonSerializer_SerializeChecksums_CorrectResults()
    {
        // Arrange: Create sample checksums
        var checksums = new[]
        {
            new SpdxChecksum
            {
                Algorithm = SpdxChecksumAlgorithm.Sha1,
                Value = "2fd4e1c67a2d28f123849ee1bb76e7391b93eb12"
            },
            new SpdxChecksum
            {
                Algorithm = SpdxChecksumAlgorithm.Md5,
                Value = "d41d8cd98f00b204e9800998ecf8427e"
            }
        };

        // Act: Serialize the checksums to JSON
        var json = Spdx2JsonSerializer.SerializeChecksums(checksums);

        // Assert: Verify the JSON is not null and has the expected structure
        Assert.IsNotNull(json);
        Assert.AreEqual(2, json.Count);
        SpdxJsonHelpers.AssertEqual("SHA1", json[0]?["algorithm"]);
        SpdxJsonHelpers.AssertEqual("2fd4e1c67a2d28f123849ee1bb76e7391b93eb12", json[0]?["checksumValue"]);
        SpdxJsonHelpers.AssertEqual("MD5", json[1]?["algorithm"]);
        SpdxJsonHelpers.AssertEqual("d41d8cd98f00b204e9800998ecf8427e", json[1]?["checksumValue"]);
    }
}
