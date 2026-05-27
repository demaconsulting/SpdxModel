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
///     SPDX Extracted Licensing Information class
/// </summary>
/// <remarks>
///     Represents a license or licensing notice that was found in a package,
///     file or snippet. Any license text that is recognized as a license may be
///     represented as a License rather than an ExtractedLicensingInfo.
/// </remarks>
public sealed class SpdxExtractedLicensingInfo
{
    /// <summary>
    ///     Equality comparer for the same extracted licensing info
    /// </summary>
    /// <remarks>
    ///     This considers extracted licensing infos as being the same if they have the same extracted text.
    /// </remarks>
    public static readonly IEqualityComparer<SpdxExtractedLicensingInfo> Same = new SpdxExtractedLicensingInfoSame();

    /// <summary>
    ///     License Identifier Field
    /// </summary>
    /// <remarks>
    ///     A human-readable short form license identifier for a license.
    /// </remarks>
    public string LicenseId { get; set; } = "";

    /// <summary>
    ///     Extracted Text Field
    /// </summary>
    /// <remarks>
    ///     Provide a copy of the actual text of the license reference extracted
    ///     from the package, file or snippet that is associated with the License
    ///     Identifier to aid in future analysis.
    /// </remarks>
    public string ExtractedText { get; set; } = "";

    /// <summary>
    ///     License Name Field
    /// </summary>
    /// <remarks>
    ///     Null when no name is present.
    /// </remarks>
    public string? Name { get; set; }

    /// <summary>
    ///     License Cross-Reference Field (optional)
    /// </summary>
    /// <remarks>
    ///     An empty array when no cross-references are present.
    /// </remarks>
    public string[] CrossReferences { get; set; } = [];

    /// <summary>
    ///     License Comment Field (optional)
    /// </summary>
    /// <remarks>
    ///     Null when no comment is present. An empty string is not used.
    /// </remarks>
    public string? Comment { get; set; }

    /// <summary>
    ///     Make a deep-copy of this object
    /// </summary>
    /// <remarks>
    ///     Used by the static Enhance merge to add new entries without aliasing the source array; also used by callers
    ///     that need an independent snapshot.
    /// </remarks>
    /// <returns>Deep copy of this object</returns>
    public SpdxExtractedLicensingInfo DeepCopy()
    {
        return new SpdxExtractedLicensingInfo
        {
            LicenseId = LicenseId,
            ExtractedText = ExtractedText,
            Name = Name,
            CrossReferences = (string[])CrossReferences.Clone(),
            Comment = Comment
        };
    }

    /// <summary>
    ///     Enhance missing fields in the extracted licensing info
    /// </summary>
    /// <remarks>
    ///     Populates LicenseId, ExtractedText, Name, and Comment using fitness-based selection.
    ///     CrossReferences are merged by concatenation and deduplication.
    /// </remarks>
    /// <param name="other">Other extracted licensing info to enhance with</param>
    public void Enhance(SpdxExtractedLicensingInfo other)
    {
        // Populate the license-id field if missing
        LicenseId = SpdxHelpers.EnhanceString(LicenseId, other.LicenseId) ?? "";

        // Populate the extracted-text field if missing
        ExtractedText = SpdxHelpers.EnhanceString(ExtractedText, other.ExtractedText) ?? "";

        // Populate the name field if missing
        Name = SpdxHelpers.EnhanceString(Name, other.Name);

        // Merge the cross-references
        CrossReferences = [.. CrossReferences.Concat(other.CrossReferences).Distinct()];

        // Populate the comment field if missing
        Comment = SpdxHelpers.EnhanceString(Comment, other.Comment);
    }

    /// <summary>
    ///     Enhance missing extracted licensing info in array
    /// </summary>
    /// <remarks>
    ///     Matches existing entries by ExtractedText (via the Same comparer) and enhances them;
    ///     entries with no match are appended as deep copies.
    /// </remarks>
    /// <param name="array">Array to enhance</param>
    /// <param name="others">Other array to enhance with</param>
    /// <returns>Updated array</returns>
    public static SpdxExtractedLicensingInfo[] Enhance(SpdxExtractedLicensingInfo[] array,
        SpdxExtractedLicensingInfo[] others)
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
    ///     Validates that LicenseId is non-empty and ExtractedText is non-empty.
    ///     Issues are appended to <paramref name="issues"/>; no exceptions are thrown.
    /// </remarks>
    /// <param name="issues">List to populate with issues</param>
    public void Validate(List<string> issues)
    {
        // Validate Extracted License ID Field
        if (LicenseId.Length == 0)
        {
            issues.Add("Extracted License Information Invalid License ID Field - Empty");
        }

        // Validate Extracted Text Field
        if (ExtractedText.Length == 0)
        {
            issues.Add($"Extracted License Information '{LicenseId}' Invalid Extracted Text Field - Empty");
        }
    }

    /// <summary>
    ///     Equality Comparer to test for the same extracted licensing info
    /// </summary>
    /// <remarks>
    ///     Instantiated once and held in the <see cref="Same"/> field. Comparison is solely by
    ///     <see cref="SpdxExtractedLicensingInfo.ExtractedText"/>; other fields such as
    ///     <see cref="SpdxExtractedLicensingInfo.LicenseId"/> and
    ///     <see cref="SpdxExtractedLicensingInfo.Comment"/> are intentionally excluded so that
    ///     two entries carrying the same license text but different metadata are still recognized
    ///     as the same entry during merge operations.
    /// </remarks>
    private sealed class SpdxExtractedLicensingInfoSame : IEqualityComparer<SpdxExtractedLicensingInfo>
    {
        /// <inheritdoc />
        /// <remarks>
        ///     Evaluation order: reference equality is checked first (returns <c>true</c>
        ///     immediately), then null-safety (either null returns <c>false</c>), then
        ///     <see cref="SpdxExtractedLicensingInfo.ExtractedText"/> string equality.
        /// </remarks>
        public bool Equals(SpdxExtractedLicensingInfo? l1, SpdxExtractedLicensingInfo? l2)
        {
            if (ReferenceEquals(l1, l2))
            {
                return true;
            }

            if (l1 == null || l2 == null)
            {
                return false;
            }

            return l1.ExtractedText == l2.ExtractedText;
        }

        /// <inheritdoc />
        /// <remarks>
        ///     The hash code is derived solely from <see cref="SpdxExtractedLicensingInfo.ExtractedText"/>.
        /// </remarks>
        public int GetHashCode(SpdxExtractedLicensingInfo obj)
        {
            return obj.ExtractedText.GetHashCode();
        }
    }
}
