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
/// SPDX Element base class
/// </summary>
public abstract class SpdxElement
{
    /// <summary>
    /// No Assertion value
    /// </summary>
    public const string NoAssertion = "NOASSERTION";

    /// <summary>
    /// Gets or sets the Element ID
    /// </summary>
    /// <remarks>
    /// Uniquely identify any element in an SPDX document which may be
    /// referenced by other elements.
    /// </remarks>
    public string Id { get; set; } = "";

    /// <summary>
    /// Enhance missing fields in the element
    /// </summary>
    /// <param name="other">Other element to enhance with</param>
    protected void EnhanceElement(SpdxElement other)
    {
        // Populate the ID if missing
        Id = SpdxHelpers.EnhanceString(Id, other.Id) ?? "";
    }
}