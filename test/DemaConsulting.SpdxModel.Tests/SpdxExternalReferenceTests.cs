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
/// Tests for the <see cref="SpdxExternalReference"/> class.
/// </summary>
[TestClass]
public class SpdxExternalReferenceTests
{
    /// <summary>
    /// Tests the <see cref="SpdxExternalReference.Same"/> comparer.
    /// </summary>
    [TestMethod]
    public void ExternalReferenceSameComparer()
    {
        var r1 = new SpdxExternalReference
        {
            Category = SpdxReferenceCategory.Security,
            Type = "cpe23Type",
            Locator = "cpe:2.3:a:company:product:0.0.0:*:*:*:*:*:*:*"
        };

        var r2 = new SpdxExternalReference
        {
            Category = SpdxReferenceCategory.Security,
            Type = "cpe23Type",
            Locator = "cpe:2.3:a:company:product:0.0.0:*:*:*:*:*:*:*",
            Comment = "CPE23 Standard Identifier"
        };

        var r3 = new SpdxExternalReference
        {
            Category = SpdxReferenceCategory.PackageManager,
            Type = "purl",
            Locator = "pkg:nuget/SomePackage@0.0.0"
        };

        // Assert external-references compare to themselves
        Assert.IsTrue(SpdxExternalReference.Same.Equals(r1, r1));
        Assert.IsTrue(SpdxExternalReference.Same.Equals(r2, r2));
        Assert.IsTrue(SpdxExternalReference.Same.Equals(r3, r3));

        // Assert external-references compare correctly
        Assert.IsTrue(SpdxExternalReference.Same.Equals(r1, r2));
        Assert.IsTrue(SpdxExternalReference.Same.Equals(r2, r1));
        Assert.IsFalse(SpdxExternalReference.Same.Equals(r1, r3));
        Assert.IsFalse(SpdxExternalReference.Same.Equals(r3, r1));
        Assert.IsFalse(SpdxExternalReference.Same.Equals(r2, r3));
        Assert.IsFalse(SpdxExternalReference.Same.Equals(r3, r2));

        // Assert same external-references have identical hashes
        Assert.AreEqual(SpdxExternalReference.Same.GetHashCode(r1), SpdxExternalReference.Same.GetHashCode(r2));
    }

    /// <summary>
    /// Tests the <see cref="SpdxExternalReference.DeepCopy"/> method.
    /// </summary>
    [TestMethod]
    public void DeepCopy()
    {
        var r1 = new SpdxExternalReference
        {
            Category = SpdxReferenceCategory.Security,
            Type = "cpe23Type",
            Locator = "cpe:2.3:a:company:product:0.0.0:*:*:*:*:*:*:*",
            Comment = "CPE23 Standard Identifier"
        };

        // Make deep copy
        var r2 = r1.DeepCopy();

        // Assert both objects are equal
        Assert.AreEqual(r1, r2, SpdxExternalReference.Same);
        Assert.AreEqual(r1.Category, r2.Category);
        Assert.AreEqual(r1.Type, r2.Type);
        Assert.AreEqual(r1.Locator, r2.Locator);
        Assert.AreEqual(r1.Comment, r2.Comment);

        // Assert separate instances
        Assert.IsFalse(ReferenceEquals(r1, r2));
    }

    /// <summary>
    /// Tests the <see cref="SpdxExternalReference.Enhance(SpdxExternalReference[], SpdxExternalReference[])"/> method.
    /// </summary>
    [TestMethod]
    public void Enhance()
    {
        var references = new[]
        {
            new SpdxExternalReference
            {
                Category = SpdxReferenceCategory.Security,
                Type = "cpe23Type",
                Locator = "cpe:2.3:a:company:product:0.0.0:*:*:*:*:*:*:*"
            }
        };

        references = SpdxExternalReference.Enhance(
            references,
            [
                new SpdxExternalReference
                {
                    Category = SpdxReferenceCategory.Security,
                    Type = "cpe23Type",
                    Locator = "cpe:2.3:a:company:product:0.0.0:*:*:*:*:*:*:*",
                    Comment = "CPE23 Standard Identifier"
                },
                new SpdxExternalReference
                {
                    Category = SpdxReferenceCategory.PackageManager,
                    Type = "purl",
                    Locator = "pkg:nuget/SomePackage@0.0.0"
                }
            ]);

        Assert.AreEqual(2, references.Length);
        Assert.AreEqual(SpdxReferenceCategory.Security, references[0].Category);
        Assert.AreEqual("cpe23Type", references[0].Type);
        Assert.AreEqual("cpe:2.3:a:company:product:0.0.0:*:*:*:*:*:*:*", references[0].Locator);
        Assert.AreEqual("CPE23 Standard Identifier", references[0].Comment);
        Assert.AreEqual(SpdxReferenceCategory.PackageManager, references[1].Category);
        Assert.AreEqual("purl", references[1].Type);
        Assert.AreEqual("pkg:nuget/SomePackage@0.0.0", references[1].Locator);
    }

    /// <summary>
    /// Tests the <see cref="SpdxReferenceCategoryExtensions.FromText(string)"/> method with valid input.
    /// </summary>
    [TestMethod]
    public void SpdxReferenceCategoryExtensions_FromText_Valid()
    {
        Assert.AreEqual(SpdxReferenceCategory.Missing, SpdxReferenceCategoryExtensions.FromText(""));
        Assert.AreEqual(SpdxReferenceCategory.Security, SpdxReferenceCategoryExtensions.FromText("SECURITY"));
        Assert.AreEqual(SpdxReferenceCategory.Security, SpdxReferenceCategoryExtensions.FromText("security"));
        Assert.AreEqual(SpdxReferenceCategory.Security, SpdxReferenceCategoryExtensions.FromText("Security"));
        Assert.AreEqual(SpdxReferenceCategory.PackageManager, SpdxReferenceCategoryExtensions.FromText("PACKAGE-MANAGER"));
        Assert.AreEqual(SpdxReferenceCategory.PackageManager, SpdxReferenceCategoryExtensions.FromText("PACKAGE_MANAGER"));
        Assert.AreEqual(SpdxReferenceCategory.PersistentId, SpdxReferenceCategoryExtensions.FromText("PERSISTENT-ID"));
        Assert.AreEqual(SpdxReferenceCategory.Other, SpdxReferenceCategoryExtensions.FromText("OTHER"));
    }

    /// <summary>
    /// Tests the <see cref="SpdxReferenceCategoryExtensions.FromText(string)"/> method with invalid input.
    /// </summary>
    [TestMethod]
    public void SpdxReferenceCategoryExtensions_FromText_Invalid()
    {
        var exception = Assert.ThrowsExactly<InvalidOperationException>(() => SpdxReferenceCategoryExtensions.FromText("invalid"));
        Assert.AreEqual("Unsupported SPDX Reference Category 'invalid'", exception.Message);
    }

    /// <summary>
    /// Tests the <see cref="SpdxReferenceCategoryExtensions.ToText"/> method with valid input.
    /// </summary>
    [TestMethod]
    public void SpdxReferenceCategoryExtensions_ToText_Valid()
    {
        Assert.AreEqual("SECURITY", SpdxReferenceCategory.Security.ToText());
        Assert.AreEqual("PACKAGE-MANAGER", SpdxReferenceCategory.PackageManager.ToText());
        Assert.AreEqual("PERSISTENT-ID", SpdxReferenceCategory.PersistentId.ToText());
        Assert.AreEqual("OTHER", SpdxReferenceCategory.Other.ToText());
    }

    /// <summary>
    /// Tests the <see cref="SpdxReferenceCategoryExtensions.ToText"/> method with invalid input.
    /// </summary>
    [TestMethod]
    public void SpdxReferenceCategoryExtensions_ToText_Invalid()
    {
        var exception = Assert.ThrowsExactly<InvalidOperationException>(() => ((SpdxReferenceCategory)1000).ToText());
        Assert.AreEqual("Unsupported SPDX Reference Category '1000'", exception.Message);
    }
}