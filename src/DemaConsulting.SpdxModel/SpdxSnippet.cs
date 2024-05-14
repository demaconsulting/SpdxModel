namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX Snippet Class
/// </summary>
public class SpdxSnippet : SpdxElement
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
    /// Snippet From File Field
    /// </summary>
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
    public string ConcludedLicense { get; set; } = string.Empty;

    /// <summary>
    /// License Information in Snippet Field
    /// </summary>
    public string[] LicenseInfoInSnippet { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Comments On License Field (optional)
    /// </summary>
    public string? LicenseComments;

    /// <summary>
    /// Copyright Text Field
    /// </summary>
    public string Copyright = string.Empty;

    /// <summary>
    /// Snippet Comment Field (optional)
    /// </summary>
    public string? Comment;

    /// <summary>
    /// Snippet Name Field (optional)
    /// </summary>
    public string? Name;

    /// <summary>
    /// Snippet Attribution Text Field
    /// </summary>
    public string[] AttributionText = Array.Empty<string>();

    /// <summary>
    /// Perform validation of information
    /// </summary>
    /// <param name="issues">List to populate with issues</param>
    public void Validate(List<string> issues)
    {
        // Validate Snippet SPDX Identifier Field
        if (!SpdxId.StartsWith("SPDXRef-"))
            issues.Add("Snippet Invalid SPDX Identifier Field");

        // Validate Snippet From File Field
        if (SnippetFromFile.Length == 0)
            issues.Add($"Snippet {SpdxId} Invalid Snippet From File Field");

        // Validate Snippet Byte Range Start Field
        if (SnippetByteStart < 1)
            issues.Add($"Snippet {SpdxId} Invalid Snippet Byte Range Start Field");

        // Validate Snippet Byte Range End Field
        if (SnippetByteEnd < SnippetByteStart)
            issues.Add($"Snippet {SpdxId} Invalid Snippet Byte Range End Field");

        // Validate Concluded License Field
        if (ConcludedLicense.Length == 0)
            issues.Add($"Snippet {SpdxId} Invalid Concluded License Field");

        // Validate Copyright Text Field
        if (Copyright.Length == 0)
            issues.Add($"Snippet {SpdxId} Invalid Copyright Text Field");
    }
}