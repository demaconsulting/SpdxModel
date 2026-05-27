## SpdxPackage

### Purpose

`SpdxPackage` represents an SPDX package — the primary building block of a Software Bill of
Materials. It captures identity, provenance, licensing, verification, and dependency metadata
for a software package.

### Data Model

**Name**: `string` — Package name. Used as the match key (together with `Version`) for array
merging.

**Version**: `string?` — Package version string; null if not specified.

**FileName**: `string?` — Filename of the package archive or distribution artifact.

**Supplier**: `string?` — Entity distributing the package (in `Organization: name` or
`Person: name` format).

**Originator**: `string?` — Entity that originally authored or created the package.

**DownloadLocation**: `string` — URI from which the package was or can be obtained.

**FilesAnalyzed**: `bool?` — Whether the files within the package have been analyzed; `null`
means unspecified.

**HasFiles**: `string[]` — SPDX IDs of `SpdxFile` elements that belong to this package. When
`doc` is passed to `Validate`, each ID is verified to exist in `doc.Files`.

**VerificationCode**: `SpdxPackageVerificationCode?` — Cryptographic verification code computed
over the package's files; absent when `FilesAnalyzed` is `false`.

**Checksums**: `SpdxChecksum[]` — Package-level checksums using one or more algorithms.

**HomePage**: `string?` — URI of the package home page; `null` if not specified.

**SourceInformation**: `string?` — Human-readable description of how the package was acquired
or modified from the original source; `null` if not specified.

**LicenseInfoFromFiles**: `string[]` — License expressions found in files of the package.

**DeclaredLicense**: `string` — License declared by the package authors; may be empty when not
specified by the package.

**Summary**: `string?` — Short human-readable description of the package; `null` if not
specified.

**Description**: `string?` — Detailed human-readable description of the package; `null` if not
specified.

**Comment**: `string?` — Free-text human annotation; `null` if not specified.

**ExternalReferences**: `SpdxExternalReference[]` — Links to external resources such as package
registries or vulnerability databases.

**PrimaryPackagePurpose**: `string?` — Primary purpose classification (e.g., `LIBRARY`,
`APPLICATION`).

**ReleaseDate**: `string?` — Date and time the package was released, in SPDX date-time format
(`YYYY-MM-DDThh:mm:ssZ`); `null` if not specified.

**BuiltDate**: `string?` — Date and time the package was built, in SPDX date-time format
(`YYYY-MM-DDThh:mm:ssZ`); `null` if not specified.

**ValidUntilDate**: `string?` — Date and time after which the package should no longer be
considered valid, in SPDX date-time format (`YYYY-MM-DDThh:mm:ssZ`); `null` if not specified.

*Inherited from `SpdxLicenseElement`*: `Id`, `ConcludedLicense`, `LicenseComments`,
`CopyrightText`, `AttributionText`, `Annotations`.

### Key Methods

**DeepCopy**: Returns a fully deep-copied instance.

- *Parameters*: none.
- *Returns*: `SpdxPackage` — independent copy including all nested objects and arrays.
- *Preconditions*: none.
- *Postconditions*: The returned instance shares no mutable references with the original.

**Enhance**: Fills in missing fields from another instance.

- *Parameters*: `SpdxPackage other` — source of additional field values.
- *Returns*: `void`
- *Preconditions*: none.
- *Postconditions*: Empty or null fields in this instance are populated from `other`. The nullable
  `FilesAnalyzed` field is populated from `other` when null. Array fields `LicenseInfoFromFiles`,
  `Checksums`, and `ExternalReferences` are merged by deduplication. The `HasFiles` array is
  intentionally not merged because it contains document-scoped SPDX element IDs that may not be
  valid across documents.

**Enhance (static array merge)**: Merges two package arrays, matching on `Name` and `Version`.

- *Parameters*: `SpdxPackage[] base`, `SpdxPackage[] additions`.
- *Returns*: `SpdxPackage[]` — merged array.
- *Preconditions*: none.
- *Postconditions*: Matching entries are enhanced; new entries are appended.

**Validate**: Validates the package, including NTIA minimum element checks when requested.

- *Parameters*: `List<string> issues` — list to append issues to; `SpdxDocument? document` —
  owning document for cross-reference validation; `bool ntia` — when `true`, also checks NTIA
  SBOM minimum elements.
- *Returns*: `void`
- *Preconditions*: none.
- *Postconditions*: All discovered issues including nested checksum and external reference
  issues are appended to `issues`; an empty `DeclaredLicense` does not produce a validation issue.
 Non-null `ReleaseDate`, `BuiltDate`, or `ValidUntilDate` values that do not conform to the SPDX
 date-time format (`YYYY-MM-DDThh:mm:ssZ`) each cause a validation issue to be recorded.
 When `doc` is non-null, entries in `HasFiles` that do not match any file ID in `doc.Files`
 cause an issue to be recorded.

**Same**: `static IEqualityComparer<SpdxPackage>` — compares by `Name` and `Version`.

### Error Handling

Validation errors are collected into the `List<string>` passed to `Validate`. Nested checksums,
external references, and the verification code are also validated. No exceptions are thrown by
`DeepCopy`, `Enhance`, or the static merge method. When `doc` is provided and `HasFiles`
contains IDs for files not present in `doc.Files`, a single issue is appended:
`Package '{name}' HasFiles references missing files`.

### Dependencies

- **SpdxLicenseElement** — abstract base class.
- **SpdxChecksum** — package-level checksums.
- **SpdxExternalReference** — external resource links.
- **SpdxPackageVerificationCode** — optional package integrity code.

### Callers

- **SpdxDocument** — holds the `Packages` array.
- **Spdx2JsonDeserializer** — constructs `SpdxPackage` instances during deserialization.
- **Spdx2JsonSerializer** — serializes `SpdxPackage` instances to JSON.
