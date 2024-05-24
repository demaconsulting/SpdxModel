﻿using System.Text.RegularExpressions;

namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX Document class
/// </summary>
public sealed class SpdxDocument : SpdxElement
{
    /// <summary>
    /// Document Name Field
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// SPDX Version field
    /// </summary>
    /// <remarks>
    /// Provide a reference number that can be used to understand how to parse
    /// and interpret the rest of the file. It will enable both future changes
    /// to the specification and to support backward compatibility. The version
    /// number consists of a major and minor version indicator. The major field
    /// will be incremented when incompatible changes between versions are made
    /// (one or more sections are created, modified or deleted). The minor
    /// field will be incremented when backwards compatible changes are made.
    /// </remarks>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Data License field
    /// </summary>
    /// <remarks>
    /// License expression for dataLicense. See SPDX Annex D for the license
    /// expression syntax.  Compliance with the SPDX specification includes
    /// populating the SPDX fields therein with data related to such fields
    /// ("SPDX-Metadata").
    /// </remarks>
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
    /// <remarks>
    /// Identify any external SPDX documents referenced within this SPDX
    /// document.
    /// </remarks>
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
    /// <remarks>
    /// Packages referenced in the SPDX document.
    /// </remarks>
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
    /// <remarks>
    /// Packages, files and/or Snippets described by this SPDX document.
    /// </remarks>
    public string[] Describes { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Perform validation of information
    /// </summary>
    /// <param name="issues">List to populate with issues</param>
    public void Validate(List<string> issues)
    {
        // Validate SPDX Identifier Field
        if (Id != "SPDXRef-DOCUMENT")
            issues.Add("Document Invalid SPDX Identifier Field");

        // Validate Document Name Field
        if (Name.Length == 0)
            issues.Add("Document Invalid Document Name Field");

        // Validate SPDX Version Field
        if (!Regex.IsMatch(Version, @"SPDX-\d+\.\d+"))
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

    /// <summary>
    /// Get the root packages this document claims to describe
    /// </summary>
    /// <returns>Array of packages described by this document</returns>
    public SpdxPackage[] GetRootPackages()
    {
        // Get the root packages this document claims to describe (by describes field)
        var packageNames = Describes.ToHashSet();

        // Ensure we have packages this document claims to describe (by relationship)
        packageNames.UnionWith(
            Relationships.Where(r => r.RelationshipType == SpdxRelationshipType.Describes && r.Id == Id).Select(r => r.RelatedSpdxElement));

        // Ensure we have packages claiming to be described by this document (by relationship)
        packageNames.UnionWith(
            Relationships.Where(r => r.RelationshipType == SpdxRelationshipType.DescribedBy && r.RelatedSpdxElement == Id).Select(r => r.Id));

        // Return the packages
        return Packages.Where(p => packageNames.Contains(p.Id)).ToArray();
    }
}