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

namespace DemaConsulting.SpdxModel.Tests;

/// <summary>
///     Tests for the <see cref="SpdxPackage" /> class.
/// </summary>
/// <remarks>
///     Covers equality comparison via the <see cref="SpdxPackage.Same" /> comparer, deep-copy independence,
///     field merging via <see cref="SpdxPackage.Enhance(SpdxPackage[],SpdxPackage[])" />, and full
///     validation including NTIA minimum-elements checks. Each test exercises a single scenario or
///     boundary condition in isolation with no shared state between tests.
/// </remarks>
public class SpdxPackageTests
{
    /// <summary>
    ///     Tests the <see cref="SpdxPackage.Same" /> comparer compares packages correctly.
    /// </summary>
    /// <remarks>
    ///     Verifies that two packages with the same <c>Name</c> and <c>Version</c> are considered equal
    ///     regardless of differing <c>Id</c> values, that packages with different names or versions are
    ///     distinct, and that null arguments are handled correctly.
    /// </remarks>
    [Fact]
    public void SpdxPackage_SameComparer_ComparesCorrectly()
    {
        // Arrange: Create several SpdxPackage instances with different IDs, names, and versions
        var p1 = new SpdxPackage
        {
            Id = "SPDXRef-Package1",
            Name = "DemaConsulting.SpdxModel",
            Version = "0.0.0"
        };
        var p2 = new SpdxPackage
        {
            Id = "SPDXRef-Package-SpdxModel",
            Name = "DemaConsulting.SpdxModel",
            Version = "0.0.0"
        };
        var p3 = new SpdxPackage
        {
            Id = "SPDXRef-Package1",
            Name = "SomePackage",
            Version = "1.2.3"
        };

        // Act / Assert: Verify packages compare to themselves
        Assert.True(SpdxPackage.Same.Equals(p1, p1));
        Assert.True(SpdxPackage.Same.Equals(p2, p2));
        Assert.True(SpdxPackage.Same.Equals(p3, p3));

        // Assert: Verify packages compare correctly
        Assert.True(SpdxPackage.Same.Equals(p1, p2));
        Assert.True(SpdxPackage.Same.Equals(p2, p1));
        Assert.False(SpdxPackage.Same.Equals(p1, p3));
        Assert.False(SpdxPackage.Same.Equals(p3, p1));
        Assert.False(SpdxPackage.Same.Equals(p2, p3));
        Assert.False(SpdxPackage.Same.Equals(p3, p2));

        // Assert: Verify same packages have identical hashes
        Assert.Equal(SpdxPackage.Same.GetHashCode(p1), SpdxPackage.Same.GetHashCode(p2));

        // Assert: Verify null handling
        Assert.False(SpdxPackage.Same.Equals(null!, p1));
        Assert.False(SpdxPackage.Same.Equals(p1, null!));
        Assert.True(SpdxPackage.Same.Equals(null!, null!));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxPackage.DeepCopy" /> method successfully creates a deep copy.
    /// </summary>
    /// <remarks>
    ///     Verifies that the returned instance has equal field values, that all array and nested object
    ///     fields are new independent instances, and that mutating the copy does not affect the original.
    /// </remarks>
    [Fact]
    public void SpdxPackage_DeepCopy_CreatesEqualButDistinctInstance()
    {
        // Arrange: Create a SpdxPackage with various properties
        var p1 = new SpdxPackage
        {
            Id = "SPDXRef-Package1",
            Name = "DemaConsulting.SpdxModel",
            Version = "0.0.0",
            VerificationCode = new SpdxPackageVerificationCode
            {
                Value = "d6a770ba38583ed4bb4525bd96e50461655d2759"
            },
            Checksums =
            [
                new SpdxChecksum
                {
                    Algorithm = SpdxChecksumAlgorithm.Sha1,
                    Value = "d6a770ba38583ed4bb4525bd96e50461655d2759"
                }
            ],
            ExternalReferences =
            [
                new SpdxExternalReference
                {
                    Category = SpdxReferenceCategory.Security,
                    Type = "cpe23Type",
                    Locator = "cpe:2.3:a:company:product:0.0.0:*:*:*:*:*:*:*"
                }
            ],
            Annotations =
            [
                new SpdxAnnotation
                {
                    Annotator = "Person: Malcolm Nixon",
                    Date = "2024-05-28T01:30:00Z",
                    Type = SpdxAnnotationType.Review,
                    Comment = "Looks good"
                }
            ]
        };

        // Act: Create a deep copy of the SpdxPackage
        var p2 = p1.DeepCopy();

        // Assert: Verify deep-copy is equal to original
        Assert.Equal(p1, p2, SpdxPackage.Same);
        Assert.Equal(p1.Id, p2.Id);
        Assert.Equal(p1.Name, p2.Name);
        Assert.Equal(p1.Version, p2.Version);
        Assert.Equal(p1.HasFiles, p2.HasFiles);
        SpdxTestHelpers.AssertEquivalent(p1.Checksums, p2.Checksums, SpdxChecksum.Same);
        SpdxTestHelpers.AssertEquivalent(p1.LicenseInfoFromFiles, p2.LicenseInfoFromFiles, StringComparer.Ordinal);
        SpdxTestHelpers.AssertEquivalent(p1.ExternalReferences, p2.ExternalReferences, SpdxExternalReference.Same);
        Assert.Equal(p1.AttributionText, p2.AttributionText);
        SpdxTestHelpers.AssertEquivalent(p1.Annotations, p2.Annotations, SpdxAnnotation.Same);
        Assert.NotNull(p2.VerificationCode);
        Assert.Equal(p1.VerificationCode!.Value, p2.VerificationCode.Value);

        // Assert: Verify deep-copy has distinct instances
        Assert.False(ReferenceEquals(p1, p2));
        Assert.False(ReferenceEquals(p1.HasFiles, p2.HasFiles));
        Assert.False(ReferenceEquals(p1.Checksums, p2.Checksums));
        Assert.False(ReferenceEquals(p1.LicenseInfoFromFiles, p2.LicenseInfoFromFiles));
        Assert.False(ReferenceEquals(p1.ExternalReferences, p2.ExternalReferences));
        Assert.False(ReferenceEquals(p1.AttributionText, p2.AttributionText));
        Assert.False(ReferenceEquals(p1.Annotations, p2.Annotations));
        Assert.False(ReferenceEquals(p1.VerificationCode, p2.VerificationCode));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxPackage.Enhance(SpdxPackage[], SpdxPackage[])" /> method correctly adds or updates
    ///     packages.
    /// </summary>
    /// <remarks>
    ///     Verifies that a matching package (same name and version) is enhanced in place — including populating a null
    ///     <c>FilesAnalyzed</c> field from the source — and that a non-matching package from the source array is
    ///     deep-copied and appended, resulting in an array of length two.
    /// </remarks>
    [Fact]
    public void SpdxPackage_Enhance_AddsOrUpdatesPackagesCorrectly()
    {
        // Arrange: Set up the initial packages and the packages to enhance with.
        var packages = new[]
        {
            new SpdxPackage
            {
                Id = "SPDXRef-Package1",
                Name = "DemaConsulting.SpdxModel",
                Version = "0.0.0"
            }
        };

        // Act: Call the Enhance method to add or update packages.
        packages = SpdxPackage.Enhance(
            packages,
            [
                new SpdxPackage
                {
                    Id = "SPDXRef-Package-SpdxModel",
                    Name = "DemaConsulting.SpdxModel",
                    Version = "0.0.0",
                    FilesAnalyzed = true
                },
                new SpdxPackage
                {
                    Id = "SPDXRef-Package1",
                    Name = "SomePackage",
                    Version = "1.2.3"
                }
            ]);

        // Assert: Verify the resulting packages are as expected.
        Assert.Equal(2, packages.Length);
        Assert.Equal("SPDXRef-Package1", packages[0].Id);
        Assert.Equal("DemaConsulting.SpdxModel", packages[0].Name);
        Assert.Equal("0.0.0", packages[0].Version);
        Assert.True(packages[0].FilesAnalyzed);
        Assert.Equal("SPDXRef-Package1", packages[1].Id);
        Assert.Equal("SomePackage", packages[1].Name);
        Assert.Equal("1.2.3", packages[1].Version);
    }

    /// <summary>
    ///     Tests that a valid package passes validation.
    /// </summary>
    /// <remarks>
    ///     Exercises the happy-path: a fully populated package with a valid SPDX ID, non-empty name, download
    ///     location, and a conforming supplier string passes all validation checks including NTIA minimum elements.
    /// </remarks>
    [Fact]
    public void SpdxPackage_Validate_Success()
    {
        // Arrange: Construct a valid SpdxPackage
        var package = new SpdxPackage
        {
            Id = "SPDXRef-Package-SpdxModel",
            Name = "DemaConsulting.SpdxModel",
            Version = "0.0.0",
            DownloadLocation = "https://www.nuget.org/packages/DemaConsulting.SpdxModel",
            Supplier = "Organization: DemaConsulting"
        };

        // Act: Validate the package
        var issues = new List<string>();
        package.Validate(issues, null, true);

        // Assert: Verify that the validation reports no issues.
        Assert.Empty(issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxPackage.Validate" /> method reports missing package names
    /// </summary>
    /// <remarks>
    ///     Verifies the boundary condition where <c>Name</c> is empty: validation must report the
    ///     "Invalid Package Name Field - Empty" issue.
    /// </remarks>
    [Fact]
    public void SpdxPackage_Validate_MissingPackageName_ReportsIssue()
    {
        // Arrange: Construct a bad SpdxPackage
        var package = new SpdxPackage
        {
            Id = "SPDXRef-Package-SpdxModel",
            Name = "",
            Version = "0.0.0",
            DownloadLocation = "https://www.nuget.org/packages/DemaConsulting.SpdxModel",
            Supplier = "Organization: DemaConsulting"
        };

        // Act: Validate the package
        var issues = new List<string>();
        package.Validate(issues, null, true);

        // Assert: Verify the issue is reported
        Assert.Contains("Package Invalid Package Name Field - Empty", issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxPackage.Validate" /> method reports invalid package IDs
    /// </summary>
    /// <remarks>
    ///     Verifies that an <c>Id</c> not starting with <c>SPDXRef-</c> is flagged as an invalid SPDX
    ///     identifier field.
    /// </remarks>
    [Fact]
    public void SpdxPackage_Validate_InvalidPackageId_ReportsIssue()
    {
        // Arrange: Construct a bad SpdxPackage
        var package = new SpdxPackage
        {
            Id = "BadId",
            Name = "DemaConsulting.SpdxModel",
            Version = "0.0.0",
            DownloadLocation = "https://www.nuget.org/packages/DemaConsulting.SpdxModel",
            Supplier = "Organization: DemaConsulting"
        };

        // Act: Validate the package
        var issues = new List<string>();
        package.Validate(issues, null, true);

        // Assert: Verify the issue is reported
        Assert.Contains("Package 'DemaConsulting.SpdxModel' Invalid SPDX Identifier Field 'BadId'", issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxPackage.Validate" /> method reports missing download locations
    /// </summary>
    /// <remarks>
    ///     Verifies that an empty <c>DownloadLocation</c> causes the "Invalid Package Download Location Field - Empty"
    ///     issue to be reported.
    /// </remarks>
    [Fact]
    public void SpdxPackage_Validate_MissingDownload_ReportsIssue()
    {
        // Arrange: Construct a bad SpdxPackage
        var package = new SpdxPackage
        {
            Id = "SPDXRef-Package-SpdxModel",
            Name = "DemaConsulting.SpdxModel",
            Version = "0.0.0",
            DownloadLocation = "",
            Supplier = "Organization: DemaConsulting"
        };

        // Act: Validate the package
        var issues = new List<string>();
        package.Validate(issues, null, true);

        // Assert: Verify the issue is reported
        Assert.Contains("Package 'DemaConsulting.SpdxModel' Invalid Package Download Location Field - Empty", issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxPackage.Validate" /> method reports invalid suppliers.
    /// </summary>
    /// <remarks>
    ///     Verifies that a supplier value that does not start with <c>Person:</c>, <c>Organization:</c>, or equal
    ///     <c>NOASSERTION</c> is flagged as an invalid supplier field.
    /// </remarks>
    [Fact]
    public void SpdxPackage_Validate_InvalidSupplier_ReportsIssue()
    {
        // Arrange: Construct a package with invalid supplier format
        var package = new SpdxPackage
        {
            Id = "SPDXRef-Package-SpdxModel",
            Name = "DemaConsulting.SpdxModel",
            Version = "0.0.0",
            DownloadLocation = "https://www.nuget.org/packages/DemaConsulting.SpdxModel",
            Supplier = "BadSupplier"
        };

        // Act: Validate the package
        var issues = new List<string>();
        package.Validate(issues, null, true);

        // Assert: Verify the issue is reported
        Assert.Contains("Package 'DemaConsulting.SpdxModel' Invalid Package Supplier Field 'BadSupplier'", issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxPackage.Validate" /> method reports invalid originators.
    /// </summary>
    /// <remarks>
    ///     Verifies that an originator value that does not start with <c>Person:</c>, <c>Organization:</c>, or equal
    ///     <c>NOASSERTION</c> is flagged as an invalid originator field.
    /// </remarks>
    [Fact]
    public void SpdxPackage_Validate_InvalidOriginator_ReportsIssue()
    {
        // Arrange: Construct a package with invalid originator format
        var package = new SpdxPackage
        {
            Id = "SPDXRef-Package-SpdxModel",
            Name = "DemaConsulting.SpdxModel",
            Version = "0.0.0",
            DownloadLocation = "https://www.nuget.org/packages/DemaConsulting.SpdxModel",
            Supplier = "Organization: DemaConsulting",
            Originator = "BadOriginator"
        };

        // Act: Validate the package
        var issues = new List<string>();
        package.Validate(issues, null, true);

        // Assert: Verify the issue is reported
        Assert.Contains("Package 'DemaConsulting.SpdxModel' Invalid Package Originator Field 'BadOriginator'", issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxPackage.Validate" /> method reports invalid release dates.
    /// </summary>
    /// <remarks>
    ///     Verifies that a <c>ReleaseDate</c> that does not conform to the SPDX date-time format causes the
    ///     "Invalid Release Date Field" issue to be reported.
    /// </remarks>
    [Fact]
    public void SpdxPackage_Validate_InvalidReleaseDate_ReportsIssue()
    {
        // Arrange: Construct a package with invalid release date format
        var package = new SpdxPackage
        {
            Id = "SPDXRef-Package-SpdxModel",
            Name = "DemaConsulting.SpdxModel",
            Version = "0.0.0",
            DownloadLocation = "https://www.nuget.org/packages/DemaConsulting.SpdxModel",
            Supplier = "Organization: DemaConsulting",
            ReleaseDate = "BadDate"
        };

        // Act: Validate the package
        var issues = new List<string>();
        package.Validate(issues, null, true);

        // Assert: Verify the issue is reported
        Assert.Contains("Package 'DemaConsulting.SpdxModel' Invalid Release Date Field 'BadDate'", issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxPackage.Validate" /> method reports invalid built dates.
    /// </summary>
    /// <remarks>
    ///     Verifies that a <c>BuiltDate</c> that does not conform to the SPDX date-time format causes the
    ///     "Invalid Built Date Field" issue to be reported.
    /// </remarks>
    [Fact]
    public void SpdxPackage_Validate_InvalidBuiltDate_ReportsIssue()
    {
        // Arrange: Construct a package with invalid built date format
        var package = new SpdxPackage
        {
            Id = "SPDXRef-Package-SpdxModel",
            Name = "DemaConsulting.SpdxModel",
            Version = "0.0.0",
            DownloadLocation = "https://www.nuget.org/packages/DemaConsulting.SpdxModel",
            Supplier = "Organization: DemaConsulting",
            BuiltDate = "BadDate"
        };

        // Act: Validate the package
        var issues = new List<string>();
        package.Validate(issues, null, true);

        // Assert: Verify the issue is reported
        Assert.Contains("Package 'DemaConsulting.SpdxModel' Invalid Built Date Field 'BadDate'", issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxPackage.Validate" /> method reports bad valid until dates
    /// </summary>
    /// <remarks>
    ///     Verifies that a <c>ValidUntilDate</c> that does not conform to the SPDX date-time format causes the
    ///     "Invalid Valid Until Date Field" issue to be reported.
    /// </remarks>
    [Fact]
    public void SpdxPackage_Validate_InvalidValidUntilDate_ReportsIssue()
    {
        // Arrange: Construct a bad SpdxPackage
        var package = new SpdxPackage
        {
            Id = "SPDXRef-Package-SpdxModel",
            Name = "DemaConsulting.SpdxModel",
            Version = "0.0.0",
            DownloadLocation = "https://www.nuget.org/packages/DemaConsulting.SpdxModel",
            Supplier = "Organization: DemaConsulting",
            ValidUntilDate = "BadDate"
        };

        // Act: Validate the package
        var issues = new List<string>();
        package.Validate(issues, null, true);

        // Assert: Verify the issue is reported
        Assert.Contains("Package 'DemaConsulting.SpdxModel' Invalid Valid Until Date Field 'BadDate'", issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxPackage.Validate" /> method validates annotations.
    /// </summary>
    /// <remarks>
    ///     Verifies that an annotation with an empty <c>Annotator</c> field causes the
    ///     "Invalid Annotator Field - Empty" issue to be reported with the correct package prefix.
    /// </remarks>
    [Fact]
    public void SpdxPackage_Validate_InvalidAnnotation_ReportsIssue()
    {
        // Arrange: Construct a package with an invalid annotation
        var package = new SpdxPackage
        {
            Id = "SPDXRef-Package-SpdxModel",
            Name = "DemaConsulting.SpdxModel",
            Version = "0.0.0",
            DownloadLocation = "https://www.nuget.org/packages/DemaConsulting.SpdxModel",
            Supplier = "Organization: DemaConsulting",
            Annotations =
            [
                new SpdxAnnotation
                {
                    Annotator = "",
                    Date = "2024-05-28T01:30:00Z",
                    Type = SpdxAnnotationType.Review,
                    Comment = "Looks good"
                }
            ]
        };

        // Act: Validate the package
        var issues = new List<string>();
        package.Validate(issues, null);

        // Assert: Verify the annotation issue is reported with the correct prefix
        Assert.Contains("Package 'DemaConsulting.SpdxModel' Invalid Annotator Field - Empty", issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxPackage.Validate" /> method reports HasFiles references to missing files.
    /// </summary>
    /// <remarks>
    ///     Verifies that when a document is provided and <c>HasFiles</c> references a file ID that does not
    ///     exist in <c>doc.Files</c>, the "HasFiles references missing files" issue is reported.
    /// </remarks>
    [Fact]
    public void SpdxPackage_Validate_HasFilesReferencesMissingFile_ReportsIssue()
    {
        // Arrange: Create a package that references a file that does not exist in the document
        var package = new SpdxPackage
        {
            Id = "SPDXRef-Package-SpdxModel",
            Name = "DemaConsulting.SpdxModel",
            Version = "0.0.0",
            DownloadLocation = "https://www.nuget.org/packages/DemaConsulting.SpdxModel",
            Supplier = "Organization: DemaConsulting",
            HasFiles = ["SPDXRef-Missing-File"]
        };
        var doc = new SpdxDocument
        {
            Files = []
        };

        // Act: Validate the package with the document
        var issues = new List<string>();
        package.Validate(issues, doc);

        // Assert: Verify the HasFiles reference issue is reported
        Assert.Contains(issues, issue => issue.Contains("Package 'DemaConsulting.SpdxModel' HasFiles references missing files"));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxPackage.Validate" /> method reports missing NTIA supplier.
    /// </summary>
    /// <remarks>
    ///     Verifies that when NTIA validation is enabled, a package without a supplier causes the
    ///     "NTIA: Package Missing Supplier" issue to be reported.
    /// </remarks>
    [Fact]
    public void SpdxPackage_ValidateNtia_MissingSupplier_ReportsIssue()
    {
        // Arrange: Create a package with no supplier
        var package = new SpdxPackage
        {
            Id = "SPDXRef-Package-SpdxModel",
            Name = "DemaConsulting.SpdxModel",
            Version = "0.0.0",
            DownloadLocation = "https://www.nuget.org/packages/DemaConsulting.SpdxModel"
        };

        // Act: Validate the package with NTIA checks enabled
        var issues = new List<string>();
        package.Validate(issues, null, ntia: true);

        // Assert: Verify the missing supplier issue is reported
        Assert.Contains(issues, issue => issue.Contains("NTIA: Package 'DemaConsulting.SpdxModel' Missing Supplier"));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxPackage.Validate" /> method reports missing NTIA version.
    /// </summary>
    /// <remarks>
    ///     Verifies that when NTIA validation is enabled, a package without a version string causes the
    ///     "NTIA: Package Missing Version" issue to be reported.
    /// </remarks>
    [Fact]
    public void SpdxPackage_ValidateNtia_MissingVersion_ReportsIssue()
    {
        // Arrange: Create a package with no version
        var package = new SpdxPackage
        {
            Id = "SPDXRef-Package-SpdxModel",
            Name = "DemaConsulting.SpdxModel",
            DownloadLocation = "https://www.nuget.org/packages/DemaConsulting.SpdxModel",
            Supplier = "Organization: DemaConsulting"
        };

        // Act: Validate the package with NTIA checks enabled
        var issues = new List<string>();
        package.Validate(issues, null, ntia: true);

        // Assert: Verify the missing version issue is reported
        Assert.Contains(issues, issue => issue.Contains("NTIA: Package 'DemaConsulting.SpdxModel' Missing Version"));
    }
}
