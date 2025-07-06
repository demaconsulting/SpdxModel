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
public enum SpdxReferenceCategory
{
    /// <summary>
    ///     Missing reference category
    /// </summary>
    Missing = -1,

    /// <summary>
    ///     Reference for security-related information
    /// </summary>
    Security,

    /// <summary>
    ///     Reference for package management information
    /// </summary>
    PackageManager,

    /// <summary>
    ///     Reference for software heritage archive persistent identifier
    /// </summary>
    PersistentId,

    /// <summary>
    ///     Reference for other reasons
    /// </summary>
    Other
}

/// <summary>
///     SPDX Reference Category Extensions
/// </summary>
public static class SpdxReferenceCategoryExtensions
{
    /// <summary>
    ///     Convert text to SpdxReferenceCategory
    /// </summary>
    /// <param name="category">Reference Category text</param>
    /// <returns>SpdxReferenceCategory</returns>
    /// <exception cref="InvalidOperationException">on error</exception>
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
    /// <param name="category">SpdxReferenceCategory</param>
    /// <returns>Reference Category text</returns>
    /// <exception cref="InvalidOperationException">on error</exception>
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