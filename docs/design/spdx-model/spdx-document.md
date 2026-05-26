## SpdxDocument

### Purpose

`SpdxDocument` is the root container of the SPDX object model. It aggregates all SPDX elements
(packages, files, snippets, relationships, annotations, and extracted licensing information)
and exposes document-level operations such as validation, deep copy, and element retrieval.

### Data Model

**Name**: `string` — Human-readable document name.

**Version**: `string` — SPDX specification version string (e.g., `SPDX-2.3`).

**DataLicense**: `string` — License for the SPDX metadata itself (always `CC0-1.0` per the
SPDX specification).

**DocumentNamespace**: `string` — Unique URI namespace for this document; used to qualify
element IDs when referencing across documents.

**Comment**: `string?` — Optional free-text comment about the document.

**CreationInformation**: `SpdxCreationInformation` — Metadata about document authorship and
creation time.

**ExternalDocumentReferences**: `SpdxExternalDocumentReference[]` — References to external SPDX
documents that this document's elements may relate to.

**ExtractedLicensingInfo**: `SpdxExtractedLicensingInfo[]` — Non-standard license texts
extracted from software in this document.

**Packages**: `SpdxPackage[]` — All software packages described in the document.

**Files**: `SpdxFile[]` — All files described in the document.

**Snippets**: `SpdxSnippet[]` — All snippets described in the document.

**Relationships**: `SpdxRelationship[]` — All directed relationships between elements in the
document.

**Annotations**: `SpdxAnnotation[]` — Document-level annotations.

**Describes**: `string[]` — SPDX IDs of elements directly described by this document (used
when `DESCRIBES` relationships are not present).

### Key Methods

**DeepCopy**: Returns a fully independent deep copy of the entire document graph.

- *Parameters*: none.
- *Returns*: `SpdxDocument` — new instance with all arrays and nested objects deep-copied.
- *Preconditions*: none.
- *Postconditions*: The returned instance is structurally identical and shares no mutable
  references with the original.

**Validate**: Validates all contained elements and the document itself.

- *Parameters*: `List<string> issues` — list to append issues to; `bool ntia` — when `true`,
  also checks NTIA SBOM minimum elements requirements.
- *Returns*: `void`
- *Preconditions*: none.
- *Postconditions*: All validation issues found in the document and its elements are appended
  to `issues`.

**GetRootPackages**: Returns packages directly described by the document via `DESCRIBES`
relationships.

- *Parameters*: none.
- *Returns*: `IEnumerable<SpdxPackage>` — packages whose SPDX ID appears in a `DESCRIBES`
  relationship from this document.
- *Preconditions*: none.
- *Postconditions*: none.

**GetAllElements**: Enumerates all `SpdxElement` instances contained in the document.

- *Parameters*: none.
- *Returns*: `IEnumerable<SpdxElement>` — all packages, files, snippets, and annotations
  (including the document itself and per-element annotations). `SpdxRelationship` elements are
  excluded because relationships are not independently addressable elements and their inclusion
  would cause them to appear as duplicates in element-ID lookups.
- *Preconditions*: none.
- *Postconditions*: none.

**GetElement / GetElement\<T\>**: Retrieves an element by SPDX ID.

- *Parameters*: `string id` — the `SPDXRef-…` identifier.
- *Returns*: `SpdxElement?` or `T?` — the matching element, or `null` if not found.
- *Preconditions*: none.
- *Postconditions*: none.

**Same**: `static IEqualityComparer<SpdxDocument>` — compares documents by `Name` and root-package
identity. Two documents are considered the same when their `Name` values match AND their sets of
root packages (as returned by `GetRootPackages`) are sequence-equal under `SpdxPackage.Same`.
Used for deduplication scenarios.

### Error Handling

Validation errors are collected into the `List<string>` passed to `Validate`. The `Version`
field format is checked by a regular expression. No exceptions are thrown by `DeepCopy`,
`GetRootPackages`, `GetAllElements`, or `GetElement`.

### Dependencies

- **SpdxElement** — base class.
- **SpdxPackage**, **SpdxFile**, **SpdxSnippet** — package, file, and snippet elements.
- **SpdxRelationship** — relationship elements.
- **SpdxAnnotation** — annotation elements.
- **SpdxCreationInformation** — creation metadata.
- **SpdxExternalDocumentReference** — external document references.
- **SpdxExtractedLicensingInfo** — extracted licensing information.
- **System.Text.RegularExpressions** — `Version` field format validation.

### Callers

- **Spdx2JsonDeserializer** — produces `SpdxDocument` instances from JSON input.
- **Spdx2JsonSerializer** — consumes `SpdxDocument` instances to produce JSON output.
- **SpdxRelationships** — adds relationships to `SpdxDocument` instances.
- External consumers of this library — use `SpdxDocument` as the root of the in-memory SPDX
  model.
