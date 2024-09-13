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

[TestClass]
public class SpdxCreationInformationTests
{
    [TestMethod]
    public void DeepCopy()
    {
        var c1 = new SpdxCreationInformation
        {
            Creators = ["Tool: LicenseFind-1.0", "Organization: ExampleCodeInspect ()", "Person: Jane Doe ()"],
            Created = "2010-01-29T18:30:22Z",
            Comment = "This package has been shipped in source and binary form.",
            LicenseListVersion = "3.9"
        };

        var c2 = c1.DeepCopy();
        c2.Creators[2] = "Person: Malcolm Nixon";

        Assert.IsFalse(ReferenceEquals(c1, c2));
        Assert.AreEqual("Person: Jane Doe ()", c1.Creators[2]);
        Assert.AreEqual("Person: Malcolm Nixon", c2.Creators[2]);
    }

    [TestMethod]
    public void Enhance()
    {
        var info = new SpdxCreationInformation
        {
            Creators = ["Tool: LicenseFind-1.0", "Organization: ExampleCodeInspect ()"],
            Created = "2010-01-29T18:30:22Z",
            Comment = "This package has been shipped in source and binary form."
        };

        info.Enhance(
            new SpdxCreationInformation
            {
                Creators = ["Person: Jane Doe ()"],
                LicenseListVersion = "3.9"
            });

        Assert.AreEqual(3, info.Creators.Length);
        Assert.AreEqual("Tool: LicenseFind-1.0", info.Creators[0]);
        Assert.AreEqual("Organization: ExampleCodeInspect ()", info.Creators[1]);
        Assert.AreEqual("Person: Jane Doe ()", info.Creators[2]);
        Assert.AreEqual("2010-01-29T18:30:22Z", info.Created);
        Assert.AreEqual("This package has been shipped in source and binary form.", info.Comment);
        Assert.AreEqual("3.9", info.LicenseListVersion);
    }
}