namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX Checksum class
/// </summary>
/// <remarks>
/// A Checksum is value that allows the contents of a file to be authenticated.
/// Even small changes to the content of the file will change its checksum.
/// This class allows the results of a variety of checksum and cryptographic
/// message digest algorithms to be represented.
/// </remarks>
public sealed class SpdxChecksum
{
    /// <summary>
    /// Checksum Algorithm Field
    /// </summary>
    /// <remarks>
    /// Identifies the algorithm used to produce the subject Checksum.
    /// </remarks>
    public SpdxChecksumAlgorithm Algorithm { get; set; } = SpdxChecksumAlgorithm.Missing;

    /// <summary>
    /// Checksum Value Field
    /// </summary>
    /// <remarks>
    /// The checksumValue property provides a lower case hexadecimal encoded
    /// digest value produced using a specific algorithm.
    /// </remarks>
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