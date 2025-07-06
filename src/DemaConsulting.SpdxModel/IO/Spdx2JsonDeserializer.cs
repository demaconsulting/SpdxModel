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

using System.Text.Json;
using System.Text.Json.Nodes;

namespace DemaConsulting.SpdxModel.IO;

/// <summary>
///     JSON Deserializer class
/// </summary>
public static class Spdx2JsonDeserializer
{
    /// <summary>
    ///     Deserialize SPDX Document
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
    ///     Deserialize the SPDX Document
    /// </summary>
    /// <param name="json">Json Document Node</param>
    /// <returns>SPDX Document</returns>
    public static SpdxDocument DeserializeDocument(JsonNode json)
    {
        return new SpdxDocument
        {
            Id = ParseString(json, SpdxConstants.FieldSpdxId),
            Version = ParseString(json, SpdxConstants.FieldSpdxVersion),
            Name = ParseString(json, SpdxConstants.FieldName),
            DataLicense = ParseString(json, SpdxConstants.FieldDataLicense),
            DocumentNamespace = ParseString(json, SpdxConstants.FieldDocumentNamespace),
            Comment = ParseOptionalString(json, SpdxConstants.FieldComment),
            CreationInformation = DeserializeCreationInformation(json[SpdxConstants.FieldCreationInfo]),
            ExternalDocumentReferences =
                DeserializeExternalDocumentReferences(json[SpdxConstants.FieldExternalDocumentRefs]?.AsArray()),
            ExtractedLicensingInfo =
                DeserializeExtractedLicensingInfos(json[SpdxConstants.FieldHasExtractedLicensingInfos]?.AsArray()),
            Annotations = DeserializeAnnotations(json[SpdxConstants.FieldAnnotations]?.AsArray()),
            Files = DeserializeFiles(json[SpdxConstants.FieldFiles]?.AsArray()),
            Packages = DeserializePackages(json[SpdxConstants.FieldPackages]?.AsArray()),
            Snippets = DeserializeSnippets(json[SpdxConstants.FieldSnippets]?.AsArray()),
            Relationships = DeserializeRelationships(json[SpdxConstants.FieldRelationships]?.AsArray()),
            Describes = ParseStringArray(json, SpdxConstants.FieldDocumentDescribes)
        };
    }

    /// <summary>
    ///     Deserialize SPDX Creation Information
    /// </summary>
    /// <param name="json">Json Creation Information Node</param>
    /// <returns>SPDX Document</returns>
    public static SpdxCreationInformation DeserializeCreationInformation(JsonNode? json)
    {
        return new SpdxCreationInformation
        {
            Creators = ParseStringArray(json, SpdxConstants.FieldCreators),
            Created = ParseString(json, SpdxConstants.FieldCreated),
            Comment = ParseOptionalString(json, SpdxConstants.FieldComment),
            LicenseListVersion = ParseOptionalString(json, SpdxConstants.FieldLicenseListVersion)
        };
    }

    /// <summary>
    ///     Deserialize SPDX External Document References
    /// </summary>
    /// <param name="json">Json External Document References Array</param>
    /// <returns>SPDX External Document References</returns>
    public static SpdxExternalDocumentReference[] DeserializeExternalDocumentReferences(JsonArray? json)
    {
        return json?.Select(DeserializeExternalDocumentReference).ToArray() ??
               [];
    }

    /// <summary>
    ///     Deserialize SPDX External Document Reference
    /// </summary>
    /// <param name="json">Json External Document Reference Node</param>
    /// <returns>SPDX External Document Reference</returns>
    public static SpdxExternalDocumentReference DeserializeExternalDocumentReference(JsonNode? json)
    {
        return new SpdxExternalDocumentReference
        {
            ExternalDocumentId = ParseString(json, SpdxConstants.FieldExternalDocumentId),
            Checksum = DeserializeChecksum(json?[SpdxConstants.FieldChecksum]),
            Document = ParseString(json, SpdxConstants.FieldSpdxDocument)
        };
    }

    /// <summary>
    ///     Deserialize SPDX Extracted Licensing Infos
    /// </summary>
    /// <param name="json">Json Extracted Licensing Info Array</param>
    /// <returns>SPDX Extracted Licensing Infos</returns>
    public static SpdxExtractedLicensingInfo[] DeserializeExtractedLicensingInfos(JsonArray? json)
    {
        return json?.Select(DeserializeExtractedLicensingInfo).ToArray() ?? [];
    }

    /// <summary>
    ///     Deserialize SPDX Extracted Licensing Info
    /// </summary>
    /// <param name="json">Json Extracted Licensing Info Node</param>
    /// <returns>SPDX Extracted Licensing Info</returns>
    public static SpdxExtractedLicensingInfo DeserializeExtractedLicensingInfo(JsonNode? json)
    {
        return new SpdxExtractedLicensingInfo
        {
            LicenseId = ParseString(json, SpdxConstants.FieldLicenseId),
            ExtractedText = ParseString(json, SpdxConstants.FieldExtractedText),
            Name = ParseOptionalString(json, SpdxConstants.FieldName),
            CrossReferences = ParseStringArray(json, SpdxConstants.FieldSeeAlsos),
            Comment = ParseOptionalString(json, SpdxConstants.FieldComment)
        };
    }

    /// <summary>
    ///     Deserialize SPDX Files
    /// </summary>
    /// <param name="json">Json Files Array</param>
    /// <returns>SPDX Files</returns>
    public static SpdxFile[] DeserializeFiles(JsonArray? json)
    {
        return json?.Select(DeserializeFile).ToArray() ?? [];
    }

    /// <summary>
    ///     Deserialize SPDX File
    /// </summary>
    /// <param name="json">Json File Node</param>
    /// <returns>SPDX File</returns>
    public static SpdxFile DeserializeFile(JsonNode? json)
    {
        return new SpdxFile
        {
            Id = ParseString(json, SpdxConstants.FieldSpdxId),
            FileName = ParseString(json, SpdxConstants.FieldFileName),
            FileTypes =
                [..ParseStringArray(json, SpdxConstants.FieldFileTypes).Select(SpdxFileTypeExtensions.FromText)],
            Checksums = DeserializeChecksums(json?[SpdxConstants.FieldChecksums]?.AsArray()),
            ConcludedLicense = ParseString(json, SpdxConstants.FieldLicenseConcluded),
            LicenseInfoInFiles = ParseStringArray(json, SpdxConstants.FieldLicenseInfoInFiles),
            LicenseComments = ParseOptionalString(json, SpdxConstants.FieldLicenseComments),
            CopyrightText = ParseString(json, SpdxConstants.FieldCopyrightText),
            Comment = ParseOptionalString(json, SpdxConstants.FieldComment),
            Notice = ParseOptionalString(json, SpdxConstants.FieldNoticeText),
            Contributors = ParseStringArray(json, SpdxConstants.FieldFileContributors),
            AttributionText = ParseStringArray(json, SpdxConstants.FieldAttributionTexts),
            Annotations = DeserializeAnnotations(json?[SpdxConstants.FieldAnnotations]?.AsArray())
        };
    }

    /// <summary>
    ///     Deserialize SPDX Packages
    /// </summary>
    /// <param name="json">Json Packages Array</param>
    /// <returns>SPDX Packages</returns>
    public static SpdxPackage[] DeserializePackages(JsonArray? json)
    {
        return json?.Select(DeserializePackage).ToArray() ?? [];
    }

    /// <summary>
    ///     Deserialize SPDX Package
    /// </summary>
    /// <param name="json">Json Package Node</param>
    /// <returns>SPDX Package</returns>
    public static SpdxPackage DeserializePackage(JsonNode? json)
    {
        return new SpdxPackage
        {
            Id = ParseString(json, SpdxConstants.FieldSpdxId),
            Name = ParseString(json, SpdxConstants.FieldName),
            Version = ParseOptionalString(json, SpdxConstants.FieldVersionInfo),
            FileName = ParseOptionalString(json, SpdxConstants.FieldPackageFileName),
            Supplier = ParseOptionalString(json, SpdxConstants.FieldSupplier),
            Originator = ParseOptionalString(json, SpdxConstants.FieldOriginator),
            DownloadLocation = ParseString(json, SpdxConstants.FieldDownloadLocation),
            FilesAnalyzed = ParseBool(json, SpdxConstants.FieldFilesAnalyzed),
            HasFiles = ParseStringArray(json, SpdxConstants.FieldHasFiles),
            VerificationCode = DeserializeVerificationCode(json?[SpdxConstants.FieldPackageVerificationCode]),
            Checksums = DeserializeChecksums(json?[SpdxConstants.FieldChecksums]?.AsArray()),
            HomePage = ParseOptionalString(json, SpdxConstants.FieldHomePage),
            SourceInformation = ParseOptionalString(json, SpdxConstants.FieldSourceInfo),
            ConcludedLicense = ParseString(json, SpdxConstants.FieldLicenseConcluded),
            LicenseInfoFromFiles = ParseStringArray(json, SpdxConstants.FieldLicenseInfoFromFiles),
            DeclaredLicense = ParseString(json, SpdxConstants.FieldLicenseDeclared),
            LicenseComments = ParseOptionalString(json, SpdxConstants.FieldLicenseComments),
            CopyrightText = ParseString(json, SpdxConstants.FieldCopyrightText),
            Summary = ParseOptionalString(json, SpdxConstants.FieldSummary),
            Description = ParseOptionalString(json, SpdxConstants.FieldDescription),
            Comment = ParseOptionalString(json, SpdxConstants.FieldComment),
            ExternalReferences = DeserializeExternalReferences(json?[SpdxConstants.FieldExternalRefs]?.AsArray()),
            AttributionText = ParseStringArray(json, SpdxConstants.FieldAttributionTexts),
            PrimaryPackagePurpose = ParseOptionalString(json, SpdxConstants.FieldPrimaryPackagePurpose),
            ReleaseDate = ParseOptionalString(json, SpdxConstants.FieldReleaseDate),
            BuiltDate = ParseOptionalString(json, SpdxConstants.FieldBuiltDate),
            ValidUntilDate = ParseOptionalString(json, SpdxConstants.FieldValidUntilDate),
            Annotations = DeserializeAnnotations(json?[SpdxConstants.FieldAnnotations]?.AsArray())
        };
    }

    /// <summary>
    ///     Deserialize SPDX Snippets
    /// </summary>
    /// <param name="json">Json Snippets Array</param>
    /// <returns>SPDX Snippets</returns>
    public static SpdxSnippet[] DeserializeSnippets(JsonArray? json)
    {
        return json?.Select(DeserializeSnippet).ToArray() ?? [];
    }

    /// <summary>
    ///     Deserialize SPDX Snippet
    /// </summary>
    /// <param name="json">Json Snippet Node</param>
    /// <returns>SPDX Snippet</returns>
    public static SpdxSnippet DeserializeSnippet(JsonNode? json)
    {
        return new SpdxSnippet
        {
            Id = ParseString(json, SpdxConstants.FieldSpdxId),
            SnippetFromFile = ParseString(json, SpdxConstants.FieldSnippetFromFile),
            SnippetByteStart = Convert.ToInt32(Find(json, SpdxConstants.FieldRanges, SpdxConstants.FieldStartPointer,
                SpdxConstants.FieldOffset)?.ToString() ?? ""),
            SnippetByteEnd = Convert.ToInt32(Find(json, SpdxConstants.FieldRanges, SpdxConstants.FieldEndPointer,
                SpdxConstants.FieldOffset)?.ToString() ?? ""),
            SnippetLineStart = Convert.ToInt32(Find(json, SpdxConstants.FieldRanges, SpdxConstants.FieldStartPointer,
                SpdxConstants.FieldLineNumber)?.ToString() ?? ""),
            SnippetLineEnd = Convert.ToInt32(Find(json, SpdxConstants.FieldRanges, SpdxConstants.FieldEndPointer,
                SpdxConstants.FieldLineNumber)?.ToString() ?? ""),
            ConcludedLicense = ParseString(json, SpdxConstants.FieldLicenseConcluded),
            LicenseInfoInSnippet = ParseStringArray(json, SpdxConstants.FieldLicenseInfoInSnippets),
            LicenseComments = ParseOptionalString(json, SpdxConstants.FieldLicenseComments),
            CopyrightText = ParseString(json, SpdxConstants.FieldCopyrightText),
            Comment = ParseOptionalString(json, SpdxConstants.FieldComment),
            Name = ParseOptionalString(json, SpdxConstants.FieldName),
            AttributionText = ParseStringArray(json, SpdxConstants.FieldAttributionTexts),
            Annotations = DeserializeAnnotations(json?[SpdxConstants.FieldAnnotations]?.AsArray())
        };
    }

    /// <summary>
    ///     Deserialize SPDX Relationships
    /// </summary>
    /// <param name="json">Json Relationships Array</param>
    /// <returns>SPDX Relationships</returns>
    public static SpdxRelationship[] DeserializeRelationships(JsonArray? json)
    {
        return json?.Select(DeserializeRelationship).ToArray() ?? [];
    }

    /// <summary>
    ///     Deserialize SPDX Relationship
    /// </summary>
    /// <param name="json">Json Relationship Node</param>
    /// <returns>SPDX Relationship</returns>
    public static SpdxRelationship DeserializeRelationship(JsonNode? json)
    {
        return new SpdxRelationship
        {
            Id = ParseString(json, SpdxConstants.FieldSpdxElementId),
            RelatedSpdxElement = ParseString(json, SpdxConstants.FieldRelatedSpdxElement),
            RelationshipType =
                SpdxRelationshipTypeExtensions.FromText(ParseString(json, SpdxConstants.FieldRelationshipType)),
            Comment = ParseOptionalString(json, SpdxConstants.FieldComment)
        };
    }

    /// <summary>
    ///     Deserialize SPDX Package Verification Code
    /// </summary>
    /// <param name="json">Json Package Verification Code Node</param>
    /// <returns>SPDX Package Verification Code</returns>
    public static SpdxPackageVerificationCode? DeserializeVerificationCode(JsonNode? json)
    {
        return json == null
            ? null
            : new SpdxPackageVerificationCode
            {
                ExcludedFiles = ParseStringArray(json, SpdxConstants.FieldPackageVerificationCodeExcludedFiles),
                Value = ParseString(json, SpdxConstants.FieldPackageVerificationCodeValue)
            };
    }

    /// <summary>
    ///     Deserialize SPDX External References
    /// </summary>
    /// <param name="json">Json External References Array</param>
    /// <returns>SPDX External References</returns>
    public static SpdxExternalReference[] DeserializeExternalReferences(JsonArray? json)
    {
        return json?.Select(DeserializeExternalReference).ToArray() ?? [];
    }

    /// <summary>
    ///     Deserialize SPDX External Reference
    /// </summary>
    /// <param name="json">Json External Reference Node</param>
    /// <returns>SPDX External Reference</returns>
    public static SpdxExternalReference DeserializeExternalReference(JsonNode? json)
    {
        return new SpdxExternalReference
        {
            Category =
                SpdxReferenceCategoryExtensions.FromText(ParseString(json, SpdxConstants.FieldReferenceCategory)),
            Type = ParseString(json, SpdxConstants.FieldReferenceType),
            Locator = ParseString(json, SpdxConstants.FieldReferenceLocator),
            Comment = ParseOptionalString(json, SpdxConstants.FieldComment)
        };
    }

    /// <summary>
    ///     Deserialize SPDX Checksums
    /// </summary>
    /// <param name="json">Json Checksums Array</param>
    /// <returns>SPDX Checksums</returns>
    public static SpdxChecksum[] DeserializeChecksums(JsonArray? json)
    {
        return json?.Select(DeserializeChecksum).ToArray() ?? [];
    }

    /// <summary>
    ///     Deserialize SPDX Checksum
    /// </summary>
    /// <param name="json">Json Checksum Node</param>
    /// <returns>SPDX Checksum</returns>
    public static SpdxChecksum DeserializeChecksum(JsonNode? json)
    {
        return new SpdxChecksum
        {
            Algorithm = SpdxChecksumAlgorithmExtensions.FromText(ParseString(json, SpdxConstants.FieldAlgorithm)),
            Value = ParseString(json, SpdxConstants.FieldChecksumValue)
        };
    }

    /// <summary>
    ///     Deserialize SPDX Annotations
    /// </summary>
    /// <param name="json">Json Annotations Array</param>
    /// <returns>SPDX Annotations</returns>
    public static SpdxAnnotation[] DeserializeAnnotations(JsonArray? json)
    {
        return json?.Select(DeserializeAnnotation).ToArray() ?? [];
    }

    /// <summary>
    ///     Deserialize SPDX Annotation
    /// </summary>
    /// <param name="json">Json Annotation Node</param>
    /// <returns>SPDX Annotation</returns>
    public static SpdxAnnotation DeserializeAnnotation(JsonNode? json)
    {
        return new SpdxAnnotation
        {
            Id = ParseString(json, SpdxConstants.FieldSpdxId),
            Annotator = ParseString(json, SpdxConstants.FieldAnnotator),
            Date = ParseString(json, SpdxConstants.FieldAnnotationDate),
            Type = SpdxAnnotationTypeExtensions.FromText(ParseString(json, SpdxConstants.FieldAnnotationType)),
            Comment = ParseString(json, SpdxConstants.FieldComment)
        };
    }

    /// <summary>
    ///     Deserialize JSON String
    /// </summary>
    /// <param name="node">Json Node</param>
    /// <param name="name">String Name</param>
    /// <returns>String Value</returns>
    private static string ParseString(JsonNode? node, string name)
    {
        return node?[name]?.ToString() ?? string.Empty;
    }

    /// <summary>
    ///     Deserialize JSON Optional String
    /// </summary>
    /// <param name="node">Json Node</param>
    /// <param name="name">String Name</param>
    /// <returns>String Value or null</returns>
    private static string? ParseOptionalString(JsonNode? node, string name)
    {
        return node?[name]?.ToString();
    }

    /// <summary>
    ///     Deserialize Json String Array
    /// </summary>
    /// <param name="node">Json Node</param>
    /// <param name="name">Strings Name</param>
    /// <returns>String Array</returns>
    private static string[] ParseStringArray(JsonNode? node, string name)
    {
        return node?[name]?.AsArray().GetValues<string>().ToArray() ?? [];
    }

    /// <summary>
    ///     Deserialize Json Boolean
    /// </summary>
    /// <param name="node">Json Node</param>
    /// <param name="name">Bool Name</param>
    /// <returns>Bool value or null</returns>
    private static bool? ParseBool(JsonNode? node, string name)
    {
        return bool.TryParse(ParseString(node, name), out var b) ? b : null;
    }

    /// <summary>
    ///     Find a node
    /// </summary>
    /// <param name="node">Starting node</param>
    /// <param name="names">Node search path</param>
    /// <returns>JsonNode or null</returns>
    private static JsonNode? Find(JsonNode? node, params string[] names)
    {
        return Find(node, 0, names);
    }

    /// <summary>
    ///     Find a named node
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