namespace DemaConsulting.SpdxModel.Tests;

[TestClass]
public class SpdxPackageVerificationCodeTests
{
    [TestMethod]
    public void PackageVerificationCodeSameComparer()
    {
        var v1 = new SpdxPackageVerificationCode
        {
            Value = "d6a770ba38583ed4bb4525bd96e50461655d2758"
        };

        var v2 = new SpdxPackageVerificationCode
        {
            ExcludedFiles = new[] { "./package.spdx" },
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
        Assert.IsTrue(SpdxPackageVerificationCode.Same.GetHashCode(v1) == SpdxPackageVerificationCode.Same.GetHashCode(v2));
    }

    [TestMethod]
    public void DeepCopy()
    {
        var v1 = new SpdxPackageVerificationCode
        {
            ExcludedFiles = new[] { "./package.spdx" },
            Value = "d6a770ba38583ed4bb4525bd96e50461655d2758"
        };

        var v2 = v1.DeepCopy();
        v2.ExcludedFiles[0] = "./test.spdx";
        
        Assert.IsFalse(ReferenceEquals(v1, v2));
        Assert.AreEqual("./package.spdx", v1.ExcludedFiles[0]);
        Assert.AreEqual("./test.spdx", v2.ExcludedFiles[0]);
    }
}