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

using DemaConsulting.SpdxModel.IO;

namespace DemaConsulting.SpdxModel.Tests.IO;

/// <summary>
/// Tests for serializing <see cref="SpdxCreationInformation"/> to JSON.
/// </summary>
[TestClass]
public class Spdx2JsonSerializeCreationInformation
{
    /// <summary>
    /// Tests serializing creation information.
    /// </summary>
    [TestMethod]
    public void SerializeCreationInformation()
    {
        // Arrange
        var creationInformation = new SpdxCreationInformation
        {
            Comment =
                "This package has been shipped in source and binary form.\nThe binaries were created with gcc 4.5.1 and expect to link to\ncompatible system run time libraries.",
            Created = "2010-01-29T18:30:22Z",
            Creators =
            [
                "Tool: LicenseFind-1.0",
                "Organization: ExampleCodeInspect ()",
                "Person: Jane Doe ()"
            ],
            LicenseListVersion = "3.17"
        };

        // Act
        var json = Spdx2JsonSerializer.SerializeCreationInformation(creationInformation);

        // Assert
        SpdxJsonHelpers.AssertEqual(
            "This package has been shipped in source and binary form.\nThe binaries were created with gcc 4.5.1 and expect to link to\ncompatible system run time libraries.",
            json["comment"]);
        SpdxJsonHelpers.AssertEqual("2010-01-29T18:30:22Z", json["created"]);
        SpdxJsonHelpers.AssertEqual("Tool: LicenseFind-1.0", json["creators"]?[0]);
        SpdxJsonHelpers.AssertEqual("Organization: ExampleCodeInspect ()", json["creators"]?[1]);
        SpdxJsonHelpers.AssertEqual("Person: Jane Doe ()", json["creators"]?[2]);
        SpdxJsonHelpers.AssertEqual("3.17", json["licenseListVersion"]);
    }
}