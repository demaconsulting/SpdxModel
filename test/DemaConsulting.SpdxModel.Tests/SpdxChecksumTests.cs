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
/// <remarks>
///     Tests the <see cref="SpdxChecksum"/> class and the
///     <see cref="SpdxChecksumAlgorithmExtensions"/> extension methods. Uses xUnit v3 as the
///     test framework. Each test method is fully self-contained with no shared fixture state.
/// </remarks>
public class SpdxChecksumTests
{
    /// <summary>
    ///     Tests the <see cref="SpdxChecksum.Same" /> comparer compares checksums correctly.
    /// </summary>
    /// <remarks>
    ///     Sets up three checksums: two with identical algorithm and value (c1 and c2) and one
    ///     with a different algorithm and value (c3). Verifies reflexive, symmetric, and
    ///     cross-inequality comparisons, and that equal checksums produce identical hash codes.
    /// </remarks>
    [Fact]
    public void SpdxChecksum_SameComparer_SameOrDifferentValues_ReturnsCorrectEquality()
    {
        // Arrange: Create three checksums with different algorithm/value combinations
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

        // Act / Assert: Verify checksums compare to themselves
        Assert.True(SpdxChecksum.Same.Equals(c1, c1));
        Assert.True(SpdxChecksum.Same.Equals(c2, c2));
        Assert.True(SpdxChecksum.Same.Equals(c3, c3));

        // Assert: Verify checksums compare correctly
        Assert.True(SpdxChecksum.Same.Equals(c1, c2));
        Assert.True(SpdxChecksum.Same.Equals(c2, c1));
        Assert.False(SpdxChecksum.Same.Equals(c1, c3));
        Assert.False(SpdxChecksum.Same.Equals(c3, c1));
        Assert.False(SpdxChecksum.Same.Equals(c2, c3));
        Assert.False(SpdxChecksum.Same.Equals(c3, c2));

        // Assert: same checksums have identical hashes
        Assert.Equal(SpdxChecksum.Same.GetHashCode(c1), SpdxChecksum.Same.GetHashCode(c2));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxChecksum.DeepCopy" /> method successfully creates a deep copy.
    /// </summary>
    /// <remarks>
    ///     Creates a fully-populated checksum instance and deep-copies it. Verifies that the
    ///     copy has equal field values (Algorithm and Value) but is a distinct object reference
    ///     from the original, confirming no shallow aliasing.
    /// </remarks>
    [Fact]
    public void SpdxChecksum_DeepCopy_PopulatedChecksum_CreatesEqualButDistinctInstance()
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
        Assert.Equal(c1, c2, SpdxChecksum.Same);
        Assert.Equal(c1.Algorithm, c2.Algorithm);
        Assert.Equal(c1.Value, c2.Value);

        // Assert: Verify deep-copy has distinct instance
        Assert.False(ReferenceEquals(c1, c2));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxChecksum.Enhance(SpdxChecksum[], SpdxChecksum[])" /> method adds or updates information
    ///     correctly.
    /// </summary>
    /// <remarks>
    ///     Starts with a single SHA1 checksum and enhances with a list containing the same SHA1
    ///     entry and a new MD5 entry. Verifies that the existing entry is preserved and the new
    ///     entry is appended, resulting in exactly two checksums.
    /// </remarks>
    [Fact]
    public void SpdxChecksum_Enhance_ExistingAndNewAlgorithms_AddsOrUpdatesInformation()
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
        Assert.Equal(2, checksums.Length);
        Assert.Equal(SpdxChecksumAlgorithm.Sha1, checksums[0].Algorithm);
        Assert.Equal("c2b4e1c67a2d28fced849ee1bb76e7391b93f125", checksums[0].Value);
        Assert.Equal(SpdxChecksumAlgorithm.Md5, checksums[1].Algorithm);
        Assert.Equal("624c1abb3664f4b35547e7c73864ad24", checksums[1].Value);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxChecksum.Validate" /> method reports bad algorithms.
    /// </summary>
    /// <remarks>
    ///     Uses <see cref="SpdxChecksumAlgorithm.Missing"/> as the algorithm sentinel to confirm
    ///     that the validator catches the absent algorithm and includes the expected description
    ///     string in the reported issue.
    /// </remarks>
    [Fact]
    public void SpdxChecksum_Validate_MissingAlgorithm_ReportsAlgorithmIssue()
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
        Assert.Contains(issues, issue => issue.Contains("Test Invalid Checksum Algorithm Field - Missing"));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxChecksum.Validate" /> method reports bad values.
    /// </summary>
    /// <remarks>
    ///     Uses an empty string as the checksum value — the minimal invalid state — to confirm
    ///     that the validator catches the empty value and includes the expected description string
    ///     in the reported issue.
    /// </remarks>
    [Fact]
    public void SpdxChecksum_Validate_EmptyValue_ReportsValueIssue()
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
        Assert.Contains(issues, issue => issue.Contains("Test Invalid Checksum Value Field - Empty"));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxChecksum.Validate" /> method reports unknown numeric algorithms.
    /// </summary>
    /// <remarks>
    ///     Casts the integer literal 1000 to <see cref="SpdxChecksumAlgorithm"/> to produce an
    ///     out-of-range enum value. Verifies that the validator treats it as an unknown algorithm
    ///     and reports the expected diagnostic message.
    /// </remarks>
    [Fact]
    public void SpdxChecksum_Validate_UnknownNumericAlgorithm_ReportsAlgorithmIssue()
    {
        // Arrange: Create a checksum with an out-of-range numeric algorithm value
        var checksum = new SpdxChecksum
        {
            Algorithm = (SpdxChecksumAlgorithm)1000,
            Value = "c2b4e1c67a2d28fced849ee1bb76e7391b93f125"
        };

        // Act: Perform validation on the SpdxChecksum instance
        var issues = new List<string>();
        checksum.Validate("Test", issues);

        // Assert: Verify that the validation reports the unknown algorithm
        Assert.Contains(issues, issue => issue.Contains("Test Invalid Checksum Algorithm Field - Unknown"));
    }

    /// <summary>
    ///     Tests that FromText maps every known SPDX algorithm string to the correct enum value.
    /// </summary>
    /// <remarks>
    ///     Exercises every known algorithm string defined by the SPDX 2.x specification, plus
    ///     case-variant inputs for SHA1, to confirm that <see cref="SpdxChecksumAlgorithmExtensions.FromText"/>
    ///     maps each string to the correct <see cref="SpdxChecksumAlgorithm"/> enum value.
    /// </remarks>
    [Fact]
    public void SpdxChecksumAlgorithmExtensions_FromText_KnownAlgorithmStrings_ReturnsCorrectEnumValues()
    {
        // Arrange: Known algorithm strings are implicit in the Act/Assert pairs below

        // Act / Assert: Verify each known algorithm string maps to the correct enum value
        Assert.Equal(SpdxChecksumAlgorithm.Missing, SpdxChecksumAlgorithmExtensions.FromText(""));
        Assert.Equal(SpdxChecksumAlgorithm.Sha1, SpdxChecksumAlgorithmExtensions.FromText("SHA1"));
        Assert.Equal(SpdxChecksumAlgorithm.Sha1, SpdxChecksumAlgorithmExtensions.FromText("sha1"));
        Assert.Equal(SpdxChecksumAlgorithm.Sha1, SpdxChecksumAlgorithmExtensions.FromText("Sha1"));
        Assert.Equal(SpdxChecksumAlgorithm.Sha224, SpdxChecksumAlgorithmExtensions.FromText("SHA224"));
        Assert.Equal(SpdxChecksumAlgorithm.Sha256, SpdxChecksumAlgorithmExtensions.FromText("SHA256"));
        Assert.Equal(SpdxChecksumAlgorithm.Sha384, SpdxChecksumAlgorithmExtensions.FromText("SHA384"));
        Assert.Equal(SpdxChecksumAlgorithm.Sha512, SpdxChecksumAlgorithmExtensions.FromText("SHA512"));
        Assert.Equal(SpdxChecksumAlgorithm.Md2, SpdxChecksumAlgorithmExtensions.FromText("MD2"));
        Assert.Equal(SpdxChecksumAlgorithm.Md4, SpdxChecksumAlgorithmExtensions.FromText("MD4"));
        Assert.Equal(SpdxChecksumAlgorithm.Md5, SpdxChecksumAlgorithmExtensions.FromText("MD5"));
        Assert.Equal(SpdxChecksumAlgorithm.Md6, SpdxChecksumAlgorithmExtensions.FromText("MD6"));
        Assert.Equal(SpdxChecksumAlgorithm.Sha3256, SpdxChecksumAlgorithmExtensions.FromText("SHA3-256"));
        Assert.Equal(SpdxChecksumAlgorithm.Sha3384, SpdxChecksumAlgorithmExtensions.FromText("SHA3-384"));
        Assert.Equal(SpdxChecksumAlgorithm.Sha3512, SpdxChecksumAlgorithmExtensions.FromText("SHA3-512"));
        Assert.Equal(SpdxChecksumAlgorithm.Blake2B256, SpdxChecksumAlgorithmExtensions.FromText("BLAKE2b-256"));
        Assert.Equal(SpdxChecksumAlgorithm.Blake2B384, SpdxChecksumAlgorithmExtensions.FromText("BLAKE2b-384"));
        Assert.Equal(SpdxChecksumAlgorithm.Blake2B512, SpdxChecksumAlgorithmExtensions.FromText("BLAKE2b-512"));
        Assert.Equal(SpdxChecksumAlgorithm.Blake3, SpdxChecksumAlgorithmExtensions.FromText("BLAKE3"));
        Assert.Equal(SpdxChecksumAlgorithm.Adler32, SpdxChecksumAlgorithmExtensions.FromText("ADLER32"));
    }

    /// <summary>
    ///     Tests that FromText throws InvalidOperationException for an unrecognized algorithm string.
    /// </summary>
    /// <remarks>
    ///     Passes the unrecognized string <c>"unknown"</c> to confirm that
    ///     <see cref="SpdxChecksumAlgorithmExtensions.FromText"/> throws
    ///     <see cref="InvalidOperationException"/> with the expected message rather than
    ///     returning a default value or silently succeeding.
    /// </remarks>
    [Fact]
    public void SpdxChecksumAlgorithmExtensions_FromText_UnknownAlgorithmString_ThrowsInvalidOperationException()
    {
        // Arrange: Use an algorithm string that is not in the known-algorithm list
        // (No variable needed — the input is inlined directly into the Act / Assert.)

        // Act / Assert: Verify that FromText throws for an unrecognized algorithm string
        var exception =
            Assert.Throws<InvalidOperationException>(() => SpdxChecksumAlgorithmExtensions.FromText("unknown"));
        Assert.Equal("Unsupported SPDX Checksum Algorithm 'unknown'", exception.Message);
    }

    /// <summary>
    ///     Tests that ToText returns the correct SPDX string for every serializable algorithm enum value.
    /// </summary>
    /// <remarks>
    ///     Exercises every serializable <see cref="SpdxChecksumAlgorithm"/> enum value to confirm
    ///     that <see cref="SpdxChecksumAlgorithmExtensions.ToText"/> returns the canonical
    ///     SPDX 2.x algorithm string for each value.
    /// </remarks>
    [Fact]
    public void SpdxChecksumAlgorithmExtensions_ToText_KnownAlgorithmEnums_ReturnsCorrectStrings()
    {
        // Arrange: Known algorithm enum values are implicit in the Act/Assert pairs below

        // Act / Assert: Verify each known algorithm enum maps to the correct string
        Assert.Equal("SHA1", SpdxChecksumAlgorithm.Sha1.ToText());
        Assert.Equal("SHA224", SpdxChecksumAlgorithm.Sha224.ToText());
        Assert.Equal("SHA256", SpdxChecksumAlgorithm.Sha256.ToText());
        Assert.Equal("SHA384", SpdxChecksumAlgorithm.Sha384.ToText());
        Assert.Equal("SHA512", SpdxChecksumAlgorithm.Sha512.ToText());
        Assert.Equal("MD2", SpdxChecksumAlgorithm.Md2.ToText());
        Assert.Equal("MD4", SpdxChecksumAlgorithm.Md4.ToText());
        Assert.Equal("MD5", SpdxChecksumAlgorithm.Md5.ToText());
        Assert.Equal("MD6", SpdxChecksumAlgorithm.Md6.ToText());
        Assert.Equal("SHA3-256", SpdxChecksumAlgorithm.Sha3256.ToText());
        Assert.Equal("SHA3-384", SpdxChecksumAlgorithm.Sha3384.ToText());
        Assert.Equal("SHA3-512", SpdxChecksumAlgorithm.Sha3512.ToText());
        Assert.Equal("BLAKE2b-256", SpdxChecksumAlgorithm.Blake2B256.ToText());
        Assert.Equal("BLAKE2b-384", SpdxChecksumAlgorithm.Blake2B384.ToText());
        Assert.Equal("BLAKE2b-512", SpdxChecksumAlgorithm.Blake2B512.ToText());
        Assert.Equal("BLAKE3", SpdxChecksumAlgorithm.Blake3.ToText());
        Assert.Equal("ADLER32", SpdxChecksumAlgorithm.Adler32.ToText());
    }

    /// <summary>
    ///     Tests that ToText throws InvalidOperationException for an out-of-range numeric enum value.
    /// </summary>
    /// <remarks>
    ///     Casts the integer literal 1000 to <see cref="SpdxChecksumAlgorithm"/> to produce an
    ///     out-of-range value. Verifies that <see cref="SpdxChecksumAlgorithmExtensions.ToText"/>
    ///     throws <see cref="InvalidOperationException"/> with the expected message.
    /// </remarks>
    [Fact]
    public void SpdxChecksumAlgorithmExtensions_ToText_OutOfRangeEnum_ThrowsInvalidOperationException()
    {
        // Arrange: Use a numeric enum value that has no named member in SpdxChecksumAlgorithm
        // (No variable needed — the value is inlined directly into the Act / Assert.)

        // Act / Assert: Verify that ToText throws for an out-of-range enum value
        var exception = Assert.Throws<InvalidOperationException>(() => ((SpdxChecksumAlgorithm)1000).ToText());
        Assert.Equal("Unsupported SPDX Checksum Algorithm '1000'", exception.Message);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxChecksumAlgorithmExtensions.ToText(SpdxChecksumAlgorithm)" /> method throws for
    ///     <see cref="SpdxChecksumAlgorithm.Missing" />.
    /// </summary>
    /// <remarks>
    ///     Passes <see cref="SpdxChecksumAlgorithm.Missing"/> — the sentinel value that must
    ///     never be serialized — to confirm that <see cref="SpdxChecksumAlgorithmExtensions.ToText"/>
    ///     throws <see cref="InvalidOperationException"/> with the expected message.
    /// </remarks>
    [Fact]
    public void SpdxChecksumAlgorithmExtensions_ToText_MissingAlgorithm_ThrowsInvalidOperationException()
    {
        // Arrange: Use the Missing sentinel value, which must never be serialized

        // Act / Assert: Verify that ToText throws for the Missing sentinel
        var exception = Assert.Throws<InvalidOperationException>(
            () => SpdxChecksumAlgorithm.Missing.ToText());
        Assert.Equal("Attempt to serialize missing SPDX Checksum Algorithm", exception.Message);
    }

    /// <summary>
    ///     Tests that <see cref="SpdxChecksum.Same" /> returns false when the first argument is null.
    /// </summary>
    /// <remarks>
    ///     Passes a null reference as the first argument and a valid checksum as the second.
    ///     Verifies that the comparer returns false rather than throwing, exercising the null-guard
    ///     on the left-hand operand.
    /// </remarks>
    [Fact]
    public void SpdxChecksum_SameComparer_NullFirstArgument_ReturnsFalse()
    {
        // Arrange: Create one valid checksum and one null reference
        var c1 = new SpdxChecksum
        {
            Algorithm = SpdxChecksumAlgorithm.Sha1,
            Value = "c2b4e1c67a2d28fced849ee1bb76e7391b93f125"
        };
        SpdxChecksum? nullChecksum = null;

        // Act: Compare null with a valid checksum
        var result = SpdxChecksum.Same.Equals(nullChecksum!, c1);

        // Assert: Verify null-first comparison returns false
        Assert.False(result);
    }

    /// <summary>
    ///     Tests that <see cref="SpdxChecksum.Same" /> returns false when the second argument is null.
    /// </summary>
    /// <remarks>
    ///     Passes a valid checksum as the first argument and a null reference as the second.
    ///     Verifies that the comparer returns false rather than throwing, exercising the null-guard
    ///     on the right-hand operand.
    /// </remarks>
    [Fact]
    public void SpdxChecksum_SameComparer_NullSecondArgument_ReturnsFalse()
    {
        // Arrange: Create one valid checksum and one null reference
        var c1 = new SpdxChecksum
        {
            Algorithm = SpdxChecksumAlgorithm.Sha1,
            Value = "c2b4e1c67a2d28fced849ee1bb76e7391b93f125"
        };
        SpdxChecksum? nullChecksum = null;

        // Act: Compare a valid checksum with null
        var result = SpdxChecksum.Same.Equals(c1, nullChecksum!);

        // Assert: Verify null-second comparison returns false
        Assert.False(result);
    }

    /// <summary>
    ///     Tests that <see cref="SpdxChecksum.Same" /> returns true when both arguments are null.
    /// </summary>
    /// <remarks>
    ///     Passes two null references to confirm that the comparer returns true when both
    ///     operands are null, consistent with standard equality-comparer semantics.
    /// </remarks>
    [Fact]
    public void SpdxChecksum_SameComparer_BothArgumentsNull_ReturnsTrue()
    {
        // Arrange: Two null references
        SpdxChecksum? c1 = null;
        SpdxChecksum? c2 = null;

        // Act: Compare null with null
        var result = SpdxChecksum.Same.Equals(c1!, c2!);

        // Assert: Verify null-null comparison returns true
        Assert.True(result);
    }

    /// <summary>
    ///     Tests that <see cref="SpdxChecksumAlgorithmExtensions.FromText" /> returns Missing for an empty string.
    /// </summary>
    /// <remarks>
    ///     Passes an empty string to confirm that <see cref="SpdxChecksumAlgorithmExtensions.FromText"/>
    ///     returns <see cref="SpdxChecksumAlgorithm.Missing"/> rather than throwing, treating
    ///     the empty string as the absent-algorithm sentinel.
    /// </remarks>
    [Fact]
    public void SpdxChecksumAlgorithmExtensions_FromText_EmptyString_ReturnsMissing()
    {
        // Arrange: An empty string
        var input = "";

        // Act: Convert the empty string to an algorithm
        var result = SpdxChecksumAlgorithmExtensions.FromText(input);

        // Assert: Verify empty string maps to Missing
        Assert.Equal(SpdxChecksumAlgorithm.Missing, result);
    }
}
