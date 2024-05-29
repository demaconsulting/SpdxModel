namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX Element base class
/// </summary>
public abstract class SpdxElement
{
    /// <summary>
    /// Gets or sets the Element ID
    /// </summary>
    /// <remarks>
    /// Uniquely identify any element in an SPDX document which may be
    /// referenced by other elements.
    /// </remarks>
    public string Id { get; set; } = string.Empty;
}