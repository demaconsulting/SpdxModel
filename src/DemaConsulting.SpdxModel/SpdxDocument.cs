using System.Text.RegularExpressions;

namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX Document class
/// </summary>
public sealed class SpdxDocument : SpdxElement
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
    /// Document Name Field
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// SPDX Version field
    /// </summary>
    public string SpdxVersion { get; set; } = string.Empty;

    /// <summary>
    /// Data License field
    /// </summary>
    public string DataLicense { get; set; } = string.Empty;

    /// <summary>
    /// SPDX Document Namespace Field
    /// </summary>
    public string DocumentNamespace { get; set; } = string.Empty;

    /// <summary>
    /// Document Comment Field (optional)
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Creation Information
    /// </summary>
    public SpdxCreationInformation CreationInformation { get; set; } = new();

    /// <summary>
    /// External Document References
    /// </summary>
    public SpdxExternalDocumentReference[] ExternalDocumentReferences { get; set; } =
        Array.Empty<SpdxExternalDocumentReference>();

    /// <summary>
    /// Extracted Licensing Information
    /// </summary>
    public SpdxExtractedLicensingInfo[] ExtractedLicensingInfo { get; set; } =
        Array.Empty<SpdxExtractedLicensingInfo>();

    /// <summary>
    /// Annotations
    /// </summary>
    public SpdxAnnotation[] Annotations { get; set; } = Array.Empty<SpdxAnnotation>();

    /// <summary>
    /// Files
    /// </summary>
    public SpdxFile[] Files { get; set; } = Array.Empty<SpdxFile>();

    /// <summary>
    /// Packages
    /// </summary>
    public SpdxPackage[] Packages { get; set; } = Array.Empty<SpdxPackage>();

    /// <summary>
    /// Snippets
    /// </summary>
    public SpdxSnippet[] Snippets { get; set; } = Array.Empty<SpdxSnippet>();

    /// <summary>
    /// Relationships
    /// </summary>
    public SpdxRelationship[] Relationships { get; set; } = Array.Empty<SpdxRelationship>();

    /// <summary>
    /// Document Describes field (optional)
    /// </summary>
    public string[] Describes { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Perform validation of information
    /// </summary>
    /// <param name="issues">List to populate with issues</param>
    public void Validate(List<string> issues)
    {
        // Validate SPDX Identifier Field
        if (SpdxId != "SPDXRef-DOCUMENT")
            issues.Add("Document Invalid SPDX Identifier Field");

        // Validate Document Name Field
        if (Name.Length == 0)
            issues.Add("Document Invalid Document Name Field");

        // Validate SPDX Version Field
        if (!Regex.IsMatch(SpdxVersion, @"SPDX-\d+\.\d+"))
            issues.Add("Document Invalid SPDX Version Field");

        // Validate Data License Field
        if (DataLicense != "CC0-1.0")
            issues.Add("Document Invalid Data License Field");

        // Validate SPDX Document Namespace Field
        if (DocumentNamespace.Length == 0)
            issues.Add("Document Invalid SPDX Document Namespace Field");

        // Validate Creation Information
        CreationInformation.Validate(issues);

        // Validate external document references
        foreach (var externalRef in ExternalDocumentReferences)
            externalRef.Validate(issues);

        // Validate Files
        foreach (var file in Files)
            file.Validate(issues);

        // Validate Packages
        foreach (var package in Packages)
            package.Validate(issues);

        // Validate Snippets
        foreach (var snippet in Snippets)
            snippet.Validate(issues);

        // Validate Relationships
        foreach (var relationship in Relationships)
            relationship.Validate(issues);
    }
}