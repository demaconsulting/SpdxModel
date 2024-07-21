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
/// SPDX Annotation Type enumeration
/// </summary>
public enum SpdxAnnotationType
{
    /// <summary>
    /// Missing annotation type
    /// </summary>
    Missing = -1,

    /// <summary>
    /// Annotation created during review
    /// </summary>
    Review,

    /// <summary>
    /// Annotation created for other reasons
    /// </summary>
    Other
}

/// <summary>
/// SPDX Annotation Type Extensions
/// </summary>
public static class SpdxAnnotationTypeExtensions
{
    /// <summary>
    /// Convert text to SpdxAnnotationType
    /// </summary>
    /// <param name="annotationType">Annotation Type text</param>
    /// <returns>SpdxAnnotationType</returns>
    /// <exception cref="InvalidOperationException">on error</exception>
    public static SpdxAnnotationType FromText(string annotationType)
    {
        return annotationType switch
        {
            "" => SpdxAnnotationType.Missing,
            "REVIEW" => SpdxAnnotationType.Review,
            "OTHER" => SpdxAnnotationType.Other,
            _ => throw new InvalidOperationException($"Unsupported SPDX Annotation Type {annotationType}")
        };
    }

    /// <summary>
    /// Convert SpdxAnnotationType to text
    /// </summary>
    /// <param name="annotationType">SpdxAnnotationType</param>
    /// <returns>Annotation Type text</returns>
    /// <exception cref="InvalidOperationException">on error</exception>
    public static string ToText(this SpdxAnnotationType annotationType)
    {
        return annotationType switch
        {
            SpdxAnnotationType.Missing => throw new InvalidOperationException(
                "Attempt to serialize missing SPDX Annotation Type"),
            SpdxAnnotationType.Review => "REVIEW",
            SpdxAnnotationType.Other => "OTHER",
            _ => throw new InvalidOperationException($"Unsupported SPDX Annotation Type {annotationType}")
        };
    }
}