﻿// Copyright(c) 2024 DEMA Consulting
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
/// Tests for the <see cref="SpdxAnnotation"/> class.
/// </summary>
[TestClass]
public class SpdxAnnotationTests
{
    /// <summary>
    /// Tests the <see cref="SpdxAnnotation.Same"/> comparer.
    /// </summary>
    [TestMethod]
    public void AnnotationSameComparer()
    {
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

        // Assert annotations compare to themselves
        Assert.IsTrue(SpdxAnnotation.Same.Equals(a1, a1));
        Assert.IsTrue(SpdxAnnotation.Same.Equals(a2, a2));
        Assert.IsTrue(SpdxAnnotation.Same.Equals(a3, a3));

        // Assert annotations compare correctly
        Assert.IsTrue(SpdxAnnotation.Same.Equals(a1, a2));
        Assert.IsTrue(SpdxAnnotation.Same.Equals(a2, a1));
        Assert.IsFalse(SpdxAnnotation.Same.Equals(a1, a3));
        Assert.IsFalse(SpdxAnnotation.Same.Equals(a3, a1));
        Assert.IsFalse(SpdxAnnotation.Same.Equals(a2, a3));
        Assert.IsFalse(SpdxAnnotation.Same.Equals(a3, a2));

        // Assert same annotations have identical hashes
        Assert.AreEqual(SpdxAnnotation.Same.GetHashCode(a1), SpdxAnnotation.Same.GetHashCode(a2));
    }

    /// <summary>
    /// Tests the <see cref="SpdxAnnotation.DeepCopy"/> method.
    /// </summary>
    [TestMethod]
    public void DeepCopy()
    {
        var a1 = new SpdxAnnotation
        {
            Annotator = "Person: Malcolm Nixon",
            Date = "2024-05-28T01:30:00Z",
            Type = SpdxAnnotationType.Review,
            Comment = "Looks good"
        };

        var a2 = a1.DeepCopy();
        a2.Comment = "Looks bad";

        Assert.IsFalse(ReferenceEquals(a1, a2));
        Assert.AreEqual("Looks good", a1.Comment);
        Assert.AreEqual("Looks bad", a2.Comment);
    }

    /// <summary>
    /// Tests the <see cref="SpdxAnnotation.Enhance(SpdxAnnotation[], SpdxAnnotation[])"/> method.
    /// </summary>
    [TestMethod]
    public void Enhance()
    {
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

        Assert.AreEqual(2, annotations.Length);
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
    /// Tests the <see cref="SpdxAnnotationTypeExtensions.FromText(string)"/> method for valid annotation types.
    /// </summary>
    [TestMethod]
    public void SpdxAnnotationTypeExtensions_FromText_Valid()
    {
        Assert.AreEqual(SpdxAnnotationType.Missing, SpdxAnnotationTypeExtensions.FromText(""));
        Assert.AreEqual(SpdxAnnotationType.Review, SpdxAnnotationTypeExtensions.FromText("REVIEW"));
        Assert.AreEqual(SpdxAnnotationType.Review, SpdxAnnotationTypeExtensions.FromText("review"));
        Assert.AreEqual(SpdxAnnotationType.Review, SpdxAnnotationTypeExtensions.FromText("Review"));
        Assert.AreEqual(SpdxAnnotationType.Other, SpdxAnnotationTypeExtensions.FromText("OTHER"));
        Assert.AreEqual(SpdxAnnotationType.Other, SpdxAnnotationTypeExtensions.FromText("other"));
        Assert.AreEqual(SpdxAnnotationType.Other, SpdxAnnotationTypeExtensions.FromText("Other"));
    }

    /// <summary>
    /// Tests the <see cref="SpdxAnnotationTypeExtensions.FromText(string)"/> method for an invalid annotation type.
    /// </summary>
    [TestMethod]
    public void SpdxAnnotationTypeExtensions_FromText_Invalid()
    {
        var exception = Assert.ThrowsException<InvalidOperationException>(() => SpdxAnnotationTypeExtensions.FromText("invalid"));
        Assert.AreEqual($"Unsupported SPDX Annotation Type 'invalid'", exception.Message);
    }

    /// <summary>
    /// Tests the <see cref="SpdxAnnotationTypeExtensions.ToText(SpdxAnnotationType)"/> method for valid annotation types.
    /// </summary>
    [TestMethod]
    public void SpdxAnnotationTypeExtensions_ToText_Valid()
    {
        Assert.AreEqual("REVIEW", SpdxAnnotationType.Review.ToText());
        Assert.AreEqual("OTHER", SpdxAnnotationType.Other.ToText());
    }

    /// <summary>
    /// Tests the <see cref="SpdxAnnotationTypeExtensions.ToText(SpdxAnnotationType)"/> method for an invalid annotation type.
    /// </summary>
    [TestMethod]
    public void SpdxAnnotationTypeExtensions_ToText_Invalid()
    {
        var exception = Assert.ThrowsException<InvalidOperationException>(() => ((SpdxAnnotationType)1000).ToText());
        Assert.AreEqual($"Unsupported SPDX Annotation Type '1000'", exception.Message);
    }
}