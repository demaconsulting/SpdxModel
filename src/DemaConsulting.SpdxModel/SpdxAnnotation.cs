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
///     Represents an SPDX annotation — a review or informational comment attached to an SPDX element.
/// </summary>
/// <remarks>
///     Annotations support compliance workflows where reviewers document findings and decisions about
///     software components. Each annotation records who made it (<see cref="Annotator"/>), when
///     (<see cref="Date"/>), what category it belongs to (<see cref="Type"/>), and the free-text
///     content (<see cref="Comment"/>). See the <em>SpdxAnnotation Design</em> for the full data
///     model and method descriptions.
/// </remarks>
public sealed class SpdxAnnotation : SpdxElement
{
    /// <summary>
    ///     Equality comparer for the same annotation
    /// </summary>
    /// <remarks>
    ///     This considers annotations as being the same if they have the same
    ///     annotator, date, type, and comment.
    /// </remarks>
    public static readonly IEqualityComparer<SpdxAnnotation> Same = new SpdxAnnotationSame();

    /// <summary>
    ///     Annotator Field
    /// </summary>
    /// <remarks>
    ///     This field identifies the person, organization, or tool that has
    ///     commented on a file, package, snippet, or the entire document.
    ///     This field is required for a valid SPDX annotation; <see cref="Validate"/>
    ///     will report an error if it is absent.
    /// </remarks>
    public string Annotator { get; set; } = "";

    /// <summary>
    ///     Annotation Date Field
    /// </summary>
    /// <remarks>
    ///     Identify when the comment was made. This is to be specified according
    ///     to the combined date and time in the UTC format, as specified in the
    ///     ISO 8601 standard.
    ///     This field is required for a valid SPDX annotation; <see cref="Validate"/>
    ///     will report an error if the value is absent or not a valid ISO 8601 date-time.
    /// </remarks>
    public string Date { get; set; } = "";

    /// <summary>
    ///     Annotation Type Field
    /// </summary>
    /// <remarks>
    ///     Indicates the category of the annotation. Valid values are
    ///     <see cref="SpdxAnnotationType.Review"/> and <see cref="SpdxAnnotationType.Other"/>.
    ///     This field is required for a valid SPDX annotation; <see cref="Validate"/>
    ///     will report an error if the value is <see cref="SpdxAnnotationType.Missing"/>.
    /// </remarks>
    public SpdxAnnotationType Type { get; set; } = SpdxAnnotationType.Missing;

    /// <summary>
    ///     Annotation Comment field
    /// </summary>
    /// <remarks>
    ///     Free-text content of the annotation describing the finding or note left by
    ///     the annotator. This field is required for a valid SPDX annotation;
    ///     <see cref="Validate"/> will report an error if the value is absent or empty.
    /// </remarks>
    public string Comment { get; set; } = "";

    /// <summary>
    ///     Make a deep-copy of this object
    /// </summary>
    /// <remarks>
    ///     Returns an independent copy with no shared mutable references — mutating the copy
    ///     does not affect the original and vice versa. Used by <see cref="Enhance(SpdxAnnotation[], SpdxAnnotation[])"/>
    ///     when a new annotation is appended from the other array.
    /// </remarks>
    /// <returns>Deep copy of this object</returns>
    public SpdxAnnotation DeepCopy()
    {
        return new SpdxAnnotation
        {
            Id = Id,
            Annotator = Annotator,
            Date = Date,
            Type = Type,
            Comment = Comment
        };
    }

    /// <summary>
    ///     Enhance missing fields in the annotation
    /// </summary>
    /// <remarks>
    ///     This operation is additive-only: it never overwrites a non-empty field on
    ///     <c>this</c> instance. Fields are only populated when they are currently empty/missing.
    ///     The <see cref="SpdxAnnotationType.Missing"/> sentinel is used to detect an absent
    ///     <see cref="Type"/> field.
    /// </remarks>
    /// <param name="other">Other annotation to enhance with</param>
    public void Enhance(SpdxAnnotation other)
    {
        // Enhance the base element
        EnhanceElement(other);

        // Populate the annotator if missing
        Annotator = SpdxHelpers.EnhanceString(Annotator, other.Annotator) ?? "";

        // Populate the date if missing
        Date = SpdxHelpers.EnhanceString(Date, other.Date) ?? "";

        // Populate the type if missing
        if (Type == SpdxAnnotationType.Missing)
        {
            Type = other.Type;
        }

        // Populate the comment if missing
        Comment = SpdxHelpers.EnhanceString(Comment, other.Comment) ?? "";
    }

    /// <summary>
    ///     Enhance missing annotations in array
    /// </summary>
    /// <remarks>
    ///     Matches annotations using <see cref="Same"/> (annotator + date + type + comment).
    ///     For each entry in <paramref name="others"/>: if a matching annotation already exists
    ///     in <paramref name="array"/> it is enhanced in place; otherwise a deep copy is appended.
    ///     The returned array may be larger than the input array.
    /// </remarks>
    /// <param name="array">Array to enhance</param>
    /// <param name="others">Other array to enhance with</param>
    /// <returns>Updated array</returns>
    public static SpdxAnnotation[] Enhance(SpdxAnnotation[] array, SpdxAnnotation[] others)
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
    ///     Issues are appended to <paramref name="issues"/> rather than thrown as exceptions,
    ///     allowing all validation errors across the document to be collected in a single pass.
    ///     The caller is responsible for checking whether any issues were added after this call.
    /// </remarks>
    /// <param name="parent">
    ///     Identifier of the parent element (e.g. package or file SPDX-ID) used as
    ///     a prefix in issue messages so callers can locate the problematic annotation.
    /// </param>
    /// <param name="issues">List to populate with validation issues</param>
    public void Validate(string parent, List<string> issues)
    {
        // Validate Annotator Field
        if (Annotator.Length == 0)
        {
            issues.Add($"{parent} Invalid Annotator Field - Empty");
        }

        // Validate Annotation Date Field
        if (!SpdxHelpers.IsValidSpdxDateTime(Date))
        {
            issues.Add($"{parent} Invalid Annotation Date Field '{Date}'");
        }

        // Validate Annotation Type Field
        if (Type == SpdxAnnotationType.Missing)
        {
            issues.Add($"{parent} Invalid Annotation Type Field - Missing");
        }

        // Validate Annotation Comment Field
        if (Comment.Length == 0)
        {
            issues.Add($"{parent} Invalid Annotation Comment - Empty");
        }
    }

    /// <summary>
    ///     Equality Comparer to test for the same annotation
    /// </summary>
    /// <remarks>
    ///     Implements identity semantics used by <see cref="Enhance(SpdxAnnotation[], SpdxAnnotation[])"/>
    ///     to detect duplicate annotations before merging. Two annotations are considered the
    ///     same when their <see cref="SpdxAnnotation.Annotator"/>, <see cref="SpdxAnnotation.Date"/>,
    ///     <see cref="SpdxAnnotation.Type"/>, and <see cref="SpdxAnnotation.Comment"/> fields are all equal,
    ///     regardless of their <see cref="SpdxElement.Id"/>.
    /// </remarks>
    private sealed class SpdxAnnotationSame : IEqualityComparer<SpdxAnnotation>
    {
        /// <inheritdoc />
        /// <remarks>
        ///     Compares the four identity fields: <see cref="SpdxAnnotation.Annotator"/>,
        ///     <see cref="SpdxAnnotation.Date"/>, <see cref="SpdxAnnotation.Type"/>, and
        ///     <see cref="SpdxAnnotation.Comment"/>. The <see cref="SpdxElement.Id"/> field
        ///     is intentionally excluded so that an annotation with or without an assigned
        ///     SPDX-ID is still recognized as the same logical entry.
        /// </remarks>
        public bool Equals(SpdxAnnotation? a1, SpdxAnnotation? a2)
        {
            if (ReferenceEquals(a1, a2))
            {
                return true;
            }

            if (a1 == null || a2 == null)
            {
                return false;
            }

            return a1.Annotator == a2.Annotator &&
                   a1.Date == a2.Date &&
                   a1.Type == a2.Type &&
                   a1.Comment == a2.Comment;
        }

        /// <inheritdoc />
        /// <remarks>
        ///     Computes the hash from the same four identity fields as <see cref="Equals"/>:
        ///     <see cref="SpdxAnnotation.Annotator"/>, <see cref="SpdxAnnotation.Date"/>,
        ///     <see cref="SpdxAnnotation.Type"/>, and <see cref="SpdxAnnotation.Comment"/>.
        ///     Uses <c>HashCode.Combine</c> for a well-distributed composite hash.
        /// </remarks>
        public int GetHashCode(SpdxAnnotation obj)
        {
            return HashCode.Combine(
                obj.Annotator,
                obj.Date,
                obj.Type,
                obj.Comment);
        }
    }
}
