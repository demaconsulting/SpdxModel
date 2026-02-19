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
///     Tests for serializing <see cref="SpdxAnnotation" /> to JSON.
/// </summary>
[TestClass]
public class Spdx2JsonSerializeAnnotation
{
    /// <summary>
    ///     Tests serializing an annotation.
    /// </summary>
    [TestMethod]
    public void Spdx2JsonSerializer_SerializeAnnotation_CorrectResults()
    {
        // Arrange: Create a sample annotation
        var annotation = new SpdxAnnotation
        {
            Id = "SPDXRef-Annotation",
            Annotator = "John Doe",
            Date = "2021-09-01T12:00:00Z",
            Type = SpdxAnnotationType.Review,
            Comment = "This is a comment"
        };

        // Act: Serialize the annotation to JSON
        var json = Spdx2JsonSerializer.SerializeAnnotation(annotation);

        // Assert: Verify the JSON is not null and has the expected structure
        SpdxJsonHelpers.AssertEqual("SPDXRef-Annotation", json["SPDXID"]);
        SpdxJsonHelpers.AssertEqual("John Doe", json["annotator"]);
        SpdxJsonHelpers.AssertEqual("2021-09-01T12:00:00Z", json["annotationDate"]);
        SpdxJsonHelpers.AssertEqual("REVIEW", json["annotationType"]);
        SpdxJsonHelpers.AssertEqual("This is a comment", json["comment"]);
    }

    /// <summary>
    ///     Tests serializing multiple annotations.
    /// </summary>
    [TestMethod]
    public void Spdx2JsonSerializer_SerializeAnnotations_CorrectResults()
    {
        // Arrange: Create a sample list of annotations
        var annotations = new[]
        {
            new SpdxAnnotation
            {
                Id = "SPDXRef-Annotation1",
                Annotator = "John Doe",
                Date = "2021-09-01T12:00:00Z",
                Type = SpdxAnnotationType.Review,
                Comment = "This is a comment"
            },
            new SpdxAnnotation
            {
                Id = "SPDXRef-Annotation2",
                Annotator = "Jane Doe",
                Date = "2021-09-02T12:00:00Z",
                Type = SpdxAnnotationType.Other,
                Comment = "This is another comment"
            }
        };

        // Act: Serialize the annotations to JSON
        var json = Spdx2JsonSerializer.SerializeAnnotations(annotations);

        // Assert: Verify the JSON is not null and has the expected structure
        Assert.IsNotNull(json);
        Assert.AreEqual(2, json.Count);
        SpdxJsonHelpers.AssertEqual("SPDXRef-Annotation1", json[0]?["SPDXID"]);
        SpdxJsonHelpers.AssertEqual("John Doe", json[0]?["annotator"]);
        SpdxJsonHelpers.AssertEqual("2021-09-01T12:00:00Z", json[0]?["annotationDate"]);
        SpdxJsonHelpers.AssertEqual("REVIEW", json[0]?["annotationType"]);
        SpdxJsonHelpers.AssertEqual("This is a comment", json[0]?["comment"]);
        SpdxJsonHelpers.AssertEqual("SPDXRef-Annotation2", json[1]?["SPDXID"]);
        SpdxJsonHelpers.AssertEqual("Jane Doe", json[1]?["annotator"]);
        SpdxJsonHelpers.AssertEqual("2021-09-02T12:00:00Z", json[1]?["annotationDate"]);
        SpdxJsonHelpers.AssertEqual("OTHER", json[1]?["annotationType"]);
        SpdxJsonHelpers.AssertEqual("This is another comment", json[1]?["comment"]);
    }
}
