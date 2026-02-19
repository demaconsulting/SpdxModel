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
[TestClass]
public class SpdxPackageTests
{
    /// <summary>
    ///     Tests the <see cref="SpdxPackage.Same" /> comparer compares packages correctly.
    /// </summary>
    [TestMethod]
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

        // Assert: Verify packages compare to themselves
        Assert.IsTrue(SpdxPackage.Same.Equals(p1, p1));
        Assert.IsTrue(SpdxPackage.Same.Equals(p2, p2));
        Assert.IsTrue(SpdxPackage.Same.Equals(p3, p3));

        // Assert: Verify packages compare correctly
        Assert.IsTrue(SpdxPackage.Same.Equals(p1, p2));
        Assert.IsTrue(SpdxPackage.Same.Equals(p2, p1));
        Assert.IsFalse(SpdxPackage.Same.Equals(p1, p3));
        Assert.IsFalse(SpdxPackage.Same.Equals(p3, p1));
        Assert.IsFalse(SpdxPackage.Same.Equals(p2, p3));
        Assert.IsFalse(SpdxPackage.Same.Equals(p3, p2));

        // Assert: Verify same packages have identical hashes
        Assert.AreEqual(SpdxPackage.Same.GetHashCode(p1), SpdxPackage.Same.GetHashCode(p2));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxPackage.DeepCopy" /> method successfully creates a deep copy.
    /// </summary>
    [TestMethod]
    public void SpdxPackage_DeepCopy_CreatesEqualButDistinctInstance()
    {
        // Arrange: Create a SpdxPackage with various properties
        var p1 = new SpdxPackage
        {
            Id = "SPDXRef-Package1",
            Name = "DemaConsulting.SpdxModel",
            Version = "0.0.0",
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
        Assert.AreEqual(p1, p2, SpdxPackage.Same);
        Assert.AreEqual(p1.Id, p2.Id);
        Assert.AreEqual(p1.Name, p2.Name);
        Assert.AreEqual(p1.Version, p2.Version);
        CollectionAssert.AreEquivalent(p1.HasFiles, p2.HasFiles);
        CollectionAssert.AreEquivalent(p1.Checksums, p2.Checksums, SpdxChecksum.Same);
        CollectionAssert.AreEquivalent(p1.LicenseInfoFromFiles, p2.LicenseInfoFromFiles);
        CollectionAssert.AreEquivalent(p1.ExternalReferences, p2.ExternalReferences, SpdxExternalReference.Same);
        CollectionAssert.AreEquivalent(p1.AttributionText, p2.AttributionText);
        CollectionAssert.AreEquivalent(p1.Annotations, p2.Annotations, SpdxAnnotation.Same);

        // Assert: Verify deep-copy has distinct instances
        Assert.IsFalse(ReferenceEquals(p1, p2));
        Assert.IsFalse(ReferenceEquals(p1.HasFiles, p2.HasFiles));
        Assert.IsFalse(ReferenceEquals(p1.Checksums, p2.Checksums));
        Assert.IsFalse(ReferenceEquals(p1.LicenseInfoFromFiles, p2.LicenseInfoFromFiles));
        Assert.IsFalse(ReferenceEquals(p1.ExternalReferences, p2.ExternalReferences));
        Assert.IsFalse(ReferenceEquals(p1.AttributionText, p2.AttributionText));
        Assert.IsFalse(ReferenceEquals(p1.Annotations, p2.Annotations));
    }

    /// <summary>
    ///     Tests the <see cref="SpdxPackage.Enhance(SpdxPackage[], SpdxPackage[])" /> method correctly adds or updates
    ///     packages.
    /// </summary>
    [TestMethod]
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
                    Version = "0.0.0"
                },
                new SpdxPackage
                {
                    Id = "SPDXRef-Package1",
                    Name = "SomePackage",
                    Version = "1.2.3"
                }
            ]);

        // Assert: Verify the resulting packages are as expected.
        Assert.HasCount(2, packages);
        Assert.AreEqual("SPDXRef-Package1", packages[0].Id);
        Assert.AreEqual("DemaConsulting.SpdxModel", packages[0].Name);
        Assert.AreEqual("0.0.0", packages[0].Version);
        Assert.AreEqual("SPDXRef-Package1", packages[1].Id);
        Assert.AreEqual("SomePackage", packages[1].Name);
        Assert.AreEqual("1.2.3", packages[1].Version);
    }

    /// <summary>
    ///     Tests that a valid package passes validation.
    /// </summary>
    [TestMethod]
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
        Assert.IsEmpty(issues);
    }

    /// <summary>
    ///     Tests the <see cref="SpdxPackage.Validate" /> method reports missing package names
    /// </summary>
    [TestMethod]
    public void SpdxPackage_Validate_MissingPackageName()
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
    [TestMethod]
    public void SpdxPackage_Validate_InvalidPackageId()
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
    [TestMethod]
    public void SpdxPackage_Validate_MissingDownload()
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
    ///     Tests the <see cref="SpdxPackage.Validate" /> method reports bad suppliers
    /// </summary>
    [TestMethod]
    public void SpdxPackage_Validate_BadSupplier()
    {
        // Arrange: Construct a bad SpdxPackage
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
    ///     Tests the <see cref="SpdxPackage.Validate" /> method reports bad originators
    /// </summary>
    [TestMethod]
    public void SpdxPackage_Validate_BadOriginator()
    {
        // Arrange: Construct a bad SpdxPackage
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
    ///     Tests the <see cref="SpdxPackage.Validate" /> method reports bad release dates
    /// </summary>
    [TestMethod]
    public void SpdxPackage_Validate_BadReleaseDate()
    {
        // Arrange: Construct a bad SpdxPackage
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
    ///     Tests the <see cref="SpdxPackage.Validate" /> method reports bad built dates
    /// </summary>
    [TestMethod]
    public void SpdxPackage_Validate_BadBuiltDate()
    {
        // Arrange: Construct a bad SpdxPackage
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
    [TestMethod]
    public void SpdxPackage_Validate_BadValidUntilDate()
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
}
