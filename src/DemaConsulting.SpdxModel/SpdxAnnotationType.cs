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