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

    /// <summary>
    /// Enhance missing fields in the element
    /// </summary>
    /// <param name="other">Other element to enhance with</param>
    protected void EnhanceElement(SpdxElement other)
    {
        // Populate the ID if missing
        if (string.IsNullOrWhiteSpace(Id))
            Id = other.Id;
    }
}