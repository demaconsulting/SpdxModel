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
    /// Regular expression for checking license list versions
    /// </summary>
    private static readonly Regex LicenseListVersionRegex = new(
        @"\d+\.\d+",
        RegexOptions.None,
        TimeSpan.FromMilliseconds(100));

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
    public string[] Creators { get; set; } = [];

    /// <summary>
    /// Created Field
    /// </summary>
    /// <remarks>
    /// Identify when the SPDX document was originally created. The date is to
    /// be specified according to combined date and time in UTC format as
    /// specified in ISO 8601 standard.
    /// </remarks>
    public string Created { get; set; } = "";

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
    /// Make a deep-copy of this object
    /// </summary>
    /// <returns>Deep copy of this object</returns>
    public SpdxCreationInformation DeepCopy() =>
        new()
        {
            Creators = Creators.ToArray(),
            Created = Created,
            Comment = Comment,
            LicenseListVersion = LicenseListVersion
        };

    /// <summary>
    /// Enhance missing fields in the creation information
    /// </summary>
    /// <param name="other">Other creation information to enhance with</param>
    public void Enhance(SpdxCreationInformation other)
    {
        // Merge the creators
        Creators = Creators.Concat(other.Creators).Distinct().ToArray();

        // Populate the created field if missing
        Created = SpdxHelpers.EnhanceString(Created, other.Created) ?? "";

        // Populate the comment field if missing
        Comment = SpdxHelpers.EnhanceString(Comment, other.Comment);

        // Populate the license-list-version field if missing
        LicenseListVersion = SpdxHelpers.EnhanceString(LicenseListVersion, other.LicenseListVersion);
    }

    /// <summary>
    /// Perform validation of information
    /// </summary>
    /// <param name="issues">List to populate with issues</param>
    public void Validate(List<string> issues)
    {
        // Validate Creator Field
        if (Creators.Length == 0)
            issues.Add("Document Invalid Creator Field");

        // Validate Creators Field Entries
        foreach (var creator in Creators)
            if (!creator.StartsWith("Person:") &&
                !creator.StartsWith("Organization:") &&
                !creator.StartsWith("Tool:"))
                issues.Add($"Document Invalid Creator Entry: {creator}");

        // Validate Created Field
        if (!SpdxHelpers.IsValidSpdxDateTime(Created))
            issues.Add("Document Invalid Created Field");

        // Validate License List Version Field
        if (!string.IsNullOrEmpty(LicenseListVersion) && !LicenseListVersionRegex.IsMatch(LicenseListVersion))
            issues.Add("Document Invalid License List Version Field");
    }
}