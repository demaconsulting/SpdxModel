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
    /// Equality comparer for the same checksum
    /// </summary>
    /// <remarks>
    /// This considers checksums as being the same if they have the same
    /// algorithm and value.
    /// </remarks>
    public static readonly IEqualityComparer<SpdxChecksum> Same = new SpdxChecksumSame();

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
    /// Make a deep-copy of this object
    /// </summary>
    /// <returns>Deep copy of this object</returns>
    public SpdxChecksum DeepCopy() =>
        new()
        {
            Algorithm = Algorithm,
            Value = Value
        };

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

    /// <summary>
    /// Equality Comparer to test for the same relationship
    /// </summary>
    private class SpdxChecksumSame : IEqualityComparer<SpdxChecksum>
    {
        /// <inheritdoc />
        public bool Equals(SpdxChecksum? c1, SpdxChecksum? c2)
        {
            if (ReferenceEquals(c1, c2)) return true;
            if (c1 == null || c2 == null) return false;

            return c1.Algorithm == c2.Algorithm &&
                   c1.Value == c2.Value;
        }

        /// <inheritdoc />
        public int GetHashCode(SpdxChecksum obj)
        {
            return obj.Algorithm.GetHashCode() ^ obj.Value.GetHashCode();
        }
    }
}