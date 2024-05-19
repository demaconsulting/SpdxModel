namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX External Reference Field
/// </summary>
public class SpdxExternalReference
{
    /// <summary>
    /// External Reference Category Field
    /// </summary>
    public SpdxReferenceCategory Category { get; set; } = SpdxReferenceCategory.Missing;

    /// <summary>
    /// External Reference Type Field
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// External Reference Locator Field
    /// </summary>
    public string Locator { get; set; } = string.Empty;

    /// <summary>
    /// External Reference Comment Field (optional)
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Perform validation of information
    /// </summary>
    /// <param name="package">Package name</param>
    /// <param name="issues">List to populate with issues</param>
    public void Validate(string package, List<string> issues)
    {
        // Validate External Reference Category Field
        if (Category == SpdxReferenceCategory.Missing)
            issues.Add($"Package {package} Invalid External Reference Category Field");

        // Validate External Reference Type Field
        if (Type.Length == 0)
            issues.Add($"Package {package} Invalid External Reference Type Field");

        // Validate External Reference Locator Field
        if (Locator.Length == 0)
            issues.Add($"Package {package} Invalid External Reference Locator Field");
    }
}