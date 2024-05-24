namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX Relationship class
/// </summary>
/// <remarks>
/// Relationships referenced in the SPDX document.
/// </remarks>
public class SpdxRelationship : SpdxElement
{
    /// <summary>
    /// Related SPDX Element Field
    /// </summary>
    /// <remarks>
    /// SPDX ID for SpdxElement.  A related SpdxElement.
    /// </remarks>
    public string RelatedSpdxElement { get; set; } = string.Empty;

    /// <summary>
    /// Relationship Type Field
    /// </summary>
    /// <remarks>
    /// Describes the type of relationship between two SPDX elements.
    /// </remarks>
    public SpdxRelationshipType RelationshipType { get; set; } = SpdxRelationshipType.Missing;

    /// <summary>
    /// Relationship Comment Field
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Perform validation of information
    /// </summary>
    /// <param name="issues">List to populate with issues</param>
    public void Validate(List<string> issues)
    {
        // Validate SPDX Element ID Field
        if (Id.Length == 0)
            issues.Add("Relationship Invalid SPDX Element ID Field");

        // Validate Related SPDX Element Field
        if (RelatedSpdxElement.Length == 0)
            issues.Add("Relationship Invalid Related SPDX Element Field");

        // Validate Relationship Type Field
        if (RelationshipType == SpdxRelationshipType.Missing)
            issues.Add("Relationship Invalid Relationship Type Field");
    }
}