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

namespace DemaConsulting.SpdxModel.Tests;

/// <summary>
///     Tests for the <see cref="SpdxElement" /> base class identity behavior.
/// </summary>
/// <remarks>
///     <see cref="SpdxElement" /> is abstract; <see cref="SpdxPackage" /> is used as the
///     concrete subclass to exercise base-class identity behavior, as its Validate method
///     uses the standard <c>SpdxRefRegex</c> check.
/// </remarks>
[TestClass]
public class SpdxElementTests
{
    /// <summary>
    ///     Tests that an element with a valid SPDXRef-&lt;name&gt; identifier passes identity validation.
    /// </summary>
    /// <remarks>
    ///     Uses <c>SPDXRef-valid</c> as the identifier because it is a minimal, well-formed
    ///     value that satisfies the SPDXRef- prefix pattern and contains only allowed characters,
    ///     making it the simplest positive example to confirm the happy-path acceptance.
    /// </remarks>
    [TestMethod]
    public void SpdxElement_Id_ValidFormat_PassesValidation()
    {
        // Arrange: Create a minimal package element with a valid SPDXRef-<name> ID
        var element = new SpdxPackage { Id = "SPDXRef-valid", Name = "test-package", Version = "1.0" };

        // Act: Validate the element
        var issues = new List<string>();
        element.Validate(issues, null);

        // Assert: No issue about the SPDX Identifier field
        Assert.IsFalse(issues.Any(i => i.Contains("Invalid SPDX Identifier")));
    }

    /// <summary>
    ///     Tests that an element with an ID that does not follow the SPDXRef-&lt;name&gt; format
    ///     reports an identity validation issue.
    /// </summary>
    /// <remarks>
    ///     Uses <c>BadId</c> as the identifier because it is a concise, obviously invalid value
    ///     that omits the required SPDXRef- prefix entirely, making the expected failure
    ///     unambiguous and the diagnostic message easy to verify.
    /// </remarks>
    [TestMethod]
    public void SpdxElement_Id_InvalidFormat_ReportsValidationIssue()
    {
        // Arrange: Create a minimal package element with a bad ID
        var element = new SpdxPackage { Id = "BadId", Name = "test-package", Version = "1.0" };

        // Act: Validate the element
        var issues = new List<string>();
        element.Validate(issues, null);

        // Assert: The invalid identifier is reported
        Assert.IsTrue(issues.Any(i => i.Contains("Invalid SPDX Identifier Field 'BadId'")));
    }
}
