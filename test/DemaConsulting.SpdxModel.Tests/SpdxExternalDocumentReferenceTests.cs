namespace DemaConsulting.SpdxModel.Tests;

[TestClass]
public class SpdxExternalDocumentReferenceTests
{
    [TestMethod]
    public void ExternalDocumentReferenceSameComparer()
    {
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

        // Assert external-document-references compare to themselves
        Assert.IsTrue(SpdxExternalDocumentReference.Same.Equals(r1, r1));
        Assert.IsTrue(SpdxExternalDocumentReference.Same.Equals(r2, r2));
        Assert.IsTrue(SpdxExternalDocumentReference.Same.Equals(r3, r3));

        // Assert external-document-references compare correctly
        Assert.IsTrue(SpdxExternalDocumentReference.Same.Equals(r1, r2));
        Assert.IsTrue(SpdxExternalDocumentReference.Same.Equals(r2, r1));
        Assert.IsFalse(SpdxExternalDocumentReference.Same.Equals(r1, r3));
        Assert.IsFalse(SpdxExternalDocumentReference.Same.Equals(r3, r1));
        Assert.IsFalse(SpdxExternalDocumentReference.Same.Equals(r2, r3));
        Assert.IsFalse(SpdxExternalDocumentReference.Same.Equals(r3, r2));

        // Assert same external-document-references have identical hashes
        Assert.IsTrue(SpdxExternalDocumentReference.Same.GetHashCode(r1) == SpdxExternalDocumentReference.Same.GetHashCode(r2));
    }

    [TestMethod]
    public void DeepCopy()
    {
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

        var r2 = r1.DeepCopy();
        r2.Document = "http://demo.com/some-document";

        Assert.IsFalse(ReferenceEquals(r1, r2));
        Assert.AreEqual("http://spdx.org/spdxdocs/spdx-tools-v1.2-3F2504E0-4F89-41D3-9A0C-0305E82C3301", r1.Document);
        Assert.AreEqual("http://demo.com/some-document", r2.Document);
    }

    [TestMethod]
    public void Enhance()
    {
        var references = new[]
        {
            new SpdxExternalDocumentReference
            {
                ExternalDocumentId = "DocumentRef-spdx-tool-1.2",
                Document = "http://spdx.org/spdxdocs/spdx-tools-v1.2-3F2504E0-4F89-41D3-9A0C-0305E82C3301"
            }
        };

        references = SpdxExternalDocumentReference.Enhance(
            references,
            new[]
            {
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
            });

        Assert.AreEqual(2, references.Length);
        Assert.AreEqual("DocumentRef-spdx-tool-1.2", references[0].ExternalDocumentId);
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, references[0].Checksum.Algorithm);
        Assert.AreEqual("d6a770ba38583ed4bb4525bd96e50461655d2759", references[0].Checksum.Value);
        Assert.AreEqual("http://spdx.org/spdxdocs/spdx-tools-v1.2-3F2504E0-4F89-41D3-9A0C-0305E82C3301", references[0].Document);
        Assert.AreEqual("DocumentRef-OtherDoc", references[1].ExternalDocumentId);
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, references[1].Checksum.Algorithm);
        Assert.AreEqual("c2b4e1c67a2d28fced849ee1bb76e7391b93f125", references[1].Checksum.Value);
        Assert.AreEqual("http://demo.com/some-document", references[1].Document);
    }
}