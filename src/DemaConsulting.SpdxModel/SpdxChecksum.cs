namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX Checksum class
/// </summary>
public sealed class SpdxChecksum
{
    /// <summary>
    /// Checksum Algorithm Field
    /// </summary>
    public SpdxChecksumAlgorithm Algorithm { get; set; } = SpdxChecksumAlgorithm.Missing;

    /// <summary>
    /// Checksum Value Field
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Perform validation of information
    /// </summary>
    /// <param name="fileName">Associated file name</param>
    /// <param name="issues">List to populate with issues</param>
    public void Validate(string fileName, List<string> issues)
    {
        // Validate Algorithm Field
        if (Algorithm == SpdxChecksumAlgorithm.Missing)
            issues.Add($"File {fileName} Invalid Checksum Algorithm Field");

        // Validate Checksum Value Field
        if (Value.Length == 0)
            issues.Add($"File {fileName} Invalid Checksum Value Field");
    }
}