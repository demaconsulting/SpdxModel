// Copyright(c) 2024 DEMA Consulting
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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
    public string Value { get; set; } = "";

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
    /// Enhance missing fields in the checksum
    /// </summary>
    /// <param name="other">Other checksum to enhance with</param>
    public void Enhance(SpdxChecksum other)
    {
        // Populate the algorithm if missing
        if (Algorithm == SpdxChecksumAlgorithm.Missing)
            Algorithm = other.Algorithm;

        // Populate the value if missing
        Value = SpdxHelpers.EnhanceString(Value, other.Value) ?? "";
    }

    /// <summary>
    /// Enhance missing checksums in array
    /// </summary>
    /// <param name="array">Array to enhance</param>
    /// <param name="others">Other array to enhance with</param>
    /// <returns>Updated array</returns>
    public static SpdxChecksum[] Enhance(SpdxChecksum[] array, SpdxChecksum[] others)
    {
        // Convert to list
        var list = array.ToList();

        // Iterate over other array
        foreach (var other in others)
        {
            // Check if other item is the same as one we have
            var annotation = list.Find(a => Same.Equals(a, other));
            if (annotation != null)
            {
                // Enhance our item with the other information
                annotation.Enhance(other);
            }
            else
            {
                // Add the new item to our list
                list.Add(other.DeepCopy());
            }
        }

        // Return as array
        return [..list];
    }

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
    private sealed class SpdxChecksumSame : IEqualityComparer<SpdxChecksum>
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
            return HashCode.Combine(obj.Algorithm, obj.Value);
        }
    }
}