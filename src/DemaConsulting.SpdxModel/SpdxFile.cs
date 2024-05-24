namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX File Information Element
/// </summary>
public sealed class SpdxFile : SpdxElement
{
    /// <summary>
    /// File Name Field
    /// </summary>
    /// <remarks>
    /// The name of the file relative to the root of the package.
    /// </remarks>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// File Types Field
    /// </summary>
    /// <remarks>
    /// The type of the file.
    /// </remarks>
    public SpdxFileType[] FileTypes { get; set; } = Array.Empty<SpdxFileType>();

    /// <summary>
    /// File Checksums
    /// </summary>
    /// <remarks>
    /// The checksum property provides a mechanism that can be used to verify
    /// that the contents of a file have not changed.
    /// </remarks>
    public SpdxChecksum[] Checksums { get; set; } = Array.Empty<SpdxChecksum>();

    /// <summary>
    /// Concluded License Field (optional)
    /// </summary>
    /// <remarks>
    /// License expression. See SPDX Annex D for the license expression syntax.
    /// 
    /// The licensing that the preparer of this SPDX document has concluded,
    /// based on the evidence, actually applies to the SPDX Item.
    ///
    /// If not present, it implies an equivalent meaning to NOASSERTION.
    /// </remarks>
    public string? LicenseConcluded { get; set; }

    /// <summary>
    /// License Information In File Field
    /// </summary>
    /// <remarks>
    /// License expressions. See SPDX Annex D for the license expression syntax.
    /// 
    /// Licensing information that was discovered directly in the subject file.
    /// This is also considered a declared license for the file.
    ///
    /// If not present for a file, it implies an equivalent meaning to NOASSERTION.
    /// </remarks>
    public string[] LicenseInfoInFiles { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Comments On License Field (optional)
    /// </summary>
    /// <remarks>
    /// This property allows the preparer of the SPDX document to describe why
    /// the LicenseConcluded was chosen.
    /// </remarks>
    public string? LicenseComments { get; set; }

    /// <summary>
    /// Copyright Text Field (optional)
    /// </summary>
    /// <remarks>
    /// The text of copyright declarations recited in the file.
    ///
    /// If not present, it implies an equivalent meaning to NOASSERTION.
    /// </remarks>
    public string? Copyright { get; set; }

    /// <summary>
    /// File Comment Field (optional)
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// File Notice Field (optional)
    /// </summary>
    /// <remarks>
    /// This field provides a place for the SPDX file creator to record
    /// potential legal notices found in the file. This may or may not
    /// include copyright statements.
    /// </remarks>
    public string? Notice { get; set; }

    /// <summary>
    /// File Contributors Field
    /// </summary>
    /// <remarks>
    /// This field provides a place for the SPDX file creator to record file
    /// contributors. Contributors could include names of copyright holders
    /// and/or authors who may not be copyright holders yet contributed to the
    /// file content.
    /// </remarks>
    public string[] Contributors { get; set; } = Array.Empty<string>();

    /// <summary>
    /// File Attribution Text Field
    /// </summary>
    /// <remarks>
    /// This field provides a place for the SPDX data creator to record
    /// acknowledgements that may be required to be communicated in some
    /// contexts.
    /// </remarks>
    public string[] AttributionText { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Annotations
    /// </summary>
    /// <remarks>
    /// Provide additional information about this file.
    /// </remarks>
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
        if (!Id.StartsWith("SPDXRef-"))
            issues.Add($"File {FileName} Invalid SPDX Identifier Field");

        // Validate Checksums
        if (!Array.Exists(Checksums, c => c.Algorithm == SpdxChecksumAlgorithm.Sha1))
            issues.Add($"File {FileName} Invalid Checksum Field (missing SHA1)");
        foreach (var checksum in Checksums)
            checksum.Validate(FileName, issues);

        // Validate Annotations
        foreach (var annotation in Annotations)
            annotation.Validate($"File {FileName}", issues);
    }
}