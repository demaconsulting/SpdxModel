## SpdxFile

### Purpose

`SpdxFile` represents an individual file within an SPDX document, enabling fine-grained tracking
of source files, binaries, and other artifacts together with their licensing, checksums, and
contributor information.

### Data Model

**FileName**: `string` — Relative path of the file (e.g., `./src/main.c`). Used as the match
key when merging file arrays.

**FileTypes**: `SpdxFileType[]` — File type classifications (e.g., `SOURCE`, `BINARY`,
`DOCUMENTATION`).

**Checksums**: `SpdxChecksum[]` — Integrity checksums for this file using one or more algorithms.

**LicenseInfoInFiles**: `string[]` — License expressions found within the file.

**Comment**: `string?` — Optional free-text comment.

**Notice**: `string?` — Optional copyright notice text found in or about the file.

**Contributors**: `string[]` — Contributors to this file.

*Inherited from `SpdxLicenseElement`*: `Id`, `ConcludedLicense`, `LicenseComments`,
`CopyrightText`, `AttributionText`, `Annotations`.

### Key Methods

**DeepCopy**: Returns a fully deep-copied instance.

- *Parameters*: none.
- *Returns*: `SpdxFile` — independent copy including all arrays.
- *Preconditions*: none.
- *Postconditions*: The returned instance shares no mutable references with the original.

**Enhance**: Fills in missing fields from another instance.

- *Parameters*: `SpdxFile other` — source of additional field values.
- *Returns*: `void`
- *Preconditions*: none.
- *Postconditions*: Empty fields and empty arrays in this instance are populated from `other`.

**Enhance (static array merge)**: Merges two file arrays, matching on `FileName`.

- *Parameters*: `SpdxFile[] base`, `SpdxFile[] additions`.
- *Returns*: `SpdxFile[]` — merged array.
- *Preconditions*: none.
- *Postconditions*: Matching entries are enhanced; new entries are appended.

**Validate**: Appends validation issues to the supplied list.

- *Parameters*: `List<string> issues` — list to append issues to.
- *Returns*: `void`
- *Preconditions*: none.
- *Postconditions*: The following rules are checked and any violations are appended to `issues`:
  1. `FileName` must start with `"./"` (SPDX §4.1 — relative paths required).
  2. `Id` must match the `SPDXRef-[id-string]` format (SPDX §4.2).
  3. At least one SHA1 checksum must be present in `Checksums` (SPDX §4.4).
  Nested checksums and annotations are also validated.

**Same**: `static IEqualityComparer<SpdxFile>` — compares by `FileName`, with the condition that
two files with differing SHA1 checksums are considered distinct even if their `FileName` values
match.

#### SHA1 Tiebreaker (SPDX specification rationale)

The SPDX specification allows the same file path to be tracked at multiple revisions. If both
entries carry a SHA1 checksum and the values differ, they represent distinct file versions.
If either entry lacks a SHA1 checksum, identity falls back to `FileName` alone.

### Error Handling

Validation errors are collected into the `List<string>` passed to `Validate`. Nested checksums
are also validated. No exceptions are thrown by `DeepCopy`, `Enhance`, or the static merge method.

### Dependencies

- **SpdxLicenseElement** — abstract base class providing license and copyright fields.
- **SpdxChecksum** — checksum instances in the `Checksums` array.
- **SpdxFileType** — enumeration for file type classification.

### SpdxFileType and SpdxFileTypeExtensions

`SpdxFileType` is an enumeration of the file type categories defined by the SPDX specification.

#### Enum Values

| Value           | Description                                           |
|-----------------|-------------------------------------------------------|
| `Source`        | Human-readable source code.                           |
| `Binary`        | Compiled object, target image, or binary executable.  |
| `Archive`       | Archive file (e.g., zip, tar).                        |
| `Application`   | Application file.                                     |
| `Audio`         | Audio file.                                           |
| `Image`         | Image file.                                           |
| `Text`          | Human-readable text file.                             |
| `Video`         | Video file.                                           |
| `Documentation` | Documentation file.                                   |
| `Spdx`          | SPDX document.                                        |
| `Other`         | Other type not matching standard categories.          |

#### FromText

Converts a file type string from an SPDX document to the corresponding `SpdxFileType` enum value.

- *Parameters*: `string fileType` — the raw text from the SPDX document (case-insensitive).
- *Returns*: The matching `SpdxFileType` enum value.
- *Preconditions*: none.
- *Postconditions*: none.
- *Exceptions*: `InvalidOperationException` — thrown with a message identifying the unsupported
  value when `fileType` does not match any known SPDX file type string.

#### ToText

Converts a `SpdxFileType` enum value to its canonical SPDX text representation.

- *Parameters*: `SpdxFileType fileType` — the enum value to serialize.
- *Returns*: The canonical SPDX text (e.g., `"SOURCE"`, `"BINARY"`).
- *Preconditions*: `fileType` must be a supported enum value.
- *Postconditions*: none.
- *Exceptions*: `InvalidOperationException` — thrown when `fileType` is an unsupported enum value.

### Callers

- **SpdxDocument** — holds the `Files` array.
- **Spdx2JsonDeserializer** — constructs `SpdxFile` instances during deserialization.
- **Spdx2JsonSerializer** — serializes `SpdxFile` instances to JSON.
