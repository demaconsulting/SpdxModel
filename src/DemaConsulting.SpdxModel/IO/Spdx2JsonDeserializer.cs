using System.Text.Json;
using System.Text.Json.Nodes;

namespace DemaConsulting.SpdxModel.IO;

/// <summary>
/// JSON Deserializer class
/// </summary>
public static class Spdx2JsonDeserializer
{
    /// <summary>
    /// Deserialize SPDX Document
    /// </summary>
    /// <param name="json">Json string</param>
    /// <returns>SPDX Document</returns>
    /// <exception cref="JsonException">thrown on error</exception>
    public static SpdxDocument Deserialize(string json)
    {
        // Deserialize the Json
        var dom = JsonNode.Parse(json) ??
                  throw new JsonException("Invalid JSON document");

        // Deserialize the SPDX Document
        return DeserializeDocument(dom);
    }

    /// <summary>
    /// Deserialize the SPDX Document
    /// </summary>
    /// <param name="json">Json Document Node</param>
    /// <returns>SPDX Document</returns>
    public static SpdxDocument DeserializeDocument(JsonNode json)
    {
        return new SpdxDocument
        {
            Id = ParseString(json, "SPDXID"),
            Version = ParseString(json, "spdxVersion"),
            Name = ParseString(json, "name"),
            DataLicense = ParseString(json, "dataLicense"),
            DocumentNamespace = ParseString(json, "documentNamespace"),
            Comment = ParseOptionalString(json, "comment"),
            CreationInformation = DeserializeCreationInformation(json["creationInfo"]),
            ExternalDocumentReferences = DeserializeExternalDocumentReferences(json["externalDocumentRefs"]?.AsArray()),
            ExtractedLicensingInfo = DeserializeExtractedLicensingInfos(json["hasExtractedLicensingInfos"]?.AsArray()),
            Annotations = DeserializeAnnotations(json["annotations"]?.AsArray()),
            Files = DeserializeFiles(json["files"]?.AsArray()),
            Packages = DeserializePackages(json["packages"]?.AsArray()),
            Snippets = DeserializeSnippets(json["snippets"]?.AsArray()),
            Relationships = DeserializeRelationships(json["relationships"]?.AsArray()),
            Describes = ParseStringArray(json, "documentDescribes")
        };
    }

    /// <summary>
    /// Deserialize SPDX Creation Information
    /// </summary>
    /// <param name="json">Json Creation Information Node</param>
    /// <returns>SPDX Document</returns>
    public static SpdxCreationInformation DeserializeCreationInformation(JsonNode? json)
    {
        return new SpdxCreationInformation
        {
            Creators = ParseStringArray(json, "creators"),
            Created = ParseString(json, "created"),
            Comment = ParseOptionalString(json, "comment"),
            LicenseListVersion = ParseOptionalString(json, "licenseListVersion")
        };
    }

    /// <summary>
    /// Deserialize SPDX External Document References
    /// </summary>
    /// <param name="json">Json External Document References Array</param>
    /// <returns>SPDX External Document References</returns>
    public static SpdxExternalDocumentReference[] DeserializeExternalDocumentReferences(JsonArray? json)
    {
        return json?.Select(DeserializeExternalDocumentReference).ToArray() ??
               Array.Empty<SpdxExternalDocumentReference>();
    }

    /// <summary>
    /// Deserialize SPDX External Document Reference
    /// </summary>
    /// <param name="json">Json External Document Reference Node</param>
    /// <returns>SPDX External Document Reference</returns>
    public static SpdxExternalDocumentReference DeserializeExternalDocumentReference(JsonNode? json)
    {
        return new SpdxExternalDocumentReference
        {
            ExternalDocumentId = ParseString(json, "externalDocumentId"),
            Checksum = DeserializeChecksum(json?["checksum"]),
            Document = ParseString(json, "spdxDocument")
        };
    }

    /// <summary>
    /// Deserialize SPDX Extracted Licensing Infos
    /// </summary>
    /// <param name="json">Json Extracted Licensing Info Array</param>
    /// <returns>SPDX Extracted Licensing Infos</returns>
    public static SpdxExtractedLicensingInfo[] DeserializeExtractedLicensingInfos(JsonArray? json)
    {
        return json?.Select(DeserializeExtractedLicensingInfo).ToArray() ?? Array.Empty<SpdxExtractedLicensingInfo>();
    }

    /// <summary>
    /// Deserialize SPDX Extracted Licensing Info
    /// </summary>
    /// <param name="json">Json Extracted Licensing Info Node</param>
    /// <returns>SPDX Extracted Licensing Info</returns>
    public static SpdxExtractedLicensingInfo DeserializeExtractedLicensingInfo(JsonNode? json)
    {
        return new SpdxExtractedLicensingInfo
        {
            LicenseId = ParseString(json, "licenseId"),
            ExtractedText = ParseString(json, "extractedText"),
            Name = ParseOptionalString(json, "name"),
            CrossReferences = ParseStringArray(json, "seeAlsos"),
            Comment = ParseOptionalString(json, "comment")
        };
    }

    /// <summary>
    /// Deserialize SPDX Files
    /// </summary>
    /// <param name="json">Json Files Array</param>
    /// <returns>SPDX Files</returns>
    public static SpdxFile[] DeserializeFiles(JsonArray? json)
    {
        return json?.Select(DeserializeFile).ToArray() ?? Array.Empty<SpdxFile>();
    }

    /// <summary>
    /// Deserialize SPDX File
    /// </summary>
    /// <param name="json">Json File Node</param>
    /// <returns>SPDX File</returns>
    public static SpdxFile DeserializeFile(JsonNode? json)
    {
        return new SpdxFile
        {
            Id = ParseString(json, "SPDXID"),
            FileName = ParseString(json, "fileName"),
            FileTypes = ParseStringArray(json, "fileTypes").Select(SpdxFileTypeExtensions.FromText).ToArray(),
            Checksums = DeserializeChecksums(json?["checksums"]?.AsArray()),
            LicenseConcluded = ParseOptionalString(json, "licenseConcluded"),
            LicenseInfoInFiles = ParseStringArray(json, "licenseInfoInFiles"),
            LicenseComments = ParseOptionalString(json, "licenseComments"),
            Copyright = ParseOptionalString(json, "copyrightText"),
            Comment = ParseOptionalString(json, "comment"),
            Notice = ParseOptionalString(json, "noticeText"),
            Contributors = ParseStringArray(json, "fileContributors"),
            AttributionText = ParseStringArray(json, "attributionTexts"),
            Annotations = DeserializeAnnotations(json?["annotations"]?.AsArray())
        };
    }

    /// <summary>
    /// Deserialize SPDX Packages
    /// </summary>
    /// <param name="json">Json Packages Array</param>
    /// <returns>SPDX Packages</returns>
    public static SpdxPackage[] DeserializePackages(JsonArray? json)
    {
        return json?.Select(DeserializePackage).ToArray() ?? Array.Empty<SpdxPackage>();
    }

    /// <summary>
    /// Deserialize SPDX Package
    /// </summary>
    /// <param name="json">Json Package Node</param>
    /// <returns>SPDX Package</returns>
    public static SpdxPackage DeserializePackage(JsonNode? json)
    {
        return new SpdxPackage
        {
            Id = ParseString(json, "SPDXID"),
            Name = ParseString(json, "name"),
            Version = ParseOptionalString(json, "versionInfo"),
            FileName = ParseOptionalString(json, "packageFileName"),
            Supplier = ParseOptionalString(json, "supplier"),
            Originator = ParseOptionalString(json, "originator"),
            DownloadLocation = ParseString(json, "downloadLocation"),
            FilesAnalyzed = ParseBool(json, "filesAnalyzed"),
            HasFiles = ParseStringArray(json, "hasFiles"),
            VerificationCode = DeserializeVerificationCode(json?["packageVerificationCode"]),
            Checksums = DeserializeChecksums(json?["checksums"]?.AsArray()),
            HomePage = ParseOptionalString(json, "homepage"),
            SourceInformation = ParseOptionalString(json, "sourceInfo"),
            ConcludedLicense = ParseOptionalString(json, "licenseConcluded"),
            LicenseInfoFromFiles = ParseStringArray(json, "licenseInfoFromFiles"),
            DeclaredLicense = ParseString(json, "licenseDeclared"),
            LicenseComments = ParseOptionalString(json, "licenseComments"),
            CopyrightText = ParseOptionalString(json, "copyrightText"),
            Summary = ParseOptionalString(json, "summary"),
            Description = ParseOptionalString(json, "description"),
            Comment = ParseOptionalString(json, "comment"),
            ExternalReferences = DeserializeExternalReferences(json?["externalRefs"]?.AsArray()),
            Attributions = ParseStringArray(json, "attributionTexts"),
            PrimaryPackagePurpose = ParseOptionalString(json, "primaryPackagePurpose"),
            ReleaseDate = ParseOptionalString(json, "releaseDate"),
            BuiltDate = ParseOptionalString(json, "builtDate"),
            ValidUntilDate = ParseOptionalString(json, "validUntilDate"),
            Annotations = DeserializeAnnotations(json?["annotations"]?.AsArray())
        };
    }

    /// <summary>
    /// Deserialize SPDX Snippets
    /// </summary>
    /// <param name="json">Json Snippets Array</param>
    /// <returns>SPDX Snippets</returns>
    public static SpdxSnippet[] DeserializeSnippets(JsonArray? json)
    {
        return json?.Select(DeserializeSnippet).ToArray() ?? Array.Empty<SpdxSnippet>();
    }

    /// <summary>
    /// Deserialize SPDX Snippet
    /// </summary>
    /// <param name="json">Json Snippet Node</param>
    /// <returns>SPDX Snippet</returns>
    public static SpdxSnippet DeserializeSnippet(JsonNode? json)
    {
        return new SpdxSnippet
        {
            Id = ParseString(json, "SPDXID"),
            SnippetFromFile = ParseString(json, "snippetFromFile"),
            SnippetByteStart = Convert.ToInt32(Find(json, "ranges", "startPointer", "offset")?.ToString() ?? ""),
            SnippetByteEnd = Convert.ToInt32(Find(json, "ranges", "endPointer", "offset")?.ToString() ?? ""),
            SnippetLineStart = Convert.ToInt32(Find(json, "ranges", "startPointer", "lineNumber")?.ToString() ?? ""),
            SnippetLineEnd = Convert.ToInt32(Find(json, "ranges", "endPointer", "lineNumber")?.ToString() ?? ""),
            ConcludedLicense = ParseString(json, "licenseConcluded"),
            LicenseInfoInSnippet = ParseStringArray(json, "licenseInfoInSnippets"),
            LicenseComments = ParseOptionalString(json, "licenseComments"),
            Copyright = ParseString(json, "copyrightText"),
            Comment = ParseOptionalString(json, "comment"),
            Name = ParseOptionalString(json, "name"),
            AttributionText = ParseStringArray(json, "attributionTexts"),
            Annotations = DeserializeAnnotations(json?["annotations"]?.AsArray())
        };
    }

    /// <summary>
    /// Deserialize SPDX Relationships
    /// </summary>
    /// <param name="json">Json Relationships Array</param>
    /// <returns>SPDX Relationships</returns>
    public static SpdxRelationship[] DeserializeRelationships(JsonArray? json)
    {
        return json?.Select(DeserializeRelationship).ToArray() ?? Array.Empty<SpdxRelationship>();
    }

    /// <summary>
    /// Deserialize SPDX Relationship
    /// </summary>
    /// <param name="json">Json Relationship Node</param>
    /// <returns>SPDX Relationship</returns>
    public static SpdxRelationship DeserializeRelationship(JsonNode? json)
    {
        return new SpdxRelationship
        {
            Id = ParseString(json, "spdxElementId"),
            RelatedSpdxElement = ParseString(json, "relatedSpdxElement"),
            RelationshipType = SpdxRelationshipTypeExtensions.FromText(ParseString(json, "relationshipType")),
            Comment = ParseOptionalString(json, "comment")
        };
    }

    /// <summary>
    /// Deserialize SPDX Package Verification Code
    /// </summary>
    /// <param name="json">Json Package Verification Code Node</param>
    /// <returns>SPDX Package Verification Code</returns>
    public static SpdxPackageVerificationCode? DeserializeVerificationCode(JsonNode? json)
    {
        return json == null
            ? null
            : new SpdxPackageVerificationCode
            {
                ExcludedFiles = ParseStringArray(json, "packageVerificationCodeExcludedFiles"),
                Value = ParseString(json, "packageVerificationCodeValue")
            };
    }

    /// <summary>
    /// Deserialize SPDX External References
    /// </summary>
    /// <param name="json">Json External References Array</param>
    /// <returns>SPDX External References</returns>
    public static SpdxExternalReference[] DeserializeExternalReferences(JsonArray? json)
    {
        return json?.Select(DeserializeExternalReference).ToArray() ?? Array.Empty<SpdxExternalReference>();
    }

    /// <summary>
    /// Deserialize SPDX External Reference
    /// </summary>
    /// <param name="json">Json External Reference Node</param>
    /// <returns>SPDX External Reference</returns>
    public static SpdxExternalReference DeserializeExternalReference(JsonNode? json)
    {
        return new SpdxExternalReference
        {
            Category = SpdxReferenceCategoryExtensions.FromText(ParseString(json, "referenceCategory")),
            Type = ParseString(json, "referenceType"),
            Locator = ParseString(json, "referenceLocator"),
            Comment = ParseOptionalString(json, "comment")
        };
    }

    /// <summary>
    /// Deserialize SPDX Checksums
    /// </summary>
    /// <param name="json">Json Checksums Array</param>
    /// <returns>SPDX Checksums</returns>
    public static SpdxChecksum[] DeserializeChecksums(JsonArray? json)
    {
        return json?.Select(DeserializeChecksum).ToArray() ?? Array.Empty<SpdxChecksum>();
    }

    /// <summary>
    /// Deserialize SPDX Checksum
    /// </summary>
    /// <param name="json">Json Checksum Node</param>
    /// <returns>SPDX Checksum</returns>
    public static SpdxChecksum DeserializeChecksum(JsonNode? json)
    {
        return new SpdxChecksum
        {
            Algorithm = SpdxChecksumAlgorithmExtensions.FromText(ParseString(json, "algorithm")),
            Value = ParseString(json, "checksumValue")
        };
    }

    /// <summary>
    /// Deserialize SPDX Annotations
    /// </summary>
    /// <param name="json">Json Annotations Array</param>
    /// <returns>SPDX Annotations</returns>
    public static SpdxAnnotation[] DeserializeAnnotations(JsonArray? json)
    {
        return json?.Select(DeserializeAnnotation).ToArray() ?? Array.Empty<SpdxAnnotation>();
    }

    /// <summary>
    /// Deserialize SPDX Annotation
    /// </summary>
    /// <param name="json">Json Annotation Node</param>
    /// <returns>SPDX Annotation</returns>
    public static SpdxAnnotation DeserializeAnnotation(JsonNode? json)
    {
        return new SpdxAnnotation
        {
            Id = ParseString(json, "SPDXID"),
            Annotator = ParseString(json, "annotator"),
            Date = ParseString(json, "annotationDate"),
            Type = SpdxAnnotationTypeExtensions.FromText(ParseString(json, "annotationType")),
            Comment = ParseString(json, "comment")
        };
    }

    /// <summary>
    /// Deserialize JSON String
    /// </summary>
    /// <param name="node">Json Node</param>
    /// <param name="name">String Name</param>
    /// <returns>String Value</returns>
    private static string ParseString(JsonNode? node, string name)
    {
        return node?[name]?.ToString() ?? string.Empty;
    }

    /// <summary>
    /// Deserialize JSON Optional String
    /// </summary>
    /// <param name="node">Json Node</param>
    /// <param name="name">String Name</param>
    /// <returns>String Value or null</returns>
    private static string? ParseOptionalString(JsonNode? node, string name)
    {
        return node?[name]?.ToString();
    }

    /// <summary>
    /// Deserialize Json String Array
    /// </summary>
    /// <param name="node">Json Node</param>
    /// <param name="name">Strings Name</param>
    /// <returns>String Array</returns>
    private static string[] ParseStringArray(JsonNode? node, string name)
    {
        return node?[name]?.AsArray().GetValues<string>().ToArray() ?? Array.Empty<string>();
    }

    /// <summary>
    /// Deserialize Json Boolean
    /// </summary>
    /// <param name="node">Json Node</param>
    /// <param name="name">Bool Name</param>
    /// <returns>Bool value or null</returns>
    private static bool? ParseBool(JsonNode? node, string name)
    {
        return bool.TryParse(ParseString(node, name), out var b) ? b : null;
    }

    /// <summary>
    /// Find a node
    /// </summary>
    /// <param name="node">Starting node</param>
    /// <param name="names">Node search path</param>
    /// <returns>JsonNode or null</returns>
    private static JsonNode? Find(JsonNode? node, params string[] names)
    {
        return Find(node, 0, names);
    }

    /// <summary>
    /// Find a named node
    /// </summary>
    /// <param name="node">Starting node</param>
    /// <param name="idx">Name index</param>
    /// <param name="names">Names list</param>
    /// <returns>JsonNode if found, else null</returns>
    private static JsonNode? Find(JsonNode? node, int idx, IReadOnlyList<string> names)
    {
        // Fail if at end
        if (node == null || idx >= names.Count)
            return node;

        // Iterate over arrays
        if (node is JsonArray array)
            return array.Select(n => Find(n, idx, names)).FirstOrDefault(n => n != null);

        // Descend into the node
        var sel = node[names[idx]];
        return sel == null ? null : Find(sel, idx + 1, names);
    }
}