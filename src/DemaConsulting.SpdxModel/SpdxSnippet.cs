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
///     SPDX Snippet Class
/// </summary>
/// <remarks>
///     Snippets referenced in the SPDX document
/// </remarks>
public sealed class SpdxSnippet : SpdxLicenseElement
{
    /// <summary>
    ///     Equality comparer for the same snippet
    /// </summary>
    /// <remarks>
    ///     This considers snippets as being the same if they share the same
    ///     file and byte range. Note that this does not work across documents
    ///     as the file element IDs are document-specific.
    /// </remarks>
    public static readonly IEqualityComparer<SpdxSnippet> Same = new SpdxSnippetSame();

    /// <summary>
    ///     Snippet From File Field
    /// </summary>
    /// <remarks>
    ///     SPDX ID for File. File containing the SPDX element (e.g. the file containing the snippet).
    /// </remarks>
    public string SnippetFromFile { get; set; } = "";

    /// <summary>
    ///     Snippet Byte Range Start Field
    /// </summary>
    /// <remarks>
    ///     Must be ≥ 1. Validated by <see cref="Validate" />. Used as part of the snippet identity key for array merging.
    /// </remarks>
    public int SnippetByteStart { get; set; }

    /// <summary>
    ///     Snippet Byte Range End Field
    /// </summary>
    /// <remarks>
    ///     Must be ≥ <see cref="SnippetByteStart" />. Validated by <see cref="Validate" />. Used as part of the snippet
    ///     identity key for array merging.
    /// </remarks>
    public int SnippetByteEnd { get; set; }

    /// <summary>
    ///     Snippet Line Range Start Field
    /// </summary>
    /// <remarks>
    ///     <c>0</c> signifies that the line range start is not specified. Not validated by <see cref="Validate" />.
    /// </remarks>
    public int SnippetLineStart { get; set; }

    /// <summary>
    ///     Snippet Line Range End Field
    /// </summary>
    /// <remarks>
    ///     <c>0</c> signifies that the line range end is not specified. Not validated by <see cref="Validate" />.
    /// </remarks>
    public int SnippetLineEnd { get; set; }

    /// <summary>
    ///     License Information in Snippet Field
    /// </summary>
    /// <remarks>
    ///     License expressions. See SPDX Annex D for the license expression syntax.
    ///     Licensing information that was discovered directly in the subject snippet.
    ///     This is also considered a declared license for the snippet.
    ///     If not present, it implies an equivalent meaning to NOASSERTION.
    /// </remarks>
    public string[] LicenseInfoInSnippet { get; set; } = [];

    /// <summary>
    ///     Snippet Comment Field (optional)
    /// </summary>
    /// <remarks>
    ///     Optional free-text comment providing human-readable context for the snippet.
    /// </remarks>
    public string? Comment { get; set; }

    /// <summary>
    ///     Snippet Name Field (optional)
    /// </summary>
    /// <remarks>
    ///     Identify name of this snippet.
    /// </remarks>
    public string? Name { get; set; }

    /// <summary>
    ///     Make a deep-copy of this object
    /// </summary>
    /// <remarks>
    ///     All nested arrays (including <see cref="LicenseInfoInSnippet" />, <see cref="SpdxLicenseElement.AttributionText" />,
    ///     <see cref="SpdxLicenseElement.Annotations" />) are deep-copied, so the caller is free to mutate the result without
    ///     affecting the original.
    /// </remarks>
    /// <returns>Deep copy of this object</returns>
    public SpdxSnippet DeepCopy()
    {
        return new SpdxSnippet
        {
            Id = Id,
            SnippetFromFile = SnippetFromFile,
            SnippetByteStart = SnippetByteStart,
            SnippetByteEnd = SnippetByteEnd,
            SnippetLineStart = SnippetLineStart,
            SnippetLineEnd = SnippetLineEnd,
            ConcludedLicense = ConcludedLicense,
            LicenseInfoInSnippet = [.. LicenseInfoInSnippet],
            LicenseComments = LicenseComments,
            CopyrightText = CopyrightText,
            Comment = Comment,
            Name = Name,
            AttributionText = (string[])AttributionText.Clone(),
            Annotations = [.. Annotations.Select(a => a.DeepCopy())]
        };
    }

    /// <summary>
    ///     Enhance missing fields in the snippet
    /// </summary>
    /// <remarks>
    ///     Field fitness ranking: <c>0</c> or empty values are replaced by non-zero / non-empty values from
    ///     <paramref name="other" />. Array fields such as <see cref="LicenseInfoInSnippet" /> are merged by deduplication.
    /// </remarks>
    /// <param name="other">Other snippet to enhance with</param>
    public void Enhance(SpdxSnippet other)
    {
        // Enhance the license information
        EnhanceLicenseElement(other);

        // Populate the snippet-from-file field if missing
        SnippetFromFile = SpdxHelpers.EnhanceString(SnippetFromFile, other.SnippetFromFile) ?? "";

        // Populate the snippet-byte-start field if missing
        if (SnippetByteStart <= 0)
        {
            SnippetByteStart = other.SnippetByteStart;
        }

        // Populate the snippet-byte-end field if missing
        if (SnippetByteEnd <= 0)
        {
            SnippetByteEnd = other.SnippetByteEnd;
        }

        // Populate the snippet-line-start field if missing
        if (SnippetLineStart <= 0)
        {
            SnippetLineStart = other.SnippetLineStart;
        }

        // Populate the snippet-line-end field if missing
        if (SnippetLineEnd <= 0)
        {
            SnippetLineEnd = other.SnippetLineEnd;
        }

        // Merge the license-info-in-snippet entries
        LicenseInfoInSnippet = [.. LicenseInfoInSnippet.Concat(other.LicenseInfoInSnippet).Distinct()];

        // Populate the comment field if missing
        Comment = SpdxHelpers.EnhanceString(Comment, other.Comment);

        // Populate the name field if missing
        Name = SpdxHelpers.EnhanceString(Name, other.Name);
    }

    /// <summary>
    ///     Enhance missing snippets in array
    /// </summary>
    /// <remarks>
    ///     Snippets are matched using the <see cref="Same" /> comparer (by <see cref="SnippetFromFile" />, byte start, and
    ///     byte end). Matching snippets are enhanced in place; non-matching snippets from <paramref name="others" /> are
    ///     deep-copied and appended.
    /// </remarks>
    /// <param name="array">Array to enhance</param>
    /// <param name="others">Other array to enhance with</param>
    /// <returns>Updated array</returns>
    public static SpdxSnippet[] Enhance(SpdxSnippet[] array, SpdxSnippet[] others)
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
    ///     Validates snippet ID, <see cref="SnippetFromFile" />, byte range (<see cref="SnippetByteStart" /> ≥ 1,
    ///     <see cref="SnippetByteEnd" /> ≥ <see cref="SnippetByteStart" />), <see cref="SpdxLicenseElement.ConcludedLicense" />,
    ///     <see cref="SpdxLicenseElement.CopyrightText" />, and annotations. Missing required fields, invalid byte ranges,
    ///     malformed IDs, and invalid annotations are recorded in <paramref name="issues" />.
    /// </remarks>
    /// <param name="issues">List to populate with issues</param>
    public void Validate(List<string> issues)
    {
        // Validate Snippet SPDX Identifier Field
        if (!SpdxRefRegex.IsMatch(Id))
        {
            issues.Add($"Snippet Invalid SPDX Identifier Field '{Id}'");
        }

        // Validate Snippet From File Field
        if (SnippetFromFile.Length == 0)
        {
            issues.Add($"Snippet '{Id}' Invalid Snippet From File Field - Empty");
        }

        // Validate Snippet Byte Range Start Field
        if (SnippetByteStart < 1)
        {
            issues.Add($"Snippet '{Id}' Invalid Snippet Byte Range Start Field '{SnippetByteStart}'");
        }

        // Validate Snippet Byte Range End Field
        if (SnippetByteEnd < SnippetByteStart)
        {
            issues.Add($"Snippet '{Id}' Invalid Snippet Byte Range End Field '{SnippetByteEnd}' < '{SnippetByteStart}'");
        }

        // Validate Concluded License Field
        if (ConcludedLicense.Length == 0)
        {
            issues.Add($"Snippet '{Id}' Invalid Concluded License Field - Empty");
        }

        // Validate Copyright Text Field
        if (CopyrightText.Length == 0)
        {
            issues.Add($"Snippet '{Id}' Invalid Copyright Text Field - Empty");
        }

        // Validate Annotations
        foreach (var annotation in Annotations)
        {
            annotation.Validate($"Snippet '{Id}'", issues);
        }
    }

    /// <summary>
    ///     Equality Comparer to test for the same snippet
    /// </summary>
    /// <remarks>
    ///     Two snippets are considered the same when they share the same <see cref="SnippetFromFile" />,
    ///     <see cref="SnippetByteStart" />, and <see cref="SnippetByteEnd" />. This is the backing implementation
    ///     for <see cref="SpdxSnippet.Same" />. A dedicated nested class is used rather than an ad-hoc lambda so the
    ///     comparer instance can be stored as a field and passed to LINQ operations without boxing or allocation.
    /// </remarks>
    private sealed class SpdxSnippetSame : IEqualityComparer<SpdxSnippet>
    {
        /// <inheritdoc />
        public bool Equals(SpdxSnippet? s1, SpdxSnippet? s2)
        {
            if (ReferenceEquals(s1, s2))
            {
                return true;
            }

            if (s1 == null || s2 == null)
            {
                return false;
            }

            return s1.SnippetFromFile == s2.SnippetFromFile &&
                   s1.SnippetByteStart == s2.SnippetByteStart &&
                   s1.SnippetByteEnd == s2.SnippetByteEnd;
        }

        /// <inheritdoc />
        public int GetHashCode(SpdxSnippet obj)
        {
            return HashCode.Combine(
                obj.SnippetFromFile,
                obj.SnippetByteStart,
                obj.SnippetByteEnd);
        }
    }
}
