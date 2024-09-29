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
/// Tests for serializing <see cref="SpdxAnnotation"/> to JSON.
/// </summary>
[TestClass]
public class Spdx2JsonSerializeAnnotation
{
    /// <summary>
    /// Tests serializing an annotation.
    /// </summary>
    [TestMethod]
    public void SerializeAnnotation()
    {
        // Arrange
        var annotation = new SpdxAnnotation
        {
            Id = "SPDXRef-Annotation",
            Annotator = "John Doe",
            Date = "2021-09-01T12:00:00Z",
            Type = SpdxAnnotationType.Review,
            Comment = "This is a comment"
        };

        // Act
        var json = Spdx2JsonSerializer.SerializeAnnotation(annotation);

        // Assert
        Assert.AreEqual("SPDXRef-Annotation", json["SPDXID"]?.ToString());
        Assert.AreEqual("John Doe", json["annotator"]?.ToString());
        Assert.AreEqual("2021-09-01T12:00:00Z", json["annotationDate"]?.ToString());
        Assert.AreEqual("REVIEW", json["annotationType"]?.ToString());
        Assert.AreEqual("This is a comment", json["comment"]?.ToString());
    }

    /// <summary>
    /// Tests serializing multiple annotations.
    /// </summary>
    [TestMethod]
    public void SerializeAnnotations()
    {
        // Arrange
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

        // Act
        var json = Spdx2JsonSerializer.SerializeAnnotations(annotations);

        // Assert
        Assert.AreEqual(2, json.Count);
        Assert.AreEqual("SPDXRef-Annotation1", json[0]?["SPDXID"]?.ToString());
        Assert.AreEqual("John Doe", json[0]?["annotator"]?.ToString());
        Assert.AreEqual("2021-09-01T12:00:00Z", json[0]?["annotationDate"]?.ToString());
        Assert.AreEqual("REVIEW", json[0]?["annotationType"]?.ToString());
        Assert.AreEqual("This is a comment", json[0]?["comment"]?.ToString());
        Assert.AreEqual("SPDXRef-Annotation2", json[1]?["SPDXID"]?.ToString());
        Assert.AreEqual("Jane Doe", json[1]?["annotator"]?.ToString());
        Assert.AreEqual("2021-09-02T12:00:00Z", json[1]?["annotationDate"]?.ToString());
        Assert.AreEqual("OTHER", json[1]?["annotationType"]?.ToString());
        Assert.AreEqual("This is another comment", json[1]?["comment"]?.ToString());
    }
}