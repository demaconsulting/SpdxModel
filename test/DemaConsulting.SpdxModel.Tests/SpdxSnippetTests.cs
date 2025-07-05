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
/// Tests for the <see cref="SpdxSnippet"/> class.
/// </summary>
[TestClass]
public class SpdxSnippetTests
{
    /// <summary>
    ///     Tests the <see cref="SpdxSnippet.Same"/> comparer compares snippets correctly.
    /// </summary>
    [TestMethod]
    public void SpdxSnippet_SameComparer_ComparesCorrectly()
    {
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

        // Assert snippets compare to themselves
        Assert.IsTrue(SpdxSnippet.Same.Equals(s1, s1));
        Assert.IsTrue(SpdxSnippet.Same.Equals(s2, s2));
        Assert.IsTrue(SpdxSnippet.Same.Equals(s3, s3));

        // Assert snippets compare correctly
        Assert.IsTrue(SpdxSnippet.Same.Equals(s1, s2));
        Assert.IsTrue(SpdxSnippet.Same.Equals(s2, s1));
        Assert.IsFalse(SpdxSnippet.Same.Equals(s1, s3));
        Assert.IsFalse(SpdxSnippet.Same.Equals(s3, s1));
        Assert.IsFalse(SpdxSnippet.Same.Equals(s2, s3));
        Assert.IsFalse(SpdxSnippet.Same.Equals(s3, s2));

        // Assert same snippets have identical hashes
        Assert.AreEqual(SpdxSnippet.Same.GetHashCode(s1), SpdxSnippet.Same.GetHashCode(s2));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxSnippet.DeepCopy"/> method successfully creates a deep copy.
    /// </summary>
    [TestMethod]
    public void SpdxSnippet_DeepCopy_CreatesEqualButDistinctInstance()
    {
        // Arrange: Create a SpdxSnippet instance with various properties
        var s1 = new SpdxSnippet
        {
            SnippetFromFile = "SPDXRef-File1",
            SnippetByteStart = 100,
            SnippetByteEnd = 200,
            Comment = "Found snippet",
            ConcludedLicense = "MIT"
        };

        // Act: Create a deep copy of the SpdxSnippet instance
        var s2 = s1.DeepCopy();

        // Assert: Verify the deep-copy is equal to the original
        Assert.AreEqual(s1, s2, SpdxSnippet.Same);
        Assert.AreEqual(s1.SnippetFromFile, s2.SnippetFromFile);
        Assert.AreEqual(s1.SnippetByteStart, s2.SnippetByteStart);
        Assert.AreEqual(s1.SnippetByteEnd, s2.SnippetByteEnd);
        Assert.AreEqual(s1.Comment, s2.Comment);
        Assert.AreEqual(s1.ConcludedLicense, s2.ConcludedLicense);

        // Assert: Verify the deep-copy is a distinct instance
        Assert.IsFalse(ReferenceEquals(s1, s2));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxSnippet.Enhance(SpdxSnippet[], SpdxSnippet[])"/> method adds or updates information correctly.
    /// </summary>
    [TestMethod]
    public void SpdxSnippet_Enhance_AddsOrUpdatesInformationCorrectly()
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
        Assert.AreEqual(2, snippets.Length);
        Assert.AreEqual("SPDXRef-File1", snippets[0].SnippetFromFile);
        Assert.AreEqual(100, snippets[0].SnippetByteStart);
        Assert.AreEqual(200, snippets[0].SnippetByteEnd);
        Assert.AreEqual("Found snippet", snippets[0].Comment);
        Assert.AreEqual("MIT", snippets[0].ConcludedLicense);
        Assert.AreEqual("SPDXRef-File2", snippets[1].SnippetFromFile);
        Assert.AreEqual(10, snippets[1].SnippetByteStart);
        Assert.AreEqual(40, snippets[1].SnippetByteEnd);
    }


    /// <summary>
    ///     Tests that an invalid snippet ID fails validation.
    /// </summary>
    [TestMethod]
    public void SpdxSnippet_Validate_ReportsInvalidSnippetId()
    {
        // Arrange: Create a SpdxSnippet with an invalid ID
        var snippet = new SpdxSnippet
        {
            Id = "Invalid_ID",
            SnippetFromFile = "SPDXRef-File1",
            SnippetByteStart = 100,
            SnippetByteEnd = 200
        };

        // Act: Validate the snippet
        var issues = new List<string>();
        snippet.Validate(issues);

        // Assert: Check that the issues list contains the expected error message
        Assert.IsTrue(
            issues.Any(issue => issue.Contains("Snippet Invalid SPDX Identifier Field")));
    }
}