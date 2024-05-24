namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX External Reference Field
/// </summary>
/// <remarks>
/// An External Reference allows a Package to reference an external source of
/// additional information, metadata, enumerations, asset identifiers, or
/// downloadable content believed to be relevant to the Package.
/// </remarks>
public class SpdxExternalReference
{
    /// <summary>
    /// External Reference Category Field
    /// </summary>
    /// <remarks>
    /// Category for the external reference
    /// </remarks>
    public SpdxReferenceCategory Category { get; set; } = SpdxReferenceCategory.Missing;

    /// <summary>
    /// External Reference Type Field
    /// </summary>
    /// <remarks>
    /// Type of the external reference. These are defined in an appendix in
    /// the SPDX specification.
    /// </remarks>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// External Reference Locator Field
    /// </summary>
    /// <remarks>
    /// The unique string with no spaces necessary to access the
    /// package-specific information, metadata, or content within the target
    /// location. The format of the locator is subject to constraints defined
    /// by the type.
    /// </remarks>
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