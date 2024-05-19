namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX External Document Reference
/// </summary>
public class SpdxExternalDocumentReference
{
    /// <summary>
    /// External Document ID Field
    /// </summary>
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
            issues.Add("$\"External Document Reference {ExternalDocumentId} Invalid  SPDX Document URI Field");
    }
}