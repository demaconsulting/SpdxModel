namespace DemaConsulting.SpdxModel.Transform;

/// <summary>
/// Transformations for SPDX relationships.
/// </summary>
public static class SpdxRelationships
{
    /// <summary>
    /// Add new relationships to the SPDX document.
    /// </summary>
    /// <param name="document">SPDX document</param>
    /// <param name="relationships">New relationships to add</param>
    /// <param name="replace">True to replace existing relationships</param>
    public static void Add(SpdxDocument document, IEnumerable<SpdxRelationship> relationships, bool replace = false)
    {
        // Handle replacing existing relationships
        if (replace)
        {
            // Remove all relationships that refer to the same elements as the new relationships
            document.Relationships = document.Relationships
                .Where(r => !relationships.Any(
                    r2 => SpdxRelationship.SameElements.Equals(r, r2)))
                .ToArray();
        }

        // Add the new relationships
        foreach (var relationship in relationships)
        {
            Add(document, relationship);
        }
    }

    /// <summary>
    /// Add a relationship to the SPDX document.
    /// </summary>
    /// <param name="document">SPDX document</param>
    /// <param name="relationship">SPDX relationship to add</param>
    /// <exception cref="ArgumentException">Thrown if the relationship argument is invalid</exception>
    public static void Add(SpdxDocument document, SpdxRelationship relationship)
    {
        // Ensure the relationship ID matches an element
        if (document.GetElement(relationship.Id) == null)
            throw new ArgumentException($"Element {relationship.Id} not found in SPDX document", nameof(relationship));

        // Ensure the relationship related-element ID matches an element
        if (document.GetElement(relationship.RelatedSpdxElement) == null)
            throw new ArgumentException($"Element {relationship.RelatedSpdxElement} not found in SPDX document", nameof(relationship));

        // Look for an existing relationship
        var existing = Array.Find(document.Relationships, r => SpdxRelationship.Same.Equals(r, relationship));
        if (existing != null)
        {
            // Enhance the existing relationship
            existing.Enhance(relationship);
        }
        else
        {
            // Copy the new relationship
            existing = relationship.DeepCopy();
            document.Relationships = document.Relationships.Append(existing).ToArray();
        }
    }
}