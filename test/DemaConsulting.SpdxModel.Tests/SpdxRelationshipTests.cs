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
/// Tests for the <see cref="SpdxRelationship"/> class.
/// </summary>
[TestClass]
public class SpdxRelationshipTests
{
    /// <summary>
    /// Tests the <see cref="SpdxRelationship.Same"/> comparer.
    /// </summary>
    [TestMethod]
    public void RelationshipSameComparer()
    {
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

        var r3 = new SpdxRelationship
        {
            Id = "SPDXRef-Package3",
            RelationshipType = SpdxRelationshipType.DevToolOf,
            RelatedSpdxElement = "SPDXRef-Package4"
        };

        // Assert relationships compare to themselves
        Assert.IsTrue(SpdxRelationship.Same.Equals(r1, r1));
        Assert.IsTrue(SpdxRelationship.Same.Equals(r2, r2));
        Assert.IsTrue(SpdxRelationship.Same.Equals(r3, r3));

        // Assert relationships compare correctly
        Assert.IsTrue(SpdxRelationship.Same.Equals(r1, r2));
        Assert.IsTrue(SpdxRelationship.Same.Equals(r2, r1));
        Assert.IsFalse(SpdxRelationship.Same.Equals(r1, r3));
        Assert.IsFalse(SpdxRelationship.Same.Equals(r3, r1));
        Assert.IsFalse(SpdxRelationship.Same.Equals(r2, r3));
        Assert.IsFalse(SpdxRelationship.Same.Equals(r3, r2));

        // Assert same relationships have identical hashes
        Assert.AreEqual(SpdxRelationship.Same.GetHashCode(r1), SpdxRelationship.Same.GetHashCode(r2));
    }

    /// <summary>
    /// Tests the <see cref="SpdxRelationship.SameElements"/> comparer.
    /// </summary>
    [TestMethod]
    public void RelationshipSameElementsComparer()
    {
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

        var r3 = new SpdxRelationship
        {
            Id = "SPDXRef-Package3",
            RelationshipType = SpdxRelationshipType.DevToolOf,
            RelatedSpdxElement = "SPDXRef-Package4"
        };

        // Assert relationships compare to themselves
        Assert.IsTrue(SpdxRelationship.SameElements.Equals(r1, r1));
        Assert.IsTrue(SpdxRelationship.SameElements.Equals(r2, r2));
        Assert.IsTrue(SpdxRelationship.SameElements.Equals(r3, r3));

        // Assert relationships compare correctly
        Assert.IsTrue(SpdxRelationship.SameElements.Equals(r1, r2));
        Assert.IsTrue(SpdxRelationship.SameElements.Equals(r2, r1));
        Assert.IsFalse(SpdxRelationship.SameElements.Equals(r1, r3));
        Assert.IsFalse(SpdxRelationship.SameElements.Equals(r3, r1));
        Assert.IsFalse(SpdxRelationship.SameElements.Equals(r2, r3));
        Assert.IsFalse(SpdxRelationship.SameElements.Equals(r3, r2));

        // Assert same relationships have identical hashes
        Assert.AreEqual(SpdxRelationship.SameElements.GetHashCode(r1), SpdxRelationship.SameElements.GetHashCode(r2));
    }

    /// <summary>
    /// Tests the <see cref="SpdxRelationship.DeepCopy"/> method.
    /// </summary>
    [TestMethod]
    public void DeepCopy()
    {
        var r1 = new SpdxRelationship
        {
            Id = "SPDXRef-Package1",
            RelationshipType = SpdxRelationshipType.Contains,
            RelatedSpdxElement = "SPDXRef-Package2",
            Comment = "Package 1 contains Package 2"
        };

        // Make deep copy
        var r2 = r1.DeepCopy();

        // Assert both objects are equal
        Assert.AreEqual(r1, r2, SpdxRelationship.Same);
        Assert.AreEqual(r1.Id, r2.Id);
        Assert.AreEqual(r1.RelationshipType, r2.RelationshipType);
        Assert.AreEqual(r1.RelatedSpdxElement, r2.RelatedSpdxElement);
        Assert.AreEqual(r1.Comment, r2.Comment);

        // Assert separate instances
        Assert.IsFalse(ReferenceEquals(r1, r2));
    }

    /// <summary>
    /// Tests the <see cref="SpdxRelationship.Enhance(SpdxRelationship[], SpdxRelationship[])"/> method.
    /// </summary>
    [TestMethod]
    public void Enhance()
    {
        var relationships = new[]
        {
            new SpdxRelationship
            {
                Id = "SPDXRef-Package1",
                RelationshipType = SpdxRelationshipType.Contains,
                RelatedSpdxElement = "SPDXRef-Package2"
            }
        };

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

        Assert.AreEqual(2, relationships.Length);
        Assert.AreEqual("SPDXRef-Package1", relationships[0].Id);
        Assert.AreEqual(SpdxRelationshipType.Contains, relationships[0].RelationshipType);
        Assert.AreEqual("SPDXRef-Package2", relationships[0].RelatedSpdxElement);
        Assert.AreEqual("Package 1 contains Package 2", relationships[0].Comment);
        Assert.AreEqual("SPDXRef-Package3", relationships[1].Id);
        Assert.AreEqual(SpdxRelationshipType.DevToolOf, relationships[1].RelationshipType);
        Assert.AreEqual("SPDXRef-Package4", relationships[1].RelatedSpdxElement);
    }

    /// <summary>
    /// Tests the <see cref="SpdxRelationshipTypeExtensions.FromText(string)"/> method for the "CONTAINS" relationship type.
    /// </summary>
    [TestMethod]
    public void SpdxRelationshipTypeExtensions_FromText_Valid()
    {
        Assert.AreEqual(SpdxRelationshipType.Missing, SpdxRelationshipTypeExtensions.FromText(""));
        Assert.AreEqual(SpdxRelationshipType.Describes, SpdxRelationshipTypeExtensions.FromText("DESCRIBES"));
        Assert.AreEqual(SpdxRelationshipType.Describes, SpdxRelationshipTypeExtensions.FromText("describes"));
        Assert.AreEqual(SpdxRelationshipType.Describes, SpdxRelationshipTypeExtensions.FromText("Describes"));
        Assert.AreEqual(SpdxRelationshipType.DescribedBy, SpdxRelationshipTypeExtensions.FromText("DESCRIBED_BY"));
        Assert.AreEqual(SpdxRelationshipType.Contains, SpdxRelationshipTypeExtensions.FromText("CONTAINS"));
        Assert.AreEqual(SpdxRelationshipType.ContainedBy, SpdxRelationshipTypeExtensions.FromText("CONTAINED_BY"));
        Assert.AreEqual(SpdxRelationshipType.DependsOn, SpdxRelationshipTypeExtensions.FromText("DEPENDS_ON"));
        Assert.AreEqual(SpdxRelationshipType.DependencyOf, SpdxRelationshipTypeExtensions.FromText("DEPENDENCY_OF"));
        Assert.AreEqual(SpdxRelationshipType.DependencyManifestOf, SpdxRelationshipTypeExtensions.FromText("DEPENDENCY_MANIFEST_OF"));
        Assert.AreEqual(SpdxRelationshipType.BuildDependencyOf, SpdxRelationshipTypeExtensions.FromText("BUILD_DEPENDENCY_OF"));
        Assert.AreEqual(SpdxRelationshipType.DevDependencyOf, SpdxRelationshipTypeExtensions.FromText("DEV_DEPENDENCY_OF"));
        Assert.AreEqual(SpdxRelationshipType.OptionalDependencyOf, SpdxRelationshipTypeExtensions.FromText("OPTIONAL_DEPENDENCY_OF"));
        Assert.AreEqual(SpdxRelationshipType.ProvidedDependencyOf, SpdxRelationshipTypeExtensions.FromText("PROVIDED_DEPENDENCY_OF"));
        Assert.AreEqual(SpdxRelationshipType.TestDependencyOf, SpdxRelationshipTypeExtensions.FromText("TEST_DEPENDENCY_OF"));
        Assert.AreEqual(SpdxRelationshipType.RuntimeDependencyOf, SpdxRelationshipTypeExtensions.FromText("RUNTIME_DEPENDENCY_OF"));
        Assert.AreEqual(SpdxRelationshipType.ExampleOf, SpdxRelationshipTypeExtensions.FromText("EXAMPLE_OF"));
        Assert.AreEqual(SpdxRelationshipType.Generates, SpdxRelationshipTypeExtensions.FromText("GENERATES"));
        Assert.AreEqual(SpdxRelationshipType.GeneratedFrom, SpdxRelationshipTypeExtensions.FromText("GENERATED_FROM"));
        Assert.AreEqual(SpdxRelationshipType.AncestorOf, SpdxRelationshipTypeExtensions.FromText("ANCESTOR_OF"));
        Assert.AreEqual(SpdxRelationshipType.DescendantOf, SpdxRelationshipTypeExtensions.FromText("DESCENDANT_OF"));
        Assert.AreEqual(SpdxRelationshipType.VariantOf, SpdxRelationshipTypeExtensions.FromText("VARIANT_OF"));
        Assert.AreEqual(SpdxRelationshipType.DistributionArtifact, SpdxRelationshipTypeExtensions.FromText("DISTRIBUTION_ARTIFACT"));
        Assert.AreEqual(SpdxRelationshipType.PatchFor, SpdxRelationshipTypeExtensions.FromText("PATCH_FOR"));
        Assert.AreEqual(SpdxRelationshipType.PatchApplied, SpdxRelationshipTypeExtensions.FromText("PATCH_APPLIED"));
        Assert.AreEqual(SpdxRelationshipType.CopyOf, SpdxRelationshipTypeExtensions.FromText("COPY_OF"));
        Assert.AreEqual(SpdxRelationshipType.FileAdded, SpdxRelationshipTypeExtensions.FromText("FILE_ADDED"));
        Assert.AreEqual(SpdxRelationshipType.FileDeleted, SpdxRelationshipTypeExtensions.FromText("FILE_DELETED"));
        Assert.AreEqual(SpdxRelationshipType.FileModified, SpdxRelationshipTypeExtensions.FromText("FILE_MODIFIED"));
        Assert.AreEqual(SpdxRelationshipType.ExpandedFromArchive, SpdxRelationshipTypeExtensions.FromText("EXPANDED_FROM_ARCHIVE"));
        Assert.AreEqual(SpdxRelationshipType.DynamicLink, SpdxRelationshipTypeExtensions.FromText("DYNAMIC_LINK"));
        Assert.AreEqual(SpdxRelationshipType.StaticLink, SpdxRelationshipTypeExtensions.FromText("STATIC_LINK"));
        Assert.AreEqual(SpdxRelationshipType.DataFileOf, SpdxRelationshipTypeExtensions.FromText("DATA_FILE_OF"));
        Assert.AreEqual(SpdxRelationshipType.TestCaseOf, SpdxRelationshipTypeExtensions.FromText("TEST_CASE_OF"));
        Assert.AreEqual(SpdxRelationshipType.BuildToolOf, SpdxRelationshipTypeExtensions.FromText("BUILD_TOOL_OF"));
        Assert.AreEqual(SpdxRelationshipType.DevToolOf, SpdxRelationshipTypeExtensions.FromText("DEV_TOOL_OF"));
        Assert.AreEqual(SpdxRelationshipType.TestOf, SpdxRelationshipTypeExtensions.FromText("TEST_OF"));
        Assert.AreEqual(SpdxRelationshipType.TestToolOf, SpdxRelationshipTypeExtensions.FromText("TEST_TOOL_OF"));
        Assert.AreEqual(SpdxRelationshipType.DocumentationOf, SpdxRelationshipTypeExtensions.FromText("DOCUMENTATION_OF"));
        Assert.AreEqual(SpdxRelationshipType.OptionalComponentOf, SpdxRelationshipTypeExtensions.FromText("OPTIONAL_COMPONENT_OF"));
        Assert.AreEqual(SpdxRelationshipType.MetafileOf, SpdxRelationshipTypeExtensions.FromText("METAFILE_OF"));
        Assert.AreEqual(SpdxRelationshipType.PackageOf, SpdxRelationshipTypeExtensions.FromText("PACKAGE_OF"));
        Assert.AreEqual(SpdxRelationshipType.Amends, SpdxRelationshipTypeExtensions.FromText("AMENDS"));
        Assert.AreEqual(SpdxRelationshipType.PrerequisiteFor, SpdxRelationshipTypeExtensions.FromText("PREREQUISITE_FOR"));
        Assert.AreEqual(SpdxRelationshipType.HasPrerequisite, SpdxRelationshipTypeExtensions.FromText("HAS_PREREQUISITE"));
        Assert.AreEqual(SpdxRelationshipType.RequirementDescriptionFor, SpdxRelationshipTypeExtensions.FromText("REQUIREMENT_DESCRIPTION_FOR"));
        Assert.AreEqual(SpdxRelationshipType.SpecificationFor, SpdxRelationshipTypeExtensions.FromText("SPECIFICATION_FOR"));
        Assert.AreEqual(SpdxRelationshipType.Other, SpdxRelationshipTypeExtensions.FromText("OTHER"));
    }

    /// <summary>
    /// Tests the <see cref="SpdxRelationshipTypeExtensions.FromText(string)"/> method for an invalid relationship type.
    /// </summary>
    [TestMethod]
    public void SpdxRelationshipTypeExtensions_FromText_Invalid()
    {
        var exception = Assert.ThrowsExactly<InvalidOperationException>(() => SpdxRelationshipTypeExtensions.FromText("invalid"));
        Assert.AreEqual("Unsupported SPDX Relationship Type 'invalid'", exception.Message);
    }

    /// <summary>
    /// Tests the <see cref="SpdxRelationshipTypeExtensions.ToText(SpdxRelationshipType)"/> method for the "CONTAINS" relationship type.
    /// </summary>
    [TestMethod]
    public void SpdxRelationshipTypeExtensions_ToText_Valid()
    {
        Assert.AreEqual("DESCRIBES", SpdxRelationshipType.Describes.ToText());
        Assert.AreEqual("DESCRIBED_BY", SpdxRelationshipType.DescribedBy.ToText());
        Assert.AreEqual("CONTAINS", SpdxRelationshipType.Contains.ToText());
        Assert.AreEqual("CONTAINED_BY", SpdxRelationshipType.ContainedBy.ToText());
        Assert.AreEqual("DEPENDS_ON", SpdxRelationshipType.DependsOn.ToText());
        Assert.AreEqual("DEPENDENCY_OF", SpdxRelationshipType.DependencyOf.ToText());
        Assert.AreEqual("DEPENDENCY_MANIFEST_OF", SpdxRelationshipType.DependencyManifestOf.ToText());
        Assert.AreEqual("BUILD_DEPENDENCY_OF", SpdxRelationshipType.BuildDependencyOf.ToText());
        Assert.AreEqual("DEV_DEPENDENCY_OF", SpdxRelationshipType.DevDependencyOf.ToText());
        Assert.AreEqual("OPTIONAL_DEPENDENCY_OF", SpdxRelationshipType.OptionalDependencyOf.ToText());
        Assert.AreEqual("PROVIDED_DEPENDENCY_OF", SpdxRelationshipType.ProvidedDependencyOf.ToText());
        Assert.AreEqual("TEST_DEPENDENCY_OF", SpdxRelationshipType.TestDependencyOf.ToText());
        Assert.AreEqual("RUNTIME_DEPENDENCY_OF", SpdxRelationshipType.RuntimeDependencyOf.ToText());
        Assert.AreEqual("EXAMPLE_OF", SpdxRelationshipType.ExampleOf.ToText());
        Assert.AreEqual("GENERATES", SpdxRelationshipType.Generates.ToText());
        Assert.AreEqual("GENERATED_FROM", SpdxRelationshipType.GeneratedFrom.ToText());
        Assert.AreEqual("ANCESTOR_OF", SpdxRelationshipType.AncestorOf.ToText());
        Assert.AreEqual("DESCENDANT_OF", SpdxRelationshipType.DescendantOf.ToText());
        Assert.AreEqual("VARIANT_OF", SpdxRelationshipType.VariantOf.ToText());
        Assert.AreEqual("DISTRIBUTION_ARTIFACT", SpdxRelationshipType.DistributionArtifact.ToText());
        Assert.AreEqual("PATCH_FOR", SpdxRelationshipType.PatchFor.ToText());
        Assert.AreEqual("PATCH_APPLIED", SpdxRelationshipType.PatchApplied.ToText());
        Assert.AreEqual("COPY_OF", SpdxRelationshipType.CopyOf.ToText());
        Assert.AreEqual("FILE_ADDED", SpdxRelationshipType.FileAdded.ToText());
        Assert.AreEqual("FILE_DELETED", SpdxRelationshipType.FileDeleted.ToText());
        Assert.AreEqual("FILE_MODIFIED", SpdxRelationshipType.FileModified.ToText());
        Assert.AreEqual("EXPANDED_FROM_ARCHIVE", SpdxRelationshipType.ExpandedFromArchive.ToText());
        Assert.AreEqual("DYNAMIC_LINK", SpdxRelationshipType.DynamicLink.ToText());
        Assert.AreEqual("STATIC_LINK", SpdxRelationshipType.StaticLink.ToText());
        Assert.AreEqual("DATA_FILE_OF", SpdxRelationshipType.DataFileOf.ToText());
        Assert.AreEqual("TEST_CASE_OF", SpdxRelationshipType.TestCaseOf.ToText());
        Assert.AreEqual("BUILD_TOOL_OF", SpdxRelationshipType.BuildToolOf.ToText());
        Assert.AreEqual("DEV_TOOL_OF", SpdxRelationshipType.DevToolOf.ToText());
        Assert.AreEqual("TEST_OF", SpdxRelationshipType.TestOf.ToText());
        Assert.AreEqual("TEST_TOOL_OF", SpdxRelationshipType.TestToolOf.ToText());
        Assert.AreEqual("DOCUMENTATION_OF", SpdxRelationshipType.DocumentationOf.ToText());
        Assert.AreEqual("OPTIONAL_COMPONENT_OF", SpdxRelationshipType.OptionalComponentOf.ToText());
        Assert.AreEqual("METAFILE_OF", SpdxRelationshipType.MetafileOf.ToText());
        Assert.AreEqual("PACKAGE_OF", SpdxRelationshipType.PackageOf.ToText());
        Assert.AreEqual("AMENDS", SpdxRelationshipType.Amends.ToText());
        Assert.AreEqual("PREREQUISITE_FOR", SpdxRelationshipType.PrerequisiteFor.ToText());
        Assert.AreEqual("HAS_PREREQUISITE", SpdxRelationshipType.HasPrerequisite.ToText());
        Assert.AreEqual("REQUIREMENT_DESCRIPTION_FOR", SpdxRelationshipType.RequirementDescriptionFor.ToText());
        Assert.AreEqual("SPECIFICATION_FOR", SpdxRelationshipType.SpecificationFor.ToText());
        Assert.AreEqual("OTHER", SpdxRelationshipType.Other.ToText());
    }

    /// <summary>
    /// Tests the <see cref="SpdxRelationshipTypeExtensions.ToText(SpdxRelationshipType)"/> method for an invalid relationship type.
    /// </summary>
    [TestMethod]
    public void SpdxRelationshipTypeExtensions_ToText_Invalid()
    {
        var exception = Assert.ThrowsExactly<InvalidOperationException>(() => ((SpdxRelationshipType)1000).ToText());
        Assert.AreEqual("Unsupported SPDX Relationship Type '1000'", exception.Message);
    }
}