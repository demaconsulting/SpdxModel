# SpdxExtractedLicensingInfo Unit Design

## Purpose

`SpdxExtractedLicensingInfo` records the full text and metadata of a non-standard license found
within a software package. It is used when the license does not appear on the SPDX License List
and must be captured verbatim for compliance purposes.

## Design

`SpdxExtractedLicensingInfo` is a sealed class with no base class.

Data members:

| Property | Type | Description |
| -------- | ---- | ----------- |
| `LicenseId` | `string` | Local identifier in `LicenseRef-…` format |
| `ExtractedText` | `string` | Full verbatim text of the license |
| `Name` | `string?` | Optional human-readable license name |
| `CrossReferences` | `string[]` | Optional URIs to the license text elsewhere |
| `Comment` | `string?` | Optional explanatory comment |

Key methods:

- `DeepCopy()` — returns a new instance with all fields deep-copied
- `Enhance(SpdxExtractedLicensingInfo)` — fills in missing fields from another instance
- `Enhance(array, array)` — static method merging two arrays by matching on `LicenseId`
- `Validate(List<string>)` — appends validation issues to the supplied list
- `Same` — static `IEqualityComparer` comparing by `ExtractedText`

## Dependencies

- No external dependencies beyond base .NET BCL types
