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

namespace DemaConsulting.SpdxModel;

/// <summary>
///     SPDX External Document Reference
/// </summary>
/// <remarks>
///     Information about an external SPDX document reference including the
///     checksum. This allows for verification of the external references.
/// </remarks>
public sealed class SpdxExternalDocumentReference
{
    /// <summary>
    ///     Equality comparer for the same external document reference
    /// </summary>
    /// <remarks>
    ///     This considers external document references as being the same if they have the same document.
    /// </remarks>
    public static readonly IEqualityComparer<SpdxExternalDocumentReference> Same =
        new SpdxExternalDocumentReferenceSame();

    /// <summary>
    ///     External Document ID Field
    /// </summary>
    /// <remarks>
    ///     A string containing letters, numbers, ., - and/or + which uniquely identifies an external document within this
    ///     document.
    /// </remarks>
    public string ExternalDocumentId { get; set; } = "";

    /// <summary>
    ///     External Document Checksum Field
    /// </summary>
    /// <remarks>
    ///     A cryptographic checksum of the referenced SPDX document used for integrity verification.
    ///     This allows consumers to confirm that the referenced document has not been modified since
    ///     the reference was created.
    /// </remarks>
    public SpdxChecksum Checksum { get; set; } = new();

    /// <summary>
    ///     SPDX Document URI Field
    /// </summary>
    /// <remarks>
    ///     The URI of the referenced external SPDX document. Must be a valid absolute URI that
    ///     uniquely identifies the external document's namespace. Used together with
    ///     <see cref="ExternalDocumentId"/> to qualify cross-document element references.
    /// </remarks>
    public string Document { get; set; } = "";

    /// <summary>
    ///     Make a deep-copy of this object
    /// </summary>
    /// <remarks>
    ///     Produces an independent copy with no shared mutable references. The nested
    ///     <see cref="Checksum"/> is deep-copied so that mutations to the returned instance
    ///     do not affect this instance, and vice versa.
    /// </remarks>
    /// <returns>Deep copy of this object</returns>
    public SpdxExternalDocumentReference DeepCopy()
    {
        return new SpdxExternalDocumentReference
        {
            ExternalDocumentId = ExternalDocumentId,
            Checksum = Checksum.DeepCopy(),
            Document = Document
        };
    }

    /// <summary>
    ///     Enhance missing fields in the external document reference
    /// </summary>
    /// <remarks>
    ///     Applies a fill-if-absent strategy: each field is updated only when its current
    ///     value is empty or default. The nested <see cref="Checksum"/> is enhanced in place
    ///     using the same additive semantics.
    /// </remarks>
    /// <param name="other">Other external document reference to enhance with</param>
    public void Enhance(SpdxExternalDocumentReference other)
    {
        // Populate the external document ID if missing
        ExternalDocumentId = SpdxHelpers.EnhanceString(ExternalDocumentId, other.ExternalDocumentId) ?? "";

        // Enhance the checksum
        Checksum.Enhance(other.Checksum);

        // Enhance the document URI if missing
        Document = SpdxHelpers.EnhanceString(Document, other.Document) ?? "";
    }

    /// <summary>
    ///     Enhance missing external document references in array
    /// </summary>
    /// <remarks>
    ///     Matching uses the <see cref="Same"/> comparer (Document URI equality). Entries in
    ///     <paramref name="others"/> that match an existing entry are merged in place via the
    ///     instance <see cref="Enhance(SpdxExternalDocumentReference)"/> method. Entries with
    ///     no match are deep-copied before being appended to preserve independence from the
    ///     source array.
    /// </remarks>
    /// <param name="array">Array to enhance</param>
    /// <param name="others">Other array to enhance with</param>
    /// <returns>Updated array</returns>
    public static SpdxExternalDocumentReference[] Enhance(SpdxExternalDocumentReference[] array,
        SpdxExternalDocumentReference[] others)
    {
        // Convert to list
        var list = array.ToList();

        // Iterate over other array
        foreach (var other in others)
        {
            // Check if other item is the same as one we have
            var existing = list.Find(a => Same.Equals(a, other));
            if (existing != null)
            {
                // Enhance our item with the other information
                existing.Enhance(other);
            }
            else
            {
                // Add the new item to our list
                list.Add(other.DeepCopy());
            }
        }

        // Return as array
        return [.. list];
    }

    /// <summary>
    ///     Perform validation of information
    /// </summary>
    /// <remarks>
    ///     Issues are appended to <paramref name="issues"/> rather than thrown as exceptions,
    ///     enabling a complete diagnostic pass across all references in a single call. The
    ///     nested <see cref="Checksum"/> is validated via <see cref="SpdxChecksum.Validate"/>.
    /// </remarks>
    /// <param name="issues">List to populate with issues</param>
    public void Validate(List<string> issues)
    {
        // Validate External Document ID Field
        if (ExternalDocumentId.Length == 0)
        {
            issues.Add("External Document Reference Invalid External Document ID Field - Empty");
        }

        // Validate External Document Checksum Field
        Checksum.Validate($"External Document Reference '{ExternalDocumentId}'", issues);

        // Validate SPDX Document URI Field
        if (Document.Length == 0)
        {
            issues.Add($"External Document Reference '{ExternalDocumentId}' Invalid SPDX Document URI Field - Empty");
        }
    }

    /// <summary>
    ///     Equality Comparer to test for the same external document reference
    /// </summary>
    /// <remarks>
    ///     Two external document references are considered the same when their
    ///     <see cref="SpdxExternalDocumentReference.Document"/> URI fields are equal.
    ///     The <see cref="SpdxExternalDocumentReference.ExternalDocumentId"/> field is
    ///     intentionally excluded from the comparison because the same external document
    ///     may be referenced under different local aliases in different documents.
    /// </remarks>
    private sealed class SpdxExternalDocumentReferenceSame : IEqualityComparer<SpdxExternalDocumentReference>
    {
        /// <inheritdoc />
        public bool Equals(SpdxExternalDocumentReference? r1, SpdxExternalDocumentReference? r2)
        {
            if (ReferenceEquals(r1, r2))
            {
                return true;
            }

            if (r1 == null || r2 == null)
            {
                return false;
            }

            return r1.Document == r2.Document;
        }

        /// <inheritdoc />
        public int GetHashCode(SpdxExternalDocumentReference obj)
        {
            return obj.Document.GetHashCode();
        }
    }
}
