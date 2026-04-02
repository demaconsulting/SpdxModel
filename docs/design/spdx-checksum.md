# SpdxChecksum Unit Design

## Purpose

`SpdxChecksum` represents an SPDX checksum — an algorithm-value pair used to verify the
integrity of files and packages. Supporting multiple algorithms provides flexibility across
different security policies and tooling ecosystems.

## Design

`SpdxChecksum` is a sealed class with no base class (not an `SpdxElement`).

Data members:

| Property | Type | Description |
| -------- | ---- | ----------- |
| `Algorithm` | `SpdxChecksumAlgorithm` | Identifies the hash algorithm (SHA1, SHA256, MD5, etc.) |
| `Value` | `string` | Lower-case hexadecimal digest value |

Key methods:

- `DeepCopy()` — returns a new `SpdxChecksum` with all fields copied
- `Enhance(SpdxChecksum)` — fills in missing fields from another instance
- `Validate(string, List<string>)` — appends validation issues to the supplied list
- `Same` — static `IEqualityComparer<SpdxChecksum>` comparing algorithm and value

## Dependencies

- `SpdxChecksumAlgorithm` (enum)
