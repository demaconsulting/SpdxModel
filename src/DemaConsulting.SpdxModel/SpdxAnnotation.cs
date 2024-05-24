using System.Text.RegularExpressions;

namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX Annotation class
/// </summary>
/// <remarks>
/// An Annotation is a comment on an SpdxItem by an agent.
/// </remarks>
public sealed class SpdxAnnotation : SpdxElement
{
    /// <summary>
    /// Annotator Field (optional)
    /// </summary>
    /// <remarks>
    /// This field identifies the person, organization, or tool that has
    /// commented on a file, package, snippet, or the entire document.
    /// </remarks>
    public string Annotator { get; set; } = string.Empty;

    /// <summary>
    /// Annotation Date Field (optional)
    /// </summary>
    /// <remarks>
    /// Identify when the comment was made. This is to be specified according
    /// to the combined date and time in the UTC format, as specified in the
    /// ISO 8601 standard.
    /// </remarks>
    public string Date { get; set; } = string.Empty;

    /// <summary>
    /// Annotation Type Field (optional)
    /// </summary>
    public SpdxAnnotationType Type { get; set; } = SpdxAnnotationType.Missing;

    /// <summary>
    /// Annotation Comment field (optional)
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Perform validation of information
    /// </summary>
    /// <param name="parent">Associated parent node</param>
    /// <param name="issues">List to populate with issues</param>
    public void Validate(string parent, List<string> issues)
    {
        // Validate Annotator Field
        if (Annotator.Length == 0)
            issues.Add($"{parent} Invalid Annotator Field");

        // Validate Annotation Date Field
        if (!Regex.IsMatch(Date, @"\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}Z"))
            issues.Add($"{parent} Invalid Annotation Date Field");

        // Validate Annotation Type Field
        if (Type == SpdxAnnotationType.Missing)
            issues.Add($"{parent} Invalid Annotation Type Field");
    }
}