# SpdxPackageVerificationCode Unit Design

## Purpose

`SpdxPackageVerificationCode` represents an SPDX package verification code — a SHA1 digest
computed over the contents of a package (optionally excluding specified files). It provides
cryptographic assurance that package contents have not been modified.

## Design

`SpdxPackageVerificationCode` is a sealed class with no base class.

Data members:

| Property | Type | Description |
| -------- | ---- | ----------- |
| `Value` | `string` | SHA1 hex digest of the package contents |
| `ExcludedFiles` | `string[]` | Files excluded from the verification code computation |

Key methods:

- `DeepCopy()` — returns a new instance with all fields deep-copied
- `Enhance(SpdxPackageVerificationCode)` — fills in missing fields from another instance
- `Validate(string, List<string>)` — validates the code value; `string` parameter is the owning package name
- `Same` — static `IEqualityComparer` comparing by `Value` and `ExcludedFiles`

## Dependencies

- No external dependencies beyond base .NET BCL types
