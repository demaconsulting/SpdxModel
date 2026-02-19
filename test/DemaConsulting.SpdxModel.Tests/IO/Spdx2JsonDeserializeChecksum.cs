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

/// <summary>
///     Tests for deserializing SPDX checksums to <see cref="SpdxChecksum" /> classes.
/// </summary>
[TestClass]
public class Spdx2JsonDeserializeChecksum
{
    /// <summary>
    ///     Tests deserializing a checksum.
    /// </summary>
    [TestMethod]
    public void Spdx2JsonDeserializer_DeserializeChecksum_CorrectResults()
    {
        // Arrange: Create a JSON object representing a checksum
        var json = new JsonObject
        {
            ["algorithm"] = "SHA1",
            ["checksumValue"] = "2fd4e1c67a2d28f123849ee1bb76e7391b93eb12"
        };

        // Act: Deserialize the JSON object to an SpdxChecksum object
        var checksum = Spdx2JsonDeserializer.DeserializeChecksum(json);

        // Assert: Verify the deserialized object has the expected properties
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, checksum.Algorithm);
        Assert.AreEqual("2fd4e1c67a2d28f123849ee1bb76e7391b93eb12", checksum.Value);
    }

    /// <summary>
    ///     Tests deserializing multiple checksums.
    /// </summary>
    [TestMethod]
    public void Spdx2JsonDeserializer_DeserializeChecksums_CorrectResults()
    {
        // Arrange: Create a JSON array representing multiple checksums
        var json = new JsonArray
        {
            new JsonObject
            {
                ["algorithm"] = "SHA1",
                ["checksumValue"] = "2fd4e1c67a2d28f123849ee1bb76e7391b93eb12"
            },
            new JsonObject
            {
                ["algorithm"] = "MD5",
                ["checksumValue"] = "d41d8cd98f00b204e9800998ecf8427e"
            }
        };

        // Act: Deserialize the JSON array to an array of SpdxChecksum objects
        var checksums = Spdx2JsonDeserializer.DeserializeChecksums(json);

        // Assert: Verify the deserialized array has the expected properties
        Assert.HasCount(2, checksums);
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, checksums[0].Algorithm);
        Assert.AreEqual("2fd4e1c67a2d28f123849ee1bb76e7391b93eb12", checksums[0].Value);
        Assert.AreEqual(SpdxChecksumAlgorithm.Md5, checksums[1].Algorithm);
        Assert.AreEqual("d41d8cd98f00b204e9800998ecf8427e", checksums[1].Value);
    }
}
