namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX Relationship Type enumeration
/// </summary>
public enum SpdxRelationshipType
{
    Missing = -1,
    Describes,
    DescribedBy,
    Contains,
    ContainedBy,
    DependsOn,
    DependencyOf,
    DependencyManifestOf,
    BuildDependencyOf,
    DevDependencyOf,
    OptionalDependencyOf,
    ProvidedDependencyOf,
    TestDependencyOf,
    RuntimeDependencyOf,
    ExampleOf,
    Generates,
    GeneratedFrom,
    AncestorOf,
    DescendantOf,
    VariantOf,
    DistributionArtifact,
    PatchFor,
    PatchApplied,
    CopyOf,
    FileAdded,
    FileDeleted,
    FileModified,
    ExpandedFromArchive,
    DynamicLink,
    StaticLink,
    DataFileOf,
    TestCaseOf,
    BuildToolOf,
    DevToolOf,
    TestOf,
    TestToolOf,
    DocumentationOf,
    OptionalComponentOf,
    MetafileOf,
    PackageOf,
    Amends,
    PrerequisiteFor,
    HasPrerequisite,
    RequirementDescriptionFor,
    SpecificationFor,
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
            SpdxRelationshipType.Missing => throw new InvalidOperationException("Attempt to serialize missing SPDX Relationship Type"),
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