# SpdxDocument Unit Design

## Purpose

`SpdxDocument` is the root container of the SPDX object model. It aggregates all SPDX elements
(packages, files, snippets, relationships, annotations, and extracted licensing information)
and exposes document-level operations such as validation, deep copy, and root-package retrieval.

## Design

`SpdxDocument` is a sealed class that extends `SpdxElement` (inheriting the `Id` field).

Data members (key fields):

| Property | Type | Description |
| -------- | ---- | ----------- |
| `Name` | `string` | Document name |
| `Version` | `string` | SPDX specification version (e.g., `SPDX-2.3`) |
| `DataLicense` | `string` | License for the SPDX metadata itself |
| `DocumentNamespace` | `string` | Unique URI namespace for this document |
| `CreationInformation` | `SpdxCreationInformation` | Creation metadata |
| `ExternalDocumentReferences` | `SpdxExternalDocumentReference[]` | References to external SPDX documents |
| `ExtractedLicensingInfo` | `SpdxExtractedLicensingInfo[]` | Non-standard license texts |
| `Packages` | `SpdxPackage[]` | All packages in the document |
| `Files` | `SpdxFile[]` | All files in the document |
| `Snippets` | `SpdxSnippet[]` | All snippets in the document |
| `Relationships` | `SpdxRelationship[]` | All relationships in the document |
| `Annotations` | `SpdxAnnotation[]` | All annotations in the document |
| `Describes` | `string[]` | IDs of elements described by this document |

Key methods:

- `DeepCopy()` — returns a fully independent deep copy of the entire document graph
- `Validate(List<string>, bool ntia)` — validates all contained elements; optional NTIA SBOM minimum elements check
- `GetRootPackages()` — returns packages directly described by the document via `DESCRIBES` relationships
- `GetAllElements()` — enumerates all contained `SpdxElement` instances
- `GetElement(string id)` / `GetElement<T>(string id)` — retrieves an element by SPDX ID
- `Same` — static `IEqualityComparer<SpdxDocument>` comparing by document name

## Dependencies

- `SpdxElement` (base class)
- All other data model units: `SpdxPackage`, `SpdxFile`, `SpdxSnippet`, `SpdxRelationship`,
  `SpdxAnnotation`, `SpdxCreationInformation`, `SpdxExternalDocumentReference`,
  `SpdxExtractedLicensingInfo`
- `System.Text.RegularExpressions` — version field format validation
