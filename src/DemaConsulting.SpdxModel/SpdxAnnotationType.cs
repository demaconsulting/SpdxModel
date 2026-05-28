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
///     SPDX Annotation Type enumeration
/// </summary>
/// <remarks>
///     The <see cref="Missing"/> sentinel value (<c>-1</c>) is used internally to represent
///     an absent or uninitialized annotation type and must not be serialized to JSON.
///     <see cref="SpdxAnnotationTypeExtensions.ToText"/> will throw if called with
///     <see cref="Missing"/>.
/// </remarks>
public enum SpdxAnnotationType
{
    /// <summary>
    ///     Missing annotation type
    /// </summary>
    /// <remarks>
    ///     Sentinel value (<c>-1</c>) representing an absent or uninitialized annotation type.
    ///     <see cref="SpdxAnnotationTypeExtensions.ToText"/> will throw
    ///     <see cref="InvalidOperationException"/> if called with this value, and it must never
    ///     be written to a serialized SPDX document.
    /// </remarks>
    Missing = -1,

    /// <summary>
    ///     Annotation created during review
    /// </summary>
    /// <remarks>
    ///     Canonical SPDX 2.x text form: <c>"REVIEW"</c>.
    /// </remarks>
    Review,

    /// <summary>
    ///     Annotation created for other reasons
    /// </summary>
    /// <remarks>
    ///     Canonical SPDX 2.x text form: <c>"OTHER"</c>.
    /// </remarks>
    Other
}

/// <summary>
///     SPDX Annotation Type Extensions
/// </summary>
/// <remarks>
///     Provides string ↔ enum conversion for SPDX 2.x JSON serialization. Both directions
///     are consumed by <see cref="IO.Spdx2JsonDeserializer"/> and
///     <see cref="IO.Spdx2JsonSerializer"/>.
/// </remarks>
public static class SpdxAnnotationTypeExtensions
{
    /// <summary>
    ///     Convert text to SpdxAnnotationType
    /// </summary>
    /// <remarks>
    ///     Matching is case-insensitive: <c>"review"</c>, <c>"REVIEW"</c>, and <c>"Review"</c>
    ///     all map to <see cref="SpdxAnnotationType.Review"/>. An empty string maps to
    ///     <see cref="SpdxAnnotationType.Missing"/>. Any other value throws
    ///     <see cref="InvalidOperationException"/>.
    /// </remarks>
    /// <param name="annotationType">Annotation Type text</param>
    /// <returns>SpdxAnnotationType</returns>
    /// <exception cref="InvalidOperationException">Thrown when <paramref name="annotationType"/> is not a recognized SPDX annotation type string.</exception>
    public static SpdxAnnotationType FromText(string annotationType)
    {
        return annotationType.ToUpperInvariant() switch
        {
            "" => SpdxAnnotationType.Missing,
            "REVIEW" => SpdxAnnotationType.Review,
            "OTHER" => SpdxAnnotationType.Other,
            _ => throw new InvalidOperationException($"Unsupported SPDX Annotation Type '{annotationType}'")
        };
    }

    /// <summary>
    ///     Convert SpdxAnnotationType to text
    /// </summary>
    /// <remarks>
    ///     Returns the uppercase SPDX 2.x text representation of the enum value
    ///     (e.g., <see cref="SpdxAnnotationType.Review"/> → <c>"REVIEW"</c>).
    ///     Throws <see cref="InvalidOperationException"/> for <see cref="SpdxAnnotationType.Missing"/>
    ///     and for any unrecognized numeric value to prevent silent serialization of invalid data.
    /// </remarks>
    /// <param name="annotationType">SpdxAnnotationType</param>
    /// <returns>Annotation Type text</returns>
    /// <exception cref="InvalidOperationException">Thrown when the enum value is <see cref="SpdxAnnotationType.Missing"/> or is not a recognized <see cref="SpdxAnnotationType"/> value.</exception>
    public static string ToText(this SpdxAnnotationType annotationType)
    {
        return annotationType switch
        {
            SpdxAnnotationType.Missing => throw new InvalidOperationException(
                "Attempt to serialize missing SPDX Annotation Type"),
            SpdxAnnotationType.Review => "REVIEW",
            SpdxAnnotationType.Other => "OTHER",
            _ => throw new InvalidOperationException($"Unsupported SPDX Annotation Type '{annotationType}'")
        };
    }
}
