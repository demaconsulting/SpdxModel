# SpdxSnippet Unit Design

## Purpose

`SpdxSnippet` represents a portion of a file in an SPDX document. Snippets are used when a
specific range of bytes (or lines) within a file has different licensing or provenance from the
rest of the file, enabling granular compliance tracking for reused code segments.

## Design

`SpdxSnippet` is a sealed class that extends `SpdxLicenseElement`, inheriting `Id`,
`ConcludedLicense`, `CopyrightText`, and attribution fields.

Data members (beyond inherited fields):

| Property | Type | Description |
| -------- | ---- | ----------- |
| `SnippetFromFile` | `string` | SPDX ID of the file containing this snippet |
| `SnippetByteStart` | `int` | Inclusive start byte offset of the snippet |
| `SnippetByteEnd` | `int` | Inclusive end byte offset of the snippet |
| `SnippetLineStart` | `int` | Optional start line number |
| `SnippetLineEnd` | `int` | Optional end line number |
| `LicenseInfoInSnippet` | `string[]` | License expressions found in this snippet |
| `Comment` | `string?` | Optional comment |
| `Name` | `string?` | Optional human-readable snippet name |

Key methods:

- `DeepCopy()` — returns a fully deep-copied instance
- `Enhance(SpdxSnippet)` — fills in missing fields from another instance
- `Enhance(array, array)` — static method merging snippet arrays, matching on file ID and byte range
- `Validate(List<string>)` — appends validation issues to the supplied list
- `Same` — static `IEqualityComparer` comparing by file, byte start, and byte end

## Dependencies

- `SpdxLicenseElement` (base class)
