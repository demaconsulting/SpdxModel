namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX Extracted Licensing Information class
/// </summary>
/// <remarks>
/// Represents a license or licensing notice that was found in a package,
/// file or snippet. Any license text that is recognized as a license may be
/// represented as a License rather than an ExtractedLicensingInfo.
/// </remarks>
public class SpdxExtractedLicensingInfo
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
    private class SpdxExtractedLicensingInfoSame : IEqualityComparer<SpdxExtractedLicensingInfo>
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