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

[TestClass]
public class Spdx2JsonDeserializeAnnotation
{
    [TestMethod]
    public void DeserializeAnnotation()
    {
        // Arrange
        var json = new JsonObject
        {
            ["annotationDate"] = "2010-01-29T18:30:22Z",
            ["annotationType"] = "OTHER",
            ["annotator"] = "Person: Jane Doe ()",
            ["comment"] = "Document level annotation"
        };

        // Act
        var annotation = Spdx2JsonDeserializer.DeserializeAnnotation(json);

        // Assert
        Assert.AreEqual("2010-01-29T18:30:22Z", annotation.Date);
        Assert.AreEqual(SpdxAnnotationType.Other, annotation.Type);
        Assert.AreEqual("Person: Jane Doe ()", annotation.Annotator);
        Assert.AreEqual("Document level annotation", annotation.Comment);
    }

    [TestMethod]
    public void DeserializeAnnotations()
    {
        // Arrange
        var json = new JsonArray
        {
            new JsonObject
            {
                ["annotationDate"] = "2010-01-29T18:30:22Z",
                ["annotationType"] = "OTHER",
                ["annotator"] = "Person: Jane Doe ()",
                ["comment"] = "Document level annotation"
            },
            new JsonObject
            {
                ["annotationDate"] = "2010-02-10T00:00:00Z",
                ["annotationType"] = "REVIEW",
                ["annotator"] = "Person: Joe Reviewer",
                ["comment"] =
                    "This is just an example.  Some of the non-standard licenses look like they are actually BSD 3 clause licenses"
            }
        };

        // Act
        var annotations = Spdx2JsonDeserializer.DeserializeAnnotations(json);

        // Assert
        Assert.AreEqual(2, annotations.Length);
        Assert.AreEqual("2010-01-29T18:30:22Z", annotations[0].Date);
        Assert.AreEqual(SpdxAnnotationType.Other, annotations[0].Type);
        Assert.AreEqual("Person: Jane Doe ()", annotations[0].Annotator);
        Assert.AreEqual("Document level annotation", annotations[0].Comment);
        Assert.AreEqual("2010-02-10T00:00:00Z", annotations[1].Date);
        Assert.AreEqual(SpdxAnnotationType.Review, annotations[1].Type);
        Assert.AreEqual("Person: Joe Reviewer", annotations[1].Annotator);
        Assert.AreEqual(
            "This is just an example.  Some of the non-standard licenses look like they are actually BSD 3 clause licenses",
            annotations[1].Comment);
    }
}