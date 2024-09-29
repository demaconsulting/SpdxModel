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
/// Tests for the <see cref="SpdxPackage"/> class.
/// </summary>
[TestClass]
public class SpdxPackageTests
{
    /// <summary>
    /// Tests the <see cref="SpdxPackage.Same"/> comparer.
    /// </summary>
    [TestMethod]
    public void PackageSameComparer()
    {
        var p1 = new SpdxPackage
        {
            Id = "SPDXRef-Package1",
            Name = "DemaConsulting.SpdxModel",
            Version = "0.0.0"
        };

        var p2 = new SpdxPackage
        {
            Id = "SPDXRef-Package-SpdxModel",
            Name = "DemaConsulting.SpdxModel",
            Version = "0.0.0"
        };

        var p3 = new SpdxPackage
        {
            Id = "SPDXRef-Package1",
            Name = "SomePackage",
            Version = "1.2.3"
        };

        // Assert packages compare to themselves
        Assert.IsTrue(SpdxPackage.Same.Equals(p1, p1));
        Assert.IsTrue(SpdxPackage.Same.Equals(p2, p2));
        Assert.IsTrue(SpdxPackage.Same.Equals(p3, p3));

        // Assert packages compare correctly
        Assert.IsTrue(SpdxPackage.Same.Equals(p1, p2));
        Assert.IsTrue(SpdxPackage.Same.Equals(p2, p1));
        Assert.IsFalse(SpdxPackage.Same.Equals(p1, p3));
        Assert.IsFalse(SpdxPackage.Same.Equals(p3, p1));
        Assert.IsFalse(SpdxPackage.Same.Equals(p2, p3));
        Assert.IsFalse(SpdxPackage.Same.Equals(p3, p2));

        // Assert same packages have identical hashes
        Assert.AreEqual(SpdxPackage.Same.GetHashCode(p1), SpdxPackage.Same.GetHashCode(p2));
    }

    /// <summary>
    /// Tests the <see cref="SpdxPackage.DeepCopy"/> method.
    /// </summary>
    [TestMethod]
    public void DeepCopy()
    {
        var p1 = new SpdxPackage
        {
            Id = "SPDXRef-Package1",
            Name = "DemaConsulting.SpdxModel",
            Version = "0.0.0"
        };

        var p2 = p1.DeepCopy();
        p2.Id = "SPDXRef-Package2";
        
        Assert.IsFalse(ReferenceEquals(p1, p2));
        Assert.AreEqual("SPDXRef-Package1", p1.Id);
        Assert.AreEqual("SPDXRef-Package2", p2.Id);
    }

    /// <summary>
    /// Tests the <see cref="SpdxPackage.Enhance(SpdxPackage[], SpdxPackage[])"/> method.
    /// </summary>
    [TestMethod]
    public void Enhance()
    {
        var packages = new[]
        {
            new SpdxPackage
            {
                Id = "SPDXRef-Package1",
                Name = "DemaConsulting.SpdxModel",
                Version = "0.0.0"
            }
        };

        packages = SpdxPackage.Enhance(
            packages,
            [
                new SpdxPackage
                {
                    Id = "SPDXRef-Package-SpdxModel",
                    Name = "DemaConsulting.SpdxModel",
                    Version = "0.0.0"
                },
                new SpdxPackage
                {
                    Id = "SPDXRef-Package1",
                    Name = "SomePackage",
                    Version = "1.2.3"
                }
            ]);

        Assert.AreEqual(2, packages.Length);
        Assert.AreEqual("SPDXRef-Package1", packages[0].Id);
        Assert.AreEqual("DemaConsulting.SpdxModel", packages[0].Name);
        Assert.AreEqual("0.0.0", packages[0].Version);
        Assert.AreEqual("SPDXRef-Package1", packages[1].Id);
        Assert.AreEqual("SomePackage", packages[1].Name);
        Assert.AreEqual("1.2.3", packages[1].Version);
    }
}