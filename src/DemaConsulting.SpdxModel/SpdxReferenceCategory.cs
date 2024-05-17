namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX Reference Category enumeration
/// </summary>
public enum SpdxReferenceCategory
{
    Missing = -1,
    Security,
    PackageManager,
    PersistentId,
    Other
}

/// <summary>
/// SPDX Reference Category Extensions
/// </summary>
public static class SpdxReferenceCategoryExtensions
{
    /// <summary>
    /// Convert text to SpdxReferenceCategory
    /// </summary>
    /// <param name="category">Reference Category text</param>
    /// <returns>SpdxReferenceCategory</returns>
    /// <exception cref="InvalidOperationException">on error</exception>
    public static SpdxReferenceCategory FromText(string category)
    {
        return category switch
        {
            "" => SpdxReferenceCategory.Missing,
            "SECURITY" => SpdxReferenceCategory.Security,
            "PACKAGE-MANAGER" => SpdxReferenceCategory.PackageManager,
            "PACKAGE_MANAGER" => SpdxReferenceCategory.PackageManager,
            "PERSISTENT-ID" => SpdxReferenceCategory.PersistentId,
            "OTHER" => SpdxReferenceCategory.Other,
            _ => throw new InvalidOperationException($"Unsupported SPDX Reference Category {category}")
        };
    }

    /// <summary>
    /// Convert SpdxReferenceCategory to text
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
            _ => throw new InvalidOperationException($"Unsupported SPDX Reference Category {category}")
        };
    }
}