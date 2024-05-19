namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX Extracted Licensing Information class
/// </summary>
public class SpdxExtractedLicensingInfo
{
    /// <summary>
    /// License Identifier Field (optional)
    /// </summary>
    public string? LicenseId { get; set; }

    /// <summary>
    /// Extracted Text Field
    /// </summary>
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