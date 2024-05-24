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
    /// License Identifier Field (optional)
    /// </summary>
    /// <remarks>
    /// A human-readable short form license identifier for a license.
    /// </remarks>
    public string? LicenseId { get; set; }

    /// <summary>
    /// Extracted Text Field
    /// </summary>
    /// <remarks>
    /// Provide a copy of the actual text of the license reference extracted
    /// from the package, file or snippet that is associated with the License
    /// Identifier to aid in future analysis.
    /// </remarks>
    public string? ExtractedText { get; set; }

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
}