namespace DemaConsulting.SpdxModel.Tests;

/// <summary>
/// Test helpers class
/// </summary>
internal static class TestHelpers
{
    /// <summary>
    /// Get an embedded resource as a string
    /// </summary>
    /// <param name="resourceName">Resource name</param>
    /// <returns>Resource string</returns>
    public static string GetEmbeddedResource(string resourceName)
    {
        // Open the resource
        using var stream = typeof(TestHelpers).Assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
            return string.Empty;

        // Read the resource
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd().ReplaceLineEndings();
    }
}