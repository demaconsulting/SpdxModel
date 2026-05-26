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
///     SPDX Checksum Algorithm enumeration
/// </summary>
/// <remarks>
///     The <see cref="Missing"/> sentinel value (-1) indicates that no algorithm has been
///     assigned. It is used as a default/uninitialised state and is not a valid SPDX
///     algorithm name. All other members correspond to named SPDX algorithm identifiers.
/// </remarks>
public enum SpdxChecksumAlgorithm
{
    /// <summary>
    ///     Missing checksum algorithm
    /// </summary>
    /// <remarks>
    ///     Sentinel value indicating that no algorithm has been assigned. This value is not
    ///     a valid SPDX checksum algorithm and must not be serialized to SPDX text form.
    ///     It is used as the default state before an algorithm is explicitly set.
    /// </remarks>
    Missing = -1,

    /// <summary>
    ///     SHA-1 checksum algorithm
    /// </summary>
    /// <remarks>Corresponds to the SPDX algorithm identifier <c>SHA1</c>.</remarks>
    Sha1,

    /// <summary>
    ///     SHA-224 checksum algorithm
    /// </summary>
    /// <remarks>Corresponds to the SPDX algorithm identifier <c>SHA224</c>.</remarks>
    Sha224,

    /// <summary>
    ///     SHA-256 checksum algorithm
    /// </summary>
    /// <remarks>Corresponds to the SPDX algorithm identifier <c>SHA256</c>.</remarks>
    Sha256,

    /// <summary>
    ///     SHA-384 checksum algorithm
    /// </summary>
    /// <remarks>Corresponds to the SPDX algorithm identifier <c>SHA384</c>.</remarks>
    Sha384,

    /// <summary>
    ///     SHA-512 checksum algorithm
    /// </summary>
    /// <remarks>Corresponds to the SPDX algorithm identifier <c>SHA512</c>.</remarks>
    Sha512,

    /// <summary>
    ///     MD2 checksum algorithm
    /// </summary>
    /// <remarks>Corresponds to the SPDX algorithm identifier <c>MD2</c>.</remarks>
    Md2,

    /// <summary>
    ///     MD4 checksum algorithm
    /// </summary>
    /// <remarks>Corresponds to the SPDX algorithm identifier <c>MD4</c>.</remarks>
    Md4,

    /// <summary>
    ///     MD5 checksum algorithm
    /// </summary>
    /// <remarks>Corresponds to the SPDX algorithm identifier <c>MD5</c>.</remarks>
    Md5,

    /// <summary>
    ///     MD6 checksum algorithm
    /// </summary>
    /// <remarks>Corresponds to the SPDX algorithm identifier <c>MD6</c>.</remarks>
    Md6,

    /// <summary>
    ///     SHA3-256 checksum algorithm
    /// </summary>
    /// <remarks>Corresponds to the SPDX algorithm identifier <c>SHA3-256</c>.</remarks>
    Sha3256,

    /// <summary>
    ///     SHA3-384 checksum algorithm
    /// </summary>
    /// <remarks>Corresponds to the SPDX algorithm identifier <c>SHA3-384</c>.</remarks>
    Sha3384,

    /// <summary>
    ///     SHA3-512 checksum algorithm
    /// </summary>
    /// <remarks>Corresponds to the SPDX algorithm identifier <c>SHA3-512</c>.</remarks>
    Sha3512,

    /// <summary>
    ///     BLAKE2b-256 checksum algorithm
    /// </summary>
    /// <remarks>Corresponds to the SPDX algorithm identifier <c>BLAKE2b-256</c>.</remarks>
    Blake2B256,

    /// <summary>
    ///     BLAKE2b-384 checksum algorithm
    /// </summary>
    /// <remarks>Corresponds to the SPDX algorithm identifier <c>BLAKE2b-384</c>.</remarks>
    Blake2B384,

    /// <summary>
    ///     BLAKE2b-512 checksum algorithm
    /// </summary>
    /// <remarks>Corresponds to the SPDX algorithm identifier <c>BLAKE2b-512</c>.</remarks>
    Blake2B512,

    /// <summary>
    ///     BLAKE3 checksum algorithm
    /// </summary>
    /// <remarks>Corresponds to the SPDX algorithm identifier <c>BLAKE3</c>.</remarks>
    Blake3,

    /// <summary>
    ///     ADLER32 checksum algorithm
    /// </summary>
    /// <remarks>Corresponds to the SPDX algorithm identifier <c>ADLER32</c>.</remarks>
    Adler32
}

/// <summary>
///     SPDX Checksum Algorithm Extensions
/// </summary>
/// <remarks>
///     Provides the <see cref="FromText"/> factory method and the <see cref="ToText"/>
///     extension method to convert between SPDX algorithm text strings and the
///     <see cref="SpdxChecksumAlgorithm"/> enumeration. Use <see cref="FromText"/> when
///     parsing SPDX JSON/tag-value input and <see cref="ToText"/> when serializing back
///     to SPDX text form.
/// </remarks>
public static class SpdxChecksumAlgorithmExtensions
{
    /// <summary>
    ///     Convert text to SpdxChecksumAlgorithm
    /// </summary>
    /// <remarks>
    ///     The input string is converted to upper-case via <c>ToUpperInvariant()</c> before
    ///     comparison, so the lookup is case-insensitive and locale-independent. An empty
    ///     string maps to <see cref="SpdxChecksumAlgorithm.Missing"/> rather than throwing;
    ///     any other unrecognized value throws <see cref="InvalidOperationException"/>.
    /// </remarks>
    /// <param name="checksumAlgorithm">Checksum algorithm text</param>
    /// <returns>SpdxChecksumAlgorithm</returns>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when <paramref name="checksumAlgorithm"/> is a non-empty string that does not match any
    ///     known SPDX checksum algorithm name (case-insensitive comparison is used before the exception is raised).
    /// </exception>
    public static SpdxChecksumAlgorithm FromText(string checksumAlgorithm)
    {
        return checksumAlgorithm.ToUpperInvariant() switch
        {
            "" => SpdxChecksumAlgorithm.Missing,
            "SHA1" => SpdxChecksumAlgorithm.Sha1,
            "SHA224" => SpdxChecksumAlgorithm.Sha224,
            "SHA256" => SpdxChecksumAlgorithm.Sha256,
            "SHA384" => SpdxChecksumAlgorithm.Sha384,
            "SHA512" => SpdxChecksumAlgorithm.Sha512,
            "MD2" => SpdxChecksumAlgorithm.Md2,
            "MD4" => SpdxChecksumAlgorithm.Md4,
            "MD5" => SpdxChecksumAlgorithm.Md5,
            "MD6" => SpdxChecksumAlgorithm.Md6,
            "SHA3-256" => SpdxChecksumAlgorithm.Sha3256,
            "SHA3-384" => SpdxChecksumAlgorithm.Sha3384,
            "SHA3-512" => SpdxChecksumAlgorithm.Sha3512,
            "BLAKE2B-256" => SpdxChecksumAlgorithm.Blake2B256,
            "BLAKE2B-384" => SpdxChecksumAlgorithm.Blake2B384,
            "BLAKE2B-512" => SpdxChecksumAlgorithm.Blake2B512,
            "BLAKE3" => SpdxChecksumAlgorithm.Blake3,
            "ADLER32" => SpdxChecksumAlgorithm.Adler32,
            _ => throw new InvalidOperationException($"Unsupported SPDX Checksum Algorithm '{checksumAlgorithm}'")
        };
    }

    /// <summary>
    ///     Convert SpdxChecksumAlgorithm to text
    /// </summary>
    /// <remarks>
    ///     <see cref="SpdxChecksumAlgorithm.Missing"/> is intentionally rejected rather than
    ///     round-tripping to an empty string: serializing a checksum without an algorithm
    ///     would produce invalid SPDX output, so the caller must ensure only valid, named
    ///     algorithms are passed to this method.
    /// </remarks>
    /// <param name="checksumAlgorithm">SpdxChecksumAlgorithm</param>
    /// <returns>Checksum Algorithm text</returns>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when <paramref name="checksumAlgorithm"/> is <see cref="SpdxChecksumAlgorithm.Missing"/> or
    ///     is a numeric value that is not a named member of the <see cref="SpdxChecksumAlgorithm"/> enumeration.
    /// </exception>
    public static string ToText(this SpdxChecksumAlgorithm checksumAlgorithm)
    {
        return checksumAlgorithm switch
        {
            SpdxChecksumAlgorithm.Missing => throw new InvalidOperationException(
                "Attempt to serialize missing SPDX Checksum Algorithm"),
            SpdxChecksumAlgorithm.Sha1 => "SHA1",
            SpdxChecksumAlgorithm.Sha224 => "SHA224",
            SpdxChecksumAlgorithm.Sha256 => "SHA256",
            SpdxChecksumAlgorithm.Sha384 => "SHA384",
            SpdxChecksumAlgorithm.Sha512 => "SHA512",
            SpdxChecksumAlgorithm.Md2 => "MD2",
            SpdxChecksumAlgorithm.Md4 => "MD4",
            SpdxChecksumAlgorithm.Md5 => "MD5",
            SpdxChecksumAlgorithm.Md6 => "MD6",
            SpdxChecksumAlgorithm.Sha3256 => "SHA3-256",
            SpdxChecksumAlgorithm.Sha3384 => "SHA3-384",
            SpdxChecksumAlgorithm.Sha3512 => "SHA3-512",
            SpdxChecksumAlgorithm.Blake2B256 => "BLAKE2b-256",
            SpdxChecksumAlgorithm.Blake2B384 => "BLAKE2b-384",
            SpdxChecksumAlgorithm.Blake2B512 => "BLAKE2b-512",
            SpdxChecksumAlgorithm.Blake3 => "BLAKE3",
            SpdxChecksumAlgorithm.Adler32 => "ADLER32",
            _ => throw new InvalidOperationException($"Unsupported SPDX Checksum Algorithm '{checksumAlgorithm}'")
        };
    }
}
