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
///     SPDX Document class
/// </summary>
/// <remarks>
///     <see cref="SpdxDocument"/> is the root container of the SPDX in-memory object model.
///     It aggregates all packages, files, snippets, relationships, and annotations that
///     together form a complete SPDX Software Bill of Materials. The class is
///     <see langword="sealed"/> to prevent inheritance. All mutable collection and scalar
///     properties are not thread-safe; external synchronization is required when instances
///     are shared across threads.
/// </remarks>
public sealed class SpdxDocument : SpdxElement
{
    /// <summary>
    ///     Regular expression for checking SPDX version fields
    /// </summary>
    /// <remarks>
    ///     The pattern <c>^SPDX-\d+\.\d+$</c> matches valid SPDX version strings such as
    ///     <c>SPDX-2.3</c>. The 100 ms timeout passed to the <see cref="Regex"/> constructor
    ///     guards against ReDoS on untrusted or malformed input.
    /// </remarks>
    private static readonly Regex VersionRegex = new(
        @"^SPDX-\d+\.\d+$",
        RegexOptions.None,
        TimeSpan.FromMilliseconds(100));

    /// <summary>
    ///     Equality comparer for the same document
    /// </summary>
    /// <remarks>
    ///     This considers documents to be the same if they have the same name and
    ///     describe the same root packages (as compared using the
    ///     SpdxPackage.Same equality comparer).
    /// </remarks>
    public static readonly IEqualityComparer<SpdxDocument> Same = new SpdxDocumentSame();

    /// <summary>
    ///     Document Name Field
    /// </summary>
    /// <remarks>
    ///     Human-readable name for this SPDX document as defined in SPDX 2.x §2.4.
    ///     Must be non-empty for a valid document.
    /// </remarks>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     SPDX Version field
    /// </summary>
    /// <remarks>
    ///     Provide a reference number that can be used to understand how to parse
    ///     and interpret the rest of the file. It will enable both future changes
    ///     to the specification and to support backward compatibility. The version
    ///     number consists of a major and minor version indicator. The major field
    ///     will be incremented when incompatible changes between versions are made
    ///     (one or more sections are created, modified or deleted). The minor
    ///     field will be incremented when backwards compatible changes are made.
    /// </remarks>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    ///     Data License field
    /// </summary>
    /// <remarks>
    ///     License expression for dataLicense. See SPDX Annex D for the license
    ///     expression syntax.  Compliance with the SPDX specification includes
    ///     populating the SPDX fields therein with data related to such fields
    ///     ("SPDX-Metadata").
    /// </remarks>
    public string DataLicense { get; set; } = string.Empty;

    /// <summary>
    ///     SPDX Document Namespace Field
    /// </summary>
    /// <remarks>
    ///     A unique URI that globally identifies this SPDX document as defined in SPDX 2.x §2.5.
    ///     Used to qualify element IDs when cross-referencing elements across documents.
    ///     Must be non-empty for a valid document.
    /// </remarks>
    public string DocumentNamespace { get; set; } = string.Empty;

    /// <summary>
    ///     Document Comment Field (optional)
    /// </summary>
    /// <remarks>
    ///     An optional free-text comment about this document. When <see langword="null"/>,
    ///     the field is absent from serialized output. The value is preserved during
    ///     deep-copy and propagated by enhance if absent from this instance.
    /// </remarks>
    public string? Comment { get; set; }

    /// <summary>
    ///     Creation Information
    /// </summary>
    /// <remarks>
    ///     Mandatory metadata describing who created this document and when as defined in SPDX 2.x §2.7–2.9.
    ///     One instance is required per document.
    /// </remarks>
    public SpdxCreationInformation CreationInformation { get; set; } = new();

    /// <summary>
    ///     External Document References
    /// </summary>
    /// <remarks>
    ///     Identify any external SPDX documents referenced within this SPDX
    ///     document.
    /// </remarks>
    public SpdxExternalDocumentReference[] ExternalDocumentReferences { get; set; } =
        [];

    /// <summary>
    ///     Extracted Licensing Information
    /// </summary>
    /// <remarks>
    ///     Non-standard license texts extracted from software described by this document, as defined in
    ///     SPDX 2.x §10. Each entry provides a locally unique license identifier and the full text.
    /// </remarks>
    public SpdxExtractedLicensingInfo[] ExtractedLicensingInfo { get; set; } =
        [];

    /// <summary>
    ///     Annotations
    /// </summary>
    /// <remarks>
    ///     Document-level reviewer or review annotations as defined in SPDX 2.x §12.
    ///     These annotations apply to the document itself rather than to individual elements.
    /// </remarks>
    public SpdxAnnotation[] Annotations { get; set; } = [];

    /// <summary>
    ///     Files
    /// </summary>
    /// <remarks>
    ///     All file elements described in this SPDX document as defined in SPDX 2.x §4.
    /// </remarks>
    public SpdxFile[] Files { get; set; } = [];

    /// <summary>
    ///     Packages
    /// </summary>
    /// <remarks>
    ///     Packages referenced in the SPDX document.
    /// </remarks>
    public SpdxPackage[] Packages { get; set; } = [];

    /// <summary>
    ///     Snippets
    /// </summary>
    /// <remarks>
    ///     All snippet elements described in this SPDX document as defined in SPDX 2.x §5.
    /// </remarks>
    public SpdxSnippet[] Snippets { get; set; } = [];

    /// <summary>
    ///     Relationships
    /// </summary>
    /// <remarks>
    ///     Relationship elements are intentionally excluded from <see cref="GetAllElements"/>
    ///     to avoid them appearing as duplicate elements alongside the source/target elements
    ///     they connect. They are validated separately via the <see cref="Validate"/> method.
    /// </remarks>
    public SpdxRelationship[] Relationships { get; set; } = [];

    /// <summary>
    ///     Document Describes field (optional)
    /// </summary>
    /// <remarks>
    ///     Packages, files and/or Snippets described by this SPDX document.
    /// </remarks>
    public string[] Describes { get; set; } = [];

    /// <summary>
    ///     Make a deep-copy of this object
    /// </summary>
    /// <remarks>
    ///     Produces a fully independent object graph — every nested array and object is
    ///     recursively deep-copied so that mutations to the returned instance have no effect
    ///     on this instance, and vice versa. This method is stateless and does not throw.
    /// </remarks>
    /// <returns>Deep copy of this object</returns>
    public SpdxDocument DeepCopy()
    {
        return new SpdxDocument
        {
            Id = Id,
            Name = Name,
            Version = Version,
            DataLicense = DataLicense,
            DocumentNamespace = DocumentNamespace,
            Comment = Comment,
            CreationInformation = CreationInformation.DeepCopy(),
            ExternalDocumentReferences = [.. ExternalDocumentReferences.Select(r => r.DeepCopy())],
            ExtractedLicensingInfo = [.. ExtractedLicensingInfo.Select(l => l.DeepCopy())],
            Annotations = [.. Annotations.Select(a => a.DeepCopy())],
            Files = [.. Files.Select(f => f.DeepCopy())],
            Packages = [.. Packages.Select(p => p.DeepCopy())],
            Snippets = [.. Snippets.Select(s => s.DeepCopy())],
            Relationships = [.. Relationships.Select(r => r.DeepCopy())],
            Describes = (string[])Describes.Clone()
        };
    }

    /// <summary>
    ///     Perform validation of information
    /// </summary>
    /// <remarks>
    ///     Issues are appended to <paramref name="issues"/> rather than thrown as exceptions,
    ///     enabling a complete diagnostic pass in a single call. The method is not thread-safe
    ///     if the document is mutated concurrently. When <paramref name="ntia"/> is
    ///     <see langword="true"/>, additional NTIA minimum-element checks are performed.
    /// </remarks>
    /// <param name="issues">List to populate with issues</param>
    /// <param name="ntia">Perform NTIA validation</param>
    public void Validate(List<string> issues, bool ntia = false)
    {
        // Validate SPDX Identifier Field
        if (Id != "SPDXRef-DOCUMENT")
        {
            issues.Add($"Document Invalid SPDX Identifier Field '{Id}'");
        }

        // Validate Document Name Field
        if (Name.Length == 0)
        {
            issues.Add("Document Invalid Document Name Field - Empty");
        }

        // Validate SPDX Version Field
        if (!VersionRegex.IsMatch(Version))
        {
            issues.Add($"Document Invalid SPDX Version Field '{Version}'");
        }

        // Validate Data License Field
        if (DataLicense != "CC0-1.0")
        {
            issues.Add($"Document Invalid Data License Field '{DataLicense}'");
        }

        // Validate SPDX Document Namespace Field
        if (DocumentNamespace.Length == 0)
        {
            issues.Add("Document Invalid SPDX Document Namespace Field - Empty");
        }

        // Validate Creation Information
        CreationInformation.Validate(issues);

        // Validate external document references
        foreach (var externalRef in ExternalDocumentReferences)
        {
            externalRef.Validate(issues);
        }

        // Validate extracted licensing info
        foreach (var license in ExtractedLicensingInfo)
        {
            license.Validate(issues);
        }

        // Validate Files
        foreach (var file in Files)
        {
            file.Validate(issues);
        }

        // Validate Packages
        foreach (var package in Packages)
        {
            package.Validate(issues, this, ntia);
        }

        // Validate Snippets
        foreach (var snippet in Snippets)
        {
            snippet.Validate(issues);
        }

        // Validate Relationships
        foreach (var relationship in Relationships)
        {
            relationship.Validate(issues, this);
        }

        // Validate Annotations
        foreach (var annotation in Annotations)
        {
            annotation.Validate("Document", issues);
        }

        // Check for duplicate elements
        var elements = GetAllElements().GroupBy(e => e.Id).Where(g => g.Count() > 1);
        foreach (var element in elements.Where(e => !string.IsNullOrWhiteSpace(e.Key)))
        {
            issues.Add($"Document Duplicate Element ID '{element.Key}'");
        }

        // SPDX NTIA Relationship Check
        if (ntia && GetRootPackages().Length == 0)
        {
            issues.Add("NTIA: Document must describe at least one package");
        }
    }

    /// <summary>
    ///     Get the root packages this document claims to describe
    /// </summary>
    /// <remarks>
    ///     A package qualifies as a root package if its ID appears in the
    ///     <see cref="Describes"/> array, is the target of a <c>DESCRIBES</c> relationship
    ///     from the document element, or is the source of a <c>DESCRIBED_BY</c> relationship
    ///     pointing at the document element. All three mechanisms are checked and the results
    ///     are unioned.
    /// </remarks>
    /// <returns>Array of packages described by this document</returns>
    public SpdxPackage[] GetRootPackages()
    {
        // Get the root packages this document claims to describe (by describes field)
        var packageNames = Describes.ToHashSet();

        // Ensure we have packages this document claims to describe (by relationship)
        packageNames.UnionWith(
            Relationships.Where(r => r.RelationshipType == SpdxRelationshipType.Describes && r.Id == Id)
                .Select(r => r.RelatedSpdxElement));

        // Ensure we have packages claiming to be described by this document (by relationship)
        packageNames.UnionWith(
            Relationships
                .Where(r => r.RelationshipType == SpdxRelationshipType.DescribedBy && r.RelatedSpdxElement == Id)
                .Select(r => r.Id));

        // Return the packages
        return [.. Packages.Where(p => packageNames.Contains(p.Id))];
    }

    /// <summary>
    ///     Get all SPDX elements in the document
    /// </summary>
    /// <remarks>
    ///     <see cref="SpdxRelationship"/> elements are deliberately excluded from the
    ///     returned sequence. Relationships are not SPDX elements in the same sense as
    ///     packages, files, and snippets, and including them would cause them to appear
    ///     alongside the elements they connect during ID-uniqueness checks and other
    ///     traversals.
    /// </remarks>
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
    ///     Get an SPDX element by ID
    /// </summary>
    /// <remarks>
    ///     Returns the first element whose <see cref="SpdxElement.Id"/> matches
    ///     <paramref name="id"/>, or <see langword="null"/> if no matching element exists.
    ///     The search includes the document itself, all files, packages, snippets, and their
    ///     annotations. Relationships are not searched.
    /// </remarks>
    /// <param name="id">Element ID</param>
    /// <returns>SPDX element or null</returns>
    public SpdxElement? GetElement(string id)
    {
        return GetAllElements().FirstOrDefault(e => e.Id == id);
    }

    /// <summary>
    ///     Get an SPDX element of a specific type
    /// </summary>
    /// <remarks>
    ///     Delegates to <see cref="GetElement(string)"/> and casts the result to
    ///     <typeparamref name="T"/>. Returns <see langword="null"/> if no element with
    ///     <paramref name="id"/> exists, or if the element exists but is not of type
    ///     <typeparamref name="T"/>.
    /// </remarks>
    /// <typeparam name="T">SPDX element type</typeparam>
    /// <param name="id">Element ID</param>
    /// <returns>SPDX element or null</returns>
    public T? GetElement<T>(string id) where T : SpdxElement
    {
        return GetElement(id) as T;
    }

    /// <summary>
    ///     Equality Comparer to test for the same document
    /// </summary>
    /// <remarks>
    ///     Two documents are considered the same when their <see cref="SpdxDocument.Name"/>
    ///     fields are equal and their root-package collections (as returned by
    ///     <see cref="SpdxDocument.GetRootPackages"/>) contain the same packages in any order,
    ///     compared using <see cref="SpdxPackage.Same"/>.
    /// </remarks>
    private sealed class SpdxDocumentSame : IEqualityComparer<SpdxDocument>
    {
        /// <inheritdoc />
        public bool Equals(SpdxDocument? d1, SpdxDocument? d2)
        {
            if (ReferenceEquals(d1, d2))
            {
                return true;
            }

            if (d1 == null || d2 == null)
            {
                return false;
            }

            // Ensure the document describes the same root packages
            var p1 = d1.GetRootPackages().OrderBy(p => p.Name);
            var p2 = d2.GetRootPackages().OrderBy(p => p.Name);
            if (!p1.SequenceEqual(p2, SpdxPackage.Same))
            {
                return false;
            }

            return d1.Name == d2.Name;
        }

        /// <inheritdoc />
        public int GetHashCode(SpdxDocument obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
