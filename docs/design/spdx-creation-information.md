# SpdxCreationInformation Unit Design

## Purpose

`SpdxCreationInformation` captures the metadata about who created an SPDX document and when.
One instance is required per SPDX document. It enables provenance tracing and forward/backward
compatibility for processing tools.

## Design

`SpdxCreationInformation` is a sealed class with no base class.

Data members:

| Property | Type | Description |
| -------- | ---- | ----------- |
| `Creators` | `string[]` | Identifies the persons, organizations, or tools that created the document |
| `Created` | `string` | ISO 8601 UTC timestamp of document creation; may be empty for partially-constructed documents |
| `Comment` | `string?` | Optional creator comment |
| `LicenseListVersion` | `string?` | Optional SPDX license list version used |

Key methods:

- `DeepCopy()` — returns a new `SpdxCreationInformation` with all fields copied
- `Enhance(SpdxCreationInformation)` — fills in missing fields from another instance
- `Validate(List<string>)` — appends validation issues; validates `Created` format via regex when non-empty (empty `Created` is permitted and skips format validation)

## Dependencies

- `System.Text.RegularExpressions` — used internally to validate the `LicenseListVersion` field format
