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
/// Tests for the <see cref="SpdxPackageVerificationCode"/> class.
/// </summary>
[TestClass]
public class SpdxPackageVerificationCodeTests
{
    /// <summary>
    /// Tests the <see cref="SpdxPackageVerificationCode.Same"/> comparer.
    /// </summary>
    [TestMethod]
    public void PackageVerificationCodeSameComparer()
    {
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
        
        // Assert package-verification-codes compare to themselves
        Assert.IsTrue(SpdxPackageVerificationCode.Same.Equals(v1, v1));
        Assert.IsTrue(SpdxPackageVerificationCode.Same.Equals(v2, v2));
        Assert.IsTrue(SpdxPackageVerificationCode.Same.Equals(v3, v3));

        // Assert package-verification-codes compare correctly
        Assert.IsTrue(SpdxPackageVerificationCode.Same.Equals(v1, v2));
        Assert.IsTrue(SpdxPackageVerificationCode.Same.Equals(v2, v1));
        Assert.IsFalse(SpdxPackageVerificationCode.Same.Equals(v1, v3));
        Assert.IsFalse(SpdxPackageVerificationCode.Same.Equals(v3, v1));
        Assert.IsFalse(SpdxPackageVerificationCode.Same.Equals(v2, v3));
        Assert.IsFalse(SpdxPackageVerificationCode.Same.Equals(v3, v2));

        // Assert same package-verification-codes have identical hashes
        Assert.AreEqual(SpdxPackageVerificationCode.Same.GetHashCode(v1), SpdxPackageVerificationCode.Same.GetHashCode(v2));
    }

    /// <summary>
    /// Tests the <see cref="SpdxPackageVerificationCode.DeepCopy"/> method.
    /// </summary>
    [TestMethod]
    public void DeepCopy()
    {
        var v1 = new SpdxPackageVerificationCode
        {
            ExcludedFiles = ["./package.spdx"],
            Value = "d6a770ba38583ed4bb4525bd96e50461655d2758"
        };

        var v2 = v1.DeepCopy();
        v2.ExcludedFiles[0] = "./test.spdx";
        
        Assert.IsFalse(ReferenceEquals(v1, v2));
        Assert.AreEqual("./package.spdx", v1.ExcludedFiles[0]);
        Assert.AreEqual("./test.spdx", v2.ExcludedFiles[0]);
    }

    /// <summary>
    /// Tests the <see cref="SpdxPackageVerificationCode.Enhance"/> method.
    /// </summary>
    [TestMethod]
    public void Enhance()
    {
        var info = new SpdxPackageVerificationCode
        {
            Value = "d6a770ba38583ed4bb4525bd96e50461655d2758"
        };

        info.Enhance(
            new SpdxPackageVerificationCode
            {
                ExcludedFiles = ["./package.spdx"],
                Value = "d6a770ba38583ed4bb4525bd96e50461655d2758"
            });

        Assert.AreEqual(1, info.ExcludedFiles.Length);
        Assert.AreEqual("./package.spdx", info.ExcludedFiles[0]);
        Assert.AreEqual("d6a770ba38583ed4bb4525bd96e50461655d2758", info.Value);
    }
}