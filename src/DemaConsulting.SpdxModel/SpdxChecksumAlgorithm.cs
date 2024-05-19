﻿namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX Checksum Algorithm enumeration
/// </summary>
public enum SpdxChecksumAlgorithm
{
    /// <summary>
    /// Missing checksum algorithm
    /// </summary>
    Missing = -1,

    /// <summary>
    /// SHA-1 checksum algorithm
    /// </summary>
    Sha1,

    /// <summary>
    /// SHA-224 checksum algorithm
    /// </summary>
    Sha224,

    /// <summary>
    /// SHA-256 checksum algorithm
    /// </summary>
    Sha256,

    /// <summary>
    /// SHA-384 checksum algorithm
    /// </summary>
    Sha384,

    /// <summary>
    /// SHA-512 checksum algorithm
    /// </summary>
    Sha512,

    /// <summary>
    /// MD2 checksum algorithm
    /// </summary>
    Md2,

    /// <summary>
    /// MD4 checksum algorithm
    /// </summary>
    Md4,

    /// <summary>
    /// MD5 checksum algorithm
    /// </summary>
    Md5,

    /// <summary>
    /// MD6 checksum algorithm
    /// </summary>
    Md6,

    /// <summary>
    /// SHA3-256 checksum algorithm
    /// </summary>
    Sha3256,

    /// <summary>
    /// SHA3-384 checksum algorithm
    /// </summary>
    Sha3384,

    /// <summary>
    /// SHA3-512 checksum algorithm
    /// </summary>
    Sha3512,

    /// <summary>
    /// BLAKE2b-256 checksum algorithm
    /// </summary>
    Blake2B256,

    /// <summary>
    /// BLAKE2b-384 checksum algorithm
    /// </summary>
    Blake2B384,

    /// <summary>
    /// BLAKE2b-512 checksum algorithm
    /// </summary>
    Blake2B512,

    /// <summary>
    /// BLAKE3 checksum algorithm
    /// </summary>
    Blake3,

    /// <summary>
    /// ADLER32 checksum algorithm
    /// </summary>
    Adler32
}

/// <summary>
/// SPDX Checksum Algorithm Extensions
/// </summary>
public static class SpdxChecksumAlgorithmExtensions
{
    /// <summary>
    /// Convert text to SpdxChecksumAlgorithm
    /// </summary>
    /// <param name="checksumAlgorithm">Checksum algorithm text</param>
    /// <returns>SpdxChecksumAlgorithm</returns>
    /// <exception cref="InvalidOperationException">on error</exception>
    public static SpdxChecksumAlgorithm FromText(string checksumAlgorithm)
    {
        return checksumAlgorithm switch
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
            "BLAKE2b-256" => SpdxChecksumAlgorithm.Blake2B256,
            "BLAKE2b-384" => SpdxChecksumAlgorithm.Blake2B384,
            "BLAKE2b-512" => SpdxChecksumAlgorithm.Blake2B512,
            "BLAKE3" => SpdxChecksumAlgorithm.Blake3,
            "ADLER32" => SpdxChecksumAlgorithm.Adler32,
            _ => throw new InvalidOperationException($"Unsupported SPDX Checksum Algorithm {checksumAlgorithm}")
        };
    }

    /// <summary>
    /// Convert SpdxChecksumAlgorithm to text
    /// </summary>
    /// <param name="checksumAlgorithm">SpdxChecksumAlgorithm</param>
    /// <returns>Checksum Algorithm text</returns>
    /// <exception cref="InvalidOperationException">on error</exception>
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
            _ => throw new InvalidOperationException($"Unsupported SPDX Checksum Algorithm {checksumAlgorithm}")
        };
    }
}