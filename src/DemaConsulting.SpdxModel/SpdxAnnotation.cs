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
///     SPDX Annotation class
/// </summary>
/// <remarks>
///     An Annotation is a comment on an SpdxItem by an agent.
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
    ///     Annotator Field (optional)
    /// </summary>
    /// <remarks>
    ///     This field identifies the person, organization, or tool that has
    ///     commented on a file, package, snippet, or the entire document.
    /// </remarks>
    public string Annotator { get; set; } = "";

    /// <summary>
    ///     Annotation Date Field (optional)
    /// </summary>
    /// <remarks>
    ///     Identify when the comment was made. This is to be specified according
    ///     to the combined date and time in the UTC format, as specified in the
    ///     ISO 8601 standard.
    /// </remarks>
    public string Date { get; set; } = "";

    /// <summary>
    ///     Annotation Type Field (optional)
    /// </summary>
    public SpdxAnnotationType Type { get; set; } = SpdxAnnotationType.Missing;

    /// <summary>
    ///     Annotation Comment field (optional)
    /// </summary>
    public string Comment { get; set; } = "";

    /// <summary>
    ///     Make a deep-copy of this object
    /// </summary>
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
    /// <param name="parent">Associated parent node</param>
    /// <param name="issues">List to populate with issues</param>
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
    private sealed class SpdxAnnotationSame : IEqualityComparer<SpdxAnnotation>
    {
        /// <inheritdoc />
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
