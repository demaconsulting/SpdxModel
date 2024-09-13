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
    public string ConcludedLicense { get; set; } = "";

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
    public string CopyrightText { get; set; } = "";

    /// <summary>
    /// Attribution Text Field
    /// </summary>
    /// <remarks>
    /// This field provides a place for the SPDX data creator to record
    /// acknowledgements that may be required to be communicated in some
    /// contexts.
    /// </remarks>
    public string[] AttributionText { get; set; } = [];

    /// <summary>
    /// Annotations
    /// </summary>
    /// <remarks>
    /// Provide additional information about this element.
    /// </remarks>
    public SpdxAnnotation[] Annotations { get; set; } = [];

    /// <summary>
    /// Enhance missing fields in the license element
    /// </summary>
    /// <param name="other">Other license element to enhance with</param>
    protected void EnhanceLicenseElement(SpdxLicenseElement other)
    {
        // Enhance the base element
        EnhanceElement(other);

        // Populate the concluded license if missing
        ConcludedLicense = SpdxHelpers.EnhanceString(ConcludedLicense, other.ConcludedLicense) ?? "";

        // Populate the license comments if missing
        LicenseComments = SpdxHelpers.EnhanceString(LicenseComments, other.LicenseComments);

        // Populate the copyright text if missing
        CopyrightText = SpdxHelpers.EnhanceString(CopyrightText, other.CopyrightText) ?? "";

        // Merge the attribution texts
        AttributionText = AttributionText.Concat(other.AttributionText).Distinct().ToArray();

        // Enhance the annotations
        Annotations = SpdxAnnotation.Enhance(Annotations, other.Annotations);
    }
}