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
[TestClass]
public class SpdxPackageVerificationCodeTests
{
    /// <summary>
    ///     Tests the <see cref="SpdxPackageVerificationCode.Same" /> comparer compares package verification codes correctly.
    /// </summary>
    [TestMethod]
    public void SpdxPackageVerificationCode_SameComparer_ComparesCorrectly()
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

        // Assert: Verify package-verification-codes compare to themselves
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
    }

    /// <summary>
    ///     Tests the <see cref="SpdxPackageVerificationCode.DeepCopy" /> method successfully creates a deep copy.
    /// </summary>
    [TestMethod]
    public void SpdxPackageVerificationCode_DeepCopy_CreatesEqualButDistinctInstance()
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
    [TestMethod]
    public void SpdxPackageVerificationCode_Enhance_AddsOrUpdatesInformationCorrectly()
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
    ///     Tests the <see cref="SpdxPackageVerificationCode.Validate" /> method reports bad annotators
    /// </summary>
    [TestMethod]
    public void SpdxPackageVerificationCode_Validate_InvalidValue()
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
}
