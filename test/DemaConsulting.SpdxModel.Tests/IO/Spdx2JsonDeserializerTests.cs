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

using System.Text.Json;
using DemaConsulting.SpdxModel.IO;

namespace DemaConsulting.SpdxModel.Tests.IO;

/// <summary>
///     Error-path tests for <see cref="Spdx2JsonDeserializer" />.
/// </summary>
/// <remarks>
///     Covers the error-handling paths of <see cref="Spdx2JsonDeserializer"/>: invalid JSON
///     input that should throw rather than return a partially-populated document.
/// </remarks>
[TestClass]
public class Spdx2JsonDeserializerTests
{
    /// <summary>
    ///     Tests that deserializing malformed JSON throws a <see cref="JsonException" />.
    /// </summary>
    /// <remarks>
    ///     Confirms that syntactically broken JSON (missing closing brace) causes a
    ///     <see cref="System.Text.Json.JsonException"/> rather than a silent failure.
    /// </remarks>
    [TestMethod]
    public void Spdx2JsonDeserializer_Deserialize_MalformedJson_ThrowsJsonException()
    {
        // Arrange
        const string malformedJson = "{ not valid json";

        // Act / Assert
        try
        {
            Spdx2JsonDeserializer.Deserialize(malformedJson);
            Assert.Fail("Expected JsonException was not thrown");
        }
        catch (JsonException)
        {
            // Pass
        }
    }
}
