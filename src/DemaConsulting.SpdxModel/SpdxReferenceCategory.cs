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
///     SPDX Reference Category enumeration
/// </summary>
/// <remarks>
///     Enumerates the broad reference categories defined by the SPDX specification.
///     The <see cref="Missing"/> sentinel (value -1) is used internally to represent an
///     unset category and must never be serialized to an SPDX document.
/// </remarks>
public enum SpdxReferenceCategory
{
    /// <summary>
    ///     Missing reference category
    /// </summary>
    /// <remarks>
    ///     Sentinel value indicating that no category has been assigned. This value must never
    ///     be serialized to an SPDX document; see
    ///     <see cref="SpdxReferenceCategoryExtensions.ToText"/> which throws when called with
    ///     this value.
    /// </remarks>
    Missing = -1,

    /// <summary>
    ///     Reference for security-related information
    /// </summary>
    /// <remarks>
    ///     Corresponds to the SPDX specification text constant <c>SECURITY</c>.
    /// </remarks>
    Security,

    /// <summary>
    ///     Reference for package management information
    /// </summary>
    /// <remarks>
    ///     Corresponds to the SPDX specification text constant <c>PACKAGE-MANAGER</c>.
    /// </remarks>
    PackageManager,

    /// <summary>
    ///     Reference for software heritage archive persistent identifier
    /// </summary>
    /// <remarks>
    ///     Corresponds to the SPDX specification text constant <c>PERSISTENT-ID</c>.
    /// </remarks>
    PersistentId,

    /// <summary>
    ///     Reference for other reasons
    /// </summary>
    /// <remarks>
    ///     Corresponds to the SPDX specification text constant <c>OTHER</c>.
    /// </remarks>
    Other
}

/// <summary>
///     SPDX Reference Category Extensions
/// </summary>
/// <remarks>
///     Static extension companion to <see cref="SpdxReferenceCategory"/> that provides
///     serialization helpers for mapping between the enum and the SPDX document text values
///     defined in the SPDX specification.
/// </remarks>
public static class SpdxReferenceCategoryExtensions
{
    /// <summary>
    ///     Convert text to SpdxReferenceCategory
    /// </summary>
    /// <remarks>
    ///     Matching is case-insensitive (implemented via <c>ToUpperInvariant</c>). Both
    ///     <c>PACKAGE-MANAGER</c> and <c>PACKAGE_MANAGER</c> are accepted for backward
    ///     compatibility with documents that use the underscore variant.
    /// </remarks>
    /// <param name="category">Reference Category text</param>
    /// <returns>
    ///     Returns SpdxReferenceCategory.Missing when category is an empty string; otherwise returns the matching enum
    ///     value.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when <paramref name="category"/> is not a recognized SPDX reference category string.
    /// </exception>
    public static SpdxReferenceCategory FromText(string category)
    {
        return category.ToUpperInvariant() switch
        {
            "" => SpdxReferenceCategory.Missing,
            "SECURITY" => SpdxReferenceCategory.Security,
            "PACKAGE-MANAGER" => SpdxReferenceCategory.PackageManager,
            "PACKAGE_MANAGER" => SpdxReferenceCategory.PackageManager,
            "PERSISTENT-ID" => SpdxReferenceCategory.PersistentId,
            "OTHER" => SpdxReferenceCategory.Other,
            _ => throw new InvalidOperationException($"Unsupported SPDX Reference Category '{category}'")
        };
    }

    /// <summary>
    ///     Convert SpdxReferenceCategory to text
    /// </summary>
    /// <remarks>
    ///     The output is always the canonical SPDX specification string (e.g., <c>PACKAGE-MANAGER</c>
    ///     not <c>PACKAGE_MANAGER</c>). Calling this method with
    ///     <see cref="SpdxReferenceCategory.Missing"/> always throws; callers must check for the
    ///     sentinel before serializing.
    /// </remarks>
    /// <param name="category">SpdxReferenceCategory</param>
    /// <returns>Reference Category text</returns>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when <paramref name="category"/> is SpdxReferenceCategory.Missing or an unsupported enum value.
    /// </exception>
    public static string ToText(this SpdxReferenceCategory category)
    {
        return category switch
        {
            SpdxReferenceCategory.Missing => throw new InvalidOperationException(
                "Attempt to serialize missing SPDX Reference Category"),
            SpdxReferenceCategory.Security => "SECURITY",
            SpdxReferenceCategory.PackageManager => "PACKAGE-MANAGER",
            SpdxReferenceCategory.PersistentId => "PERSISTENT-ID",
            SpdxReferenceCategory.Other => "OTHER",
            _ => throw new InvalidOperationException($"Unsupported SPDX Reference Category '{category}'")
        };
    }
}
