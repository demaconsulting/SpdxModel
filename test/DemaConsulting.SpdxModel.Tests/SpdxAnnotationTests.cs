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
///     Tests for the <see cref="SpdxAnnotation" /> class.
/// </summary>
/// <remarks>
///     Uses MSTest as the approved test framework for this repository (formal exception to
///     xUnit documented in <c>csharp-testing.md</c>). Each test method is fully isolated:
///     no shared state is maintained between tests and all test inputs are constructed
///     inline within the method body.
/// </remarks>
[TestClass]
public class SpdxAnnotationTests
{
    /// <summary>
    ///     Tests the <see cref="SpdxAnnotation.Same" /> comparer compares annotations correctly.
    /// </summary>
    /// <remarks>
    ///     Verifies reflexive equality (each annotation equals itself), cross-equality (two
    ///     annotations with identical fields but different IDs are equal), and that hash codes
    ///     match for equal annotations.
    /// </remarks>
    [TestMethod]
    public void SpdxAnnotation_SameComparer_ComparesCorrectly()
    {
        // Arrange: Create three annotations with different properties
        var a1 = new SpdxAnnotation
        {
            Annotator = "Person: Malcolm Nixon",
            Date = "2024-05-28T01:30:00Z",
            Type = SpdxAnnotationType.Review,
            Comment = "Looks good"
        };
        var a2 = new SpdxAnnotation
        {
            Id = "SPDXRef-Annotation1",
            Annotator = "Person: Malcolm Nixon",
            Date = "2024-05-28T01:30:00Z",
            Type = SpdxAnnotationType.Review,
            Comment = "Looks good"
        };
        var a3 = new SpdxAnnotation
        {
            Annotator = "Person: John Doe",
            Date = "2023-11-20T12:34:23Z",
            Type = SpdxAnnotationType.Other
        };

        // Act / Assert: Verify annotations compare to themselves
        Assert.IsTrue(SpdxAnnotation.Same.Equals(a1, a1));
        Assert.IsTrue(SpdxAnnotation.Same.Equals(a2, a2));
        Assert.IsTrue(SpdxAnnotation.Same.Equals(a3, a3));

        // Act / Assert: Verify annotations compare correctly
        Assert.IsTrue(SpdxAnnotation.Same.Equals(a1, a2));
        Assert.IsTrue(SpdxAnnotation.Same.Equals(a2, a1));
        Assert.IsFalse(SpdxAnnotation.Same.Equals(a1, a3));
        Assert.IsFalse(SpdxAnnotation.Same.Equals(a3, a1));
        Assert.IsFalse(SpdxAnnotation.Same.Equals(a2, a3));
        Assert.IsFalse(SpdxAnnotation.Same.Equals(a3, a2));

        // Act / Assert: Verify same annotations have identical hashes
        Assert.AreEqual(SpdxAnnotation.Same.GetHashCode(a1), SpdxAnnotation.Same.GetHashCode(a2));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxAnnotation.DeepCopy" /> method successfully creates a deep copy.
    /// </summary>
    /// <remarks>
    ///     Verifies that the deep copy is logically equal (all fields match) but is a distinct
    ///     object (not reference-equal), confirming no shared mutable references exist between
    ///     original and copy.
    /// </remarks>
    [TestMethod]
    public void SpdxAnnotation_DeepCopy_CreatesEqualButDistinctInstance()
    {
        // Arrange: Create an original SpdxAnnotation object
        var a1 = new SpdxAnnotation
        {
            Annotator = "Person: Malcolm Nixon",
            Date = "2024-05-28T01:30:00Z",
            Type = SpdxAnnotationType.Review,
            Comment = "Looks good"
        };

        // Act: Create a deep copy of the original object
        var a2 = a1.DeepCopy();

        // Assert: Verify deep-copy is equal to original
        Assert.AreEqual(a1, a2, SpdxAnnotation.Same);
        Assert.AreEqual(a1.Annotator, a2.Annotator);
        Assert.AreEqual(a1.Date, a2.Date);
        Assert.AreEqual(a1.Type, a2.Type);
        Assert.AreEqual(a1.Comment, a2.Comment);

        // Assert: Verify deep-copy has distinct instance
        Assert.IsFalse(ReferenceEquals(a1, a2));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxAnnotation.Enhance(SpdxAnnotation[], SpdxAnnotation[])" /> method adds or updates
    ///     information correctly
    /// </summary>
    /// <remarks>
    ///     Verifies two sub-scenarios in one test: (1) an existing annotation is enhanced with
    ///     additional field values from a matching entry (Id is populated), and (2) a new
    ///     annotation is appended when no match exists. Both sub-scenarios use the
    ///     <see cref="SpdxAnnotation.Same"/> comparer for matching.
    /// </remarks>
    [TestMethod]
    public void SpdxAnnotation_Enhance_AddsOrUpdatesInformationCorrectly()
    {
        // Arrange: Create an array of annotations with one annotation
        var annotations = new[]
        {
            new SpdxAnnotation
            {
                Annotator = "Person: Malcolm Nixon",
                Date = "2024-05-28T01:30:00Z",
                Type = SpdxAnnotationType.Review,
                Comment = "Looks good"
            }
        };

        // Act: Enhance the annotations with additional annotations
        annotations = SpdxAnnotation.Enhance(
            annotations,
            [
                new SpdxAnnotation
                {
                    Id = "SPDXRef-Annotation1",
                    Annotator = "Person: Malcolm Nixon",
                    Date = "2024-05-28T01:30:00Z",
                    Type = SpdxAnnotationType.Review,
                    Comment = "Looks good"
                },

                new SpdxAnnotation
                {
                    Annotator = "Person: John Doe",
                    Date = "2023-11-20T12:34:23Z",
                    Type = SpdxAnnotationType.Other
                }
            ]);

        // Assert: Verify the annotations array has correct information
        Assert.HasCount(2, annotations);
        Assert.AreEqual("SPDXRef-Annotation1", annotations[0].Id);
        Assert.AreEqual("Person: Malcolm Nixon", annotations[0].Annotator);
        Assert.AreEqual("2024-05-28T01:30:00Z", annotations[0].Date);
        Assert.AreEqual(SpdxAnnotationType.Review, annotations[0].Type);
        Assert.AreEqual("Looks good", annotations[0].Comment);
        Assert.AreEqual("Person: John Doe", annotations[1].Annotator);
        Assert.AreEqual("2023-11-20T12:34:23Z", annotations[1].Date);
        Assert.AreEqual(SpdxAnnotationType.Other, annotations[1].Type);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxAnnotation.Validate" /> method reports bad annotators.
    /// </summary>
    /// <remarks>
    ///     Boundary: an empty <see cref="SpdxAnnotation.Annotator"/> string is the only invalid
    ///     field; all other fields are valid so that the issue list contains exactly one entry
    ///     for the annotator.
    /// </remarks>
    [TestMethod]
    public void SpdxAnnotation_Validate_InvalidAnnotator()
    {
        // Arrange: Create a bad annotation
        var annotation = new SpdxAnnotation
        {
            Annotator = "",
            Date = "2024-05-28T01:30:00Z",
            Type = SpdxAnnotationType.Review,
            Comment = "Looks good"
        };

        // Act: Perform validation on the SpdxAnnotation instance.
        var issues = new List<string>();
        annotation.Validate("Test", issues);

        // Assert: Verify that the validation fails and the error message includes the description
        Assert.Contains(issue => issue.Contains("Test Invalid Annotator Field - Empty"), issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxAnnotation.Validate" /> method reports bad dates.
    /// </summary>
    /// <remarks>
    ///     Boundary: a non-ISO-8601 date string is the only invalid field; all other fields are
    ///     valid so that the issue list contains exactly one entry for the date.
    /// </remarks>
    [TestMethod]
    public void SpdxAnnotation_Validate_InvalidDate()
    {
        // Arrange: Create a bad annotation
        var annotation = new SpdxAnnotation
        {
            Annotator = "Person: Malcolm Nixon",
            Date = "BadDate",
            Type = SpdxAnnotationType.Review,
            Comment = "Looks good"
        };

        // Act: Perform validation on the SpdxAnnotation instance.
        var issues = new List<string>();
        annotation.Validate("Test", issues);

        // Assert: Verify that the validation fails and the error message includes the description
        Assert.Contains(issue => issue.Contains("Test Invalid Annotation Date Field 'BadDate'"), issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxAnnotation.Validate" /> method reports bad annotation types.
    /// </summary>
    /// <remarks>
    ///     Boundary: <see cref="SpdxAnnotationType.Missing"/> is the only invalid field; all
    ///     other fields are valid so that the issue list contains exactly one entry for the type.
    /// </remarks>
    [TestMethod]
    public void SpdxAnnotation_Validate_InvalidType()
    {
        // Arrange: Create a bad annotation
        var annotation = new SpdxAnnotation
        {
            Annotator = "Person: Malcolm Nixon",
            Date = "2024-05-28T01:30:00Z",
            Type = SpdxAnnotationType.Missing,
            Comment = "Looks good"
        };

        // Act: Perform validation on the SpdxAnnotation instance.
        var issues = new List<string>();
        annotation.Validate("Test", issues);

        // Assert: Verify that the validation fails and the error message includes the description
        Assert.Contains(issue => issue.Contains("Test Invalid Annotation Type Field - Missing"), issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxAnnotation.Validate" /> method reports bad comments.
    /// </summary>
    /// <remarks>
    ///     Boundary: an empty <see cref="SpdxAnnotation.Comment"/> string is the only invalid
    ///     field; <see cref="SpdxAnnotation.Type"/> is set to
    ///     <see cref="SpdxAnnotationType.Review"/> (valid) so that the issue list contains
    ///     exactly one entry for the comment.
    /// </remarks>
    [TestMethod]
    public void SpdxAnnotation_Validate_InvalidComment()
    {
        // Arrange: Create a bad annotation
        var annotation = new SpdxAnnotation
        {
            Annotator = "Person: Malcolm Nixon",
            Date = "2024-05-28T01:30:00Z",
            Type = SpdxAnnotationType.Review,
            Comment = ""
        };

        // Act: Perform validation on the SpdxAnnotation instance.
        var issues = new List<string>();
        annotation.Validate("Test", issues);

        // Assert: Verify that the validation fails and the error message includes the description
        Assert.Contains(issue => issue.Contains("Test Invalid Annotation Comment - Empty"), issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxAnnotationTypeExtensions.FromText(string)" /> method for valid annotation types.
    /// </summary>
    /// <remarks>
    ///     Verifies case-insensitive mapping: "REVIEW", "review", and "Review" all map to
    ///     <see cref="SpdxAnnotationType.Review"/>; similarly for "OTHER". An empty string maps
    ///     to <see cref="SpdxAnnotationType.Missing"/>.
    /// </remarks>
    [TestMethod]
    public void SpdxAnnotationTypeExtensions_FromText_Valid()
    {
        // Arrange: no setup needed - testing pure string-to-enum conversion

        // Act / Assert: each recognized text converts to the correct enum value
        Assert.AreEqual(SpdxAnnotationType.Missing, SpdxAnnotationTypeExtensions.FromText(""));
        Assert.AreEqual(SpdxAnnotationType.Review, SpdxAnnotationTypeExtensions.FromText("REVIEW"));
        Assert.AreEqual(SpdxAnnotationType.Review, SpdxAnnotationTypeExtensions.FromText("review"));
        Assert.AreEqual(SpdxAnnotationType.Review, SpdxAnnotationTypeExtensions.FromText("Review"));
        Assert.AreEqual(SpdxAnnotationType.Other, SpdxAnnotationTypeExtensions.FromText("OTHER"));
        Assert.AreEqual(SpdxAnnotationType.Other, SpdxAnnotationTypeExtensions.FromText("other"));
        Assert.AreEqual(SpdxAnnotationType.Other, SpdxAnnotationTypeExtensions.FromText("Other"));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxAnnotationTypeExtensions.FromText(string)" /> method for an invalid annotation type.
    /// </summary>
    /// <remarks>
    ///     Boundary: an unrecognized string causes <see cref="InvalidOperationException"/> with
    ///     the expected message, confirming the error path is not silently swallowed.
    /// </remarks>
    [TestMethod]
    public void SpdxAnnotationTypeExtensions_FromText_Invalid()
    {
        // Arrange: no setup needed — testing pure string-to-enum conversion error path

        // Act / Assert:
        var exception =
            Assert.ThrowsExactly<InvalidOperationException>(() => SpdxAnnotationTypeExtensions.FromText("invalid"));
        Assert.AreEqual("Unsupported SPDX Annotation Type 'invalid'", exception.Message);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxAnnotationTypeExtensions.ToText(SpdxAnnotationType)" /> method for valid annotation types.
    /// </summary>
    /// <remarks>
    ///     Verifies that <see cref="SpdxAnnotationType.Review"/> produces <c>"REVIEW"</c> and
    ///     <see cref="SpdxAnnotationType.Other"/> produces <c>"OTHER"</c>.
    /// </remarks>
    [TestMethod]
    public void SpdxAnnotationTypeExtensions_ToText_Valid()
    {
        // Arrange: no setup needed - testing pure enum-to-string conversion

        // Act / Assert: each recognized enum value converts to the expected text
        Assert.AreEqual("REVIEW", SpdxAnnotationType.Review.ToText());
        Assert.AreEqual("OTHER", SpdxAnnotationType.Other.ToText());
    }

    /// <summary>
    ///     Tests the <see cref="SpdxAnnotationTypeExtensions.ToText(SpdxAnnotationType)" /> method for an invalid annotation
    ///     type.
    /// </summary>
    /// <remarks>
    ///     Boundary: an unrecognized numeric enum value (1000, which is not a defined
    ///     <see cref="SpdxAnnotationType"/> member) causes <see cref="InvalidOperationException"/>
    ///     with the expected message.
    /// </remarks>
    [TestMethod]
    public void SpdxAnnotationTypeExtensions_ToText_Invalid()
    {
        // Arrange: no setup needed - testing pure enum-to-string conversion error path

        // Act / Assert: an unknown numeric enum value throws
        var exception = Assert.ThrowsExactly<InvalidOperationException>(() => ((SpdxAnnotationType)1000).ToText());
        Assert.AreEqual("Unsupported SPDX Annotation Type '1000'", exception.Message);
    }

    /// <summary>
    ///     Tests that <see cref="SpdxAnnotationTypeExtensions.ToText(SpdxAnnotationType)" /> throws for the
    ///     <see cref="SpdxAnnotationType.Missing" /> sentinel value.
    /// </summary>
    /// <remarks>
    ///     Boundary: <see cref="SpdxAnnotationType.Missing"/> is a sentinel value that must
    ///     never be serialized. Calling <c>ToText</c> with it throws
    ///     <see cref="InvalidOperationException"/> with the expected "Attempt to serialize
    ///     missing SPDX Annotation Type" message.
    /// </remarks>
    [TestMethod]
    public void SpdxAnnotationTypeExtensions_ToText_Missing()
    {
        // Arrange: no setup needed - testing the Missing sentinel value error path

        // Act / Assert: Missing throws with the expected message
        var exception = Assert.ThrowsExactly<InvalidOperationException>(
            () => SpdxAnnotationType.Missing.ToText());
        Assert.AreEqual("Attempt to serialize missing SPDX Annotation Type", exception.Message);
    }
}
