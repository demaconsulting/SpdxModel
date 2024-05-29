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
    /// Perform validation of information
    /// </summary>
    /// <param name="issues">List to populate with issues</param>
    /// <param name="ntia">Perform NTIA validation</param>
    public void Validate(List<string> issues, bool ntia = false)
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

        // SPDX NTIA Supplier Name Check
        if (ntia && string.IsNullOrEmpty(Supplier))
            issues.Add($"NTIA: Package {Name} Missing Supplier");

        // SPDX NTIA Version String Check
        if (ntia && string.IsNullOrEmpty(Version))
            issues.Add($"NTIA: Package {Name} Missing Version");

        // SPDX NTIA Unique Identifier Check
        if (ntia && string.IsNullOrEmpty(Id))
            issues.Add($"NTIA: Package {Name} Missing Unique Identifier");
    }

    /// <summary>
    /// Equality Comparer to test for the same package
    /// </summary>
    private class SpdxPackageSame : IEqualityComparer<SpdxPackage>
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