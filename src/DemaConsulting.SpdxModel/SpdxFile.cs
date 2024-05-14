namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX File Information Element
/// </summary>
public sealed class SpdxFile : SpdxElement
{
    /// <summary>
    /// Gets or sets the SPDX Identifier Field
    /// </summary>
    public string SpdxId
    {
        get => Id;
        set => Id = value;
    }

    /// <summary>
    /// File Name Field
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// File Types Field
    /// </summary>
    public SpdxFileType[] FileTypes { get; set; } = Array.Empty<SpdxFileType>();

    /// <summary>
    /// File Checksums
    /// </summary>
    public SpdxChecksum[] Checksums { get; set; } = Array.Empty<SpdxChecksum>();

    /// <summary>
    /// Concluded License Field (optional)
    /// </summary>
    public string? LicenseConcluded { get; set; }

    /// <summary>
    /// License Information In File Field
    /// </summary>
    public string[] LicenseInfoInFiles { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Comments On License Field (optional)
    /// </summary>
    public string? LicenseComments { get; set; }

    /// <summary>
    /// Copyright Text Field (optional)
    /// </summary>
    public string? Copyright { get; set; }

    /// <summary>
    /// File Comment Field (optional)
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// File Notice Field (optional)
    /// </summary>
    public string? Notice { get; set; }

    /// <summary>
    /// File Contributors Field
    /// </summary>
    public string[] Contributors { get; set; } = Array.Empty<string>();

    /// <summary>
    /// File Attribution Text Field
    /// </summary>
    public string[] AttributionText { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Annotations
    /// </summary>
    public SpdxAnnotation[] Annotations { get; set; } = Array.Empty<SpdxAnnotation>();

    /// <summary>
    /// Perform validation of information
    /// </summary>
    /// <param name="issues">List to populate with issues</param>
    public void Validate(List<string> issues)
    {
        // Validate File Name Field
        if (!FileName.StartsWith("./"))
            issues.Add($"File {FileName} Invalid File Name Field");

        // Validate File SPDX Identifier Field
        if (!SpdxId.StartsWith("SPDXRef-"))
            issues.Add($"File {FileName} Invalid SPDX Identifier Field");

        // Validate Checksums
        if (Checksums.FirstOrDefault(c => c.Algorithm == SpdxChecksumAlgorithm.Sha1) == null)
            issues.Add($"File {FileName} Invalid Checksum Field (missing SHA1)");
        foreach (var checksum in Checksums)
            checksum.Validate(FileName, issues);

        // Validate Annotations
        foreach (var annotation in Annotations)
            annotation.Validate($"File {FileName}", issues);
    }
}