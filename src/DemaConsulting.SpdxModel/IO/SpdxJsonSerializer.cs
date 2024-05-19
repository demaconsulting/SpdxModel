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
    /// <returns>Json object</returns>
    public static JsonObject SerializeDocument(SpdxDocument document)
    {
        var json = new JsonObject();
        EmitString(json, "SPDXID", document.Id);
        EmitString(json, "spdxVersion", document.Version);
        EmitString(json, "name", document.Name);
        EmitString(json, "dataLicense", document.DataLicense);
        EmitString(json, "documentNamespace", document.DocumentNamespace);
        EmitOptionalString(json, "comment", document.Comment);
        json["creationInfo"] = SerializeCreationInformation(document.CreationInformation);
        if (document.ExternalDocumentReferences.Length > 0)
            json["externalDocumentRefs"] = SerializeExternalDocumentReferences(document.ExternalDocumentReferences);
        if (document.ExtractedLicensingInfo.Length > 0)
            json["hasExtractedLicensingInfos"] = SerializeExtractedLicensingInfos(document.ExtractedLicensingInfo);
        if (document.Annotations.Length > 0)
            json["annotations"] = SerializeAnnotations(document.Annotations);
        json["files"] = SerializeFiles(document.Files);
        json["packages"] = SerializePackages(document.Packages);
        json["snippets"] = SerializeSnippets(document.Snippets);
        json["relationships"] = SerializeRelationships(document.Relationships);
        EmitOptionalStrings(json, "documentDescribes", document.Describes);
        return json;
    }

    /// <summary>
    /// Serialize SPDX Creation information to JSON object
    /// </summary>
    /// <param name="info">SPDX Creation Information</param>
    /// <returns>JSON object</returns>
    public static JsonObject SerializeCreationInformation(SpdxCreationInformation info)
    {
        var json = new JsonObject();
        EmitOptionalStrings(json, "creators", info.Creators);
        EmitString(json, "created", info.Created);
        EmitOptionalString(json, "comment", info.Comment);
        EmitOptionalString(json, "licenseListVersion", info.LicenseListVersion);
        return json;
    }

    /// <summary>
    /// Serialize array of SPDX External Document References to JSON array
    /// </summary>
    /// <param name="references">SPDX External Document References</param>
    /// <returns>JSON array</returns>
    public static JsonArray SerializeExternalDocumentReferences(SpdxExternalDocumentReference[] references)
    {
        var json = new JsonArray();
        foreach (var reference in references)
            json.Add(SerializeExternalDocumentReference(reference));
        return json;
    }

    /// <summary>
    /// Serialize SPDX External Document Reference to JSON object
    /// </summary>
    /// <param name="reference">SPDX External Document Reference</param>
    /// <returns>JSON object</returns>
    public static JsonObject SerializeExternalDocumentReference(SpdxExternalDocumentReference reference)
    {
        var json = new JsonObject();
        EmitString(json, "externalDocumentId", reference.ExternalDocumentId);
        json["checksum"] = SerializeChecksum(reference.Checksum);
        EmitString(json, "spdxDocument", reference.Document);
        return json;
    }

    /// <summary>
    /// Serialize array of SPDX Extracted Licensing Infos to JSON array
    /// </summary>
    /// <param name="infos">SPDX Extracted Licensing Infos</param>
    /// <returns>JSON array</returns>
    public static JsonArray SerializeExtractedLicensingInfos(SpdxExtractedLicensingInfo[] infos)
    {
        var json = new JsonArray();
        foreach (var info in infos)
            json.Add(SerializeExtractedLicensingInfo(info));
        return json;
    }

    /// <summary>
    /// Serialize SPDX Extracted Licensing Info to JSON object
    /// </summary>
    /// <param name="info">SPDX Extracted Licensing Info</param>
    /// <returns>JSON object</returns>
    public static JsonObject SerializeExtractedLicensingInfo(SpdxExtractedLicensingInfo info)
    {
        var json = new JsonObject();
        EmitOptionalString(json, "licenseId", info.LicenseId);
        EmitOptionalString(json, "extractedText", info.ExtractedText);
        EmitOptionalString(json, "name", info.Name);
        EmitOptionalStrings(json, "seeAlsos", info.CrossReferences);
        EmitOptionalString(json, "comment", info.Comment);
        return json;
    }

    /// <summary>
    /// Serialize array of SPDX Files to JSON array
    /// </summary>
    /// <param name="files">SPDX Files</param>
    /// <returns>JSON array</returns>
    public static JsonArray SerializeFiles(SpdxFile[] files)
    {
        var json = new JsonArray();
        foreach (var file in files)
            json.Add(SerializeFile(file));
        return json;
    }

    /// <summary>
    /// Serialize SPDX File to JSON object
    /// </summary>
    /// <param name="file">SPDX File</param>
    /// <returns>JSON object</returns>
    public static JsonObject SerializeFile(SpdxFile file)
    {
        var json = new JsonObject();
        EmitString(json, "SPDXID", file.Id);
        EmitString(json, "fileName", file.FileName);
        EmitOptionalStrings(json, "fileTypes", file.FileTypes.Select(SpdxFileTypeExtensions.ToText).ToArray());
        json["checksums"] = SerializeChecksums(file.Checksums);
        EmitOptionalString(json, "licenseConcluded", file.LicenseConcluded);
        EmitOptionalStrings(json, "licenseInfoInFile", file.LicenseInfoInFiles);
        EmitOptionalString(json, "licenseComments", file.LicenseComments);
        EmitOptionalString(json, "copyrightText", file.Copyright);
        EmitOptionalString(json, "comment", file.Comment);
        EmitOptionalString(json, "noticeText", file.Notice);
        EmitOptionalStrings(json, "fileContributors", file.Contributors);
        EmitOptionalStrings(json, "attributionTexts", file.AttributionText);
        if (file.Annotations.Length > 0)
            json["annotations"] = SerializeAnnotations(file.Annotations);
        return json;
    }

    /// <summary>
    /// Serialize array of SPDX Packages to JSON array
    /// </summary>
    /// <param name="packages">SPDX Packages</param>
    /// <returns>JSON array</returns>
    public static JsonArray SerializePackages(SpdxPackage[] packages)
    {
        var json = new JsonArray();
        foreach (var package in packages)
            json.Add(SerializePackage(package));
        return json;
    }

    /// <summary>
    /// Serialize SPDX Package to JSON object
    /// </summary>
    /// <param name="package">SPDX Package</param>
    /// <returns>JSON object</returns>
    public static JsonObject SerializePackage(SpdxPackage package)
    {
        var json = new JsonObject();
        EmitString(json, "SPDXID", package.Id);
        EmitString(json, "name", package.Name);
        EmitOptionalString(json, "versionInfo", package.Version);
        EmitOptionalString(json, "packageFileName", package.FileName);
        EmitOptionalString(json, "supplier", package.Supplier);
        EmitOptionalString(json, "originator", package.Originator);
        EmitOptionalString(json, "downloadLocation", package.DownloadLocation);
        if (package.FilesAnalyzed != null)
            json["filesAnalyzed"] = package.FilesAnalyzed;
        EmitOptionalStrings(json, "hasFiles", package.HasFiles);
        if (package.VerificationCode != null)
            json["packageVerificationCode"] = SerializeVerificationCode(package.VerificationCode);
        json["checksums"] = SerializeChecksums(package.Checksums);
        EmitOptionalString(json, "homepage", package.HomePage);
        EmitOptionalString(json, "sourceInfo", package.SourceInformation);
        EmitOptionalString(json, "licenseConcluded", package.ConcludedLicense);
        EmitOptionalStrings(json, "licenseInfoFromFiles", package.LicenseInfoFromFiles);
        EmitString(json, "licenseDeclared", package.DeclaredLicense);
        EmitOptionalString(json, "licenseComments", package.LicenseComments);
        EmitOptionalString(json, "copyrightText", package.CopyrightText);
        EmitOptionalString(json, "summary", package.Summary);
        EmitOptionalString(json, "description", package.Description);
        EmitOptionalString(json, "comment", package.Comment);
        if (package.ExternalReferences.Length > 0)
            json["externalRefs"] = SerializeExternalReferences(package.ExternalReferences);
        EmitOptionalStrings(json, "attributionTexts", package.Attributions);
        EmitOptionalString(json, "primaryPackagePurpose", package.PrimaryPackagePurpose);
        EmitOptionalString(json, "releaseDate", package.ReleaseDate);
        EmitOptionalString(json, "builtDate", package.BuiltDate);
        EmitOptionalString(json, "validUntilDate", package.ValidUntilDate);
        if (package.Annotations.Length > 0)
            json["annotations"] = SerializeAnnotations(package.Annotations);

        return json;
    }

    /// <summary>
    /// Serialize array of SPDX Snippets to JSON array
    /// </summary>
    /// <param name="snippets">SPDX Snippets</param>
    /// <returns>JSON array</returns>
    public static JsonArray SerializeSnippets(SpdxSnippet[] snippets)
    {
        var json = new JsonArray();
        foreach (var snippet in snippets)
            json.Add(SerializeSnippet(snippet));
        return json;
    }

    /// <summary>
    /// Serialize SPDX Snippet to JSON object
    /// </summary>
    /// <param name="snippet">SPDX Snippet</param>
    /// <returns>JSON object</returns>
    public static JsonObject SerializeSnippet(SpdxSnippet snippet)
    {
        var json = new JsonObject();
        EmitString(json, "SPDXID", snippet.Id);
        EmitString(json, "snippetFromFile", snippet.SnippetFromFile);
        EmitOptionalString(json, "name", snippet.Name);
        EmitString(json, "licenseConcluded", snippet.ConcludedLicense);
        EmitOptionalStrings(json, "licenseInfoInSnippets", snippet.LicenseInfoInSnippet);
        EmitOptionalString(json, "licenseComments", snippet.LicenseComments);
        EmitString(json, "copyrightText", snippet.Copyright);
        EmitOptionalString(json, "comment", snippet.Comment);
        EmitOptionalStrings(json, "attributionTexts", snippet.AttributionText);

        // Add ranges
        var ranges = new JsonArray
        {
            new JsonObject
            {
                ["endPointer"] = new JsonObject
                {
                    ["reference"] = snippet.SnippetFromFile,
                    ["offset"] = snippet.SnippetByteEnd
                },
                ["startPointer"] = new JsonObject
                {
                    ["reference"] = snippet.SnippetFromFile,
                    ["offset"] = snippet.SnippetByteStart
                }
            }
        };
        if (snippet.SnippetLineEnd > 0 || snippet.SnippetLineStart > 0)
            ranges.Add(new JsonObject
            {
                ["endPointer"] = new JsonObject
                {
                    ["reference"] = snippet.SnippetFromFile,
                    ["lineNumber"] = snippet.SnippetLineEnd
                },
                ["startPointer"] = new JsonObject
                {
                    ["reference"] = snippet.SnippetFromFile,
                    ["lineNumber"] = snippet.SnippetLineStart
                }
            });
        json["ranges"] = ranges;
        return json;
    }

    /// <summary>
    /// Serialize array of SPDX Relationships to JSON array
    /// </summary>
    /// <param name="relationships">SPDX Relationships</param>
    /// <returns>JSON array</returns>
    public static JsonArray SerializeRelationships(SpdxRelationship[] relationships)
    {
        var json = new JsonArray();
        foreach (var relationship in relationships)
            json.Add(SerializeRelationship(relationship));
        return json;
    }

    /// <summary>
    /// Serialize SPDX Relationship to JSON object
    /// </summary>
    /// <param name="relationship">SPDX Relationship</param>
    /// <returns>JSON object</returns>
    public static JsonObject SerializeRelationship(SpdxRelationship relationship)
    {
        var json = new JsonObject();
        EmitString(json, "spdxElementId", relationship.Id);
        EmitString(json, "relatedSpdxElement", relationship.RelatedSpdxElement);
        EmitString(json, "relationshipType", relationship.RelationshipType.ToText());
        EmitOptionalString(json, "comment", relationship.Comment);
        return json;
    }

    /// <summary>
    /// Serialize SPDX Package Verification Code to JSON object
    /// </summary>
    /// <param name="code">SPDX Package Verification Code</param>
    /// <returns>JSON object</returns>
    public static JsonObject SerializeVerificationCode(SpdxPackageVerificationCode code)
    {
        var json = new JsonObject();
        EmitString(json, "packageVerificationCodeValue", code.Value);
        EmitOptionalStrings(json, "packageVerificationCodeExcludedFiles", code.ExcludedFiles);
        return json;
    }

    /// <summary>
    /// Serialize array of SPDX External References to JSON array
    /// </summary>
    /// <param name="references">SPDX External References</param>
    /// <returns>JSON array</returns>
    public static JsonArray SerializeExternalReferences(SpdxExternalReference[] references)
    {
        var json = new JsonArray();
        foreach (var checksum in references)
            json.Add(SerializeExternalReference(checksum));
        return json;
    }

    /// <summary>
    /// Serialize SPDX External Reference to JSON object
    /// </summary>
    /// <param name="reference">SPDX External Reference</param>
    /// <returns>JSON object</returns>
    public static JsonObject SerializeExternalReference(SpdxExternalReference reference)
    {
        var json = new JsonObject();
        EmitString(json, "referenceCategory", reference.Category.ToText());
        EmitString(json, "referenceType", reference.Type);
        EmitString(json, "referenceLocator", reference.Locator);
        EmitOptionalString(json, "comment", reference.Comment);
        return json;
    }

    /// <summary>
    /// Serialize array of SPDX Checksums to JSON array
    /// </summary>
    /// <param name="checksums">SPDX Checksums</param>
    /// <returns>JSON array</returns>
    public static JsonArray SerializeChecksums(SpdxChecksum[] checksums)
    {
        var json = new JsonArray();
        foreach (var checksum in checksums)
            json.Add(SerializeChecksum(checksum));
        return json;
    }

    /// <summary>
    /// Serialize SPDX Checksum to JSON object
    /// </summary>
    /// <param name="checksum">SPDX Checksum</param>
    /// <returns>JSON object</returns>
    public static JsonObject SerializeChecksum(SpdxChecksum checksum)
    {
        var json = new JsonObject();
        EmitString(json, "algorithm", checksum.Algorithm.ToText());
        EmitString(json, "checksumValue", checksum.Value);
        return json;
    }

    /// <summary>
    /// Serialize array of SPDX Annotations to JSON array
    /// </summary>
    /// <param name="annotations">SPDX Annotations</param>
    /// <returns>JSON array</returns>
    public static JsonArray SerializeAnnotations(SpdxAnnotation[] annotations)
    {
        var json = new JsonArray();
        foreach (var annotation in annotations)
            json.Add(SerializeAnnotation(annotation));
        return json;
    }

    /// <summary>
    /// Serialize SPDX Annotation to JSON object
    /// </summary>
    /// <param name="annotation">SPDX Annotation to serialize</param>
    /// <returns>JSON object</returns>
    public static JsonObject SerializeAnnotation(SpdxAnnotation annotation)
    {
        var json = new JsonObject();
        EmitOptionalString(json, "SPDXID", annotation.Id);
        EmitString(json, "annotator", annotation.Annotator);
        EmitString(json, "annotationDate", annotation.Date);
        EmitString(json, "annotationType", annotation.Type.ToText());
        EmitOptionalString(json, "comment", annotation.Comment);
        return json;
    }

    /// <summary>
    /// Emit a string property into a JSON object
    /// </summary>
    /// <param name="json">JSON object</param>
    /// <param name="name">Property name</param>
    /// <param name="value">Property value</param>
    private static void EmitString(JsonObject json, string name, string value)
    {
        json[name] = value;
    }

    /// <summary>
    /// Emit an optional string property into a JSON object
    /// </summary>
    /// <param name="json">JSON object</param>
    /// <param name="name">Property name</param>
    /// <param name="value">Optional property value</param>
    private static void EmitOptionalString(JsonObject json, string name, string? value)
    {
        // Skip if empty
        if (string.IsNullOrEmpty(value))
            return;

        json[name] = value;
    }

    private static void EmitOptionalStrings(JsonObject json, string name, string[] values)
    {
        // Skip if empty
        if (values.Length == 0)
            return;

        var array = new JsonArray();
        foreach (var value in values)
            array.Add(value);
        json[name] = array;
    }
}