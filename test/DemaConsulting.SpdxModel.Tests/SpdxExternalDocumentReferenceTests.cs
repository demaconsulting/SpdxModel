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
[TestClass]
public class SpdxExternalDocumentReferenceTests
{
    /// <summary>
    ///     Tests the <see cref="SpdxExternalDocumentReference.Same" /> comparer compares external document references
    ///     correctly.
    /// </summary>
    [TestMethod]
    public void SpdxExternalDocumentReference_SameComparer_ComparesCorrectly()
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

        // Assert: Verify external-document-references compare to themselves
        Assert.IsTrue(SpdxExternalDocumentReference.Same.Equals(r1, r1));
        Assert.IsTrue(SpdxExternalDocumentReference.Same.Equals(r2, r2));
        Assert.IsTrue(SpdxExternalDocumentReference.Same.Equals(r3, r3));

        // Assert: Verify external-document-references compare correctly
        Assert.IsTrue(SpdxExternalDocumentReference.Same.Equals(r1, r2));
        Assert.IsTrue(SpdxExternalDocumentReference.Same.Equals(r2, r1));
        Assert.IsFalse(SpdxExternalDocumentReference.Same.Equals(r1, r3));
        Assert.IsFalse(SpdxExternalDocumentReference.Same.Equals(r3, r1));
        Assert.IsFalse(SpdxExternalDocumentReference.Same.Equals(r2, r3));
        Assert.IsFalse(SpdxExternalDocumentReference.Same.Equals(r3, r2));

        // Assert: Verify same external-document-references have identical hashes
        Assert.AreEqual(SpdxExternalDocumentReference.Same.GetHashCode(r1),
            SpdxExternalDocumentReference.Same.GetHashCode(r2));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxExternalDocumentReference.DeepCopy" /> method successfully creates a deep copy.
    /// </summary>
    [TestMethod]
    public void SpdxExternalDocumentReference_DeepCopy_CreatesEqualButDistinctInstance()
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
        Assert.AreEqual(r1, r2, SpdxExternalDocumentReference.Same);
        Assert.AreEqual(r1.ExternalDocumentId, r2.ExternalDocumentId);
        Assert.AreEqual(r1.Checksum, r2.Checksum, SpdxChecksum.Same);
        Assert.AreEqual(r1.Document, r2.Document);

        // Assert: Verify deep-copy has distinct instances
        Assert.IsFalse(ReferenceEquals(r1, r2));
        Assert.IsFalse(ReferenceEquals(r1.Checksum, r2.Checksum));
    }

    /// <summary>
    ///     Tests the
    ///     <see cref="SpdxExternalDocumentReference.Enhance(SpdxExternalDocumentReference[], SpdxExternalDocumentReference[])" />
    ///     method adds or updates information correctly.
    /// </summary>
    [TestMethod]
    public void SpdxExternalDocumentReference_Enhance_AddsOrUpdatesInformationCorrectly()
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
        Assert.HasCount(2, references);
        Assert.AreEqual("DocumentRef-spdx-tool-1.2", references[0].ExternalDocumentId);
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, references[0].Checksum.Algorithm);
        Assert.AreEqual("d6a770ba38583ed4bb4525bd96e50461655d2759", references[0].Checksum.Value);
        Assert.AreEqual("http://spdx.org/spdxdocs/spdx-tools-v1.2-3F2504E0-4F89-41D3-9A0C-0305E82C3301",
            references[0].Document);
        Assert.AreEqual("DocumentRef-OtherDoc", references[1].ExternalDocumentId);
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, references[1].Checksum.Algorithm);
        Assert.AreEqual("c2b4e1c67a2d28fced849ee1bb76e7391b93f125", references[1].Checksum.Value);
        Assert.AreEqual("http://demo.com/some-document", references[1].Document);
    }
}