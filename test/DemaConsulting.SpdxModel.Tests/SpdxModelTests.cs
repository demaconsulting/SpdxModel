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

    /// <summary>
    ///     Tests that malformed JSON throws a JsonException when deserialized.
    /// </summary>
    [TestMethod]
    public void SpdxModel_Deserialize_MalformedJson_ThrowsJsonException()
    {
        // Arrange: Prepare malformed JSON text
        const string malformedJson = "{ this is not valid json }";

        // Act / Assert: malformed JSON throws a JsonException
        try
        {
            Spdx2JsonDeserializer.Deserialize(malformedJson);
            Assert.Fail("Expected JsonException was not thrown.");
        }
        catch (System.Text.Json.JsonException)
        {
            // Expected — pass
        }
    }

    /// <summary>
    ///     Tests that an invalid SPDX document reports specific validation issues.
    /// </summary>
    [TestMethod]
    public void SpdxModel_Validate_InvalidDocument_ReportsIssues()
    {
        // Arrange: Create a deliberately incomplete SPDX document
        var document = new SpdxDocument
        {
            Id = "",
            Name = "",
            Version = "",
            DataLicense = "",
            DocumentNamespace = ""
        };

        // Act: Validate the document
        var issues = new List<string>();
        document.Validate(issues);

        // Assert: Verify that specific validation issues are reported
        Assert.IsTrue(issues.Count > 0, "Expected validation issues but none were reported.");
        Assert.IsTrue(issues.Exists(i => i.Contains("SPDX Version")),
            "Expected a SPDX Version validation issue.");
    }

    /// <summary>
    ///     Tests that required fields on SPDX data model types are non-nullable,
    ///     and optional fields are nullable.
    /// </summary>
    [TestMethod]
    public void SpdxModel_FieldOptionality_RequiredFieldsNotNull_OptionalFieldsNullable()
    {
        // Arrange: Create a default instance of key data model types
        var document = new SpdxDocument();
        var package = new SpdxPackage();
        var file = new SpdxFile();
        var relationship = new SpdxRelationship();

        // Act / Assert: default-constructed instances have the expected field nullability

        // Assert: Required fields are non-nullable (strings default to empty, not null)
        Assert.IsNotNull(document.Id);
        Assert.IsNotNull(document.Name);
        Assert.IsNotNull(document.Version);
        Assert.IsNotNull(document.DataLicense);
        Assert.IsNotNull(document.DocumentNamespace);
        Assert.IsNotNull(package.Id);
        Assert.IsNotNull(package.Name);
        Assert.IsNotNull(package.DownloadLocation);
        Assert.IsNotNull(file.Id);
        Assert.IsNotNull(file.FileName);
        Assert.IsNotNull(relationship.Id);
        Assert.IsNotNull(relationship.RelatedSpdxElement);

        // Assert: Optional fields are nullable
        Assert.IsNull(document.Comment);
        Assert.IsNull(package.Comment);
        Assert.IsNull(package.Version);
        Assert.IsNull(file.Comment);
        Assert.IsNull(relationship.Comment);
    }

    /// <summary>
    ///     Tests that SPDX date-time validation helper behavior is observable through the document model.
    /// </summary>
    /// <remarks>
    ///     Demonstrates that <see cref="SpdxHelpers.IsValidSpdxDateTime"/> is exercised end-to-end when
    ///     a real SPDX document is validated, satisfying the system-level observability requirement for
    ///     helper utilities.
    /// </remarks>
    [TestMethod]
    public void SpdxModel_Helpers_DateTimeValidation_IsObservableThroughDocumentModel()
    {
        // Arrange: Load and deserialize a real SPDX 2.3 document
        var json = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var document = Spdx2JsonDeserializer.Deserialize(json);

        // Act: Validate the document — validation internally invokes IsValidSpdxDateTime on
        // the creation-information timestamp, making helper behavior observable at system level
        var issues = new List<string>();
        document.Validate(issues);

        // Assert: The document is valid and the Created timestamp is a non-empty, well-formed value
        Assert.IsEmpty(issues);
        Assert.IsFalse(string.IsNullOrEmpty(document.CreationInformation.Created),
            "Expected the Created field to be non-empty after deserialization.");
    }

    /// <summary>
    ///     Tests that adding a relationship via the transform API is observable through the document model.
    /// </summary>
    /// <remarks>
    ///     Demonstrates end-to-end transform behavior: deserialize a document, add a relationship using
    ///     the public transform API, and verify the relationship is present in the document model.
    ///     This satisfies the system-level observability requirement for the transform subsystem.
    /// </remarks>
    [TestMethod]
    public void SpdxModel_Transform_AddRelationship_IsObservableThroughDocumentModel()
    {
        // Arrange: Load and deserialize a real SPDX 2.3 document
        var json = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var document = Spdx2JsonDeserializer.Deserialize(json);
        var initialCount = document.Relationships.Length;
        var newRelationship = new SpdxRelationship
        {
            Id = "SPDXRef-DOCUMENT",
            RelationshipType = SpdxRelationshipType.DependsOn,
            RelatedSpdxElement = "SPDXRef-Package"
        };

        // Act: Add a relationship using the transform public API
        DemaConsulting.SpdxModel.Transform.SpdxRelationships.Add(document, newRelationship);

        // Assert: The relationship is now present in the document
        Assert.AreEqual(initialCount + 1, document.Relationships.Length,
            "Expected document.Relationships to grow by one after Add.");
        Assert.IsTrue(
            Array.Exists(document.Relationships, r =>
                r.Id == "SPDXRef-DOCUMENT" &&
                r.RelationshipType == SpdxRelationshipType.DependsOn &&
                r.RelatedSpdxElement == "SPDXRef-Package"),
            "Expected the newly added relationship to be present in the document.");
    }
}
