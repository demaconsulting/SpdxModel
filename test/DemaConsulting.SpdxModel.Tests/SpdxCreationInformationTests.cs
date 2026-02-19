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
///     Tests for the <see cref="SpdxCreationInformation" /> class.
/// </summary>
[TestClass]
public class SpdxCreationInformationTests
{
    /// <summary>
    ///     Tests the <see cref="SpdxCreationInformation.DeepCopy" /> method successfully creates a deep copy
    /// </summary>
    [TestMethod]
    public void SpdxCreationInformation_DeepCopy_CreatesEqualButDistinctInstance()
    {
        // Arrange: Create an instance of SpdxCreationInformation with multiple creators
        var c1 = new SpdxCreationInformation
        {
            Creators = ["Tool: LicenseFind-1.0", "Organization: ExampleCodeInspect ()", "Person: Jane Doe ()"],
            Created = "2010-01-29T18:30:22Z",
            Comment = "This package has been shipped in source and binary form.",
            LicenseListVersion = "3.9"
        };

        // Act: Create a deep copy of the SpdxCreationInformation instance
        var c2 = c1.DeepCopy();

        // Assert: Verify deep-copy is equal to original
        CollectionAssert.AreEqual(c1.Creators, c2.Creators);
        Assert.AreEqual(c1.Created, c2.Created);
        Assert.AreEqual(c1.Comment, c2.Comment);
        Assert.AreEqual(c1.LicenseListVersion, c2.LicenseListVersion);

        // Assert: Verify deep-copy has distinct instances
        Assert.IsFalse(ReferenceEquals(c1, c2));
        Assert.IsFalse(ReferenceEquals(c1.Creators, c2.Creators));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxCreationInformation.Enhance" /> method adds or updates information correctly
    /// </summary>
    [TestMethod]
    public void SpdxCreationInformation_Enhance_AddsOrUpdatesInformationCorrectly()
    {
        // Arrange: Create an instance of SpdxCreationInformation with initial values
        var info = new SpdxCreationInformation
        {
            Creators = ["Tool: LicenseFind-1.0", "Organization: ExampleCodeInspect ()"],
            Created = "2010-01-29T18:30:22Z",
            Comment = "This package has been shipped in source and binary form."
        };

        // Act: Enhance the instance with additional information
        info.Enhance(
            new SpdxCreationInformation
            {
                Creators = ["Person: Jane Doe ()"],
                LicenseListVersion = "3.9"
            });

        // Assert: Verify the enhanced information
        Assert.HasCount(3, info.Creators);
        Assert.AreEqual("Tool: LicenseFind-1.0", info.Creators[0]);
        Assert.AreEqual("Organization: ExampleCodeInspect ()", info.Creators[1]);
        Assert.AreEqual("Person: Jane Doe ()", info.Creators[2]);
        Assert.AreEqual("2010-01-29T18:30:22Z", info.Created);
        Assert.AreEqual("This package has been shipped in source and binary form.", info.Comment);
        Assert.AreEqual("3.9", info.LicenseListVersion);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxCreationInformation.Validate" /> method reports missing creators.
    /// </summary>
    [TestMethod]
    public void SpdxCreationInformation_Validate_MissingCreators()
    {
        // Arrange: Create creation information with empty creators array
        var info = new SpdxCreationInformation
        {
            Creators = [],
            Created = "2010-01-29T18:30:22Z",
            Comment = "This package has been shipped in source and binary form."
        };

        // Act: Perform validation on the SpdxCreationInformation instance
        var issues = new List<string>();
        info.Validate(issues);

        // Assert: Verify that the validation reports the missing creators
        Assert.IsTrue(
            issues.Any(issue => issue.Contains("Document Invalid Creator Field - Empty")));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxCreationInformation.Validate" /> method reports invalid creators.
    /// </summary>
    [TestMethod]
    public void SpdxCreationInformation_Validate_InvalidCreator()
    {
        // Arrange: Create creation information with invalid creator format
        var info = new SpdxCreationInformation
        {
            Creators = ["BadCreator"],
            Created = "2010-01-29T18:30:22Z",
            Comment = "This package has been shipped in source and binary form."
        };

        // Act: Perform validation on the SpdxCreationInformation instance
        var issues = new List<string>();
        info.Validate(issues);

        // Assert: Verify that the validation reports the invalid creator
        Assert.IsTrue(
            issues.Any(issue => issue.Contains("Document Invalid Creator Entry 'BadCreator'")));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxCreationInformation.Validate" /> method reports invalid created dates.
    /// </summary>
    [TestMethod]
    public void SpdxCreationInformation_Validate_InvalidCreatedDate()
    {
        // Arrange: Create creation information with invalid created date
        var info = new SpdxCreationInformation
        {
            Creators = ["Tool: LicenseFind-1.0", "Organization: ExampleCodeInspect ()"],
            Created = "BadDate",
            Comment = "This package has been shipped in source and binary form."
        };

        // Act: Perform validation on the SpdxCreationInformation instance
        var issues = new List<string>();
        info.Validate(issues);

        // Assert: Verify that the validation reports the invalid created date
        Assert.IsTrue(
            issues.Any(issue => issue.Contains("Document Invalid Created Field 'BadDate'")));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxCreationInformation.Validate" /> method reports invalid versions.
    /// </summary>
    [TestMethod]
    public void SpdxCreationInformation_Validate_InvalidVersion()
    {
        // Arrange: Create creation information with invalid license list version
        var info = new SpdxCreationInformation
        {
            Creators = ["Tool: LicenseFind-1.0", "Organization: ExampleCodeInspect ()"],
            Created = "BadDate",
            Comment = "This package has been shipped in source and binary form.",
            LicenseListVersion = "BadVersion"
        };

        // Act: Perform validation on the SpdxCreationInformation instance
        var issues = new List<string>();
        info.Validate(issues);

        // Assert: Verify that the validation reports the invalid license list version
        Assert.IsTrue(
            issues.Any(issue => issue.Contains("Document Invalid License List Version Field 'BadVersion'")));
    }
}
