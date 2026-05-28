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
///     Tests for the <see cref="SpdxSnippet" /> class.
/// </summary>
/// <remarks>
///     Covers equality comparison via the <see cref="SpdxSnippet.Same" /> comparer, deep-copy independence,
///     field merging via <see cref="SpdxSnippet.Enhance(SpdxSnippet[],SpdxSnippet[])" />, and full
///     validation of required fields and byte range constraints. Each test exercises a single scenario or
///     boundary condition in isolation with no shared state between tests.
/// </remarks>
public class SpdxSnippetTests
{
    /// <summary>
    ///     Tests the <see cref="SpdxSnippet.Same" /> comparer compares snippets correctly.
    /// </summary>
    /// <remarks>
    ///     Verifies that two snippets with the same <c>SnippetFromFile</c>, <c>SnippetByteStart</c>, and
    ///     <c>SnippetByteEnd</c> are considered equal even when other fields differ, and that snippets
    ///     with different file or byte range are distinct.
    /// </remarks>
    [Fact]
    public void SpdxSnippet_SameComparer_SameFileAndByteRange_ReturnsEqual()
    {
        // Arrange: Create two snippets with the same byte range and one distinct snippet
        var s1 = new SpdxSnippet
        {
            SnippetFromFile = "SPDXRef-File1",
            SnippetByteStart = 100,
            SnippetByteEnd = 200
        };

        var s2 = new SpdxSnippet
        {
            SnippetFromFile = "SPDXRef-File1",
            SnippetByteStart = 100,
            SnippetByteEnd = 200,
            Comment = "Found snippet",
            ConcludedLicense = "MIT"
        };

        var s3 = new SpdxSnippet
        {
            SnippetFromFile = "SPDXRef-File2",
            SnippetByteStart = 10,
            SnippetByteEnd = 40
        };

        // Act / Assert: Verify snippets compare to themselves
        Assert.True(SpdxSnippet.Same.Equals(s1, s1));
        Assert.True(SpdxSnippet.Same.Equals(s2, s2));
        Assert.True(SpdxSnippet.Same.Equals(s3, s3));

        // Assert: snippets compare correctly
        Assert.True(SpdxSnippet.Same.Equals(s1, s2));
        Assert.True(SpdxSnippet.Same.Equals(s2, s1));
        Assert.False(SpdxSnippet.Same.Equals(s1, s3));
        Assert.False(SpdxSnippet.Same.Equals(s3, s1));
        Assert.False(SpdxSnippet.Same.Equals(s2, s3));
        Assert.False(SpdxSnippet.Same.Equals(s3, s2));

        // Assert: same snippets have identical hashes
        Assert.Equal(SpdxSnippet.Same.GetHashCode(s1), SpdxSnippet.Same.GetHashCode(s2));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxSnippet.DeepCopy" /> method successfully creates a deep copy.
    /// </summary>
    /// <remarks>
    ///     Verifies that the returned instance has equal field values for all properties including
    ///     array fields, is a distinct object reference from the original, and that all array fields
    ///     are independent instances so that mutating the copy does not affect the original.
    /// </remarks>
    [Fact]
    public void SpdxSnippet_DeepCopy_FullyPopulatedSnippet_CreatesEqualButDistinctCopy()
    {
        // Arrange: Create a fully-populated SpdxSnippet instance with all fields set
        var s1 = new SpdxSnippet
        {
            Id = "SPDXRef-Snippet",
            SnippetFromFile = "SPDXRef-File1",
            SnippetByteStart = 100,
            SnippetByteEnd = 200,
            SnippetLineStart = 5,
            SnippetLineEnd = 10,
            ConcludedLicense = "MIT",
            LicenseInfoInSnippet = ["MIT", "Apache-2.0"],
            LicenseComments = "License comment",
            CopyrightText = "Copyright(c) 2024 DEMA Consulting",
            Comment = "Found snippet",
            Name = "MySnippet",
            AttributionText = ["Attribution text"],
            Annotations =
            [
                new SpdxAnnotation
                {
                    Annotator = "Tool: test-tool",
                    Date = "2024-05-28T01:30:00Z",
                    Type = SpdxAnnotationType.Review,
                    Comment = "Reviewed"
                }
            ]
        };

        // Act: Create a deep copy of the SpdxSnippet instance
        var s2 = s1.DeepCopy();

        // Assert: Verify the deep-copy has equal field values to the original
        Assert.Equal(s1, s2, SpdxSnippet.Same);
        Assert.Equal(s1.Id, s2.Id);
        Assert.Equal(s1.SnippetFromFile, s2.SnippetFromFile);
        Assert.Equal(s1.SnippetByteStart, s2.SnippetByteStart);
        Assert.Equal(s1.SnippetByteEnd, s2.SnippetByteEnd);
        Assert.Equal(s1.SnippetLineStart, s2.SnippetLineStart);
        Assert.Equal(s1.SnippetLineEnd, s2.SnippetLineEnd);
        Assert.Equal(s1.ConcludedLicense, s2.ConcludedLicense);
        Assert.Equal(s1.LicenseInfoInSnippet, s2.LicenseInfoInSnippet);
        Assert.Equal(s1.LicenseComments, s2.LicenseComments);
        Assert.Equal(s1.CopyrightText, s2.CopyrightText);
        Assert.Equal(s1.Comment, s2.Comment);
        Assert.Equal(s1.Name, s2.Name);
        Assert.Equal(s1.AttributionText, s2.AttributionText);

        // Assert: Verify the deep-copy is a distinct instance with independent array references
        Assert.False(ReferenceEquals(s1, s2));
        Assert.False(ReferenceEquals(s1.LicenseInfoInSnippet, s2.LicenseInfoInSnippet));
        Assert.False(ReferenceEquals(s1.AttributionText, s2.AttributionText));
        Assert.False(ReferenceEquals(s1.Annotations, s2.Annotations));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxSnippet.Enhance(SpdxSnippet[], SpdxSnippet[])" /> method adds or updates information
    ///     correctly.
    /// </summary>
    /// <remarks>
    ///     Verifies that a matching snippet (same file and byte range) is enhanced in place and that a non-matching
    ///     snippet from the source array is deep-copied and appended, resulting in an array of length two.
    /// </remarks>
    [Fact]
    public void SpdxSnippet_Enhance_MatchingAndNewSnippets_MergesCorrectly()
    {
        // Arrange: Create an array of SpdxSnippet objects
        var snippets = new[]
        {
            new SpdxSnippet
            {
                SnippetFromFile = "SPDXRef-File1",
                SnippetByteStart = 100,
                SnippetByteEnd = 200
            }
        };

        // Act: Enhance the snippets with additional information
        snippets = SpdxSnippet.Enhance(
            snippets,
            [
                new SpdxSnippet
                {
                    SnippetFromFile = "SPDXRef-File1",
                    SnippetByteStart = 100,
                    SnippetByteEnd = 200,
                    Comment = "Found snippet",
                    ConcludedLicense = "MIT"
                },
                new SpdxSnippet
                {
                    SnippetFromFile = "SPDXRef-File2",
                    SnippetByteStart = 10,
                    SnippetByteEnd = 40
                }
            ]);

        // Assert: Check that the snippets array has been enhanced correctly
        Assert.Equal(2, snippets.Length);
        Assert.Equal("SPDXRef-File1", snippets[0].SnippetFromFile);
        Assert.Equal(100, snippets[0].SnippetByteStart);
        Assert.Equal(200, snippets[0].SnippetByteEnd);
        Assert.Equal("Found snippet", snippets[0].Comment);
        Assert.Equal("MIT", snippets[0].ConcludedLicense);
        Assert.Equal("SPDXRef-File2", snippets[1].SnippetFromFile);
        Assert.Equal(10, snippets[1].SnippetByteStart);
        Assert.Equal(40, snippets[1].SnippetByteEnd);
    }

    /// <summary>
    ///     Tests that an invalid snippet ID fails validation.
    /// </summary>
    /// <remarks>
    ///     Verifies that an <c>Id</c> not matching the <c>SPDXRef-</c> prefix format causes the
    ///     "Snippet Invalid SPDX Identifier Field" issue to be reported.
    /// </remarks>
    [Fact]
    public void SpdxSnippet_Validate_InvalidSnippetId_ReportsIssue()
    {
        // Arrange: Create a SpdxSnippet with an invalid ID
        var snippet = new SpdxSnippet
        {
            Id = "Invalid_ID",
            SnippetFromFile = "SPDXRef-File1",
            SnippetByteStart = 100,
            SnippetByteEnd = 200,
            ConcludedLicense = "MIT",
            CopyrightText = "Copyright(c) 2024 DEMA Consulting"
        };

        // Act: Validate the snippet
        var issues = new List<string>();
        snippet.Validate(issues);

        // Assert: Check that the issues list contains the expected error message
        Assert.Contains(issues, issue => issue.Contains("Snippet Invalid SPDX Identifier Field 'Invalid_ID'"));
    }

    /// <summary>
    ///     Tests that a valid snippet passes validation.
    /// </summary>
    /// <remarks>
    ///     Exercises the happy-path: a snippet with all required fields populated (valid SPDX ID,
    ///     non-empty <c>SnippetFromFile</c>, byte range ≥ 1, non-empty license, and copyright)
    ///     passes all validation checks without reporting any issues.
    /// </remarks>
    [Fact]
    public void SpdxSnippet_Validate_AllRequiredFieldsPresent_ReturnsNoIssues()
    {
        // Arrange: Create a valid SpdxSnippet
        var snippet = new SpdxSnippet
        {
            Id = "SPDXRef-Snippet",
            SnippetFromFile = "SPDXRef-File1",
            SnippetByteStart = 100,
            SnippetByteEnd = 200,
            ConcludedLicense = "MIT",
            CopyrightText = "Copyright(c) 2024 DEMA Consulting"
        };

        // Act: Validate the snippet
        var issues = new List<string>();
        snippet.Validate(issues);

        // Assert: Verify that the validation reports no issues.
        Assert.Empty(issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxSnippet.Validate" /> method validates annotations.
    /// </summary>
    /// <remarks>
    ///     Verifies that an annotation with an empty <c>Annotator</c> field causes the
    ///     "Invalid Annotator Field - Empty" issue to be reported with the correct snippet prefix.
    /// </remarks>
    [Fact]
    public void SpdxSnippet_Validate_InvalidAnnotation_ReportsIssue()
    {
        // Arrange: Create a valid snippet with an invalid annotation
        var snippet = new SpdxSnippet
        {
            Id = "SPDXRef-Snippet",
            SnippetFromFile = "SPDXRef-File1",
            SnippetByteStart = 100,
            SnippetByteEnd = 200,
            ConcludedLicense = "MIT",
            CopyrightText = "Copyright(c) 2024 DEMA Consulting",
            Annotations =
            [
                new SpdxAnnotation
                {
                    Annotator = "",
                    Date = "2024-05-28T01:30:00Z",
                    Type = SpdxAnnotationType.Review,
                    Comment = "Looks good"
                }
            ]
        };

        // Act: Validate the snippet
        var issues = new List<string>();
        snippet.Validate(issues);

        // Assert: Verify the annotation issue is reported with the correct prefix
        Assert.Contains(issues, issue => issue.Contains("Snippet 'SPDXRef-Snippet' Invalid Annotator Field - Empty"));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxSnippet.Validate" /> method reports an empty snippet-from-file field.
    /// </summary>
    /// <remarks>
    ///     Verifies the boundary condition where <c>SnippetFromFile</c> is empty: validation must report
    ///     the "Invalid Snippet From File Field - Empty" issue.
    /// </remarks>
    [Fact]
    public void SpdxSnippet_Validate_EmptySnippetFromFile_ReportsIssue()
    {
        // Arrange: Create a snippet with an empty SnippetFromFile
        var snippet = new SpdxSnippet
        {
            Id = "SPDXRef-Snippet",
            SnippetFromFile = "",
            SnippetByteStart = 100,
            SnippetByteEnd = 200,
            ConcludedLicense = "MIT",
            CopyrightText = "Copyright(c) 2024 DEMA Consulting"
        };

        // Act: Validate the snippet
        var issues = new List<string>();
        snippet.Validate(issues);

        // Assert: Verify the empty SnippetFromFile issue is reported
        Assert.Contains(issues, issue => issue.Contains("Snippet 'SPDXRef-Snippet' Invalid Snippet From File Field - Empty"));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxSnippet.Validate" /> method reports an invalid byte start value.
    /// </summary>
    /// <remarks>
    ///     Verifies the lower boundary condition: a <c>SnippetByteStart</c> value of 0 (less than the
    ///     required minimum of 1) causes the "Invalid Snippet Byte Range Start Field" issue to be reported.
    /// </remarks>
    [Fact]
    public void SpdxSnippet_Validate_InvalidByteStart_ReportsIssue()
    {
        // Arrange: Create a snippet with SnippetByteStart < 1
        var snippet = new SpdxSnippet
        {
            Id = "SPDXRef-Snippet",
            SnippetFromFile = "SPDXRef-File1",
            SnippetByteStart = 0,
            SnippetByteEnd = 200,
            ConcludedLicense = "MIT",
            CopyrightText = "Copyright(c) 2024 DEMA Consulting"
        };

        // Act: Validate the snippet
        var issues = new List<string>();
        snippet.Validate(issues);

        // Assert: Verify the invalid byte start issue is reported
        Assert.Contains(issues, issue => issue.Contains("Snippet 'SPDXRef-Snippet' Invalid Snippet Byte Range Start Field '0'"));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxSnippet.Validate" /> method reports an invalid byte end value.
    /// </summary>
    /// <remarks>
    ///     Verifies the range boundary condition: a <c>SnippetByteEnd</c> less than <c>SnippetByteStart</c>
    ///     causes the "Invalid Snippet Byte Range End Field" issue to be reported.
    /// </remarks>
    [Fact]
    public void SpdxSnippet_Validate_InvalidByteEnd_ReportsIssue()
    {
        // Arrange: Create a snippet where SnippetByteEnd is less than SnippetByteStart
        var snippet = new SpdxSnippet
        {
            Id = "SPDXRef-Snippet",
            SnippetFromFile = "SPDXRef-File1",
            SnippetByteStart = 100,
            SnippetByteEnd = 50,
            ConcludedLicense = "MIT",
            CopyrightText = "Copyright(c) 2024 DEMA Consulting"
        };

        // Act: Validate the snippet
        var issues = new List<string>();
        snippet.Validate(issues);

        // Assert: Verify the invalid byte end issue is reported
        Assert.Contains(issues, issue => issue.Contains("Snippet 'SPDXRef-Snippet' Invalid Snippet Byte Range End Field '50' < '100'"));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxSnippet.Validate" /> method reports an empty concluded license field.
    /// </summary>
    /// <remarks>
    ///     Verifies that an empty <c>ConcludedLicense</c> causes the "Invalid Concluded License Field - Empty"
    ///     issue to be reported.
    /// </remarks>
    [Fact]
    public void SpdxSnippet_Validate_EmptyConcludedLicense_ReportsIssue()
    {
        // Arrange: Create a snippet with an empty ConcludedLicense
        var snippet = new SpdxSnippet
        {
            Id = "SPDXRef-Snippet",
            SnippetFromFile = "SPDXRef-File1",
            SnippetByteStart = 100,
            SnippetByteEnd = 200,
            ConcludedLicense = "",
            CopyrightText = "Copyright(c) 2024 DEMA Consulting"
        };

        // Act: Validate the snippet
        var issues = new List<string>();
        snippet.Validate(issues);

        // Assert: Verify the empty ConcludedLicense issue is reported
        Assert.Contains(issues, issue => issue.Contains("Snippet 'SPDXRef-Snippet' Invalid Concluded License Field - Empty"));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxSnippet.Validate" /> method reports an empty copyright text field.
    /// </summary>
    /// <remarks>
    ///     Verifies that an empty <c>CopyrightText</c> causes the "Invalid Copyright Text Field - Empty"
    ///     issue to be reported.
    /// </remarks>
    [Fact]
    public void SpdxSnippet_Validate_EmptyCopyrightText_ReportsIssue()
    {
        // Arrange: Create a snippet with an empty CopyrightText
        var snippet = new SpdxSnippet
        {
            Id = "SPDXRef-Snippet",
            SnippetFromFile = "SPDXRef-File1",
            SnippetByteStart = 100,
            SnippetByteEnd = 200,
            ConcludedLicense = "MIT",
            CopyrightText = ""
        };

        // Act: Validate the snippet
        var issues = new List<string>();
        snippet.Validate(issues);

        // Assert: Verify the empty CopyrightText issue is reported
        Assert.Contains(issues, issue => issue.Contains("Snippet 'SPDXRef-Snippet' Invalid Copyright Text Field - Empty"));
    }
}
