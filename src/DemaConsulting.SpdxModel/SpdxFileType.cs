﻿namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX File Type enumeration
/// </summary>
public enum SpdxFileType
{
    /// <summary>
    /// Human-readable source code
    /// </summary>
    Source,

    /// <summary>
    /// Compiled object, target image or binary executable
    /// </summary>
    Binary,

    /// <summary>
    /// File represents an archive
    /// </summary>
    Archive,

    /// <summary>
    /// Application file
    /// </summary>
    Application,

    /// <summary>
    /// Audio file
    /// </summary>
    Audio,

    /// <summary>
    /// Image file
    /// </summary>
    Image,

    /// <summary>
    /// Human-readable text file
    /// </summary>
    Text,

    /// <summary>
    /// Video file
    /// </summary>
    Video,

    /// <summary>
    /// Documentation file
    /// </summary>
    Documentation,

    /// <summary>
    /// SPDX document
    /// </summary>
    Spdx,

    /// <summary>
    /// Other type of document not matching standard categories
    /// </summary>
    Other
}

/// <summary>
/// SPDX File Type Extensions
/// </summary>
public static class SpdxFileTypeExtensions
{
    /// <summary>
    /// Convert text to SpdxFileType
    /// </summary>
    /// <param name="fileType">File Type text</param>
    /// <returns>SpdxFileType</returns>
    /// <exception cref="InvalidOperationException">on error</exception>
    public static SpdxFileType FromText(string fileType)
    {
        return fileType switch
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
            _ => throw new InvalidOperationException($"Unsupported SPDX File Type {fileType}")
        };
    }

    /// <summary>
    /// Convert SpdxFileType to text
    /// </summary>
    /// <param name="fileType">SpdxFileType</param>
    /// <returns>Annotation Type text</returns>
    /// <exception cref="InvalidOperationException">on error</exception>
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
            _ => throw new InvalidOperationException($"Unsupported SPDX File Type {fileType}")
        };
    }
}