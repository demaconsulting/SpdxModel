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
/// <remarks>
///     Unit tests for <see cref="SpdxCreationInformation"/>. Each test is self-contained
///     with no shared state and no external dependencies. Tests cover deep copy, enhance,
///     validate, and edge-case behaviors.
/// </remarks>
[TestClass]
public class SpdxCreationInformationTests
{
    /// <summary>
    ///     Tests the <see cref="SpdxCreationInformation.DeepCopy" /> method successfully creates a deep copy
    /// </summary>
    /// <remarks>
    ///     Exercises the deep-copy path with all four fields populated (two or more creators,
    ///     a created timestamp, a comment, and a license-list version). Verifying that both
    ///     the top-level reference and the Creators array reference are distinct confirms that
    ///     no shallow-copy aliasing occurs.
    /// </remarks>
    [TestMethod]
    public void SpdxCreationInformation_DeepCopy_WithAllFieldsPopulated_CreatesEqualButDistinctInstance()
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
    /// <remarks>
    ///     Exercises the enhance path where the base instance is missing a creator and the
    ///     LicenseListVersion field. The source instance provides both, allowing the test to
    ///     confirm additive merging of creators and fill-if-absent semantics for scalar fields.
    /// </remarks>
    [TestMethod]
    public void SpdxCreationInformation_Enhance_WithMissingFieldsInBase_AddsOrUpdatesInformationCorrectly()
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
    /// <remarks>
    ///     Boundary test: an empty Creators array is the minimal invalid state. Chosen to
    ///     confirm that the absence of any creator entry is caught independently of other
    ///     field values.
    /// </remarks>
    [TestMethod]
    public void SpdxCreationInformation_Validate_MissingCreators_ReportsIssue()
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
        Assert.Contains(issue => issue.Contains("Document Invalid Creator Field - Empty"), issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxCreationInformation.Validate" /> method reports invalid creators.
    /// </summary>
    /// <remarks>
    ///     Exercises the per-entry validation rule that each creator must start with
    ///     <c>Person:</c>, <c>Organization:</c>, or <c>Tool:</c>. The input <c>"BadCreator"</c>
    ///     fails all three prefixes, making the expected issue deterministic.
    /// </remarks>
    [TestMethod]
    public void SpdxCreationInformation_Validate_InvalidCreator_ReportsIssue()
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
        Assert.Contains(issue => issue.Contains("Document Invalid Creator Entry 'BadCreator'"), issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxCreationInformation.Validate" /> method reports invalid created dates.
    /// </summary>
    /// <remarks>
    ///     Exercises the Created field validation rule. The value <c>"BadDate"</c> is
    ///     chosen because it is unambiguously non-empty and non-conforming, confirming that
    ///     the regex/helper rejects it without false negatives.
    /// </remarks>
    [TestMethod]
    public void SpdxCreationInformation_Validate_InvalidCreatedDate_ReportsIssue()
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
        Assert.Contains(issue => issue.Contains("Document Invalid Created Field 'BadDate'"), issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxCreationInformation.Validate" /> method reports invalid versions.
    /// </summary>
    /// <remarks>
    ///     Exercises the LicenseListVersion field validation rule. The value
    ///     <c>"BadVersion"</c> does not match the <c>\d+\.\d+</c> pattern and confirms
    ///     that the regex rejects non-numeric version strings.
    /// </remarks>
    [TestMethod]
    public void SpdxCreationInformation_Validate_InvalidVersion_ReportsIssue()
    {
        // Arrange: Create creation information with invalid license list version
        var info = new SpdxCreationInformation
        {
            Creators = ["Tool: LicenseFind-1.0", "Organization: ExampleCodeInspect ()"],
            Created = "2021-01-01T00:00:00Z",
            Comment = "This package has been shipped in source and binary form.",
            LicenseListVersion = "BadVersion"
        };

        // Act: Perform validation on the SpdxCreationInformation instance
        var issues = new List<string>();
        info.Validate(issues);

        // Assert: Verify that the validation reports the invalid license list version
        Assert.Contains(issue => issue.Contains("Document Invalid License List Version Field 'BadVersion'"), issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxCreationInformation.Validate" /> method reports no issues for valid information.
    /// </summary>
    /// <remarks>
    ///     Happy-path test using a fully-populated valid instance. Confirms that no
    ///     spurious validation issues are reported when all fields satisfy their
    ///     respective rules.
    /// </remarks>
    [TestMethod]
    public void SpdxCreationInformation_Validate_ValidInformation_NoIssues()
    {
        // Arrange: Create valid creation information
        var info = new SpdxCreationInformation
        {
            Creators = ["Tool: LicenseFind-1.0", "Organization: ExampleCodeInspect ()"],
            Created = "2010-01-29T18:30:22Z",
            Comment = "This package has been shipped in source and binary form.",
            LicenseListVersion = "3.9"
        };

        // Act: Perform validation on the SpdxCreationInformation instance
        var issues = new List<string>();
        info.Validate(issues);

        // Assert: Verify that no issues are reported
        Assert.IsEmpty(issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxCreationInformation.Validate" /> method accepts an empty Created field.
    /// </summary>
    /// <remarks>
    ///     Boundary test: an empty Created field is permitted for partially-constructed
    ///     documents. Confirms that the validator does not report a date-format issue when
    ///     the field is intentionally left blank.
    /// </remarks>
    [TestMethod]
    public void SpdxCreationInformation_Validate_EmptyCreatedField_NoDateIssue()
    {
        // Arrange: Create creation information with an empty Created field
        var info = new SpdxCreationInformation
        {
            Creators = ["Tool: LicenseFind-1.0"],
            Created = ""
        };

        // Act: Perform validation
        var issues = new List<string>();
        info.Validate(issues);

        // Assert: Verify that no date-related issue is reported (empty Created is permitted)
        Assert.IsFalse(issues.Any(issue => issue.Contains("Invalid Created Field")));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxCreationInformation.Enhance" /> method deduplicates creators.
    /// </summary>
    /// <remarks>
    ///     Exercises the deduplication branch of the Enhance method. The base and source
    ///     instances share one common creator, allowing the test to confirm that the merged
    ///     Creators array contains exactly three distinct entries without duplicates.
    /// </remarks>
    [TestMethod]
    public void SpdxCreationInformation_Enhance_DuplicateCreators_DeduplicatesCreators()
    {
        // Arrange: Create creation information with an initial creator list that contains a duplicate
        var info = new SpdxCreationInformation
        {
            Creators = ["Tool: LicenseFind-1.0", "Organization: ExampleCodeInspect ()"],
            Created = "2010-01-29T18:30:22Z"
        };

        // Act: Enhance with an overlapping creator list
        info.Enhance(
            new SpdxCreationInformation
            {
                Creators = ["Tool: LicenseFind-1.0", "Person: Jane Doe ()"]
            });

        // Assert: Verify that duplicate creators are removed and unique entries are preserved
        Assert.HasCount(3, info.Creators);
        Assert.IsTrue(info.Creators.Contains("Tool: LicenseFind-1.0"));
        Assert.IsTrue(info.Creators.Contains("Organization: ExampleCodeInspect ()"));
        Assert.IsTrue(info.Creators.Contains("Person: Jane Doe ()"));
    }
}
