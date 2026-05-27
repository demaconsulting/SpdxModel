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

using System.Text.Json.Nodes;
using DemaConsulting.SpdxModel.IO;

namespace DemaConsulting.SpdxModel.Tests.IO;

/// <summary>
///     Tests for deserializing SPDX creation information to <see cref="SpdxCreationInformation" /> classes.
/// </summary>
/// <remarks>
///     Exercises deserialization of SPDX creation information using xUnit v3 as the test
///     framework. Constructs inline JSON and verifies the resulting
///     <see cref="SpdxCreationInformation"/> fields.
/// </remarks>
public class Spdx2JsonDeserializeCreationInformation
{
    /// <summary>
    ///     Tests deserializing creation information.
    /// </summary>
    /// <remarks>
    ///     Verifies that all creation information fields (comment, created, creators array,
    ///     licenseListVersion) are correctly mapped to the
    ///     <see cref="SpdxCreationInformation"/> properties.
    /// </remarks>
    [Fact]
    public void Spdx2JsonDeserializer_DeserializeCreationInformation_ValidInput_CorrectResults()
    {
        // Arrange: Create a JSON object representing creation information
        var json = new JsonObject
        {
            ["comment"] =
                "This package has been shipped in source and binary form.\nThe binaries were created with gcc 4.5.1 and expect to link to\ncompatible system run time libraries.",
            ["created"] = "2010-01-29T18:30:22Z",
            ["creators"] = new JsonArray
            {
                "Tool: LicenseFind-1.0",
                "Organization: ExampleCodeInspect ()",
                "Person: Jane Doe ()"
            },
            ["licenseListVersion"] = "3.17"
        };

        // Act: Deserialize the JSON object to an SpdxCreationInformation object
        var creationInformation = Spdx2JsonDeserializer.DeserializeCreationInformation(json);

        // Assert: Verify the deserialized object has the expected properties
        Assert.Equal(
            "This package has been shipped in source and binary form.\nThe binaries were created with gcc 4.5.1 and expect to link to\ncompatible system run time libraries.",
            creationInformation.Comment);
        Assert.Equal("2010-01-29T18:30:22Z", creationInformation.Created);
        Assert.Equal(3, creationInformation.Creators.Length);
        Assert.Equal("Tool: LicenseFind-1.0", creationInformation.Creators[0]);
        Assert.Equal("Organization: ExampleCodeInspect ()", creationInformation.Creators[1]);
        Assert.Equal("Person: Jane Doe ()", creationInformation.Creators[2]);
        Assert.Equal("3.17", creationInformation.LicenseListVersion);
    }
}
