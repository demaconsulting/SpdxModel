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
///     SPDX Relationship class
/// </summary>
/// <remarks>
///     Relationships referenced in the SPDX document.
/// </remarks>
public sealed class SpdxRelationship : SpdxElement
{
    /// <summary>
    ///     Equality comparer for the same relationship
    /// </summary>
    /// <remarks>
    ///     This considers relationships as being the same if they have the same
    ///     elements and relationship type. Note that this does not work across
    ///     documents as the element IDs are document-specific.
    /// </remarks>
    public static readonly IEqualityComparer<SpdxRelationship> Same = new SpdxRelationshipSame();

    /// <summary>
    ///     Equality comparer for the same relationship elements
    /// </summary>
    /// <remarks>
    ///     This considers relationships as being the same if they have the same
    ///     elements. Note that this does not work across documents as the element
    ///     IDs are document-specific.
    /// </remarks>
    public static readonly IEqualityComparer<SpdxRelationship> SameElements = new SpdxRelationshipSameElements();

    /// <summary>
    ///     Related SPDX Element Field
    /// </summary>
    /// <remarks>
    ///     SPDX ID for SpdxElement.  A related SpdxElement.
    /// </remarks>
    public string RelatedSpdxElement { get; set; } = "";

    /// <summary>
    ///     Relationship Type Field
    /// </summary>
    /// <remarks>
    ///     Describes the type of relationship between two SPDX elements.
    /// </remarks>
    public SpdxRelationshipType RelationshipType { get; set; } = SpdxRelationshipType.Missing;

    /// <summary>
    ///     Relationship Comment Field
    /// </summary>
    /// <remarks>
    ///     Optional free-text comment providing human-readable context for the relationship.
    /// </remarks>
    public string? Comment { get; set; }

    /// <summary>
    ///     Make a deep-copy of this object
    /// </summary>
    /// <remarks>
    ///     All scalar fields are copied by value. The returned instance is fully independent
    ///     of the original and may be mutated without side effects.
    /// </remarks>
    /// <returns>Deep copy of this object</returns>
    public SpdxRelationship DeepCopy()
    {
        return new SpdxRelationship
        {
            Id = Id,
            RelatedSpdxElement = RelatedSpdxElement,
            RelationshipType = RelationshipType,
            Comment = Comment
        };
    }

    /// <summary>
    ///     Enhance missing fields in the relationship
    /// </summary>
    /// <remarks>
    ///     Each field in this instance is replaced only if its current value is null, empty, or the
    ///     <see cref="SpdxRelationshipType.Missing" /> sentinel. Existing non-empty values are never overwritten.
    /// </remarks>
    /// <param name="other">Other relationship to enhance with</param>
    public void Enhance(SpdxRelationship other)
    {
        // Enhance the element information
        EnhanceElement(other);

        // Populate the related-element field if missing
        RelatedSpdxElement = SpdxHelpers.EnhanceString(RelatedSpdxElement, other.RelatedSpdxElement) ?? "";

        // Populate the relationship-type field if missing
        if (RelationshipType == SpdxRelationshipType.Missing)
        {
            RelationshipType = other.RelationshipType;
        }

        // Populate the comment if missing
        Comment = SpdxHelpers.EnhanceString(Comment, other.Comment);
    }

    /// <summary>
    ///     Enhance missing relationships in array
    /// </summary>
    /// <remarks>
    ///     Relationships are matched using the <see cref="Same" /> comparer (by <see cref="SpdxElement.Id" />,
    ///     <see cref="RelationshipType" />, and <see cref="RelatedSpdxElement" />). Matching relationships are enhanced
    ///     in place; non-matching relationships from <paramref name="others" /> are deep-copied and appended.
    /// </remarks>
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
            var existing = list.Find(a => Same.Equals(a, other));
            if (existing != null)
            {
                // Enhance our item with the other information
                existing.Enhance(other);
            }
            else
            {
                // Add the new item to our list
                list.Add(other.DeepCopy());
            }
        }

        // Return as array
        return [.. list];
    }

    /// <summary>
    ///     Perform validation of information
    /// </summary>
    /// <remarks>
    ///     Validates the SPDX element ID, the related element ID, and the relationship type. When
    ///     <paramref name="doc" /> is provided, element IDs are also checked for existence within that document.
    ///     External references (prefixed with <c>DocumentRef-</c>) and <c>NOASSERTION</c> are accepted without
    ///     document lookup.
    /// </remarks>
    /// <param name="issues">List to populate with issues</param>
    /// <param name="doc">Optional document for checking references</param>
    public void Validate(List<string> issues, SpdxDocument? doc)
    {
        // Validate SPDX Element ID Field - must refer to an element in this document
        if (Id.Length == 0)
        {
            issues.Add("Relationship Invalid SPDX Element ID Field - Empty");
        }
        else if (doc != null && doc.GetElement(Id) == null)
        {
            issues.Add($"Relationship Invalid SPDX Element ID Field '{Id}'");
        }

        // Validate Related SPDX Element Field - can be NOASSERTION or external reference
        if (RelatedSpdxElement.Length == 0)
        {
            issues.Add("Relationship Invalid Related SPDX Element Field - Empty");
        }
        else if (!RelatedSpdxElement.StartsWith("DocumentRef-") && RelatedSpdxElement != NoAssertion && doc != null &&
                 doc.GetElement(RelatedSpdxElement) == null)
        {
            issues.Add($"Relationship Invalid Related SPDX Element Field '{RelatedSpdxElement}'");
        }

        // Validate Relationship Type Field
        if (RelationshipType == SpdxRelationshipType.Missing)
        {
            issues.Add("Relationship Invalid Relationship Type Field - Missing");
        }
    }

    /// <summary>
    ///     Equality Comparer to test for the same relationship
    /// </summary>
    /// <remarks>
    ///     Two relationships are considered the same when they share the same <see cref="SpdxElement.Id" />,
    ///     <see cref="SpdxRelationship.RelationshipType" />, and <see cref="SpdxRelationship.RelatedSpdxElement" />.
    ///     A dedicated nested class is used rather than an ad-hoc lambda so the comparer instance can be stored in the
    ///     <see cref="SpdxRelationship.Same" /> field and passed to LINQ operations without boxing or allocation.
    /// </remarks>
    private sealed class SpdxRelationshipSame : IEqualityComparer<SpdxRelationship>
    {
        /// <summary>
        ///     Determines whether two <see cref="SpdxRelationship" /> instances are considered the same.
        /// </summary>
        /// <remarks>
        ///     Two relationships are considered equal when they share the same <see cref="SpdxElement.Id" />,
        ///     <see cref="SpdxRelationship.RelatedSpdxElement" />, and <see cref="SpdxRelationship.RelationshipType" />.
        ///     Fields such as <see cref="SpdxRelationship.Comment" /> are ignored during comparison.
        /// </remarks>
        public bool Equals(SpdxRelationship? r1, SpdxRelationship? r2)
        {
            if (ReferenceEquals(r1, r2))
            {
                return true;
            }

            if (r1 == null || r2 == null)
            {
                return false;
            }

            return r1.Id == r2.Id &&
                   r1.RelationshipType == r2.RelationshipType &&
                   r1.RelatedSpdxElement == r2.RelatedSpdxElement;
        }

        /// <summary>
        ///     Returns a hash code for the specified <see cref="SpdxRelationship" /> instance.
        /// </summary>
        /// <remarks>
        ///     The hash is derived from the <see cref="SpdxElement.Id" />, <see cref="SpdxRelationship.RelationshipType" />,
        ///     and <see cref="SpdxRelationship.RelatedSpdxElement" /> fields, consistent with the equality comparison
        ///     performed by <see cref="Equals(SpdxRelationship, SpdxRelationship)" />.
        /// </remarks>
        public int GetHashCode(SpdxRelationship obj)
        {
            return HashCode.Combine(
                obj.Id,
                obj.RelationshipType,
                obj.RelatedSpdxElement);
        }
    }

    /// <summary>
    ///     Equality Comparer to test for the same elements
    /// </summary>
    /// <remarks>
    ///     Two relationships are considered to share the same elements when they have the same
    ///     <see cref="SpdxElement.Id" /> and <see cref="SpdxRelationship.RelatedSpdxElement" />,
    ///     regardless of <see cref="SpdxRelationship.RelationshipType" />. Used when deduplicating by
    ///     element endpoints regardless of relationship kind. Backed by the <see cref="SpdxRelationship.SameElements" /> field.
    /// </remarks>
    private sealed class SpdxRelationshipSameElements : IEqualityComparer<SpdxRelationship>
    {
        /// <summary>
        ///     Determines whether two <see cref="SpdxRelationship" /> instances share the same elements.
        /// </summary>
        /// <remarks>
        ///     Two relationships are considered equal when they share the same <see cref="SpdxElement.Id" /> and
        ///     <see cref="SpdxRelationship.RelatedSpdxElement" />, regardless of
        ///     <see cref="SpdxRelationship.RelationshipType" />. Used when deduplicating by element endpoints only.
        /// </remarks>
        public bool Equals(SpdxRelationship? r1, SpdxRelationship? r2)
        {
            if (ReferenceEquals(r1, r2))
            {
                return true;
            }

            if (r1 == null || r2 == null)
            {
                return false;
            }

            return r1.Id == r2.Id &&
                   r1.RelatedSpdxElement == r2.RelatedSpdxElement;
        }

        /// <summary>
        ///     Returns a hash code for the specified <see cref="SpdxRelationship" /> instance.
        /// </summary>
        /// <remarks>
        ///     The hash is derived from the <see cref="SpdxElement.Id" /> and
        ///     <see cref="SpdxRelationship.RelatedSpdxElement" /> fields, consistent with the equality comparison
        ///     performed by <see cref="Equals(SpdxRelationship, SpdxRelationship)" />.
        /// </remarks>
        public int GetHashCode(SpdxRelationship obj)
        {
            return HashCode.Combine(obj.Id, obj.RelatedSpdxElement);
        }
    }
}
