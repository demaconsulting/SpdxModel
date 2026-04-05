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

namespace DemaConsulting.SpdxModel.Tests;

/// <summary>
///     System-level integration tests for the SpdxModel library.
/// </summary>
[TestClass]
public class SpdxModelTests
{
    /// <summary>
    ///     Tests that an SPDX 2.2 JSON document can be read by the library.
    /// </summary>
    [TestMethod]
    public void SpdxModel_ReadSpdxJson_Spdx22Example_ParsesSuccessfully()
    {
        // Arrange: Load the SPDX 2.2 JSON example from embedded resources
        var json = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.2.spdx.json");

        // Act: Deserialize the document using the library public API
        var document = Spdx2JsonDeserializer.Deserialize(json);

        // Assert: Verify the document was read correctly
        Assert.IsNotNull(document);
        Assert.AreEqual("SPDX-Tools-v2.0", document.Name);
        Assert.AreEqual("SPDX-2.2", document.Version);
        Assert.AreEqual("CC0-1.0", document.DataLicense);
    }

    /// <summary>
    ///     Tests that an SPDX 2.3 JSON document can be read by the library.
    /// </summary>
    [TestMethod]
    public void SpdxModel_ReadSpdxJson_Spdx23Example_ParsesSuccessfully()
    {
        // Arrange: Load the SPDX 2.3 JSON example from embedded resources
        var json = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");

        // Act: Deserialize the document using the library public API
        var document = Spdx2JsonDeserializer.Deserialize(json);

        // Assert: Verify the document was read correctly
        Assert.IsNotNull(document);
        Assert.AreEqual("SPDX-Tools-v2.0", document.Name);
        Assert.AreEqual("SPDX-2.3", document.Version);
        Assert.AreEqual("CC0-1.0", document.DataLicense);
    }

    /// <summary>
    ///     Tests that an SPDX 2.2 document loaded by the library passes validation.
    /// </summary>
    [TestMethod]
    public void SpdxModel_ReadSpdxJson_Spdx22Example_PassesValidation()
    {
        // Arrange: Load and deserialize the SPDX 2.2 JSON example
        var json = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.2.spdx.json");
        var document = Spdx2JsonDeserializer.Deserialize(json);

        // Act: Validate the document using the library public API
        var issues = new List<string>();
        document.Validate(issues);

        // Assert: Verify no validation issues were found
        Assert.IsEmpty(issues);
    }

    /// <summary>
    ///     Tests that an SPDX 2.3 document loaded by the library passes validation.
    /// </summary>
    [TestMethod]
    public void SpdxModel_ReadSpdxJson_Spdx23Example_PassesValidation()
    {
        // Arrange: Load and deserialize the SPDX 2.3 JSON example
        var json = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var document = Spdx2JsonDeserializer.Deserialize(json);

        // Act: Validate the document using the library public API
        var issues = new List<string>();
        document.Validate(issues);

        // Assert: Verify no validation issues were found
        Assert.IsEmpty(issues);
    }

    /// <summary>
    ///     Tests that root packages can be identified from a loaded SPDX document.
    /// </summary>
    [TestMethod]
    public void SpdxModel_ReadSpdxJson_Spdx23Example_RootPackagesIdentified()
    {
        // Arrange: Load and deserialize the SPDX 2.3 JSON example
        var json = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var document = Spdx2JsonDeserializer.Deserialize(json);

        // Act: Retrieve root packages using the library public API
        var rootPackages = document.GetRootPackages();

        // Assert: Verify that root packages were identified
        Assert.IsNotNull(rootPackages);
        Assert.IsTrue(rootPackages.Length > 0);
        Assert.IsTrue(Array.Exists(rootPackages, p => p.Id == "SPDXRef-Package"));
    }

    /// <summary>
    ///     Tests that a deep copy of a loaded SPDX document produces an equivalent document.
    /// </summary>
    [TestMethod]
    public void SpdxModel_ReadSpdxJson_Spdx23Example_DeepCopyProducesEquivalentDocument()
    {
        // Arrange: Load and deserialize the SPDX 2.3 JSON example
        var json = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var original = Spdx2JsonDeserializer.Deserialize(json);

        // Act: Deep copy the document using the library public API
        var copy = original.DeepCopy();

        // Assert: Verify the copy is equivalent but a distinct instance
        Assert.IsNotNull(copy);
        Assert.AreNotSame(original, copy);
        Assert.IsTrue(SpdxDocument.Same.Equals(original, copy));
    }

    /// <summary>
    ///     Tests that an SPDX document can be written and read back in a complete round trip.
    /// </summary>
    [TestMethod]
    public void SpdxModel_WriteReadSpdxJson_Spdx23Example_RoundTripSucceeds()
    {
        // Arrange: Load and deserialize the SPDX 2.3 JSON example
        var json = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var original = Spdx2JsonDeserializer.Deserialize(json);

        // Act: Serialize and then deserialize the document using the library public API
        var serialized = Spdx2JsonSerializer.Serialize(original);
        var roundTripped = Spdx2JsonDeserializer.Deserialize(serialized);

        // Assert: Verify the round-tripped document matches the original and passes validation
        Assert.IsNotNull(roundTripped);
        Assert.AreEqual(original.Name, roundTripped.Name);
        Assert.AreEqual(original.Version, roundTripped.Version);
        var issues = new List<string>();
        roundTripped.Validate(issues);
        Assert.IsEmpty(issues);
    }
}
