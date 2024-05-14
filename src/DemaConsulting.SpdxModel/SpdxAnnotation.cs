using System.Text.RegularExpressions;

namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX Annotation class
/// </summary>
public sealed class SpdxAnnotation : SpdxElement
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
    /// Annotator Field (optional)
    /// </summary>
    public string Annotator { get; set; } = string.Empty;

    /// <summary>
    /// Annotation Date Field (optional)
    /// </summary>
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