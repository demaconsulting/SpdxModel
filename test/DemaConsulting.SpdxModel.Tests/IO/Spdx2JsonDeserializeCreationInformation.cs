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
/// Tests for deserializing SPDX creation information to <see cref="SpdxCreationInformation"/> classes.
/// </summary>
[TestClass]
public class Spdx2JsonDeserializeCreationInformation
{
    /// <summary>
    /// Tests deserializing creation information.
    /// </summary>
    [TestMethod]
    public void DeserializeCreationInformation()
    {
        // Arrange
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

        // Act
        var creationInformation = Spdx2JsonDeserializer.DeserializeCreationInformation(json);

        // Assert
        Assert.AreEqual(
            "This package has been shipped in source and binary form.\nThe binaries were created with gcc 4.5.1 and expect to link to\ncompatible system run time libraries.",
            creationInformation.Comment);
        Assert.AreEqual("2010-01-29T18:30:22Z", creationInformation.Created);
        Assert.AreEqual(3, creationInformation.Creators.Length);
        Assert.AreEqual("Tool: LicenseFind-1.0", creationInformation.Creators[0]);
        Assert.AreEqual("Organization: ExampleCodeInspect ()", creationInformation.Creators[1]);
        Assert.AreEqual("Person: Jane Doe ()", creationInformation.Creators[2]);
        Assert.AreEqual("3.17", creationInformation.LicenseListVersion);
    }
}