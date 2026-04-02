# SpdxExternalReference Unit Design

## Purpose

`SpdxExternalReference` represents a link from an SPDX package to an external resource, such as
a package registry URL, vulnerability database entry, or documentation site. External references
enrich SBOMs with contextual information from authoritative sources.

## Design

`SpdxExternalReference` is a sealed class with no base class.

Data members:

| Property | Type | Description |
| -------- | ---- | ----------- |
| `Category` | `SpdxReferenceCategory` | Broad category (e.g., SECURITY, PACKAGE-MANAGER) |
| `Type` | `string` | Specific reference type within the category (e.g., `cpe23Type`, `purl`) |
| `Locator` | `string` | URI or identifier for the external resource |
| `Comment` | `string?` | Optional explanatory comment |

Key methods:

- `DeepCopy()` — returns a new instance with all fields copied
- `Enhance(SpdxExternalReference)` — fills in missing fields from another instance
- `Enhance(array, array)` — static method merging two arrays by matching on category, type, and locator
- `Validate(string, List<string>)` — validates the reference; `string` parameter is the owning package name
- `Same` — static `IEqualityComparer` comparing by category, type, and locator

## Dependencies

- `SpdxReferenceCategory` (enum)
