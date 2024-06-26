﻿namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX External Document Reference
/// </summary>
/// <remarks>
/// Information about an external SPDX document reference including the
/// checksum. This allows for verification of the external references.
/// </remarks>
public sealed class SpdxExternalDocumentReference
{
    /// <summary>
    /// Equality comparer for the same external document reference
    /// </summary>
    /// <remarks>
    /// This considers packages as being the same if they have the same document.
    /// </remarks>
    public static readonly IEqualityComparer<SpdxExternalDocumentReference> Same = new SpdxExternalDocumentReferenceSame();

    /// <summary>
    /// External Document ID Field
    /// </summary>
    /// <remarks>
    /// A string containing letters, numbers, ., - and/or + which uniquely identifies an external document within this document.
    /// </remarks>
    public string ExternalDocumentId { get; set; } = string.Empty;

    /// <summary>
    /// External Document Checksum Field
    /// </summary>
    public SpdxChecksum Checksum { get; set; } = new();

    /// <summary>
    /// SPDX Document URI Field
    /// </summary>
    public string Document { get; set; } = string.Empty;

    /// <summary>
    /// Make a deep-copy of this object
    /// </summary>
    /// <returns>Deep copy of this object</returns>
    public SpdxExternalDocumentReference DeepCopy() =>
        new()
        {
            ExternalDocumentId = ExternalDocumentId,
            Checksum = Checksum.DeepCopy(),
            Document = Document
        };

    /// <summary>
    /// Enhance missing fields in the checksum
    /// </summary>
    /// <param name="other">Other checksum to enhance with</param>
    public void Enhance(SpdxExternalDocumentReference other)
    {
        // Populate the external document ID if missing
        if (string.IsNullOrWhiteSpace(ExternalDocumentId))
            ExternalDocumentId = other.ExternalDocumentId;

        // Enhance the checksum
        Checksum.Enhance(other.Checksum);

        if (string.IsNullOrWhiteSpace(Document))
            Document = other.Document;
    }

    /// <summary>
    /// Enhance missing external document references in array
    /// </summary>
    /// <param name="array">Array to enhance</param>
    /// <param name="others">Other array to enhance with</param>
    /// <returns>Updated array</returns>
    public static SpdxExternalDocumentReference[] Enhance(SpdxExternalDocumentReference[] array, SpdxExternalDocumentReference[] others)
    {
        // Convert to list
        var list = array.ToList();

        // Iterate over other array
        foreach (var other in others)
        {
            // Check if other item is the same as one we have
            var annotation = list.FirstOrDefault(a => Same.Equals(a, other));
            if (annotation != null)
            {
                // Enhance our item with the other information
                annotation.Enhance(other);
            }
            else
            {
                // Add the new item to our list
                list.Add(other.DeepCopy());
            }
        }

        // Return as array
        return list.ToArray();
    }

    /// <summary>
    /// Perform validation of information
    /// </summary>
    /// <param name="issues">List to populate with issues</param>
    public void Validate(List<string> issues)
    {
        // Validate External Document ID Field
        if (ExternalDocumentId.Length == 0)
            issues.Add("External Document Reference Invalid External Document ID Field");

        // Validate External Document Checksum Field
        Checksum.Validate($"External Document Reference {ExternalDocumentId}", issues);

        // Validate SPDX Document URI Field
        if (Document.Length == 0)
            issues.Add($"External Document Reference {ExternalDocumentId} Invalid  SPDX Document URI Field");
    }

    /// <summary>
    /// Equality Comparer to test for the same external document reference
    /// </summary>
    private class SpdxExternalDocumentReferenceSame : IEqualityComparer<SpdxExternalDocumentReference>
    {
        /// <inheritdoc />
        public bool Equals(SpdxExternalDocumentReference? r1, SpdxExternalDocumentReference? r2)
        {
            if (ReferenceEquals(r1, r2)) return true;
            if (r1 == null || r2 == null) return false;

            return r1.Document == r2.Document;
        }

        /// <inheritdoc />
        public int GetHashCode(SpdxExternalDocumentReference obj)
        {
            return obj.Document.GetHashCode();
        }
    }
}