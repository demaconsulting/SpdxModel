using System.Text.Json.Nodes;
using DemaConsulting.SpdxModel.IO;

namespace DemaConsulting.SpdxModel.Tests.IO;

[TestClass]
public class SpdxJsonDeserializeAnnotation
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
        var annotation = SpdxJsonDeserializer.DeserializeAnnotation(json);

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
        var annotations = SpdxJsonDeserializer.DeserializeAnnotations(json);

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