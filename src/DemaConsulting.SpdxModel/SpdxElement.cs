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

using System.Text.RegularExpressions;

namespace DemaConsulting.SpdxModel;

/// <summary>
///     SPDX Element base class
/// </summary>
/// <remarks>
///     Acts as the abstract base for all identifiable SPDX model objects (documents, packages,
///     files, snippets, relationships, and annotations). Centralizing the identity property here
///     ensures that element lookup, traversal, and duplicate-detection logic works uniformly
///     across the entire object model.
/// </remarks>
public abstract class SpdxElement
{
    /// <summary>
    ///     Sentinel value indicating that a field was intentionally omitted or its value is not known.
    /// </summary>
    /// <remarks>
    ///     Used by optional fields throughout the SPDX model (e.g., package supplier, originator, and
    ///     download location) to distinguish an explicit "no assertion" from an absent value.
    /// </remarks>
    public const string NoAssertion = "NOASSERTION";

    /// <summary>
    ///     Regular expression for checking element IDs of the form "SPDXRef-name"
    /// </summary>
    /// <remarks>
    ///     Matches the full pattern <c>^SPDXRef-[a-zA-Z0-9.-]+$</c>, which allows letters,
    ///     digits, hyphens, and dots after the mandatory <c>SPDXRef-</c> prefix.
    ///     Declared as <c>static readonly</c> so a single compiled instance is shared safely
    ///     across all concurrent callers. The 100 ms timeout is a ReDoS protection measure
    ///     against pathological input strings from untrusted SPDX sources.
    /// </remarks>
    protected static readonly Regex SpdxRefRegex = new(
        "^SPDXRef-[a-zA-Z0-9.-]+$",
        RegexOptions.None,
        TimeSpan.FromMilliseconds(100)); // 100 ms timeout guards against ReDoS on untrusted SPDX identifier strings

    /// <summary>
    ///     Gets or sets the Element ID
    /// </summary>
    /// <remarks>
    ///     Uniquely identify any element in an SPDX document which may be
    ///     referenced by other elements.
    /// </remarks>
    public string Id { get; set; } = "";

    /// <summary>
    ///     Enhance missing fields in the element
    /// </summary>
    /// <remarks>
    ///     Called by subclass <c>Enhance</c> methods to propagate the element identity from
    ///     <paramref name="other"/> when the current <see cref="Id"/> is empty. This is a no-op if
    ///     <paramref name="other"/>'s <see cref="Id"/> is also empty.
    /// </remarks>
    /// <param name="other">Other element to enhance with</param>
    protected void EnhanceElement(SpdxElement other)
    {
        // Populate the ID if missing
        Id = SpdxHelpers.EnhanceString(Id, other.Id) ?? "";
    }
}
