namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX Package Verification Code
/// </summary>
public class SpdxPackageVerificationCode
{
    /// <summary>
    /// Excluded Files Field
    /// </summary>
    public string[] ExcludedFiles { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Verification Code Value Field
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Perform validation of information
    /// </summary>
    /// <param name="package">Associated package</param>
    /// <param name="issues">List to populate with issues</param>
    public void Validate(string package, List<string> issues)
    {
        // Validate Package Verification Code Value Field
        if (Value.Length != 40)
            issues.Add($"Package {package} Invalid Package Verification Code Value");
    }
}