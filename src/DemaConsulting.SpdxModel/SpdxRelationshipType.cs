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

namespace DemaConsulting.SpdxModel;

/// <summary>
///     SPDX Relationship Type enumeration
/// </summary>
/// <remarks>
///     The <see cref="Missing" /> sentinel value (value -1) represents an unknown or uninitialized
///     relationship type during partial deserialization. It must not be serialized to SPDX output;
///     <see cref="SpdxRelationshipTypeExtensions.ToText" /> throws <see cref="InvalidOperationException" />
///     for <see cref="Missing" />.
/// </remarks>
public enum SpdxRelationshipType
{
    /// <summary>
    ///     Missing relationship type
    /// </summary>
    /// <remarks>
    ///     Sentinel value used internally to represent an absent or uninitialized relationship type.
    ///     Never serialized to SPDX output.
    /// </remarks>
    Missing = -1,

    /// <summary>
    ///     Element describes the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>DESCRIBES</c>.
    /// </remarks>
    Describes,

    /// <summary>
    ///     Element is described by the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>DESCRIBED_BY</c>.
    /// </remarks>
    DescribedBy,

    /// <summary>
    ///     Element contains the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>CONTAINS</c>.
    /// </remarks>
    Contains,

    /// <summary>
    ///     Element is contained by the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>CONTAINED_BY</c>.
    /// </remarks>
    ContainedBy,

    /// <summary>
    ///     Element depends on the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>DEPENDS_ON</c>.
    /// </remarks>
    DependsOn,

    /// <summary>
    ///     Element is a dependency of the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>DEPENDENCY_OF</c>.
    /// </remarks>
    DependencyOf,

    /// <summary>
    ///     Element is a manifest file that lists a set of dependencies for the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>DEPENDENCY_MANIFEST_OF</c>.
    /// </remarks>
    DependencyManifestOf,

    /// <summary>
    ///     Element is a build dependency of the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>BUILD_DEPENDENCY_OF</c>.
    /// </remarks>
    BuildDependencyOf,

    /// <summary>
    ///     Element is a development dependency of the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>DEV_DEPENDENCY_OF</c>.
    /// </remarks>
    DevDependencyOf,

    /// <summary>
    ///     Element is an optional dependency of the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>OPTIONAL_DEPENDENCY_OF</c>.
    /// </remarks>
    OptionalDependencyOf,

    /// <summary>
    ///     Element is a to-be-provided dependency of the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>PROVIDED_DEPENDENCY_OF</c>.
    /// </remarks>
    ProvidedDependencyOf,

    /// <summary>
    ///     Element is a test dependency of the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>TEST_DEPENDENCY_OF</c>.
    /// </remarks>
    TestDependencyOf,

    /// <summary>
    ///     Element is a dependency required for the execution of the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>RUNTIME_DEPENDENCY_OF</c>.
    /// </remarks>
    RuntimeDependencyOf,

    /// <summary>
    ///     Element is an example of the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>EXAMPLE_OF</c>.
    /// </remarks>
    ExampleOf,

    /// <summary>
    ///     Element generates the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>GENERATES</c>.
    /// </remarks>
    Generates,

    /// <summary>
    ///     Element was generated from the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>GENERATED_FROM</c>.
    /// </remarks>
    GeneratedFrom,

    /// <summary>
    ///     Element is an ancestor (same lineage but pre-dated) the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>ANCESTOR_OF</c>.
    /// </remarks>
    AncestorOf,

    /// <summary>
    ///     Element is a descendant of (same lineage but post-dates) the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>DESCENDANT_OF</c>.
    /// </remarks>
    DescendantOf,

    /// <summary>
    ///     Element is a variant of (same lineage but not clear which came first) the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>VARIANT_OF</c>.
    /// </remarks>
    VariantOf,

    /// <summary>
    ///     Element is a distribution artifact of the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>DISTRIBUTION_ARTIFACT</c>.
    /// </remarks>
    DistributionArtifact,

    /// <summary>
    ///     Element is a patch file for (to be applied to) the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>PATCH_FOR</c>.
    /// </remarks>
    PatchFor,

    /// <summary>
    ///     Element is a patch file that has been applied to the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>PATCH_APPLIED</c>.
    /// </remarks>
    PatchApplied,

    /// <summary>
    ///     Element is an exact copy of the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>COPY_OF</c>.
    /// </remarks>
    CopyOf,

    /// <summary>
    ///     Element is a file that was added to the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>FILE_ADDED</c>.
    /// </remarks>
    FileAdded,

    /// <summary>
    ///     Element is a file that was deleted from the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>FILE_DELETED</c>.
    /// </remarks>
    FileDeleted,

    /// <summary>
    ///     Element is a file that was modified from the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>FILE_MODIFIED</c>.
    /// </remarks>
    FileModified,

    /// <summary>
    ///     Element has been expanded from an archive file
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>EXPANDED_FROM_ARCHIVE</c>.
    /// </remarks>
    ExpandedFromArchive,

    /// <summary>
    ///     Element dynamically links to the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>DYNAMIC_LINK</c>.
    /// </remarks>
    DynamicLink,

    /// <summary>
    ///     Element statically links to the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>STATIC_LINK</c>.
    /// </remarks>
    StaticLink,

    /// <summary>
    ///     Element is a data file used by the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>DATA_FILE_OF</c>.
    /// </remarks>
    DataFileOf,

    /// <summary>
    ///     Element is a test case used in testing the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>TEST_CASE_OF</c>.
    /// </remarks>
    TestCaseOf,

    /// <summary>
    ///     Element is used to build the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>BUILD_TOOL_OF</c>.
    /// </remarks>
    BuildToolOf,

    /// <summary>
    ///     Element is used as a development tool for the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>DEV_TOOL_OF</c>.
    /// </remarks>
    DevToolOf,

    /// <summary>
    ///     Element is used for testing the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>TEST_OF</c>.
    /// </remarks>
    TestOf,

    /// <summary>
    ///     Element is used as a test tool for the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>TEST_TOOL_OF</c>.
    /// </remarks>
    TestToolOf,

    /// <summary>
    ///     Element provides documentation of the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>DOCUMENTATION_OF</c>.
    /// </remarks>
    DocumentationOf,

    /// <summary>
    ///     Element is an optional component of the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>OPTIONAL_COMPONENT_OF</c>.
    /// </remarks>
    OptionalComponentOf,

    /// <summary>
    ///     Element is a metafile of the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>METAFILE_OF</c>.
    /// </remarks>
    MetafileOf,

    /// <summary>
    ///     Element is a package as part of the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>PACKAGE_OF</c>.
    /// </remarks>
    PackageOf,

    /// <summary>
    ///     Element is an SPDX document amending the SPDX information in the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>AMENDS</c>.
    /// </remarks>
    Amends,

    /// <summary>
    ///     Element is a prerequisite for the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>PREREQUISITE_FOR</c>.
    /// </remarks>
    PrerequisiteFor,

    /// <summary>
    ///     Element has a prerequisite of the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>HAS_PREREQUISITE</c>.
    /// </remarks>
    HasPrerequisite,

    /// <summary>
    ///     Element describes, illustrates, or specifies a requirement statement for the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>REQUIREMENT_DESCRIPTION_FOR</c>.
    /// </remarks>
    RequirementDescriptionFor,

    /// <summary>
    ///     Element describes, illustrates, or defines a design specification for the related element
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>SPECIFICATION_FOR</c>.
    /// </remarks>
    SpecificationFor,

    /// <summary>
    ///     Element has a relationship with the related element described in the comment field
    /// </summary>
    /// <remarks>
    ///     Maps to the SPDX relationship type token <c>OTHER</c>.
    /// </remarks>
    Other
}

/// <summary>
///     SPDX Relationship Type Extensions
/// </summary>
/// <remarks>
///     This class is stateless and thread-safe. Note the asymmetry: <see cref="FromText" /> accepts an empty string and
///     returns <see cref="SpdxRelationshipType.Missing" />, but <see cref="ToText" /> throws for
///     <see cref="SpdxRelationshipType.Missing" />. This design prevents silent serialization of sentinel values while
///     permitting partial deserialization.
/// </remarks>
public static class SpdxRelationshipTypeExtensions
{
    /// <summary>
    ///     Convert text to SpdxRelationshipType
    /// </summary>
    /// <remarks>
    ///     Comparison is case-insensitive. An empty or null input maps to <see cref="SpdxRelationshipType.Missing" />.
    ///     All 44 SPDX 2.x relationship type tokens are recognized.
    /// </remarks>
    /// <param name="relationshipType">Relationship Type text</param>
    /// <returns>SpdxRelationshipType</returns>
    /// <exception cref="InvalidOperationException">Thrown when <paramref name="relationshipType"/> is not a recognized SPDX relationship type string.</exception>
    public static SpdxRelationshipType FromText(string relationshipType)
    {
        return (relationshipType ?? string.Empty).ToUpperInvariant() switch
        {
            "" => SpdxRelationshipType.Missing,
            "DESCRIBES" => SpdxRelationshipType.Describes,
            "DESCRIBED_BY" => SpdxRelationshipType.DescribedBy,
            "CONTAINS" => SpdxRelationshipType.Contains,
            "CONTAINED_BY" => SpdxRelationshipType.ContainedBy,
            "DEPENDS_ON" => SpdxRelationshipType.DependsOn,
            "DEPENDENCY_OF" => SpdxRelationshipType.DependencyOf,
            "DEPENDENCY_MANIFEST_OF" => SpdxRelationshipType.DependencyManifestOf,
            "BUILD_DEPENDENCY_OF" => SpdxRelationshipType.BuildDependencyOf,
            "DEV_DEPENDENCY_OF" => SpdxRelationshipType.DevDependencyOf,
            "OPTIONAL_DEPENDENCY_OF" => SpdxRelationshipType.OptionalDependencyOf,
            "PROVIDED_DEPENDENCY_OF" => SpdxRelationshipType.ProvidedDependencyOf,
            "TEST_DEPENDENCY_OF" => SpdxRelationshipType.TestDependencyOf,
            "RUNTIME_DEPENDENCY_OF" => SpdxRelationshipType.RuntimeDependencyOf,
            "EXAMPLE_OF" => SpdxRelationshipType.ExampleOf,
            "GENERATES" => SpdxRelationshipType.Generates,
            "GENERATED_FROM" => SpdxRelationshipType.GeneratedFrom,
            "ANCESTOR_OF" => SpdxRelationshipType.AncestorOf,
            "DESCENDANT_OF" => SpdxRelationshipType.DescendantOf,
            "VARIANT_OF" => SpdxRelationshipType.VariantOf,
            "DISTRIBUTION_ARTIFACT" => SpdxRelationshipType.DistributionArtifact,
            "PATCH_FOR" => SpdxRelationshipType.PatchFor,
            "PATCH_APPLIED" => SpdxRelationshipType.PatchApplied,
            "COPY_OF" => SpdxRelationshipType.CopyOf,
            "FILE_ADDED" => SpdxRelationshipType.FileAdded,
            "FILE_DELETED" => SpdxRelationshipType.FileDeleted,
            "FILE_MODIFIED" => SpdxRelationshipType.FileModified,
            "EXPANDED_FROM_ARCHIVE" => SpdxRelationshipType.ExpandedFromArchive,
            "DYNAMIC_LINK" => SpdxRelationshipType.DynamicLink,
            "STATIC_LINK" => SpdxRelationshipType.StaticLink,
            "DATA_FILE_OF" => SpdxRelationshipType.DataFileOf,
            "TEST_CASE_OF" => SpdxRelationshipType.TestCaseOf,
            "BUILD_TOOL_OF" => SpdxRelationshipType.BuildToolOf,
            "DEV_TOOL_OF" => SpdxRelationshipType.DevToolOf,
            "TEST_OF" => SpdxRelationshipType.TestOf,
            "TEST_TOOL_OF" => SpdxRelationshipType.TestToolOf,
            "DOCUMENTATION_OF" => SpdxRelationshipType.DocumentationOf,
            "OPTIONAL_COMPONENT_OF" => SpdxRelationshipType.OptionalComponentOf,
            "METAFILE_OF" => SpdxRelationshipType.MetafileOf,
            "PACKAGE_OF" => SpdxRelationshipType.PackageOf,
            "AMENDS" => SpdxRelationshipType.Amends,
            "PREREQUISITE_FOR" => SpdxRelationshipType.PrerequisiteFor,
            "HAS_PREREQUISITE" => SpdxRelationshipType.HasPrerequisite,
            "REQUIREMENT_DESCRIPTION_FOR" => SpdxRelationshipType.RequirementDescriptionFor,
            "SPECIFICATION_FOR" => SpdxRelationshipType.SpecificationFor,
            "OTHER" => SpdxRelationshipType.Other,
            _ => throw new InvalidOperationException($"Unsupported SPDX Relationship Type '{relationshipType}'")
        };
    }

    /// <summary>
    ///     Convert SpdxRelationshipType to text
    /// </summary>
    /// <remarks>
    ///     This is an extension method so it can be called directly on a <see cref="SpdxRelationshipType" /> value.
    ///     Throws for the <see cref="SpdxRelationshipType.Missing" /> sentinel to prevent accidental serialization
    ///     of incomplete relationship data.
    /// </remarks>
    /// <param name="relationshipType">SpdxRelationshipType</param>
    /// <returns>Relationship Type text</returns>
    /// <exception cref="InvalidOperationException">Thrown when <paramref name="relationshipType"/> is <see cref="SpdxRelationshipType.Missing"/> or an unrecognized enum value.</exception>
    public static string ToText(this SpdxRelationshipType relationshipType)
    {
        return relationshipType switch
        {
            SpdxRelationshipType.Missing => throw new InvalidOperationException(
                "Attempt to serialize missing SPDX Relationship Type"),
            SpdxRelationshipType.Describes => "DESCRIBES",
            SpdxRelationshipType.DescribedBy => "DESCRIBED_BY",
            SpdxRelationshipType.Contains => "CONTAINS",
            SpdxRelationshipType.ContainedBy => "CONTAINED_BY",
            SpdxRelationshipType.DependsOn => "DEPENDS_ON",
            SpdxRelationshipType.DependencyOf => "DEPENDENCY_OF",
            SpdxRelationshipType.DependencyManifestOf => "DEPENDENCY_MANIFEST_OF",
            SpdxRelationshipType.BuildDependencyOf => "BUILD_DEPENDENCY_OF",
            SpdxRelationshipType.DevDependencyOf => "DEV_DEPENDENCY_OF",
            SpdxRelationshipType.OptionalDependencyOf => "OPTIONAL_DEPENDENCY_OF",
            SpdxRelationshipType.ProvidedDependencyOf => "PROVIDED_DEPENDENCY_OF",
            SpdxRelationshipType.TestDependencyOf => "TEST_DEPENDENCY_OF",
            SpdxRelationshipType.RuntimeDependencyOf => "RUNTIME_DEPENDENCY_OF",
            SpdxRelationshipType.ExampleOf => "EXAMPLE_OF",
            SpdxRelationshipType.Generates => "GENERATES",
            SpdxRelationshipType.GeneratedFrom => "GENERATED_FROM",
            SpdxRelationshipType.AncestorOf => "ANCESTOR_OF",
            SpdxRelationshipType.DescendantOf => "DESCENDANT_OF",
            SpdxRelationshipType.VariantOf => "VARIANT_OF",
            SpdxRelationshipType.DistributionArtifact => "DISTRIBUTION_ARTIFACT",
            SpdxRelationshipType.PatchFor => "PATCH_FOR",
            SpdxRelationshipType.PatchApplied => "PATCH_APPLIED",
            SpdxRelationshipType.CopyOf => "COPY_OF",
            SpdxRelationshipType.FileAdded => "FILE_ADDED",
            SpdxRelationshipType.FileDeleted => "FILE_DELETED",
            SpdxRelationshipType.FileModified => "FILE_MODIFIED",
            SpdxRelationshipType.ExpandedFromArchive => "EXPANDED_FROM_ARCHIVE",
            SpdxRelationshipType.DynamicLink => "DYNAMIC_LINK",
            SpdxRelationshipType.StaticLink => "STATIC_LINK",
            SpdxRelationshipType.DataFileOf => "DATA_FILE_OF",
            SpdxRelationshipType.TestCaseOf => "TEST_CASE_OF",
            SpdxRelationshipType.BuildToolOf => "BUILD_TOOL_OF",
            SpdxRelationshipType.DevToolOf => "DEV_TOOL_OF",
            SpdxRelationshipType.TestOf => "TEST_OF",
            SpdxRelationshipType.TestToolOf => "TEST_TOOL_OF",
            SpdxRelationshipType.DocumentationOf => "DOCUMENTATION_OF",
            SpdxRelationshipType.OptionalComponentOf => "OPTIONAL_COMPONENT_OF",
            SpdxRelationshipType.MetafileOf => "METAFILE_OF",
            SpdxRelationshipType.PackageOf => "PACKAGE_OF",
            SpdxRelationshipType.Amends => "AMENDS",
            SpdxRelationshipType.PrerequisiteFor => "PREREQUISITE_FOR",
            SpdxRelationshipType.HasPrerequisite => "HAS_PREREQUISITE",
            SpdxRelationshipType.RequirementDescriptionFor => "REQUIREMENT_DESCRIPTION_FOR",
            SpdxRelationshipType.SpecificationFor => "SPECIFICATION_FOR",
            SpdxRelationshipType.Other => "OTHER",
            _ => throw new InvalidOperationException($"Unsupported SPDX Relationship Type '{relationshipType}'")
        };
    }
}
