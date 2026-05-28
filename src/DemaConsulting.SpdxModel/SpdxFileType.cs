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
///     SPDX File Type enumeration
/// </summary>
/// <remarks>
///     Enumerates the file types defined by the SPDX specification. Each value corresponds to
///     a canonical uppercase SPDX text constant used during serialization and deserialization.
/// </remarks>
public enum SpdxFileType
{
    /// <summary>
    ///     Human-readable source code
    /// </summary>
    /// <remarks>
    ///     Corresponds to the SPDX specification text constant <c>SOURCE</c>.
    /// </remarks>
    Source,

    /// <summary>
    ///     Compiled object, target image or binary executable
    /// </summary>
    /// <remarks>
    ///     Corresponds to the SPDX specification text constant <c>BINARY</c>.
    /// </remarks>
    Binary,

    /// <summary>
    ///     File represents an archive
    /// </summary>
    /// <remarks>
    ///     Corresponds to the SPDX specification text constant <c>ARCHIVE</c>.
    /// </remarks>
    Archive,

    /// <summary>
    ///     Application file
    /// </summary>
    /// <remarks>
    ///     Corresponds to the SPDX specification text constant <c>APPLICATION</c>.
    /// </remarks>
    Application,

    /// <summary>
    ///     Audio file
    /// </summary>
    /// <remarks>
    ///     Corresponds to the SPDX specification text constant <c>AUDIO</c>.
    /// </remarks>
    Audio,

    /// <summary>
    ///     Image file
    /// </summary>
    /// <remarks>
    ///     Corresponds to the SPDX specification text constant <c>IMAGE</c>.
    /// </remarks>
    Image,

    /// <summary>
    ///     Human-readable text file
    /// </summary>
    /// <remarks>
    ///     Corresponds to the SPDX specification text constant <c>TEXT</c>.
    /// </remarks>
    Text,

    /// <summary>
    ///     Video file
    /// </summary>
    /// <remarks>
    ///     Corresponds to the SPDX specification text constant <c>VIDEO</c>.
    /// </remarks>
    Video,

    /// <summary>
    ///     Documentation file
    /// </summary>
    /// <remarks>
    ///     Corresponds to the SPDX specification text constant <c>DOCUMENTATION</c>.
    /// </remarks>
    Documentation,

    /// <summary>
    ///     SPDX document
    /// </summary>
    /// <remarks>
    ///     Corresponds to the SPDX specification text constant <c>SPDX</c>.
    /// </remarks>
    Spdx,

    /// <summary>
    ///     Other type of document not matching standard categories
    /// </summary>
    /// <remarks>
    ///     Corresponds to the SPDX specification text constant <c>OTHER</c>.
    /// </remarks>
    Other
}

/// <summary>
///     SPDX File Type Extensions
/// </summary>
/// <remarks>
///     Static extension companion to <see cref="SpdxFileType"/> that provides serialization
///     helpers for mapping between the enum and the canonical uppercase SPDX document text
///     values.
/// </remarks>
public static class SpdxFileTypeExtensions
{
    /// <summary>
    ///     Convert text to SpdxFileType
    /// </summary>
    /// <remarks>
    ///     Matching is case-insensitive, implemented via <c>ToUpperInvariant</c> before the
    ///     switch expression.
    /// </remarks>
    /// <param name="fileType">File Type text</param>
    /// <returns>SpdxFileType</returns>
    /// <exception cref="InvalidOperationException">Thrown when <paramref name="fileType"/> does not match any known SPDX file type string.</exception>
    public static SpdxFileType FromText(string fileType)
    {
        return fileType.ToUpperInvariant() switch
        {
            "SOURCE" => SpdxFileType.Source,
            "BINARY" => SpdxFileType.Binary,
            "ARCHIVE" => SpdxFileType.Archive,
            "APPLICATION" => SpdxFileType.Application,
            "AUDIO" => SpdxFileType.Audio,
            "IMAGE" => SpdxFileType.Image,
            "TEXT" => SpdxFileType.Text,
            "VIDEO" => SpdxFileType.Video,
            "DOCUMENTATION" => SpdxFileType.Documentation,
            "SPDX" => SpdxFileType.Spdx,
            "OTHER" => SpdxFileType.Other,
            _ => throw new InvalidOperationException($"Unsupported SPDX File Type '{fileType}'")
        };
    }

    /// <summary>
    ///     Convert SpdxFileType to text
    /// </summary>
    /// <remarks>
    ///     Returned strings are canonical uppercase SPDX representations per the SPDX
    ///     specification (e.g., <c>SOURCE</c>, <c>BINARY</c>).
    /// </remarks>
    /// <param name="fileType">SpdxFileType</param>
    /// <returns>File Type text</returns>
    /// <exception cref="InvalidOperationException">Thrown when <paramref name="fileType"/> is not a supported enum value.</exception>
    public static string ToText(this SpdxFileType fileType)
    {
        return fileType switch
        {
            SpdxFileType.Source => "SOURCE",
            SpdxFileType.Binary => "BINARY",
            SpdxFileType.Archive => "ARCHIVE",
            SpdxFileType.Application => "APPLICATION",
            SpdxFileType.Audio => "AUDIO",
            SpdxFileType.Image => "IMAGE",
            SpdxFileType.Text => "TEXT",
            SpdxFileType.Video => "VIDEO",
            SpdxFileType.Documentation => "DOCUMENTATION",
            SpdxFileType.Spdx => "SPDX",
            SpdxFileType.Other => "OTHER",
            _ => throw new InvalidOperationException($"Unsupported SPDX File Type '{fileType}'")
        };
    }
}
