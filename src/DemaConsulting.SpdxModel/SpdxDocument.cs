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

using System.Text.RegularExpressions;

namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX Document class
/// </summary>
public sealed class SpdxDocument : SpdxElement
{
    /// <summary>
    /// Equality comparer for the same document
    /// </summary>
    /// <remarks>
    /// This considers documents to be the same if they have the same name and
    /// describe the same root packages (as compared using the
    /// SpdxPackage.Same equality comparer).
    /// </remarks>
    public static readonly IEqualityComparer<SpdxDocument> Same = new SpdxDocumentSame();

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
    /// Make a deep-copy of this object
    /// </summary>
    /// <returns>Deep copy of this object</returns>
    public SpdxDocument DeepCopy() =>
        new()
        {
            Id = Id,
            Name = Name,
            Version = Version,
            DataLicense = DataLicense,
            DocumentNamespace = DocumentNamespace,
            Comment = Comment,
            CreationInformation = CreationInformation.DeepCopy(),
            ExternalDocumentReferences = ExternalDocumentReferences.Select(r => r.DeepCopy()).ToArray(),
            ExtractedLicensingInfo = ExtractedLicensingInfo.Select(l => l.DeepCopy()).ToArray(),
            Annotations = Annotations.Select(a => a.DeepCopy()).ToArray(),
            Files = Files.Select(f => f.DeepCopy()).ToArray(),
            Packages = Packages.Select(p => p.DeepCopy()).ToArray(),
            Snippets = Snippets.Select(s => s.DeepCopy()).ToArray(),
            Relationships = Relationships.Select(r => r.DeepCopy()).ToArray(),
            Describes = Describes.ToArray()
        };

    /// <summary>
    /// Perform validation of information
    /// </summary>
    /// <param name="issues">List to populate with issues</param>
    /// <param name="ntia">Perform NTIA validation</param>
    public void Validate(List<string> issues, bool ntia = false)
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

        // Validate extracted licensing info
        foreach (var license in ExtractedLicensingInfo)
            license.Validate(issues);

        // Validate Files
        foreach (var file in Files)
            file.Validate(issues);

        // Validate Packages
        foreach (var package in Packages)
            package.Validate(issues, this, ntia);

        // Validate Snippets
        foreach (var snippet in Snippets)
            snippet.Validate(issues);

        // Validate Relationships
        foreach (var relationship in Relationships)
            relationship.Validate(issues, this);

        // Check for duplicate elements
        var elements = GetAllElements().GroupBy(e => e.Id).Where(g => g.Count() > 1);
        foreach (var element in elements.Where(e => !string.IsNullOrWhiteSpace(e.Key)))
            issues.Add($"Document Duplicate Element ID: {element.Key}");

        // SPDX NTIA Relationship Check
        if (ntia && GetRootPackages().Length == 0)
            issues.Add("NTIA: Document must describe at least one package");
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

    /// <summary>
    /// Get all SPDX elements in the document
    /// </summary>
    /// <returns>Enumerable of all elements</returns>
    public IEnumerable<SpdxElement> GetAllElements()
    {
        // Note: This excludes SpdxRelationship elements as these would appear as duplicates.
        return Enumerable.Empty<SpdxElement>()
            .Append(this)
            .Concat(Annotations)
            .Concat(Files)
            .Concat(Packages)
            .Concat(Snippets)
            .Concat(Files.SelectMany(f => f.Annotations))
            .Concat(Packages.SelectMany(p => p.Annotations))
            .Concat(Snippets.SelectMany(s => s.Annotations));
    }

    /// <summary>
    /// Get an SPDX element by ID
    /// </summary>
    /// <param name="id">Element ID</param>
    /// <returns>SPDX element or null</returns>
    public SpdxElement? GetElement(string id)
    {
        return GetAllElements().FirstOrDefault(e => e.Id == id);
    }

    /// <summary>
    /// Get an SPDX element of a specific type
    /// </summary>
    /// <typeparam name="T">SPDX element type</typeparam>
    /// <param name="id">Element ID</param>
    /// <returns>SPDX element or null</returns>
    public T? GetElement<T>(string id) where T : SpdxElement
    {
        return GetElement(id) as T;
    }

    /// <summary>
    /// Equality Comparer to test for the same relationship
    /// </summary>
    private sealed class SpdxDocumentSame : IEqualityComparer<SpdxDocument>
    {
        /// <inheritdoc />
        public bool Equals(SpdxDocument? d1, SpdxDocument? d2)
        {
            if (ReferenceEquals(d1, d2)) return true;
            if (d1 == null || d2 == null) return false;

            // Ensure the document describes the same root packages
            var p1 = d1.GetRootPackages().OrderBy(p => p.Name);
            var p2 = d2.GetRootPackages().OrderBy(p => p.Name);
            if (!p1.SequenceEqual(p2, SpdxPackage.Same))
                return false;

            return d1.Name == d2.Name;
        }

        /// <inheritdoc />
        public int GetHashCode(SpdxDocument obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}