using DemaConsulting.SpdxModel.IO;

namespace DemaConsulting.SpdxModel.Tests.IO;

[TestClass]
public class SpdxJsonSerializeAnnotation
{
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
        var json = SpdxJsonSerializer.SerializeAnnotation(annotation);

        // Assert
        Assert.AreEqual("SPDXRef-Annotation", json["SPDXID"]?.ToString());
        Assert.AreEqual("John Doe", json["annotator"]?.ToString());
        Assert.AreEqual("2021-09-01T12:00:00Z", json["annotationDate"]?.ToString());
        Assert.AreEqual("REVIEW", json["annotationType"]?.ToString());
        Assert.AreEqual("This is a comment", json["comment"]?.ToString());
    }

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
        var json = SpdxJsonSerializer.SerializeAnnotations(annotations);

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