namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX Relationship Type enumeration
/// </summary>
public enum SpdxRelationshipType
{
    /// <summary>
    /// Missing relationship type
    /// </summary>
    Missing = -1,

    /// <summary>
    /// Element describes the related element
    /// </summary>
    Describes,

    /// <summary>
    /// Element is described by the related element
    /// </summary>
    DescribedBy,

    /// <summary>
    /// Element contains the related element
    /// </summary>
    Contains,

    /// <summary>
    /// Element is contained by the related element
    /// </summary>
    ContainedBy,

    /// <summary>
    /// Element depends on the related element
    /// </summary>
    DependsOn,

    /// <summary>
    /// Element is a dependency of the related element
    /// </summary>
    DependencyOf,

    /// <summary>
    /// Element is a manifest file that lists a set of dependencies for the related element
    /// </summary>
    DependencyManifestOf,

    /// <summary>
    /// Element is a build dependency of the related element
    /// </summary>
    BuildDependencyOf,

    /// <summary>
    /// Element is a development dependency of the related element
    /// </summary>
    DevDependencyOf,

    /// <summary>
    /// Element is an optional dependency of the related element
    /// </summary>
    OptionalDependencyOf,

    /// <summary>
    /// Element is a to-be-provided dependency of the related element
    /// </summary>
    ProvidedDependencyOf,

    /// <summary>
    /// Element is a test dependency of the related element
    /// </summary>
    TestDependencyOf,

    /// <summary>
    /// Element is a dependency required for the execution of the related element
    /// </summary>
    RuntimeDependencyOf,

    /// <summary>
    /// Element is an example of the related element
    /// </summary>
    ExampleOf,

    /// <summary>
    /// Element generates the related element
    /// </summary>
    Generates,

    /// <summary>
    /// Element was generated from the related element
    /// </summary>
    GeneratedFrom,

    /// <summary>
    /// Element is an ancestor (same lineage but pre-dated) the related element
    /// </summary>
    AncestorOf,

    /// <summary>
    /// Element is a descendant of (same lineage but post-dates) the related element
    /// </summary>
    DescendantOf,

    /// <summary>
    /// Element is a variant of (same lineage but not clear which came first) the related element
    /// </summary>
    VariantOf,

    /// <summary>
    /// Element is a distribution artifact of the related element
    /// </summary>
    DistributionArtifact,

    /// <summary>
    /// Element is a patch file for (to be applied to) the related element
    /// </summary>
    PatchFor,

    /// <summary>
    /// Element is a patch file that has been applied to the related element
    /// </summary>
    PatchApplied,

    /// <summary>
    /// Element is an exact copy of the related element
    /// </summary>
    CopyOf,

    /// <summary>
    /// Element is a file that was added to the related element
    /// </summary>
    FileAdded,

    /// <summary>
    /// Element is a file that was deleted from the related element
    /// </summary>
    FileDeleted,

    /// <summary>
    /// Element is a file that was modified from the related element
    /// </summary>
    FileModified,

    /// <summary>
    /// Element has been expanded from an archive file
    /// </summary>
    ExpandedFromArchive,

    /// <summary>
    /// Element dynamically links to the related element
    /// </summary>
    DynamicLink,

    /// <summary>
    /// Element statically links to the related element
    /// </summary>
    StaticLink,

    /// <summary>
    /// Element is a data file used by the related element
    /// </summary>
    DataFileOf,

    /// <summary>
    /// Element is a test cased used in testing the related element
    /// </summary>
    TestCaseOf,

    /// <summary>
    /// Element is used to build the related element
    /// </summary>
    BuildToolOf,

    /// <summary>
    /// Element is used as a development tool for the related element
    /// </summary>
    DevToolOf,

    /// <summary>
    /// Element is used for testing the related element
    /// </summary>
    TestOf,

    /// <summary>
    /// Element is used as a test tool for the related element
    /// </summary>
    TestToolOf,

    /// <summary>
    /// Element provides documentation of the related element
    /// </summary>
    DocumentationOf,

    /// <summary>
    /// Element is an optional component of the related element
    /// </summary>
    OptionalComponentOf,

    /// <summary>
    /// Element is a metafile of the related element
    /// </summary>
    MetafileOf,

    /// <summary>
    /// Element is a package as part of the related element
    /// </summary>
    PackageOf,

    /// <summary>
    /// Element is an SPDX document amending the SPDX information in the related element
    /// </summary>
    Amends,

    /// <summary>
    /// Element is a prerequisite for the related element
    /// </summary>
    PrerequisiteFor,

    /// <summary>
    /// Element has a prerequisite of the related element
    /// </summary>
    HasPrerequisite,

    /// <summary>
    /// Element describes, illustrates, or specifies a requirement statement for the related element
    /// </summary>
    RequirementDescriptionFor,

    /// <summary>
    /// Element describes, illustrates, or defines a design specification for the related element
    /// </summary>
    SpecificationFor,

    /// <summary>
    /// Element has a relationship with the related element described in the comment field
    /// </summary>
    Other
}

/// <summary>
/// SPDX Relationship Type Extensions
/// </summary>
public static class SpdxRelationshipTypeExtensions
{
    /// <summary>
    /// Convert text to SpdxRelationshipType
    /// </summary>
    /// <param name="relationshipType">Relationship Type text</param>
    /// <returns>SpdxRelationshipType</returns>
    /// <exception cref="InvalidOperationException">on error</exception>
    public static SpdxRelationshipType FromText(string relationshipType)
    {
        return relationshipType switch
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
            _ => throw new InvalidOperationException($"Unsupported SPDX Relationship Type {relationshipType}")
        };
    }

    /// <summary>
    /// Convert SpdxRelationshipType to text
    /// </summary>
    /// <param name="relationshipType">SpdxRelationshipType</param>
    /// <returns>Relationship Type text</returns>
    /// <exception cref="InvalidOperationException">on error</exception>
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
            _ => throw new InvalidOperationException($"Unsupported SPDX Relationship Type {relationshipType}")
        };
    }
}