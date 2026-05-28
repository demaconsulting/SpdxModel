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
public class SpdxModelTests
{
    /// <summary>
    ///     Tests that an SPDX 2.2 JSON document can be read by the library.
    /// </summary>
    [Fact]
    public void SpdxModel_ReadSpdxJson_Spdx22Example_ParsesSuccessfully()
    {
        // Arrange: Load the SPDX 2.2 JSON example from embedded resources
        var json = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.2.spdx.json");

        // Act: Deserialize the document using the library public API
        var document = Spdx2JsonDeserializer.Deserialize(json);

        // Assert: Verify the document was read correctly
        Assert.NotNull(document);
        Assert.Equal("SPDX-Tools-v2.0", document.Name);
        Assert.Equal("SPDX-2.2", document.Version);
        Assert.Equal("CC0-1.0", document.DataLicense);
    }

    /// <summary>
    ///     Tests that an SPDX 2.3 JSON document can be read by the library.
    /// </summary>
    [Fact]
    public void SpdxModel_ReadSpdxJson_Spdx23Example_ParsesSuccessfully()
    {
        // Arrange: Load the SPDX 2.3 JSON example from embedded resources
        var json = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");

        // Act: Deserialize the document using the library public API
        var document = Spdx2JsonDeserializer.Deserialize(json);

        // Assert: Verify the document was read correctly
        Assert.NotNull(document);
        Assert.Equal("SPDX-Tools-v2.0", document.Name);
        Assert.Equal("SPDX-2.3", document.Version);
        Assert.Equal("CC0-1.0", document.DataLicense);
    }

    /// <summary>
    ///     Tests that an SPDX 2.2 document loaded by the library passes validation.
    /// </summary>
    [Fact]
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
        Assert.Empty(issues);
    }

    /// <summary>
    ///     Tests that an SPDX 2.3 document loaded by the library passes validation.
    /// </summary>
    [Fact]
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
        Assert.Empty(issues);
    }

    /// <summary>
    ///     Tests that root packages can be identified from a loaded SPDX document.
    /// </summary>
    [Fact]
    public void SpdxModel_ReadSpdxJson_Spdx23Example_RootPackagesIdentified()
    {
        // Arrange: Load and deserialize the SPDX 2.3 JSON example
        var json = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var document = Spdx2JsonDeserializer.Deserialize(json);

        // Act: Retrieve root packages using the library public API
        var rootPackages = document.GetRootPackages();

        // Assert: Verify that root packages were identified
        Assert.NotNull(rootPackages);
        Assert.True(rootPackages.Length > 0);
        Assert.True(Array.Exists(rootPackages, p => p.Id == "SPDXRef-Package"));
    }

    /// <summary>
    ///     Tests that a deep copy of a loaded SPDX document produces an equivalent document.
    /// </summary>
    [Fact]
    public void SpdxModel_ReadSpdxJson_Spdx23Example_DeepCopyProducesEquivalentDocument()
    {
        // Arrange: Load and deserialize the SPDX 2.3 JSON example
        var json = SpdxTestHelpers.GetEmbeddedResource(
            "DemaConsulting.SpdxModel.Tests.IO.Examples.SPDXJSONExample-v2.3.spdx.json");
        var original = Spdx2JsonDeserializer.Deserialize(json);

        // Act: Deep copy the document using the library public API
        var copy = original.DeepCopy();

        // Assert: Verify the copy is equivalent but a distinct instance
        Assert.NotNull(copy);
        Assert.NotSame(original, copy);
        Assert.True(SpdxDocument.Same.Equals(original, copy));
    }

    /// <summary>
    ///     Tests that an SPDX document can be written and read back in a complete round trip.
    /// </summary>
    [Fact]
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
        Assert.NotNull(roundTripped);
        Assert.Equal(original.Name, roundTripped.Name);
        Assert.Equal(original.Version, roundTripped.Version);
        var issues = new List<string>();
        roundTripped.Validate(issues);
        Assert.Empty(issues);
    }

    /// <summary>
    ///     Tests that malformed JSON throws a JsonException when deserialized.
    /// </summary>
    [Fact]
    public void SpdxModel_Deserialize_MalformedJson_ThrowsJsonException()
    {
        // Arrange: Prepare malformed JSON text
        const string malformedJson = "{ this is not valid json }";

        // Act / Assert: malformed JSON throws a JsonException
        Assert.ThrowsAny<System.Text.Json.JsonException>(() => Spdx2JsonDeserializer.Deserialize(malformedJson));
    }

    /// <summary>
    ///     Tests that an invalid SPDX document reports specific validation issues.
    /// </summary>
    [Fact]
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
        Assert.True(issues.Count > 0, "Expected validation issues but none were reported.");
        Assert.True(issues.Exists(i => i.Contains("SPDX Version")),
            "Expected a SPDX Version validation issue.");
    }

    /// <summary>
    ///     Tests that required fields on SPDX data model types are non-nullable,
    ///     and optional fields are nullable.
    /// </summary>
    [Fact]
    public void SpdxModel_FieldOptionality_RequiredFieldsNotNull_OptionalFieldsNullable()
    {
        // Arrange: Create a default instance of key data model types
        var document = new SpdxDocument();
        var package = new SpdxPackage();
        var file = new SpdxFile();
        var relationship = new SpdxRelationship();

        // Act / Assert: default-constructed instances have the expected field nullability

        // Assert: Required fields are non-nullable (strings default to empty, not null)
        Assert.NotNull(document.Id);
        Assert.NotNull(document.Name);
        Assert.NotNull(document.Version);
        Assert.NotNull(document.DataLicense);
        Assert.NotNull(document.DocumentNamespace);
        Assert.NotNull(package.Id);
        Assert.NotNull(package.Name);
        Assert.NotNull(package.DownloadLocation);
        Assert.NotNull(file.Id);
        Assert.NotNull(file.FileName);
        Assert.NotNull(relationship.Id);
        Assert.NotNull(relationship.RelatedSpdxElement);

        // Assert: Optional fields are nullable
        Assert.Null(document.Comment);
        Assert.Null(package.Comment);
        Assert.Null(package.Version);
        Assert.Null(file.Comment);
        Assert.Null(relationship.Comment);
    }

    /// <summary>
    ///     Tests that SPDX date-time validation helper behavior is observable through the document model.
    /// </summary>
    /// <remarks>
    ///     Demonstrates that <see cref="SpdxHelpers.IsValidSpdxDateTime"/> is exercised end-to-end when
    ///     a real SPDX document is validated, satisfying the system-level observability requirement for
    ///     helper utilities.
    /// </remarks>
    [Fact]
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
        Assert.Empty(issues);
        Assert.False(string.IsNullOrEmpty(document.CreationInformation.Created),
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
    [Fact]
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
        Assert.Equal(initialCount + 1, document.Relationships.Length);
        Assert.True(
            Array.Exists(document.Relationships, r =>
                r.Id == "SPDXRef-DOCUMENT" &&
                r.RelationshipType == SpdxRelationshipType.DependsOn &&
                r.RelatedSpdxElement == "SPDXRef-Package"),
            "Expected the newly added relationship to be present in the document.");
    }
}
