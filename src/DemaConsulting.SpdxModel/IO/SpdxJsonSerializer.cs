using System.Text.Json;
using System.Text.Json.Nodes;

namespace DemaConsulting.SpdxModel.IO;

/// <summary>
/// JSON Serializer class
/// </summary>
public static class SpdxJsonSerializer
{
    /// <summary>
    /// Serialize SPDX Document
    /// </summary>
    /// <param name="document">SPDX Document</param>
    /// <returns>Json string</returns>
    public static string Serialize(SpdxDocument document)
    {
        // Serialize the document
        var json = SerializeDocument(document);

        // Convert to string
        return json.ToJsonString(
            new JsonSerializerOptions { WriteIndented = true });
    }

    /// <summary>
    /// Serialize SPDX Document
    /// </summary>
    /// <param name="document">SPDX Document</param>
    /// <returns>Json node</returns>
    private static JsonNode SerializeDocument(SpdxDocument document)
    {
        return new JsonObject
        {
            ["SPDXID"] = document.SpdxId,
            ["spdxVersion"] = document.SpdxVersion,
            ["name"] = document.Name,
            ["dataLicense"] = document.DataLicense,
            ["documentNamespace"] = document.DocumentNamespace
            // TODO - In progress
        };
    }
}