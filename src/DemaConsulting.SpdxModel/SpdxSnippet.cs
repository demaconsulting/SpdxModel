﻿namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX Snippet Class
/// </summary>
/// <remarks>
/// Snippets referenced in the SPDX document
/// </remarks>
public class SpdxSnippet : SpdxElement
{
    /// <summary>
    /// Equality comparer for the same snippet
    /// </summary>
    /// <remarks>
    /// This considers snippets as being the same if they share the same
    /// file and byte range. Note that this does not work across documents
    /// as the file element IDs are document-specific.
    /// </remarks>
    public static readonly IEqualityComparer<SpdxSnippet> Same = new SpdxSnippetSame();

    /// <summary>
    /// Snippet From File Field
    /// </summary>
    /// <remarks>
    /// SPDX ID for File. File containing the SPDX element (e.g. the file containing the snippet).
    /// </remarks>
    public string SnippetFromFile { get; set; } = string.Empty;

    /// <summary>
    /// Snippet Byte Range Start Field
    /// </summary>
    public int SnippetByteStart { get; set; }

    /// <summary>
    /// Snippet Byte Range End Field
    /// </summary>
    public int SnippetByteEnd { get; set; }

    /// <summary>
    /// Snippet Line Range Start Field
    /// </summary>
    public int SnippetLineStart { get; set; }

    /// <summary>
    /// Snippet Line Range End Field
    /// </summary>
    public int SnippetLineEnd { get; set; }

    /// <summary>
    /// Concluded License Field
    /// </summary>
    /// <remarks>
    /// License expression. See SPDX Annex D for the license expression syntax.
    ///
    /// The licensing that the preparer of this SPDX document has concluded,
    /// based on the evidence, actually applies to the snippet.
    ///
    /// If not present, it implies an equivalent meaning to NOASSERTION.
    /// </remarks>
    public string ConcludedLicense { get; set; } = string.Empty;

    /// <summary>
    /// License Information in Snippet Field
    /// </summary>
    /// <remarks>
    /// License expressions. See SPDX Annex D for the license expression syntax.
    /// 
    /// Licensing information that was discovered directly in the subject snippet.
    /// This is also considered a declared license for the snippet.
    ///
    /// If not present, it implies an equivalent meaning to NOASSERTION.
    /// </remarks>
    public string[] LicenseInfoInSnippet { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Comments On License Field (optional)
    /// </summary>
    /// <remarks>
    /// This property allows the preparer of the SPDX document to describe why
    /// the ConcludedLicense was chosen.
    /// </remarks>
    public string? LicenseComments { get; set; }

    /// <summary>
    /// Copyright Text Field
    /// </summary>
    /// <remarks>
    /// The text of copyright declarations recited in the snippet
    /// 
    /// If not present, it implies an equivalent meaning to NOASSERTION.
    /// </remarks>
    public string Copyright { get; set; } = string.Empty;

    /// <summary>
    /// Snippet Comment Field (optional)
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Snippet Name Field (optional)
    /// </summary>
    /// <remarks>
    /// Identify name of this snippet.
    /// </remarks>
    public string? Name { get; set; }

    /// <summary>
    /// Snippet Attribution Text Field
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
    /// Provide additional information about this snippet.
    /// </remarks>
    public SpdxAnnotation[] Annotations { get; set; } = Array.Empty<SpdxAnnotation>();

    /// <summary>
    /// Make a deep-copy of this object
    /// </summary>
    /// <returns>Deep copy of this object</returns>
    public SpdxSnippet DeepCopy() =>
        new()
        {
            Id = Id,
            SnippetFromFile = SnippetFromFile,
            SnippetByteStart = SnippetByteStart,
            SnippetByteEnd = SnippetByteEnd,
            SnippetLineStart = SnippetLineStart,
            SnippetLineEnd = SnippetLineEnd,
            ConcludedLicense = ConcludedLicense,
            LicenseInfoInSnippet = LicenseInfoInSnippet,
            LicenseComments = LicenseComments,
            Copyright = Copyright,
            Comment = Comment,
            Name = Name,
            AttributionText = AttributionText.ToArray(),
            Annotations = Annotations.Select(a => a.DeepCopy()).ToArray()
        };

    /// <summary>
    /// Perform validation of information
    /// </summary>
    /// <param name="issues">List to populate with issues</param>
    public void Validate(List<string> issues)
    {
        // Validate Snippet SPDX Identifier Field
        if (!Id.StartsWith("SPDXRef-"))
            issues.Add("Snippet Invalid SPDX Identifier Field");

        // Validate Snippet From File Field
        if (SnippetFromFile.Length == 0)
            issues.Add($"Snippet {Id} Invalid Snippet From File Field");

        // Validate Snippet Byte Range Start Field
        if (SnippetByteStart < 1)
            issues.Add($"Snippet {Id} Invalid Snippet Byte Range Start Field");

        // Validate Snippet Byte Range End Field
        if (SnippetByteEnd < SnippetByteStart)
            issues.Add($"Snippet {Id} Invalid Snippet Byte Range End Field");

        // Validate Concluded License Field
        if (ConcludedLicense.Length == 0)
            issues.Add($"Snippet {Id} Invalid Concluded License Field");

        // Validate Copyright Text Field
        if (Copyright.Length == 0)
            issues.Add($"Snippet {Id} Invalid Copyright Text Field");
    }

    /// <summary>
    /// Equality Comparer to test for the same snippet
    /// </summary>
    private class SpdxSnippetSame : IEqualityComparer<SpdxSnippet>
    {
        /// <inheritdoc />
        public bool Equals(SpdxSnippet? s1, SpdxSnippet? s2)
        {
            if (ReferenceEquals(s1, s2)) return true;
            if (s1 == null || s2 == null) return false;

            return s1.SnippetFromFile == s2.SnippetFromFile &&
                   s1.SnippetByteStart == s2.SnippetByteStart &&
                   s1.SnippetByteEnd == s2.SnippetByteEnd;
        }

        /// <inheritdoc />
        public int GetHashCode(SpdxSnippet obj)
        {
            return obj.SnippetFromFile.GetHashCode() ^
                   obj.SnippetByteStart.GetHashCode() ^
                   obj.SnippetByteEnd.GetHashCode();
        }
    }
}