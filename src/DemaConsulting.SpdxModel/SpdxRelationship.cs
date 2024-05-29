namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX Relationship class
/// </summary>
/// <remarks>
/// Relationships referenced in the SPDX document.
/// </remarks>
public sealed class SpdxRelationship : SpdxElement
{
    /// <summary>
    /// Equality comparer for the same relationship
    /// </summary>
    /// <remarks>
    /// This considers relationships as being the same if they have the same
    /// elements and relationship type. Note that this does not work across
    /// documents as the element IDs are document-specific.
    /// </remarks>
    public static readonly IEqualityComparer<SpdxRelationship> Same = new SpdxRelationshipSame();

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
    /// Make a deep-copy of this object
    /// </summary>
    /// <returns>Deep copy of this object</returns>
    public SpdxRelationship DeepCopy() =>
        new()
        {
            Id = Id,
            RelatedSpdxElement = RelatedSpdxElement,
            RelationshipType = RelationshipType,
            Comment = Comment
        };

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

    /// <summary>
    /// Equality Comparer to test for the same relationship
    /// </summary>
    private class SpdxRelationshipSame : IEqualityComparer<SpdxRelationship>
    {
        /// <inheritdoc />
        public bool Equals(SpdxRelationship? r1, SpdxRelationship? r2)
        {
            if (ReferenceEquals(r1, r2)) return true;
            if (r1 == null || r2 == null) return false;

            return r1.Id == r2.Id &&
                   r1.RelationshipType == r2.RelationshipType &&
                   r1.RelatedSpdxElement == r2.RelatedSpdxElement;
        }

        /// <inheritdoc />
        public int GetHashCode(SpdxRelationship obj)
        {
            return obj.Id.GetHashCode() ^
                   obj.RelationshipType.GetHashCode() ^
                   obj.RelatedSpdxElement.GetHashCode();
        }
    }
}