## SpdxChecksum

### Purpose

`SpdxChecksum` represents an SPDX checksum — an algorithm-value pair used to verify the
integrity of files and packages. Supporting multiple algorithms provides flexibility across
different security policies and tooling ecosystems.

### Data Model

**Algorithm**: `SpdxChecksumAlgorithm` — Identifies the hash algorithm (e.g., `SHA1`, `SHA256`,
`SHA512`, `MD5`). Defaults to `Missing` when not populated.

**Value**: `string` — Lower-case hexadecimal digest value produced by the algorithm.

### SpdxChecksumAlgorithm Enumeration

`SpdxChecksumAlgorithm` is an enumeration with a sentinel value and 17 named algorithm values:

| Enum Value     | SPDX Text Form          |
|----------------|-------------------------|
| `Missing`      | `""` (empty string)     |
| `Sha1`         | `SHA1`                  |
| `Sha224`       | `SHA224`                |
| `Sha256`       | `SHA256`                |
| `Sha384`       | `SHA384`                |
| `Sha512`       | `SHA512`                |
| `Md2`          | `MD2`                   |
| `Md4`          | `MD4`                   |
| `Md5`          | `MD5`                   |
| `Md6`          | `MD6`                   |
| `Sha3256`      | `SHA3-256`              |
| `Sha3384`      | `SHA3-384`              |
| `Sha3512`      | `SHA3-512`              |
| `Blake2B256`   | `BLAKE2b-256`           |
| `Blake2B384`   | `BLAKE2b-384`           |
| `Blake2B512`   | `BLAKE2b-512`           |
| `Blake3`       | `BLAKE3`                |
| `Adler32`      | `ADLER32`               |

`SpdxChecksumAlgorithmExtensions` provides two static helper methods:

**FromText**: Converts an SPDX algorithm text string to its enum value.

- *Signature*: `static SpdxChecksumAlgorithm FromText(string checksumAlgorithm)`
- *Parameters*: `string checksumAlgorithm` — the SPDX text form of the algorithm (case-insensitive).
- *Returns*: `SpdxChecksumAlgorithm` — the corresponding enum value; returns `Missing` for an
  empty string.
- *Exceptions*: `InvalidOperationException` — thrown when the input is a non-empty string that
  does not match any known algorithm name (comparison is case-insensitive).
- *Case-insensitivity*: The input is converted to upper-case before comparison, so `"sha1"`,
  `"SHA1"`, and `"Sha1"` are all accepted.

**ToText**: Converts an enum value to its SPDX text form.

- *Signature*: `static string ToText(this SpdxChecksumAlgorithm checksumAlgorithm)`
- *Parameters*: `SpdxChecksumAlgorithm checksumAlgorithm` — the algorithm enum value.
- *Returns*: `string` — the corresponding SPDX text representation.
- *Exceptions*: `InvalidOperationException` — thrown when the value is `Missing` or is a numeric
  value that does not correspond to any named enum member.

### Key Methods

**DeepCopy**: Returns a new `SpdxChecksum` with all fields copied.

- *Parameters*: none.
- *Returns*: `SpdxChecksum` — independent copy of this instance.
- *Preconditions*: none.
- *Postconditions*: The returned instance has the same field values and shares no mutable
  references with the original.

**Enhance (instance)**: Fills in missing fields from another instance.

- *Parameters*: `SpdxChecksum other` — source of additional field values.
- *Returns*: `void`
- *Preconditions*: none.
- *Postconditions*: Any empty or default-valued fields in this instance are replaced with
  non-empty values from `other`.

**Enhance (static array merge)**: Merges two checksum arrays by matching on algorithm and value.

- *Signature*: `static SpdxChecksum[] Enhance(SpdxChecksum[] array, SpdxChecksum[] others)`
- *Parameters*: `SpdxChecksum[] array` — base array; `SpdxChecksum[] others` — additions.
- *Returns*: `SpdxChecksum[]` — merged array.
- *Preconditions*: none.
- *Postconditions*: Entries in `others` that match an existing entry (by `Same.Equals`) are used
  to enhance the existing entry. Entries in `others` that do not match any existing entry are
  deep-copied and appended. The order of existing entries is preserved.

**Validate**: Appends validation issues to the supplied list.

- *Parameters*: `string parent` — identifier for error messages; `List<string> issues` — list
  to append issues to.
- *Returns*: `void`
- *Preconditions*: none.
- *Postconditions*: Missing or malformed fields are recorded in `issues`. Specifically:
  - If `Algorithm` is `Missing`, the message `"{parent} Invalid Checksum Algorithm Field - Missing"` is appended.
  - If `Algorithm` is a numeric value not defined in the enumeration, the message `"{parent} Invalid Checksum Algorithm Field - Unknown"` is appended.
  - If `Value` is empty, the message `"{parent} Invalid Checksum Value Field - Empty"` is appended.

**Same**: `static IEqualityComparer<SpdxChecksum>` — compares checksums by algorithm and value.
Used for deduplication when merging checksum arrays.

### Error Handling

Validation errors are collected into the `List<string>` passed to `Validate`. No exceptions are
thrown by `DeepCopy` or `Enhance`.

- **`FromText`**: throws `InvalidOperationException` when the input string is non-empty and
  unrecognized.
- **`ToText`**: throws `InvalidOperationException` when the algorithm value is `Missing` or is
  an out-of-range numeric value not corresponding to any named enum member.
- **`Validate` unknown-algorithm branch**: when `Algorithm` holds a numeric value that is not a named member of `SpdxChecksumAlgorithm`, `Validate` appends `"{parent} Invalid Checksum Algorithm Field - Unknown"` to the issues list.

### Dependencies

- **SpdxChecksumAlgorithm** — enumeration of supported hash algorithms.
- **SpdxHelpers** — `EnhanceString` utility used in the instance `Enhance` method.

### Callers

- **SpdxPackage** — holds the package-level `Checksums` array.
- **SpdxFile** — holds the file-level `Checksums` array.
- **SpdxExternalDocumentReference** — holds a single `Checksum` for document integrity.
- **Spdx2JsonDeserializer** — constructs `SpdxChecksum` instances during deserialization.
- **Spdx2JsonSerializer** — serializes `SpdxChecksum` instances to JSON.
