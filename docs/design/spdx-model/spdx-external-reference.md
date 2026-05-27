## SpdxExternalReference

### Purpose

`SpdxExternalReference` represents a link from an SPDX package to an external resource, such as
a package registry URL, vulnerability database entry, or documentation site. External references
enrich SBOMs with contextual information from authoritative sources.

### Data Model

**Category**: `SpdxReferenceCategory` — Broad category of the reference (e.g., `SECURITY`,
`PACKAGE-MANAGER`, `OTHER`).

**Type**: `string` — Specific reference type within the category (e.g., `cpe23Type`, `purl`,
`advisory`).

**Locator**: `string` — URI or other identifier for the external resource.

**Comment**: `string?` — Optional explanatory comment.

### Key Methods

**DeepCopy**: Returns a new instance with all fields copied.

- *Parameters*: none.
- *Returns*: `SpdxExternalReference` — independent copy.
- *Preconditions*: none.
- *Postconditions*: The returned instance shares no mutable state with the original.

**Enhance**: Fills in missing fields from another instance.

- *Parameters*: `SpdxExternalReference other` — source of additional field values.
- *Returns*: `void`
- *Preconditions*: none.
- *Postconditions*: Empty fields in this instance are populated from `other`.

**Enhance (static array merge)**: Merges two external reference arrays by matching on category,
type, and locator.

- *Parameters*: `SpdxExternalReference[] base`, `SpdxExternalReference[] additions`.
- *Returns*: `SpdxExternalReference[]` — merged array.
- *Preconditions*: none.
- *Postconditions*: Matching entries are enhanced; new entries are appended.

**Validate**: Appends validation issues to the supplied list.

- *Parameters*: `string package` — owning package name for error messages;
  `List<string> issues` — list to append issues to.
- *Returns*: `void`
- *Preconditions*: none.
- *Postconditions*: Missing or invalid fields are recorded in `issues`.

**Same**: `static IEqualityComparer<SpdxExternalReference>` — compares by category, type, and
locator.

### Error Handling

Validation errors are collected into the `List<string>` passed to `Validate`. No exceptions are
thrown by `DeepCopy`, `Enhance`, or the static merge method.

### Dependencies

- **SpdxReferenceCategory** — enumeration of supported reference categories.
- **SpdxHelpers** — `EnhanceString` used in `Enhance`.

### SpdxReferenceCategory

`SpdxReferenceCategory` is an enumeration of the broad reference categories defined by the SPDX
specification.

#### Enum Values

| Value            | Integer | Description                                     |
|------------------|---------|-------------------------------------------------|
| `Missing`        | -1      | Sentinel indicating no category has been set.   |
| `Security`       | 0       | References to security-related information.     |
| `PackageManager` | 1       | References to package management systems.       |
| `PersistentId`   | 2       | References to software-heritage persistent IDs. |
| `Other`          | 3       | References that do not fit other categories.    |

#### FromText

Converts a category string from an SPDX document to the corresponding `SpdxReferenceCategory` enum
value.

- *Parameters*: `string category` — the raw text from the SPDX document (case-insensitive).
- *Returns*: `SpdxReferenceCategory.Missing` when `category` is an empty string; otherwise the
  matching enum value.
- *Preconditions*: none.
- *Postconditions*: none.
- *Exceptions*: `InvalidOperationException` — thrown when `category` is not a recognized SPDX
  reference category string.
- *Note*: `PACKAGE_MANAGER` (with underscore) is accepted as a backward-compatibility alias for
  `PACKAGE-MANAGER` (with hyphen). No equivalent underscore alias exists for any other category.

#### ToText

Converts a `SpdxReferenceCategory` enum value to its canonical SPDX text representation.

- *Parameters*: `SpdxReferenceCategory category` — the enum value to serialize.
- *Returns*: The canonical SPDX text (e.g., `"SECURITY"`, `"PACKAGE-MANAGER"`).
- *Preconditions*: `category` must not be `SpdxReferenceCategory.Missing`.
- *Postconditions*: none.
- *Exceptions*: `InvalidOperationException` — thrown when `category` is
  `SpdxReferenceCategory.Missing` or an unsupported enum value.

### Callers

- **SpdxPackage** — holds the `ExternalReferences` array.
- **Spdx2JsonDeserializer** — constructs `SpdxExternalReference` instances during deserialization.
- **Spdx2JsonSerializer** — serializes `SpdxExternalReference` instances to JSON.
