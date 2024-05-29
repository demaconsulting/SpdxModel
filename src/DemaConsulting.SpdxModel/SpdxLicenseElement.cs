namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX Element with License
/// </summary>
public abstract class SpdxLicenseElement : SpdxElement
{
    /// <summary>
    /// Concluded License Field (optional)
    /// </summary>
    /// <remarks>
    /// License expression. See SPDX Annex D for the license expression syntax.
    /// 
    /// The licensing that the preparer of this SPDX document has concluded,
    /// based on the evidence, actually applies to the SPDX Item.
    ///
    /// If not present, it implies an equivalent meaning to NOASSERTION.
    /// </remarks>
    public string ConcludedLicense { get; set; } = string.Empty;

    /// <summary>
    /// Comments On License Field (optional)
    /// </summary>
    /// <remarks>
    /// This property allows the preparer of the SPDX document to describe why
    /// the ConcludedLicense was chosen.
    /// </remarks>
    public string? LicenseComments { get; set; }

    /// <summary>
    /// Copyright Text Field (optional)
    /// </summary>
    /// <remarks>
    /// The text of copyright declarations.
    ///
    /// If not present, it implies an equivalent meaning to NOASSERTION.
    /// </remarks>
    public string CopyrightText { get; set; } = string.Empty;

    /// <summary>
    /// Attribution Text Field
    /// </summary>
    /// <remarks>
    /// This field provides a place for the SPDX data creator to record
    /// acknowledgements that may be required to be communicated in some
    /// contexts.
    /// </remarks>
    public string[] AttributionText { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Annotations
    /// </summary>
    /// <remarks>
    /// Provide additional information about this element.
    /// </remarks>
    public SpdxAnnotation[] Annotations { get; set; } = Array.Empty<SpdxAnnotation>();
}