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
///     Tests for the <see cref="SpdxHelpers" /> class.
/// </summary>
[TestClass]
public class SpdxHelpersTests
{
    /// <summary>
    ///     Tests that <see cref="SpdxHelpers.IsValidSpdxDateTime" /> returns true for null input.
    /// </summary>
    [TestMethod]
    public void SpdxHelpers_IsValidSpdxDateTime_NullInput_ReturnsTrue()
    {
        // Arrange: null represents a not-set date-time field

        // Act
        var result = SpdxHelpers.IsValidSpdxDateTime(null);

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    ///     Tests that <see cref="SpdxHelpers.IsValidSpdxDateTime" /> returns true for an empty string.
    /// </summary>
    [TestMethod]
    public void SpdxHelpers_IsValidSpdxDateTime_EmptyInput_ReturnsTrue()
    {
        // Arrange: empty string represents a not-set date-time field

        // Act
        var result = SpdxHelpers.IsValidSpdxDateTime("");

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    ///     Tests that <see cref="SpdxHelpers.IsValidSpdxDateTime" /> returns true for a valid ISO 8601 UTC timestamp.
    /// </summary>
    [TestMethod]
    public void SpdxHelpers_IsValidSpdxDateTime_ValidFormat_ReturnsTrue()
    {
        // Arrange
        const string validDateTime = "2024-01-01T00:00:00Z";

        // Act
        var result = SpdxHelpers.IsValidSpdxDateTime(validDateTime);

        // Assert
        Assert.IsTrue(result);
    }

    /// <summary>
    ///     Tests that <see cref="SpdxHelpers.IsValidSpdxDateTime" /> returns false for an invalid format.
    /// </summary>
    [TestMethod]
    public void SpdxHelpers_IsValidSpdxDateTime_InvalidFormat_ReturnsFalse()
    {
        // Arrange
        const string invalidDateTime = "not-a-date";

        // Act
        var result = SpdxHelpers.IsValidSpdxDateTime(invalidDateTime);

        // Assert
        Assert.IsFalse(result);
    }

    /// <summary>
    ///     Tests that <see cref="SpdxHelpers.EnhanceString" /> returns the concrete value when given a
    ///     mix of concrete value and NOASSERTION.
    /// </summary>
    [TestMethod]
    public void SpdxHelpers_EnhanceString_ConcretePreferredOverNoAssertion_ReturnsConcreteValue()
    {
        // Arrange
        const string concrete = "MIT";
        const string noAssertion = SpdxElement.NoAssertion;

        // Act
        var result = SpdxHelpers.EnhanceString(noAssertion, concrete);

        // Assert
        Assert.AreEqual(concrete, result);
    }

    /// <summary>
    ///     Tests that <see cref="SpdxHelpers.EnhanceString" /> returns null when all inputs are null.
    /// </summary>
    [TestMethod]
    public void SpdxHelpers_EnhanceString_NullInputs_ReturnsNull()
    {
        // Arrange: all candidates are null

        // Act
        var result = SpdxHelpers.EnhanceString(null, null);

        // Assert
        Assert.IsNull(result);
    }
}
