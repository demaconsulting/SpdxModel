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
///     Tests for the <see cref="SpdxExternalReference" /> class.
/// </summary>
/// <remarks>
///     Covers the Same equality comparer, DeepCopy, Enhance merge, Validate, and the
///     SpdxReferenceCategory text-conversion extension methods (FromText/ToText).
/// </remarks>
[TestClass]
public class SpdxExternalReferenceTests
{
    /// <summary>
    ///     Tests the <see cref="SpdxExternalReference.Same" /> comparer compares external references correctly.
    /// </summary>
    /// <remarks>
    ///     Verifies that two references with the same Category, Type, and Locator are equal
    ///     regardless of Comment differences, that differing field values produce inequality,
    ///     and that equal references produce identical hash codes.
    /// </remarks>
    [TestMethod]
    public void SpdxExternalReference_SameComparer_ComparesCorrectly()
    {
        // Arrange: Create three external references with different properties
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

        // Act / Assert: Verify external-references compare to themselves
        Assert.IsTrue(SpdxExternalReference.Same.Equals(r1, r1));
        Assert.IsTrue(SpdxExternalReference.Same.Equals(r2, r2));
        Assert.IsTrue(SpdxExternalReference.Same.Equals(r3, r3));

        // Assert: Verify external-references compare correctly
        Assert.IsTrue(SpdxExternalReference.Same.Equals(r1, r2));
        Assert.IsTrue(SpdxExternalReference.Same.Equals(r2, r1));
        Assert.IsFalse(SpdxExternalReference.Same.Equals(r1, r3));
        Assert.IsFalse(SpdxExternalReference.Same.Equals(r3, r1));
        Assert.IsFalse(SpdxExternalReference.Same.Equals(r2, r3));
        Assert.IsFalse(SpdxExternalReference.Same.Equals(r3, r2));

        // Assert: Verify same external-references have identical hashes
        Assert.AreEqual(SpdxExternalReference.Same.GetHashCode(r1), SpdxExternalReference.Same.GetHashCode(r2));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxExternalReference.DeepCopy" /> method successfully creates a deep copy.
    /// </summary>
    /// <remarks>
    ///     Verifies that the copy has equal field values to the original and that it is a distinct
    ///     object reference (not the same instance).
    /// </remarks>
    [TestMethod]
    public void SpdxExternalReference_DeepCopy_CreatesEqualButDistinctInstance()
    {
        // Arrange: Create an external reference
        var r1 = new SpdxExternalReference
        {
            Category = SpdxReferenceCategory.Security,
            Type = "cpe23Type",
            Locator = "cpe:2.3:a:company:product:0.0.0:*:*:*:*:*:*:*",
            Comment = "CPE23 Standard Identifier"
        };

        // Act: Create a deep copy of the original external reference
        var r2 = r1.DeepCopy();

        // Assert: Verify deep-copy is equal to original
        Assert.AreEqual(r1, r2, SpdxExternalReference.Same);
        Assert.AreEqual(r1.Category, r2.Category);
        Assert.AreEqual(r1.Type, r2.Type);
        Assert.AreEqual(r1.Locator, r2.Locator);
        Assert.AreEqual(r1.Comment, r2.Comment);

        // Assert: Verify deep-copy has distinct instance
        Assert.IsFalse(ReferenceEquals(r1, r2));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxExternalReference.Enhance(SpdxExternalReference[], SpdxExternalReference[])" /> method
    ///     adds or updates information correctly.
    /// </summary>
    /// <remarks>
    ///     Verifies that matching entries are enhanced in place and unmatched entries from the
    ///     source array are appended as new independent copies.
    /// </remarks>
    [TestMethod]
    public void SpdxExternalReference_Enhance_AddsOrUpdatesInformationCorrectly()
    {
        // Arrange: Create an array of external references
        var references = new[]
        {
            new SpdxExternalReference
            {
                Category = SpdxReferenceCategory.Security,
                Type = "cpe23Type",
                Locator = "cpe:2.3:a:company:product:0.0.0:*:*:*:*:*:*:*"
            }
        };

        // Act: Enhance the external references with additional references
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

        // Assert: Verify the references array has correct information
        Assert.HasCount(2, references);
        Assert.AreEqual(SpdxReferenceCategory.Security, references[0].Category);
        Assert.AreEqual("cpe23Type", references[0].Type);
        Assert.AreEqual("cpe:2.3:a:company:product:0.0.0:*:*:*:*:*:*:*", references[0].Locator);
        Assert.AreEqual("CPE23 Standard Identifier", references[0].Comment);
        Assert.AreEqual(SpdxReferenceCategory.PackageManager, references[1].Category);
        Assert.AreEqual("purl", references[1].Type);
        Assert.AreEqual("pkg:nuget/SomePackage@0.0.0", references[1].Locator);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxExternalReference.Validate" /> method reports invalid categories.
    /// </summary>
    /// <remarks>
    ///     Verifies that Validate appends an issue message when the Category field is
    ///     <see cref="SpdxReferenceCategory.Missing"/>.
    /// </remarks>
    [TestMethod]
    public void SpdxExternalReference_Validate_InvalidCategory_ReportsIssue()
    {
        // Arrange: Create an external reference with invalid category
        var reference = new SpdxExternalReference
        {
            Category = SpdxReferenceCategory.Missing,
            Type = "cpe23Type",
            Locator = "cpe:2.3:a:company:product:0.0.0:*:*:*:*:*:*:*",
            Comment = "CPE23 Standard Identifier"
        };

        // Act: Perform validation on the SpdxExternalReference instance
        var issues = new List<string>();
        reference.Validate("Test", issues);

        // Assert: Verify that the validation reports the invalid category
        Assert.Contains(issue => issue.Contains("Package 'Test' Invalid External Reference Category Field - Missing"), issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxExternalReference.Validate" /> method reports invalid types.
    /// </summary>
    /// <remarks>
    ///     Verifies that Validate appends an issue message when the Type field is empty.
    /// </remarks>
    [TestMethod]
    public void SpdxExternalReference_Validate_InvalidType_ReportsIssue()
    {
        // Arrange: Create an external reference with invalid type
        var reference = new SpdxExternalReference
        {
            Category = SpdxReferenceCategory.Security,
            Type = "",
            Locator = "cpe:2.3:a:company:product:0.0.0:*:*:*:*:*:*:*",
            Comment = "CPE23 Standard Identifier"
        };

        // Act: Perform validation on the SpdxExternalReference instance
        var issues = new List<string>();
        reference.Validate("Test", issues);

        // Assert: Verify that the validation reports the invalid type
        Assert.Contains(issue => issue.Contains("Package 'Test' Invalid External Reference Type Field - Empty"), issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxExternalReference.Validate" /> method reports invalid locators.
    /// </summary>
    /// <remarks>
    ///     Verifies that Validate appends an issue message when the Locator field is empty.
    /// </remarks>
    [TestMethod]
    public void SpdxExternalReference_Validate_InvalidLocator_ReportsIssue()
    {
        // Arrange: Create an external reference with invalid locator
        var reference = new SpdxExternalReference
        {
            Category = SpdxReferenceCategory.Security,
            Type = "cpe23Type",
            Locator = "",
            Comment = "CPE23 Standard Identifier"
        };

        // Act: Perform validation on the SpdxExternalReference instance
        var issues = new List<string>();
        reference.Validate("Test", issues);

        // Assert: Verify that the validation reports the invalid locator
        Assert.Contains(issue => issue.Contains("Package 'Test' Invalid External Reference Locator Field - Empty"), issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxReferenceCategoryExtensions.FromText(string)" /> method with valid input.
    /// </summary>
    /// <remarks>
    ///     Verifies that all recognized category strings, including case variants, map to the
    ///     expected enum values, and that an empty string maps to Missing.
    /// </remarks>
    [TestMethod]
    public void SpdxReferenceCategoryExtensions_FromText_ValidInput_ParsesCorrectly()
    {
        // Arrange: (no external state needed)

        // Act / Assert: Verify all recognized category strings map to expected enum values
        Assert.AreEqual(SpdxReferenceCategory.Missing, SpdxReferenceCategoryExtensions.FromText(""));
        Assert.AreEqual(SpdxReferenceCategory.Security, SpdxReferenceCategoryExtensions.FromText("SECURITY"));
        Assert.AreEqual(SpdxReferenceCategory.Security, SpdxReferenceCategoryExtensions.FromText("security"));
        Assert.AreEqual(SpdxReferenceCategory.Security, SpdxReferenceCategoryExtensions.FromText("Security"));
        Assert.AreEqual(SpdxReferenceCategory.PackageManager,
            SpdxReferenceCategoryExtensions.FromText("PACKAGE-MANAGER"));
        Assert.AreEqual(SpdxReferenceCategory.PackageManager,
            SpdxReferenceCategoryExtensions.FromText("PACKAGE_MANAGER"));
        Assert.AreEqual(SpdxReferenceCategory.PersistentId, SpdxReferenceCategoryExtensions.FromText("PERSISTENT-ID"));
        Assert.AreEqual(SpdxReferenceCategory.Other, SpdxReferenceCategoryExtensions.FromText("OTHER"));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxReferenceCategoryExtensions.FromText(string)" /> method with invalid input.
    /// </summary>
    /// <remarks>
    ///     Verifies that FromText throws <see cref="InvalidOperationException"/> with a message
    ///     identifying the unsupported value when given an unrecognized category string.
    /// </remarks>
    [TestMethod]
    public void SpdxReferenceCategoryExtensions_FromText_InvalidInput_ReturnsNull()
    {
        // Arrange: (no external state needed)

        // Act / Assert: Verify that FromText throws for an unrecognized category string
        var exception =
            Assert.ThrowsExactly<InvalidOperationException>(() => SpdxReferenceCategoryExtensions.FromText("invalid"));
        Assert.AreEqual("Unsupported SPDX Reference Category 'invalid'", exception.Message);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxReferenceCategoryExtensions.ToText" /> method with valid input.
    /// </summary>
    /// <remarks>
    ///     Verifies that all known enum values map to their expected SPDX text representations.
    /// </remarks>
    [TestMethod]
    public void SpdxReferenceCategoryExtensions_ToText_ValidReference_FormatsCorrectly()
    {
        // Arrange: (no external state needed)

        // Act / Assert: Verify all known enum values map to expected text representations
        Assert.AreEqual("SECURITY", SpdxReferenceCategory.Security.ToText());
        Assert.AreEqual("PACKAGE-MANAGER", SpdxReferenceCategory.PackageManager.ToText());
        Assert.AreEqual("PERSISTENT-ID", SpdxReferenceCategory.PersistentId.ToText());
        Assert.AreEqual("OTHER", SpdxReferenceCategory.Other.ToText());
    }

    /// <summary>
    ///     Tests the <see cref="SpdxReferenceCategoryExtensions.ToText" /> method with invalid input.
    /// </summary>
    /// <remarks>
    ///     Verifies that ToText throws <see cref="InvalidOperationException"/> with the
    ///     unsupported-category message when called with an unrecognized enum value.
    /// </remarks>
    [TestMethod]
    public void SpdxReferenceCategoryExtensions_ToText_InvalidCategory_ReturnsNull()
    {
        // Arrange: Create an invalid reference category
        var invalidCategory = (SpdxReferenceCategory)1000;

        // Act / Assert: Verify that ToText throws an exception for unsupported category
        var exception = Assert.ThrowsExactly<InvalidOperationException>(() => invalidCategory.ToText());
        Assert.AreEqual("Unsupported SPDX Reference Category '1000'", exception.Message);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxReferenceCategoryExtensions.ToText" /> method with Missing category.
    /// </summary>
    /// <remarks>
    ///     Verifies that ToText throws <see cref="InvalidOperationException"/> with a specific
    ///     message when called with <see cref="SpdxReferenceCategory.Missing"/>.
    /// </remarks>
    [TestMethod]
    public void SpdxReferenceCategoryExtensions_ToText_MissingCategory_ReturnsNull()
    {
        // Arrange: Use Missing reference category
        var category = SpdxReferenceCategory.Missing;

        // Act / Assert: Verify that ToText throws an exception for Missing category
        var exception = Assert.ThrowsExactly<InvalidOperationException>(() => category.ToText());
        Assert.AreEqual("Attempt to serialize missing SPDX Reference Category", exception.Message);
    }
}
