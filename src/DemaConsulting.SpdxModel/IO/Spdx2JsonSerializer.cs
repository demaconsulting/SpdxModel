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
using System.Text.Json.Serialization.Metadata;

namespace DemaConsulting.SpdxModel.IO;

/// <summary>
/// JSON Serializer class
/// </summary>
public static class Spdx2JsonSerializer
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
            new JsonSerializerOptions
            {
                WriteIndented = true,
                TypeInfoResolver = new DefaultJsonTypeInfoResolver()
            });
    }

    /// <summary>
    /// Serialize SPDX Document
    /// </summary>
    /// <param name="document">SPDX Document</param>
    /// <returns>Json object</returns>
    public static JsonObject SerializeDocument(SpdxDocument document)
    {
        var json = new JsonObject();
        EmitString(json, SpdxConstants.FieldSpdxId, document.Id);
        EmitString(json, SpdxConstants.FieldSpdxVersion, document.Version);
        EmitString(json, SpdxConstants.FieldName, document.Name);
        EmitString(json, SpdxConstants.FieldDataLicense, document.DataLicense);
        EmitString(json, SpdxConstants.FieldDocumentNamespace, document.DocumentNamespace);
        EmitOptionalString(json, SpdxConstants.FieldComment, document.Comment);
        json[SpdxConstants.FieldCreationInfo] = SerializeCreationInformation(document.CreationInformation);
        if (document.ExternalDocumentReferences.Length > 0)
            json[SpdxConstants.FieldExternalDocumentRefs] = SerializeExternalDocumentReferences(document.ExternalDocumentReferences);
        if (document.ExtractedLicensingInfo.Length > 0)
            json[SpdxConstants.FieldHasExtractedLicensingInfos] = SerializeExtractedLicensingInfos(document.ExtractedLicensingInfo);
        if (document.Annotations.Length > 0)
            json[SpdxConstants.FieldAnnotations] = SerializeAnnotations(document.Annotations);
        json[SpdxConstants.FieldFiles] = SerializeFiles(document.Files);
        json[SpdxConstants.FieldPackages] = SerializePackages(document.Packages);
        json[SpdxConstants.FieldSnippets] = SerializeSnippets(document.Snippets);
        json[SpdxConstants.FieldRelationships] = SerializeRelationships(document.Relationships);
        EmitOptionalStrings(json, SpdxConstants.FieldDocumentDescribes, document.Describes);
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
        EmitOptionalStrings(json, SpdxConstants.FieldCreators, info.Creators);
        EmitString(json, SpdxConstants.FieldCreated, info.Created);
        EmitOptionalString(json, SpdxConstants.FieldComment, info.Comment);
        EmitOptionalString(json, SpdxConstants.FieldLicenseListVersion, info.LicenseListVersion);
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
        EmitString(json, SpdxConstants.FieldExternalDocumentId, reference.ExternalDocumentId);
        json[SpdxConstants.FieldChecksum] = SerializeChecksum(reference.Checksum);
        EmitString(json, SpdxConstants.FieldSpdxDocument, reference.Document);
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
        EmitString(json, SpdxConstants.FieldLicenseId, info.LicenseId);
        EmitString(json, SpdxConstants.FieldExtractedText, info.ExtractedText);
        EmitOptionalString(json, SpdxConstants.FieldName, info.Name);
        EmitOptionalStrings(json, SpdxConstants.FieldSeeAlsos, info.CrossReferences);
        EmitOptionalString(json, SpdxConstants.FieldComment, info.Comment);
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
        EmitString(json, SpdxConstants.FieldSpdxId, file.Id);
        EmitString(json, SpdxConstants.FieldFileName, file.FileName);
        EmitOptionalStrings(json, SpdxConstants.FieldFileTypes, [..file.FileTypes.Select(SpdxFileTypeExtensions.ToText)]);
        json[SpdxConstants.FieldChecksums] = SerializeChecksums(file.Checksums);
        EmitOptionalString(json, SpdxConstants.FieldLicenseConcluded, file.ConcludedLicense);
        EmitOptionalStrings(json, SpdxConstants.FieldLicenseInfoInFiles, file.LicenseInfoInFiles);
        EmitOptionalString(json, SpdxConstants.FieldLicenseComments, file.LicenseComments);
        EmitOptionalString(json, SpdxConstants.FieldCopyrightText, file.CopyrightText);
        EmitOptionalString(json, SpdxConstants.FieldComment, file.Comment);
        EmitOptionalString(json, SpdxConstants.FieldNoticeText, file.Notice);
        EmitOptionalStrings(json, SpdxConstants.FieldFileContributors, file.Contributors);
        EmitOptionalStrings(json, SpdxConstants.FieldAttributionTexts, file.AttributionText);
        if (file.Annotations.Length > 0)
            json[SpdxConstants.FieldAnnotations] = SerializeAnnotations(file.Annotations);
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
        EmitString(json, SpdxConstants.FieldSpdxId, package.Id);
        EmitString(json, SpdxConstants.FieldName, package.Name);
        EmitOptionalString(json, SpdxConstants.FieldVersionInfo, package.Version);
        EmitOptionalString(json, SpdxConstants.FieldPackageFileName, package.FileName);
        EmitOptionalString(json, SpdxConstants.FieldSupplier, package.Supplier);
        EmitOptionalString(json, SpdxConstants.FieldOriginator, package.Originator);
        EmitOptionalString(json, SpdxConstants.FieldDownloadLocation, package.DownloadLocation);
        if (package.FilesAnalyzed != null)
            json[SpdxConstants.FieldFilesAnalyzed] = package.FilesAnalyzed;
        EmitOptionalStrings(json, SpdxConstants.FieldHasFiles, package.HasFiles);
        if (package.VerificationCode != null)
            json[SpdxConstants.FieldPackageVerificationCode] = SerializeVerificationCode(package.VerificationCode);
        json[SpdxConstants.FieldChecksums] = SerializeChecksums(package.Checksums);
        EmitOptionalString(json, SpdxConstants.FieldHomePage, package.HomePage);
        EmitOptionalString(json, SpdxConstants.FieldSourceInfo, package.SourceInformation);
        EmitOptionalString(json, SpdxConstants.FieldLicenseConcluded, package.ConcludedLicense);
        EmitOptionalStrings(json, SpdxConstants.FieldLicenseInfoFromFiles, package.LicenseInfoFromFiles);
        EmitString(json, SpdxConstants.FieldLicenseDeclared, package.DeclaredLicense);
        EmitOptionalString(json, SpdxConstants.FieldLicenseComments, package.LicenseComments);
        EmitOptionalString(json, SpdxConstants.FieldCopyrightText, package.CopyrightText);
        EmitOptionalString(json, SpdxConstants.FieldSummary, package.Summary);
        EmitOptionalString(json, SpdxConstants.FieldDescription, package.Description);
        EmitOptionalString(json, SpdxConstants.FieldComment, package.Comment);
        if (package.ExternalReferences.Length > 0)
            json[SpdxConstants.FieldExternalRefs] = SerializeExternalReferences(package.ExternalReferences);
        EmitOptionalStrings(json, SpdxConstants.FieldAttributionTexts, package.AttributionText);
        EmitOptionalString(json, SpdxConstants.FieldPrimaryPackagePurpose, package.PrimaryPackagePurpose);
        EmitOptionalString(json, SpdxConstants.FieldReleaseDate, package.ReleaseDate);
        EmitOptionalString(json, SpdxConstants.FieldBuiltDate, package.BuiltDate);
        EmitOptionalString(json, SpdxConstants.FieldValidUntilDate, package.ValidUntilDate);
        if (package.Annotations.Length > 0)
            json[SpdxConstants.FieldAnnotations] = SerializeAnnotations(package.Annotations);

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
        EmitString(json, SpdxConstants.FieldSpdxId, snippet.Id);
        EmitString(json, SpdxConstants.FieldSnippetFromFile, snippet.SnippetFromFile);
        EmitOptionalString(json, SpdxConstants.FieldName, snippet.Name);
        EmitOptionalString(json, SpdxConstants.FieldLicenseConcluded, snippet.ConcludedLicense);
        EmitOptionalStrings(json, SpdxConstants.FieldLicenseInfoInSnippets, snippet.LicenseInfoInSnippet);
        EmitOptionalString(json, SpdxConstants.FieldLicenseComments, snippet.LicenseComments);
        EmitOptionalString(json, SpdxConstants.FieldCopyrightText, snippet.CopyrightText);
        EmitOptionalString(json, SpdxConstants.FieldComment, snippet.Comment);
        EmitOptionalStrings(json, SpdxConstants.FieldAttributionTexts, snippet.AttributionText);
        if (snippet.Annotations.Length > 0)
            json[SpdxConstants.FieldAnnotations] = SerializeAnnotations(snippet.Annotations);

        // Add ranges
        var ranges = new JsonArray
        {
            new JsonObject
            {
                [SpdxConstants.FieldEndPointer] = new JsonObject
                {
                    [SpdxConstants.FieldReference] = snippet.SnippetFromFile,
                    [SpdxConstants.FieldOffset] = snippet.SnippetByteEnd
                },
                [SpdxConstants.FieldStartPointer] = new JsonObject
                {
                    [SpdxConstants.FieldReference] = snippet.SnippetFromFile,
                    [SpdxConstants.FieldOffset] = snippet.SnippetByteStart
                }
            }
        };
        if (snippet.SnippetLineEnd > 0 || snippet.SnippetLineStart > 0)
            ranges.Add(new JsonObject
            {
                [SpdxConstants.FieldEndPointer] = new JsonObject
                {
                    [SpdxConstants.FieldReference] = snippet.SnippetFromFile,
                    [SpdxConstants.FieldLineNumber] = snippet.SnippetLineEnd
                },
                [SpdxConstants.FieldStartPointer] = new JsonObject
                {
                    [SpdxConstants.FieldReference] = snippet.SnippetFromFile,
                    [SpdxConstants.FieldLineNumber] = snippet.SnippetLineStart
                }
            });
        json[SpdxConstants.FieldRanges] = ranges;
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
        EmitString(json, SpdxConstants.FieldSpdxElementId, relationship.Id);
        EmitString(json, SpdxConstants.FieldRelatedSpdxElement, relationship.RelatedSpdxElement);
        EmitString(json, SpdxConstants.FieldRelationshipType, relationship.RelationshipType.ToText());
        EmitOptionalString(json, SpdxConstants.FieldComment, relationship.Comment);
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
        EmitString(json, SpdxConstants.FieldPackageVerificationCodeValue, code.Value);
        EmitOptionalStrings(json, SpdxConstants.FieldPackageVerificationCodeExcludedFiles, code.ExcludedFiles);
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
        EmitString(json, SpdxConstants.FieldReferenceCategory, reference.Category.ToText());
        EmitString(json, SpdxConstants.FieldReferenceType, reference.Type);
        EmitString(json, SpdxConstants.FieldReferenceLocator, reference.Locator);
        EmitOptionalString(json, SpdxConstants.FieldComment, reference.Comment);
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
        EmitString(json, SpdxConstants.FieldAlgorithm, checksum.Algorithm.ToText());
        EmitString(json, SpdxConstants.FieldChecksumValue, checksum.Value);
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
        EmitOptionalString(json, SpdxConstants.FieldSpdxId, annotation.Id);
        EmitString(json, SpdxConstants.FieldAnnotator, annotation.Annotator);
        EmitString(json, SpdxConstants.FieldAnnotationDate, annotation.Date);
        EmitString(json, SpdxConstants.FieldAnnotationType, annotation.Type.ToText());
        EmitString(json, SpdxConstants.FieldComment, annotation.Comment);
        return json;
    }

    /// <summary>
    /// Emit a string property into a JSON object
    /// </summary>
    /// <param name="json">JSON object</param>
    /// <param name="name">Property name</param>
    /// <param name="value">Property value</param>
    private static void EmitString(JsonNode json, string name, string value)
    {
        json[name] = value;
    }

    /// <summary>
    /// Emit an optional string property into a JSON object
    /// </summary>
    /// <param name="json">JSON object</param>
    /// <param name="name">Property name</param>
    /// <param name="value">Optional property value</param>
    private static void EmitOptionalString(JsonNode json, string name, string? value)
    {
        // Skip if empty
        if (string.IsNullOrEmpty(value))
            return;

        json[name] = value;
    }

    private static void EmitOptionalStrings(JsonNode json, string name, string[] values)
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