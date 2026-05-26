## SpdxPackageVerificationCode

### Purpose

`SpdxPackageVerificationCode` represents an SPDX package verification code — a SHA1 digest
computed over the contents of a package (optionally excluding specified files). It provides
cryptographic assurance that package contents have not been modified.

### Data Model

**Value**: `string` — SHA1 hex digest computed over the sorted file checksums of the package.

**ExcludedFiles**: `string[]` — File paths excluded from the verification code computation
(e.g., the `.spdx` file itself).

### Key Methods

**DeepCopy**: Returns a new instance with all fields deep-copied.

- *Parameters*: none.
- *Returns*: `SpdxPackageVerificationCode` — independent copy.
- *Preconditions*: none.
- *Postconditions*: The returned instance shares no mutable references with the original.

**Enhance**: Fills in missing fields from another instance.

- *Parameters*: `SpdxPackageVerificationCode other` — source of additional field values.
- *Returns*: `void`
- *Preconditions*: none.
- *Postconditions*: Empty or null fields in this instance are populated from `other`.

**Validate**: Appends validation issues to the supplied list.

- *Parameters*: `string package` — owning package name for error messages;
  `List<string> issues` — list to append issues to.
- *Returns*: `void`
- *Preconditions*: none.
- *Postconditions*: An empty or malformed `Value` is recorded in `issues`.

**Same**: `static IEqualityComparer<SpdxPackageVerificationCode>` — compares by `Value` only.
`ExcludedFiles` is not considered for equality.

### Error Handling

Validation errors are collected into the `List<string>` passed to `Validate`. No exceptions are
thrown by `DeepCopy` or `Enhance`.

### Dependencies

N/A - no external dependencies beyond base .NET BCL types.

### Callers

- **SpdxPackage** — holds an optional `VerificationCode` instance.
- **Spdx2JsonDeserializer** — constructs `SpdxPackageVerificationCode` instances during deserialization.
- **Spdx2JsonSerializer** — serializes `SpdxPackageVerificationCode` instances to JSON.
