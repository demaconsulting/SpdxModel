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

namespace DemaConsulting.SpdxModel.IO;

/// <summary>
///     SPDX 2.x constants.
/// </summary>
/// <remarks>
///     All SPDX 2.x JSON field name strings are centralised here to provide a single
///     maintenance point. Both <see cref="Spdx2JsonDeserializer"/> and
///     <see cref="Spdx2JsonSerializer"/> consume these constants, ensuring that any
///     SPDX schema field name change requires an update in exactly one place.
/// </remarks>
internal static class SpdxConstants
{
    /// <summary>
    ///     Constant for SPDX ID field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the SPDX element identifier.</remarks>
    internal const string FieldSpdxId = "SPDXID";

    /// <summary>
    ///     Constant for SPDX Version field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the SPDX specification version.</remarks>
    internal const string FieldSpdxVersion = "spdxVersion";

    /// <summary>
    ///     Constant for SPDX Name field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the document or element name.</remarks>
    internal const string FieldName = "name";

    /// <summary>
    ///     Constant for SPDX Data License field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the data license of the SPDX document.</remarks>
    internal const string FieldDataLicense = "dataLicense";

    /// <summary>
    ///     Constant for SPDX Document Namespace field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the document namespace URI.</remarks>
    internal const string FieldDocumentNamespace = "documentNamespace";

    /// <summary>
    ///     Constant for SPDX Comment field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for an optional free-text comment.</remarks>
    internal const string FieldComment = "comment";

    /// <summary>
    ///     Constant for SPDX Creation Info field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the document creation information object.</remarks>
    internal const string FieldCreationInfo = "creationInfo";

    /// <summary>
    ///     Constant for SPDX External Document Refs field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the array of external document references.</remarks>
    internal const string FieldExternalDocumentRefs = "externalDocumentRefs";

    /// <summary>
    ///     Constant for SPDX Has Extracted Licensing Infos field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the array of extracted licensing info entries.</remarks>
    internal const string FieldHasExtractedLicensingInfos = "hasExtractedLicensingInfos";

    /// <summary>
    ///     Constant for SPDX Annotations field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the annotations array on a document or element.</remarks>
    internal const string FieldAnnotations = "annotations";

    /// <summary>
    ///     Constant for SPDX Files field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the top-level files array.</remarks>
    internal const string FieldFiles = "files";

    /// <summary>
    ///     Constant for SPDX Packages field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the top-level packages array.</remarks>
    internal const string FieldPackages = "packages";

    /// <summary>
    ///     Constant for SPDX Snippets field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the top-level snippets array.</remarks>
    internal const string FieldSnippets = "snippets";

    /// <summary>
    ///     Constant for SPDX Relationships field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the top-level relationships array.</remarks>
    internal const string FieldRelationships = "relationships";

    /// <summary>
    ///     Constant for SPDX Document Describes field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the array of element IDs described by the document.</remarks>
    internal const string FieldDocumentDescribes = "documentDescribes";

    /// <summary>
    ///     Constant for SPDX Creators field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the creators array in creation information.</remarks>
    internal const string FieldCreators = "creators";

    /// <summary>
    ///     Constant for SPDX Created field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the creation date-time in creation information.</remarks>
    internal const string FieldCreated = "created";

    /// <summary>
    ///     Constant for SPDX License List Version field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the SPDX license list version.</remarks>
    internal const string FieldLicenseListVersion = "licenseListVersion";

    /// <summary>
    ///     Constant for SPDX External Document Id field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the external document identifier.</remarks>
    internal const string FieldExternalDocumentId = "externalDocumentId";

    /// <summary>
    ///     Constant for SPDX Checksum field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for a single checksum object (used in external document references).</remarks>
    internal const string FieldChecksum = "checksum";

    /// <summary>
    ///     Constant for SPDX Checksums field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the checksums array on a package or file.</remarks>
    internal const string FieldChecksums = "checksums";

    /// <summary>
    ///     Constant for SPDX Document field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the SPDX document URI in an external document reference.</remarks>
    internal const string FieldSpdxDocument = "spdxDocument";

    /// <summary>
    ///     Constant for SPDX License ID field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the license identifier in extracted licensing info.</remarks>
    internal const string FieldLicenseId = "licenseId";

    /// <summary>
    ///     Constant for SPDX Extracted Text field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the extracted license text.</remarks>
    internal const string FieldExtractedText = "extractedText";

    /// <summary>
    ///     Constant for SPDX See Alsos field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the cross-reference URLs in extracted licensing info.</remarks>
    internal const string FieldSeeAlsos = "seeAlsos";

    /// <summary>
    ///     Constant for SPDX File Name field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the file path or name.</remarks>
    internal const string FieldFileName = "fileName";

    /// <summary>
    ///     Constant for SPDX File Types field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the file type classification array.</remarks>
    internal const string FieldFileTypes = "fileTypes";

    /// <summary>
    ///     Constant for SPDX License Concluded field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the concluded license expression.</remarks>
    internal const string FieldLicenseConcluded = "licenseConcluded";

    /// <summary>
    ///     Constant for SPDX License Info In Files field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the license information found in a file.</remarks>
    internal const string FieldLicenseInfoInFiles = "licenseInfoInFiles";

    /// <summary>
    ///     Constant for SPDX License Comments field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for license-related comments.</remarks>
    internal const string FieldLicenseComments = "licenseComments";

    /// <summary>
    ///     Constant for SPDX Copyright Text field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the copyright text.</remarks>
    internal const string FieldCopyrightText = "copyrightText";

    /// <summary>
    ///     Constant for SPDX Notice Text field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the notice text on a file.</remarks>
    internal const string FieldNoticeText = "noticeText";

    /// <summary>
    ///     Constant for SPDX File Contributors field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the file contributors array.</remarks>
    internal const string FieldFileContributors = "fileContributors";

    /// <summary>
    ///     Constant for SPDX Attribution Texts field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the attribution texts array.</remarks>
    internal const string FieldAttributionTexts = "attributionTexts";

    /// <summary>
    ///     Constant for SPDX Version Info field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the package version string.</remarks>
    internal const string FieldVersionInfo = "versionInfo";

    /// <summary>
    ///     Constant for SPDX Package File Name field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the package file name.</remarks>
    internal const string FieldPackageFileName = "packageFileName";

    /// <summary>
    ///     Constant for SPDX Supplier field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the package supplier.</remarks>
    internal const string FieldSupplier = "supplier";

    /// <summary>
    ///     Constant for SPDX Originator field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the package originator.</remarks>
    internal const string FieldOriginator = "originator";

    /// <summary>
    ///     Constant for SPDX Download Location field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the package download location URI.</remarks>
    internal const string FieldDownloadLocation = "downloadLocation";

    /// <summary>
    ///     Constant for SPDX Files Analyzed field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the files-analyzed flag on a package.</remarks>
    internal const string FieldFilesAnalyzed = "filesAnalyzed";

    /// <summary>
    ///     Constant for SPDX Has Files field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the array of file IDs belonging to a package.</remarks>
    internal const string FieldHasFiles = "hasFiles";

    /// <summary>
    ///     Constant for SPDX Package Verification Code field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the package verification code object.</remarks>
    internal const string FieldPackageVerificationCode = "packageVerificationCode";

    /// <summary>
    ///     Constant for SPDX Home Page field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the package home page URL.</remarks>
    internal const string FieldHomePage = "homepage";

    /// <summary>
    ///     Constant for SPDX Source Info field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the package source information text.</remarks>
    internal const string FieldSourceInfo = "sourceInfo";

    /// <summary>
    ///     Constant for SPDX License Info From Files field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the license information from files array in a package.</remarks>
    internal const string FieldLicenseInfoFromFiles = "licenseInfoFromFiles";

    /// <summary>
    ///     Constant for SPDX License Declared field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the declared license expression of a package.</remarks>
    internal const string FieldLicenseDeclared = "licenseDeclared";

    /// <summary>
    ///     Constant for SPDX Summary field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the package summary text.</remarks>
    internal const string FieldSummary = "summary";

    /// <summary>
    ///     Constant for SPDX Description field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the package description text.</remarks>
    internal const string FieldDescription = "description";

    /// <summary>
    ///     Constant for SPDX External Refs field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the external references array on a package.</remarks>
    internal const string FieldExternalRefs = "externalRefs";

    /// <summary>
    ///     Constant for SPDX Primary Package Purpose field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the primary package purpose string.</remarks>
    internal const string FieldPrimaryPackagePurpose = "primaryPackagePurpose";

    /// <summary>
    ///     Constant for SPDX Release Date field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the package release date.</remarks>
    internal const string FieldReleaseDate = "releaseDate";

    /// <summary>
    ///     Constant for SPDX Build Date field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the package build date.</remarks>
    internal const string FieldBuiltDate = "builtDate";

    /// <summary>
    ///     Constant for SPDX Valid Until Date field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the package valid-until date.</remarks>
    internal const string FieldValidUntilDate = "validUntilDate";

    /// <summary>
    ///     Constant for SPDX Snippet From File field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the file reference in a snippet.</remarks>
    internal const string FieldSnippetFromFile = "snippetFromFile";

    /// <summary>
    ///     Constant for SPDX License Info In Snippets field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the license information in snippets array.</remarks>
    internal const string FieldLicenseInfoInSnippets = "licenseInfoInSnippets";

    /// <summary>
    ///     Constant for SPDX Ranges field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the ranges array in a snippet.</remarks>
    internal const string FieldRanges = "ranges";

    /// <summary>
    ///     Constant for SPDX Start Pointer field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the start pointer object in a snippet range.</remarks>
    internal const string FieldStartPointer = "startPointer";

    /// <summary>
    ///     Constant for SPDX End Pointer field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the end pointer object in a snippet range.</remarks>
    internal const string FieldEndPointer = "endPointer";

    /// <summary>
    ///     Constant for SPDX Offset field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the byte offset in a snippet pointer.</remarks>
    internal const string FieldOffset = "offset";

    /// <summary>
    ///     Constant for SPDX Line Number field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the line number in a snippet pointer.</remarks>
    internal const string FieldLineNumber = "lineNumber";

    /// <summary>
    ///     Constant for SPDX Reference field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the file reference in a snippet pointer.</remarks>
    internal const string FieldReference = "reference";

    /// <summary>
    ///     Constant for SPDX Element ID field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the source element ID in a relationship.</remarks>
    internal const string FieldSpdxElementId = "spdxElementId";

    /// <summary>
    ///     Constant for SPDX Related Spdx Element field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the target element ID in a relationship.</remarks>
    internal const string FieldRelatedSpdxElement = "relatedSpdxElement";

    /// <summary>
    ///     Constant for SPDX Relationship Type field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the relationship type string.</remarks>
    internal const string FieldRelationshipType = "relationshipType";

    /// <summary>
    ///     Constant for SPDX Package Verification Code Excluded Files field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the excluded files array in a package verification code.</remarks>
    internal const string FieldPackageVerificationCodeExcludedFiles = "packageVerificationCodeExcludedFiles";

    /// <summary>
    ///     Constant for SPDX Package Verification Code Value field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the hash value in a package verification code.</remarks>
    internal const string FieldPackageVerificationCodeValue = "packageVerificationCodeValue";

    /// <summary>
    ///     Constant for SPDX Reference Category field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the reference category in an external reference.</remarks>
    internal const string FieldReferenceCategory = "referenceCategory";

    /// <summary>
    ///     Constant for SPDX Reference Type field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the reference type in an external reference.</remarks>
    internal const string FieldReferenceType = "referenceType";

    /// <summary>
    ///     Constant for SPDX Reference Locator field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the reference locator in an external reference.</remarks>
    internal const string FieldReferenceLocator = "referenceLocator";

    /// <summary>
    ///     Constant for SPDX Algorithm field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the checksum algorithm.</remarks>
    internal const string FieldAlgorithm = "algorithm";

    /// <summary>
    ///     Constant for SPDX Checksum Value field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the checksum hash value.</remarks>
    internal const string FieldChecksumValue = "checksumValue";

    /// <summary>
    ///     Constant for SPDX Annotator field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the annotator field in an annotation.</remarks>
    internal const string FieldAnnotator = "annotator";

    /// <summary>
    ///     Constant for SPDX Annotation Date field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the annotation date field.</remarks>
    internal const string FieldAnnotationDate = "annotationDate";

    /// <summary>
    ///     Constant for SPDX Annotation Type field
    /// </summary>
    /// <remarks>The SPDX 2.x JSON field name for the annotation type field.</remarks>
    internal const string FieldAnnotationType = "annotationType";
}
