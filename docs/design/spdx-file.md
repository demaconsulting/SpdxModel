# SpdxFile Unit Design

## Purpose

`SpdxFile` represents an individual file within an SPDX document, enabling fine-grained tracking
of source files, binaries, and other artifacts together with their licensing, checksums, and
contributor information.

## Design

`SpdxFile` is a sealed class that extends `SpdxLicenseElement` (which extends `SpdxElement`),
inheriting `Id`, `ConcludedLicense`, `CopyrightText`, and related license fields.

Data members (beyond inherited fields):

| Property | Type | Description |
| -------- | ---- | ----------- |
| `FileName` | `string` | Relative path of the file (e.g., `./src/main.c`) |
| `FileTypes` | `SpdxFileType[]` | File type classifications (SOURCE, BINARY, etc.) |
| `Checksums` | `SpdxChecksum[]` | Integrity checksums for the file |
| `LicenseInfoInFiles` | `string[]` | License expressions found in the file |
| `Comment` | `string?` | Optional comment |
| `Notice` | `string?` | Optional copyright notice text |
| `Contributors` | `string[]` | Contributors to this file |

Key methods:

- `DeepCopy()` — returns a fully deep-copied instance
- `Enhance(SpdxFile)` — fills in missing fields from another instance
- `Enhance(array, array)` — static method merging two file arrays, matching on `FileName`
- `Validate(List<string>)` — appends validation issues to the supplied list
- `Same` — static `IEqualityComparer` comparing by `FileName`

## Dependencies

- `SpdxLicenseElement` (base class)
- `SpdxChecksum`, `SpdxFileType` (enum)
