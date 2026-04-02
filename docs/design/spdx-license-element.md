# SpdxLicenseElement Unit Design

## Purpose

`SpdxLicenseElement` is an abstract intermediate base class that adds license-related fields to
`SpdxElement`. It is the common ancestor of `SpdxPackage`, `SpdxFile`, and `SpdxSnippet`,
avoiding duplication of the concluded-license, copyright, and attribution fields.

## Design

`SpdxLicenseElement` is a public abstract class that extends `SpdxElement`.

Data members (beyond `SpdxElement.Id`):

| Property | Type | Description |
| -------- | ---- | ----------- |
| `ConcludedLicense` | `string` | License expression concluded by the SPDX document preparer |
| `LicenseComments` | `string?` | Explanation of the concluded license choice |
| `CopyrightText` | `string` | Copyright declarations text |
| `AttributionText` | `string[]` | Attribution notices required for use |

Key design decisions:

- Abstract (non-instantiable) — no direct consumers; always subclassed.
- Provides `EnhanceLicenseElement(SpdxLicenseElement)` protected helper analogous to
  `SpdxElement.EnhanceElement` for consistent field merging.

## Dependencies

- `SpdxElement` (base class)
