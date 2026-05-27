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
///     Tests for the <see cref="SpdxExtractedLicensingInfo" /> class.
/// </summary>
/// <remarks>
///     Covers the Same equality comparer, DeepCopy, Enhance merge, and Validate methods.
/// </remarks>
public class SpdxExtractedLicensingInfoTests
{
    /// <summary>
    ///     Tests the <see cref="SpdxExtractedLicensingInfo.Same" /> comparer compares extracted licensing infos correctly.
    /// </summary>
    /// <remarks>
    ///     Validates that the Same comparer treats two instances with identical ExtractedText as equal
    ///     regardless of other field differences, treats instances with differing ExtractedText as
    ///     distinct, handles reference equality, null arguments, and produces consistent hash codes
    ///     for equal instances.
    /// </remarks>
    [Fact]
    public void SpdxExtractedLicensingInfo_SameComparer_ComparesCorrectly()
    {
        // Arrange: Create three extracted licensing infos with different properties
        var l1 = new SpdxExtractedLicensingInfo
        {
            LicenseId = "LicenseRef-1",
            ExtractedText = "The CyberNeko Software License"
        };
        var l2 = new SpdxExtractedLicensingInfo
        {
            LicenseId = "LicenseRef-1",
            ExtractedText = "The CyberNeko Software License",
            Comment = "Extracted from files"
        };
        var l3 = new SpdxExtractedLicensingInfo
        {
            LicenseId = "LicenseRef-2",
            ExtractedText = "Some Random License"
        };

        // Act / Assert: Verify extracted-licensing-infos compare to themselves
        Assert.True(SpdxExtractedLicensingInfo.Same.Equals(l1, l1));
        Assert.True(SpdxExtractedLicensingInfo.Same.Equals(l2, l2));
        Assert.True(SpdxExtractedLicensingInfo.Same.Equals(l3, l3));

        // Assert: Verify extracted-licensing-infos compare correctly
        Assert.True(SpdxExtractedLicensingInfo.Same.Equals(l1, l2));
        Assert.True(SpdxExtractedLicensingInfo.Same.Equals(l2, l1));
        Assert.False(SpdxExtractedLicensingInfo.Same.Equals(l1, l3));
        Assert.False(SpdxExtractedLicensingInfo.Same.Equals(l3, l1));
        Assert.False(SpdxExtractedLicensingInfo.Same.Equals(l2, l3));
        Assert.False(SpdxExtractedLicensingInfo.Same.Equals(l3, l2));

        // Assert: Verify null handling
        Assert.True(SpdxExtractedLicensingInfo.Same.Equals(null!, null!));
        Assert.False(SpdxExtractedLicensingInfo.Same.Equals(null!, l1));
        Assert.False(SpdxExtractedLicensingInfo.Same.Equals(l1, null!));

        // Assert: Verify same extracted-licensing-infos have identical hashes
        Assert.Equal(SpdxExtractedLicensingInfo.Same.GetHashCode(l1),
            SpdxExtractedLicensingInfo.Same.GetHashCode(l2));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxExtractedLicensingInfo.DeepCopy" /> method.
    /// </summary>
    /// <remarks>
    ///     Validates that DeepCopy produces a new instance with field values equal to the original
    ///     and that arrays are independently copied (no shared references between original and copy).
    /// </remarks>
    [Fact]
    public void SpdxExtractedLicensingInfo_DeepCopy_CreatesEqualButDistinctInstance()
    {
        // Arrange: Create an extracted licensing info object
        var l1 = new SpdxExtractedLicensingInfo
        {
            LicenseId = "LicenseRef-1",
            ExtractedText = "The CyberNeko Software License",
            CrossReferences = ["https://example.com/license"],
            Comment = "Extracted from files"
        };

        // Act: Create a deep copy of the object
        var l2 = l1.DeepCopy();

        // Assert: Verify deep-copy is equal to original
        Assert.Equal(l1, l2, SpdxExtractedLicensingInfo.Same);
        Assert.Equal(l1.LicenseId, l2.LicenseId);
        Assert.Equal(l1.ExtractedText, l2.ExtractedText);
        Assert.Equal(l1.Comment, l2.Comment);

        // Assert: Verify deep-copy has distinct instance
        Assert.False(ReferenceEquals(l1, l2));
        Assert.False(ReferenceEquals(l1.CrossReferences, l2.CrossReferences));
    }

    /// <summary>
    ///     Tests the
    ///     <see cref="SpdxExtractedLicensingInfo.Enhance(SpdxExtractedLicensingInfo[], SpdxExtractedLicensingInfo[])" />
    ///     method adds or updates information correctly.
    /// </summary>
    /// <remarks>
    ///     Validates that the static Enhance merges arrays by enhancing matching entries (matched
    ///     by ExtractedText) and appending unmatched entries as new deep copies.
    /// </remarks>
    [Fact]
    public void SpdxExtractedLicensingInfo_Enhance_AddsOrUpdatesInformationCorrectly()
    {
        // Arrange: Create an array of extracted licensing infos
        var infos = new[]
        {
            new SpdxExtractedLicensingInfo
            {
                LicenseId = "LicenseRef-1",
                ExtractedText = "The CyberNeko Software License"
            }
        };

        // Act: Enhance the extracted licensing infos with additional infos
        infos = SpdxExtractedLicensingInfo.Enhance(
            infos,
            [
                new SpdxExtractedLicensingInfo
                {
                    LicenseId = "LicenseRef-1",
                    ExtractedText = "The CyberNeko Software License",
                    Comment = "Extracted from files"
                },
                new SpdxExtractedLicensingInfo
                {
                    LicenseId = "LicenseRef-2",
                    ExtractedText = "Some Random License"
                }
            ]);

        // Assert: Verify the infos array has correct information
        Assert.Equal(2, infos.Length);
        Assert.Equal("LicenseRef-1", infos[0].LicenseId);
        Assert.Equal("The CyberNeko Software License", infos[0].ExtractedText);
        Assert.Equal("Extracted from files", infos[0].Comment);
        Assert.Equal("LicenseRef-2", infos[1].LicenseId);
        Assert.Equal("Some Random License", infos[1].ExtractedText);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxExtractedLicensingInfo.Validate" /> method returns no issues for a valid input.
    /// </summary>
    /// <remarks>
    ///     Validates that Validate reports no issues when both LicenseId and ExtractedText are
    ///     non-empty, confirming the happy-path behavior of the validation logic.
    /// </remarks>
    [Fact]
    public void SpdxExtractedLicensingInfo_Validate_ValidInput_ReturnsNoIssues()
    {
        // Arrange: Create a valid extracted licensing info
        var info = new SpdxExtractedLicensingInfo
        {
            LicenseId = "LicenseRef-1",
            ExtractedText = "The CyberNeko Software License"
        };

        // Act: Perform validation on the SpdxExtractedLicensingInfo instance.
        var issues = new List<string>();
        info.Validate(issues);

        // Assert: Verify that the validation reports no issues
        Assert.Empty(issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxExtractedLicensingInfo.Validate" /> method reports bad license IDs
    /// </summary>
    /// <remarks>
    ///     Validates that Validate appends an issue message to the supplied list when LicenseId is
    ///     empty, confirming the LicenseId validation path.
    /// </remarks>
    [Fact]
    public void SpdxExtractedLicensingInfo_Validate_InvalidLicenseId_ReportsIssue()
    {
        // Arrange: Create a bad licensing info
        var info = new SpdxExtractedLicensingInfo
        {
            LicenseId = "",
            ExtractedText = "The CyberNeko Software License"
        };

        // Act: Perform validation on the SpdxExtractedLicensingInfo instance.
        var issues = new List<string>();
        info.Validate(issues);

        // Assert: Verify that the validation fails and the error message includes the description
        Assert.Contains(issues, issue => issue.Contains("Extracted License Information Invalid License ID Field - Empty"));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxExtractedLicensingInfo.Validate" /> method reports bad extracted text
    /// </summary>
    /// <remarks>
    ///     Validates that Validate appends an issue message to the supplied list when ExtractedText
    ///     is empty, confirming the ExtractedText validation path.
    /// </remarks>
    [Fact]
    public void SpdxExtractedLicensingInfo_Validate_InvalidExtractedText_ReportsIssue()
    {
        // Arrange: Create a bad licensing info
        var info = new SpdxExtractedLicensingInfo
        {
            LicenseId = "LicenseRef-1",
            ExtractedText = ""
        };

        // Act: Perform validation on the SpdxExtractedLicensingInfo instance.
        var issues = new List<string>();
        info.Validate(issues);

        // Assert: Verify that the validation fails and the error message includes the description
        Assert.Contains(issues, issue => issue.Contains("Extracted License Information 'LicenseRef-1' Invalid Extracted Text Field - Empty"));
    }
}
