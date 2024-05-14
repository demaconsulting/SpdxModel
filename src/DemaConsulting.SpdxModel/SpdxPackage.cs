namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX Package class
/// </summary>
public class SpdxPackage : SpdxElement
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
    /// Package Name Field
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Package Version Field (optional)
    /// </summary>
    public string? Version { get; set; }

    /// <summary>
    /// Package File Name Field (optional)
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// Package Supplier Field (optional)
    /// </summary>
    public string? Supplier { get; set; }

    /// <summary>
    /// Package Originator Field (optional)
    /// </summary>
    public string? Originator { get; set; }

    /// <summary>
    /// Package Download Location Field
    /// </summary>
    public string DownloadLocation { get; set; } = string.Empty;

    /// <summary>
    /// Files Analyzed Field (optional)
    /// </summary>
    public bool? FilesAnalyzed;

    /// <summary>
    /// Has Files (optional)
    /// </summary>
    public string[] HasFiles { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Package Verification Code (optional)
    /// </summary>
    public SpdxPackageVerificationCode? VerificationCode { get; set; }

    /// <summary>
    /// Package Checksum Field (optional)
    /// </summary>
    public SpdxChecksum[] Checksums { get; set; } = Array.Empty<SpdxChecksum>();

    /// <summary>
    /// Package Home Page Field (optional)
    /// </summary>
    public string? HomePage { get; set; }

    /// <summary>
    /// Source Information Field (optional)
    /// </summary>
    public string? SourceInformation { get; set; }

    /// <summary>
    /// Concluded License Field (optional)
    /// </summary>
    public string? ConcludedLicense { get; set; }

    /// <summary>
    /// All Licenses Information From Files Field
    /// </summary>
    public string[] LicenseInfoFromFiles { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Declared License Field
    /// </summary>
    public string DeclaredLicense { get; set; } = string.Empty;

    /// <summary>
    /// Comments On License Field (optional)
    /// </summary>
    public string? LicenseComments { get; set; }

    /// <summary>
    /// Copyright Text Field (optional)
    /// </summary>
    public string? CopyrightText { get; set; }

    /// <summary>
    /// Package Summary Description Field (optional)
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// Package Detailed Description Field (optional)
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Package Comment Field (optional)
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// External References Field (optional)
    /// </summary>
    public SpdxExternalReference[] ExternalReferences { get; set; } = Array.Empty<SpdxExternalReference>();

    /// <summary>
    /// Package Attribution Text Field
    /// </summary>
    public string[] Attributions { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Perform validation of information
    /// </summary>
    /// <param name="issues">List to populate with issues</param>
    public void Validate(List<string> issues)
    {
        // Validate Package Name Field
        if (Name.Length == 0)
            issues.Add("Package Invalid Package Name Field");

        // Validate Package Download Location Field
        if (DownloadLocation.Length == 0)
            issues.Add($"Package {Name} Invalid Package Download Location Field");

        // Validate verification code
        VerificationCode?.Validate(Name, issues);

        // Validate checksums
        foreach (var checksum in Checksums)
            checksum.Validate($"Package {Name}", issues);

        // Validate external references
        foreach (var externalReference in ExternalReferences)
            externalReference.Validate(Name, issues);
    }
}