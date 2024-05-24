namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX Package Verification Code
/// </summary>
/// <remarks>
/// A manifest based verification code (the algorithm is defined in section 4.7
/// of the full specification) of the SPDX Item. This allows consumers of this
/// data and/or database to determine if an SPDX item they have in hand is
/// identical to the SPDX item from which the data was produced. This
/// algorithm works even if the SPDX document is included in the SPDX item.
/// </remarks>
public class SpdxPackageVerificationCode
{
    /// <summary>
    /// Excluded Files Field
    /// </summary>
    /// <remarks>
    /// Files that was excluded when calculating the package verification code.
    /// This is usually a file containing SPDX data regarding the package.
    /// If a package contains more than one SPDX file all SPDX files must be
    /// excluded from the package verification code. If this is not done it
    /// would be impossible to correctly calculate the verification codes in
    /// both files.
    /// </remarks>
    public string[] ExcludedFiles { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Verification Code Value Field
    /// </summary>
    /// <remarks>
    /// The actual package verification code as a hex encoded value.
    /// </remarks>
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