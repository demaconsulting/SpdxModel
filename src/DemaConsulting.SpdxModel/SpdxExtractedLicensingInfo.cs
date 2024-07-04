namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX Extracted Licensing Information class
/// </summary>
/// <remarks>
/// Represents a license or licensing notice that was found in a package,
/// file or snippet. Any license text that is recognized as a license may be
/// represented as a License rather than an ExtractedLicensingInfo.
/// </remarks>
public sealed class SpdxExtractedLicensingInfo
{
    /// <summary>
    /// Equality comparer for the same extracted licensing info
    /// </summary>
    /// <remarks>
    /// This considers packages as being the same if they have the same
    /// extracted text.
    /// </remarks>
    public static readonly IEqualityComparer<SpdxExtractedLicensingInfo> Same = new SpdxExtractedLicensingInfoSame();

    /// <summary>
    /// License Identifier Field (optional)
    /// </summary>
    /// <remarks>
    /// A human-readable short form license identifier for a license.
    /// </remarks>
    public string LicenseId { get; set; } = string.Empty;

    /// <summary>
    /// Extracted Text Field
    /// </summary>
    /// <remarks>
    /// Provide a copy of the actual text of the license reference extracted
    /// from the package, file or snippet that is associated with the License
    /// Identifier to aid in future analysis.
    /// </remarks>
    public string ExtractedText { get; set; } = string.Empty;

    /// <summary>
    /// License Name Field
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// License Cross-Reference Field (optional)
    /// </summary>
    public string[] CrossReferences { get; set; } = Array.Empty<string>();

    /// <summary>
    /// License Comment Field (optional)
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Make a deep-copy of this object
    /// </summary>
    /// <returns>Deep copy of this object</returns>
    public SpdxExtractedLicensingInfo DeepCopy() =>
        new()
        {
            LicenseId = LicenseId,
            ExtractedText = ExtractedText,
            Name = Name,
            CrossReferences = CrossReferences.ToArray(),
            Comment = Comment
        };

    /// <summary>
    /// Enhance missing fields in the extracted licensing info
    /// </summary>
    /// <param name="other">Other extracted licensing info to enhance with</param>
    public void Enhance(SpdxExtractedLicensingInfo other)
    {
        // Populate the license-id field if missing
        if (string.IsNullOrWhiteSpace(LicenseId))
            LicenseId = other.LicenseId;

        // Populate the extracted-text field if missing
        if (string.IsNullOrWhiteSpace(ExtractedText))
            ExtractedText = other.ExtractedText;

        // Populate the name field if missing
        if (string.IsNullOrWhiteSpace(Name))
            Name = other.Name;

        // Merge the cross-references
        CrossReferences = CrossReferences.Concat(other.CrossReferences).Distinct().ToArray();

        // Populate the comment field if missing
        if (string.IsNullOrWhiteSpace(Comment))
            Comment = other.Comment;
    }

    /// <summary>
    /// Enhance missing extracted licensing info in array
    /// </summary>
    /// <param name="array">Array to enhance</param>
    /// <param name="others">Other array to enhance with</param>
    /// <returns>Updated array</returns>
    public static SpdxExtractedLicensingInfo[] Enhance(SpdxExtractedLicensingInfo[] array, SpdxExtractedLicensingInfo[] others)
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
    public void Validate(List<string> issues)
    {
        // Validate Extracted License ID ID Field
        if (LicenseId.Length == 0)
            issues.Add("Extracted License Information Invalid License ID Field");

        // Validate Extracted Text Field
        if (ExtractedText.Length == 0)
            issues.Add($"Extracted License Information {LicenseId} Invalid Extracted Text Field");
    }

    /// <summary>
    /// Equality Comparer to test for the same external document reference
    /// </summary>
    private sealed class SpdxExtractedLicensingInfoSame : IEqualityComparer<SpdxExtractedLicensingInfo>
    {
        /// <inheritdoc />
        public bool Equals(SpdxExtractedLicensingInfo? l1, SpdxExtractedLicensingInfo? l2)
        {
            if (ReferenceEquals(l1, l2)) return true;
            if (l1 == null || l2 == null) return false;

            return l1.ExtractedText == l2.ExtractedText;
        }

        /// <inheritdoc />
        public int GetHashCode(SpdxExtractedLicensingInfo obj)
        {
            return obj.ExtractedText.GetHashCode();
        }
    }
}