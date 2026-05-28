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
///     Tests for the <see cref="SpdxLicenseElement" /> abstract base class.
/// </summary>
/// <remarks>
///     Exercises the fitness-based field merging implemented in <c>EnhanceLicenseElement</c>
///     through the concrete <see cref="SpdxPackage" /> subclass. Covers all five inherited
///     fields: <c>ConcludedLicense</c>, <c>LicenseComments</c>, <c>CopyrightText</c>,
///     <c>AttributionText</c>, and <c>Annotations</c>.
/// </remarks>
public class SpdxLicenseElementTests
{
    /// <summary>
    ///     Tests that empty and null license-element fields are replaced by concrete source values.
    /// </summary>
    /// <remarks>
    ///     Verifies the lowest-fitness case: an empty <c>ConcludedLicense</c>, empty
    ///     <c>CopyrightText</c>, and null <c>LicenseComments</c> are all replaced when the
    ///     source carries concrete (rank-3) values.
    /// </remarks>
    [Fact]
    public void SpdxLicenseElement_Enhance_EmptyAndNullFields_ReplacedByConcreteValues()
    {
        // Arrange: Create a package with empty/null license-element fields
        var primary = new SpdxPackage
        {
            Name = "TestPackage",
            Version = "1.0.0",
            ConcludedLicense = "",
            CopyrightText = "",
            LicenseComments = null
        };
        var secondary = new SpdxPackage
        {
            Name = "TestPackage",
            Version = "1.0.0",
            ConcludedLicense = "MIT",
            CopyrightText = "Copyright 2024 DEMA Consulting",
            LicenseComments = "License determined from source headers"
        };

        // Act: Enhance the primary package with the secondary
        primary.Enhance(secondary);

        // Assert: Verify that empty/null fields were replaced with concrete values
        Assert.Equal("MIT", primary.ConcludedLicense);
        Assert.Equal("Copyright 2024 DEMA Consulting", primary.CopyrightText);
        Assert.Equal("License determined from source headers", primary.LicenseComments);
    }

    /// <summary>
    ///     Tests that NOASSERTION license-element fields are replaced by concrete source values.
    /// </summary>
    /// <remarks>
    ///     Verifies the mid-fitness case: <c>ConcludedLicense</c> and <c>CopyrightText</c> set to
    ///     <c>NOASSERTION</c> (rank 2) are replaced when the source carries concrete (rank-3)
    ///     values. <c>LicenseComments</c> set to <c>NOASSERTION</c> is similarly replaced.
    /// </remarks>
    [Fact]
    public void SpdxLicenseElement_Enhance_NoAssertionFields_ReplacedByConcreteValues()
    {
        // Arrange: Create a package with NOASSERTION license-element fields
        var primary = new SpdxPackage
        {
            Name = "TestPackage",
            Version = "1.0.0",
            ConcludedLicense = SpdxElement.NoAssertion,
            CopyrightText = SpdxElement.NoAssertion,
            LicenseComments = SpdxElement.NoAssertion
        };
        var secondary = new SpdxPackage
        {
            Name = "TestPackage",
            Version = "1.0.0",
            ConcludedLicense = "Apache-2.0",
            CopyrightText = "Copyright 2024 DEMA Consulting",
            LicenseComments = "Apache license confirmed"
        };

        // Act: Enhance the primary package with the secondary
        primary.Enhance(secondary);

        // Assert: Verify that NOASSERTION fields were replaced with concrete values
        Assert.Equal("Apache-2.0", primary.ConcludedLicense);
        Assert.Equal("Copyright 2024 DEMA Consulting", primary.CopyrightText);
        Assert.Equal("Apache license confirmed", primary.LicenseComments);
    }

    /// <summary>
    ///     Tests that concrete license-element fields are not replaced by secondary values.
    /// </summary>
    /// <remarks>
    ///     Verifies the highest-fitness case: once a field holds a concrete (rank-3) value it
    ///     must not be overwritten by any secondary value regardless of the secondary's fitness
    ///     level (null, empty, NOASSERTION, or another concrete value).
    /// </remarks>
    [Fact]
    public void SpdxLicenseElement_Enhance_ConcreteFields_NotReplacedBySecondaryValues()
    {
        // Arrange: Create a package with concrete license-element fields
        var primary = new SpdxPackage
        {
            Name = "TestPackage",
            Version = "1.0.0",
            ConcludedLicense = "MIT",
            CopyrightText = "Copyright 2024 DEMA Consulting",
            LicenseComments = "MIT license confirmed"
        };
        var secondary = new SpdxPackage
        {
            Name = "TestPackage",
            Version = "1.0.0",
            ConcludedLicense = "Apache-2.0",
            CopyrightText = "Copyright 2024 Other Corp",
            LicenseComments = "Different comment"
        };

        // Act: Enhance the primary package with the secondary
        primary.Enhance(secondary);

        // Assert: Verify that concrete fields were not replaced
        Assert.Equal("MIT", primary.ConcludedLicense);
        Assert.Equal("Copyright 2024 DEMA Consulting", primary.CopyrightText);
        Assert.Equal("MIT license confirmed", primary.LicenseComments);
    }

    /// <summary>
    ///     Tests that attribution text entries are merged by concatenation and deduplication.
    /// </summary>
    /// <remarks>
    ///     Verifies that unique entries from the source are appended to the target's
    ///     <c>AttributionText</c> array while duplicate entries are discarded so that each
    ///     attribution notice appears exactly once in the merged result.
    /// </remarks>
    [Fact]
    public void SpdxLicenseElement_Enhance_AttributionText_MergedByDeduplication()
    {
        // Arrange: Create packages with overlapping and unique attribution texts
        var primary = new SpdxPackage
        {
            Name = "TestPackage",
            Version = "1.0.0",
            AttributionText = ["Attribution A", "Attribution B"]
        };
        var secondary = new SpdxPackage
        {
            Name = "TestPackage",
            Version = "1.0.0",
            AttributionText = ["Attribution B", "Attribution C"]
        };

        // Act: Enhance the primary package with the secondary
        primary.Enhance(secondary);

        // Assert: Verify that attribution texts were merged with deduplication
        Assert.Equal(3, primary.AttributionText.Length);
        Assert.Contains("Attribution A", primary.AttributionText);
        Assert.Contains("Attribution B", primary.AttributionText);
        Assert.Contains("Attribution C", primary.AttributionText);
    }

    /// <summary>
    ///     Tests that annotations are merged by identity-match and append.
    /// </summary>
    /// <remarks>
    ///     Verifies that annotations matching an existing entry are recognized as the same
    ///     (identity-match on all four fields) and that annotations with no matching entry in
    ///     the primary are appended as new independent copies, leaving the total annotation
    ///     count equal to the number of distinct annotations across both sources.
    /// </remarks>
    [Fact]
    public void SpdxLicenseElement_Enhance_Annotations_MergedByIdentityAndAppend()
    {
        // Arrange: Create packages where primary has one annotation and secondary adds a new one
        var primary = new SpdxPackage
        {
            Name = "TestPackage",
            Version = "1.0.0",
            Annotations =
            [
                new SpdxAnnotation
                {
                    Annotator = "Tool: tool-a",
                    Date = "2024-01-01T00:00:00Z",
                    Type = SpdxAnnotationType.Review,
                    Comment = "Initial review"
                }
            ]
        };
        var secondary = new SpdxPackage
        {
            Name = "TestPackage",
            Version = "1.0.0",
            Annotations =
            [
                new SpdxAnnotation
                {
                    Annotator = "Tool: tool-a",
                    Date = "2024-01-01T00:00:00Z",
                    Type = SpdxAnnotationType.Review,
                    Comment = "Initial review"
                },
                new SpdxAnnotation
                {
                    Annotator = "Tool: tool-b",
                    Date = "2024-02-01T00:00:00Z",
                    Type = SpdxAnnotationType.Other,
                    Comment = "Additional review"
                }
            ]
        };

        // Act: Enhance the primary package with the secondary
        primary.Enhance(secondary);

        // Assert: Verify that annotations were merged by identity-match and append
        Assert.Equal(2, primary.Annotations.Length);
        Assert.Equal("Tool: tool-a", primary.Annotations[0].Annotator);
        Assert.Equal("Initial review", primary.Annotations[0].Comment);
        Assert.Equal("Tool: tool-b", primary.Annotations[1].Annotator);
        Assert.Equal("Additional review", primary.Annotations[1].Comment);
    }
}
