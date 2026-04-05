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
///     Integration tests for the SpdxModel IO subsystem.
/// </summary>
[TestClass]
public class SpdxModelIOTests
{
    /// <summary>
    ///     Tests that an SPDX 2.2 document survives a JSON serialization round trip.
    /// </summary>
    [TestMethod]
    public void SpdxModelIO_ReadWriteSpdxJson_Spdx22Document_RoundTripProducesValidDocument()
    {
        // Arrange: Load the SPDX 2.2 JSON example from embedded resources
        var json = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.2.spdx.json");

        // Act: Deserialize, serialize, and deserialize again
        var original = Spdx2JsonDeserializer.Deserialize(json);
        var serialized = Spdx2JsonSerializer.Serialize(original);
        var roundTripped = Spdx2JsonDeserializer.Deserialize(serialized);

        // Assert: Verify the round-tripped document is valid and matches the original
        Assert.IsNotNull(roundTripped);
        Assert.AreEqual(original.Name, roundTripped.Name);
        Assert.AreEqual(original.Version, roundTripped.Version);
        var issues = new List<string>();
        roundTripped.Validate(issues);
        Assert.IsEmpty(issues);
    }

    /// <summary>
    ///     Tests that an SPDX 2.3 document survives a JSON serialization round trip.
    /// </summary>
    [TestMethod]
    public void SpdxModelIO_ReadWriteSpdxJson_Spdx23Document_RoundTripProducesValidDocument()
    {
        // Arrange: Load the SPDX 2.3 JSON example from embedded resources
        var json = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");

        // Act: Deserialize, serialize, and deserialize again
        var original = Spdx2JsonDeserializer.Deserialize(json);
        var serialized = Spdx2JsonSerializer.Serialize(original);
        var roundTripped = Spdx2JsonDeserializer.Deserialize(serialized);

        // Assert: Verify the round-tripped document is valid and matches the original
        Assert.IsNotNull(roundTripped);
        Assert.AreEqual(original.Name, roundTripped.Name);
        Assert.AreEqual(original.Version, roundTripped.Version);
        var issues = new List<string>();
        roundTripped.Validate(issues);
        Assert.IsEmpty(issues);
    }
}
