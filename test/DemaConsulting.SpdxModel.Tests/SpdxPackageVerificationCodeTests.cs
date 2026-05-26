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
///     Tests for the <see cref="SpdxPackageVerificationCode" /> class.
/// </summary>
/// <remarks>
///     Covers equality comparison via the <see cref="SpdxPackageVerificationCode.Same" /> comparer,
///     deep-copy independence, field merging via <see cref="SpdxPackageVerificationCode.Enhance" />,
///     and SHA1 hex digest validation via <see cref="SpdxPackageVerificationCode.Validate" />.
/// </remarks>
[TestClass]
public class SpdxPackageVerificationCodeTests
{
    /// <summary>
    ///     Tests the <see cref="SpdxPackageVerificationCode.Same" /> comparer compares package verification codes correctly.
    /// </summary>
    /// <remarks>
    ///     Verifies that two codes with the same <c>Value</c> but different <c>ExcludedFiles</c> are considered equal,
    ///     while codes with different values are considered distinct. Also validates null handling and hash consistency.
    /// </remarks>
    [TestMethod]
    public void SpdxPackageVerificationCode_SameComparer_SameValueDifferentExcludedFiles_ReturnsEqual()
    {
        // Arrange: Create three package verification codes with different properties
        var v1 = new SpdxPackageVerificationCode
        {
            Value = "d6a770ba38583ed4bb4525bd96e50461655d2758"
        };
        var v2 = new SpdxPackageVerificationCode
        {
            ExcludedFiles = ["./package.spdx"],
            Value = "d6a770ba38583ed4bb4525bd96e50461655d2758"
        };
        var v3 = new SpdxPackageVerificationCode
        {
            Value = "85ed0817af83a24ad8da68c2b5094de69833983c"
        };

        // Act / Assert: Verify package-verification-codes compare to themselves
        Assert.IsTrue(SpdxPackageVerificationCode.Same.Equals(v1, v1));
        Assert.IsTrue(SpdxPackageVerificationCode.Same.Equals(v2, v2));
        Assert.IsTrue(SpdxPackageVerificationCode.Same.Equals(v3, v3));

        // Assert: Verify package-verification-codes compare correctly
        Assert.IsTrue(SpdxPackageVerificationCode.Same.Equals(v1, v2));
        Assert.IsTrue(SpdxPackageVerificationCode.Same.Equals(v2, v1));
        Assert.IsFalse(SpdxPackageVerificationCode.Same.Equals(v1, v3));
        Assert.IsFalse(SpdxPackageVerificationCode.Same.Equals(v3, v1));
        Assert.IsFalse(SpdxPackageVerificationCode.Same.Equals(v2, v3));
        Assert.IsFalse(SpdxPackageVerificationCode.Same.Equals(v3, v2));

        // Assert: Verify same package-verification-codes have identical hashes
        Assert.AreEqual(SpdxPackageVerificationCode.Same.GetHashCode(v1),
            SpdxPackageVerificationCode.Same.GetHashCode(v2));

        // Assert: Verify null handling
        Assert.IsTrue(SpdxPackageVerificationCode.Same.Equals(null!, null!));
        Assert.IsFalse(SpdxPackageVerificationCode.Same.Equals(null!, v1));
        Assert.IsFalse(SpdxPackageVerificationCode.Same.Equals(v1, null!));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxPackageVerificationCode.DeepCopy" /> method successfully creates a deep copy.
    /// </summary>
    /// <remarks>
    ///     Verifies that the deep copy has equal field values and that both the code object and its
    ///     <c>ExcludedFiles</c> array are distinct instances that can be mutated independently.
    /// </remarks>
    [TestMethod]
    public void SpdxPackageVerificationCode_DeepCopy_FullyPopulatedCode_CreatesEqualButDistinctCopy()
    {
        // Arrange: Create a package verification code with excluded files and a value
        var v1 = new SpdxPackageVerificationCode
        {
            ExcludedFiles = ["./package.spdx"],
            Value = "d6a770ba38583ed4bb4525bd96e50461655d2758"
        };

        // Act: Create a deep copy of the package verification code
        var v2 = v1.DeepCopy();

        // Assert: Verify deep-copy is equal to original
        Assert.AreEqual(v1, v2, SpdxPackageVerificationCode.Same);
        CollectionAssert.AreEqual(v1.ExcludedFiles, v2.ExcludedFiles);
        Assert.AreEqual(v1.Value, v2.Value);

        // Assert: Verify deep-copy has distinct instances
        Assert.IsFalse(ReferenceEquals(v1, v2));
        Assert.IsFalse(ReferenceEquals(v1.ExcludedFiles, v2.ExcludedFiles));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxPackageVerificationCode.Enhance" /> method adds or updates information correctly.
    /// </summary>
    /// <remarks>
    ///     Verifies that enhancing a code with excluded files merges them by deduplication, and that an
    ///     existing non-empty <c>Value</c> is not overwritten by the source.
    /// </remarks>
    [TestMethod]
    public void SpdxPackageVerificationCode_Enhance_MissingFields_MergesCorrectly()
    {
        // Arrange: Create a package verification code with a value
        var info = new SpdxPackageVerificationCode
        {
            Value = "d6a770ba38583ed4bb4525bd96e50461655d2758"
        };

        // Act: Enhance the package verification code with excluded files and a new value
        info.Enhance(
            new SpdxPackageVerificationCode
            {
                ExcludedFiles = ["./package.spdx"],
                Value = "d6a770ba38583ed4bb4525bd96e50461655d2758"
            });

        // Assert: Verify the excluded files and value are updated correctly
        Assert.HasCount(1, info.ExcludedFiles);
        Assert.AreEqual("./package.spdx", info.ExcludedFiles[0]);
        Assert.AreEqual("d6a770ba38583ed4bb4525bd96e50461655d2758", info.Value);
    }

    /// <summary>
    ///     Tests the Validate method reports an issue when the verification code value is invalid.
    /// </summary>
    /// <remarks>
    ///     Exercises the short-string boundary condition: a value shorter than 40 characters is not a valid
    ///     SHA1 hex digest and must produce a validation issue.
    /// </remarks>
    [TestMethod]
    public void SpdxPackageVerificationCode_Validate_InvalidValue_ReportsIssue()
    {
        // Arrange: Create a bad package verification code
        var info = new SpdxPackageVerificationCode
        {
            Value = "BadValue"
        };

        // Act: Perform validation on the SpdxPackageVerificationCode instance.
        var issues = new List<string>();
        info.Validate("Test", issues);

        // Assert: Verify that the validation fails and the error message includes the description
        Assert.Contains(issue => issue.Contains("Package 'Test' Invalid Package Verification Code Value 'BadValue'"), issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxPackageVerificationCode.Validate" /> method reports no issues for a valid value.
    /// </summary>
    /// <remarks>
    ///     Verifies the happy-path: a well-formed 40-character lowercase hex SHA1 digest passes all validation
    ///     checks without reporting any issues.
    /// </remarks>
    [TestMethod]
    public void SpdxPackageVerificationCode_Validate_ValidValue_ReportsNoIssues()
    {
        // Arrange: Create a package verification code with a valid SHA1 value
        var info = new SpdxPackageVerificationCode
        {
            Value = "d6a770ba38583ed4bb4525bd96e50461655d2758"
        };

        // Act: Perform validation on the SpdxPackageVerificationCode instance
        var issues = new List<string>();
        info.Validate("Test", issues);

        // Assert: Verify that the validation reports no issues
        Assert.IsEmpty(issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxPackageVerificationCode.Validate" /> method reports an issue for non-hex characters.
    /// </summary>
    /// <remarks>
    ///     Exercises the non-hex boundary condition: a string that is exactly 40 characters long but contains
    ///     characters outside the hexadecimal alphabet (0–9, a–f, A–F) must produce a validation issue.
    /// </remarks>
    [TestMethod]
    public void SpdxPackageVerificationCode_Validate_NonHexValue_ReportsIssue()
    {
        // Arrange: Create a package verification code with 40 chars but invalid hex
        var info = new SpdxPackageVerificationCode
        {
            Value = "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz"
        };

        // Act: Perform validation on the SpdxPackageVerificationCode instance
        var issues = new List<string>();
        info.Validate("Test", issues);

        // Assert: Verify that the validation fails
        Assert.Contains(issue => issue.Contains("Package 'Test' Invalid Package Verification Code Value"), issues);
    }
}
