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

namespace DemaConsulting.SpdxModel.Tests;

/// <summary>
///     Tests for the <see cref="SpdxChecksum" /> class.
/// </summary>
[TestClass]
public class SpdxChecksumTests
{
    /// <summary>
    ///     Tests the <see cref="SpdxChecksum.Same" /> comparer compares checksums correctly.
    /// </summary>
    [TestMethod]
    public void SpdxChecksum_SameComparer_ComparesCorrectly()
    {
        var c1 = new SpdxChecksum
        {
            Algorithm = SpdxChecksumAlgorithm.Sha1,
            Value = "c2b4e1c67a2d28fced849ee1bb76e7391b93f125"
        };

        var c2 = new SpdxChecksum
        {
            Algorithm = SpdxChecksumAlgorithm.Sha1,
            Value = "c2b4e1c67a2d28fced849ee1bb76e7391b93f125"
        };

        var c3 = new SpdxChecksum
        {
            Algorithm = SpdxChecksumAlgorithm.Md5,
            Value = "624c1abb3664f4b35547e7c73864ad24"
        };

        // Assert checksums compare to themselves
        Assert.IsTrue(SpdxChecksum.Same.Equals(c1, c1));
        Assert.IsTrue(SpdxChecksum.Same.Equals(c2, c2));
        Assert.IsTrue(SpdxChecksum.Same.Equals(c3, c3));

        // Assert checksums compare correctly
        Assert.IsTrue(SpdxChecksum.Same.Equals(c1, c2));
        Assert.IsTrue(SpdxChecksum.Same.Equals(c2, c1));
        Assert.IsFalse(SpdxChecksum.Same.Equals(c1, c3));
        Assert.IsFalse(SpdxChecksum.Same.Equals(c3, c1));
        Assert.IsFalse(SpdxChecksum.Same.Equals(c2, c3));
        Assert.IsFalse(SpdxChecksum.Same.Equals(c3, c2));

        // Assert same checksums have identical hashes
        Assert.AreEqual(SpdxChecksum.Same.GetHashCode(c1), SpdxChecksum.Same.GetHashCode(c2));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxChecksum.DeepCopy" /> method successfully creates a deep copy.
    /// </summary>
    [TestMethod]
    public void SpdxChecksum_DeepCopy_CreatesEqualButDistinctInstance()
    {
        // Arrange: Create a checksum instance
        var c1 = new SpdxChecksum
        {
            Algorithm = SpdxChecksumAlgorithm.Sha1,
            Value = "c2b4e1c67a2d28fced849ee1bb76e7391b93f125"
        };

        // Act: Create a deep copy of the original checksum
        var c2 = c1.DeepCopy();

        // Assert: Verify deep-copy is equal to original
        Assert.AreEqual(c1, c2, SpdxChecksum.Same);
        Assert.AreEqual(c1.Algorithm, c2.Algorithm);
        Assert.AreEqual(c1.Value, c2.Value);

        // Assert: Verify deep-copy has distinct instance
        Assert.IsFalse(ReferenceEquals(c1, c2));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxChecksum.Enhance(SpdxChecksum[], SpdxChecksum[])" /> method adds or updates information
    ///     correctly.
    /// </summary>
    [TestMethod]
    public void SpdxChecksum_Enhance_AddsOrUpdatesInformationCorrectly()
    {
        // Arrange: Create an original checksum
        var checksums = new[]
        {
            new SpdxChecksum
            {
                Algorithm = SpdxChecksumAlgorithm.Sha1,
                Value = "c2b4e1c67a2d28fced849ee1bb76e7391b93f125"
            }
        };

        // Act: Enhance with additional checksums
        checksums = SpdxChecksum.Enhance(
            checksums,
            [
                new SpdxChecksum
                {
                    Algorithm = SpdxChecksumAlgorithm.Sha1,
                    Value = "c2b4e1c67a2d28fced849ee1bb76e7391b93f125"
                },
                new SpdxChecksum
                {
                    Algorithm = SpdxChecksumAlgorithm.Md5,
                    Value = "624c1abb3664f4b35547e7c73864ad24"
                }
            ]);

        // Assert: Verify checksums contain the expected values
        Assert.HasCount(2, checksums);
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, checksums[0].Algorithm);
        Assert.AreEqual("c2b4e1c67a2d28fced849ee1bb76e7391b93f125", checksums[0].Value);
        Assert.AreEqual(SpdxChecksumAlgorithm.Md5, checksums[1].Algorithm);
        Assert.AreEqual("624c1abb3664f4b35547e7c73864ad24", checksums[1].Value);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxChecksum.Validate" /> method reports bad algorithms.
    /// </summary>
    [TestMethod]
    public void SpdxChecksum_Validate_InvalidAlgorithm()
    {
        // Arrange: Create a bad instance
        var checksum = new SpdxChecksum
        {
            Algorithm = SpdxChecksumAlgorithm.Missing,
            Value = "c2b4e1c67a2d28fced849ee1bb76e7391b93f125"
        };

        // Act: Perform validation on the SpdxChecksum instance.
        var issues = new List<string>();
        checksum.Validate("Test", issues);

        // Assert: Verify that the validation fails and the error message includes the description
        Assert.IsTrue(
            issues.Any(issue => issue.Contains("Test Invalid Checksum Algorithm Field - Missing")));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxChecksum.Validate" /> method reports bad values.
    /// </summary>
    [TestMethod]
    public void SpdxChecksum_Validate_InvalidValue()
    {
        // Arrange: Create a bad instance
        var checksum = new SpdxChecksum
        {
            Algorithm = SpdxChecksumAlgorithm.Sha1,
            Value = ""
        };

        // Act: Perform validation on the SpdxChecksum instance.
        var issues = new List<string>();
        checksum.Validate("Test", issues);

        // Assert: Verify that the validation fails and the error message includes the description
        Assert.IsTrue(
            issues.Any(issue => issue.Contains("Test Invalid Checksum Value Field - Empty")));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxChecksumAlgorithmExtensions.FromText(string)" /> method.
    /// </summary>
    [TestMethod]
    public void SpdxChecksumAlgorithmExtensions_FromText_Valid()
    {
        Assert.AreEqual(SpdxChecksumAlgorithm.Missing, SpdxChecksumAlgorithmExtensions.FromText(""));
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, SpdxChecksumAlgorithmExtensions.FromText("SHA1"));
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, SpdxChecksumAlgorithmExtensions.FromText("sha1"));
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, SpdxChecksumAlgorithmExtensions.FromText("Sha1"));
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha224, SpdxChecksumAlgorithmExtensions.FromText("SHA224"));
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha256, SpdxChecksumAlgorithmExtensions.FromText("SHA256"));
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha384, SpdxChecksumAlgorithmExtensions.FromText("SHA384"));
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha512, SpdxChecksumAlgorithmExtensions.FromText("SHA512"));
        Assert.AreEqual(SpdxChecksumAlgorithm.Md2, SpdxChecksumAlgorithmExtensions.FromText("MD2"));
        Assert.AreEqual(SpdxChecksumAlgorithm.Md4, SpdxChecksumAlgorithmExtensions.FromText("MD4"));
        Assert.AreEqual(SpdxChecksumAlgorithm.Md5, SpdxChecksumAlgorithmExtensions.FromText("MD5"));
        Assert.AreEqual(SpdxChecksumAlgorithm.Md6, SpdxChecksumAlgorithmExtensions.FromText("MD6"));
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha3256, SpdxChecksumAlgorithmExtensions.FromText("SHA3-256"));
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha3384, SpdxChecksumAlgorithmExtensions.FromText("SHA3-384"));
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha3512, SpdxChecksumAlgorithmExtensions.FromText("SHA3-512"));
        Assert.AreEqual(SpdxChecksumAlgorithm.Blake2B256, SpdxChecksumAlgorithmExtensions.FromText("BLAKE2b-256"));
        Assert.AreEqual(SpdxChecksumAlgorithm.Blake2B384, SpdxChecksumAlgorithmExtensions.FromText("BLAKE2b-384"));
        Assert.AreEqual(SpdxChecksumAlgorithm.Blake2B512, SpdxChecksumAlgorithmExtensions.FromText("BLAKE2b-512"));
        Assert.AreEqual(SpdxChecksumAlgorithm.Blake3, SpdxChecksumAlgorithmExtensions.FromText("BLAKE3"));
        Assert.AreEqual(SpdxChecksumAlgorithm.Adler32, SpdxChecksumAlgorithmExtensions.FromText("ADLER32"));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxChecksumAlgorithmExtensions.FromText(string)" /> method.
    /// </summary>
    [TestMethod]
    public void SpdxChecksumAlgorithmExtensions_FromText_InvalidAlgorithm()
    {
        var exception =
            Assert.ThrowsExactly<InvalidOperationException>(() => SpdxChecksumAlgorithmExtensions.FromText("unknown"));
        Assert.AreEqual("Unsupported SPDX Checksum Algorithm 'unknown'", exception.Message);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxChecksumAlgorithmExtensions.ToText(SpdxChecksumAlgorithm)" /> method.
    /// </summary>
    [TestMethod]
    public void SpdxChecksumAlgorithmExtensions_ToText_Valid()
    {
        Assert.AreEqual("SHA1", SpdxChecksumAlgorithm.Sha1.ToText());
        Assert.AreEqual("SHA224", SpdxChecksumAlgorithm.Sha224.ToText());
        Assert.AreEqual("SHA256", SpdxChecksumAlgorithm.Sha256.ToText());
        Assert.AreEqual("SHA384", SpdxChecksumAlgorithm.Sha384.ToText());
        Assert.AreEqual("SHA512", SpdxChecksumAlgorithm.Sha512.ToText());
        Assert.AreEqual("MD2", SpdxChecksumAlgorithm.Md2.ToText());
        Assert.AreEqual("MD4", SpdxChecksumAlgorithm.Md4.ToText());
        Assert.AreEqual("MD5", SpdxChecksumAlgorithm.Md5.ToText());
        Assert.AreEqual("MD6", SpdxChecksumAlgorithm.Md6.ToText());
        Assert.AreEqual("SHA3-256", SpdxChecksumAlgorithm.Sha3256.ToText());
        Assert.AreEqual("SHA3-384", SpdxChecksumAlgorithm.Sha3384.ToText());
        Assert.AreEqual("SHA3-512", SpdxChecksumAlgorithm.Sha3512.ToText());
        Assert.AreEqual("BLAKE2b-256", SpdxChecksumAlgorithm.Blake2B256.ToText());
        Assert.AreEqual("BLAKE2b-384", SpdxChecksumAlgorithm.Blake2B384.ToText());
        Assert.AreEqual("BLAKE2b-512", SpdxChecksumAlgorithm.Blake2B512.ToText());
        Assert.AreEqual("BLAKE3", SpdxChecksumAlgorithm.Blake3.ToText());
        Assert.AreEqual("ADLER32", SpdxChecksumAlgorithm.Adler32.ToText());
    }

    /// <summary>
    ///     Tests the <see cref="SpdxChecksumAlgorithmExtensions.ToText(SpdxChecksumAlgorithm)" /> method.
    /// </summary>
    [TestMethod]
    public void SpdxChecksumAlgorithmExtensions_ToText_InvalidAlgorithm()
    {
        var exception = Assert.ThrowsExactly<InvalidOperationException>(() => ((SpdxChecksumAlgorithm)1000).ToText());
        Assert.AreEqual("Unsupported SPDX Checksum Algorithm '1000'", exception.Message);
    }
}
