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

namespace DemaConsulting.SpdxModel.Tests;

/// <summary>
///     Tests for the <see cref="SpdxRelationship" /> class.
/// </summary>
/// <remarks>
///     Covers equality comparison via <see cref="SpdxRelationship.Same" /> and
///     <see cref="SpdxRelationship.SameElements" /> comparers, deep-copy independence, field merging via
///     <see cref="SpdxRelationship.Enhance(SpdxRelationship[],SpdxRelationship[])" />, validation via
///     <see cref="SpdxRelationship.Validate" />, and round-trip text conversion via
///     <see cref="SpdxRelationshipTypeExtensions" />. Each test exercises a single scenario or
///     boundary condition in isolation with no shared state between tests.
/// </remarks>
public class SpdxRelationshipTests
{
    /// <summary>
    ///     Tests that the <see cref="SpdxRelationship.Same" /> comparer identifies matching relationships as equal.
    /// </summary>
    /// <remarks>
    ///     Verifies that two relationships with the same <c>Id</c>, <c>RelationshipType</c>, and
    ///     <c>RelatedSpdxElement</c> are considered equal even when <c>Comment</c> differs.
    /// </remarks>
    [Fact]
    public void SpdxRelationship_SameComparer_MatchingRelationships_ReturnsTrue()
    {
        // Arrange: Create two relationships that differ only in Comment
        var r1 = new SpdxRelationship
        {
            Id = "SPDXRef-Package1",
            RelationshipType = SpdxRelationshipType.Contains,
            RelatedSpdxElement = "SPDXRef-Package2"
        };
        var r2 = new SpdxRelationship
        {
            Id = "SPDXRef-Package1",
            RelationshipType = SpdxRelationshipType.Contains,
            RelatedSpdxElement = "SPDXRef-Package2",
            Comment = "Package 1 contains Package 2"
        };

        // Act: Compare the two relationships
        var result = SpdxRelationship.Same.Equals(r1, r2);

        // Assert: Verify the relationships are considered equal
        Assert.True(result);
        Assert.True(SpdxRelationship.Same.Equals(r2, r1));
    }

    /// <summary>
    ///     Tests that the <see cref="SpdxRelationship.Same" /> comparer identifies different relationships as not equal.
    /// </summary>
    /// <remarks>
    ///     Verifies that two relationships with different <c>Id</c>, <c>RelationshipType</c>, or
    ///     <c>RelatedSpdxElement</c> values are considered distinct.
    /// </remarks>
    [Fact]
    public void SpdxRelationship_SameComparer_DifferentRelationships_ReturnsFalse()
    {
        // Arrange: Create two relationships with different key fields
        var r1 = new SpdxRelationship
        {
            Id = "SPDXRef-Package1",
            RelationshipType = SpdxRelationshipType.Contains,
            RelatedSpdxElement = "SPDXRef-Package2"
        };
        var r3 = new SpdxRelationship
        {
            Id = "SPDXRef-Package3",
            RelationshipType = SpdxRelationshipType.DevToolOf,
            RelatedSpdxElement = "SPDXRef-Package4"
        };

        // Act: Compare the two relationships
        var result = SpdxRelationship.Same.Equals(r1, r3);

        // Assert: Verify the relationships are considered distinct
        Assert.False(result);
        Assert.False(SpdxRelationship.Same.Equals(r3, r1));
    }

    /// <summary>
    ///     Tests that the <see cref="SpdxRelationship.Same" /> comparer produces the same hash code for equal relationships.
    /// </summary>
    /// <remarks>
    ///     Verifies that two relationships considered equal by <see cref="SpdxRelationship.Same" /> produce
    ///     identical hash codes, satisfying the hash/equality contract.
    /// </remarks>
    [Fact]
    public void SpdxRelationship_SameComparer_MatchingRelationships_ReturnsSameHashCode()
    {
        // Arrange: Create two relationships that differ only in Comment
        var r1 = new SpdxRelationship
        {
            Id = "SPDXRef-Package1",
            RelationshipType = SpdxRelationshipType.Contains,
            RelatedSpdxElement = "SPDXRef-Package2"
        };
        var r2 = new SpdxRelationship
        {
            Id = "SPDXRef-Package1",
            RelationshipType = SpdxRelationshipType.Contains,
            RelatedSpdxElement = "SPDXRef-Package2",
            Comment = "Package 1 contains Package 2"
        };

        // Act: Compute hash codes for both relationships
        var hash1 = SpdxRelationship.Same.GetHashCode(r1);
        var hash2 = SpdxRelationship.Same.GetHashCode(r2);

        // Assert: Verify the hash codes are identical
        Assert.Equal(hash1, hash2);
    }

    /// <summary>
    ///     Tests that the <see cref="SpdxRelationship.SameElements" /> comparer identifies matching elements as equal.
    /// </summary>
    /// <remarks>
    ///     Verifies that two relationships with the same <c>Id</c> and <c>RelatedSpdxElement</c> are considered equal
    ///     even when <c>RelationshipType</c> differs.
    /// </remarks>
    [Fact]
    public void SpdxRelationship_SameElementsComparer_MatchingElements_ReturnsTrue()
    {
        // Arrange: Create two relationships that differ only in RelationshipType
        var r1 = new SpdxRelationship
        {
            Id = "SPDXRef-Package1",
            RelationshipType = SpdxRelationshipType.Contains,
            RelatedSpdxElement = "SPDXRef-Package2"
        };
        var r2 = new SpdxRelationship
        {
            Id = "SPDXRef-Package1",
            RelationshipType = SpdxRelationshipType.BuildToolOf,
            RelatedSpdxElement = "SPDXRef-Package2",
            Comment = "Package 1 builds Package 2"
        };

        // Act: Compare the two relationships
        var result = SpdxRelationship.SameElements.Equals(r1, r2);

        // Assert: Verify the relationships are considered equal
        Assert.True(result);
        Assert.True(SpdxRelationship.SameElements.Equals(r2, r1));
    }

    /// <summary>
    ///     Tests that the <see cref="SpdxRelationship.SameElements" /> comparer identifies different elements as not equal.
    /// </summary>
    /// <remarks>
    ///     Verifies that two relationships with different <c>Id</c> or <c>RelatedSpdxElement</c> are considered distinct,
    ///     regardless of their <c>RelationshipType</c>.
    /// </remarks>
    [Fact]
    public void SpdxRelationship_SameElementsComparer_DifferentElements_ReturnsFalse()
    {
        // Arrange: Create two relationships with different element IDs
        var r1 = new SpdxRelationship
        {
            Id = "SPDXRef-Package1",
            RelationshipType = SpdxRelationshipType.Contains,
            RelatedSpdxElement = "SPDXRef-Package2"
        };
        var r3 = new SpdxRelationship
        {
            Id = "SPDXRef-Package3",
            RelationshipType = SpdxRelationshipType.DevToolOf,
            RelatedSpdxElement = "SPDXRef-Package4"
        };

        // Act: Compare the two relationships
        var result = SpdxRelationship.SameElements.Equals(r1, r3);

        // Assert: Verify the relationships are considered distinct
        Assert.False(result);
        Assert.False(SpdxRelationship.SameElements.Equals(r3, r1));
    }

    /// <summary>
    ///     Tests that the <see cref="SpdxRelationship.SameElements" /> comparer produces the same hash code for equal
    ///     relationships.
    /// </summary>
    /// <remarks>
    ///     Verifies that two relationships considered equal by <see cref="SpdxRelationship.SameElements" /> produce
    ///     identical hash codes, satisfying the hash/equality contract.
    /// </remarks>
    [Fact]
    public void SpdxRelationship_SameElementsComparer_MatchingElements_ReturnsSameHashCode()
    {
        // Arrange: Create two relationships that differ only in RelationshipType
        var r1 = new SpdxRelationship
        {
            Id = "SPDXRef-Package1",
            RelationshipType = SpdxRelationshipType.Contains,
            RelatedSpdxElement = "SPDXRef-Package2"
        };
        var r2 = new SpdxRelationship
        {
            Id = "SPDXRef-Package1",
            RelationshipType = SpdxRelationshipType.BuildToolOf,
            RelatedSpdxElement = "SPDXRef-Package2"
        };

        // Act: Compute hash codes for both relationships
        var hash1 = SpdxRelationship.SameElements.GetHashCode(r1);
        var hash2 = SpdxRelationship.SameElements.GetHashCode(r2);

        // Assert: Verify the hash codes are identical
        Assert.Equal(hash1, hash2);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxRelationship.DeepCopy" /> method successfully creates a deep copy.
    /// </summary>
    /// <remarks>
    ///     Verifies that the returned instance has equal field values for all scalar properties and
    ///     is a distinct object reference from the original.
    /// </remarks>
    [Fact]
    public void SpdxRelationship_DeepCopy_FullyPopulatedRelationship_CreatesEqualButDistinctCopy()
    {
        // Arrange: Create a relationship with properties
        var r1 = new SpdxRelationship
        {
            Id = "SPDXRef-Package1",
            RelationshipType = SpdxRelationshipType.Contains,
            RelatedSpdxElement = "SPDXRef-Package2",
            Comment = "Package 1 contains Package 2"
        };

        // Act: Create a deep copy of the relationship
        var r2 = r1.DeepCopy();

        // Assert: Verifies deep-copy is equal to original
        Assert.Equal(r1, r2, SpdxRelationship.Same);
        Assert.Equal(r1.Id, r2.Id);
        Assert.Equal(r1.RelationshipType, r2.RelationshipType);
        Assert.Equal(r1.RelatedSpdxElement, r2.RelatedSpdxElement);
        Assert.Equal(r1.Comment, r2.Comment);

        // Assert: Verifies deep-copy has distinct instance
        Assert.False(ReferenceEquals(r1, r2));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxRelationship.Enhance(SpdxRelationship[], SpdxRelationship[])" /> method adds or updates
    ///     information correctly.
    /// </summary>
    /// <remarks>
    ///     Verifies that a matching relationship (same id, type, and related element) is enhanced in place
    ///     and that a non-matching relationship from the source array is deep-copied and appended, resulting in
    ///     an array of length two.
    /// </remarks>
    [Fact]
    public void SpdxRelationship_Enhance_MatchingAndNewRelationships_MergesCorrectly()
    {
        // Arrange: Create an array of relationships
        var relationships = new[]
        {
            new SpdxRelationship
            {
                Id = "SPDXRef-Package1",
                RelationshipType = SpdxRelationshipType.Contains,
                RelatedSpdxElement = "SPDXRef-Package2"
            }
        };

        // Act: Enhance the relationships with additional relationships
        relationships = SpdxRelationship.Enhance(
            relationships,
            [
                new SpdxRelationship
                {
                    Id = "SPDXRef-Package1",
                    RelationshipType = SpdxRelationshipType.Contains,
                    RelatedSpdxElement = "SPDXRef-Package2",
                    Comment = "Package 1 contains Package 2"
                },
                new SpdxRelationship
                {
                    Id = "SPDXRef-Package3",
                    RelationshipType = SpdxRelationshipType.DevToolOf,
                    RelatedSpdxElement = "SPDXRef-Package4"
                }
            ]);

        // Assert: Verify the relationships array has correct information
        Assert.Equal(2, relationships.Length);
        Assert.Equal("SPDXRef-Package1", relationships[0].Id);
        Assert.Equal(SpdxRelationshipType.Contains, relationships[0].RelationshipType);
        Assert.Equal("SPDXRef-Package2", relationships[0].RelatedSpdxElement);
        Assert.Equal("Package 1 contains Package 2", relationships[0].Comment);
        Assert.Equal("SPDXRef-Package3", relationships[1].Id);
        Assert.Equal(SpdxRelationshipType.DevToolOf, relationships[1].RelationshipType);
        Assert.Equal("SPDXRef-Package4", relationships[1].RelatedSpdxElement);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxRelationship.Validate" /> method reports missing ids
    /// </summary>
    /// <remarks>
    ///     Verifies that an empty <c>Id</c> causes the "Relationship Invalid SPDX Element ID Field - Empty"
    ///     issue to be reported.
    /// </remarks>
    [Fact]
    public void SpdxRelationship_Validate_MissingRelationshipId_ReportsIssue()
    {
        // Arrange: Create a bad relationship
        var relationship = new SpdxRelationship()
        {
            Id = "",
            RelationshipType = SpdxRelationshipType.Contains,
            RelatedSpdxElement = "SPDXRef-Package2"
        };

        // Act: Perform validation on the SpdxRelationship instance.
        var issues = new List<string>();
        relationship.Validate(issues, null);

        // Assert: Verify that the validation fails and the error message includes the description
        Assert.Contains(issues, issue => issue.Contains("Relationship Invalid SPDX Element ID Field - Empty"));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxRelationship.Validate" /> method reports missing or empty related element IDs.
    /// </summary>
    /// <remarks>
    ///     Verifies that an empty <c>RelatedSpdxElement</c> causes the "Relationship Invalid Related SPDX Element
    ///     Field - Empty" issue to be reported.
    /// </remarks>
    [Fact]
    public void SpdxRelationship_Validate_MissingRelatedElementId_ReportsIssue()
    {
        // Arrange: Create a bad relationship
        var relationship = new SpdxRelationship()
        {
            Id = "SPDXRef-Package1",
            RelationshipType = SpdxRelationshipType.Contains,
            RelatedSpdxElement = ""
        };

        // Act: Perform validation on the SpdxRelationship instance.
        var issues = new List<string>();
        relationship.Validate(issues, null);

        // Assert: Verify that the validation fails and the error message includes the description
        Assert.Contains(issues, issue => issue.Contains("Relationship Invalid Related SPDX Element Field - Empty"));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxRelationship.Validate" /> method reports missing relationships
    /// </summary>
    /// <remarks>
    ///     Verifies that a <c>RelationshipType</c> of <see cref="SpdxRelationshipType.Missing" /> causes the
    ///     "Relationship Invalid Relationship Type Field - Missing" issue to be reported.
    /// </remarks>
    [Fact]
    public void SpdxRelationship_Validate_MissingRelationshipType_ReportsIssue()
    {
        // Arrange: Create a bad relationship
        var relationship = new SpdxRelationship()
        {
            Id = "SPDXRef-Package1",
            RelationshipType = SpdxRelationshipType.Missing,
            RelatedSpdxElement = "SPDXRef-Package2"
        };

        // Act: Perform validation on the SpdxRelationship instance.
        var issues = new List<string>();
        relationship.Validate(issues, null);

        // Assert: Verify that the validation fails and the error message includes the description
        Assert.Contains(issues, issue => issue.Contains("Relationship Invalid Relationship Type Field - Missing"));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxRelationshipTypeExtensions.FromText(string)" /> method for the "CONTAINS" relationship
    ///     type.
    /// </summary>
    /// <remarks>
    ///     Verifies that all 45 recognized SPDX relationship type tokens (including case-insensitive variants) are
    ///     correctly parsed to their corresponding <see cref="SpdxRelationshipType" /> enum values, and that an empty
    ///     string maps to <see cref="SpdxRelationshipType.Missing" />.
    /// </remarks>
    [Fact]
    public void SpdxRelationshipTypeExtensions_FromText_KnownText_ReturnsMappedEnum()
    {
        // Arrange: (none required)

        // Act / Assert: Verify all recognized relationship type strings parse to their enum values
        Assert.Equal(SpdxRelationshipType.Missing, SpdxRelationshipTypeExtensions.FromText(""));
        Assert.Equal(SpdxRelationshipType.Describes, SpdxRelationshipTypeExtensions.FromText("DESCRIBES"));
        Assert.Equal(SpdxRelationshipType.Describes, SpdxRelationshipTypeExtensions.FromText("describes"));
        Assert.Equal(SpdxRelationshipType.Describes, SpdxRelationshipTypeExtensions.FromText("Describes"));
        Assert.Equal(SpdxRelationshipType.DescribedBy, SpdxRelationshipTypeExtensions.FromText("DESCRIBED_BY"));
        Assert.Equal(SpdxRelationshipType.Contains, SpdxRelationshipTypeExtensions.FromText("CONTAINS"));
        Assert.Equal(SpdxRelationshipType.ContainedBy, SpdxRelationshipTypeExtensions.FromText("CONTAINED_BY"));
        Assert.Equal(SpdxRelationshipType.DependsOn, SpdxRelationshipTypeExtensions.FromText("DEPENDS_ON"));
        Assert.Equal(SpdxRelationshipType.DependencyOf, SpdxRelationshipTypeExtensions.FromText("DEPENDENCY_OF"));
        Assert.Equal(SpdxRelationshipType.DependencyManifestOf,
            SpdxRelationshipTypeExtensions.FromText("DEPENDENCY_MANIFEST_OF"));
        Assert.Equal(SpdxRelationshipType.BuildDependencyOf,
            SpdxRelationshipTypeExtensions.FromText("BUILD_DEPENDENCY_OF"));
        Assert.Equal(SpdxRelationshipType.DevDependencyOf,
            SpdxRelationshipTypeExtensions.FromText("DEV_DEPENDENCY_OF"));
        Assert.Equal(SpdxRelationshipType.OptionalDependencyOf,
            SpdxRelationshipTypeExtensions.FromText("OPTIONAL_DEPENDENCY_OF"));
        Assert.Equal(SpdxRelationshipType.ProvidedDependencyOf,
            SpdxRelationshipTypeExtensions.FromText("PROVIDED_DEPENDENCY_OF"));
        Assert.Equal(SpdxRelationshipType.TestDependencyOf,
            SpdxRelationshipTypeExtensions.FromText("TEST_DEPENDENCY_OF"));
        Assert.Equal(SpdxRelationshipType.RuntimeDependencyOf,
            SpdxRelationshipTypeExtensions.FromText("RUNTIME_DEPENDENCY_OF"));
        Assert.Equal(SpdxRelationshipType.ExampleOf, SpdxRelationshipTypeExtensions.FromText("EXAMPLE_OF"));
        Assert.Equal(SpdxRelationshipType.Generates, SpdxRelationshipTypeExtensions.FromText("GENERATES"));
        Assert.Equal(SpdxRelationshipType.GeneratedFrom, SpdxRelationshipTypeExtensions.FromText("GENERATED_FROM"));
        Assert.Equal(SpdxRelationshipType.AncestorOf, SpdxRelationshipTypeExtensions.FromText("ANCESTOR_OF"));
        Assert.Equal(SpdxRelationshipType.DescendantOf, SpdxRelationshipTypeExtensions.FromText("DESCENDANT_OF"));
        Assert.Equal(SpdxRelationshipType.VariantOf, SpdxRelationshipTypeExtensions.FromText("VARIANT_OF"));
        Assert.Equal(SpdxRelationshipType.DistributionArtifact,
            SpdxRelationshipTypeExtensions.FromText("DISTRIBUTION_ARTIFACT"));
        Assert.Equal(SpdxRelationshipType.PatchFor, SpdxRelationshipTypeExtensions.FromText("PATCH_FOR"));
        Assert.Equal(SpdxRelationshipType.PatchApplied, SpdxRelationshipTypeExtensions.FromText("PATCH_APPLIED"));
        Assert.Equal(SpdxRelationshipType.CopyOf, SpdxRelationshipTypeExtensions.FromText("COPY_OF"));
        Assert.Equal(SpdxRelationshipType.FileAdded, SpdxRelationshipTypeExtensions.FromText("FILE_ADDED"));
        Assert.Equal(SpdxRelationshipType.FileDeleted, SpdxRelationshipTypeExtensions.FromText("FILE_DELETED"));
        Assert.Equal(SpdxRelationshipType.FileModified, SpdxRelationshipTypeExtensions.FromText("FILE_MODIFIED"));
        Assert.Equal(SpdxRelationshipType.ExpandedFromArchive,
            SpdxRelationshipTypeExtensions.FromText("EXPANDED_FROM_ARCHIVE"));
        Assert.Equal(SpdxRelationshipType.DynamicLink, SpdxRelationshipTypeExtensions.FromText("DYNAMIC_LINK"));
        Assert.Equal(SpdxRelationshipType.StaticLink, SpdxRelationshipTypeExtensions.FromText("STATIC_LINK"));
        Assert.Equal(SpdxRelationshipType.DataFileOf, SpdxRelationshipTypeExtensions.FromText("DATA_FILE_OF"));
        Assert.Equal(SpdxRelationshipType.TestCaseOf, SpdxRelationshipTypeExtensions.FromText("TEST_CASE_OF"));
        Assert.Equal(SpdxRelationshipType.BuildToolOf, SpdxRelationshipTypeExtensions.FromText("BUILD_TOOL_OF"));
        Assert.Equal(SpdxRelationshipType.DevToolOf, SpdxRelationshipTypeExtensions.FromText("DEV_TOOL_OF"));
        Assert.Equal(SpdxRelationshipType.TestOf, SpdxRelationshipTypeExtensions.FromText("TEST_OF"));
        Assert.Equal(SpdxRelationshipType.TestToolOf, SpdxRelationshipTypeExtensions.FromText("TEST_TOOL_OF"));
        Assert.Equal(SpdxRelationshipType.DocumentationOf,
            SpdxRelationshipTypeExtensions.FromText("DOCUMENTATION_OF"));
        Assert.Equal(SpdxRelationshipType.OptionalComponentOf,
            SpdxRelationshipTypeExtensions.FromText("OPTIONAL_COMPONENT_OF"));
        Assert.Equal(SpdxRelationshipType.MetafileOf, SpdxRelationshipTypeExtensions.FromText("METAFILE_OF"));
        Assert.Equal(SpdxRelationshipType.PackageOf, SpdxRelationshipTypeExtensions.FromText("PACKAGE_OF"));
        Assert.Equal(SpdxRelationshipType.Amends, SpdxRelationshipTypeExtensions.FromText("AMENDS"));
        Assert.Equal(SpdxRelationshipType.PrerequisiteFor,
            SpdxRelationshipTypeExtensions.FromText("PREREQUISITE_FOR"));
        Assert.Equal(SpdxRelationshipType.HasPrerequisite,
            SpdxRelationshipTypeExtensions.FromText("HAS_PREREQUISITE"));
        Assert.Equal(SpdxRelationshipType.RequirementDescriptionFor,
            SpdxRelationshipTypeExtensions.FromText("REQUIREMENT_DESCRIPTION_FOR"));
        Assert.Equal(SpdxRelationshipType.SpecificationFor,
            SpdxRelationshipTypeExtensions.FromText("SPECIFICATION_FOR"));
        Assert.Equal(SpdxRelationshipType.Other, SpdxRelationshipTypeExtensions.FromText("OTHER"));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxRelationshipTypeExtensions.FromText(string)" /> method for an invalid relationship type.
    /// </summary>
    /// <remarks>
    ///     Verifies that an unrecognized relationship type string throws an
    ///     <see cref="InvalidOperationException" /> with a descriptive error message.
    /// </remarks>
    [Fact]
    public void SpdxRelationshipTypeExtensions_FromText_UnknownText_ThrowsInvalidOperationException()
    {
        // Arrange: (none required)

        // Act / Assert: Verify that an unknown type throws InvalidOperationException
        var exception =
            Assert.Throws<InvalidOperationException>(() => SpdxRelationshipTypeExtensions.FromText("invalid"));
        Assert.Equal("Unsupported SPDX Relationship Type 'invalid'", exception.Message);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxRelationshipTypeExtensions.ToText(SpdxRelationshipType)" /> method for the "CONTAINS"
    ///     relationship type.
    /// </summary>
    /// <remarks>
    ///     Verifies that all 45 recognized <see cref="SpdxRelationshipType" /> enum values are correctly serialized to
    ///     their canonical SPDX text representations (uppercase, underscore-separated tokens).
    /// </remarks>
    [Fact]
    public void SpdxRelationshipTypeExtensions_ToText_KnownEnum_ReturnsMappedText()
    {
        // Arrange: (none required)

        // Act / Assert: Verify all relationship type enum values serialize to their SPDX text representations
        Assert.Equal("DESCRIBES", SpdxRelationshipType.Describes.ToText());
        Assert.Equal("DESCRIBED_BY", SpdxRelationshipType.DescribedBy.ToText());
        Assert.Equal("CONTAINS", SpdxRelationshipType.Contains.ToText());
        Assert.Equal("CONTAINED_BY", SpdxRelationshipType.ContainedBy.ToText());
        Assert.Equal("DEPENDS_ON", SpdxRelationshipType.DependsOn.ToText());
        Assert.Equal("DEPENDENCY_OF", SpdxRelationshipType.DependencyOf.ToText());
        Assert.Equal("DEPENDENCY_MANIFEST_OF", SpdxRelationshipType.DependencyManifestOf.ToText());
        Assert.Equal("BUILD_DEPENDENCY_OF", SpdxRelationshipType.BuildDependencyOf.ToText());
        Assert.Equal("DEV_DEPENDENCY_OF", SpdxRelationshipType.DevDependencyOf.ToText());
        Assert.Equal("OPTIONAL_DEPENDENCY_OF", SpdxRelationshipType.OptionalDependencyOf.ToText());
        Assert.Equal("PROVIDED_DEPENDENCY_OF", SpdxRelationshipType.ProvidedDependencyOf.ToText());
        Assert.Equal("TEST_DEPENDENCY_OF", SpdxRelationshipType.TestDependencyOf.ToText());
        Assert.Equal("RUNTIME_DEPENDENCY_OF", SpdxRelationshipType.RuntimeDependencyOf.ToText());
        Assert.Equal("EXAMPLE_OF", SpdxRelationshipType.ExampleOf.ToText());
        Assert.Equal("GENERATES", SpdxRelationshipType.Generates.ToText());
        Assert.Equal("GENERATED_FROM", SpdxRelationshipType.GeneratedFrom.ToText());
        Assert.Equal("ANCESTOR_OF", SpdxRelationshipType.AncestorOf.ToText());
        Assert.Equal("DESCENDANT_OF", SpdxRelationshipType.DescendantOf.ToText());
        Assert.Equal("VARIANT_OF", SpdxRelationshipType.VariantOf.ToText());
        Assert.Equal("DISTRIBUTION_ARTIFACT", SpdxRelationshipType.DistributionArtifact.ToText());
        Assert.Equal("PATCH_FOR", SpdxRelationshipType.PatchFor.ToText());
        Assert.Equal("PATCH_APPLIED", SpdxRelationshipType.PatchApplied.ToText());
        Assert.Equal("COPY_OF", SpdxRelationshipType.CopyOf.ToText());
        Assert.Equal("FILE_ADDED", SpdxRelationshipType.FileAdded.ToText());
        Assert.Equal("FILE_DELETED", SpdxRelationshipType.FileDeleted.ToText());
        Assert.Equal("FILE_MODIFIED", SpdxRelationshipType.FileModified.ToText());
        Assert.Equal("EXPANDED_FROM_ARCHIVE", SpdxRelationshipType.ExpandedFromArchive.ToText());
        Assert.Equal("DYNAMIC_LINK", SpdxRelationshipType.DynamicLink.ToText());
        Assert.Equal("STATIC_LINK", SpdxRelationshipType.StaticLink.ToText());
        Assert.Equal("DATA_FILE_OF", SpdxRelationshipType.DataFileOf.ToText());
        Assert.Equal("TEST_CASE_OF", SpdxRelationshipType.TestCaseOf.ToText());
        Assert.Equal("BUILD_TOOL_OF", SpdxRelationshipType.BuildToolOf.ToText());
        Assert.Equal("DEV_TOOL_OF", SpdxRelationshipType.DevToolOf.ToText());
        Assert.Equal("TEST_OF", SpdxRelationshipType.TestOf.ToText());
        Assert.Equal("TEST_TOOL_OF", SpdxRelationshipType.TestToolOf.ToText());
        Assert.Equal("DOCUMENTATION_OF", SpdxRelationshipType.DocumentationOf.ToText());
        Assert.Equal("OPTIONAL_COMPONENT_OF", SpdxRelationshipType.OptionalComponentOf.ToText());
        Assert.Equal("METAFILE_OF", SpdxRelationshipType.MetafileOf.ToText());
        Assert.Equal("PACKAGE_OF", SpdxRelationshipType.PackageOf.ToText());
        Assert.Equal("AMENDS", SpdxRelationshipType.Amends.ToText());
        Assert.Equal("PREREQUISITE_FOR", SpdxRelationshipType.PrerequisiteFor.ToText());
        Assert.Equal("HAS_PREREQUISITE", SpdxRelationshipType.HasPrerequisite.ToText());
        Assert.Equal("REQUIREMENT_DESCRIPTION_FOR", SpdxRelationshipType.RequirementDescriptionFor.ToText());
        Assert.Equal("SPECIFICATION_FOR", SpdxRelationshipType.SpecificationFor.ToText());
        Assert.Equal("OTHER", SpdxRelationshipType.Other.ToText());
    }

    /// <summary>
    ///     Tests the <see cref="SpdxRelationshipTypeExtensions.ToText(SpdxRelationshipType)" /> method for the
    ///     <see cref="SpdxRelationshipType.Missing" /> sentinel value.
    /// </summary>
    /// <remarks>
    ///     Verifies that attempting to serialize the <see cref="SpdxRelationshipType.Missing" /> sentinel throws an
    ///     <see cref="InvalidOperationException" /> with a descriptive error message, since the sentinel is not a valid
    ///     SPDX relationship type token.
    /// </remarks>
    [Fact]
    public void SpdxRelationshipTypeExtensions_ToText_MissingSentinel_ThrowsInvalidOperationException()
    {
        // Arrange: (none required)

        // Act: Attempt to convert the Missing sentinel to text
        var exception = Assert.Throws<InvalidOperationException>(() => SpdxRelationshipType.Missing.ToText());

        // Assert: Verify the exception has the expected message
        Assert.Equal("Attempt to serialize missing SPDX Relationship Type", exception.Message);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxRelationshipTypeExtensions.ToText(SpdxRelationshipType)" /> method for an invalid
    ///     relationship type.
    /// </summary>
    /// <remarks>
    ///     Verifies that an out-of-range <see cref="SpdxRelationshipType" /> value (including the
    ///     <see cref="SpdxRelationshipType.Missing" /> sentinel) throws an <see cref="InvalidOperationException" />
    ///     with a descriptive error message.
    /// </remarks>
    [Fact]
    public void SpdxRelationshipTypeExtensions_ToText_UnknownEnum_ThrowsInvalidOperationException()
    {
        // Arrange: (none required)

        // Act / Assert: Verify that an unknown type throws InvalidOperationException
        var exception = Assert.Throws<InvalidOperationException>(() => ((SpdxRelationshipType)1000).ToText());
        Assert.Equal("Unsupported SPDX Relationship Type '1000'", exception.Message);
    }
}
