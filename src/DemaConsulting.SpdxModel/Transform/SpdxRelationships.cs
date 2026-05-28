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

namespace DemaConsulting.SpdxModel.Transform;

/// <summary>
///     Transformations for SPDX relationships.
/// </summary>
/// <remarks>
///     This class is a stateless utility: all methods are static and carry no instance
///     state. The same instance (or the class itself) may be called concurrently from
///     multiple threads provided each call operates on a different <see cref="SpdxDocument"/>;
///     concurrent calls on the same document are not safe without external synchronization.
///     All mutating methods operate directly on <see cref="SpdxDocument.Relationships"/>.
/// </remarks>
public static class SpdxRelationships
{
    /// <summary>
    ///     Add new relationships to the SPDX document.
    /// </summary>
    /// <remarks>
    ///     All incoming relationships are validated before any mutation is performed.
    ///     This ensures that a validation failure leaves the document in its original state
    ///     (atomic with respect to the batch). Replacement uses
    ///     <see cref="SpdxRelationship.SameElements"/> (type-agnostic, matches by source and
    ///     target ID only) so that a replace-and-add can change the relationship type between
    ///     the same pair of elements. Individual deduplication within the add path uses
    ///     <see cref="SpdxRelationship.Same"/> (type-inclusive) so that relationships of
    ///     different types between the same elements co-exist correctly.
    ///     Side-effect: mutates <paramref name="document"/>.<see cref="SpdxDocument.Relationships"/>.
    /// </remarks>
    /// <param name="document">SPDX document to modify</param>
    /// <param name="relationships">New relationships to add</param>
    /// <param name="replace">
    ///     When <c>true</c>, existing relationships whose source and target match any incoming
    ///     relationship are removed before the new relationships are added.
    /// </param>
    /// <exception cref="ArgumentException">
    ///     Thrown when any relationship's source element ID is not found in the document,
    ///     or when any relationship's target element ID is not found in the document and is
    ///     neither <c>NOASSERTION</c> nor prefixed with <c>DocumentRef-</c>.
    ///     When this exception is thrown, the document is left unmodified.
    /// </exception>
    public static void Add(SpdxDocument document, IEnumerable<SpdxRelationship> relationships, bool replace = false)
    {
        // Materialize the enumerable so it can be iterated multiple times
        var incoming = relationships.ToArray();

        // Pre-validate all relationships before making any mutations.
        // This ensures the document is left unmodified if any relationship is invalid.
        foreach (var relationship in incoming)
        {
            ValidateRelationship(document, relationship);
        }

        // Handle replacing existing relationships
        if (replace)
        {
            // Remove all relationships that refer to the same elements as the new relationships
            document.Relationships =
            [
                ..document.Relationships
                    .Where(r => !incoming.Any(r2 => SpdxRelationship.SameElements.Equals(r, r2)))
            ];
        }

        // Add the new relationships (validation already passed, so no exceptions expected here)
        foreach (var relationship in incoming)
        {
            AddValidated(document, relationship);
        }
    }

    /// <summary>
    ///     Add a relationship to the SPDX document.
    /// </summary>
    /// <remarks>
    ///     If a relationship with the same source, target, and type already exists (as determined
    ///     by <see cref="SpdxRelationship.Same"/>), the existing entry is enhanced with any
    ///     additional field values from <paramref name="relationship"/>. Otherwise a deep copy is
    ///     appended to <see cref="SpdxDocument.Relationships"/>.
    ///     Side-effect: mutates <paramref name="document"/>.<see cref="SpdxDocument.Relationships"/>.
    /// </remarks>
    /// <param name="document">SPDX document to modify</param>
    /// <param name="relationship">SPDX relationship to add</param>
    /// <exception cref="ArgumentException">
    ///     Thrown when the relationship's source element ID (<see cref="SpdxElement.Id"/>)
    ///     is not found in the document, or when the target element ID
    ///     (<see cref="SpdxRelationship.RelatedSpdxElement"/>) is not found in the document and
    ///     is neither <c>NOASSERTION</c> nor prefixed with <c>DocumentRef-</c>.
    /// </exception>
    public static void Add(SpdxDocument document, SpdxRelationship relationship)
    {
        // Validate before any mutation
        ValidateRelationship(document, relationship);

        // Perform the actual add (validation already passed)
        AddValidated(document, relationship);
    }

    /// <summary>
    ///     Validates a relationship against the document without mutating it.
    /// </summary>
    /// <remarks>
    ///     This method performs all ID-existence checks WITHOUT mutating the document.
    ///     It MUST be called before any mutation (replacement or addition) to preserve
    ///     the atomicity guarantee: if validation fails the document remains unchanged.
    /// </remarks>
    /// <param name="document">SPDX document providing the element registry</param>
    /// <param name="relationship">Relationship to validate</param>
    /// <exception cref="ArgumentException">
    ///     Thrown when the source element ID is not found in the document, or when the target
    ///     element ID is not found and is neither <c>NOASSERTION</c> nor <c>DocumentRef-</c>-prefixed.
    /// </exception>
    private static void ValidateRelationship(SpdxDocument document, SpdxRelationship relationship)
    {
        // Ensure the relationship ID matches an element
        if (document.GetElement(relationship.Id) == null)
        {
            throw new ArgumentException($"Element {relationship.Id} not found in SPDX document", nameof(relationship));
        }

        // Ensure the relationship related-element ID matches an element, is NOASSERTION, or uses the DocumentRef- external-reference prefix
        if (!relationship.RelatedSpdxElement.StartsWith("DocumentRef-") &&
            relationship.RelatedSpdxElement != SpdxElement.NoAssertion &&
            document.GetElement(relationship.RelatedSpdxElement) == null)
        {
            throw new ArgumentException($"Element {relationship.RelatedSpdxElement} not found in SPDX document",
                nameof(relationship));
        }
    }

    /// <summary>
    ///     Adds a pre-validated relationship to the document (no validation performed).
    /// </summary>
    /// <remarks>
    ///     Callers MUST invoke <see cref="ValidateRelationship"/> on <paramref name="relationship"/>
    ///     before calling this method. No further ID validation is performed here; passing an
    ///     unvalidated relationship may produce an inconsistent document.
    /// </remarks>
    /// <param name="document">SPDX document to modify</param>
    /// <param name="relationship">Already-validated relationship to add</param>
    private static void AddValidated(SpdxDocument document, SpdxRelationship relationship)
    {
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
            document.Relationships = [.. document.Relationships.Append(existing)];
        }
    }
}
