# SpdxPackage Unit Design

## Purpose

`SpdxPackage` represents an SPDX package — the primary building block of a Software Bill of
Materials. It captures identity, provenance, licensing, verification, and dependency metadata
for a software package.

## Design

`SpdxPackage` is a sealed class that extends `SpdxLicenseElement`, inheriting `Id`,
`ConcludedLicense`, `CopyrightText`, and attribution fields.

Data members (key fields beyond inherited):

| Property | Type | Description |
| -------- | ---- | ----------- |
| `Name` | `string` | Package name |
| `Version` | `string?` | Package version string |
| `FileName` | `string?` | Filename of the package archive |
| `Supplier` / `Originator` | `string?` | Entity distributing / originating the package |
| `DownloadLocation` | `string` | URI from which the package was obtained |
| `FilesAnalyzed` | `bool?` | Whether files in the package have been analyzed |
| `VerificationCode` | `SpdxPackageVerificationCode?` | Cryptographic verification code |
| `Checksums` | `SpdxChecksum[]` | Package-level checksums |
| `LicenseInfoFromFiles` | `string[]` | Licenses found in files of the package |
| `DeclaredLicense` | `string` | License declared by the package authors; may be empty when not specified |
| `ExternalReferences` | `SpdxExternalReference[]` | Links to external resources |
| `PrimaryPackagePurpose` | `string?` | Primary purpose classification |

Key methods:

- `DeepCopy()` — returns a fully deep-copied instance
- `Enhance(SpdxPackage)` — fills in missing fields from another instance
- `Enhance(array, array)` — static merging of two package arrays, matching on `Name` + `Version`
- `Validate(List<string>, SpdxDocument?, bool ntia)` — full validation including NTIA minimum elements; empty `DeclaredLicense` is permitted and does not produce a validation issue
- `Same` — static `IEqualityComparer` comparing by `Name` and `Version`

## Dependencies

- `SpdxLicenseElement` (base class)
- `SpdxChecksum`, `SpdxExternalReference`, `SpdxPackageVerificationCode`
