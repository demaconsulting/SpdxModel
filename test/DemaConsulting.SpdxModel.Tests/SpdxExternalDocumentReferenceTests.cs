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
///     Tests for the <see cref="SpdxExternalDocumentReference" /> class.
/// </summary>
/// <remarks>
///     Tests the <see cref="SpdxExternalDocumentReference"/> class using xUnit v3. Each test method constructs
///     its own instance state with no shared fixture, covering the Same comparer, DeepCopy,
///     Enhance, and Validate methods.
/// </remarks>
public class SpdxExternalDocumentReferenceTests
{
    /// <summary>
    ///     Tests the <see cref="SpdxExternalDocumentReference.Same" /> comparer compares external document references
    ///     correctly.
    /// </summary>
    /// <remarks>
    ///     Constructs three references: r1 and r2 share the same Document URI (making them
    ///     equal under the Same comparer) while r3 has a different URI. Verifies reflexive,
    ///     symmetric, and cross-inequality comparisons, and that equal references produce
    ///     identical hash codes.
    /// </remarks>
    [Fact]
    public void SpdxExternalDocumentReference_SameComparer_SameDocument_ReturnsEqual()
    {
        // Arrange: Create three external document references with different properties
        var r1 = new SpdxExternalDocumentReference
        {
            ExternalDocumentId = "DocumentRef-spdx-tool-1.2",
            Checksum = new SpdxChecksum
            {
                Algorithm = SpdxChecksumAlgorithm.Sha1,
                Value = "d6a770ba38583ed4bb4525bd96e50461655d2759"
            },
            Document = "http://spdx.org/spdxdocs/spdx-tools-v1.2-3F2504E0-4F89-41D3-9A0C-0305E82C3301"
        };
        var r2 = new SpdxExternalDocumentReference
        {
            ExternalDocumentId = "DocumentRef-Reference",
            Checksum = new SpdxChecksum
            {
                Algorithm = SpdxChecksumAlgorithm.Sha1,
                Value = "d6a770ba38583ed4bb4525bd96e50461655d2759"
            },
            Document = "http://spdx.org/spdxdocs/spdx-tools-v1.2-3F2504E0-4F89-41D3-9A0C-0305E82C3301"
        };
        var r3 = new SpdxExternalDocumentReference
        {
            ExternalDocumentId = "DocumentRef-OtherDoc",
            Checksum = new SpdxChecksum
            {
                Algorithm = SpdxChecksumAlgorithm.Sha1,
                Value = "c2b4e1c67a2d28fced849ee1bb76e7391b93f125"
            },
            Document = "http://demo.com/some-document"
        };

        // Act / Assert: Verify external-document-references compare to themselves
        Assert.True(SpdxExternalDocumentReference.Same.Equals(r1, r1));
        Assert.True(SpdxExternalDocumentReference.Same.Equals(r2, r2));
        Assert.True(SpdxExternalDocumentReference.Same.Equals(r3, r3));

        // Assert: Verify external-document-references compare correctly
        Assert.True(SpdxExternalDocumentReference.Same.Equals(r1, r2));
        Assert.True(SpdxExternalDocumentReference.Same.Equals(r2, r1));
        Assert.False(SpdxExternalDocumentReference.Same.Equals(r1, r3));
        Assert.False(SpdxExternalDocumentReference.Same.Equals(r3, r1));
        Assert.False(SpdxExternalDocumentReference.Same.Equals(r2, r3));
        Assert.False(SpdxExternalDocumentReference.Same.Equals(r3, r2));

        // Assert: Verify same external-document-references have identical hashes
        Assert.Equal(SpdxExternalDocumentReference.Same.GetHashCode(r1),
            SpdxExternalDocumentReference.Same.GetHashCode(r2));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxExternalDocumentReference.DeepCopy" /> method successfully creates a deep copy.
    /// </summary>
    /// <remarks>
    ///     Creates a fully-populated external document reference with a checksum and deep-copies
    ///     it. Verifies that the copy has equal field values but that both the top-level reference
    ///     and the nested Checksum are distinct object references from the original.
    /// </remarks>
    [Fact]
    public void SpdxExternalDocumentReference_DeepCopy_ValidInstance_ReturnsEqualButDistinctInstance()
    {
        // Arrange: Create an external document reference with a checksum
        var r1 = new SpdxExternalDocumentReference
        {
            ExternalDocumentId = "DocumentRef-spdx-tool-1.2",
            Checksum = new SpdxChecksum
            {
                Algorithm = SpdxChecksumAlgorithm.Sha1,
                Value = "d6a770ba38583ed4bb4525bd96e50461655d2759"
            },
            Document = "http://spdx.org/spdxdocs/spdx-tools-v1.2-3F2504E0-4F89-41D3-9A0C-0305E82C3301"
        };

        // Act: Create a deep copy of the original external document reference
        var r2 = r1.DeepCopy();

        // Assert: Verify deep-copy is equal to original
        Assert.Equal(r1, r2, SpdxExternalDocumentReference.Same);
        Assert.Equal(r1.ExternalDocumentId, r2.ExternalDocumentId);
        Assert.Equal(r1.Checksum, r2.Checksum, SpdxChecksum.Same);
        Assert.Equal(r1.Document, r2.Document);

        // Assert: Verify deep-copy has distinct instances
        Assert.False(ReferenceEquals(r1, r2));
        Assert.False(ReferenceEquals(r1.Checksum, r2.Checksum));
    }

    /// <summary>
    ///     Tests the
    ///     <see cref="SpdxExternalDocumentReference.Enhance(SpdxExternalDocumentReference[], SpdxExternalDocumentReference[])" />
    ///     method adds or updates information correctly.
    /// </summary>
    /// <remarks>
    ///     Starts with a single reference that lacks a checksum, then enhances with a list
    ///     containing one entry that updates the existing reference's checksum and one entirely
    ///     new reference. Verifies that the merged array has exactly two entries with the correct
    ///     checksum data and new reference details.
    /// </remarks>
    [Fact]
    public void SpdxExternalDocumentReference_Enhance_WithNewAndMatchingEntries_MergesAndAppendsCorrectly()
    {
        // Arrange: Create an array of external document references
        var references = new[]
        {
            new SpdxExternalDocumentReference
            {
                ExternalDocumentId = "DocumentRef-spdx-tool-1.2",
                Document = "http://spdx.org/spdxdocs/spdx-tools-v1.2-3F2504E0-4F89-41D3-9A0C-0305E82C3301"
            }
        };

        // Act: Enhance the external document references with additional references
        references = SpdxExternalDocumentReference.Enhance(
            references,
            [
                new SpdxExternalDocumentReference
                {
                    Checksum = new SpdxChecksum
                    {
                        Algorithm = SpdxChecksumAlgorithm.Sha1,
                        Value = "d6a770ba38583ed4bb4525bd96e50461655d2759"
                    },
                    Document = "http://spdx.org/spdxdocs/spdx-tools-v1.2-3F2504E0-4F89-41D3-9A0C-0305E82C3301"
                },
                new SpdxExternalDocumentReference
                {
                    ExternalDocumentId = "DocumentRef-OtherDoc",
                    Checksum = new SpdxChecksum
                    {
                        Algorithm = SpdxChecksumAlgorithm.Sha1,
                        Value = "c2b4e1c67a2d28fced849ee1bb76e7391b93f125"
                    },
                    Document = "http://demo.com/some-document"
                }
            ]);

        // Assert: Verify the references array has correct information
        Assert.Equal(2, references.Length);
        Assert.Equal("DocumentRef-spdx-tool-1.2", references[0].ExternalDocumentId);
        Assert.Equal(SpdxChecksumAlgorithm.Sha1, references[0].Checksum.Algorithm);
        Assert.Equal("d6a770ba38583ed4bb4525bd96e50461655d2759", references[0].Checksum.Value);
        Assert.Equal("http://spdx.org/spdxdocs/spdx-tools-v1.2-3F2504E0-4F89-41D3-9A0C-0305E82C3301",
            references[0].Document);
        Assert.Equal("DocumentRef-OtherDoc", references[1].ExternalDocumentId);
        Assert.Equal(SpdxChecksumAlgorithm.Sha1, references[1].Checksum.Algorithm);
        Assert.Equal("c2b4e1c67a2d28fced849ee1bb76e7391b93f125", references[1].Checksum.Value);
        Assert.Equal("http://demo.com/some-document", references[1].Document);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxExternalDocumentReference.Validate" /> method reports missing external document ID
    /// </summary>
    /// <remarks>
    ///     Sets ExternalDocumentId to an empty string — the minimal invalid state — to confirm
    ///     that the validator catches the absent ID and includes the expected description string
    ///     in the reported issue.
    /// </remarks>
    [Fact]
    public void SpdxExternalDocumentReference_Validate_MissingId_ReportsIssue()
    {
        // Arrange: Create a bad reference
        var reference = new SpdxExternalDocumentReference
        {
            ExternalDocumentId = "",
            Document = "http://spdx.org/spdxdocs/spdx-tools-v1.2-3F2504E0-4F89-41D3-9A0C-0305E82C3301"
        };

        // Act: Perform validation on the SpdxExternalDocumentReference instance.
        var issues = new List<string>();
        reference.Validate(issues);

        // Assert: Verify that the validation fails and the error message includes the description
        Assert.Contains(issues, issue => issue.Contains("External Document Reference Invalid External Document ID Field - Empty"));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxExternalDocumentReference.Validate" /> method reports missing document URI
    /// </summary>
    /// <remarks>
    ///     Sets the Document URI to an empty string with a valid ExternalDocumentId to confirm
    ///     that the validator catches the absent URI and includes the expected description string
    ///     (including the reference ID) in the reported issue.
    /// </remarks>
    [Fact]
    public void SpdxExternalDocumentReference_Validate_MissingDocument_ReportsIssue()
    {
        // Arrange: Create a bad reference
        var reference = new SpdxExternalDocumentReference
        {
            ExternalDocumentId = "DocumentRef-spdx-tool-1.2",
            Document = ""
        };

        // Act: Perform validation on the SpdxExternalDocumentReference instance.
        var issues = new List<string>();
        reference.Validate(issues);

        // Assert: Verify that the validation fails and the error message includes the description
        Assert.Contains(issues, issue => issue.Contains("External Document Reference 'DocumentRef-spdx-tool-1.2' Invalid SPDX Document URI Field - Empty"));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxExternalDocumentReference.Validate" /> method reports an invalid checksum.
    /// </summary>
    /// <remarks>
    ///     Constructs a reference with a <see cref="SpdxChecksumAlgorithm.Missing"/> algorithm
    ///     and an empty checksum value to confirm that the validator delegates to the checksum
    ///     validator and surfaces the algorithm-missing diagnostic in the reported issues.
    /// </remarks>
    [Fact]
    public void SpdxExternalDocumentReference_Validate_InvalidChecksum_ReportsIssue()
    {
        // Arrange: Create a reference with a missing-algorithm checksum
        var reference = new SpdxExternalDocumentReference
        {
            ExternalDocumentId = "DocumentRef-spdx-tool-1.2",
            Checksum = new SpdxChecksum
            {
                Algorithm = SpdxChecksumAlgorithm.Missing,
                Value = ""
            },
            Document = "http://spdx.org/spdxdocs/spdx-tools-v1.2-3F2504E0-4F89-41D3-9A0C-0305E82C3301"
        };

        // Act: Perform validation
        var issues = new List<string>();
        reference.Validate(issues);

        // Assert: Verify that the checksum algorithm issue is reported
        Assert.Contains(
            issues,
            issue => issue.Contains("Invalid Checksum Algorithm Field - Missing"));
    }
}
