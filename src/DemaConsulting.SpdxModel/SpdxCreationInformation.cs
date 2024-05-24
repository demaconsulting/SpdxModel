using System.Text.RegularExpressions;

namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX Creation Information
/// </summary>
/// <remarks>
/// One instance is required for each SPDX file produced. It provides the
/// necessary information for forward and backward compatibility for
/// processing tools.
/// </remarks>
public sealed class SpdxCreationInformation
{
    /// <summary>
    /// Creator Field
    /// </summary>
    /// <remarks>
    /// Identify who (or what, in the case of a tool) created the SPDX
    /// document. If the SPDX document was created by an individual, indicate
    /// the person's name. If the SPDX document was created on behalf of a
    /// company or organization, indicate the entity name. If the SPDX
    /// document was created using a software tool, indicate the name and
    /// version for that tool. If multiple participants or tools were
    /// involved, use multiple instances of this field. Person name or
    /// organization name may be designated as “anonymous” if appropriate.
    /// </remarks>
    public string[] Creators { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Created Field
    /// </summary>
    /// <remarks>
    /// Identify when the SPDX document was originally created. The date is to
    /// be specified according to combined date and time in UTC format as
    /// specified in ISO 8601 standard.
    /// </remarks>
    public string Created { get; set; } = string.Empty;

    /// <summary>
    /// Creator Comment Field (optional)
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// License List Version Field (optional)
    /// </summary>
    /// <remarks>
    /// An optional field for creators of the SPDX file to provide the
    /// version of the SPDX License List used when the SPDX file was created.
    /// </remarks>
    public string? LicenseListVersion { get; set; }

    /// <summary>
    /// Perform validation of information
    /// </summary>
    /// <param name="issues">List to populate with issues</param>
    public void Validate(List<string> issues)
    {
        // Validate Creator Field
        if (Creators.Length == 0)
            issues.Add("Document Invalid Creator Field");

        // Validate Created Field
        if (!Regex.IsMatch(Created, @"\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}Z"))
            issues.Add("Document Invalid Created Field");

        // Validate License List Version Field
        if (LicenseListVersion != null && !Regex.IsMatch(LicenseListVersion, @"\d+\.\d+"))
            issues.Add("Document Invalid License List Version Field");
    }
}