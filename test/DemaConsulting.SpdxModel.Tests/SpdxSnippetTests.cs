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
    /// Tests the <see cref="SpdxSnippet.Same"/> comparer.
    /// </summary>
    [TestMethod]
    public void SnippetSameComparer()
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
    /// Tests the <see cref="SpdxSnippet.DeepCopy"/> method.
    /// </summary>
    [TestMethod]
    public void DeepCopy()
    {
        var s1 = new SpdxSnippet
        {
            SnippetFromFile = "SPDXRef-File1",
            SnippetByteStart = 100,
            SnippetByteEnd = 200
        };

        var s2 = s1.DeepCopy();
        s2.SnippetFromFile = "SPDXRef-File2";

        Assert.IsFalse(ReferenceEquals(s1, s2));
        Assert.AreEqual("SPDXRef-File1", s1.SnippetFromFile);
        Assert.AreEqual("SPDXRef-File2", s2.SnippetFromFile);
    }

    /// <summary>
    /// Tests the <see cref="SpdxSnippet.Enhance(SpdxSnippet[], SpdxSnippet[])"/> method.
    /// </summary>
    [TestMethod]
    public void Enhance()
    {
        var snippets = new[]
        {
            new SpdxSnippet
            {
                SnippetFromFile = "SPDXRef-File1",
                SnippetByteStart = 100,
                SnippetByteEnd = 200
            }
        };

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
}