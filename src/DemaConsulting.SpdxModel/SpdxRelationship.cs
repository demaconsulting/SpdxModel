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
    /// Enhance missing fields in the relationship
    /// </summary>
    /// <param name="other">Other relationship to enhance with</param>
    public void Enhance(SpdxRelationship other)
    {
        // Enhance the element information
        EnhanceElement(other);
        
        // Populate the related-element field if missing
        if (string.IsNullOrWhiteSpace(RelatedSpdxElement))
            RelatedSpdxElement = other.RelatedSpdxElement;

        // Populate the relationship-type field if missing
        if (RelationshipType == SpdxRelationshipType.Missing)
            RelationshipType = other.RelationshipType;

        // Populate the comment if missing
        if (string.IsNullOrWhiteSpace(Comment))
            Comment = other.Comment;
    }

    /// <summary>
    /// Enhance missing relationships in array
    /// </summary>
    /// <param name="array">Array to enhance</param>
    /// <param name="others">Other array to enhance with</param>
    /// <returns>Updated array</returns>
    public static SpdxRelationship[] Enhance(SpdxRelationship[] array, SpdxRelationship[] others)
    {
        // Convert to list
        var list = array.ToList();

        // Iterate over other array
        foreach (var other in others)
        {
            // Check if other item is the same as one we have
            var annotation = list.Find(a => Same.Equals(a, other));
            if (annotation != null)
            {
                // Enhance our item with the other information
                annotation.Enhance(other);
            }
            else
            {
                // Add the new item to our list
                list.Add(other.DeepCopy());
            }
        }

        // Return as array
        return list.ToArray();
    }

    /// <summary>
    /// Perform validation of information
    /// </summary>
    /// <param name="issues">List to populate with issues</param>
    /// <param name="doc">Optional document for checking references</param>
    public void Validate(List<string> issues, SpdxDocument? doc)
    {
        // Validate SPDX Element ID Field - must refer to an element in this document
        if (Id.Length == 0)
            issues.Add("Relationship Invalid SPDX Element ID Field");
        else if (doc != null && doc.GetElement(Id) == null)
            issues.Add($"Relationship Invalid SPDX Element ID Field: {Id}");

        // Validate Related SPDX Element Field - can be NOASSERTION or external reference
        if (RelatedSpdxElement.Length == 0)
            issues.Add("Relationship Invalid Related SPDX Element Field");
        else if (!RelatedSpdxElement.StartsWith("DocumentRef-") && RelatedSpdxElement != "NOASSERTION" && doc != null && doc.GetElement(RelatedSpdxElement) == null)
            issues.Add($"Relationship Invalid Related SPDX Element Field: {RelatedSpdxElement}");

        // Validate Relationship Type Field
        if (RelationshipType == SpdxRelationshipType.Missing)
            issues.Add("Relationship Invalid Relationship Type Field");
    }

    /// <summary>
    /// Equality Comparer to test for the same relationship
    /// </summary>
    private sealed class SpdxRelationshipSame : IEqualityComparer<SpdxRelationship>
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