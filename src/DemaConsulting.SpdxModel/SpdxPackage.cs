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

namespace DemaConsulting.SpdxModel;

/// <summary>
/// SPDX Package class
/// </summary>
public sealed class SpdxPackage : SpdxLicenseElement
{
    /// <summary>
    /// Equality comparer for the same package
    /// </summary>
    /// <remarks>
    /// This considers packages as being the same if they have the same name
    /// and version.
    /// </remarks>
    public static readonly IEqualityComparer<SpdxPackage> Same = new SpdxPackageSame();

    /// <summary>
    /// Package Name Field
    /// </summary>
    /// <remarks>
    /// Name of this package.
    /// </remarks>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Package Version Field (optional)
    /// </summary>
    /// <remarks>
    /// Provides an indication of the version of the package.
    /// </remarks>
    public string? Version { get; set; }

    /// <summary>
    /// Package File Name Field (optional)
    /// </summary>
    /// <remarks>
    /// The base name of the package file name.
    /// </remarks>
    public string? FileName { get; set; }

    /// <summary>
    /// Package Supplier Field (optional)
    /// </summary>
    /// <remarks>
    /// The name and, optionally, contact information of the person or
    /// organization who was the immediate supplier of this package to the
    /// recipient.
    ///
    /// The supplier may be different from originator when the software has
    /// been repackaged.
    ///
    /// Values of this property must conform to the agent and tool syntax.
    /// </remarks>
    public string? Supplier { get; set; }

    /// <summary>
    /// Package Originator Field (optional)
    /// </summary>
    /// <remarks>
    /// The name and, optionally, contact information of the person or
    /// organization that originally created the package. Values of this
    /// property must conform to the agent and tool syntax.
    /// </remarks>
    public string? Originator { get; set; }

    /// <summary>
    /// Package Download Location Field
    /// </summary>
    /// <remarks>
    /// The URI at which this package is available for download. Private
    /// (i.e., not publicly reachable) URIs are acceptable as values of this
    /// property.
    /// 
    /// The values http://spdx.org/rdf/terms#none and
    /// http://spdx.org/rdf/terms#noassertion may be used to specify that the
    /// package is not downloadable or that no attempt was made to determine
    /// its download location, respectively.
    /// </remarks>
    public string DownloadLocation { get; set; } = string.Empty;

    /// <summary>
    /// Files Analyzed Field (optional)
    /// </summary>
    /// <remarks>
    /// Indicates whether the file content of this package has been available
    /// for or subjected to analysis when creating the SPDX document. If false
    /// indicates packages that represent metadata or URI references to a
    /// project, product, artifact, distribution or a component. If set to
    /// false, the package must not contain any files.
    /// </remarks>
    public bool? FilesAnalyzed { get; set; }

    /// <summary>
    /// Has Files (optional)
    /// </summary>
    /// <remarks>
    /// SPDX ID for files. Indicates that a particular file belongs to this
    /// package.
    /// </remarks>
    public string[] HasFiles { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Package Verification Code (optional)
    /// </summary>
    /// <remarks>
    /// A manifest based verification code of the package.
    /// </remarks>
    public SpdxPackageVerificationCode? VerificationCode { get; set; }

    /// <summary>
    /// Package Checksum Field (optional)
    /// </summary>
    public SpdxChecksum[] Checksums { get; set; } = Array.Empty<SpdxChecksum>();

    /// <summary>
    /// Package Home Page Field (optional)
    /// </summary>
    public string? HomePage { get; set; }

    /// <summary>
    /// Source Information Field (optional)
    /// </summary>
    /// <remarks>
    /// Allows the producer(s) of the SPDX document to describe how the package
    /// was acquired and/or changed from the original source.
    /// </remarks>
    public string? SourceInformation { get; set; }

    /// <summary>
    /// All Licenses Information From Files Field
    /// </summary>
    /// <remarks>
    /// License expressions. See SPDX Annex D for the license expression syntax.
    ///
    /// The licensing information that was discovered directly within the package.
    ///
    /// If not present and FilesAnalyzed property is true or omitted, it implies
    /// an equivalent meaning to NOASSERTION.
    /// </remarks>
    public string[] LicenseInfoFromFiles { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Declared License Field
    /// </summary>
    /// <remarks>
    /// License expression. See SPDX Annex D for the license expression syntax.
    /// 
    /// The licensing that the creators of the software in the package, or the
    /// packager, have declared. Declarations by the original software creator
    /// should be preferred, if they exist.
    /// </remarks>
    public string DeclaredLicense { get; set; } = string.Empty;

    /// <summary>
    /// Package Summary Description Field (optional)
    /// </summary>
    /// <remarks>
    /// Provides a short description of the package.
    /// </remarks>
    public string? Summary { get; set; }

    /// <summary>
    /// Package Detailed Description Field (optional)
    /// </summary>
    /// <remarks>
    /// Provides a detailed description of the package.
    /// </remarks>
    public string? Description { get; set; }

    /// <summary>
    /// Package Comment Field (optional)
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// External References Field (optional)
    /// </summary>
    /// <remarks>
    /// An External Reference allows a Package to reference an external source
    /// of additional information, metadata, enumerations, asset identifiers,
    /// or downloadable content believed to be relevant to the Package.
    /// </remarks>
    public SpdxExternalReference[] ExternalReferences { get; set; } = Array.Empty<SpdxExternalReference>();

    /// <summary>
    /// Primary Package Purpose Field (optional)
    /// </summary>
    /// <remarks>
    /// This field provides information about the primary purpose of the
    /// identified package. Package Purpose is intrinsic to how the package
    /// is being used rather than the content of the package.
    /// </remarks>
    public string? PrimaryPackagePurpose { get; set; }

    /// <summary>
    /// Release Date Field (optional)
    /// </summary>
    /// <remarks>
    /// This field provides a place for recording the date the package was
    /// released.
    /// </remarks>
    public string? ReleaseDate { get; set; }

    /// <summary>
    /// Built Date Field (optional)
    /// </summary>
    /// <remarks>
    /// This field provides a place for recording the actual date the package
    /// was built.
    /// </remarks>
    public string? BuiltDate { get; set; }

    /// <summary>
    /// Valid Until Date Field (optional)
    /// </summary>
    /// <remarks>
    /// This field provides a place for recording the end of the support
    /// period for a package from the supplier.
    /// </remarks>
    public string? ValidUntilDate { get; set; }

    /// <summary>
    /// Make a deep-copy of this object
    /// </summary>
    /// <returns>Deep copy of this object</returns>
    public SpdxPackage DeepCopy() =>
        new()
        {
            Id = Id,
            Name = Name,
            Version = Version,
            FileName = FileName,
            Supplier = Supplier,
            Originator = Originator,
            DownloadLocation = DownloadLocation,
            FilesAnalyzed = FilesAnalyzed,
            HasFiles = HasFiles.ToArray(),
            VerificationCode = VerificationCode?.DeepCopy(),
            Checksums = Checksums.Select(c => c.DeepCopy()).ToArray(),
            HomePage = HomePage,
            SourceInformation = SourceInformation,
            ConcludedLicense = ConcludedLicense,
            LicenseInfoFromFiles = LicenseInfoFromFiles.ToArray(),
            DeclaredLicense = DeclaredLicense,
            LicenseComments = LicenseComments,
            CopyrightText = CopyrightText,
            Summary = Summary,
            Description = Description,
            Comment = Comment,
            ExternalReferences = ExternalReferences.Select(r => r.DeepCopy()).ToArray(),
            AttributionText = AttributionText.ToArray(),
            PrimaryPackagePurpose = PrimaryPackagePurpose,
            ReleaseDate = ReleaseDate,
            BuiltDate = BuiltDate,
            ValidUntilDate = ValidUntilDate,
            Annotations = Annotations.Select(a => a.DeepCopy()).ToArray()
        };

    /// <summary>
    /// Enhance missing fields in the package
    /// </summary>
    /// <param name="other">Other package to enhance with</param>
    public void Enhance(SpdxPackage other)
    {
        // Enhance license information
        EnhanceLicenseElement(other);

        // Populate the name field if missing
        if (string.IsNullOrWhiteSpace(Name))
            Name = other.Name;

        // Populate the version field if missing
        if (string.IsNullOrWhiteSpace(Version))
            Version = other.Version;

        // Populate the file-name field if missing
        if (string.IsNullOrWhiteSpace(FileName))
            FileName = other.FileName;

        // Populate the supplier field if missing
        if (string.IsNullOrWhiteSpace(Supplier) || Supplier == "NOASSERTION")
            Supplier = other.Supplier;

        // Populate the originator field if missing
        if (string.IsNullOrWhiteSpace(Originator) || Originator == "NOASSERTION")
            Originator = other.Originator;

        // Populate the download-location field if missing
        if (string.IsNullOrWhiteSpace(DownloadLocation) || DownloadLocation == "NOASSERTION")
            DownloadLocation = other.DownloadLocation;

        // Enhance or populate the verification code
        if (VerificationCode != null && other.VerificationCode != null)
            VerificationCode?.Enhance(other.VerificationCode);
        else
            VerificationCode = other.VerificationCode?.DeepCopy();

        // Enhance the checksums
        Checksums = SpdxChecksum.Enhance(Checksums, other.Checksums);

        // Populate the home-page if missing
        if (string.IsNullOrWhiteSpace(HomePage) || HomePage == "NOASSERTION")
            HomePage = other.HomePage;

        // Populate the source-information if missing
        if (string.IsNullOrWhiteSpace(SourceInformation))
            SourceInformation = other.SourceInformation;

        // Merge the license-info-from-files entries
        LicenseInfoFromFiles = LicenseInfoFromFiles.Concat(other.LicenseInfoFromFiles).Distinct().ToArray();

        // Populate the declared-license field if missing
        if (string.IsNullOrWhiteSpace(DeclaredLicense) || DeclaredLicense == "NOASSERTION")
            DeclaredLicense = other.DeclaredLicense;

        // Populate the summary field if missing
        if (string.IsNullOrWhiteSpace(Summary))
            Summary = other.Summary;

        // Populate the description field if missing
        if (string.IsNullOrWhiteSpace(Description))
            Description = other.Description;

        // Populate the comment field if missing
        if (string.IsNullOrWhiteSpace(Comment))
            Comment = other.Comment;

        // Enhance external references
        ExternalReferences = SpdxExternalReference.Enhance(ExternalReferences, other.ExternalReferences);

        // Populate the primary-package-purpose field if missing
        if (string.IsNullOrWhiteSpace(PrimaryPackagePurpose))
            PrimaryPackagePurpose = other.PrimaryPackagePurpose;

        // Populate the release-date field if missing
        if (string.IsNullOrWhiteSpace(ReleaseDate))
            ReleaseDate = other.ReleaseDate;

        // Populate the built-date field if missing
        if (string.IsNullOrWhiteSpace(BuiltDate))
            BuiltDate = other.BuiltDate;

        // Populate the valid-until-date field if missing
        if (string.IsNullOrWhiteSpace(ValidUntilDate))
            ValidUntilDate = other.ValidUntilDate;
    }

    /// <summary>
    /// Enhance missing packages in array
    /// </summary>
    /// <param name="array">Array to enhance</param>
    /// <param name="others">Other array to enhance with</param>
    /// <returns>Updated array</returns>
    public static SpdxPackage[] Enhance(SpdxPackage[] array, SpdxPackage[] others)
    {
        // Convert to list
        var list = array.ToList();

        // Iterate over other array
        foreach (var other in others)
        {
            // Check if other item is the same as one we have
            var annotation = list.Find(a => Same.Equals(a, other));
            if (annotation != null)
            {
                // Enhance our item with the other information
                annotation.Enhance(other);
            }
            else
            {
                // Add the new item to our list
                list.Add(other.DeepCopy());
            }
        }

        // Return as array
        return list.ToArray();
    }

    /// <summary>
    /// Perform validation of information
    /// </summary>
    /// <param name="issues">List to populate with issues</param>
    /// <param name="doc">Optional document for checking file-references</param>
    /// <param name="ntia">Perform NTIA validation</param>
    public void Validate(List<string> issues, SpdxDocument? doc, bool ntia = false)
    {
        // Validate Package Name Field
        if (Name.Length == 0)
            issues.Add("Package Invalid Package Name Field");

        // Validate Package Download Location Field
        if (DownloadLocation.Length == 0)
            issues.Add($"Package {Name} Invalid Package Download Location Field");

        // Validate Package Supplier Field
        if (Supplier != null && 
            Supplier != "NOASSERTION" && 
            !Supplier.StartsWith("Person:") &&
            !Supplier.StartsWith("Organization:"))
            issues.Add($"Package {Name} Invalid Package Supplier Field");

        // Validate Package Originator Field
        if (Originator != null &&
            Originator != "NOASSERTION" &&
            !Originator.StartsWith("Person:") &&
            !Originator.StartsWith("Organization:"))
            issues.Add($"Package {Name} Invalid Package Originator Field");

        // Validate verification code
        VerificationCode?.Validate(Name, issues);

        // Validate checksums
        foreach (var checksum in Checksums)
            checksum.Validate($"Package {Name}", issues);

        // Validate external references
        foreach (var externalReference in ExternalReferences)
            externalReference.Validate(Name, issues);

        // If the document is provided then ensure all referenced files exist
        if (doc != null && Array.Exists(HasFiles, file => Array.TrueForAll(doc.Files, df => df.Id != file)))
            issues.Add($"Package {Name} HasFiles references missing files");

        // SPDX NTIA Supplier Name Check
        if (ntia && string.IsNullOrEmpty(Supplier))
            issues.Add($"NTIA: Package {Name} Missing Supplier");

        // SPDX NTIA Version String Check
        if (ntia && string.IsNullOrEmpty(Version))
            issues.Add($"NTIA: Package {Name} Missing Version");

        // SPDX NTIA Unique Identifier Check
        if (ntia && string.IsNullOrEmpty(Id))
            issues.Add($"NTIA: Package {Name} Missing Unique Identifier");

        // Release Date field
        if (!SpdxHelpers.IsValidSpdxDateTime(ReleaseDate))
            issues.Add($"Package {Name} Invalid Release Date Field");

        // Built Date field
        if (!SpdxHelpers.IsValidSpdxDateTime(BuiltDate))
            issues.Add($"Package {Name} Invalid Built Date Field");

        // Valid Until Date field
        if (!SpdxHelpers.IsValidSpdxDateTime(ValidUntilDate))
            issues.Add($"Package {Name} Invalid Valid Until Date Field");
    }

    /// <summary>
    /// Equality Comparer to test for the same package
    /// </summary>
    private sealed class SpdxPackageSame : IEqualityComparer<SpdxPackage>
    {
        /// <inheritdoc />
        public bool Equals(SpdxPackage? p1, SpdxPackage? p2)
        {
            if (ReferenceEquals(p1, p2)) return true;
            if (p1 == null || p2 == null) return false;

            return p1.Name == p2.Name &&
                   p1.Version == p2.Version;
        }

        /// <inheritdoc />
        public int GetHashCode(SpdxPackage obj)
        {
            return obj.Name.GetHashCode() ^ (obj.Version?.GetHashCode() ?? 0);
        }
    }
}