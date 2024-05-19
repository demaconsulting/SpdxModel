using System.Text.RegularExpressions;

namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX Creation Information
/// </summary>
public sealed class SpdxCreationInformation
{
    /// <summary>
    /// Creator Field
    /// </summary>
    public string[] Creators { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Created Field
    /// </summary>
    public string Created { get; set; } = string.Empty;

    /// <summary>
    /// Creator Comment Field (optional)
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// License List Version Field (optional)
    /// </summary>
    public string? LicenseListVersion { get; set; }

    /// <summary>
    /// Perform validation of information
    /// </summary>
    /// <param name="issues">List to populate with issues</param>
    public void Validate(List<string> issues)
    {
        // Validate Creator Field
        if (Creators.Length == 0)
            issues.Add("Document Invalid Creator Field");

        // Validate Created Field
        if (!Regex.IsMatch(Created, @"\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}Z"))
            issues.Add("Document Invalid Created Field");

        // Validate License List Version Field
        if (LicenseListVersion != null && !Regex.IsMatch(LicenseListVersion, @"\d+\.\d+"))
            issues.Add("Document Invalid License List Version Field");
    }
}